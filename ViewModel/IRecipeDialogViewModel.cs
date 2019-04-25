using ViewModelLib;

namespace Recipes.ViewModel
{
	public interface IRecipeDialogViewModel : IDialogViewModelBase
	{
		string Name { get; set; }

		string Description { get; set; }

		string Image { get; set; }

		string Source { get; set; }

		double Grade { get; set; }
	}
}