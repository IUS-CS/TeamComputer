using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodProject.Concrete;
using FoodProject.Abstract;
using PagedList;
using FoodProject.Models;
using System.Web.UI.WebControls;

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

                int pageSize = 20;
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
        public RedirectToRouteResult addFoods(User user,int id)
        {

            Pantry p = new Pantry() { UserID = (int)user.UserID, FoodID = id };
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
        public ActionResult update(int id,String search = "")
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
        public ViewResult addFoodSearch()
        {
            //string s = ViewBag.Search; 
            String s = String.Format("{0}", Request.Form["SearchBox"]);
            s = s.ToLower();
            IEnumerable<Food> foods;
            if (!s.Equals(""))
            {
                foods = foodRepository.Foods.Where(x => x.Name.ToLower().Contains(s)).ToList();
            }
            else
            {
                foods = foodRepository.Foods.ToList();
            }
            return View("addFood", foods);
        }
        [HttpGet]
        public ViewResult createNewFood()
        {
            Food f = new Food();
            return View(f);
        }

        [HttpPost]
        public RedirectToRouteResult createNewFood(User user, Food f)
        {
            if (foodRepository.Foods.Where(x => x.Name.ToLower().Equals(f.Name)).FirstOrDefault() == null)
            {
                foodRepository.InsertFood(f);
                foodRepository.Save();
                pantryRepository.add(new Pantry { UserID = (int)user.UserID, FoodID = f.FoodID });
                pantryRepository.save();
                return RedirectToAction("Index", "Food");
            }
            else
            {
                return RedirectToAction("CreateNewFood", "Food");
            }
        }
    }
}