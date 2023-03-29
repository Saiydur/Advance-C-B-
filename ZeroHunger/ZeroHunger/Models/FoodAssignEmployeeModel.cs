using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZeroHunger.DB;

namespace ZeroHunger.Models
{
    public class FoodAssignEmployeeModel
    {
        public Guid FoodId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid NgoId { get; set; }
        public Guid CollectionRequestId { get; set; }
        public string FoodType { get; set; }
        public double FoodQuantity { get; set; }
        public string FoodExpiryDate { get; set; }
        public string CollectionTime { get; set; }
        public string PreservationTime { get; set; }
        public string Status { get; set; }
        public string IsCollected { get; set; }

        public List<Employee> Employees { get; set; }
    }
}