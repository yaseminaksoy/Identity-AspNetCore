using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class UserSignInViewModel
    {
        [Display(Name ="Username: ")]
        [Required(ErrorMessage ="Please enter your username")]
        public string UserName { get; set; }
        [Display(Name = "Password: ")]
        [Required(ErrorMessage ="Please enter your password")]
        public string Password { get; set; }
    }
}
