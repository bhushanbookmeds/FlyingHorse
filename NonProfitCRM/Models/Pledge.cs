using System;
using System.Collections.Generic;

namespace NonProfitCRM.Models
{
    public partial class Pledge
    {
        public int Id { get; set; }
        public string OrgId { get; set; }
        public int? EventId { get; set; }
        public int? CampaignId { get; set; }
        public int? ContactId { get; set; }
        public int? Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
        public bool? Closed { get; set; }
        public int? DonationId { get; set; }

        public int? VolunteersId { get; set; }

        public Campaign Campaign { get; set; }
        public Contact Contact { get; set; }
        public Donation Donation { get; set; }
        public Event Event { get; set; }
        public Organization Org { get; set; }

        public Volunteers Volunteers { get; set; }
    }
}
