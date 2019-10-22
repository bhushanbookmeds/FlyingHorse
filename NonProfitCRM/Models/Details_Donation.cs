using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitCRM.Models
{
    public class Details_Donation
    {
        public DateTime? Date { get; set; }
        public string Name { get; set; }
        public string CampaignName { get; set; }
        public string EventName { get; set; }
        public String TransactionTypeId { get; set; }
        public String DonationType { get; set; }
        public decimal? Amount { get; set; }
        public bool? GuestDonation { get; set; }
        public int Id { get; set; }

    }
}
