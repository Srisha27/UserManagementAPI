using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Models.Authentication.SignUp
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }


        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = " DOB is required")]

        public DateTime ? DOB { get; set; }
        [Required(ErrorMessage = "PhoneNumber is required")]

        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = " Address is required")]

        public string? Address { get; set; }


    }
}
