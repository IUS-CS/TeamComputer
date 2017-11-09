﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodProject.Concrete;
using FoodProject.Abstract;
using PagedList;
using FoodProject.Models;
namespace FoodProject.Controllers
{
    public class FoodController : Controller
    {
        private IFoodRepository foodRepository;
        private IPantryRepository pantryRepository;

        public FoodController(IFoodRepository foodRepository,IPantryRepository pantryRepository)
        {
            this.foodRepository = foodRepository;
            this.pantryRepository = pantryRepository;
        }

        public ActionResult Index(User user, string sortOrder, string currentFilter, string searchString, int? page)
        {
            IEnumerable<Pantry> pantrys =  pantryRepository.Pantrys.Where(x => x.UserID == user.UserID).ToList();
            if (user.UserID == null)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                var foods = from f in pantrys
                            select f.Food;

                if (!String.IsNullOrEmpty(searchString))
                {
                    foods = foods.Where(f => f.Name.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        foods = foods.OrderByDescending(f => f.Name);
                        break;
                    default:
                        foods = foods.OrderBy(f => f.Name);
                        break;
                }

                int pageSize = 5;
                int pageNumber = (page ?? 1);
                return View(pantrys.ToPagedList(pageNumber, pageSize));
            }
        }

        [HttpGet]
        public ActionResult addFood()
        {
            var foods = foodRepository.Foods.ToList();


            return View(foods);
        }
        [HttpPost]
        public RedirectToRouteResult addFoods(User user, Food f)
        {
            Pantry p = new Pantry() { UserID = (int)user.UserID, FoodID = foodID };
            pantryRepository.add(p);
            pantryRepository.save();
            return RedirectToAction("Index", "Food");
        }

        public RedirectToRouteResult delete(int id)
        {
            pantryRepository.delete(id);
            pantryRepository.save();
            return RedirectToAction("Index", "Food");
        }
        [HttpGet]
        public ActionResult update(int id)
        {
            Pantry pantry = pantryRepository.Pantrys.Where(x => x.PantryID == id).First();
            Session["PantryID"] = id;
            return View(pantry);
        }
        [HttpPost]
        public RedirectToRouteResult update(Pantry pantry)
        {
            pantry.PantryID = (int)Session["PantryID"];
            pantryRepository.updatePantry(pantry);
            pantryRepository.save();
            return RedirectToAction("Index", "Food");
        }
    }
}