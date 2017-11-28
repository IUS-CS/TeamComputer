using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodProject.Models;

namespace FoodProject.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }
        void Add(User user);
        void UpdatePassword(User user, String password);
    }
}
