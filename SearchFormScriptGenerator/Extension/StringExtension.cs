namespace SearchFormScriptGenerator
{
	internal static class StringExtension
	{
		internal static string StringToSql(this string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return "NULL";
			}
			return $"'{str}'";
		}
	}
}
