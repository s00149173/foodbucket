using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace FoodBucket.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Please enter your real name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
            "Numbers and special characters are not allowed in the name.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage =
            "First name must be between 3 and 50 characters long.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your real name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
            "Numbers and special characters are not allowed in the name.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage =
            "Last name must be between 3 and 50 characters long.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "An UserName for your account is required")]
        [Display(Name = "User Name")]
        [Remote("doesUserNameExist", "User", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "A Password for your account is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "A Valid Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email{ get; set; }

        public HttpPostedFileBase ProfileImage { get; set; }

    }
}