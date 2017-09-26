using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Prototype.Models;

namespace Prototype.DAL
{
    public class FoodInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<FoodContext>
    {
        protected override void Seed(FoodContext context)
        {
            var students = new List<User>
            {
            new User{Name="Madden"}
            };

            students.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

            var foods = new List<Food>
            {
            new Food{Name="milk"},
            new Food{Name="coco"},
            new Food{Name="turkey"},
            new Food{Name="cheese"},
            new Food{Name="bread"},
            new Food{Name="pasta"},
            new Food{Name="sauce"},
            new Food{Name="ricotta"},
            new Food{Name="cocaine"},
            new Food{Name="baking soda"},
            };
            foods.ForEach(s => context.Foods.Add(s));
            context.SaveChanges();

            var pantries = new List<Pantry>
            {
                new Pantry{UserID=1,FoodID=1},
                new Pantry{UserID=1,FoodID=2},
                new Pantry{UserID=1,FoodID=3},
                new Pantry{UserID=1,FoodID=4},
                new Pantry{UserID=1,FoodID=5},
                new Pantry{UserID=1,FoodID=6},
                new Pantry{UserID=1,FoodID=7},
            };

            var ingredients = new List<Ingredient>
            {
                new Ingredient{RecipeID=1, FoodID=1},
                new Ingredient{RecipeID=1, FoodID=2},
                new Ingredient{RecipeID=2, FoodID=3},
                new Ingredient{RecipeID=2, FoodID=4},
                new Ingredient{RecipeID=2, FoodID=5},
                new Ingredient{RecipeID=3, FoodID=6},
                new Ingredient{RecipeID=3, FoodID=7},
                new Ingredient{RecipeID=3, FoodID=8},
                new Ingredient{RecipeID=4, FoodID=9 },
                new Ingredient{RecipeID=4, FoodID=10},
            };

            var recipies = new List<Recipe>
            {
            new Recipe{Name= "ChocoMilk"},
            new Recipe{Name= "Sandwich"},
            new Recipe{Name= "Italian"},
            new Recipe{Name= "Crack"},

            };
            recipies.ForEach(s => context.Recipies.Add(s));
            context.SaveChanges();
        }
    }
}