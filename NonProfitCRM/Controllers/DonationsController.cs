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
    public class DonationsController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly string orgId;


        public DonationsController()
        {
            _unitOfWork = new UnitOfWork();
            orgId = "cac8a4ec-edd5-4554-8c91-24574282b9c1";
        }

        // GET: Donations
        public async Task<IActionResult> Index(int? id, Details_Donation details_Donation)
        {
            var donations = await _unitOfWork.DonationRepository.GetManyAsync(c => c.OrgId == orgId);

            IList<Details_Donation> list_details_donations = new List<Details_Donation>();
            foreach (var donation in donations)
            {
                var campignName = string.Empty;
                var eventName = string.Empty;
                var contactName = string.Empty;
                var DonationTypeId = string.Empty;
                var TransactionTypeId = string.Empty;
                var Date = string.Empty;


                if (donation.ContactId != null)
                {
                    var contacts = await _unitOfWork.ContactRepository.GetByIDAsync(donation.ContactId);
                    contactName = contacts.Name;
                }

                if (donation.EventId != null)
                {
                    var events = await _unitOfWork.EventRepository.GetByIDAsync(donation.EventId);
                    eventName = events.Name;
                }


                if (donation.CampaignId != null)
                {
                    var campaings = await _unitOfWork.CampaignRepository.GetByIDAsync(donation.CampaignId);
                    campignName = campaings.Name;
                }

                if (donation.DonationTypeId != null)
                {
                    var donationTypes = await _unitOfWork.DonationTypeRepository.GetByIDAsync(donation.DonationTypeId);
                    DonationTypeId = donationTypes.Name;
                }
                if (donation.TransactionTypeId != null)
                {
                    var TransactionTypes = await _unitOfWork.TransactionTypeRepository.GetByIDAsync(donation.TransactionTypeId);
                    TransactionTypeId = TransactionTypes.Name;
                }


                Details_Donation details_donations = new Details_Donation();

                details_donations.Name = contactName;
                details_donations.Date = donation.Date;
                details_donations.EventName = eventName;
                details_donations.CampaignName = campignName;
                details_donations.DonationType = DonationTypeId;
                details_donations.GuestDonation = donation.GuestDonation;
                details_donations.TransactionTypeId = TransactionTypeId;
                details_donations.Amount = donation.Amount;
                details_donations.Id = donation.Id;

                list_details_donations.Add(details_donations);
            }



            return View(list_details_donations);
        }


        // GET: Donations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            {
                if (id == null)
                {
                    return NotFound();
                }
                var donation = await _unitOfWork.DonationRepository.GetByIDAsync(id);

                if (donation == null)
                {
                    return NotFound();
                }


                return View(donation);


            }
        }

        // GET: Donations/Create
        public IActionResult Create()
        {
            var CampaignId = _unitOfWork.CampaignRepository.GetAll().ToList();
            ViewBag.CampaignId = new SelectList(CampaignId, "Id", "Name");

            var EventId = _unitOfWork.EventRepository.GetAll().ToList();
            ViewBag.EventId = new SelectList(EventId, "Id", "Name");

            var ContactId = _unitOfWork.ContactRepository.GetAll().ToList();
            ViewBag.ContactId = new SelectList(ContactId, "Id", "Name");
            var DonationTypeId = _unitOfWork.DonationTypeRepository.GetAll().ToList();
            ViewBag.DonationTypeId = new SelectList(DonationTypeId, "Id", "Name");
            var TransactionTypeId = _unitOfWork.TransactionTypeRepository.GetAll().ToList();
            ViewBag.TransactionTypeId = new SelectList(TransactionTypeId, "Id", "Name");
            var ContactTypeId = _unitOfWork.ContactTypeRepository.GetAll().ToList();
            ViewBag.ContactTypeId = new SelectList(ContactTypeId, "Id", "Name");


            return View();

        }

        // POST: Donations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrgId,EventId,CampaignId,ContactId,GuestDonation,GuestEmail,Amount,RecurringDonation,Date,TransactionTypeId,DonationTypeId,ContactTypeId")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                donation.OrgId = orgId;

                _unitOfWork.DonationRepository.Insert(donation);
                await _unitOfWork.SaveAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["EventId"] = new SelectList(_unitOfWork.EventRepository.GetAll(), "Id", "Name", donation.EventId);
            ViewData["CampaignId"] = new SelectList(_unitOfWork.CampaignRepository.GetAll(), "Id", "Name", donation.CampaignId);
            ViewData["ContactId"] = new SelectList(_unitOfWork.ContactRepository.GetAll(), "Id", "Name", donation.ContactId);
            ViewData["DonationTypeId"] = new SelectList(_unitOfWork.DonationTypeRepository.GetAll(), "Id", "Name", donation.DonationTypeId);
            ViewData["TransactionTypeId"] = new SelectList(_unitOfWork.TransactionTypeRepository.GetAll(), "Id", "Name", donation.TransactionTypeId);
            ViewData["ContactTypeId"] = new SelectList(_unitOfWork.ContactTypeRepository.GetAll(), "Id", "Name");


            return View(donation);
        }
        // GET: Donation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var donation = await _unitOfWork.DonationRepository.GetByIDAsync(id);
            if (donation == null)
            {
                return NotFound();
            }

            ViewData["EventId"] = new SelectList(_unitOfWork.EventRepository.GetAll(), "Id", "Name", donation.EventId);
            ViewData["CampaignId"] = new SelectList(_unitOfWork.CampaignRepository.GetAll(), "Id", "Name", donation.CampaignId);
            ViewData["ContactId"] = new SelectList(_unitOfWork.ContactRepository.GetAll(), "Id", "Name", donation.ContactId);
            ViewData["DonationTypeId"] = new SelectList(_unitOfWork.DonationTypeRepository.GetAll(), "Id", "Name", donation.DonationTypeId);
            ViewData["TransactionTypeId"] = new SelectList(_unitOfWork.TransactionTypeRepository.GetAll(), "Id", "Name", donation.TransactionTypeId);


            return View(donation);
        }

        // POST: Donations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrgId,EventId,CampaignId,ContactId,GuestDonation,GuestEmail,Amount,RecurringDonation,Date,TransactionTypeId,DonationTypeId")] Donation donation)
        {
            if (id != donation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.DonationRepository.Update(donation);
                    await _unitOfWork.SaveAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonationExists(donation.Id))
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

            ViewData["CampaignId"] = new SelectList(_unitOfWork.CampaignRepository.CampaignId, "Id", "OrgId", donation.CampaignId);
            ViewData["ContactId"] = new SelectList(_unitOfWork.ContactRepository.Contact, "Id", "Name", donation.ContactId);
            ViewData["DonationTypeId"] = new SelectList(_unitOfWork.DonationTypeRepository.DonationType, "Id", "Name", donation.DonationTypeId);
            ViewData["EventId"] = new SelectList(_unitOfWork.EventRepository.Event, "Id", "Id", donation.EventId);
            ViewData["OrgId"] = new SelectList(_unitOfWork.OrganizationRepository.Organization, "Id", "Id", donation.OrgId);
            ViewData["TransactionTypeId"] = new SelectList(_unitOfWork.TransactionTypeRepository.TransactionType, "Id", "Name", donation.TransactionTypeId);
            return View(donation);
        }

        // GET: Donations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _unitOfWork.DonationRepository.GetByIDAsync(id);
            if (events == null)
            {
                return NotFound();
            }

            _unitOfWork.DonationRepository.Delete(events);
            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donation = await _unitOfWork.DonationRepository.GetByIDAsync(id);
            _unitOfWork.DonationRepository.Delete(donation);
            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool DonationExists(int id)
        {
            var donation = _unitOfWork.DonationRepository.GetByID(id);
            if (donation == null)
            {
                return false;
            }
            return true;
        }
        public IActionResult Search(string term)
        {
            int phoneNumber;
            bool result = int.TryParse(term, out phoneNumber);
            IQueryable<Contact> contacts;

            if (result)
                contacts = _unitOfWork.ContactRepository.GetDbSet().Where(x => x.PhoneNumber.Contains(term));
            else
                contacts = _unitOfWork.ContactRepository.GetDbSet().Where(x => x.Name.Contains(term));

            var list = (from c in contacts
                        select new
                        {
                            label = c.Name,
                            id = c.Id.ToString()
                        }).ToList();

            return Json(list);
        }
        public async Task<IActionResult> GetContactDetails(int id)
        {
            var contact = await _unitOfWork.ContactRepository.GetDbSet().FirstOrDefaultAsync(m => m.Id == id);

            return Json(contact);
        }

    }
}
