using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NonProfitCRM.Data;
using NonProfitCRM.Models;

namespace NonProfitCRM.Controllers
{
    public class ContactsController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly string orgId;


        public ContactsController()
        {
            _unitOfWork = new UnitOfWork();
            orgId = "cac8a4ec-edd5-4554-8c91-24574282b9c1";
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            var contacts = await _unitOfWork.ContactRepository.GetManyAsync(c => c.OrgId == orgId);
            return View(contacts);
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contact = await _unitOfWork.ContactRepository.GetByIDAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            var contactTypes = _unitOfWork.ContactTypeRepository.GetAll().ToList();
            ViewBag.ContactType = new SelectList(contactTypes, "Id", "Name");
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrgId,Name,ContactTypeId,ParentContactId,PhoneNumber,Email,DonorScore,ProfilePicture,FacebookProfile,InstagramProfile,TwitterProfile,AddressLine1,AddressLine2,AddressStreet,AddressCity,AddressState,AddressCountry,AddressZipcode")] Contact contact)


        {
            if (ModelState.IsValid)
            {
                contact.OrgId = orgId;

                _unitOfWork.ContactRepository.Insert(contact);
                await _unitOfWork.SaveAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["ContactTypeId"] = new SelectList(_unitOfWork.ContactTypeRepository.GetAll(), "Id", "Name", contact.ContactTypeId);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contact = await _unitOfWork.ContactRepository.GetByIDAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            ViewData["ContactTypeId"] = new SelectList(_unitOfWork.ContactTypeRepository.GetAll(), "Id", "Name", contact.ContactTypeId);
            return View(contact);

        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrgId,Name,ContactTypeId,ParentContactId,PhoneNumber,Email,DonorScore,ProfilePicture,FacebookProfile,InstagramProfile,TwitterProfile,AddressLine1,AddressLine2,AddressStreet,AddressCity,AddressState,AddressCountry,AddressZipcode")] Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    contact.OrgId = orgId;
                    _unitOfWork.ContactRepository.Update(contact);
                    await _unitOfWork.SaveAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.Id))
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
            ViewData["ContactTypeId"] = new SelectList(_unitOfWork.ContactTypeRepository.GetAll(), "Id", "Name", contact.ContactTypeId);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _unitOfWork.ContactRepository.GetByIDAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _unitOfWork.ContactRepository.GetByIDAsync(id);
            _unitOfWork.ContactRepository.Delete(contact);
            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            var contact = _unitOfWork.ContactRepository.GetByID(id);
            if (contact == null)
            {
                return false;
            }
            return true;
        }
        // GET: Contacts/Countries/6
      /* public IActionResult Country(int id)
        {
            var Country=_unitOfWork.ContactRepository.GetByID(Id)
            List<Country> CountryList = new List<Country>();
            CountryList = (from product in _unitOfWork.Country select product).ToList();
            CountryList.Insert(0, new Country { Id = 0, Country = "Select" });
            ViewBag.ListOfCountries = CountryList;
            return View();


        }*/
    }
}
