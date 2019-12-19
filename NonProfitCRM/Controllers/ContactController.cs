using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Domain;
using Data;
using Service.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using NonProfitCRM.Models;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace NonProfitCRM.Controllers
{
    public class ContactController : Controller
    {
        private NonProfitCrmDbContext db = new NonProfitCrmDbContext();
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommonService _commonService;
        private readonly IContactService _contactService;               // injection
        private readonly string orgId;                                  
        private readonly int CountryId;

        public ContactController(IUnitOfWork unitOfWork, ICommonService commonService, IContactService contactService)
        {
            _unitOfWork = unitOfWork;
            _commonService = commonService;
            _contactService = contactService;
            orgId = "cac8a4ec-edd5-4554-8c91-24574282b9c1";
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _unitOfWork.ContactRepository.GetManyAsync(c => c.OrgId == orgId);
            var contactList = new List<Contact>();

            foreach (var contact in contacts)
            {
                contact.AddressCountry = _unitOfWork.CountryRepository.GetByID(Convert.ToInt32(contact.AddressCountry))?.Name;

                // volunteer.AddressState = _unitOfWork.StateRepository.GetByID(Convert.ToInt32(volunteer.AddressState))?.Name;
                contactList.Add(contact);
            }

            return View(contactList.OrderByDescending(v => v.Id));
        }

        public async Task<IActionResult> Index1(string searchBy , string search)
        {
            if(searchBy == "Name")
            {
                return View(db.Contact.Where(c=> c.Name.Contains(search)).ToList());
            }
            else
            {
                int id = Convert.ToInt32(search);
                return View(db.Contact.Where(c => c.Id == id).ToList());
            }
        }

        // GET: Contact/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contacts = await _unitOfWork.ContactRepository.GetByIDAsync(id);
            if (contacts == null)
            {
                return NotFound();
            }

            return View(contacts);
        }

        // GET: Volunteers/Create
        public async Task<IActionResult> Create()
        {

            var country = await _commonService.GetCountries();
            ViewBag.AddressCountry = new SelectList(country, "Id", "Name");

            int CountryId = 99;

            var states = await _commonService.GetStates(CountryId);
            ViewBag.AddressState = new SelectList(states, "Id", "Name");

            var types = await _commonService.GetTypes();
            ViewBag.ContactTypeId = new SelectList(types, "Id", "Name");

            //var types = await _commonService.GetType();

            return View();
        }

        public async Task<IActionResult> State(int CountryId)
        {
            var states = await _commonService.GetStates(CountryId);

            return Json(states);
        }

        public async Task<IActionResult> CntactType()
        {
            var types = await _commonService.GetTypes();

            return Json(types);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrgId,Name,PhoneCode,PhoneNumber,Age,Email,InstagramProfile,FacebookProfile,TwitterProfile,AddressLine1,AddressLine2,AddressStreet,AddressCity,AddressState,AddressCountry,AddressZipcode,Gender,ContactTypeId")] Contact contacts)
        {
            var country = _unitOfWork.CountryRepository.GetByID(Convert.ToInt32(contacts.AddressCountry));

            var states = _unitOfWork.StateRepository.GetByID(Convert.ToInt32(contacts.AddressState));

            var types = _unitOfWork.ContactTypeRepository.GetByID(Convert.ToInt32(contacts.ContactType));

            if (ModelState.IsValid)
            {
                contacts.OrgId = orgId;
                await _unitOfWork.ContactRepository.InsertAsync(contacts);
                await _unitOfWork.SaveAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressCountry"] = new SelectList(await _commonService.GetCountries(), "Id", "Name", contacts.AddressCountry);

            ViewData["AddressState"] = new SelectList(await _commonService.GetStates(CountryId), "Id", "Name", contacts.AddressState);

            ViewData["ContactTypeId"] = new SelectList(await _commonService.GetTypes(), "Id", "Name", contacts.ContactType);

            return View(contacts);
        }

        // GET: Volunteers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var contacts = await _unitOfWork.ContactRepository.GetByIDAsync(id);
            //var phoneNumber = volunteers.PhoneNumber;
            //var phoneCode = volunteers.PhoneCode;
            //volunteers.PhoneNumber = phoneNumber.Substring((phoneNumber.Length - 10));
            //volunteers.ImagePath = await _imageService.ImageUpload(volunteers.ImageFile, "Banners");
            //volunteers.PhoneCode = volunteers.AddressCountry;
            //volunteers.PhoneNumber = volunteers.PhoneCode.ToString() + volunteers.PhoneNumber;


            var country = await _commonService.GetCountries();

            //volunteers.AddressCountry = volunteers.AddressState
            ViewBag.AddressCountry = new SelectList(country, "Id", "Name", contacts.AddressCountry);
            //int x = Int32.TryParse(AddressCountry.ToString);
            int x = Convert.ToInt32(contacts.AddressCountry);
            var states = await _commonService.GetStates(x);
            ViewBag.AddressState = new SelectList(states, "Id", "Name", contacts.AddressState);
            //var phoneisd = _commonService.GetPhoneCode();
            //ViewBag.PhoneCode = new SelectList(phoneisd, "Id", "PhoneCode", contacts.AddressCountry);
            var types = await _commonService.GetTypes();
            ViewBag.ContactTypeId = new SelectList(types, "Id", "Name", contacts.ContactType);

            if (contacts == null)
            {
                return NotFound();
            }

            //ViewData["VolunteersTypeId"] = new SelectList(_unitOfWork.VolunteersTypeRepository.GetAll(), "Id", "Name", volunteers.VolunteersTypeId);
            return View(contacts);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,OrgId,Name,PhoneCode,PhoneNumber,Age,Email,InstagramProfile,FacebookProfile,TwitterProfile,AddressLine1,AddressLine2,AddressStreet,AddressCity,AddressState,AddressCountry,AddressZipcode,Gender,ContactTypeId")] Contact contacts)

        {

            var country = _unitOfWork.CountryRepository.GetByID(Convert.ToInt32(contacts.AddressCountry));


            var state = _unitOfWork.StateRepository.GetByID(Convert.ToInt32(contacts.AddressState));
            //contacts.PhoneCode = country.PhoneCode.ToString();
            //contacts.PhoneNumber = contacts.PhoneCode.ToString() + volunteers.PhoneNumber;
            var types = _unitOfWork.ContactTypeRepository.GetByID(Convert.ToInt32(contacts.ContactTypeId));

            if (id != contacts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // volunteers.ImagePath = await _imageService.ImageUpload(volunteers.ImageFile, "Banners");
                    contacts.OrgId = orgId;
                    //volunteers.ImagePath = await _imageService.ImageUpload(volunteers.ImageFile, "Banners");
                    //volunteers.ImagePath = await _imageService.ImageUpload(volunteers.ImageFile, "Banners");
                    _unitOfWork.ContactRepository.Update(contacts);
                    await _unitOfWork.SaveAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactsExists(contacts.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            // ViewData["VolunteersTypeId"] = new SelectList(_unitOfWork.VolunteersTypeRepository.GetAll(), "Id", "Name", volunteers.VolunteersTypeId);
            ViewData["AddressCountry"] = new SelectList(await _commonService.GetCountries(), "Id", "Name", contacts.AddressCountry);
            //int n = Convert.ToInt32(volunteers.AddressState);
            //ViewData["AddressState"] = new SelectList(_commonServices.GetStates(n), "Id", "Name", volunteers.AddressState);
            //ViewData["PhoneCode"] = new SelectList(_commonService.GetPhoneCode(), "Id", "PhoneCode", contacts.PhoneCode);
            ViewData["ContactTypeId"] = new SelectList(await _commonService.GetTypes(), "Id", "Names", contacts.ContactTypeId);
            return View(contacts);
        }

        // GET: Volunteers/Delete/5


        // POST: Volunteers/Delete/5
        [HttpDelete]
        public async Task Delete(int id)
        {
            var contacts = await _unitOfWork.ContactRepository.GetByIDAsync(id);
            _unitOfWork.ContactRepository.Delete(contacts);
            await _unitOfWork.SaveAsync();

        }

        private bool ContactsExists(int id)
        {
            var contacts = _unitOfWork.ContactRepository.GetByID(id);
            if (contacts == null)
            {
                return false;
            }
            return true;
        }
    }
}