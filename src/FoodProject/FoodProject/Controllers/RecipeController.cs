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
    public class RecipeController : Controller
    {
        private IRecipeRepository recipeRepository;
        private IFoodRepository foodRepository;
        private IIngredientRepository ingredientRepository;
        private IPantryRepository pantryRepository;
        private Ingredient ingredient = new Ingredient();
               

        public RecipeController(IRecipeRepository recipeRepository, IFoodRepository foodRepository, IIngredientRepository ingredientRepository, IPantryRepository pantryRepository)
        {
            this.ingredientRepository = ingredientRepository;
            this.recipeRepository = recipeRepository;
            this.foodRepository = foodRepository;
            this.pantryRepository = pantryRepository;
        }
        

        public ActionResult Index()
        {
            return View(recipeRepository.Recipes);
        }

        public ActionResult Details(int id)
        {
            Recipe r = recipeRepository.GetRecipeID(id);
            return View(r);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Recipe r = new Recipe();
            return View(r);
        }
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

        // recipe name user clicks is id...passes it to global ingredient.recipe id
        // reviews list of all foods
        public ActionResult AddRecipeId(int id)
        {
            // pass recipe id from details to global ingredient obj
            Session["RecipeID"] = id;
           //return list of total foods 
            return View(foodRepository.Foods);
        }

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

        public ActionResult CanIMakeIt(User user, int recipeId)
        {
            
            //makes list of user foods
            IEnumerable<Pantry> userFoods = pantryRepository.Pantrys.
                                            Where(x => x.UserID == user.UserID).ToList();
            //makes list if food IDs from user list.........can i do this in one LINQ statement??????
            List<int> userFoodId = new List<int>();
            foreach(var x in userFoods)
            {
                userFoodId.Add(x.FoodID);
            }
            //list of ingredients for desired recipe from linked view ID param
            IEnumerable<Ingredient> recipeIngrgedients = ingredientRepository.Ingredients.
                                                        Where(x => x.RecipeID == recipeId).ToList();
            bool b = false;
            ViewBag.Answer = b == true ? "Yes" : "No";
            int isIn;
            foreach (var x in recipeIngrgedients)
            {
                isIn = x.FoodID;
                //if user foods does not contain ingredient.FoodId
                if (userFoodId.Contains(isIn))
                {
                    //cant make it view
                    b = true;
                }               
            }
            return View();

        }
    }
}