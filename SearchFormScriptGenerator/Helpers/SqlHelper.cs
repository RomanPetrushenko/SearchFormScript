using Dapper;
#if NETSTANDARD2_0
using Microsoft.Data.SqlClient;
#elif NET451
using System.Data.SqlClient;
#endif
using SearchFormScriptGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SearchFormScriptGenerator.Helpers
{
	internal static class SqlHelper<T>
		where T : IInsertEntity, new()
	{
		public static IEnumerable<T> List(string sqlConnStr, int mlsRegionId, 
			string mlsFieldName = "MLSRegion_ID", string tableName = null)
		{
			if (string.IsNullOrEmpty(tableName))
			{
				T ent = new T();
				tableName = ent.GetTableName();
			}
			if (string.IsNullOrEmpty(tableName))
			{
				throw new ArgumentNullException("Table name is empty");
			}
			if (string.IsNullOrEmpty(mlsFieldName))
			{
				throw new ArgumentNullException("MLS Region field name is empty");
			}

			using (IDbConnection db = new SqlConnection(sqlConnStr))
			{
				return db.Query<T>($"SELECT * FROM dbo.[{tableName}] WHERE [{mlsFieldName}] = {mlsRegionId}");
			}
		}
	}
}
