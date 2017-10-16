using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodProject.Models
{
    public class User
    {
        public int? UserID { get; set; } = null;
        public string Name { get; set; }
        public string Password { get; set; }

        //  virtual ICollection
        //  Read More to understand this real good like!
        public virtual ICollection<Pantry> Pantrys { get; set; }
    }
}