using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodProject.Models
{
    public class Ingredient
    {
        public int IngredientID { get; set; }
        public int FoodID { get; set; }
        public int RecipeID { get; set; }

        public virtual Food Food {get; set;}
        public virtual Recipe Recipe { get; set; }
    }
}