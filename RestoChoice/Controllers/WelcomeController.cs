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
				Message = "Welcome to the Home Page",
				Date = DateTime.Now,
				Restaurants = new List<Restaurant>() {
					new Restaurant {Name = "Resto pinambour", PhoneNumber ="1234" },
					new Restaurant {Name = "Resto tologie", PhoneNumber ="5678" },
					new Restaurant {Name = "Resto toro", PhoneNumber ="555" },
					new Restaurant {Name = "Resto ride", PhoneNumber ="2345" },
				}
			};

			return View(vm); 
        }
    }
}