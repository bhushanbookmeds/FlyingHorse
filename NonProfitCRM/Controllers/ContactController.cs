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

namespace NonProfitCRM.Controllers
{
    public class ContactController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommonService _commonService;
        private readonly IContactService _contactService;             // injection 

        public ContactController(IUnitOfWork unitOfWork, ICommonService commonService, IContactService contactService)
        {
            _unitOfWork = unitOfWork;
            _commonService = commonService;
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _unitOfWork.ContactRepository.GetManyAsync(c => c.Id == Id);
            var contactsList = new List<Contact>();

            foreach (var contacts in Contact)
            {
                volunteer.AddressCountry = _unitOfWork.CountryRepository.GetByID(Convert.ToInt32(volunteer.AddressCountry))?.Name;

                // volunteer.AddressState = _unitOfWork.StateRepository.GetByID(Convert.ToInt32(volunteer.AddressState))?.Name;
                volunteersList.Add(volunteer);
            }

            return View(volunteersList.OrderByDescending(v => v.Id));
        }

        public async Task<IActionResult> Create()
        {
            await InitializeViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            await _contactService.Insert(contact);
            return View();
        }
        public async Task<IActionResult> Update(string Id)
        {
            int ContactId = Convert.ToInt32(Id);
            var emp = _contactService.GetId(ContactId);
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Contact contact)
        {
            var emp = await _contactService.GetId(contact.Id);
            if (emp != null)
            {
                emp.Name = contact.Name;
                emp.InstagramProfile = contact.InstagramProfile;
                emp.Organisation = contact.Organisation;
                emp.OrgId = contact.OrgId;
                emp.ParentContactId = contact.ParentContactId;
                emp.PhoneNumber = contact.PhoneNumber;
                emp.ProfilePicture = contact.ProfilePicture;
                emp.TwitterProfile = contact.TwitterProfile;
                emp.ContactTypeId = contact.ContactTypeId;
                emp.Email = contact.Email;
                emp.DonorScore = contact.DonorScore;
                emp.FacebookProfile = contact.FacebookProfile;
                emp.AddressLine1 = contact.AddressLine1;
                emp.AddressLine2 = contact.AddressLine2;
                emp.AddressStreet = contact.AddressStreet;
                emp.AddressCity = contact.AddressCity;
                emp.AddressState = contact.AddressState;
                emp.AddressCountry = contact.AddressCountry;
                emp.AddressZipcode = contact.AddressZipcode;
                emp.Age = contact.Age;
                emp.Gender = contact.Gender;
                emp.PhoneCode = contact.PhoneCode;
            }
            await _contactService.Update(contact);
            return View();
        }

        public async Task<IActionResult> Delete(string id)
        {
            int ContactId = Convert.ToInt32(id);
            var contact = await _contactService.GetId(ContactId);
            if (contact != null)
            {
                await _contactService.Delete(contact);
            }
            return View();
        }

        public IActionResult State(int countryId)
        {
            var states = _commonService.GetStates(countryId);
            return Json(states);
        }


        private async Task InitializeViewBag()
        {
            var countries = await _commonService.GetCountries();
            ViewBag.AddressCountry = new SelectList(countries, "Id", "Name");
        }
    }
}