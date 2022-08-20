using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SMPCTool
{
	// Token: 0x0200000E RID: 14
	public partial class SearchFindForm : Form
	{
		// Token: 0x0600008A RID: 138 RVA: 0x0000BD90 File Offset: 0x00009F90
		public SearchFindForm(MainForm mainForm)
		{
			this.mainForm = mainForm;
			this.InitializeComponent();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000BDAF File Offset: 0x00009FAF
		private void SearchFindForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			MainForm.searchFindForm = null;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000BDB8 File Offset: 0x00009FB8
		private void searchButton_Click(object sender, EventArgs e)
		{
			this.resultsListBox.Sorted = true;
			this.resultsListBox.Items.Clear();
			string text = this.searchTextbox.Text;
			string text2 = this.filtersTextBox.Text;
			text = text.Replace("/", "\\");
			text2 = text2.Replace("/", "\\");
			for (int i = 0; i < Globals.TOC.TOCMaps.Length; i++)
			{
				foreach (TOCMapEntry tocmapEntry in Globals.TOC.TOCMaps[i].TOCMapEntries)
				{
					bool flag = tocmapEntry.FileName.ToLower().Contains(text.ToLower());
					if (flag)
					{
						bool flag2 = !string.IsNullOrWhiteSpace(text2);
						if (flag2)
						{
							bool flag3 = !tocmapEntry.FileName.ToLower().Contains(text2.ToLower());
							if (flag3)
							{
								continue;
							}
						}
						this.resultsListBox.Items.Add(tocmapEntry.ArchiveName + ", " + tocmapEntry.FileName);
					}
				}
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000BF14 File Offset: 0x0000A114
		private void resultsListBox_DoubleClick(object sender, EventArgs e)
		{
			bool flag = this.resultsListBox.SelectedItem == null;
			if (!flag)
			{
				string text = this.resultsListBox.SelectedItem.ToString();
				text = text.Replace(" ", "");
				string[] array = text.Split(new char[]
				{
					','
				});
				string text2 = array[0];
				string text3 = array[1];
				for (int i = 0; i < Globals.TOC.TOCMaps.Length; i++)
				{
					foreach (TOCMapEntry tocmapEntry in Globals.TOC.TOCMaps[i].TOCMapEntries)
					{
						bool flag2 = tocmapEntry.ArchiveName.ToLower() == text2.ToLower();
						if (flag2)
						{
							bool flag3 = tocmapEntry.FileName.ToLower() == text3.ToLower();
							if (flag3)
							{
								this.mainForm.SearchFind(tocmapEntry);
								return;
							}
						}
					}
				}
			}
		}

		// Token: 0x04000072 RID: 114
		private MainForm mainForm;
	}
}
