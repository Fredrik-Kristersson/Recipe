using System;
using Castle.ActiveRecord;

namespace Recipes.Model.Database
{
	[ActiveRecord, Serializable]
	public abstract class ValueObjectBase : ActiveRecordBase
	{
		[PrimaryKey("id")]
		public int Id { get; set; }
	}
}
