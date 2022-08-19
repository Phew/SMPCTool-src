using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace SMPCTool
{
	// Token: 0x02000006 RID: 6
	public partial class CreateModForm : Form
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002B58 File Offset: 0x00000D58
		public CreateModForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002B70 File Offset: 0x00000D70
		private void CreateModForm_Load(object sender, EventArgs e)
		{
			Globals.ModCreateDirectory = Globals.TemporaryDirectory + "ModCreate\\";
			bool flag = Directory.Exists(Globals.ModCreateDirectory);
			if (flag)
			{
				Directory.Delete(Globals.ModCreateDirectory, true);
			}
			Directory.CreateDirectory(Globals.ModCreateDirectory);
			for (int i = 0; i < Globals.ReplacedTOCMapEntries.Count; i++)
			{
				this.listBox1.Items.Add(Globals.ReplacedTOCMapEntries[i].FileName);
			}
			this.titleTextBox.Text = Globals.ModTitle;
			this.authorTextBox.Text = Globals.ModAuthor;
			this.descriptionTextBox.Text = Globals.ModDescription.Replace("~n~", "\r\n");
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002C38 File Offset: 0x00000E38
		private void createButton_Click(object sender, EventArgs e)
		{
			bool flag = Globals.ReplacedTOCMapEntries.Count <= 0;
			if (flag)
			{
				MessageBox.Show(this, "Cannot create a mod with no modified files!", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				SMPCMod smpcmod = new SMPCMod();
				smpcmod.Title = this.titleTextBox.Text;
				smpcmod.Author = this.authorTextBox.Text;
				smpcmod.Description = this.descriptionTextBox.Text;
				smpcmod.Description = smpcmod.Description.Replace("\r\n", "~n~");
				string text = "";
				text = text + "Title=" + smpcmod.Title + "\n";
				text = text + "Author=" + smpcmod.Author + "\n";
				text = text + "Description=" + smpcmod.Description + "\n";
				File.WriteAllText(Globals.ModCreateDirectory + "SMPCMod.info", text);
				Directory.CreateDirectory(Globals.ModCreateDirectory + "ModFiles\\");
				for (int i = 0; i < Globals.ReplacedTOCMapEntries.Count; i++)
				{
					string sourceFileName = Globals.ReplacedTOCMapEntriesReplaceFileNames[i];
					TOCMapEntry tocmapEntry = Globals.ReplacedTOCMapEntries[i];
					File.Copy(sourceFileName, string.Concat(new string[]
					{
						Globals.ModCreateDirectory,
						"ModFiles\\",
						tocmapEntry.ArchiveIndex.ToString(),
						"_",
						tocmapEntry.FileAssetID.ToString("X2")
					}));
				}
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Title = "Save SMPCMod";
				saveFileDialog.Filter = "SMPCMod (*.smpcmod)|*.smpcmod";
				saveFileDialog.RestoreDirectory = true;
				saveFileDialog.FileName = smpcmod.Title + ".smpcmod";
				bool flag2 = saveFileDialog.ShowDialog() != DialogResult.OK;
				if (!flag2)
				{
					bool flag3 = File.Exists(saveFileDialog.FileName);
					if (flag3)
					{
						File.Delete(saveFileDialog.FileName);
					}
					ZipFile.CreateFromDirectory(Globals.ModCreateDirectory, saveFileDialog.FileName, CompressionLevel.Fastest, false);
					MessageBox.Show(this, "Successfully created Spider-Man Mod", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					base.Close();
				}
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002E6C File Offset: 0x0000106C
		private void pictureBox1_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Thumbnail PNG (*.png)|*.png";
			bool flag = openFileDialog.ShowDialog() != DialogResult.OK;
			if (!flag)
			{
				bool flag2 = File.Exists(Globals.ModCreateDirectory + "Thumbnail.png");
				if (flag2)
				{
					File.Delete(Globals.ModCreateDirectory + "Thumbnail.png");
				}
				File.Copy(openFileDialog.FileName, Globals.ModCreateDirectory + "Thumbnail.png");
				this.thumbnailPictureBox.Image = Image.FromFile(Globals.ModCreateDirectory + "Thumbnail.png");
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002F08 File Offset: 0x00001108
		private void CreateModForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool flag = this.thumbnailPictureBox.Image != null;
			if (flag)
			{
				this.thumbnailPictureBox.Image.Dispose();
			}
			bool flag2 = Directory.Exists(Globals.ModCreateDirectory);
			if (flag2)
			{
				Directory.Delete(Globals.ModCreateDirectory, true);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002F57 File Offset: 0x00001157
		private void clearModifiedFilesBtn_Click(object sender, EventArgs e)
		{
			Globals.ReplacedTOCMapEntries.Clear();
			Globals.ReplacedTOCMapEntriesReplaceFileNames.Clear();
			this.listBox1.Items.Clear();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002F84 File Offset: 0x00001184
		private void deleteModifiedFile_Click(object sender, EventArgs e)
		{
			int selectedIndex = this.listBox1.SelectedIndex;
			bool flag = selectedIndex == -1;
			if (!flag)
			{
				this.listBox1.Items.RemoveAt(selectedIndex);
				Globals.ReplacedTOCMapEntries.RemoveAt(selectedIndex);
				Globals.ReplacedTOCMapEntriesReplaceFileNames.RemoveAt(selectedIndex);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002FD2 File Offset: 0x000011D2
		private void titleTextBox_TextChanged(object sender, EventArgs e)
		{
			Globals.ModTitle = this.titleTextBox.Text;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002FE5 File Offset: 0x000011E5
		private void authorTextBox_TextChanged(object sender, EventArgs e)
		{
			Globals.ModAuthor = this.authorTextBox.Text;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002FF8 File Offset: 0x000011F8
		private void descriptionTextBox_TextChanged(object sender, EventArgs e)
		{
			Globals.ModDescription = this.descriptionTextBox.Text.Replace("\r\n", "~n~");
		}
	}
}
