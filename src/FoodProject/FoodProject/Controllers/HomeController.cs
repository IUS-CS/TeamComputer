using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodProject.Models;

namespace FoodProject.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            
        }

        //homescreen gets the user object through model binding
        //this is the first time it happens and acts as the initalizer for user object
        public ActionResult Index(User user)
        {
            return View(user);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


    }
}