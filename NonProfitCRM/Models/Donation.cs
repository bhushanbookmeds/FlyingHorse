using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NonProfitCRM.Models
{
    public partial class Donation
    {
        public Donation()
        {
            Pledge = new HashSet<Pledge>();
        }
        [Required(ErrorMessage="Feild Cant be Empty")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Feild Cant be Empty")]
        public string OrgId { get; set; }
        [Required(ErrorMessage = "Feild Cant be Empty")]
        public int? EventId { get; set; }
        [Required(ErrorMessage = "Feild Cant be Empty")]
        public int? CampaignId { get; set; }
        [Required(ErrorMessage = "Feild Cant be Empty")]
        public int? ContactId { get; set; }
        [Required(ErrorMessage = "Feild Cant be Empty")]
        public bool? GuestDonation { get; set; }
        [Required(ErrorMessage = "Feild Cant be Empty")]
        public string GuestEmail { get; set; }
        [Required(ErrorMessage = "Feild Cant be Empty")]
        public decimal? Amount { get; set; }
        [Required(ErrorMessage = "Feild Cant be Empty")]
        public bool? RecurringDonation { get; set; }
        [Required(ErrorMessage = "Feild Cant be Empty")]
        public DateTime? Date { get; set; }
        [Required(ErrorMessage = "Feild Cant be Empty")]
        public int? TransactionTypeId { get; set; }
        [Required(ErrorMessage = "Feild Cant be Empty")]
        public int? DonationTypeId { get; set; }


        public Campaign Campaign { get; set; }
        public Contact Contact { get; set; }
        public DonationType DonationType { get; set; }
        public Event Event { get; set; }
        public Organization Org { get; set; }
        public TransactionType TransactionType { get; set; }
        public ICollection<Pledge> Pledge { get; set; }
    }
}
