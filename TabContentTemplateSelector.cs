using System.Windows;
using System.Windows.Controls;
using Recipes.ViewModel;

namespace Recipes
{
	public class TabContentTemplateSelector : DataTemplateSelector
	{
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			Window win = Application.Current.MainWindow;
			if (item is MainTabViewModel mainTab)
			{
				return win.FindResource("MainTabTemplate") as DataTemplate;
			}
			else if (item is TabRecipeContentViewModel recipeTab)
			{
				return win.FindResource("RecipeTabTemplate") as DataTemplate;
			}

			return null;
		}
	}
}