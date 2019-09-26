using System;
using System.Collections.Generic;

namespace NonProfitCRM.Models
{
    public partial class Event
    {
        public Event()
        {
            Donation = new HashSet<Donation>();
            Pledge = new HashSet<Pledge>();
        }

        public int Id { get; set; }
        public string OrgId { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public int? CampaignId { get; set; }
        public DateTime? Date { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressCountry { get; set; }
        public string AddressZipcode { get; set; }

        public Campaign Campaign { get; set; }
        public CampaignCategory Category { get; set; }
        public Organization Org { get; set; }
        public ICollection<Donation> Donation { get; set; }
        public ICollection<Pledge> Pledge { get; set; }
    }
}
