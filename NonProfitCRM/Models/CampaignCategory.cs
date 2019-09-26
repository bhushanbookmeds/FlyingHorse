using System;
using System.Collections.Generic;

namespace NonProfitCRM.Models
{
    public partial class CampaignCategory
    {
        public CampaignCategory()
        {
            Campaign = new HashSet<Campaign>();
            Event = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string OrgId { get; set; }

        public ICollection<Campaign> Campaign { get; set; }
        public ICollection<Event> Event { get; set; }
    }
}
