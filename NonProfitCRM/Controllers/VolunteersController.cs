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
    public class VolunteersController : Controller
    {


        private readonly UnitOfWork _unitOfWork;
        private readonly string orgId;


        public VolunteersController()
        {
            _unitOfWork = new UnitOfWork();
            orgId = "cac8a4ec-edd5-4554-8c91-24574282b9c1";
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            var volunteers = await _unitOfWork.VolunteersRepository.GetManyAsync(c => c.OrgId == orgId);
           
            return View(volunteers);
        }
        
        // GET: Volunteers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var volunteers = await _unitOfWork.VolunteersRepository.GetByIDAsync(id);
            if (volunteers == null)
            {
                return NotFound();
            }

            return View(volunteers);
        }
        // GET: Volunteers/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrgId,Name,PhoneNumber,Age,Email,InstagramProfile,AddressLine1,AddressLine2,AddressStreet,AddressCity,AddressState,AddressCountry,AddressZipcode")] Volunteers volunteers)


        {
            if (ModelState.IsValid)
            {
                  volunteers.OrgId = orgId;

                _unitOfWork.VolunteersRepository.Insert(volunteers);
                await _unitOfWork.SaveAsync();

                return RedirectToAction(nameof(Index));
            }
           
            return View(volunteers);
        }


        // GET: Volunteers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var volunteers = await _unitOfWork.VolunteersRepository.GetByIDAsync(id);
            if (volunteers == null)
            {
                return NotFound();
            }
            //ViewData["VolunteersTypeId"] = new SelectList(_unitOfWork.VolunteersTypeRepository.GetAll(), "Id", "Name", volunteers.VolunteersTypeId);
            return View(volunteers);

        }


        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrgId,Name,PhoneNumber,Age,Email,InstagramProfile,AddressLine1,AddressLine2,AddressStreet,AddressCity,AddressState,AddressCountry,AddressZipcode")] Volunteers volunteers)
        {
            if (id != volunteers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    volunteers.OrgId = orgId;
                    _unitOfWork.VolunteersRepository.Update(volunteers);
                    await _unitOfWork.SaveAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolunteersExists(volunteers.Id))
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
        //   ViewData["VolunteersTypeId"] = new SelectList(_unitOfWork.VolunteersTypeRepository.GetAll(), "Id", "Name", volunteers.VolunteersTypeId);
            return View(volunteers);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteers = await _unitOfWork.VolunteersRepository.GetByIDAsync(id);
            if (volunteers == null)
            {
                return NotFound();
            }

            return View(volunteers);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var volunteers = await _unitOfWork.VolunteersRepository.GetByIDAsync(id);
            _unitOfWork.VolunteersRepository.Delete(volunteers);
            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool VolunteersExists(int id)
        {
            var volunteers = _unitOfWork.VolunteersRepository.GetByID(id);
            if (volunteers == null)
            {
                return false;
            }
            return true;
        }

    }
}