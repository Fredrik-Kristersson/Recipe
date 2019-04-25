using Castle.ActiveRecord;
using log4net;
using Recipes.Model;
using Recipes.Model.Database;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ViewModelLib;
using ViewModelLib.Event;

namespace Recipes.Service
{
	[Export(typeof(IRecipeService))]
	public class RecipeService : IRecipeService
	{
		private readonly IEventMessenger eventMessenger;
		ILog log;

		[ImportingConstructor]
		public RecipeService(
			ILogger logger,
			IEventMessenger eventMessenger)
		{
			log = logger.GetLogger(GetType());
			this.eventMessenger = eventMessenger;
		}

		public void AddRecipe(Recipe recipe)
		{
			RecipeVO newRecipe = ToRecipeVO(recipe);
			newRecipe.Create();
			eventMessenger.Publish(new RecipesUpdatedEvent());
		}

		public void EditRecipe(Recipe recipe)
		{
			RecipeVO edited = ActiveRecordBase<RecipeVO>.Find(recipe.Id);
			edited.Description = recipe.Description;
			edited.Grade = recipe.Grade;
			edited.Image = recipe.Image;
			edited.Name = recipe.Name;
			edited.Source = recipe.Source;
			edited.Url = recipe.Url;
			edited.SaveCopy();

			eventMessenger.Publish(new RecipesUpdatedEvent());
		}

		public IEnumerable<Recipe> GetAllRecipes()
		{
			return RecipeVO.FindAllRecipes().Select(ToRecipe);
		}

		public void RemoveRecipe(int id)
		{
			RecipeVO recipeVo = ActiveRecordBase<RecipeVO>.Find(id);
			recipeVo.Delete();
			eventMessenger.Publish(new RecipesUpdatedEvent());
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
	}
}
