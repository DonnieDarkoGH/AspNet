﻿using System;
using System.Collections.Generic;

namespace Survey.Models
{
	public class Survey
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public virtual List<Restaurant> Restaurants { get; set; }
		public virtual List<Vote> Votes { get; set; }
	}
}