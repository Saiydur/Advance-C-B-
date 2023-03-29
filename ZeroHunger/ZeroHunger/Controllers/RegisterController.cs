using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.DB;
using ZeroHunger.Models;
using ZeroHunger.Utils;

namespace ZeroHunger.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ZeroHungerEntities _zeroHungerEntities;

        public RegisterController()
        {
            _zeroHungerEntities= new ZeroHungerEntities();
        }


        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Index(UserCreateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Guid id = Guid.NewGuid();
                    if (model.RoleType == UserRole.Restaurant)
                    {
                        Restaurant restaurantEO = new Restaurant
                        {
                            ID= id,
                            Name = model.Name,
                            Password = model.Password,
                            Phone = model.Phone,
                            Address= model.Address,
                            Email= model.Email,
                        };
                        _zeroHungerEntities.Restaurants.Add(restaurantEO);
                        _zeroHungerEntities.SaveChanges();
                    }
                    if (model.RoleType == UserRole.Employee)
                    {
                        Employee employeeEO = new Employee
                        {
                            ID = id,
                            Name = model.Name,
                            Password = model.Password,
                            Phone = model.Phone,
                            Address = model.Address,
                            Email = model.Email,
                        };
                        _zeroHungerEntities.Employees.Add(employeeEO);
                        _zeroHungerEntities.SaveChanges();
                    }
                    if (model.RoleType == UserRole.NGO)
                    {
                        NGO NgoEO = new NGO
                        {
                            ID = id,
                            Name = model.Name,
                            Password = model.Password,
                            Phone = model.Phone,
                            Address = model.Address,
                            Email = model.Email,
                        };
                        _zeroHungerEntities.NGOes.Add(NgoEO);
                        _zeroHungerEntities.SaveChanges();
                    }
                    TempData["ResponseMsg"] = "Successfully Registered";
                    TempData["Type"] = "Success";
                    return RedirectToAction("Index", "Login");
                }
                catch (Exception e)
                {
                    TempData["ResponseMsg"] = "Registered Error"+e.Message;
                    TempData["Type"] = "Danger";
                    return RedirectToAction("Index", "Login");
                }
            }
            return View(model);
        }
    }
}