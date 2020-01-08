using SearchFormScriptGenerator.Interfaces;
using System.Linq;

namespace SearchFormScriptGenerator.Models
{
	public class SearchFeatureGroup : IInsertEntity
	{
		public int ID { get; set; }
		public int MLSRegion_ID { get; set; }
		public int SelectionType { get; set; }
		public string Label { get; set; }
		public int Order { get; set; }

		public string GetCommentSql()
		{
			return string.Empty;
		}

		public string GetFieldsStrToSql()
		{
			return "[MLSRegion_ID], [SelectionType], [Label], [Order]";
		}

		public string GetTableName()
		{
			return "SearchFormFeatureGroups";
		}

		public string GetValuesStrToSql()
		{
			return $" {MLSRegion_ID}, {SelectionType}, {Label.StringToSql()}, {Order}";
		}

		public string GetGroupNameForVariable()
		{
			if (string.IsNullOrEmpty(Label))
			{
				return string.Empty;
			}

			return Label.Split(' ').First().Replace("/", "").Replace("'", "").Replace("\\", "");
		}
	}
}
