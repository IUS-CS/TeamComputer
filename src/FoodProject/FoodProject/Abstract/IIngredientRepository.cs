using FoodProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodProject.Abstract
{
    public interface IIngredientRepository
    {
        IEnumerable<Ingredient> Ingredients { get; }
        void Add(Ingredient i);
        void Save();
    }
}
