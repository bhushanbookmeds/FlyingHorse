using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Domain
{
    public class Donation
    {
        [Key]
        public int Id { get; set; }
        public string OrgId { get; set; }
        public int? EventId { get; set; }
        public int? CampaignId { get; set; }
        public int? ContactId { get; set; }
        public bool? GuestDonation { get; set; }
        public string GuestEmail { get; set; }
        public decimal? Amount { get; set; }
        public bool? RecurringDonation { get; set; }
        public DateTime Date { get; set; }
        public int? TransactionTypeId { get; set; }
        public int? DonationTypeId { get; set; }
    }
}
