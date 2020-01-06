using System;
using System.Collections.Generic;
using System.Text;

namespace SearchFormScriptGenerator.Interfaces
{
	public interface IInsertEntity
	{
		string GetTableName();
		string GetValuesStrToSql();
		string GetFieldsStrToSql();

		string GetCommentSql();
	}
}
