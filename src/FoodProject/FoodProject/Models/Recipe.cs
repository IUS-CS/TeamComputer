using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodProject.Models
{
    public class Recipe
    {
        public int RecipeID { get; set; }
        public string Name { get; set; }
        
        //  virtual ICollection
        //  Read More to understand this real good like!
        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}