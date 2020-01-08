using SearchFormScriptGenerator.Interfaces;

namespace SearchFormScriptGenerator.Models
{
	public class SearchFeature : IInsertEntity
	{
		public string SearchFormFeatureGroup_ID { get; set; }
		public string MLSTable { get; set; }
		public string MLSField { get; set; }
		public string Value { get; set; }
		public string Label { get; set; }
		public int Order { get; set; }

		public string GetCommentSql()
		{
			return string.Empty;
		}

		public string GetFieldsStrToSql()
		{
			return "[SearchFormFeatureGroup_ID], [MLSTable], [MLSField], [Value], [Label], [Order]";
		}

		public string GetTableName()
		{
			return "SearchFormFeatures";
		}

		public string GetValuesStrToSql()
		{
			return $"{SearchFormFeatureGroup_ID.StringToSql()}, {MLSTable.StringToSql()}, {MLSField.StringToSql()}, {Value.StringToSql()}, {Label.StringToSql()}, {Order}";
		}
	}
}
