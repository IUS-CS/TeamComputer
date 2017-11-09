using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodProject.Models;
using FoodProject.Concrete;

namespace FoodProject.Abstract
{
    public interface IPantryRepository
    {
        IEnumerable<Pantry> Pantrys { get; }
        void add(Pantry p);
        void save();
        void delete(int id);
        void updatePantry(Pantry p);
    }
}