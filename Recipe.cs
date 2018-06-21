using System;
using ViewModelLib;

namespace Recipe
{
	public class Recipe : ViewModelBase
	{
		public Recipe()
		{

		}

		public int Id
		{
			get => Get<int>();
			set => Set(value);
		}

		public string Name
		{
			get => Get<string>();
			set => Set(value);
		}

		public string Grade
		{
			get => Get<string>();
			set => Set(value);
		}

		public string Source
		{
			get => Get<string>();
			set => Set(value);
		}

		public string Url
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

		public Object Clone()
		{
			var clone = new Recipe();
			clone.Id = Id;
			clone.Name = Name;
			clone.Grade = Grade;
			clone.Source = Source;
			clone.Url = Url;
			clone.Description = Description;
			clone.Image = Image;
			return clone;
		}
	}
}
