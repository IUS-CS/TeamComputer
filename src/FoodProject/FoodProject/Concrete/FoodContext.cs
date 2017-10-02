using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FoodProject.Models;

namespace FoodProject.Concrete
{
    public class FoodContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Pantry> Pantrys { get; set; }
    }
}