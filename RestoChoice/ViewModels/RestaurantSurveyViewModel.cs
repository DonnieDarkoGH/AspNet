using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Survey.ViewModels
{
	public class RestaurantSurveyViewModel : IValidatableObject
	{
		public List<RestaurantSelectionViewModel> SelectionsList { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (SelectionsList.Any(r => r.IsSelected) == false)
			{
				yield return new ValidationResult("You must select at least one restaurant", new[] { "SelectionsList" });
			}
		}
	}
}