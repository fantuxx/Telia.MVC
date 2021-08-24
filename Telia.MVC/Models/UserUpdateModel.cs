using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace Telia.MVC.Data
{
    public class UserUpdateModel
    {
        [Required(ErrorMessage = "Please enter Full name")]
        [Display(Name = "First name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Please enter last name")]
        [Display(Name = "Last name name")]
        public string LastName { get; set; }



        [Required(ErrorMessage = "Please enter  phone number")]
        [Phone(ErrorMessage = "Phone number is not in a correct format")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string Id { get; set; }

        
        public  Roles Role { get; set; }


        public enum Roles
        {
            Admin,
            Moderator,
            User
        }
        
    }
}