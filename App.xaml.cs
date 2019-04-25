using Recipes.View;
using Recipes.ViewModel;
using System;
using System.IO;
using System.Windows;
using ViewModelLib;
using ViewModelLib.Boot;

namespace Recipes
{
	public partial class App : System.Windows.Application
	{

		private void App_Startup(object sender, StartupEventArgs e)
		{
			SetupConfiguration();
		}

		private void SetupConfiguration()
		{
			string logFilePath = Path.Combine(Environment.CurrentDirectory, "log4net_config.xml");

			var container = BootStrapper.Compose(this, logFilePath);

			var dbConfigurator = container.GetExport<IDatabaseStarter>();

			dbConfigurator.SetupDatabase("database.db", "appconfig.xml", GetType().Assembly);

			var mainViewModel = container.GetExport<IMainWindowViewModel>();
			var dialogService = container.GetExport<IDialogService>();

			dialogService.OpenWindow(typeof(MainWindow), mainViewModel);
		}
	}
}
