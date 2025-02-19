using System.ComponentModel.DataAnnotations;

namespace Shopon.WebApp.Models
{
    public class RegisterVM
    {
        [Display(Name = "Enter user Name")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage ="User name cannot be blank")]
        public string UserName { get; set; }

        [Display(Name = "Enter Email Id")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email Id cannot be blank")]
        public string EmailId { get; set; }

        [Display(Name = "Enter Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Mobile Number cannot be blank")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter a valid 10-digit mobile number.")]
        public string MobileNumber { get; set; }

        [Display(Name = "Enter Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password cannot be blank")]
        public string Password { get; set; }

        [Display(Name = "Enter Confirm Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm password cannot be blank")]
        [Compare("Password", ErrorMessage ="Password and confirm password are not same!")]
        public string ConfirmPassword { get; set; }
    }
}
