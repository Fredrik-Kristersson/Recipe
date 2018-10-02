using ViewModelLib;

namespace Recipes.ViewModel
{
	public abstract class TabViewModelBase : ViewModelBase, ITabViewModelBase
	{
		public abstract string TabName { get; }

		public abstract bool IsCloseable { get; }
	}
}
