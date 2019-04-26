using log4net;
using Recipes.Model;
using Recipes.Service;
using Recipes.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ViewModelLib;
using ViewModelLib.Boot;

namespace Recipes.ViewModel
{
	[Export(typeof(IMainWindowViewModel))]
	public class MainWindowViewModel : DialogViewModelBase, IMainWindowViewModel
	{
		private readonly IRecipeService recipeService;
		private readonly IContainer container;
		private ILog log;

		[Import]
		public ITabRecipeContentViewModel RecipeContentViewModel { get; set; }

		[Import]
		public IMainTabViewModel RecipesListViewModel { get; set; }

		[ImportingConstructor]
		public MainWindowViewModel(
			IRecipeService recipeService,
			IDialogService dialogService,
			IMainTabViewModel mainTabViewModel,
			ILogger logger,
			IContainer container,
			ICommandFactory commandFactory) : base(dialogService)
		{
			log = logger.GetLogger(GetType());
			MainTab = mainTabViewModel;
			this.container = container;
			Tabs = new ObservableCollection<ITabViewModelBase>();
			this.recipeService = recipeService;

			OpenSourceCommand = commandFactory.CreateCommand(OpenSource);
			EditRecipeCommand = commandFactory.CreateCommand(EditRecipe);
			AddRecipeCommand = commandFactory.CreateCommand(AddRecipe);
			//OpenRecipeCommand = commandFactory.CreateCommand(OpenRecipe);
			CloseRecipeCommand = commandFactory.CreateCommand(CloseRecipe);
			RemoveRecipeCommand = commandFactory.CreateCommand(RemoveRecipe);
			ExitCommand = commandFactory.CreateCommand(OnExit);

			Tabs.Add(MainTab);
		}


		public ObservableCollection<Recipe> Recipes => MainTab.Recipes;

		public ICommand OpenSourceCommand { get; }
		public ICommand EditRecipeCommand { get; }
		public ICommand AddRecipeCommand { get; }
		public ICommand CloseRecipeCommand { get; }
		public ICommand OpenRecipeCommand { get; }
		public ICommand RemoveRecipeCommand { get; }
		public ICommand ExitCommand { get; }

		public ObservableCollection<ITabViewModelBase> Tabs { get; }

		public IMainTabViewModel MainTab
		{
			get => Get<IMainTabViewModel>();
			set => Set(value);
		}

		public int TabSelectedIndex
		{
			get => Get<int>();
			set => Set(value);
		}

		private void OpenSource(object obj)
		{
			if (obj == null || !(obj is string url))
			{
				return;
			}

			try
			{
				if (!String.IsNullOrEmpty(url.ToString()))
				{
					Process.Start(url.ToString());
				}
			}
			catch (Exception ex)
			{
				log.Error("Error occurred when opening source.", ex);
			}
		}

		private void AddRecipe(object obj)
		{
			var recipeDialogViewModel = container.GetExport<IRecipeDialogViewModel>();

			if (DialogService.OpenDialogWindow(typeof(AddRecipeDialog), recipeDialogViewModel, this) == true)
			{
				var recipe = new Recipe
				{
					Name = recipeDialogViewModel.Name,
					Description = recipeDialogViewModel.Description,
					Grade = recipeDialogViewModel.Grade,
					Image = recipeDialogViewModel.Image,
					Source = recipeDialogViewModel.Source,
					Url = recipeDialogViewModel.Source
				};

				recipeService.AddRecipe(recipe);
			}
		}

		private void RemoveRecipe(Object obj)
		{
			if (MainTab.SelectedRecipe == null)
			{
				return;
			}

			string message = $"Are you sure you want to delete [{MainTab.SelectedRecipe.Name}]";

			if (DialogService.ShowMessageBox("Remove Recipe?", message, MessageBoxButton.YesNo,
				MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				recipeService.RemoveRecipe(MainTab.SelectedRecipe.Id);
			}
		}

		private void EditRecipe(object o)
		{
			if (MainTab.SelectedRecipe == null)
			{
				return;
			}

			var recipeDialogViewModel = container.GetExport<IRecipeDialogViewModel>();
			var oldRecipe = MainTab.SelectedRecipe;
			recipeDialogViewModel.Name = oldRecipe.Name;
			recipeDialogViewModel.Description = oldRecipe.Description;
			recipeDialogViewModel.Grade = oldRecipe.Grade;
			recipeDialogViewModel.Source = oldRecipe.Source;
			recipeDialogViewModel.Image = oldRecipe.Image;

			if (DialogService.OpenDialogWindow(typeof(AddRecipeDialog), recipeDialogViewModel, this) == true)
			{
				var recipe = new Recipe
				{
					Id = oldRecipe.Id,
					Name = recipeDialogViewModel.Name,
					Description = recipeDialogViewModel.Description,
					Grade = recipeDialogViewModel.Grade,
					Image = recipeDialogViewModel.Image,
					Source = recipeDialogViewModel.Source,
					Url = recipeDialogViewModel.Source
				};

				recipeService.EditRecipe(recipe);
			}
		}

		private Recipe FindRecipeFromName(string name)
		{
			return MainTab.Recipes.FirstOrDefault(curr => curr.Name.Equals(name));
		}

		//private void OpenRecipe(object obj)
		//{
		//	if (obj != null && obj is string name)
		//	{
		//		try
		//		{
		//			if (!string.IsNullOrEmpty(name))
		//			{
		//				Recipe rec = FindRecipeFromName(name);
		//				if (rec != null)
		//				{
		//					var tabViewModel = new TabRecipeContentViewModel();
		//					tabViewModel.Name = rec.Name;
		//					tabViewModel.Description = rec.Description;
		//					tabViewModel.Image = rec.Image;
		//					Tabs.Add(tabViewModel);
		//					TabSelectedIndex = Tabs.Count - 1;
		//				}
		//			}
		//		}
		//		catch (Exception ex)
		//		{
		//			log.Error("Error occurred!", ex);
		//		}
		//	}
		//}

		private void CloseRecipe(object obj)
		{
			if (obj != null && obj is string name && !string.IsNullOrEmpty(name))
			{
				var tabViewModel = Tabs.FirstOrDefault(t => string.Equals(t.TabName, name, StringComparison.OrdinalIgnoreCase));
				if (tabViewModel != null)
				{
					Tabs.Remove(tabViewModel);
				}
			}
		}

		private void OnExit(object obj)
		{
			Close();
		}

	}
}