using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab4.Models
{
    public class User
    {
        [Required(ErrorMessage ="Please Enter Your Name")]
        [StringLength(60,ErrorMessage ="Max Length Crosssed"),
         MinLength(6,ErrorMessage ="Enter At Least 6 Charecter")
        ]
        public string Name { get; set; }
        [Required,MinLength(8,ErrorMessage ="Minimum At Digit Needed")]
        public string Id { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Profession { get; set; }
        [Required]
        public string[] Hobbies { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}