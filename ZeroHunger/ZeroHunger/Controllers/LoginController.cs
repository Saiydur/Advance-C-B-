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
    public class LoginController : Controller
    {
        private readonly ZeroHungerEntities _entities;

        public LoginController()
        {
            _entities= new ZeroHungerEntities();
        }
        // GET: Login
        public ActionResult Index()
        {
            if (Session["user"]==null)
                return View();
            else
                return RedirectToAction("Index","Dashboard");
        }

        [HttpPost]
        public ActionResult Index(UserLoginModel model) 
        {
            if(ModelState.IsValid)
            {
                try
                {
                    Guid id = Guid.NewGuid();
                    if (model.RoleType == UserRole.Restaurant) 
                    {
                        var data = _entities.Restaurants.FirstOrDefault(eo=>eo.Email == model.Email && eo.Password == model.Password);
                        id = data.ID;
                    }
                    if (model.RoleType == UserRole.Employee) 
                    {
                        var data = _entities.Employees.FirstOrDefault(eo => eo.Email == model.Email && eo.Password == model.Password);
                        id = data.ID;
                    }
                    if (model.RoleType == UserRole.NGO) 
                    {
                        var data = _entities.NGOes.FirstOrDefault(eo => eo.Email == model.Email && eo.Password == model.Password);
                        id = data.ID;
                    }
                    if (id != null) {
                        HttpContext.Session["user"] = id.ToString();
                        HttpContext.Session["type"] = model.RoleType.ToString();
                    }
                    else
                    {
                        TempData["ResponseMsg"] = "Invalid Credential";
                        TempData["Type"] = "Danger";
                        return RedirectToAction("Index", "Login");
                    }
                    return RedirectToAction("Index", "Dashboard");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    TempData["ResponseMsg"] = "Something wrong"+e.Message;
                    TempData["Type"] = "Danger";
                    return RedirectToAction("Index", "Login");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Login");
        }
    }
}