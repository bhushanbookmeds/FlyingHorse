using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NonProfitCRM.Data;
using NonProfitCRM.Models;

namespace NonProfitCRM.Controllers
{
    public class DashboardController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly string orgId;

        public DashboardController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            ViewData["TotalDonations"] = _unitOfWork.DonationRepository.DbSet.Sum(donation => donation.Amount);
            ViewData["Credit"] = 100;
            ViewData["Cash"] = 200;
            return View();
        }

        public async Task<Dictionary<DateTime, decimal>> GetDonation(string type)
        {
            if (type == "YEAR")
            {
                return null;
            }
            else if (type == "MONTH")
            {
                return null;
            }
            else
            {
                //BY DEFAULT WEEK 

                var donations = await _unitOfWork.DonationRepository.GetManyAsync(src => src.Date.GetValueOrDefault() >= DateTime.Now.AddDays(-7));

                var donationData = await GetDonation(donations.ToList());

                return donationData;
            }
        }

        private async Task<Dictionary<DateTime, decimal>> GetDonation(List<Donation> donations)
        {
            var result =
                (from d in donations
                 group d by d.Date.GetValueOrDefault().Date into g
                 select new
                 {
                     Date = g.Key.Date,
                     Amount = g.Sum(x => x.Amount)
                 }).ToDictionary(src => src.Date, x => x.Amount.GetValueOrDefault());

            return result;
        }


    }
}