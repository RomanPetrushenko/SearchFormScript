using SearchFormScriptGenerator.Interfaces;

namespace SearchFormScriptGenerator.Models
{
	public class LocationTypes : IInsertEntity
	{
		public int MLSRegion_ID { get; set; }
		public int LocationType { get; set; }
		public string Label { get; set; }
		public int Order { get; set; }

		public string GetFieldsStrToSql()
		{
			return "[MLSRegion_ID], [LocationType], [Label], [Order]";
		}

		public string GetValuesStrToSql()
		{
			return $" {MLSRegion_ID}, {LocationType}, {Label.StringToSql()}, {Order}";
		}

		public string GetTableName()
		{
			return "SearchFormLocationTypes";
		}

		public string GetCommentSql()
		{
			return string.Empty;
		}
	}
}
