﻿using System;
using System.Linq;
using FoodProject.Abstract;
using FoodProject.Models;
using FoodProject.Controllers;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest1
{
    class MockUserRepository : IUserRepository
    {
        private List<User> listUsers;
        public IEnumerable<User> Users
        {
            get
            {
                return listUsers;
            }
            set { }
        }
        public MockUserRepository()
        {
            listUsers = new List<User>(new User[] {
                new User {UserID = 0, Name = "coolemail@site.com",Password = "password",Pantrys = new Pantry[] {new Pantry { } } },
                new User {UserID = 1, Name = "coolemail2@site.com",Password = "password",Pantrys = new Pantry[] {new Pantry {PantryID = 0, UserID = 1, FoodID = 1, Food = new Food {FoodID = 1,Name = "milk",Units = 0 } } } },
                new User {UserID = 2, Name = "coolemail3@site.com",Password = "password",Pantrys = new Pantry[] {new Pantry {PantryID = 1, UserID = 2, FoodID = 2, Food = new Food {FoodID = 2,Name = "bread",Units = 0 } } } }
            });
        }
        

        public void Add(User user)
        {
            listUsers.Add(user);
        }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanCreateUser()
        {
            // setup
            MockUserRepository mock = new MockUserRepository();
            UserController controller = new UserController(mock);
            User user = new User();
            //controller.Session["User"] = user;
            UserLogin temp = new UserLogin() { un = "newemail@site.com", pword = "password", pword2 = "password" };

            // excute Createuser
            controller.Createuser(user,temp);

            // assert user object holds new login and user is added to database
            Assert.AreEqual(user.Name, "newemail@site.com");
            user = mock.Users.Where(x => x.Name == "newemail@site.com").FirstOrDefault();
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void CanLogin()
        {
            // setup 
            MockUserRepository mock = new MockUserRepository();
            UserController controller = new UserController(mock);
            UserLogin temp = new UserLogin() { un = "coolemail@site.com",pword ="password"};
            User user = new User();

            // execute Login
            controller.Index(user, temp);

            // assert user is logged in 
            Assert.AreEqual(user.Name, "coolemail@site.com");
            Assert.AreEqual(user.Password, "password");
            Assert.AreEqual(user.UserID, 0);
        }

        [TestMethod]
        public void CanLogOut()
        {
            MockUserRepository mock = new MockUserRepository();
            UserController controller = new UserController(mock);
            UserLogin temp = new UserLogin() { un = "coolemail@site.com", pword = "password" };
            User user = new User();

            // execute Login
            controller.Index(user, temp);

            // execute Logout
            controller.LogOut(user);

            // assert user is logged out
            Assert.IsNull(user.UserID);
            Assert.IsNull(user.Name);
            Assert.IsNull(user.Password);
            Assert.IsNull(user.Pantrys);

        }

        [TestMethod]
        public void WillNotLoginWithWrongPassword()
        {
            // setup 
            MockUserRepository mock = new MockUserRepository();
            UserController controller = new UserController(mock);
            UserLogin temp = new UserLogin() { un = "coolemail@site.com", pword = "wrongPassword" };
            User user = new User();

            // execute Login
            controller.Index(user, temp);

            // assert not logged in
            Assert.AreNotEqual(user.Name, temp.un);
            Assert.AreNotEqual(user.Password, temp.pword);
        }

        [TestMethod]
        public void WillNotCreateUserWithSameName()
        {
            // setup 
            MockUserRepository mock = new MockUserRepository();
            UserController controller = new UserController(mock);
            UserLogin temp = new UserLogin() { un = "coolemail@site.com", pword = "wrongPassword" };
            User user = new User();

            // execute createuser
            controller.Createuser(user,temp);

            // Assert user not created
            Assert.AreNotEqual(user.Name, temp.un);
            Assert.AreNotEqual(user.Password, temp.pword);
            int s = mock.Users.Select(x => x).Where(x => x.Name == temp.un).Count();
            Assert.AreEqual(s, 1);
        }
    }
}