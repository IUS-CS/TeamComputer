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
    /// <summary>
    /// food controller handles the viewing/editing of food
    /// </summary>
    public class FoodController : Controller
    {
        private IFoodRepository foodRepository;
        private IPantryRepository pantryRepository;

        public FoodController(IFoodRepository foodRepository,IPantryRepository pantryRepository)
        {
            this.foodRepository = foodRepository;
            this.pantryRepository = pantryRepository;
        }

        /// <summary>
        /// the defualt view for the food controller if the user isn't logged in it redirects to login
        /// if the user is logged in it displays a view that shows all of the users foods, and gives option to add more
        /// </summary>
        /// <param name="user">The object that holds the logged in user, you don't need to pass this in
        /// it is model binded and paassed for you</param>
        /// <param name="sortOrder"></param>
        /// <param name="currentFilter"></param>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <returns>View of the login screen if not logged in, if logged it returns view of foods</returns>
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
        /// <summary>
        /// this method is called to bring up the view that lets the user add a food to their pantry
        /// </summary>
        /// <returns>a view to add food</returns>
        [HttpGet]
        public ActionResult addFood()
        {
            var foods = foodRepository.Foods.ToList();
            return View(foods);
        }
        /// <summary>
        /// this method adds the food the user selected from the addFood method to the user's pantry
        /// </summary>
        /// <param name="user">The object that holds the logged in user, you don't need to pass this in
        /// it is model binded and paassed for you</param>
        /// <param name="id">the id of the food to add to the pantry</param>
        /// <returns>a view of the updated food list</returns>
        public RedirectToRouteResult addFoods(User user,int id)
        {

            Pantry p = new Pantry() { UserID = (int)user.UserID, FoodID = id };
            pantryRepository.add(p);
            pantryRepository.save();
            return RedirectToAction("Index", "Food");
        }
        /// <summary>
        /// this method deletes food out of the user's pantry
        /// </summary>
        /// <param name="id">the id of the food to delete</param>
        /// <returns>a view of the updated food list</returns>
        public RedirectToRouteResult delete(int id)
        {
            pantryRepository.delete(id);
            pantryRepository.save();
            return RedirectToAction("Index", "Food");
        }
        /// <summary>
        /// this method updates the amount of a food item the user has
        /// </summary>
        /// <param name="id">is the id of the food to update</param>
        /// <returns>a view that lets the user update the food item</returns>
        [HttpGet]
        public ActionResult update(int id)
        {
            Pantry pantry = pantryRepository.Pantrys.Where(x => x.PantryID == id).First();
            Session["PantryID"] = id;
            return View(pantry);
        }
        /// <summary>
        /// this method gets a pantry object from the view in the update method, and actually updates the pantry list
        /// </summary>
        /// <param name="pantry">this pantry object is passed to the method through the update view</param>
        /// <returns></returns>
        [HttpPost]
        public RedirectToRouteResult update(Pantry pantry)
        {
            pantry.PantryID = (int)Session["PantryID"];
            pantryRepository.updatePantry(pantry);
            pantryRepository.save();
            return RedirectToAction("Index", "Food");
        }
        /// <summary>
        /// this method handles when a users searches for food
        /// </summary>
        /// <returns>a view with an updated list of foods similar to the search term</returns>
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
        /// <summary>
        /// this method returns a view that lets the user create a new food
        /// </summary>
        /// <returns>a view</returns>
        [HttpGet]
        public ViewResult createNewFood()
        {
            Food f = new Food();
            return View(f);
        }
        /// <summary>
        /// this method gets the new food the user created and adds it to the database
        /// if the food item doesn't already exist
        /// </summary>
        /// <param name="user">The object that holds the logged in user, you don't need to pass this in
        /// it is model binded and paassed for you</param>
        /// <param name="f">the food object passed through the createNewFood view</param>
        /// <returns>a view</returns>
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