using FoodProject.Abstract;
using FoodProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodProject.Concrete
{
    public class IngredientRepository : IIngredientRepository
    {
        FoodContext context = new FoodContext();

        public IEnumerable<Ingredient> Ingredients
        {
            get { return context.Ingredients; }
        }

        public void Add(Ingredient i)
        {
            context.Ingredients.Add(i);
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }
}