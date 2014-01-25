using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodBucket.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "An UserName for your account is required")]
        [Display(Name="User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "An Password for your account is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Valid Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email{ get; set; }
        
        [DataType(DataType.ImageUrl)]
        [StringLength(1024)]
        public string ProfileImageUrl { get; set; }

    }
}