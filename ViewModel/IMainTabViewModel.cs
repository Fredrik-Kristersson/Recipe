using Recipes.Model;
using System.Collections.ObjectModel;

namespace Recipes.ViewModel
{
	public interface IMainTabViewModel : ITabViewModelBase
	{
		Recipe SelectedRecipe { get; set; }
		ObservableCollection<Recipe> Recipes { get; }
	}
}
