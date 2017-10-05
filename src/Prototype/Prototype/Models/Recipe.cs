using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prototype.Models
{
    public class Recipe
    {
        public int ID { get; set; }
        public String Name { get; set; }
        

       /*
       * Navigation properties are typically defined as virtual so that they can take advantage of certain 
       * Entity Framework functionality such as lazy loading. (Lazy loading will be explained later, in the 
       * Reading Related Data tutorial later in this series.) If a navigation property can hold multiple 
       * entities (as in many-to-many or one-to-many relationships), its type must be a list in which entries 
       * can be added, deleted, and updated, such as ICollection.
       */
        public virtual ICollection<Ingredient> ingredients { get; set; } 
    }
}