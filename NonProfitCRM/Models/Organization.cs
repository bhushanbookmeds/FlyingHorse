using System;
using System.Collections.Generic;

namespace NonProfitCRM.Models
{
    public partial class Organization
    {
        public Organization()
        {
            Campaign = new HashSet<Campaign>();
            Donation = new HashSet<Donation>();
            Event = new HashSet<Event>();
            Pledge = new HashSet<Pledge>();
            Users = new HashSet<Users>();
            Project = new HashSet<Project>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsActive { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressCountry { get; set; }
        public string AddressZipcode { get; set; }

        public ICollection<Campaign> Campaign { get; set; }
        public ICollection<Donation> Donation { get; set; }
        public ICollection<Event> Event { get; set; }
        public ICollection<Project> Project { get; set; }
        public ICollection<Pledge> Pledge { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
