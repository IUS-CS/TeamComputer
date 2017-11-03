using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodProject.Abstract;
using FoodProject.Models;
namespace FoodProject.Concrete
{
    public class PantryRepository : IPantryRepository
    {
        FoodContext context = new FoodContext();
        public IEnumerable<Pantry> Pantrys
        {
            get { return context.Pantrys; }
        }
        public void add(Pantry p)
        {
            context.Pantrys.Add(p);
            
        }
        public void save()
        {
            context.SaveChanges();
        }
        public void deleteByFoodID(int id)
        {
            Pantry p = context.Pantrys.Select(x => x).Where(x => x.FoodID == id).First();
            context.Pantrys.Remove(p);
        }
    }
}