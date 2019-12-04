using System;
using System.Collections.Generic;

namespace Core.Domain
{
    public partial class Users
    {
        public Users()
        {
            UserRoleMapping = new HashSet<UserRoleMapping>();
        }

        public int Id { get; set; }
        public string OrgId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ProfilePicture { get; set; }
        public int? CreatedBy { get; set; }
        public string PasswordResetToken { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressCountry { get; set; }
        public string AddressZipcode { get; set; }

        public Organization Org { get; set; }
        public ICollection<UserRoleMapping> UserRoleMapping { get; set; }
    }
}
