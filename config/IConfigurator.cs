namespace Recipe.config
{
	interface IConfigurator
	{
		void ConfigureLog4Net(string configFilePath);
		void SetupDatabase(string databaseName, string databaseConfigFilePath);
	}
}
