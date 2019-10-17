using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitCRM.Models
{
    public partial class ProjectType
    {
        public ProjectType()
        {
            Project = new HashSet<Project>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string OrgId { get; set; }

        public ICollection<Project> Project { get; set; }
    }
}
