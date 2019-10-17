using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task<IActionResult> Create([Bind("Id,OrgId,Name,CategoryId,CampaignId,StartDate,StartTime," +
            "EndDate,EndTime,ImagePath,ImageFile,Description,AddressLine1,AddressLine2,AddressStreet,AddressCity,AddressState,AddressCountry,AddressZipcode")] Event events)
        {
            if (ModelState.IsValid)
            {
                events.OrgId = orgId;

                #region //Name is already Exist 
                var isExist = IsNameExist(events.Name);
                if (isExist)
                {
                    ModelState.AddModelError("Name", "This name is already exist");

                }
                #endregion
                else
                {

                    //Upload Image
                    string FileName = Path.GetFileNameWithoutExtension(events.ImageFile.FileName);
                    string extension = Path.GetExtension(events.ImageFile.FileName);
                    FileName = FileName + DateTime.Now.ToString("yymmssfff") + extension;
                    var mediaFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Banners");
                    var path = Path.Combine(mediaFolderPath, FileName);
                    bool exists = Directory.Exists(mediaFolderPath);
                    if (!exists)
                        Directory.CreateDirectory(mediaFolderPath);

                    using (var bits = new FileStream(path, FileMode.Create))
                    {
                        await events.ImageFile.CopyToAsync(bits);
                    }

                    string baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                    events.ImagePath = Path.Combine(baseUrl, "Banners", FileName);

                    _unitOfWork.EventRepository.Insert(events);
                    await _unitOfWork.SaveAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            //var campaign = _unitOfWork.CampaignRepository.GetAll().ToList();
            //ViewBag.Campaign = new SelectList(campaign, "Id", "Name");
            //var category = _unitOfWork.CampaignCategoryRepository.GetAll().ToList();
            //ViewBag.Category = new SelectList(category, "Id", "Name");

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

            //var campaign = _unitOfWork.CampaignRepository.GetAll().ToList();
            //ViewBag.Campaign = new SelectList(campaign, "Id", "Name");
            //var category = _unitOfWork.CampaignCategoryRepository.GetAll().ToList();
            //ViewBag.Category = new SelectList(category, "Id", "Name");
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrgId,Name,CategoryId,CampaignId,StartDate,StartTime," +
            "EndDate,EndTime,ImagePath,ImageFile,Description,AddressLine1,AddressLine2,AddressStreet,AddressCity,AddressState,AddressCountry,AddressZipcode")] Event events)
        {
            if (id != events.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    events.OrgId = orgId;
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
        public async Task<IActionResult> Delete(int? id)
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

            _unitOfWork.EventRepository.Delete(events);
            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var events = await _unitOfWork.EventRepository.GetByIDAsync(id);
            _unitOfWork.EventRepository.Delete(events);
            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            var events = _unitOfWork.EventRepository.GetByID(id);
            if (events == null)
            {
                return false;
            }
            return true;
        }

        [NonAction]
        public bool IsNameExist(string name)
        {

            {
                var events = _unitOfWork.EventRepository.DbSet.FirstOrDefault(n => n.Name.Equals(name));
                if (events == null)
                {
                    return false;
                }
                return true;
            }
        }
    }
}