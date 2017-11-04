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

        public RecipeController(IRecipeRepository recipeRepository)
        {
            this.recipeRepository = recipeRepository;
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

    }
}