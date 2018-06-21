namespace Recipe.ViewModel
{
	public class TabRecipeContentViewModel : TabViewModelBase
	{
		public TabRecipeContentViewModel(Recipe model)
		{
			Name = model.Name;
			Description = model.Description;
			Image = model.Image;
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
	}
}
