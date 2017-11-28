using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodProject.Abstract;
using FoodProject.Models;
using FoodProject.Concrete;

namespace FoodProject.Controllers
{
    /// <summary>
    /// Handles Recipe and Ingredient Functions
    /// </summary>
    public class RecipeController : Controller
    {
        private IRecipeRepository recipeRepository;
        private IFoodRepository foodRepository;
        private IIngredientRepository ingredientRepository;
        private IPantryRepository pantryRepository;
        private Ingredient ingredient = new Ingredient();

        /// <summary>
        /// Constructor
        /// Initialized the repositories needed for all functions needed for controller
        /// </summary>
        /// <param name="recipeRepository">creates recipe repository through dependency injection</param>
        /// <param name="foodRepository">>creates food repository through dependency injection</param>
        /// <param name="ingredientRepository">>creates ingredient repository through dependency injection</param>
        /// <param name="pantryRepository">>creates pantry repository through dependency injection</param>
        public RecipeController(IRecipeRepository recipeRepository, IFoodRepository foodRepository, IIngredientRepository ingredientRepository, IPantryRepository pantryRepository)
        {
            this.ingredientRepository = ingredientRepository;
            this.recipeRepository = recipeRepository;
            this.foodRepository = foodRepository;
            this.pantryRepository = pantryRepository;
        }
        
        /// <summary>
        /// List View
        /// shows a list of all recipies
        /// </summary>
        /// <returns> html view of  recipe repository</returns>
        public ActionResult Index()
        {
            return View(recipeRepository.Recipes);
        }
        /// <summary>
        /// Details View
        /// lists ingredients in current recipe
        /// </summary>
        /// <param name="id">recipe id passed thought List View</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            Recipe r = recipeRepository.GetRecipeID(id);
            return View(r);
        }
        /// <summary>
        /// GET:Create 
        /// NEW recipe object created, shows create recipe view
        /// </summary>
        /// <returns>View allowing text entry</returns>
        [HttpGet]
        public ActionResult Create()
        {
            Recipe r = new Recipe();
            return View(r);
        }
        /// <summary>
        /// POST:Create
        /// passes text to recipe object
        /// adds to table/db
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [HttpPost]
        public RedirectToRouteResult Create(Recipe r)
        {
            if (r.Name != null)
            {
                recipeRepository.Add(r);
                recipeRepository.Save();
            }
            return RedirectToAction("Index", "Recipe");
        }

        /// <summary>
        /// Part 1 to add ingredient entry to table 
        /// creates session to hold the recipeID of recipe you want to add food to
        /// </summary>
        /// <param name="id">from linked recipe object</param>
        /// <returns>View - list of all foods for Part 2</returns>
        public ActionResult AddRecipeId(int id)
        {
            Session["RecipeID"] = id;
            return View(foodRepository.Foods);
        }

        /// <summary>
        /// Part 2 to add ingredient entry to table 
        /// uses session ID(part 1) and foodID from view to set ingredient properties, then adds them
        /// </summary>
        /// <param name="id">food ID from view linked</param>
        /// <returns>View back to list</returns>
        public ActionResult AddFoodId(int id)
        {
            ingredient.FoodID = id;
            int ri = (int)Session["RecipeID"];
            ingredient.RecipeID = ri;
            ingredientRepository.Add(ingredient);
            ingredientRepository.Save();
            Recipe r = recipeRepository.GetRecipeID(ri);
            return View("Details",r);
        }

        /// <summary>
        /// Tells user whether they can make the recipe.
        /// Checks pantry.foodID against ingredient.foodID to see if user has foods requited to make recipe 
        /// </summary>
        /// <param name="user">global user id</param>
        /// <param name="recipeId">from htmlAction linker, recipe ID </param>
        /// <returns>View displaying "yes" or "no"</returns>
        public ActionResult CanIMakeIt(User user, int recipeId)
        {
            //make list of user foods
            IEnumerable<Pantry> userFoods = pantryRepository.Pantrys.Where(x => x.UserID == user.UserID).ToList();
            //makes list if food IDs from user list
            List<int> userFoodId = new List<int>();
            foreach(var x in userFoods)
            {
                userFoodId.Add(x.FoodID);
            }
            //list of ingredients for desired recipe from linked view ID param
            IEnumerable<Ingredient> recipeIngrgedients = ingredientRepository.Ingredients.Where(x => x.RecipeID == recipeId).ToList();
            bool b = false;
            int isIn;
            foreach (var x in recipeIngrgedients)
            {
                isIn = x.FoodID;
                if (userFoodId.Contains(isIn))
                {
                    b = true;
                }
                else
                {
                    b = false;
                    break;
                }
            }
            ViewBag.Answer = b == true ? "Yes" : "No";
            return View();

        }
    }
}