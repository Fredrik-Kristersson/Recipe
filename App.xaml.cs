using System.Windows;
using Autofac;
using Recipes.config;

namespace Recipes
{
	public partial class App : System.Windows.Application
	{
		IContainer container;

		private void App_Startup(object sender, StartupEventArgs e)
		{
			SetupConfiguration();
		}

		private void SetupConfiguration()
		{
			container = ContainerBootstrapper.Configure();

			IConfigurator configurator = container.Resolve<IConfigurator>();
			configurator.ConfigureLog4Net("log4net_config.xml");
			configurator.SetupDatabase("database.db", "appconfig.xml");
		}
	}
}
