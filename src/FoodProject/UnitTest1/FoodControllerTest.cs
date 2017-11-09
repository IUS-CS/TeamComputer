using System;
using System.Linq;
using FoodProject.Abstract;
using FoodProject.Models;
using FoodProject.Controllers;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web;
namespace UnitTest1
{
    [TestClass]
    public class FoodControllerTest
    {
        // used to set the controller we are testing context's
        public static HttpContextBase FakeHttpContext()
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new MockHttpSession();
            var server = new Mock<HttpServerUtilityBase>();

            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Response).Returns(response.Object);
            context.Setup(ctx => ctx.Session).Returns(session);
            context.Setup(ctx => ctx.Server).Returns(server.Object);

            return context.Object;
        }
        public class MockHttpSession : System.Web.HttpSessionStateBase
        {
            Dictionary<string, object> m_SessionStorage = new Dictionary<string, object>();

            public override object this[string name]
            {
                get { return m_SessionStorage[name]; }
                set { m_SessionStorage[name] = value; }
            }
        }

        // The context each repository is going to get their data from
        class MyContext
        {
            public List<Food> foods { get; set; } 
            public List<Pantry> pantrys { get; set; } 
            public MyContext()
            {
                foods  = new List<Food>( new Food[] { new Food() { FoodID = 0, Name = "food 0"},
                    new Food() {FoodID = 1, Name = "food 1"},
                    new Food() {FoodID = 2, Name = "food 2"} });
                pantrys = new List<Pantry>(new Pantry[] { new Pantry() { PantryID = 0,FoodID = 0,UserID = 1,Units = 1},
                new Pantry() { PantryID = 1,FoodID = 1,UserID = 0, Units = 1},
                new Pantry() { PantryID = 2,FoodID = 2,UserID = 1, Units = 1}});
            }
        }
        class MockIPantryRepository : IPantryRepository
        {
            MyContext c;
            public MockIPantryRepository(MyContext c)
            {
                this.c = c;
            }
            public IEnumerable<Pantry> Pantrys
            {

                get
                {
                    return c.pantrys;
                }
            }

            public void add(Pantry p)
            {
                c.pantrys.Add(p);
            }

            public void delete(int id)
            {
                Pantry p = c.pantrys.Select(x => x).Where(x => x.PantryID == id).First();
                c.pantrys.Remove(p);
                c.pantrys.Select(x => x);
            }

            public void save()
            {
                //save not needed
            }
            public void updatePantry(Pantry p)
            {
                Pantry temp = c.pantrys.Select(x => x).Where(x => x.PantryID == p.PantryID).First();
                temp.Units = p.Units;
            }
        }
        class MockIFoodRepository : IFoodRepository
        {
            private MyContext c;
            public MockIFoodRepository(MyContext c)
            {
                this.c = c;
            }
            public IEnumerable<Food> Foods
            {
                get
                {
                    return c.foods;
                }
            }

            public void DeleteFood(int food)
            {
                Food f = c.foods.Select(x => x).Where(x => x.FoodID == food).First();
                c.foods.Remove(f);
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public Food GetFoodById(int foodId)
            {
                throw new NotImplementedException();
            }

            public void InsertFood(Food food)
            {
                food.FoodID = c.foods.Count();
                c.foods.Add(food);
            }

            public void Save()
            {
                //don't have to save in mock
            }

            public void UpdateFood(Food food)
            {
                Food f = c.foods.Where(x => x.FoodID == food.FoodID).First();
                f.Name = food.Name;
                
            }
        }

        [TestMethod]
        public void CanAddFood()
        {
            // setup
            MyContext mc = new MyContext();
            MockIFoodRepository foodRepository = new MockIFoodRepository(mc);
            MockIPantryRepository pantryRepository = new MockIPantryRepository(mc);
            FoodController controller = new FoodController(foodRepository, pantryRepository);
            User user = new User() {UserID = 1};

            // execute add food
            controller.addFoods(user, 1);

            // assert food is added to pantry
            //if not null then it is found in pantry
            Assert.IsNotNull(pantryRepository.Pantrys.Where(x=> x.FoodID == 1).First());
        }

        [TestMethod]
        public void CanDeleteFood()
        {
            // setup
            MyContext mc = new MyContext();
            MockIFoodRepository foodRepository = new MockIFoodRepository(mc);
            MockIPantryRepository pantryRepository = new MockIPantryRepository(mc);
            FoodController controller = new FoodController(foodRepository, pantryRepository);
            int id = 1;

            // execute delete
            controller.delete(id);

            // assert food is removed
            //if null food was removed from user pantry
            Assert.IsNull(pantryRepository.Pantrys.Select(x => x).Where(x => x.FoodID == id).FirstOrDefault());
        }

        [TestMethod]
        public void CanUpdatePantry()
        {
            // setup
            MyContext mc = new MyContext();
            MockIFoodRepository foodRepository = new MockIFoodRepository(mc);
            MockIPantryRepository pantryRepository = new MockIPantryRepository(mc);
            FoodController controller = new FoodController(foodRepository, pantryRepository);
            Pantry p = new Pantry { PantryID = 0, Units = 35 };
            //Food f = new Food() { FoodID = id, Name = "updated name"};

            // execute food update
            // have to create a new controller context and make the httpcontext equal fakehttpcontext so the controller can
            // use the session when testing
            controller.ControllerContext = new System.Web.Mvc.ControllerContext() { HttpContext = FakeHttpContext() };
            controller.update(0);
            controller.update(p);

            // assert food has been updated
            p = pantryRepository.Pantrys.Select(x => x).Where(x => x.PantryID == 0).First();
            Assert.AreEqual(35, p.Units);
        }
    }
}
