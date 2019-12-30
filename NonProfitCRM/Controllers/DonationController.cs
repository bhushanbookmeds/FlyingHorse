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
    public class DonationController : Controller
    {

        private NonProfitCrmDbContext db = new NonProfitCrmDbContext();
        private readonly IUnitOfWork _unitOfWork;
        private readonly string orgId;

        public DonationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            orgId = "cac8a4ec-edd5-4554-8c91-24574282b9c1";
        }
        public async Task<IActionResult> Index()
        {
            var donations = await _unitOfWork.DonationRepository.GetManyAsync(c => c.OrgId == orgId);
            var donationList = new List<Donation>();

            foreach (var donation in donations)
            {
                donationList.Add(donation);
            }

            return View(donationList.OrderByDescending(v => v.Id));
        }


        [HttpPost]
        public async Task<IActionResult> Index(string searchTerm)
        {
            if (!String.IsNullOrEmpty(searchTerm))
            {
                return View(db.Contact.Where(c => c.Name.Contains(searchTerm)).ToList());
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            
        }

        public async Task<IActionResult> Index1(string searchTerm)
        {
            List<int> requiredID = db.Contact.Where(e => e.Name.Contains(searchTerm)).Select(y => y.Id).ToList();
            foreach (var a in requiredID)
            {
                Donation donation = 
            }
            return View(donation);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amount,Date,RecurringDonation,GuestEmail")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                donation.OrgId = orgId;
                await _unitOfWork.DonationRepository.InsertAsync(donation);
                await _unitOfWork.SaveAsync();

               return RedirectToAction(nameof(Index));
            }

            return View(donation);
        }

        public JsonResult GetDonations(string term)
        {
            List<string> donations;
            
            donations = db.Contact.Where(x => x.Name.StartsWith(term)).Select(y => y.Name).ToList();

            return Json(donations);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            else
            {
                Donation donation = _unitOfWork.DonationRepository.GetByID(id);
                return View(donation);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? Id , [Bind("Amount,Date,RecurringDonation,GuestEmail")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                donation.OrgId = orgId;
                _unitOfWork.DonationRepository.Update(donation);
                await _unitOfWork.SaveAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            var donations = await _unitOfWork.DonationRepository.GetByIDAsync(id);
            _unitOfWork.DonationRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }
    }
}