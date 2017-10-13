using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodProject.Concrete;

namespace FoodProject.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            TempData.Keep();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


    }
}