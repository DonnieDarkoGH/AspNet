using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Survey;

namespace Survey.Models
{
	public class RestaurantInitializer : DropCreateDatabaseAlways<SurveyDbContext>
	{
		protected override void Seed(SurveyDbContext context)
		{
			context.Restaurants.Add(new Restaurant { Id = 1, Name = "Resto pinambour", PhoneNumber = "0611223344" });
			context.Restaurants.Add(new Restaurant { Id = 2, Name = "Resto pinière", PhoneNumber = "0655667788" });
			context.Restaurants.Add(new Restaurant { Id = 3, Name = "Resto toro", PhoneNumber = "0622446688" });

			base.Seed(context);
		}
	}
}