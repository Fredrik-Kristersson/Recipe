using Recipes.Model;
using System.Collections.Generic;

namespace Recipes.Service
{
	public interface IRecipeService
	{
		void AddRecipe(Recipe recipe);
		void RemoveRecipe(int id);
		void EditRecipe(Recipe recipe);

		IEnumerable<Recipe> GetAllRecipes();
	}
}
