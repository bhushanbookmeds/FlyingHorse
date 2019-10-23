using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitCRM.Models
{
    public partial class Project
    {
        public Project()
        {
            Expenditures = new HashSet<Expenditures>();
            
        }
        public int Id { get; set; }
        public string OrgId { get; set; }
        [Required(ErrorMessage = "Field can't be empty.")]
      
        [RegularExpression(@"^[\D]*$", ErrorMessage = "Use letters only please")]
        [DisplayName("Name")]
        public string Name { get; set; }
        public string Lead { get; set; }

        [Required(ErrorMessage = "Field can't be empty.")]
        [DisplayName("Start Date")]

        [DataType(DataType.Date)]

        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Field can't be empty.")]
        [DataType(DataType.Date)]
        [DisplayName("End Date")]
        public DateTime? EndDate { get; set; }
        public decimal? TotalExpenses { get; set; }

        [Required(ErrorMessage = "Field can't be empty.")]
        [DisplayName("AddressLine1")]
        public string AddressLine1 { get; set; }
        
        public string AddressLine2 { get; set; }
        [Required(ErrorMessage = "Field can't be empty.")]
        [DisplayName("AddressStreet")]
        public string AddressStreet { get; set; }
        [Required(ErrorMessage = "Field can't be empty.")]
        [DisplayName("AddressCity")]
        public string AddressCity { get; set; }
        [Required(ErrorMessage = "Field can't be empty.")]
        [DisplayName("AddresState")]
        public string AddressState { get; set; }
        [Required(ErrorMessage = "Field can't be empty.")]
        [DisplayName("AddressCountry")]
        public string AddressCountry { get; set; }
        [Required(ErrorMessage = "Field can't be empty.")]
        [DisplayName("AddressZipcode")]
        public string AddressZipcode { get; set; }

        public string Description { get; set; }
        [UIHint("Currency")]
        [Required(ErrorMessage = "Field can't be empty.")]
        [DisplayName("AllocatedFund")]
        public decimal? AllocatedFund { get; set; }


        [Required(ErrorMessage = "Field can't be empty.")]
        [DisplayName("ProjectType")]
        public int ProjectTypeId { get; set; }
        public int? ContactId { get; set; }
        public Organization Org { get; set; }
        public Contact Contact { get; set; }

        public ICollection<Expenditures> Expenditures { get; set; }
        public ProjectType ProjectType { get; set; }
    }
}
