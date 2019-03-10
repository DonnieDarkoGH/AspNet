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
						return View("Error");
					}

					return View(restaurant);
				}
			}
			else
			{
				return View("Error");
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
				if (Request.HttpMethod == "POST")
				{
					dal.ModifyRestaurant(resto.Id, resto.Name, resto.PhoneNumber);
				}

				return RedirectToAction("Index");
			}
		}
	}
}