using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodProject.Concrete;


namespace FoodProject.Controllers
{
    public class UserController : Controller
    {
        UserRepo Repository = new UserRepo();

        // GET: User
        public ActionResult Index()
        {
            return View(Repository.Users);
        }
    }
}