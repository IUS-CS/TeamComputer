using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prototype.Models
{
    // food user has
    public class Pantry
    {
        public int ID { get; set; }
        public int FoodID { get; set; }
        public int UserID { get; set; }

    }
}