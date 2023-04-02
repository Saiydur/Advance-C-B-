using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsApp.Models
{
    public class NewsInputModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Enter at least 3 charecter")]
        public string Title { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Enter at least 10 charecter")]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}