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
    public class ProjectController : Controller
    {


        private readonly UnitOfWork _unitOfWork;
        private readonly string orgId;

        public ProjectController()
        {
            _unitOfWork = new UnitOfWork();
            orgId = "cac8a4ec-edd5-4554-8c91-24574282b9c1";
        }
        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var projects = await _unitOfWork.ProjectRepository.GetManyAsync(c => c.OrgId == orgId);
            return View(projects);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var project = await _unitOfWork.ProjectRepository.GetByIDAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            var expenditures = await _unitOfWork.ExpendituresRepository.GetManyAsync(x=>x.ProjectId==id);

            project.Expenditures = expenditures.ToList();
           
            ViewBag.TotalAmount = project.Expenditures.Sum(x => x.Amount);
            
            return View(project);
        }
        // GET: Project/Create
        public IActionResult Create()
        {

            var ProjectTypeId = _unitOfWork.ProjectTypeRepository.GetAll().ToList();
            ViewBag.ProjectTypeId = new SelectList(ProjectTypeId, "Id", "Name");

            return View(new Project());
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartDate,EndDate,Description,AllocatedFund,TotalExpenses,AddressLine1," +
            "AddressLine2,AddressStreet,AddressCity,AddressState,AddressCountry,AddressZipcode,ProjectTypeId")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.OrgId = orgId;

                _unitOfWork.ProjectRepository.Insert(project);
                await _unitOfWork.SaveAsync();

                return RedirectToAction(nameof(Index));
            }
            var ProjectTypeId = _unitOfWork.ProjectTypeRepository.GetAll().ToList();
            ViewBag.ProjectTypeId = new SelectList(ProjectTypeId, "Id", "Name");

            return View(project);
        }
        [HttpPost]
        public async Task<IActionResult> Expenditures(Expenditures expenditures)
        {

            if(expenditures == null)
               return Json(new { ReturnStatus = "error", ReturnData = new Object() });

            Expenditures expend = new Expenditures();
            expend.ProjectId = expenditures.ProjectId;
            expend.Name = expenditures.Name;
            expend.Date = expenditures.Date;
            expend.Submitter = expenditures.Submitter;
            expend.Amount = expenditures.Amount;
            //expend.Invoice = formCollection["Invoice"];
            var expenditure = _unitOfWork.ExpendituresRepository.GetDbSet().FirstOrDefault(x => x.Name == expend.Name);
            if (expenditure == null)
            {
                try
                { 
                _unitOfWork.ExpendituresRepository.Insert(expend);
                await _unitOfWork.SaveAsync();
                }
                catch(Exception ex)
                {
                    throw ex;
                }

                return Json(new { ReturnStatus = "success", ReturnData = expend });


            }
            return Json(new { ReturnStatus = "error", ReturnData = new Object() });
        }
        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var project = await _unitOfWork.ProjectRepository.GetByIDAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["ProjectTypeId"] = new SelectList(_unitOfWork.ProjectTypeRepository.GetAll(), "Id", "Name", project.ProjectTypeId);
            return View(project);

        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AllocatedFund,Description,TotalExpenses,StartDate,EndDate,AddressLine1,AddressLine2,AddressStreet,AddressCity,AddressState,AddressCountry,AddressZipcode,")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    project.OrgId = orgId;
                    _unitOfWork.ProjectRepository.Update(project);
                    await _unitOfWork.SaveAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
            ViewData["ProjectTypeId"] = new SelectList(_unitOfWork.ContactTypeRepository.GetAll(), "Id", "Name", project.ProjectTypeId);
            return View(project);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _unitOfWork.ProjectRepository.GetByIDAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _unitOfWork.ProjectRepository.GetByIDAsync(id);
            _unitOfWork.ProjectRepository.Delete(project);
            await _unitOfWork.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            var project = _unitOfWork.ProjectRepository.GetByID(id);
            if (project == null)
            {
                return false;
            }
            return true;
        }
    }
}



