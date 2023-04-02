using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsApp.Models
{
    public class CategoryInputModel
    {
        [Required]
        [MinLength(3,ErrorMessage ="Enter at least 3 charecter")]
        public string Name { get; set; }
    }
}