using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Core.Domain
{
    public class Contact
    {
        public int Id { get; set; }
        public string OrgId { get; set; }
        public string PhoneCode { get; set; }
        
        public string Name { get; set; }
        public int? ContactTypeId { get; set; }
        public int? ParentContactId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal? DonorScore { get; set; }
        public string ProfilePicture { get; set; }
        public string FacebookProfile { get; set; }
        public string InstagramProfile { get; set; }
        public string TwitterProfile { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressCountry { get; set; }
        public string AddressZipcode { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

        public IFormFile ImageFile { get; set; }

        //public virtual Organization Organization { get; set; }
        public virtual ContactType ContactType { get; set; }

    }
}
