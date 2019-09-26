using System;
using System.Collections.Generic;

namespace NonProfitCRM.Models
{
    public partial class ContactType
    {
        public ContactType()
        {
            Contact = new HashSet<Contact>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Contact> Contact { get; set; }
    }
}
