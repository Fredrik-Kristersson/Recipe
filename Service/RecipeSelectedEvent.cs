using Recipes.Model;
using ViewModelLib.Event;

namespace Recipes.Service
{
	public class RecipeSelectedEvent : ISubscriptionEvent
	{
		public RecipeSelectedEvent(Recipe selectedRecipe)
		{
			SelectedRecipe = selectedRecipe;
		}
		public Recipe SelectedRecipe { get; set; }
	}
}
