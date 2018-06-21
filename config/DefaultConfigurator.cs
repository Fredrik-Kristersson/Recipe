using System;
using System.IO;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using log4net.Config;
using Recipe.Model.Database;

namespace Recipe.config
{
	class DefaultConfigurator : IConfigurator
	{
		public void ConfigureLog4Net(string configFilePath)
		{
			XmlConfigurator.Configure(new FileInfo(configFilePath));
		}

		public void SetupDatabase(string databaseName, string databaseConfigFilePath)
		{
			try
			{
				XmlConfigurationSource source = new XmlConfigurationSource(databaseConfigFilePath);
				ActiveRecordStarter.Initialize(source, typeof(RecipeVO));
				if (!DatabaseExists(databaseName))
				{
					ActiveRecordStarter.CreateSchema();
				}
			}
			catch (Exception ex)
			{
				while (ex != null)
				{
					Console.Out.WriteLine("Message: " + ex.Message);
					ex = ex.InnerException;
				}
			}
		}

		private static bool DatabaseExists(string databaseName)
		{
			return File.Exists(databaseName);
		}
	}
}
