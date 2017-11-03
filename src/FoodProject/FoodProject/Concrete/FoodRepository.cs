using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodProject.Abstract;
using FoodProject.Models;
using System.Data.Entity;

namespace FoodProject.Concrete
{
    public class FoodRepository : IFoodRepository, IDisposable
    {
        public FoodContext context = new FoodContext();

        public FoodRepository()
        {
           // context = new FoodContext();
        }

       public IEnumerable<Food> Foods
        {
           get { return context.Foods; }
        }

        public Food GetFoodById(int foodId)
        {
            return context.Foods.Find(foodId);
            //throw new NotImplementedException();
        }

        public void InsertFood(Food food)
        {
            context.Foods.Add(food);
            //throw new NotImplementedException();
        }

        public void DeleteFood(int foodId)
        {
            Food food = context.Foods.Find(foodId);
            context.Foods.Remove(food);
        }

        public void UpdateFood(Food food)
        {
            Food f = context.Foods.Where(x => x.FoodID == food.FoodID).First();
            f.Name = food.Name;
            f.Units = food.Units;
            //throw new NotImplementedException();
        }

        public void Save()
        {
            context.SaveChanges();
            //throw new NotImplementedException();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
   
    }
}