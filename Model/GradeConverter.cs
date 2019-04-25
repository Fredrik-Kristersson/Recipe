using System.Globalization;

namespace Recipes.Model
{
	public static class GradeConverter
	{
		public static string Convert(double value)
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}

		public static double Convert(string value)
		{
			double.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double result);
			return result;
		}
	}
}
