using System.Windows;
using Recipe.config;
using Unity;

namespace Recipe
{
	public partial class App : System.Windows.Application
	{

		private void App_Startup(object sender, StartupEventArgs e)
		{
			SetupConfiguration();
		}

		private void SetupConfiguration()
		{
			IConfigurator configurator = new DefaultConfigurator();
			configurator.ConfigureLog4Net("log4net_config.xml");
			configurator.SetupDatabase("database.db", "appconfig.xml");

			var container = new UnityContainer();
			var bootstrapper = new ContainerBootstrapper();
			bootstrapper.RegisterTypes(container);
		}
	}
}
