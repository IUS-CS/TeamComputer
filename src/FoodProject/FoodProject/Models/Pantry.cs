using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodProject.Models
{
    public class Pantry
    {
        public int PantryID { get; set; }
        public int UserID { get; set;}
        public int FoodID { get; set; }

        public virtual User User { get; set; }
        public virtual Food Food { get; set; }
    }
}