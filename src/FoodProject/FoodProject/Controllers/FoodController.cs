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
        public FoodController(IFoodRepository foodRepository)
        {
            this.foodRepository = foodRepository;
        }

        public ActionResult Index(User user, string sortOrder, string currentFilter, string searchString, int? page)
        {
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

                var foods = from f in user.Pantrys
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

    }
}