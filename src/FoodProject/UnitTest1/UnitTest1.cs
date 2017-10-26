using System;
using Moq;
using FoodProject.Abstract;
using FoodProject.Models;
using FoodProject.Controllers;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanCreateUser()
        {
            Mock<IUserRepository> mock = new Mock<IUserRepository>();
            User user = new User();
            List<User> users = new List<User>();
            mock.Setup(m => m.Add()).;
            mock.Setup(m => m.Users).Returns(new User[] {
                new User {UserID = 0, Name = "coolemail@site.com",Password = "password",Pantrys = new Pantry[] {new Pantry { } } },
                new User {UserID = 1, Name = "coolemail2@site.com",Password = "password",Pantrys = new Pantry[] {new Pantry {PantryID = 0, UserID = 1, FoodID = 1, Food = new Food {FoodID = 1,Name = "milk",Units = 0 } } } },
                new User {UserID = 2, Name = "coolemail3@site.com",Password = "password",Pantrys = new Pantry[] {new Pantry {PantryID = 1, UserID = 2, FoodID = 2, Food = new Food {FoodID = 2,Name = "bread",Units = 0 } } } }
            });
            
            UserController controller = new UserController(mock.Object);
            
            
        }
    }
}
