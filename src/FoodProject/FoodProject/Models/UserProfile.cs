using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodProject.Models
{
    public class UserProfile
    {
        public string currentPW { get; set; }
        public string newPW { get; set; }
        public string newPW2 { get; set; }
    }
}