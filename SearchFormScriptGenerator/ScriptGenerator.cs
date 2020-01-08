using SearchFormScriptGenerator.Helpers;
using SearchFormScriptGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchFormScriptGenerator
{
	public class ScriptGenerator : IDisposable
	{
		private StringBuilder builder;
		public MlsRegion TargetMlsRegion { get; private set; }
		public MlsRegion SourceMlsRegion { get; private set; }

		private string _sourceSqlConStr;
		private string _targetSqlConStr;

		public ScriptGenerator(MlsRegion targetMlsRegion, MlsRegion sourceMlsRegion, 
			string sourceSqlString, string targetSqlString = null)
		{
			builder = new StringBuilder();
			TargetMlsRegion = targetMlsRegion;
			SourceMlsRegion = sourceMlsRegion;
			_sourceSqlConStr = sourceSqlString;
			_targetSqlConStr = targetSqlString;
		}

		public void Generate(bool withFeatures = false, bool withCheckFeatures = true)
		{
			builder.AppendLine(SqlTextGenerator.CommentHeaderGenerate(TargetMlsRegion.GetHeaderText()));
			builder.AppendLine();

			builder.AppendLine(GetLocationTypes());
			builder.AppendLine();

			builder.AppendLine(GetPrimaryCriteriaTypes());
			builder.AppendLine();

			builder.AppendLine(GetSecondaryCriteriaTypes());
			builder.AppendLine();

			if (!withFeatures)
			{
				return;
			}

			builder.AppendLine(GetSearchGroups(withCheckFeatures));
		}

		public override string ToString()
		{
			return builder.ToString();
		}

		public void Clear()
		{
			builder.Clear();
		}

		public void Dispose()
		{
			builder.Clear();
			TargetMlsRegion = null;
			SourceMlsRegion = null;
			_sourceSqlConStr = null;
			_targetSqlConStr = null;
		}

		#region

		private string GetLocationTypes()
		{
			var entities = SqlHelper<LocationTypes>.List(_sourceSqlConStr, SourceMlsRegion.MlsRegionId);
			if (entities == null || entities.Count() == 0)
			{
				return string.Empty;
			}

			foreach (var entity in entities)
			{
				entity.MLSRegion_ID = TargetMlsRegion.MlsRegionId;
			}

			return SqlTextGenerator.InsertQueryGenerate(entities);
		}

		private string GetPrimaryCriteriaTypes()
		{
			var entities = SqlHelper<PrimaryCriteria>.List(_sourceSqlConStr, SourceMlsRegion.MlsRegionId);
			if (entities == null || entities.Count() == 0)
			{
				return string.Empty;
			}

			foreach (var entity in entities)
			{
				entity.MLSRegion_ID = TargetMlsRegion.MlsRegionId;
			}

			return SqlTextGenerator.InsertQueryGenerate(entities);
		}

		private string GetSecondaryCriteriaTypes()
		{
			var entities = SqlHelper<SecondaryCriteria>.List(_sourceSqlConStr, SourceMlsRegion.MlsRegionId);
			if (entities == null || entities.Count() == 0)
			{
				return string.Empty;
			}

			foreach (var entity in entities)
			{
				entity.MLSRegion_ID = TargetMlsRegion.MlsRegionId;
			}

			return SqlTextGenerator.InsertQueryGenerate(entities);
		}

		private string GetSearchGroups(bool withCheckFeatures)
		{
			if (withCheckFeatures && string.IsNullOrEmpty(_targetSqlConStr))
			{
				throw new ArgumentNullException("For mode \"Check Features\" need set Target Connection String");
			}
			StringBuilder builder = new StringBuilder();
			var entities = SqlHelper<SearchFeatureGroup>.List(_sourceSqlConStr, SourceMlsRegion.MlsRegionId);
			var searchFeatures = SqlHelper<SearchFeature>.List(_sourceSqlConStr, SourceMlsRegion.MlsRegionId);
			if (entities == null || entities.Count() == 0)
			{
				return string.Empty;
			}

			var exEnt = entities.First();
			int order = 1;
			foreach (var entity in entities)
			{
				entity.MLSRegion_ID = TargetMlsRegion.MlsRegionId;
				entity.Order = order;
				order++;
			}

			var nameVariable = $"@{exEnt.GetGroupNameForVariable()}ID";
			builder.AppendLine(SqlTextGenerator.DeclareVariableInt(nameVariable));
			builder.AppendLine();
			builder.AppendLine(SqlTextGenerator.SetVariableInt(nameVariable));
			builder.AppendLine();
			foreach(var entity in entities)
			{
				builder.AppendLine(SqlTextGenerator.InsertQueryGenerate(new SearchFeatureGroup[] { entity }));
				var srchFeatures = searchFeatures.Where(x => x.SearchFormFeatureGroup_ID == entity.ID + "");
				builder.AppendLine();
				builder.AppendLine(SqlTextGenerator.InsertQueryGenerate(srchFeatures));
				builder.AppendLine();
			}
			return builder.ToString();
		}

		#endregion
	}
}
