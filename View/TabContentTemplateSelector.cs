using Recipes.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Recipes.View
{
	public class TabContentTemplateSelector : DataTemplateSelector
	{
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			Window win = Application.Current.MainWindow;
			if (item is IMainTabViewModel mainTab)
			{
				return win.FindResource("MainTabTemplate") as DataTemplate;
			}
			else if (item is ITabRecipeContentViewModel recipeTab)
			{
				return win.FindResource("RecipeTabTemplate") as DataTemplate;
			}

			return null;
		}
	}
}