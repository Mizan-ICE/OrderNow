using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;

namespace OrderNow.Web.Models;

public class RegisterModel
{

    [Display(Name = "Full name")]
    [Required(ErrorMessage = "Full name is required")]
    public string FullName { get; set; }

    [Display(Name = "Email address")]
    [Required(ErrorMessage = "Email address is required")]
    public string EmailAddress { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, ErrorMessage = "Password must be at least {2} characters long.", MinimumLength = 6)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
    ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one number.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }


    [Display(Name = "Confirm password")]
    [Required(ErrorMessage = "Confirm password is required")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }
    public string? ReturnUrl { get; set; }
    public IList<AuthenticationScheme>? ExternalLogins { get; set; }
}
