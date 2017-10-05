using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prototype.Models
{
    //foods for recipe
    public class Ingredient
    {
        public int ID { get; set; }
        public int FoodID { get; set; }
        public int RecipeID { get; set; } 
    }
}