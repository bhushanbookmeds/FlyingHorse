using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    public class State
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public virtual Country Country { get; set; }    // since one state has only one country assigned to it 
    }
}
