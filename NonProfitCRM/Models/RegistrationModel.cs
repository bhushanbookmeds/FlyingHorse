using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitCRM.Models
{
    public class RegistrationModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "please select organization name")]
        public string OrgId { get; set; } = "00000-00000";

        [Required(ErrorMessage = "please enter user name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "please enter user email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "please enter password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "please enter confirm password")]
        [Compare("Password",ErrorMessage ="Password and Confirm password did not matched")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "please enter phone number")]
        public string PhoneNumber { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ProfilePicture { get; set; }

        public int? CreatedBy { get; set; }

        public string PasswordResetToken { get; set; }

       /// [Required]
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

       // [Required]
        public string AddressStreet { get; set; }

       // [Required]
        public string AddressCity { get; set; }

       // [Required]
        public string AddressState { get; set; }

        //[Required]
        public string AddressCountry { get; set; }

        //[Required]
        public string AddressZipcode { get; set; }

        [Required(ErrorMessage ="please select user role")]
        public int UserRoleId { get; set; }
    }
}
