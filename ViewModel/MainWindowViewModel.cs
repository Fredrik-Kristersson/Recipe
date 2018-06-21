
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Castle.ActiveRecord;
using Recipe.Model.Database;
using ViewModelLib;

namespace Recipe.ViewModel
{
	public class MainWindowViewModel : ViewModelBase
	{
		private readonly IDialogService dialogService;

		public MainWindowViewModel(IDialogService dialogService)
		{
			MainTab = new MainTabViewModel();
			Tabs = new ObservableCollection<TabViewModelBase>();

			this.dialogService = dialogService;

			OpenSourceCommand = new MyCommand(OpenSource);
			EditRecipeCommand = new MyCommand(EditRecipe);
			AddRecipeCommand = new MyCommand(AddRecipe);
			OpenRecipeCommand = new MyCommand(OpenRecipe);
			CloseRecipeCommand = new MyCommand(CloseRecipe);
			RemoveRecipeCommand = new MyCommand(RemoveRecipe);

			AddMainTab();
		}

		public ObservableCollection<Recipe> Recipes => MainTab.Recipes;

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
			catch (Exception)
			{
				//log.Error("Error occurred!", ex);
			}
		}

		public ICommand OpenSourceCommand { get; }
		public ICommand EditRecipeCommand { get; }
		public ICommand AddRecipeCommand { get; }
		public ICommand CloseRecipeCommand { get; }
		public ICommand OpenRecipeCommand { get; }
		public MyCommand RemoveRecipeCommand { get; }

		public ObservableCollection<TabViewModelBase> Tabs { get; }

		public MainTabViewModel MainTab
		{
			get => Get<MainTabViewModel>();
			set => Set(value);
		}


		public int Position
		{
			get => Get<int>();
			set => Set(value);
		}

		public int TabSelectedIndex
		{
			get => Get<int>();
			set => Set(value);
		}

		private void AddRecipe(object obj)
		{
			var addRecipeDialogViewModel = new AddRecipeDialogViewModel(dialogService);

			if (dialogService.OpenDialogWindow(typeof(AddRecipeDialog), addRecipeDialogViewModel, this) == true)
			{
				RecipeVO newRecipe = ToRecipeVO(addRecipeDialogViewModel.Model);
				newRecipe.Create();
				MainTab.RefreshRecipesFromModel();
			}
		}

		private void RemoveRecipe(Object obj)
		{
			if (MainTab.SelectedRecipe == null)
			{
				return;
			}

			string message = $"Are you sure you want to delete [{MainTab.SelectedRecipe.Name}]";

			if (dialogService.ShowMessageBox(this, "Remove Recipe?", message, MessageBoxButton.YesNo,
				MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				RecipeVO recipeVo = ActiveRecordBase<RecipeVO>.Find(MainTab.SelectedRecipe.Id);
				recipeVo.Delete();

				MainTab.RefreshRecipesFromModel();
			}
		}

		public void EditRecipe(object o)
		{
			if (MainTab.SelectedRecipe == null)
			{
				return;
			}

			var clone = MainTab.SelectedRecipe.Clone() as Recipe;
			if (clone == null)
			{
				throw new ArgumentException("Recipe was not cloned correctly.");
			}
			var editRecipeDialogViewModel = new AddRecipeDialogViewModel(dialogService, clone);

			if (dialogService.OpenDialogWindow(typeof(AddRecipeDialog), editRecipeDialogViewModel, this) == true)
			{
				var recipe = editRecipeDialogViewModel.Model;

				RecipeVO edited = ActiveRecordBase<RecipeVO>.Find(recipe.Id);
				edited.Description = recipe.Description;
				edited.Grade = recipe.Grade;
				edited.Image = recipe.Image;
				edited.Name = recipe.Name;
				edited.Source = recipe.Source;
				edited.Url = recipe.Url;
				edited.SaveCopy();
				MainTab.RefreshRecipesFromModel();
			}
		}

		private RecipeVO ToRecipeVO(Recipe recipe)
		{
			RecipeVO curr = new RecipeVO();
			curr.Id = recipe.Id;
			curr.Name = recipe.Name;
			curr.Grade = recipe.Grade;
			curr.Image = recipe.Image;
			curr.Source = recipe.Source;
			curr.Url = recipe.Url;
			curr.Description = recipe.Description;
			return curr;
		}

		private Recipe FindRecipeFromName(string name)
		{
			return MainTab.Recipes.FirstOrDefault(curr => curr.Name.Equals(name));
		}

		private void OpenRecipe(object obj)
		{
			if (obj != null && obj is string name)
			{
				try
				{
					if (!string.IsNullOrEmpty(name))
					{
						Recipe rec = FindRecipeFromName(name);
						if (rec != null)
						{
							var tabViewModel = new TabRecipeContentViewModel(rec);
							Tabs.Add(tabViewModel);
							TabSelectedIndex = Tabs.Count - 1;
						}
					}
				}
				catch (Exception)
				{
					//log.Error("Error occurred!", ex);
				}
			}
		}

		private void AddMainTab()
		{
			Tabs.Add(MainTab);
		}

		private void CloseRecipe(object obj)
		{
			if (obj != null && obj is string name)
			{
				if (!string.IsNullOrEmpty(name))
				{
					Recipe rec = FindRecipeFromName(name);
					if (rec != null)
					{
						var tabViewModel = Tabs.FirstOrDefault(t => string.Equals(t.TabName, name, StringComparison.OrdinalIgnoreCase));
						if (tabViewModel != null)
						{
							Tabs.Remove(tabViewModel);
						}
					}
				}
			}
		}
	}
}
