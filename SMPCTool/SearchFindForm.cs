using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SMPCTool
{
	// Token: 0x0200000B RID: 11
	public partial class SearchFindForm : Form
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00007F37 File Offset: 0x00006137
		public SearchFindForm(MainForm mainForm)
		{
			this.mainForm = mainForm;
			this.InitializeComponent();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00007F56 File Offset: 0x00006156
		private void SearchFindForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			MainForm.searchFindForm = null;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00007F60 File Offset: 0x00006160
		private void searchButton_Click(object sender, EventArgs e)
		{
			this.resultsListBox.Sorted = true;
			this.resultsListBox.Items.Clear();
			string text = this.searchTextbox.Text;
			for (int i = 0; i < Globals.TOC.TOCMaps.Length; i++)
			{
				foreach (TOCMapEntry tocmapEntry in Globals.TOC.TOCMaps[i].TOCMapEntries)
				{
					bool flag = tocmapEntry.FileName.ToLower().Contains(text.ToLower());
					if (flag)
					{
						this.resultsListBox.Items.Add(tocmapEntry.ArchiveName + ", " + tocmapEntry.FileName);
					}
				}
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00008050 File Offset: 0x00006250
		private void resultsListBox_DoubleClick(object sender, EventArgs e)
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
					bool flag = tocmapEntry.ArchiveName.ToLower() == text2.ToLower();
					if (flag)
					{
						bool flag2 = tocmapEntry.FileName.ToLower() == text3.ToLower();
						if (flag2)
						{
							this.mainForm.SearchFind(tocmapEntry);
							return;
						}
					}
				}
			}
		}

		// Token: 0x04000051 RID: 81
		private MainForm mainForm;
	}
}
