using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Models
{
	public class Restaurant
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Display(Name = "Phone")]
		public string PhoneNumber { get; set; }
	}
}