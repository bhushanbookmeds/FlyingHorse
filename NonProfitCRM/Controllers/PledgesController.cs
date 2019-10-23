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
    public class PledgesController : Controller
    {
        private readonly DB_3221_crmContext _context;
        private readonly UnitOfWork _unitOfWork;

        public PledgesController(DB_3221_crmContext context, UnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: Pledges

        public async Task<IActionResult> Index()
        {
            string[] include = new string[2] { "Campaign", "Event" };
            var pledges = _unitOfWork.PledgeRepository.GetWithInclude(pledge => pledge.Id > 0, include).ToList();
            return View(pledges);
        }

        // GET: Pledges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pledge = await _context.Pledge
                .Include(p => p.Campaign)
                .Include(p => p.Contact)
                .Include(p => p.Donation)
                .Include(p => p.Event)
                .Include(p => p.PhoneNumber)
                .Include(p => p.Date)
                .Include(P => P.Email)


                .FirstOrDefaultAsync(m => m.Id == id);
            if (pledge == null)
            {
                return NotFound();
            }

            return View(pledge);
        }

        // GET: Pledges/Create
        public IActionResult Create()
        {
            ViewData["CampaignId"] = new SelectList(_context.Campaign, "Id", "Name");
            ViewData["ContactId"] = new SelectList(_context.Contact, "Id", "Name");
            ViewData["DonationId"] = new SelectList(_context.Donation, "Id", "Amount");
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name");
            ViewData["OrgId"] = new SelectList(_context.Organization, "Id", "Name");

            return View();
        }

        // POST: Pledges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrgId,EventId,CampaignId,Name,Gender,Email,PhoneNumber,Date,Amount")] Pledge pledge)
        {
            if (ModelState.IsValid)
            {
                pledge.OrgId = "cac8a4ec-edd5-4554-8c91-24574282b9c1";
                _context.Add(pledge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CampaignId"] = new SelectList(_context.Campaign, "Id", "Name", pledge.CampaignId);
            ViewData["ContactId"] = new SelectList(_context.Contact, "Id", "Name", pledge.ContactId);
            ViewData["DonationId"] = new SelectList(_context.Donation, "Id", "Amount", pledge.DonationId);
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name", pledge.EventId);
            ViewData["OrgId"] = new SelectList(_context.Organization, "Id", "Name", pledge.OrgId);
            return View(pledge);
        }

        // GET: Pledges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pledge = await _context.Pledge.FindAsync(id);
            if (pledge == null)
            {
                return NotFound();
            }

            ViewData["CampaignId"] = new SelectList(_context.Campaign, "Id", "Name", pledge.CampaignId);
            ViewData["ContactId"] = new SelectList(_context.Contact, "Id", "Name", pledge.ContactId);
            ViewData["DonationId"] = new SelectList(_context.Donation, "Id", "Amount", pledge.DonationId);
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name", pledge.EventId);
            ViewData["OrgId"] = new SelectList(_context.Organization, "Id", "Name", pledge.OrgId);
            return View(pledge);
        }

        // POST: Pledges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrgId,EventId,Name,Gender,CampaignId,Email,PhoneNumber,Amount,Date,")] Pledge pledge)
        {
            if (id != pledge.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    pledge.OrgId = "cac8a4ec-edd5-4554-8c91-24574282b9c1";
                    _context.Update(pledge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PledgeExists(pledge.Id))
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
            ViewData["CampaignId"] = new SelectList(_context.Campaign, "Id", "OrgId", pledge.CampaignId);
            ViewData["ContactId"] = new SelectList(_context.Contact, "Id", "Name", pledge.ContactId);
            ViewData["DonationId"] = new SelectList(_context.Donation, "Id", "OrgId", pledge.DonationId);
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Id", pledge.EventId);
            ViewData["OrgId"] = new SelectList(_context.Organization, "Id", "Id", pledge.OrgId);
            return View(pledge);
        }

        // GET: Pledges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pledge = await _context.Pledge
                .Include(p => p.Campaign)
                .Include(p => p.Contact)
                .Include(p => p.Donation)
                .Include(p => p.Event)
                .Include(p => p.Org)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pledge == null)
            {
                return NotFound();
            }

            return View(pledge);
        }

        // POST: Pledges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pledge = await _context.Pledge.FindAsync(id);
            _context.Pledge.Remove(pledge);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PledgeExists(int id)
        {
            return _context.Pledge.Any(e => e.Id == id);
        }
    }
}
