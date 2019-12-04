using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    public class ContactType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
