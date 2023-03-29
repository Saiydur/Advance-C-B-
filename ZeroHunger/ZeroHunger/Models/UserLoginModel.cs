using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ZeroHunger.Utils;

namespace ZeroHunger.Models
{
    public class UserLoginModel
    {
        [Required]
        [RegularExpression("[a-z0-9]+@[a-z]+\\.[a-z]{2,3}", ErrorMessage = "Enter valid email")]
        public string Email { get; set; }

        [Required]
        [MinLength(8,ErrorMessage ="Enter at least 8 character")]
        public string Password { get; set; }

        [Required]
        [DisplayName("Logged as a")]
        public UserRole RoleType { get; set; }
    }
}