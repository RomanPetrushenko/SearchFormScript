using Microsoft.Win32;
using SearchFormScriptGenerator;
using SearchFormScriptGenerator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfCreateSearchFormScript
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private ScriptGenerator GetGenerator()
		{
			if (string.IsNullOrEmpty(txtSrcConnString.Text))
			{
				MessageBox.Show("Souce Connection String is empty");
				return null;
			}
			int srcRegionId = 0;
			if (string.IsNullOrEmpty(txtSrcRegionId.Text) || !int.TryParse(txtSrcRegionId.Text, out srcRegionId))
			{
				MessageBox.Show("Souce Region Id is empty");
				return null;
			}
			int trgRegionId = 0;
			if (string.IsNullOrEmpty(txtTargetRegionId.Text) || !int.TryParse(txtTargetRegionId.Text, out trgRegionId))
			{
				MessageBox.Show("Target Region Id is empty");
				return null;
			}

			MlsRegion targetMlsRegion = new MlsRegion(trgRegionId, txtTargetRegionName.Text);
			MlsRegion sourceMlsRegion = new MlsRegion(srcRegionId, txtSrcRegionName.Text);

			return new ScriptGenerator(targetMlsRegion, sourceMlsRegion, 
				txtSrcConnString.Text, txtTargetConnString.Text);
		}

		private void BtSave_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				btSave.IsEnabled = txtSrcConnString.IsEnabled = txtSrcRegionId.IsEnabled = txtSrcRegionName.IsEnabled = 
					txtTargetConnString.IsEnabled = txtTargetRegionId.IsEnabled = txtTargetRegionName.IsEnabled = false;
				using (var scriptGen = GetGenerator())
				{
					if (scriptGen == null)
					{
						return;
					}
					var saveFileDialog = new SaveFileDialog();
					saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
					saveFileDialog.Filter = "SQL file (*.sql)|*.sql";
					var res = saveFileDialog.ShowDialog();
					if (res.HasValue && res.Value)
					{
						scriptGen.Generate();
						File.WriteAllText(saveFileDialog.FileName, scriptGen.ToString());
					}
				}
			}
			catch
			{

			}
			finally
			{
				btSave.IsEnabled = txtSrcConnString.IsEnabled = txtSrcRegionId.IsEnabled = txtSrcRegionName.IsEnabled =
					txtTargetConnString.IsEnabled = txtTargetRegionId.IsEnabled = txtTargetRegionName.IsEnabled = true;
			}
		}
	}
}
