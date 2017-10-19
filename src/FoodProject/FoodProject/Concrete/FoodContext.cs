using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FoodProject.Models;
using FoodProject.Concrete;

namespace FoodProject.Concrete
{
    public class FoodContext : DbContext
    {
        public FoodContext() : base("FoodContext")
        {
            Database.SetInitializer(new Initializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Pantry> Pantrys { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
    }
}