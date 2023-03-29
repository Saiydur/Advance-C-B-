using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZeroHunger.Utils;
using System.ComponentModel.DataAnnotations;

namespace ZeroHunger.Models
{
    public class FoodEditModel
    {
        public Guid FoodId { get; set; }
        [Required]
        public string CollectionTime { get; set; }
        [Required]
        public string PreservationTime { get; set; }
        [Required]
        public string FoodExpiryDate { get; set; }
        [Required]
        public FoodType FoodType { get; set; }
        [Required]
        [Range(0, 1000000,ErrorMessage  = "Food Quantity must be between 0 and 1000000")]
        public double FoodQuantity { get; set; }
        [Required]
        public string Status { get; set; } = "false";
    }
}