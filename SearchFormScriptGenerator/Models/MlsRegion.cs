namespace SearchFormScriptGenerator.Models
{
	public class MlsRegion
	{
		public int MlsRegionId { get; private set; }
		public string NameRegion { get; private set; }

		public MlsRegion(int mlsRegionId, string name = null)
		{
			NameRegion = name;
			MlsRegionId = mlsRegionId;
		}

		internal string GetHeaderText()
		{
			string name = string.IsNullOrEmpty(NameRegion) ? "Unknown Region" : NameRegion;
			return $"Search Form records for {name} ({MlsRegionId}) region";
		}
	}
}
