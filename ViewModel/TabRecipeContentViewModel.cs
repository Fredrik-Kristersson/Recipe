using Recipes.Service;
using System.ComponentModel.Composition;
using ViewModelLib.Event;

namespace Recipes.ViewModel
{
	[Export(typeof(ITabRecipeContentViewModel))]
	public class TabRecipeContentViewModel : TabViewModelBase, ITabRecipeContentViewModel
	{
		[ImportingConstructor]
		public TabRecipeContentViewModel(IEventMessenger eventMessenger)
		{
			eventMessenger.Subscribe<RecipeSelectedEvent>(OnSelectedRecipeChanged);
		}

		public override string TabName => Name;

		public override bool IsCloseable => true;

		public string Name
		{
			get => Get<string>();
			set => Set(value);
		}

		public string Description
		{
			get => Get<string>();
			set => Set(value);
		}

		public string Image
		{
			get => Get<string>();
			set => Set(value);
		}

		private void OnSelectedRecipeChanged(RecipeSelectedEvent obj)
		{
			Name = obj.SelectedRecipe.Name;
			Description = obj.SelectedRecipe.Description;
			Image = obj.SelectedRecipe.Image;
		}
	}
}
