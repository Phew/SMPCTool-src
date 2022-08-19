using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SMPCTool
{
	// Token: 0x0200000E RID: 14
	public partial class SearchFindForm : Form
	{
		// Token: 0x06000082 RID: 130 RVA: 0x0000B7DB File Offset: 0x000099DB
		public SearchFindForm(MainForm mainForm)
		{
			this.mainForm = mainForm;
			this.InitializeComponent();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000B7FA File Offset: 0x000099FA
		private void SearchFindForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			MainForm.searchFindForm = null;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000B804 File Offset: 0x00009A04
		private void searchButton_Click(object sender, EventArgs e)
		{
			this.resultsListBox.Sorted = true;
			this.resultsListBox.Items.Clear();
			string text = this.searchTextbox.Text;
			text = text.Replace("/", "\\");
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

		// Token: 0x06000085 RID: 133 RVA: 0x0000B908 File Offset: 0x00009B08
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

		// Token: 0x04000071 RID: 113
		private MainForm mainForm;
	}
}
