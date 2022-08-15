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
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002BF8 File Offset: 0x00000DF8
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

		// Token: 0x0600001C RID: 28 RVA: 0x00002DF8 File Offset: 0x00000FF8
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

		// Token: 0x0600001D RID: 29 RVA: 0x00002E94 File Offset: 0x00001094
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
	}
}
