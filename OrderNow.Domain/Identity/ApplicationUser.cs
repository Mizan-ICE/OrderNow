using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace OrderNow.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "FullName")]
        public string FullName { get; set; }


    }
}
