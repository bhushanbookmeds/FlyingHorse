﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace NonProfitCRM.Models
{
    public partial class Contact
    {
       
        public Contact()
        {
            Donation = new HashSet<Donation>();
            Pledge = new HashSet<Pledge>();
        }

        public IFormFile ImageFile { get; set; }

        public int Id { get; set; }
        public string OrgId { get; set; }
        [Required(ErrorMessage = "Field can't be empty.")]
        [RegularExpression(@"^[\D]*$", ErrorMessage = " Please use alphabets only.")]
        [DisplayName("Name")]

        public string Name { get; set; }

        [Required(ErrorMessage = "Please select contact type")]
        public int ContactTypeId { get; set; }
        public int? ParentContactId { get; set; }
        public int PhoneCode { get; set; }
        [Required(ErrorMessage = "Field can't be empty.")]
      //  [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
       // [DisplayName("PhoneNumber")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Field can't be empty.")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        [DisplayName("Email")]
        public string Email { get; set; }
        public decimal? DonorScore { get; set; }
        public string ProfilePicture { get; set; }
        public string FacebookProfile { get; set; }
        public string InstagramProfile { get; set; }
        public string TwitterProfile { get; set; }
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
        [Required(ErrorMessage = "Must select one item")]
        [DisplayName("Country Address")]

        public string AddressCountry { get; set; }
        [Required(ErrorMessage = "Field can't be empty.")]
        [DisplayName("Zipcode Address")]
        public string AddressZipcode { get; set; }
        [Required(ErrorMessage = "Field can't be empty.")]
        [Range(13, 100, ErrorMessage = "Please enter age between 13 to 100.")]
        [DisplayName("Age")]
        public int? Age { get; set; }
        [Required(ErrorMessage = "Field can't be empty.")]
        [DisplayName("Gender")]
        public string Gender { get; set; }
        public string ImagePath { get; set; }

        public string Country { get; set; }

        public ContactType ContactType { get; set; }
        public ICollection<Project> Project { get; set; }
        public ICollection<Donation> Donation { get; set; }
        public ICollection<Pledge> Pledge { get; set; }
    }
}
