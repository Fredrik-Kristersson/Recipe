using System;
using System.Windows.Input;
using Microsoft.Win32;
using ViewModelLib;

namespace Recipe.ViewModel
{
	public class AddRecipeDialogViewModel : ViewModelBase
	{
		private readonly IDialogService dialogService;

		public AddRecipeDialogViewModel(IDialogService dialogService) : this(dialogService, new Recipe())
		{
		}

		public AddRecipeDialogViewModel(IDialogService dialogService, Recipe model)
		{
			Model = model;
			SelectImageCommand = new MyCommand(SelectImage);
			this.dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
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

		public ICommand SelectImageCommand { get; }

		public Recipe Model { get; }

		public string Name
		{
			get { return Model.Name; }
			set
			{
				if (Model.Name == value) return;
				Model.Name = value;
				OnPropertyChanged("Name");
			}
		}

		public string Description
		{
			get { return Model.Description; }
			set
			{
				if (Model.Description == value) return;
				Model.Description = value;
				OnPropertyChanged("Description");
			}
		}
		public string Image
		{
			get { return Model.Image; }
			set
			{
				if (Model.Image == value) return;
				Model.Image = value;
				OnPropertyChanged("Image");
			}
		}

		public string Source
		{
			get { return Model.Source; }
			set
			{
				if (Model.Source == value) return;
				Model.Source = value;
				OnPropertyChanged("Source");
			}
		}

		public string Grade
		{
			get { return Model.Grade; }
			set
			{
				if (Model.Grade == value) return;
				Model.Grade = value;
				OnPropertyChanged("Grade");
			}
		}
	}
}
