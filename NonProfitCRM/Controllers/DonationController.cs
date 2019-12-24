using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace NonProfitCRM.Controllers
{
    public class DonationController : Controller
    {
        public async Task<IActionResult> Index(Donation donation)
        {
            return View(donation);
        }

        public async Task<IActionResult> Create(Donation donation)
        {
            return View(donation);
        }
    }
}