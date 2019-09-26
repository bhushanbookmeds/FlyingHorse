using System;
using System.Collections.Generic;

namespace NonProfitCRM.Models
{
    public partial class UserRole
    {
        public UserRole()
        {
            UserRoleMapping = new HashSet<UserRoleMapping>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserRoleMapping> UserRoleMapping { get; set; }
    }
}
