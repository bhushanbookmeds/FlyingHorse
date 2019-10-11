using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NonProfitCRM.Data;
using NonProfitCRM.Models;

namespace NonProfitCRM.Controllers
{
    public class EventsController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly string orgId;

        public EventsController()
        {
            _unitOfWork = new UnitOfWork();
            orgId = "cac8a4ec-edd5-4554-8c91-24574282b9c1";
        }



        // GET: Events
        public async Task<IActionResult> Index()
        {
            var events = await _unitOfWork.EventRepository.GetManyAsync(c => c.OrgId == orgId);
            return View(events);
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var events = await _unitOfWork.EventRepository.GetByIDAsync(id);
            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            var organization = _unitOfWork.OrganizationRepository.GetAll().ToList();
            ViewBag.Organization = new SelectList(organization, "Id", "Name");
            var campaign = _unitOfWork.CampaignRepository.GetAll().ToList();
            ViewBag.Campaign = new SelectList(campaign, "Id", "Name");
            var category = _unitOfWork.CampaignCategoryRepository.GetAll().ToList();
            ViewBag.Category = new SelectList(category, "Id", "Name");

            return View();
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrgId,Name,CategoryId,CampaginId,Date," +
            "AddressLine1,AddressLine2,AddressStreet,AddressCity,AddressState,AddressCountry,AddressZipcode")] Event events)
        {
            if (ModelState.IsValid)
            {
                events.OrgId = orgId;

                _unitOfWork.EventRepository.Insert(events);
                await _unitOfWork.SaveAsync();

                return RedirectToAction(nameof(Index));
            }
            //ViewData["ContactTypeId"] = new SelectList(_unitOfWork.ContactTypeRepository.GetAll(), "Id", "Name", contact.ContactTypeId);
            return View(events);
        }

        // GET: Events/Edit/5
         public async Task<IActionResult> Edit(int? id)
        { 
            if (id == null)
            {
                return NotFound();
            }
            var events = await _unitOfWork.EventRepository.GetByIDAsync(id);
            var organization = _unitOfWork.OrganizationRepository.GetAll().ToList();
            ViewBag.Organization = new SelectList(organization, "Id", "Name");
            var campaign = _unitOfWork.CampaignRepository.GetAll().ToList();
            ViewBag.Campaign = new SelectList(campaign, "Id", "Name");
            var category = _unitOfWork.CampaignCategoryRepository.GetAll().ToList();
            ViewBag.Category = new SelectList(category, "Id", "Name");
            if (events == null)
            {
                return NotFound();
            }
            //ViewData["ContactTypeId"] = new SelectList(_unitOfWork.ContactTypeRepository.GetAll(), "Id", "Name", contact.ContactTypeId);
            return View(events);
          }

        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrgId,Name,CategoryId,CampaginId,Date," +
            "AddressLine1,AddressLine2,AddressStreet,AddressCity,AddressState,AddressCountry,AddressZipcode")] Event events)
        {
            if (id != events.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.EventRepository.Update(events);
                    await _unitOfWork.SaveAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(events.Id))
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
            //ViewData["ContactTypeId"] = new SelectList(_unitOfWork.ContactTypeRepository.GetAll(), "Id", "Name", contact.ContactTypeId);
            return View(events);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Events/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private bool EventExists(int id)
        {
            var events = _unitOfWork.ContactRepository.GetByID(id);
            if (events == null)
            {
                return false;
            }
            return true;
        }


    }
}