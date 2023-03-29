using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using ZeroHunger.Utils;

namespace ZeroHunger.Models
{
    public class UserCreateModel
    {
        [Required] 
        public string Name { get; set; }

        [Required]
        [RegularExpression("[a-z0-9]+@[a-z]+\\.[a-z]{2,3}",ErrorMessage ="Enter valid email")]
        public string Email { get; set; }

        [Required]
        [MinLength(8,ErrorMessage ="Enter at least 8 charecter")]
        public string Password { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public UserRole RoleType { get; set; }
    }
}