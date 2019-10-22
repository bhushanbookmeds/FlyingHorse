using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NonProfitCRM.Data;
using NonProfitCRM.Models;
using System.Runtime.InteropServices;
using NonProfitCRM.Services;
//using Excel = Microsoft.Office.Interop.Excel;
//using ImportExcelFileInASPNETMVC.Model



namespace NonProfitCRM.Controllers
{
    public class ContactsController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ICommonServices _commonServices;

        private readonly string orgId;


        public ContactsController()
        {
            _unitOfWork = new UnitOfWork();
            _commonServices = new CommonServices();

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
                ViewBag.ContactTypeId = new SelectList(contactTypes, "Id", "Name");
                var country = _commonServices.GetCountries();
                ViewBag.AddressCountry = new SelectList(country, "Id", "Name");
                    
            return View();

        }
        public IActionResult State(int CountryId)
        {
            var states = _commonServices.GetStates(CountryId);
            
            return Json(states);
        }




        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrgId,Name,ContactTypeId,ParentContactId,PhoneNumber,Email," +
            "DonorScore,ProfilePicture,FacebookProfile,InstagramProfile,TwitterProfile,Age,Gender,AddressLine1,AddressLine2,AddressStreet," +
            "AddressCity,AddressState,AddressCountry,AddressZipcode")] Contact contact)


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
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrgId,Name,ContactTypeId,ParentContactId,PhoneNumber,Email,DonorScore,ProfilePicture," +
            "FacebookProfile,InstagramProfile,TwitterProfile,AddressLine1,AddressLine2,AddressStreet,AddressCity,AddressState,AddressCountry,AddressZipcode")] Contact contact)
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
       
       
        public ActionResult ReadExcel()
        {
            return View();
        }
    }
      //  [HttpPost]
        //public ActionResult ReadExcel(HttpPostedfileBase upload)
        //{

        //}

    
}
    

