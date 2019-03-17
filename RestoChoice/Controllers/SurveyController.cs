using Survey.Models;
using Survey.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Survey.Controllers
{
    public class SurveyController : Controller
    {
        // GET: Survey
        public ActionResult Index()
        {
			using (IDal dal = new Dal())
			{
				RestaurantSurveyViewModel surveyVM = new RestaurantSurveyViewModel {
					SelectionsList = dal.GetAllRestaurants().Select(r => new RestaurantSelectionViewModel {
						IsSelected = false, NameAndPhone = String.Format("{0} ({1})", r.Name, r.PhoneNumber)
					}).ToList()};

				return View(surveyVM);
			}
        }
    }
}