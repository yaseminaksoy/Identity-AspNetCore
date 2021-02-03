using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class UserUpdateViewModel
    {
        [Display(Name="Email: ")]
        [Required(ErrorMessage ="Please enter your email")]
        [EmailAddress(ErrorMessage ="Please enter a valid email address")]
        public string Email { get; set; }
        [Display(Name="Phone: ")]
        public string PhoneNumber { get; set; }
        public string PictureUrl { get; set; }
        public IFormFile Picture { get; set; }
        [Display(Name="Name: ")]
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }
        [Display(Name="Surname: ")]
        [Required(ErrorMessage = "Please enter your surname")]
        public string Surname { get; set; }
    }
}
