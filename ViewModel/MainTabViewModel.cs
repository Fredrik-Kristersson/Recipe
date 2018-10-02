using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Recipes.Model.Database;

namespace Recipes.ViewModel
{
	public class MainTabViewModel : TabViewModelBase, IMainTabViewModel
	{
		public MainTabViewModel()
		{
			Recipes = new ObservableCollection<Recipe>();


			RefreshRecipesFromModel();
		}

		public Recipe SelectedRecipe
		{
			get => Get<Recipe>();
			set => Set(value);
		}

		public ObservableCollection<Recipe> Recipes { get; }

		public override string TabName => "Recipes";

		public override bool IsCloseable => false;

		internal void RefreshRecipesFromModel()
		{
			try
			{
				Recipes.Clear();
				IList<RecipeVO> recipes = RecipeVO.FindAllRecipes().ToList();
				foreach (RecipeVO recipe in recipes)
				{
					Recipe curr = ToRecipe(recipe);
					Recipes.Add(curr);
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

		private Recipe ToRecipe(RecipeVO recipe)
		{
			Recipe curr = new Recipe();
			curr.Id = recipe.Id;
			curr.Name = recipe.Name;
			curr.Grade = recipe.Grade;
			curr.Image = recipe.Image;
			curr.Source = recipe.Source;
			curr.Url = recipe.Url;
			curr.Description = recipe.Description;
			return curr;
		}
	}
}
