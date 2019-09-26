using System;
using System.Collections.Generic;

namespace NonProfitCRM.Models
{
    public partial class Campaign
    {
        public Campaign()
        {
            Donation = new HashSet<Donation>();
            Event = new HashSet<Event>();
            Pledge = new HashSet<Pledge>();
        }

        public int Id { get; set; }
        public string OrgId { get; set; }
        public int? CreatedBy { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? FundsRequired { get; set; }
        public bool? OnetimeCampaign { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressCountry { get; set; }
        public string AddressZipcode { get; set; }

        public CampaignCategory Category { get; set; }
        public Users CreatedByNavigation { get; set; }
        public Organization Org { get; set; }
        public ICollection<Donation> Donation { get; set; }
        public ICollection<Event> Event { get; set; }
        public ICollection<Pledge> Pledge { get; set; }
    }
}
