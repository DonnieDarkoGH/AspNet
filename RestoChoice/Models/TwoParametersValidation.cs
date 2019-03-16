using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Survey.Models
{
	public class TwoParametersValidation : ValidationAttribute
	{
		public string Parameter1 { get; set; }
		public string Parameter2 { get; set; }

		public TwoParametersValidation() : base("You must enter at least one of two values")
		{

		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			PropertyInfo[] properties = validationContext.ObjectType.GetProperties();
			PropertyInfo info1 = properties.FirstOrDefault(p => p.Name == Parameter1);
			PropertyInfo info2 = properties.FirstOrDefault(p => p.Name == Parameter2);

			string value1 = info1.GetValue(validationContext.ObjectInstance) as string;
			string value2 = info2.GetValue(validationContext.ObjectInstance) as string;

			if (string.IsNullOrWhiteSpace(value1) && string.IsNullOrWhiteSpace(value1))
			{
				return new ValidationResult(ErrorMessage);
			}

			return ValidationResult.Success;
		}

		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
		{
			ModelClientValidationRule rule = new ModelClientValidationRule();
			rule.ValidationType = "verifcontact";
			rule.ValidationParameters.Add("parameter1", Parameter1);
			rule.ValidationParameters.Add("parameter2", Parameter2);
			rule.ErrorMessage = ErrorMessage;

			return new List<ModelClientValidationRule>() { rule };
		}
	}
}