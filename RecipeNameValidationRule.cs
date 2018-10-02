using System;
using System.Globalization;
using System.Windows.Controls;

namespace Recipes
{
	class RecipeNameValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			string name = value.ToString();
			if (String.IsNullOrEmpty(name))
			{
				return new ValidationResult(false, "Can't have empty name.");
			}
			return ValidationResult.ValidResult;
		}
	}
}
