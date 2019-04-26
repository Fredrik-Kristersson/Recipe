using System;
using ViewModelLib;

namespace Recipes.Model
{
	public class Recipe : ViewModelBase
	{
		public bool IsSelected
		{
			get => Get<bool>();
			set
			{
				Set(value);
			}
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

		public double Grade
		{
			get => Get<double>();
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
