using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NonProfitCRM.Data;
using NonProfitCRM.Models;
using NonProfitCRM.Services;
using System.Threading.Tasks;

namespace NonProfitCRM.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly UnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public AccountController(IAuthenticationService authenticationService,
                                 IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _unitOfWork = new UnitOfWork();
        }

        public IActionResult Login()
        {
            var model = new LoginModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }

            var validationResult = await _authenticationService.ValidateUser(loginModel);

            switch (validationResult)
            {
                case LoginResults.Successful:
                    {
                        var customer = await _unitOfWork.UsersRepository.GetAsync(usr => usr.Email == loginModel.UsernameOrEmail);

                        //sign in new customer
                        await _authenticationService.SignIn(customer, loginModel.RememberMe);

                        if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                            return RedirectToAction("Index", "Home");

                        return Redirect(returnUrl);
                    }
                case LoginResults.CustomerNotExist:
                    ModelState.AddModelError("", "Customer does not exist");
                    break;
                case LoginResults.WrongPassword:
                default:
                    ModelState.AddModelError("", "Incorrect credentials");
                    break;
            }

            return View(loginModel);
        }

        public async Task<IActionResult> LogOut()
        {
            await _authenticationService.SignOut();

            return RedirectToAction("Login", "Account");
        }

        //[Authorize]
        public virtual IActionResult Register()
        {
            var model = new RegistrationModel();

            return View(model);
        }

        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Register(RegistrationModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(model.Email))
                {
                    model.Email = model.Email.Trim();
                }

                var loggedInCustomer = await _authenticationService.GetAuthenticatedCustomer();

                model.OrgId = loggedInCustomer.OrgId;
                model.CreatedBy = loggedInCustomer.Id;
                model.UserRoleId = (int)UserRoleEnum.Organization;

                var registrationResult = await _userService.RegisterUser(model);

                if (!registrationResult)
                    return View(model);

                if (!string.IsNullOrWhiteSpace(returnUrl))
                    Redirect(returnUrl);

                RedirectToAction("Index", "Home");
            }

            return View(model);
        }

    }
}