using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using ViewModelLib;

namespace Recipes.ViewModel
{
	[Export(typeof(IRecipeDialogViewModel))]
	[PartCreationPolicy(CreationPolicy.NonShared)]
	public class RecipeDialogViewModel : DialogViewModelBase, IRecipeDialogViewModel
	{
		[ImportingConstructor]
		public RecipeDialogViewModel(IDialogService dialogService) : base(dialogService)
		{
			SelectImageCommand = new MyCommand(SelectImage);

			AddValidator(nameof(Name), () => string.IsNullOrEmpty(Name), "Name me!");
		}

		public ICommand SelectImageCommand { get; }

		public string Name
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		public string Description
		{
			get { return Get<string>(); }
			set { Set(value); }
		}
		public string Image
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		public string Source
		{
			get { return Get<string>(); }
			set { Set(value); }
		}

		public double Grade
		{
			get { return Get<double>(); }
			set
			{
				var roundedValue = Math.Round(value, 1);
				Set(roundedValue);
			}
		}

		private void SelectImage(object obj)
		{
			if (DialogService.OpenFileDialog("", out string filePath, Image))
			{
				Image = filePath;
			}
		}
	}
}
