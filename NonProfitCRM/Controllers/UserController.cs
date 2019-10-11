using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NonProfitCRM.Data;
using NonProfitCRM.Models;
using NonProfitCRM.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NonProfitCRM.Controllers
{
    [Authorize(Roles = "Admin,Organization")]
    public class UserController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly UnitOfWork _unitOfWork;
        private readonly IEncryptionService _encryptionService;
        private string OrgId = null;

        public UserController(IUserService userService,
                              IAuthenticationService authenticationService,
                              IEncryptionService encryptionService)
        {
            this._authenticationService = authenticationService;
            this._userService = userService;
            this._encryptionService = encryptionService;
            this._unitOfWork = new UnitOfWork();
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var currentCustomer = await _authenticationService.GetAuthenticatedCustomer();

                if (User.IsInRole("Organization"))
                    OrgId = currentCustomer.OrgId;

                var users = await _userService.GetUsers(OrgId);
                if (users != null)
                    return View(users);

                return RedirectToAction("Index", "Home");

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public async Task<IActionResult> Create()
        {
            var model = new RegistrationModel();

            if (User.IsInRole("Admin"))
                await SetOrgAndRoleViewbag();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegistrationModel registrationModel)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(registrationModel.Email))
                {
                    registrationModel.Email = registrationModel.Email.Trim();
                }

                var loggedInCustomer = await _authenticationService.GetAuthenticatedCustomer();

                registrationModel.CreatedBy = loggedInCustomer.Id;

                if (User.IsInRole("Organization"))
                {
                    registrationModel.OrgId = loggedInCustomer.OrgId;
                    registrationModel.UserRoleId = (int)UserRoleEnum.Employee;
                }

                var registrationResult = await _userService.RegisterUser(registrationModel);

                if (registrationResult)
                    return RedirectToAction("Index");
            }

            if (User.IsInRole("Admin"))
                await SetOrgAndRoleViewbag();

            return View(registrationModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userService.GetUserById(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            var model = Mapper.Map<Users, RegistrationModel>(user);

            if (User.IsInRole("Admin"))
                await SetOrgAndRoleViewbag();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RegistrationModel registrationModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userService.GetUserById(registrationModel.Id);

                    var updatedUser = PrepareUserModel(registrationModel, user);

                    await _userService.UpdateUser(updatedUser);

                    if (registrationModel.UserRoleId > 0)
                        await _userService.UpdateUserRole(updatedUser.Id, registrationModel.UserRoleId);
                }
                catch (Exception)
                {
                    if (User.IsInRole("Admin"))
                        await SetOrgAndRoleViewbag();

                    return View(registrationModel);
                }

                return RedirectToAction(nameof(Index));
            }

            if (User.IsInRole("Admin"))
                await SetOrgAndRoleViewbag();

            return View(registrationModel);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserById(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.DeleteUser(user);

            return Ok();
        }

        #region Utilities

        private Users PrepareUserModel(RegistrationModel registrationModel, Users user)
        {
            user.Name = registrationModel.Name;
            user.OrgId = registrationModel.OrgId;
            user.Password = _encryptionService.EncryptText(registrationModel.Password);
            user.PhoneNumber = registrationModel.PhoneNumber;
            user.ProfilePicture = registrationModel.ProfilePicture;
            user.UpdatedDate = DateTime.UtcNow;
            user.Email = registrationModel.Email;
            user.AddressLine1 = registrationModel.AddressLine1;
            user.AddressLine2 = registrationModel.AddressLine2;
            user.AddressCity = registrationModel.AddressCity;
            user.AddressStreet = registrationModel.AddressStreet;
            user.AddressCountry = registrationModel.AddressCountry;
            user.AddressState = registrationModel.AddressState;
            user.AddressZipcode = registrationModel.AddressZipcode;

            return user;
        }

        private async Task SetOrgAndRoleViewbag()
        {
            IEnumerable<Organization> organizations = await _unitOfWork.OrganizationRepository.GetManyAsync(org => !org.Name.Equals("Admin") && org.Id != "6562F517-EE73-4378-B9BD-8A8F64BB051C");
            IEnumerable<UserRole> userRoles = await _unitOfWork.UserRoleRepository.GetAllAsync();

            ViewBag.Organizations = new SelectList(organizations, "Id", "Name");
            ViewBag.UserRoles = new SelectList(userRoles, "Id", "Name");
        }

        #endregion
    }
}