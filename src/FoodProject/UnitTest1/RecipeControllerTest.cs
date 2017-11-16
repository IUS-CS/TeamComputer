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
    public class RecipeControllerTest
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
            public List<Ingredient>ingredients { get; set; }
            public List<Recipe>recipies { get; set; }
            public MyContext()
            {
                foods = new List<Food>(new Food[] { new Food() { FoodID = 0, Name = "food 0"},
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
                food.FoodID = c.foods.Count();
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
        public void TestMethod1()
        {

        }
    }
}
