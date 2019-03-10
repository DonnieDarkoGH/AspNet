using System;
using System.Collections.Generic;

namespace Survey.ViewModels
{
	public class WelcomeViewModel
	{
		public int Id { get; set; }
		public string Message { get; set; }
		public DateTime Date { get; set; }
		public List<Models.Restaurant> Restaurants { get; set; }
		public Models.Restaurant Restaurant { get; set; }
		public string Login { get; set; }
	}
}
