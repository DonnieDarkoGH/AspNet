using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Survey.Models;
using Survey.ViewModels;

namespace RestoChoice.Controllers
{
    public class WelcomeController : Controller
    {
        // GET: Welcome
        public ActionResult Index()
        {
			WelcomeViewModel vm = new WelcomeViewModel {
				Id = 666,
				Message = "Welcome to the Home Page",
				Date = DateTime.Now,
				Restaurants = new List<Restaurant>() {
					new Restaurant {Id = 1, Name = "Resto pinambour", PhoneNumber ="1234" },
					new Restaurant {Id = 3, Name = "Resto tologie", PhoneNumber ="5678" },
					new Restaurant {Id = 5, Name = "Resto toro", PhoneNumber ="555" },
					new Restaurant {Id = 7, Name = "Resto ride", PhoneNumber ="2345" },
				},
				Restaurant = new Restaurant() { Name = "Resto toro", PhoneNumber = "555" },
				Login = "Enter your login"
			};

			ViewBag.RestoList = new SelectList(vm.Restaurants, "Id", "Name", 5);

			return View(vm); 
        }

		[ChildActionOnly]
		public ActionResult DisplayRestaurantsList()
		{
			List<Restaurant> restaurants = new List<Restaurant>() {
					new Restaurant {Id = 1, Name = "Resto pinambour", PhoneNumber ="1234" },
					new Restaurant {Id = 3, Name = "Resto tologie", PhoneNumber ="5678" },
					new Restaurant {Id = 5, Name = "Resto toro", PhoneNumber ="555" },
					new Restaurant {Id = 7, Name = "Resto ride", PhoneNumber ="2345" },
			};

			return PartialView(restaurants);
		}
    }
}