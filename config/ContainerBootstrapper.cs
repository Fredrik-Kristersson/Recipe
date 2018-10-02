using System.Linq;
using System.Reflection;
using Autofac;
using ViewModelLib;

namespace Recipes.config
{
	public static class ContainerBootstrapper
	{
		private static IContainer container;

		public static IContainer Configure()
		{
			var builder = new ContainerBuilder();
			builder.RegisterType<DefaultConfigurator>().As<IConfigurator>();

			builder.RegisterType<DefaultDialogService>().As<IDialogService>();

			var assembly = Assembly.GetExecutingAssembly();
			builder.RegisterAssemblyTypes(assembly).InNamespace("Recipes.ViewModel")
			.AssignableTo<IViewModelBase>()
			.AsImplementedInterfaces()
			.InstancePerRequest();

			builder.RegisterAssemblyTypes(assembly).InNamespace("Recipes.ViewModel")
			 .Where(t => t.IsClass)
			 .As(t => t.GetInterfaces().Single(i => i.Name.EndsWith(t.Name)));

			//builder.RegisterAssemblyTypes().InNamespace("Recipes.ViewModel");

			container = builder.Build();
			return container;
		}

		public static TService Resolve<TService>()
		{
			return container.Resolve<TService>();
		}
	}
}
