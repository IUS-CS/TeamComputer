using System;
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
                return View(foods.ToPagedList(pageNumber, pageSize));
            }
        }


        [HttpGet]
        public ActionResult addFood()
        {
            Food f = new Food();

            return View(f);
        }


        [HttpPost]
        public RedirectToRouteResult addFood(User user, Food f)
        {
            if(f.Name!= null)
            {
                foodRepository.InsertFood(f);
                foodRepository.Save();
                Pantry p = new Pantry() { UserID = (int)user.UserID, FoodID = f.FoodID };
                pantryRepository.add(p);
                pantryRepository.save();
            }
            return RedirectToAction("Index", "Food");
        }

        public RedirectToRouteResult delete(int id)
        {
            pantryRepository.deleteByFoodID(id);
            pantryRepository.save();
            foodRepository.DeleteFood(id);
            foodRepository.Save();
            return RedirectToAction("Index", "Food");
        }
        [HttpGet]
        public ActionResult update(int id)
        {
            Food food = foodRepository.Foods.Where(x => x.FoodID == id).First();
            Session["FoodID"] = id;
            return View(food);
        }
        [HttpPost]
        public RedirectToRouteResult update(Food food)
        {
            food.FoodID = (int)Session["FoodID"];
            foodRepository.UpdateFood(food);
            foodRepository.Save();
            return RedirectToAction("Index", "Food");
        }
    }
}