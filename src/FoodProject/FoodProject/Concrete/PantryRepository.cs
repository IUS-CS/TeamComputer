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
        public void delete(int id)
        {
            Pantry p = context.Pantrys.Select(x => x).Where(x => x.PantryID == id).First();
            context.Pantrys.Remove(p);
        }
        public void updatePantry(Pantry p)
        {
            Pantry temp = context.Pantrys.Select(x => x).Where(x => x.PantryID == p.PantryID).First();
            temp.Units = p.Units;
        }
    }
}