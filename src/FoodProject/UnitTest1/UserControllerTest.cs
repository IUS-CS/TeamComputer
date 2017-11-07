using System;
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
    public class UserControllerTest
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
            // if equal user is signed in
            Assert.AreEqual(user.Name, "newemail@site.com");
            //if not null new user was added to database
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
            //checks to see if user object got right data assigned to its attributes
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
            // if null user was logged out
            Assert.IsNull(user.UserID);
            Assert.IsNull(user.Name);
            Assert.IsNull(user.Password);
            Assert.IsNull(user.Pantrys);

        }
        [TestMethod]
        public void WillNotLoginWithWrongUsername()
        {
            // setup 
            MockUserRepository mock = new MockUserRepository();
            UserController controller = new UserController(mock);
            UserLogin temp = new UserLogin() { un = "wrongEmail@site.com", pword = "password" };
            User user = new User();

            // execute login
            controller.Index(user, temp);

            // assert not logged in
            // if null user was not logged in
            Assert.IsNull(user.Name);
            Assert.IsNull(user.Password);
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
            // if null user was not logged in
            Assert.IsNull(user.Name);
            Assert.IsNull(user.Password);
        }

        [TestMethod]
        public void WillNotLoginUserWithoutEmail()
        {
            //setup
            MockUserRepository mock = new MockUserRepository();
            UserController controller = new UserController(mock);
            UserLogin temp = new UserLogin() { un = "letmelogin", pword = "password" };
            User user = new User();

            //execute login
            controller.Index(user, temp);

            //assert not logged in
            //if null user was not logged in
            Assert.IsNull(user.Name);
            Assert.IsNull(user.Password);
        }

        [TestMethod] 
        public void WillNotCreateUserWithPasswordsNotMatching()
        {
            // setup 
            MockUserRepository mock = new MockUserRepository();
            UserController controller = new UserController(mock);
            UserLogin temp = new UserLogin() { un = "newEmail@site.com", pword = "password", pword2 = "Password" };
            User user = new User();

            // execute createuser
            controller.Createuser(user, temp);

            // Assert user not created
            // if null user wasn't created
            Assert.IsNull(user.Name);
            Assert.IsNull(user.Password);

        }

        [TestMethod]
        public void WillNotCreateUserWithoutEmail()
        {
            //setup
            MockUserRepository mock = new MockUserRepository();
            UserController controller = new UserController(mock);
            UserLogin temp = new UserLogin() { un = "IWantToSignUp", pword = "password", pword2 = "password" };
            User user = new User();

            // execute createuser
            controller.Createuser(user, temp);

            // Assert user not created
            // if null user wasn't created
            Assert.IsNull(user.Name);
            Assert.IsNull(user.Password);

        }

        [TestMethod]
        public void WillNotCreateUserWithSameName()
        {
            // setup 
            MockUserRepository mock = new MockUserRepository();
            UserController controller = new UserController(mock);
            UserLogin temp = new UserLogin() { un = "coolemail@site.com", pword = "password", pword2 = "password" };
            User user = new User();

            // execute createuser
            controller.Createuser(user,temp);

            // Assert user not created
            // if null user is not created
            Assert.IsNull(user.Name);
            Assert.IsNull(user.Password);
            //if one user wasn't added to database 
            int s = mock.Users.Select(x => x).Where(x => x.Name == temp.un).Count();
            Assert.AreEqual(s, 1);
        }
    }
}
