using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Survey.Models;

namespace Survey.Controllers
{
    public class RestaurantController : Controller
    {
        // GET: Restaurant
        public ActionResult Index()
        {
			using (IDal dal = new Dal())
			{
				List<Restaurant> allRestaurants = dal.GetAllRestaurants();
				return View(allRestaurants);
			}		
        }

		public ActionResult ModifyRestaurant(int? id)
		{
			if (id.HasValue)
			{
				using (IDal dal = new Dal())
				{
					Restaurant restaurant = dal.GetAllRestaurants().FirstOrDefault(r => r.Id == id);

					if (restaurant == null)
					{
						return HttpNotFound();
					}

					return View(restaurant);
				}
			}
			else
			{
				return View("Error");
			}
		}

		public ActionResult CreateRestaurant()
		{
			return View();
		}

		[HttpPost]
		public ActionResult CreateRestaurant(Restaurant resto)
		{
			using (IDal dal = new Dal())
			{
				if (dal.IsExistingRestaurant(resto.Name))
				{
					ModelState.AddModelError("Name", "This restaurant already exists");
					return View(resto);
				}
				else if (ModelState.IsValid)
				{
					dal.CreateRestaurant(resto.Name, resto.PhoneNumber);
					return RedirectToAction("Index");
				}
				else
				{
					return View(resto);
				}		
			}
		}

		[HttpPost]
		public ActionResult ModifyRestaurant(Restaurant resto)
		{
			if (ModelState.IsValid == false)
			{
				return View(resto);
			}

			using (IDal dal = new Dal())
			{
				dal.ModifyRestaurant(resto.Id, resto.Name, resto.PhoneNumber);

				return RedirectToAction("Index");
			}
		}
	}
}