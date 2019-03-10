using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;

namespace Survey
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

			IDatabaseInitializer<Models.SurveyDbContext> initializer = new Models.RestaurantInitializer();
			Database.SetInitializer(initializer);
			initializer.InitializeDatabase(new Models.SurveyDbContext());
		}
    }
}
