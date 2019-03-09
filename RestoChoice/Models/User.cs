using System.ComponentModel.DataAnnotations;

namespace Survey.Models
{
	public class User
	{
		public int Id { get; set; }
		[Required, MaxLength(80)]
		public string Name { get; set; }
		[Required]
		public string Password { get; set; }
	}
}