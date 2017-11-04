using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodProject.Abstract;
using FoodProject.Models;

namespace FoodProject.Controllers
{
    public class RecipeController : Controller
    {
        private IRecipeRepository RecipeRepository;

        public RecipeController(IRecipeRepository recipeRepository)
        {
            this.RecipeRepository = recipeRepository;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}