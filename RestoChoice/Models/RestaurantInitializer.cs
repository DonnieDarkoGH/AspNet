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
			context.Restaurants.Add(new Restaurant { Id = 1, Name = "Resto pinambour", PhoneNumber = "123" });
			context.Restaurants.Add(new Restaurant { Id = 2, Name = "Resto pinière", PhoneNumber = "456" });
			context.Restaurants.Add(new Restaurant { Id = 3, Name = "Resto toro", PhoneNumber = "789" });

			base.Seed(context);
		}
	}
}