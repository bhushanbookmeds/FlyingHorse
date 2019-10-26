using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NonProfitCRM.Data;
using NonProfitCRM.Models;
using NonProfitCRM.Services;

namespace NonProfitCRM.Controllers
{
    public class EventsController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly string orgId;
        private readonly IImageService _imageService;
        private readonly ICommonServices _commonServices;


        public EventsController(IImageService imageService)
        {
            _unitOfWork = new UnitOfWork();
            _imageService = imageService;
            _commonServices = new CommonServices();
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
                return NotFound();

            var pictures = await _unitOfWork.PictureRepository.GetManyAsync
            (p => p.EntityId == events.Id && p.EntityType.Equals("Event"));

            events.Pictures = pictures.Select(p => p.PictureUrl);

            return View(events);
        }


        //GET: Events/Details-Pictures/5
        [HttpPost]
        public async Task<IActionResult> Pictures(UploadFiles files)
        {

            foreach (IFormFile source in files.Picture)
            {
                Picture picture = new Picture();
                picture.EntityId = files.Id;
                picture.EntityType = "Event";

                picture.PictureUrl = await _imageService.ImageUpload(source, "Pictures");

                _unitOfWork.PictureRepository.Insert(picture);
                await _unitOfWork.SaveAsync();
            }

            return this.View();

        }


        // GET: Events/Create
        public IActionResult Create()
        {
            var country = _commonServices.GetCountries();
            ViewBag.AddressCountry = new SelectList(country, "Id", "Name");
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
        public async Task<IActionResult> Create([Bind("Id,OrgId,Name,CategoryId,CampaignId,StartDate," +
            "EndDate,ImagePath,ImageFile,Description,LongDescription,AddressLine1,AddressLine2,AddressStreet,AddressCity,AddressState,AddressCountry,AddressZipcode")] Event events)
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

                    events.ImagePath = await _imageService.ImageUpload(events.ImageFile, "Banners");

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
            var country = _commonServices.GetCountries();
            ViewBag.AddressCountry = new SelectList(country, "Id", "Name");
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrgId,Name,CategoryId,CampaignId,StartDate," +
            "EndDate,ImagePath,ImageFile,Description,LongDescription,AddressLine1,AddressLine2,AddressStreet,AddressCity,AddressState,AddressCountry,AddressZipcode")] Event events)
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

                    events.ImagePath = await _imageService.ImageUpload(events.ImageFile);

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

        public IActionResult Calendar()
        {
            return View();
        }


    }
}
