using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FoodProject.Models;
using FoodProject.Concrete;

namespace FoodProject
{
    public class Initializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<FoodContext>
    {
        protected override void Seed(FoodContext context)
        {
            var users = new List<User>
            {
                new User{Name="Ryan@mail.com",Password="Ryan"},
                new User{Name="user@mail.com",Password="password"}
            };

            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();


            var foods = new List<Food>
            {
                new Food{Name="Milk"},
                new Food{Name="Bread"},
                new Food{Name="Cheese"},
                new Food{Name="Ham" },
                new Food{Name="Lettuce"}
            };
            foods.ForEach(f => context.Foods.Add(f));
            context.SaveChanges();


            var pantry = new List<Pantry>
            {
                new Pantry{UserID=1, FoodID=1, Units = 1},
                new Pantry{UserID=1, FoodID=2, Units =1},
                new Pantry{UserID=1, FoodID=3, Units = 1},
                new Pantry{UserID=2, FoodID=1, Units = 2},
                new Pantry{UserID=2, FoodID=5, Units = 1},
            };
            pantry.ForEach(p => context.Pantrys.Add(p));
            context.SaveChanges();

            var recipe = new List<Recipe>
            {
                new Recipe{Name="Ham Sandwich"},
                new Recipe{Name= "Milky Ham"}
            };
            recipe.ForEach(r => context.Recipes.Add(r));
            context.SaveChanges();

            var ingredient = new List<Ingredient>
            {
                new Ingredient{FoodID=2, RecipeID=1},
                new Ingredient{FoodID=4, RecipeID=1},
                new Ingredient{FoodID=1, RecipeID=2},
                new Ingredient{FoodID=4, RecipeID=2}
            };
            ingredient.ForEach(i => context.Ingredients.Add(i));
            context.SaveChanges();
        }
    }
}