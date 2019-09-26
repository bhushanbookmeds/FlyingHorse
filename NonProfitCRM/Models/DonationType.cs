using System;
using System.Collections.Generic;

namespace NonProfitCRM.Models
{
    public partial class DonationType
    {
        public DonationType()
        {
            Donation = new HashSet<Donation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string OrgId { get; set; }

        public ICollection<Donation> Donation { get; set; }
    }
}
