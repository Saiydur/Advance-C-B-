using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.Models;
using ZeroHunger.DB;
using ZeroHunger.Utils;

namespace ZeroHunger.Controllers
{
    public class FoodController : Controller
    {
        private readonly ZeroHungerEntities _zeroHungerEntities;
        public FoodController()
        {
            _zeroHungerEntities = new ZeroHungerEntities();
        }
        public ActionResult Index()
        {
            var collectList=_zeroHungerEntities.Collect_Requests.ToList();
            var foodList = _zeroHungerEntities.Foods.ToList();
            var collectionFoodListById =  from collect in collectList
                                      join food in foodList on collect.ID equals food.Collect_Request_ID
                                      where collect.Restaurant_ID == Guid.Parse(Session["user"].ToString())
                                      select new FoodViewModel
                                      {
                                          FoodId = food.ID,
                                          FoodType = food.Type,
                                          FoodQuantity = (double)food.Quantity,
                                          FoodExpiryDate = food.Expiry_Date,
                                          CollectionTime = collect.Collection_Time,
                                          PreservationTime = collect.Preservation_Time,
                                          Status = collect.Status
                                      };
            return View(collectionFoodListById);
        }

        [HttpGet]
        public ActionResult AddFood() 
        {
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult AddFood(FoodCreateModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    
                    if (Session["type"].Equals("Restaurant"))
                    {
                        string userId = Session["user"].ToString();
                        Collect_Request colletRequest = new Collect_Request
                        {
                            ID = Guid.NewGuid(),
                            Collection_Time = model.CollectionTime,
                            Preservation_Time = model.PreservationTime,
                            Status = model.Status,
                            Restaurant_ID = Guid.Parse(userId)
                        };
                        _zeroHungerEntities.Collect_Requests.Add(colletRequest);
                        _zeroHungerEntities.SaveChanges();
                        Food foodEO = new Food
                        {
                            ID = Guid.NewGuid(),
                            Collect_Request_ID = colletRequest.ID,
                            Type = model.FoodType.ToString(),
                            Quantity = (int?)model.FoodQuantity,
                            Expiry_Date = model.FoodExpiryDate,
                        };
                        _zeroHungerEntities.Foods.Add(foodEO);
                        _zeroHungerEntities.SaveChanges();
                        TempData["ResponseMsg"] = "Food Added Successfully";
                        TempData["Type"] = "Success";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["ResponseMsg"] = "You are not authorized to add food";
                        TempData["Type"] = "Danger";
                        return RedirectToAction("Index", "Food");
                    }
                }
                catch (Exception e)
                {
                    TempData["ResponseMsg"] = "Error: " + e.Message;
                    TempData["Type"] = "Danger";
                }
            }
            return View();
        }
    
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var food = _zeroHungerEntities.Foods.FirstOrDefault(x => x.ID == id);
            var collect = _zeroHungerEntities.Collect_Requests.FirstOrDefault(x => x.ID == food.Collect_Request_ID);
            FoodEditModel foodEditModel = new FoodEditModel
            {
                FoodId = food.ID,
                FoodType = (FoodType)Enum.Parse(typeof(FoodType), food.Type),
                FoodQuantity = (double)food.Quantity,
                FoodExpiryDate = food.Expiry_Date,
                CollectionTime = collect.Collection_Time,
                PreservationTime = collect.Preservation_Time,
                Status = collect.Status
            };
            return View(foodEditModel);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Edit(FoodEditModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var food = _zeroHungerEntities.Foods.FirstOrDefault(x => x.ID == model.FoodId);
                    var collect = _zeroHungerEntities.Collect_Requests.FirstOrDefault(x => x.ID == food.Collect_Request_ID);
                    food.Type = model.FoodType.ToString();
                    food.Quantity = (int?)model.FoodQuantity;
                    food.Expiry_Date = model.FoodExpiryDate;
                    collect.Collection_Time = model.CollectionTime;
                    collect.Preservation_Time = model.PreservationTime;
                    collect.Status = model.Status;
                    _zeroHungerEntities.SaveChanges();
                    TempData["ResponseMsg"] = "Food Updated Successfully";
                    TempData["Type"] = "Success";
                    return RedirectToAction("Index", "Food");
                }
                catch (Exception e)
                {
                    TempData["ResponseMsg"] = "Error: " + e.Message;
                    TempData["Type"] = "Danger";
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            try
            {
                var food = _zeroHungerEntities.Foods.FirstOrDefault(x => x.ID == id);
                var collect = _zeroHungerEntities.Collect_Requests.FirstOrDefault(x => x.ID == food.Collect_Request_ID);
                _zeroHungerEntities.Foods.Remove(food);
                _zeroHungerEntities.Collect_Requests.Remove(collect);
                _zeroHungerEntities.SaveChanges();
                TempData["ResponseMsg"] = "Food Deleted Successfully";
                TempData["Type"] = "Success";
            }
            catch (Exception e)
            {
                TempData["ResponseMsg"] = "Error: " + e.Message;
                TempData["Type"] = "Danger";
            }
            return RedirectToAction("Index", "Food");
        }

