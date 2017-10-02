using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodProject.Abstract;
using FoodProject.Models;

namespace FoodProject.Concrete
{
    public class FoodRepo : IFoodRepo
    {
        public FoodContext Context = new FoodContext();

        public IEnumerable<Food> Foods
        {
            get { return Context.Foods; }
        }

    }
}