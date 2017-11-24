using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodProject.Models;

namespace FoodProject.Controllers
{
    /// <summary>
    /// this controller is the starting point of the site
    /// </summary>
    public class HomeController : Controller
    {
        public HomeController()
        {
            
        }

        /// <summary>
        /// this method brings up the home screen
        /// and serves a purpose of initializing the User object with model binding
        /// </summary>
        /// <param name="user">The object that holds the logged in user, you don't need to pass this in
        /// it is model binded and passed for you</param>
        /// <returns>a view of the home screen</returns>
        public ActionResult Index(User user)
        {
            return View(user);
        }

        public ActionResult About()
        {
            ViewBag.Message = "This is our our project for C346, Software Engineering.";

            return View();
        }


    }
}