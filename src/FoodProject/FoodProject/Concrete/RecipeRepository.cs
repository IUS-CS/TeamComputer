using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodProject.Abstract;
using FoodProject.Models;

namespace FoodProject.Concrete
{
    public class RecipeRepository : IRecipeRepository
    {
        FoodContext context = new FoodContext();

        public IEnumerable<Recipe> Recipes
        {
            get { return context.Recipes; }
        }

        public void Add(Recipe r)
        {
            context.Recipes.Add(r);
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }
}