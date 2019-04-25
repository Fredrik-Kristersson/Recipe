﻿using System.ComponentModel.Composition;

namespace Recipes.ViewModel
{
	[Export(typeof(ITabRecipeContentViewModel))]
	public class TabRecipeContentViewModel : TabViewModelBase, ITabRecipeContentViewModel
	{
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
	}
}
