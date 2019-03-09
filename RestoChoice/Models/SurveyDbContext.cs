using System.Data.Entity;

namespace Survey.Models
{
	public class SurveyDbContext : DbContext
	{
		public DbSet<Restaurant> Restaurants { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Survey> Surveys { get; set; }
	}
}