using Unity;
using ViewModelLib;

namespace Recipe.config
{
	public class ContainerBootstrapper
	{
		public void RegisterTypes(IUnityContainer container)
		{
			container.RegisterType<IDialogService, DefaultDialogService>();
		}
	}
}
