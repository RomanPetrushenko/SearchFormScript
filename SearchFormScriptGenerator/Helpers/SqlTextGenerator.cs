using SearchFormScriptGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchFormScriptGenerator.Helpers
{
	internal static class SqlTextGenerator
	{
		internal static string CommentHeaderGenerate(string text)
		{
			return $"------------------ {text} ------------------";
		}
		internal static string CommentGenerate(string text)
		{
			return $"-- {text}";
		}

		internal static string InsertQueryGenerate(IEnumerable<IInsertEntity> entities)
		{
			if(entities == null || entities.Count() == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();

			var ent = entities.First();
			stringBuilder.AppendLine($"INSERT INTO dbo.[{ent.GetTableName()}] ({ent.GetFieldsStrToSql()})");
			stringBuilder.AppendLine("VALUES");
			foreach (var entity in entities)
			{
				stringBuilder.AppendLine($"({entity.GetValuesStrToSql()})   {CommentGenerate(entity.GetCommentSql())}");
			}

			return stringBuilder.ToString();
		}

		internal static string DeclareVariableInt(string variableName)
		{
			return $"DECLARE @{variableName} int";
		}

		internal static string SetVariableInt(string variableName, string value = "@@IDENTITY")
		{
			return $"SET @{variableName} = {value}";
		}
	}
}
