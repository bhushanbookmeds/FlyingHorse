using System;
using System.Collections.Generic;

namespace NonProfitCRM.Models
{
    public partial class UserRoleMapping
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public UserRole Role { get; set; }
        public Users User { get; set; }
    }
}
