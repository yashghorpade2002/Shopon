using System.ComponentModel.DataAnnotations;

namespace Shopon.WebApp.Models
{
    public class LoginVM
    {
        [Display(Name = "Enter Email Id")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email Id cannot be blank")]
        public string EmailId { get; set; }

        [Display(Name = "Enter Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password cannot be blank")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
