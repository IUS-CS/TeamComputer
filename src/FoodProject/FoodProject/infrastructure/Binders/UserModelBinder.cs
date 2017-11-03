using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FoodProject.Models;
namespace FoodProject.infrastructure.Binders
{
    public class UserModelBinder : IModelBinder
    {
        //ket in the session to get user object
        private const string sessionKey = "User";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingConetext)
        {
            User user = null;
            //checks to see if the session is not null
            if (controllerContext.HttpContext.Session != null)
            {
                //if not null get the user object out of the session
                user = (User)controllerContext.HttpContext.Session[sessionKey];
            }
            //if user == null user in the session object was null
            if (user == null)
            {
                //need to create a new user object
                user = new User();
                user.Pantrys = new List<Pantry>();
                //if session is not null add new user object to session
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = user;
                }
            }
            //return user object from session or new user object if there wasn't one in the session
            return user;
        }
    }
}