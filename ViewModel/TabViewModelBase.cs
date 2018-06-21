using ViewModelLib;

namespace Recipe.ViewModel
{
	public abstract class TabViewModelBase : ViewModelBase
	{

		public abstract string TabName { get; }

		public abstract bool IsCloseable { get; }
	}
}