        [HttpGet]
        public ActionResult GetFoodList()
        {
            var collectList = _zeroHungerEntities.Collect_Requests.ToList();
            var foodList = _zeroHungerEntities.Foods.ToList();

            var collectionFoodList = from collect in collectList
                                     join food in foodList on collect.ID equals food.Collect_Request_ID
                                     select new NgoFoodViewModel
                                     {
                                            FoodId = food.ID,
                                            FoodType = food.Type,
                                            FoodQuantity = (double)food.Quantity,
                                            FoodExpiryDate = food.Expiry_Date,
                                            CollectionTime = collect.Collection_Time,
                                            PreservationTime = collect.Preservation_Time,
                                            Status = collect.Status
                                     };
            return View(collectionFoodList);
        }

        [HttpGet]
        public ActionResult AssignEmployee(Guid id)
        {
            var food = _zeroHungerEntities.Foods.FirstOrDefault(x => x.ID == id);
            var collect = _zeroHungerEntities.Collect_Requests.FirstOrDefault(x => x.ID == food.Collect_Request_ID);
            var employeeList = _zeroHungerEntities.Employees.ToList();
            List<Employee> employeeListWithoutPassword = new List<Employee>();
            foreach (var employee in employeeList)
            {
                employee.Password = null;
                employeeListWithoutPassword.Add(employee);
            }
            FoodAssignEmployeeModel foodAssignEmployeeModel = new FoodAssignEmployeeModel
            {
                NgoId = Guid.Parse(Session["user"].ToString()),
                FoodId = food.ID,
                CollectionRequestId = collect.ID,
                FoodType = food.Type,
                FoodQuantity = (double)food.Quantity,
                FoodExpiryDate = food.Expiry_Date,
                CollectionTime = collect.Collection_Time,
                PreservationTime = collect.Preservation_Time,
                Status = collect.Status,
                Employees = employeeListWithoutPassword
            };
            return View(foodAssignEmployeeModel);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult AssignEmployee(FoodAssignEmployeeModel model)
        {
            if(ModelState.IsValid)
            {
                //try
                //{
                    NGOCollection nGOCollection = new NGOCollection
                    {
                        ID = Guid.NewGuid(),
                        Employee_ID = model.EmployeeId,
                        Food_ID = model.FoodId,
                        Collect_Request_ID = model.CollectionRequestId,
                        NGO_ID = model.NgoId,
                        isCollected = "true",
                    };
                    _zeroHungerEntities.NGOCollections.Add(nGOCollection);
                    _zeroHungerEntities.SaveChanges();
                    var collectRequest = _zeroHungerEntities.Collect_Requests.FirstOrDefault(x => x.ID == nGOCollection.Collect_Request_ID);
                    collectRequest.Status = "true";
                    _zeroHungerEntities.SaveChanges();
                    TempData["ResponseMsg"] = "Employee Assigned Successfully";
                    TempData["Type"] = "Success";
                    return RedirectToAction("Index", "Dashboard");
                //}
                //catch (Exception e)
                //{
                //    TempData["ResponseMsg"] = "Error: " + e.Message;
                //    TempData["Type"] = "Danger";
                //    return RedirectToAction("Index", "Dashboard");
                //}
            }
            return View(model);
        }
    }
}