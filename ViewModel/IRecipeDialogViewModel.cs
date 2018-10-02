using ViewModelLib;

namespace Recipes.ViewModel
{
	public interface IRecipeDialogViewModel : IViewModelBase
	{
		Recipe Recipe { get; set; }
	}
}