using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Recipe.ViewModel;
using ViewModelLib;

namespace Recipe
{
	public partial class Window1
	{

		private const string EmptyFilterValue = "...";

		private string filterText = EmptyFilterValue;

		GridViewColumnHeader lastHeaderClicked = null;

		ListSortDirection lastDirection = ListSortDirection.Ascending;

		private readonly MainWindowViewModel viewModel;

		private IDialogService diagService;

		public Window1()
		{
			InitializeComponent();
			// Insert code required on object creation below this point.            
			diagService = new DefaultDialogService(this);
			viewModel = new MainWindowViewModel(diagService);
			DataContext = viewModel;
		}

		private void filterBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			//filterBox.SelectAll();
		}

		private void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
		{
			var headerClicked = e.OriginalSource as GridViewColumnHeader;

			if (headerClicked != null)
			{
				ListSortDirection direction;
				if (headerClicked != lastHeaderClicked)
				{
					direction = ListSortDirection.Ascending;
				}
				else
				{
					direction = lastDirection ==
						ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
				}
				string header = headerClicked.Column.Header as string;
				var listView = TryFindResource("RecipesView") as CollectionViewSource;
				if (listView != null)
				{
					Sort(header, direction, listView);
					lastHeaderClicked = headerClicked;
					lastDirection = direction;
				}
			}
		}

		private static void Sort(string sortBy, ListSortDirection direction, CollectionViewSource sortIn)
		{
			sortIn.SortDescriptions.Clear();
			sortIn.SortDescriptions.Add(new SortDescription(sortBy, direction));
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//tableView = CollectionViewSource.GetDefaultView(recipeTable.ItemsSource);
			//tableView.Filter = FilterCallback;
		}

		private bool FilterCallback(object item)
		{
			var recipe = item as Recipe;
			if (recipe != null)
			{
				if (recipe.Name.ToLower().Contains(filterText.ToLower()) ||
						recipe.Source.ToLower().Contains(filterText.ToLower()) ||
						/*recipe.Description.ToLower().Contains(filterText.ToLower()) || ??? Vill ha ???*/
						filterText.Equals(EmptyFilterValue))
				{
					return true;
				}
			}
			return false;

		}

		private void TextBox_KeyUp(object sender, KeyEventArgs e)
		{
			//filterText = filterBox.Text;
			//tableView.Refresh();
		}

		private void recipeTable_KeyDown(object sender, KeyEventArgs e)
		{
			//var selected = recipeTable.SelectedItem as Recipe;
			//if (e.Key == Key.Delete && selected != null)
			//{
			//	RemoveRecipe(selected);
			//}
		}

		private void path_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			//var pathSender = sender as System.Windows.Shapes.Path;
			//if (pathSender != null)
			//{
			//	DependencyObject parent = VisualTreeHelper.GetParent(pathSender);
			//	int i = 0;
			//	while (!(parent is TabItem) && i < 100)
			//	{
			//		parent = VisualTreeHelper.GetParent(parent);
			//		i++;
			//	}
			//	var tabParent = parent as TabItem;
			//	if (tabParent != null)
			//	{
			//		tabs.Items.Remove(tabParent);

			//	}
			//}
		}

		private void MenuItem_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void openMenuItem_Click(object sender, RoutedEventArgs e)
		{
		}
	}
}