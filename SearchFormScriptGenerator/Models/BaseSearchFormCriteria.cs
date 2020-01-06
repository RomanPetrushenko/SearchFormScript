using SearchFormScriptGenerator.Interfaces;

namespace SearchFormScriptGenerator.Models
{
	public abstract class BaseSearchFormCriteria : IInsertEntity
	{
		public int MLSRegion_ID { get; set; }
		public int CriteriaType { get; set; }
		public string Label { get; set; }
		public int Order { get; set; }

		public virtual string GetFieldsStrToSql()
		{
			return "[MLSRegion_ID], [CriteriaType], [Label], [Order]";
		}

		public virtual string GetValuesStrToSql()
		{
			return $" {MLSRegion_ID}, {CriteriaType}, {Label.StringToSql()}, {Order}";
		}

		public virtual string GetCommentSql()
		{
			return string.Empty;
		}

		public abstract string GetTableName();
	}
}
