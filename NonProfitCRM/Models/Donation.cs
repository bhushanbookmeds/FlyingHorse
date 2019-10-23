using Microsoft.AspNetCore.Http;
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
        
        public int Id { get; set; }
       
        public string OrgId { get; set; }
        
        public int? EventId { get; set; }
      
        public int? CampaignId { get; set; }
    
        public int? ContactId { get; set; }
        
        public bool? GuestDonation { get; set; }
       
        public string GuestEmail { get; set; }

       
        public decimal? Amount { get; set; }
        
        public bool? RecurringDonation { get; set; }
        
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
