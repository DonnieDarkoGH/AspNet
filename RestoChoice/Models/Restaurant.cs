using System.Collections.Generic;
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
		[RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "This is not a valid phone number")]
		[TwoParametersValidation(Parameter1 = "PhoneNumber", Parameter2 = "Email", ErrorMessage = "You must specify at least one way to contact the restaurant")]
		public string PhoneNumber { get; set; }
		[TwoParametersValidation(Parameter1 = "PhoneNumber", Parameter2 = "Email", ErrorMessage = "You must specify at least one way to contact the restaurant")]
		public string Email { get; set; }

		//public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		//{
		//	if (string.IsNullOrWhiteSpace(PhoneNumber) && string.IsNullOrWhiteSpace(Email))
		//	{
		//		yield return new ValidationResult(
		//			"You must enter a phone number or an email adress", 
		//			new [] { "PhoneNumber", "Email"});
		//	}
		//}
	}
}