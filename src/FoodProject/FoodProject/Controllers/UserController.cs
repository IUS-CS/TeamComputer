using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodProject.Models;
using FoodProject.Abstract;
using System.Text.RegularExpressions;


namespace FoodProject.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository userRepository;
        private UserLogin temp;
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
        public RedirectToRouteResult Index(User user,UserLogin temp)
        {
            String s = user == null ? "" : "";
            User tempUser;
            String regexString = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            Regex r = new Regex(regexString, RegexOptions.IgnoreCase);
            Match m = r.Match(temp.un);
            if (!m.Success || (temp.un==null && temp.pword==null))
            {
                return RedirectToAction("Index", "User");
            }
            user.Name = temp.un;
            user.Password = temp.pword;
            if (user.Name != null && user.Password != null)
            {
                tempUser = userRepository.Users.Select(x => x).Where(x => x.Name == user.Name).FirstOrDefault();
                if (tempUser.Password.Equals(user.Password))
                {
                    user.UserID = tempUser.UserID;
                    user.Pantrys = tempUser.Pantrys;
                }
                //password doesn't match
                else
                {
                    return RedirectToAction("Index", "User");
                }
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
        [HttpGet]
        public ViewResult CreateUser()
        {
            return View(temp);
        }
        [HttpPost]
        public RedirectToRouteResult Createuser(User user,UserLogin temp)
        {
            if(temp.un!=null && temp.pword != null && temp.pword2!=null)
            {
                if (temp.pword.Equals(temp.pword2))
                {
                    User tempUser = userRepository.Users.Select(x => x).Where(x => x.Name == temp.un).FirstOrDefault();
                    if (tempUser == null)
                    {
                        user.Name = temp.un;
                        user.Password = temp.pword;
                        userRepository.Add(user);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            //password not matching or non unique user name
            return RedirectToAction("CreateUser", "User");
        }

        public RedirectToRouteResult LogOut(User user)
        {
            user.Name = null;
            user.Pantrys = null;
            user.UserID = null;
            user.Password = null;
            return RedirectToAction("Index", "Home");
        }
    }
}