using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Survey.Models;
using Survey.ViewModels;

namespace Survey.Controllers
{
    public class WelcomeController : Controller
    {
        // GET: Welcome
        public ActionResult Index()
        {

			return View(); 
        }

		public ActionResult DisplayMessageWithDate(string id)
		{
			ViewBag.Message = "Hello " + id + " !";
			ViewData["Date"] = DateTime.Now;

			return View("Index");
		}
    }
}