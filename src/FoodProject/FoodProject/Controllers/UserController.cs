using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodProject.Models;
using FoodProject.Abstract;


namespace FoodProject.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository userRepository;
        private User user;
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        // GET: User
        public ActionResult Index()
        {
            user = null;
            return View(user);
        }
        [HttpPost]
        public RedirectToRouteResult Index(User user)
        {
            User temp = null;
            if (user.Name != null && user.Password != null)
            {
                 temp = userRepository.Users.Select(x => x).Where(x => x.Name == user.Name).FirstOrDefault();
            }
            if (temp !=null)
            {

                //returns a view showing their name it means login was succesfull need
                //to change to somthing that actually does something
                TempData["UserID"] = temp.UserID;
                TempData.Keep();
                //TempData.Keep("UserID");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //there is a username error go back to view
                return RedirectToAction("Index", "User");
            }

        }
    }
}