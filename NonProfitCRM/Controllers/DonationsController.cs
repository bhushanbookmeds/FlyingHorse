using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NonProfitCRM.Models;

namespace NonProfitCRM.Controllers
{
    public class DonationsController : Controller
    {
        private readonly DB_3221_crmContext _context;

        public DonationsController()
        {
            _context = new DB_3221_crmContext();
        }

        // GET: Donations
        public async Task<IActionResult> Index()
        {
            var dB_3221_crmContext = _context.Donation.Include(d => d.Campaign).Include(d => d.Contact).Include(d => d.DonationType).Include(d => d.Event).Include(d => d.Org).Include(d => d.TransactionType);
            return View(await dB_3221_crmContext.ToListAsync());
        }
       
        // GET: Donations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donation
                .Include(d => d.Campaign)
                .Include(d => d.Contact)
                .Include(d => d.DonationType)
                .Include(d => d.Event)
                .Include(d => d.Org)
                .Include(d => d.TransactionType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        // GET: Donations/Create
        public IActionResult Create()
        {
            ViewData["CampaignId"] = new SelectList(_context.Campaign, "Id", "Name");
            ViewData["ContactId"] = new SelectList(_context.Contact, "Id", "Name");
            ViewData["DonationTypeId"] = new SelectList(_context.DonationType, "Id", "Name");
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name");
            ViewData["OrgId"] = new SelectList(_context.Organization, "Id", "Id");
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionType, "Id", "Name");
           
            return View();
            
        }

        // POST: Donations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrgId,EventId,CampaignId,ContactId,GuestDonation,GuestEmail,Amount,RecurringDonation,Date,TransactionTypeId,DonationTypeId")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CampaignId"] = new SelectList(_context.Campaign, "Id", "OrgId", donation.CampaignId);
            ViewData["ContactId"] = new SelectList(_context.Contact, "Id", "Name", donation.ContactId);
            ViewData["DonationTypeId"] = new SelectList(_context.DonationType, "Id", "Name", donation.DonationTypeId);
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Id", donation.EventId);
            ViewData["OrgId"] = new SelectList(_context.Organization, "Id", "Id", donation.OrgId);
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionType, "Id", "Name", donation.TransactionTypeId);
            return View(donation);
        }

        // GET: Donations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donation.FindAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            ViewData["CampaignId"] = new SelectList(_context.Campaign, "Id", "OrgId", donation.CampaignId);
            ViewData["ContactId"] = new SelectList(_context.Contact, "Id", "Name", donation.ContactId);
            ViewData["DonationTypeId"] = new SelectList(_context.DonationType, "Id", "Name", donation.DonationTypeId);
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Id", donation.EventId);
            ViewData["OrgId"] = new SelectList(_context.Organization, "Id", "Id", donation.OrgId);
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionType, "Id", "Name", donation.TransactionTypeId);
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
                    _context.Update(donation);
                    await _context.SaveChangesAsync();
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
            ViewData["CampaignId"] = new SelectList(_context.Campaign, "Id", "OrgId", donation.CampaignId);
            ViewData["ContactId"] = new SelectList(_context.Contact, "Id", "Name", donation.ContactId);
            ViewData["DonationTypeId"] = new SelectList(_context.DonationType, "Id", "Name", donation.DonationTypeId);
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Id", donation.EventId);
            ViewData["OrgId"] = new SelectList(_context.Organization, "Id", "Id", donation.OrgId);
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionType, "Id", "Name", donation.TransactionTypeId);
            return View(donation);
        }

        // GET: Donations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donation
                .Include(d => d.Campaign)
                .Include(d => d.Contact)
                .Include(d => d.DonationType)
                .Include(d => d.Event)
                .Include(d => d.Org)
                .Include(d => d.TransactionType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donation = await _context.Donation.FindAsync(id);
            _context.Donation.Remove(donation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonationExists(int id)
        {
            return _context.Donation.Any(e => e.Id == id);
        }
         public IActionResult Search(string term)
        {
            int phoneNumber;
            bool result = int.TryParse(term, out phoneNumber);
            IQueryable<Contact> contacts;

            if (result)
                contacts = _context.Contact.Where(x => x.PhoneNumber.Contains(term));
            else
                contacts = _context.Contact.Where(x => x.Name.Contains(term));

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
            var contact = await _context.Contact.FirstOrDefaultAsync(m => m.Id == id);

            return Json(contact);
        }

    }
}
