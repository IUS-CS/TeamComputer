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

        [HttpGet]
        public ActionResult AddIngredient(int id)
        {

            // new up an ingredient to add, pass  it recipe id from details action link

            Ingredient i = new Ingredient
            {
                RecipeID = id
            };
            return View(foodRepository.Foods);
        }

        [HttpPost]
        public RedirectToRouteResult AddIngredient(Ingredient i , int id)
        {
            i.FoodID = id;
            ingredientRepository.Add(i);
            ingredientRepository.Save();
           
            return RedirectToAction("Index", "Recipe");
        }

    }
}