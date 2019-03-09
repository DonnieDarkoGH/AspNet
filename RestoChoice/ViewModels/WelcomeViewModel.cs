using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.ViewModels
{
	public class WelcomeViewModel
	{
		public string Message { get; set; }
		public DateTime Date { get; set; }
		public List<Models.Restaurant> Restaurants { get; set; }
		public Models.Restaurant Restaurant { get; set; }
	}
}
