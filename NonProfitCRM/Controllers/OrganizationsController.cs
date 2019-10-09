using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NonProfitCRM.Data;
using NonProfitCRM.Models;

namespace NonProfitCRM.Controllers
{
    [Authorize(Roles = "Admin,Organization")]
    public class OrganizationsController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public OrganizationsController()
        {
            _unitOfWork = new UnitOfWork();
        }

        // GET: Organizations
        public async Task<IActionResult> Index()
        {
            var orgs = await _unitOfWork.OrganizationRepository.GetManyAsync(org=> !org.Name.Equals("Admin") && org.Id != "6562F517-EE73-4378-B9BD-8A8F64BB051C");
            return View(orgs);
        }

        // GET: Organizations/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organization = await _unitOfWork.OrganizationRepository.GetByIDAsync(id);
            if (organization == null)
            {
                return NotFound();
            }

            return View(organization);
        }

        // GET: Organizations/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Organizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles ="Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,StartDate,ExpiryDate,IsActive,AddressLine1,AddressLine2,AddressStreet,AddressCity,AddressState,AddressCountry,AddressZipcode")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                string guid = Guid.NewGuid().ToString();
                organization.Id = guid;
                organization.CreatedOn = DateTime.UtcNow;

                await _unitOfWork.OrganizationRepository.InsertAsync(organization);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(organization);
        }

        // GET: Organizations/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || id == "6562F517-EE73-4378-B9BD-8A8F64BB051C")
            {
                return NotFound();
            }

            var organization = await _unitOfWork.OrganizationRepository.GetByIDAsync(id);
            if (organization == null)
            {
                return NotFound();
            }
            return View(organization);
        }

        // POST: Organizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,StartDate,ExpiryDate,IsActive,AddressLine1,AddressLine2,AddressStreet,AddressCity,AddressState,AddressCountry,AddressZipcode")] Organization organization)
        {
            if (id != organization.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.OrganizationRepository.Update(organization);
                    await _unitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizationExists(organization.Id))
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
            return View(organization);
        }

        // GET: Organizations/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organization = await _unitOfWork.OrganizationRepository.GetByIDAsync(id);
            if (organization == null)
            {
                return NotFound();
            }

            return View(organization);
        }

        // POST: Organizations/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var organization = await _unitOfWork.OrganizationRepository.GetByIDAsync(id);
            _unitOfWork.OrganizationRepository.Delete(organization);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganizationExists(string id)
        {
            var org = _unitOfWork.OrganizationRepository.GetByID(id);
            if (org == null)
            {
                return false;
            }
            return true;
        }
    }
}
