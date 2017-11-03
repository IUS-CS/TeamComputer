using System;
using System.Collections.Generic;
using FoodProject.Models;

namespace FoodProject.Abstract
{
    public interface IFoodRepository: IDisposable
    {
        IEnumerable<Food> Foods { get; }
        Food GetFoodById(int foodId);
        void InsertFood(Food food);
        void DeleteFood(int food);
        void UpdateFood(Food food);
        void Save();
    }
}
