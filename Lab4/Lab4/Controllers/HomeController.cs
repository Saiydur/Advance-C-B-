using Lab4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Create() 
        { 
            return View();
        }

        [HttpPost]
        public ActionResult Create(User usermodel) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Index","Home");
                }
                return View(usermodel);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return View(usermodel);
            }
        }
    }
}