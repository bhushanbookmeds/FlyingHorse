using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NonProfitCRM.Models
{
    public partial class Volunteers
    { 
        public int Id { get; set; }
        public string OrgId { get; set; }

        [Required(ErrorMessage = "Field can't be empty.")]
        [DisplayName("Name")]
        public string Name { get; set; }

        //public string CountryCode { get; set; }
        public string PhoneCode { get; set; }

        [Required(ErrorMessage = "Field can't be empty.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        [DisplayName("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Field can't be empty.")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        [DisplayName("Email")]
        public string Email { get; set; }
       
        [Required(ErrorMessage = "Field can't be empty.")]
        [Range(18,100,ErrorMessage ="Please Enter Age Between 18 to 100")]
        [DisplayName("Age")]
        public int Age { get; set; }

        public string Gender { get; set; }

        [Required(ErrorMessage = "Field can't be empty.")]
        [DisplayName("AddressLine1")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "Field can't be empty.")]
        [DisplayName("StreetAddress")]
        public string AddressStreet { get; set; }

        [Required(ErrorMessage = "Field can't be empty.")]
        [DisplayName("CityAddress")]
        public string AddressCity { get; set; }

        [Required(ErrorMessage = "Field can't be empty.")]
        [DisplayName("State Address")]
        public string AddressState { get; set; }

        [Required(ErrorMessage = "Field can't be empty.")]
        [DisplayName("Country Address")]
        public string AddressCountry { get; set; }
        [Required(ErrorMessage = "Field can't be empty.")]
        [DisplayName("Zipcode Address")]
        public string AddressZipcode { get; set; }

        public string ImagePath { get; set; }

        //[Required(ErrorMessage = "Field can't be empty.")]
        //[DisplayName("Instagram")]
        public string InstagramProfile { get; set; }

        public string FacebookProfile { get; set; }

        public string TwitterProfile { get; set; }


    }
}

