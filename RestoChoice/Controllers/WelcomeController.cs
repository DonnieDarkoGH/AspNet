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

		[HttpPost]
		[ActionName("Index")]
		public ActionResult CreateSurvey()
		{
			using (IDal dal = new Dal())
			{
				int newSurveyId = dal.CreateSurvey();
				return RedirectToAction("Index", "Survey", new { id = newSurveyId });
			}
		}
    }
}