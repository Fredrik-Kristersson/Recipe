using System;
using System.Collections.Generic;
using System.Linq;
using Castle.ActiveRecord;

namespace Recipes.Model.Database
{
	[ActiveRecord, Serializable]
	public class RecipeVO : ValueObjectBase
	{
		[Property]

		public string Name { get; set; }
		[Property]

		public string Grade { get; set; }
		[Property]

		public string Source { get; set; }
		[Property]
		public string Url { get; set; }

		[Property]
		public string Description { get; set; }

		[Property]
		public string Image { get; set; }

		public static RecipeVO FindById(int id)
		{
			return ActiveRecordBase<RecipeVO>.Find(id);
			//return all.Where(rec => rec.Id == id).ToArray()[0];
		}

		public static IEnumerable<RecipeVO> FindAllRecipes()
		{
			var result = FindAll(typeof(RecipeVO));
			return result.Cast<RecipeVO>();
		}
	}
}
