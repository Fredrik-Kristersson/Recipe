using ViewModelLib;

namespace Recipes.ViewModel
{
	public interface ITabViewModelBase : IViewModelBase
	{
		string TabName { get; }
		bool IsCloseable { get; }
	}
}
