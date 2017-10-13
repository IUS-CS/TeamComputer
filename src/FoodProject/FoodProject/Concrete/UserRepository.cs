using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodProject.Abstract;
using FoodProject.Models;

namespace FoodProject.Concrete
{
    public class UserRepository: IUserRepository
    {
        public FoodContext Context = new FoodContext();

        public IEnumerable<User> Users
        {
            get { return Context.Users; }

        }

    }
}