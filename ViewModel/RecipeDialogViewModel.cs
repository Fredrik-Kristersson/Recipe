using System;
using System.Windows.Input;
using Microsoft.Win32;
using ViewModelLib;

namespace Recipes.ViewModel
{
	public class RecipeDialogViewModel : ViewModelBase, IRecipeDialogViewModel
	{
		private readonly IDialogService dialogService;

		public RecipeDialogViewModel(IDialogService dialogService)
		{
			SelectImageCommand = new MyCommand(SelectImage);
			this.dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
		}

		public ICommand SelectImageCommand { get; }

		public Recipe Recipe { get; set; }

		public string Name
		{
			get { return Recipe.Name; }
			set
			{
				if (Recipe.Name == value) return;
				Recipe.Name = value;
				OnPropertyChanged("Name");
			}
		}

		public string Description
		{
			get { return Recipe.Description; }
			set
			{
				if (Recipe.Description == value) return;
				Recipe.Description = value;
				OnPropertyChanged("Description");
			}
		}
		public string Image
		{
			get { return Recipe.Image; }
			set
			{
				if (Recipe.Image == value) return;
				Recipe.Image = value;
				OnPropertyChanged("Image");
			}
		}

		public string Source
		{
			get { return Recipe.Source; }
			set
			{
				if (Recipe.Source == value) return;
				Recipe.Source = value;
				OnPropertyChanged("Source");
			}
		}

		public string Grade
		{
			get { return Recipe.Grade; }
			set
			{
				if (Recipe.Grade == value) return;
				Recipe.Grade = value;
				OnPropertyChanged("Grade");
			}
		}

		private void SelectImage(object obj)
		{
			dialogService.OpenFileDialog("");
			var dialog = new OpenFileDialog { Multiselect = false };
			if (!string.IsNullOrEmpty(Image))
			{
				dialog.FileName = Image;
			}

			bool? result = null;
			try
			{
				result = dialog.ShowDialog(null);
			}
			catch (InvalidOperationException)
			{
				dialog.FileName = string.Empty;
				result = dialog.ShowDialog(null);
			}

			if (result == true)
			{
				Image = dialog.FileName;
			}
		}
	}
}
