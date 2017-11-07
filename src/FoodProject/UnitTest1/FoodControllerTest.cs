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
        class MyContext
        {
            public List<Food> foods { get; set; } 
            public List<Pantry> pantrys { get; set; } 
            public MyContext()
            {
                foods  = new List<Food>( new Food[] { new Food() { FoodID = 0, Name = "food 0", Units = 1 },
                    new Food() {FoodID = 1, Name = "food 1",Units = 1 },
                    new Food() {FoodID = 2, Name = "food 2", Units = 1 } });
                pantrys = new List<Pantry>(new Pantry[] { new Pantry() { PantryID = 0,FoodID = 0,UserID = 1},
                new Pantry() { PantryID = 1,FoodID = 1,UserID = 1},
                new Pantry() { PantryID = 2,FoodID = 2,UserID = 1}});
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

            public void deleteByFoodID(int id)
            {
                Pantry p = c.pantrys.Select(x => x).Where(x => x.FoodID == id).First();
                c.pantrys.Remove(p);
                c.pantrys.Select(x => x);
            }

            public void save()
            {
                //save not needed
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
                f.Units = food.Units;
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
            Food f = new Food() { FoodID = -1, Name = "new food", Units = 1 };

            // execute add food
            controller.addFood(user, f);

            // assert food is added
            //if foodid ==3 then the id got updated
            Assert.IsTrue(f.FoodID == 3);
            f = foodRepository.Foods.Select(x => x).Where(x => x.FoodID == f.FoodID).FirstOrDefault();
            //if not null it found the food
            Assert.IsNotNull(f);
            Assert.AreEqual("new food", f.Name);
            //if not null the food got added to the user pantry
            Assert.IsNotNull(pantryRepository.Pantrys.Select(x => x).Where(x => x.FoodID == f.FoodID).FirstOrDefault());
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
            //if null food is not in list of foods
            Assert.IsNull(foodRepository.Foods.Select(x => x).Where(x => x.FoodID == id).FirstOrDefault());
            //if null food was removed from user pantry
            Assert.IsNull(pantryRepository.Pantrys.Select(x => x).Where(x => x.FoodID == id).FirstOrDefault());
        }

        [TestMethod]
        public void CanUpdateFood()
        {
            // setup
            MyContext mc = new MyContext();
            MockIFoodRepository foodRepository = new MockIFoodRepository(mc);
            MockIPantryRepository pantryRepository = new MockIPantryRepository(mc);
            FoodController controller = new FoodController(foodRepository, pantryRepository);
            int id = 0;
            Food f = new Food() { FoodID = id, Name = "updated name", Units = Int32.MaxValue };

            // execute food update
            controller.ControllerContext = new System.Web.Mvc.ControllerContext() { HttpContext = FakeHttpContext() };
            //controller.Session = new MockHttpSession();
            controller.update(id);
            controller.update(f);

            // assert food has been updated
            f = foodRepository.Foods.Select(x => x).Where(x => x.FoodID == f.FoodID).FirstOrDefault();
            Assert.AreEqual("updated name", f.Name);
            Assert.AreEqual(Int32.MaxValue, f.Units);
        }
    }
}
