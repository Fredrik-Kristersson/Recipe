using Recipes.Model;
using Recipes.Service;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using ViewModelLib.Event;

namespace Recipes.ViewModel
{
	[Export(typeof(IMainTabViewModel))]
	public class MainTabViewModel : TabViewModelBase, IMainTabViewModel
	{
		private readonly IRecipeService recipeService;

		[ImportingConstructor]
		public MainTabViewModel(
			IEventMessenger eventMessenger,
			IRecipeService recipeService) : base()
		{
			Recipes = new ObservableCollection<Recipe>();

			eventMessenger.Subscribe<RecipesUpdatedEvent>(OnRecipesUpdated);
			this.recipeService = recipeService;

			UpdateRecipes();
		}

		public Recipe SelectedRecipe
		{
			get => Get<Recipe>();
			set => Set(value);
		}

		public ObservableCollection<Recipe> Recipes { get; }

		public override string TabName => "Recipes";

		public override bool IsCloseable => false;

		private void OnRecipesUpdated(RecipesUpdatedEvent obj)
		{
			UpdateRecipes();
		}

		private void UpdateRecipes()
		{
			Recipes.Clear();

			foreach (var recipe in recipeService.GetAllRecipes())
			{
				Recipes.Add(recipe);
			}
		}
	}
}
