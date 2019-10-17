using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace NonProfitCRM.Models
{
    
    public partial class Event
    {
        public IFormFile ImageFile { get; set; }

        public Event()
        {
            Donation = new HashSet<Donation>();
            Pledge = new HashSet<Pledge>();

             
    }

        public int Id { get; set; }
        public string OrgId { get; set; }

        //public string BannerPath { get; set; }

        //[Required]
        //[Display(Name = "Name")]
        //[Remote("doesNameExist", "Events", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]

        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name Should be Unique")]
        public string Name { get; set; }


        public int? CategoryId { get; set; }


        public int? CampaignId { get; set; }

        [Display(Name = "StartDate")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Start Date is required")]
        public DateTime? StartDate { get; set; }

        public string StartTime { get; set; }

        [Display(Name = "EndDate")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "End Date is required")]
        public DateTime? EndDate { get; set; }

        public string EndTime { get; set; }

        //[Display(Name = "Description")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Description Field is required")]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        [Display(Name = "AddressLine1")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address Line1 Field is required")]
        public string AddressLine1 { get; set; }

        [Display(Name = "AddressLine2")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address Line2 Field is required")]
        public string AddressLine2 { get; set; }

        [Display(Name = "AddressStreet")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Street Field is required")]
        public string AddressStreet { get; set; }

        [Display(Name = "AddressCity")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "City Field is required")]
        public string AddressCity { get; set; }

        [Display(Name = "AddressState")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "State Field is required")]
        public string AddressState { get; set; }

        [Display(Name = "AddressCountry")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Country Field is required")]
        public string AddressCountry { get; set; }

        [Display(Name = "AddressZipcode")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "ZipCode Field is required")]
        public string AddressZipcode { get; set; }

        public Campaign Campaign { get; set; }
        public CampaignCategory Category { get; set; }
        public Organization Org { get; set; }
        public ICollection<Donation> Donation { get; set; }
        public ICollection<Pledge> Pledge { get; set; }
    }
}
