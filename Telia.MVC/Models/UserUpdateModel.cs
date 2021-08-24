using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [Display(Name = "Add user to new Role")]
        public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem("Select a role", ""),
            new SelectListItem("Admin", "Admin"),
            new SelectListItem("Moderator", "Moderator"),
            new SelectListItem("Customer", "Customer")
        };
     
        public string Role { get; set; }
        [Display(Name = "User currently is in these roles:")]
        public List<string> UserRoles { get; set; }
    }
}