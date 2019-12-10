using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    public class Country
    {
        public static int countryId;

        public int Id { get; set; }
        public string Name { get; set; }
        public string TwoLetterISOCode { get; set; }
        public string ThreeLetterISOCode { get; set; }
        public int? NumericISOCode { get; set; }
        public int? PhoneCode { get; set; }

        public virtual ICollection<State> States { get; set; }   //navigation properties--- 
                                                                 // Icollection represents the list of states in one country

    }
}
