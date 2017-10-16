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
        private Temp temp;
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        // GET: User
        [HttpGet]
        public ActionResult Index(User user)
        {
            if (user.UserID != null)
            {
                return RedirectToAction("Index","Home");
            }
            return View(temp);
        }
        [HttpPost]
        public RedirectToRouteResult Index(User user,Temp temp)
        {
            User tempUser;
            user.Name = temp.un;
            user.Password = temp.pword;
            if (user.Name != null && user.Password != null)
            {
                 tempUser = userRepository.Users.Select(x => x).Where(x => x.Name == user.Name).FirstOrDefault();
                user.UserID = tempUser.UserID;
                user.Pantrys = tempUser.Pantrys;
            }
            if (user.UserID != null)
            {
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