using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NonProfitCRM.Models
{
    public partial class Pledge
    {
        public int Id { get; set; }


        public string OrgId { get; set; }
        public int? EventId { get; set; }
        public int? CampaignId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public int? ContactId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter valid Email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "this field is required")]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Donation is required")]
        [Display(Name = "Donation")]
        public decimal? Amount { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Display(Name = "Donation")]
        public int? DonationId { get; set; }

        public Campaign Campaign { get; set; }

        [Display(Name = "Name")]
        public Contact Contact { get; set; }

        public Donation Donation { get; set; }

        public Event Event { get; set; }
        public Organization Org { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
