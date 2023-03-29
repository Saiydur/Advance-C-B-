using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZeroHunger.Utils;

namespace ZeroHunger.Models
{
    public class NgoFoodViewModel
    {
        public Guid FoodId { get; set; }
        public string CollectionTime { get; set; }
        public string PreservationTime { get; set; }
        public string FoodExpiryDate { get; set; }
        public string FoodType { get; set; }
        public double FoodQuantity { get; set; }
        public string Status { get; set; }
    }
}