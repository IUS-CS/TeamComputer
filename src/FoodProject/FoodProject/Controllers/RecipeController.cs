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
        private Ingredient ingredient = new Ingredient();
               

        public RecipeController(IRecipeRepository recipeRepository, IFoodRepository foodRepository, IIngredientRepository ingredientRepository)
        {
            this.ingredientRepository = ingredientRepository;
            this.recipeRepository = recipeRepository;
            this.foodRepository = foodRepository;
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
    }
}