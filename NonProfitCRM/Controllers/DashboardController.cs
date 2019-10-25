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
            ViewData["Cash"] = 100;

            ViewData["NewContacts"] = _unitOfWork.ContactRepository.DbSet.Count();
            ViewData["TotalDonors"] = _unitOfWork.DonationRepository.DbSet.Where(donation=> donation.Date.GetValueOrDefault().Month == DateTime.Now.Month && donation.Date.GetValueOrDefault().Year == DateTime.Now.Year).Count();
            ViewData["TotalDonations"] = _unitOfWork.DonationRepository.DbSet.Sum(src => src.Amount);
            ViewData["TotalExpenditures"] = 800;
            return View();
        }

        public async Task<Dictionary<string, decimal>> GetDonation(string type)
        {
            if (type == "YEAR")
            {
                var donations = await _unitOfWork.DonationRepository.GetManyAsync(src => src.Date.GetValueOrDefault() >= DateTime.Now.AddDays(-365));

                var donationData = await GetDonationMonthWiseData(donations.OrderBy(src => src.Date).ToList());

                return donationData;
            }
            else if (type == "MONTH")
            {
                var donations = await _unitOfWork.DonationRepository.GetManyAsync(src => src.Date.GetValueOrDefault() >= DateTime.Now.AddDays(-30));

                var donationData = await GetDonation(donations.OrderBy(src => src.Date).ToList());

                return donationData;
            }
            else
            {
                //BY DEFAULT WEEK 

                var donations = await _unitOfWork.DonationRepository.GetManyAsync(src => src.Date.GetValueOrDefault() >= DateTime.Now.AddDays(-7));

                var donationData = await GetDonation(donations.OrderBy(src=>src.Date).ToList());

                return donationData;
            }
        }

        private async Task<Dictionary<string, decimal>> GetDonation(List<Donation> donations)
        {
            try
            {
                var result =
                    (from d in donations
                     group d by d.Date.GetValueOrDefault().Date into g
                     select new
                     {
                         Date = g.Key.Date.ToString("MMM dd"),
                         Amount = g.Sum(x => x.Amount)
                     }).ToDictionary(src => src.Date, x => x.Amount.GetValueOrDefault());

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        private async Task<Dictionary<string, decimal>> GetDonationMonthWiseData(List<Donation> donations)
        {
            try
            {
                var result = (from d in donations
                              group d by new { month = d.Date.GetValueOrDefault().ToString("MMM"), year = d.Date.GetValueOrDefault().Year } into donation
                              select new
                              {
                                  Date = string.Format("{0} {1}", donation.Key.month, donation.Key.year),
                                  Amount = donation.Sum(x => x.Amount.GetValueOrDefault())
                              }).ToDictionary(src => src.Date, x => x.Amount); ;
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}