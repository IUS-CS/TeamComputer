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
        public void Add(User user)
        {
            Context.Users.Add(user);
            Context.SaveChanges();
        }

        public void UpdatePassword(User product)
        {
            User dbEntry =
            Context.Users.Find(product.UserID);
            if (dbEntry != null)
            {
                dbEntry.Password = product.Password;
            }
            Context.SaveChanges();
        }
    }
}