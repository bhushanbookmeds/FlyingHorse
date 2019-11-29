using System;
using System.Collections.Generic;

namespace NonProfitCRM.Models
{
    public partial class Organization
    {
        public Organization()
        {
            Users = new HashSet<Users>();
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
        
        public ICollection<Users> Users { get; set; }
    }
}
