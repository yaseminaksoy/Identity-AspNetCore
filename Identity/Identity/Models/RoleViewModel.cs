using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage ="Enter the name")]
        [Display(Name ="Name : ")]
        public string Name { get; set; }
    }
}
