using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitCRM.Models
{
    public partial class Expenditures
    {
     
        public int Id { get; set; }
        public string Name { get; set; }
        public string Submitter { get; set; }
        public decimal? Amount { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
        public String Invoice { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
       
    }

