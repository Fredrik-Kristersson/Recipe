using System.Windows;
using System.Windows.Controls;
using Recipe.ViewModel;

namespace Recipe
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