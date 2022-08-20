using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows.Forms;

namespace SMPCTool
{
	// Token: 0x0200000C RID: 12
	public partial class ModManagerForm : Form
	{
		// Token: 0x06000073 RID: 115 RVA: 0x0000A2D3 File Offset: 0x000084D3
		public ModManagerForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000A2EC File Offset: 0x000084EC
		private void LoadModManager()
		{
			Globals.ModManagerDirectory = "ModManager\\";
			bool flag = !Directory.Exists(Globals.ModManagerDirectory);
			if (flag)
			{
				Directory.CreateDirectory(Globals.ModManagerDirectory);
			}
			bool flag2 = !Directory.Exists(Globals.ModManagerDirectory + "SMPCMods\\");
			if (flag2)
			{
				Directory.CreateDirectory(Globals.ModManagerDirectory + "SMPCMods\\");
			}
			bool flag3 = !Directory.Exists(Globals.AssetArchivesDirectory + "mods\\");
			if (flag3)
			{
				Directory.CreateDirectory(Globals.AssetArchivesDirectory + "mods\\");
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000A388 File Offset: 0x00008588
		private void LoadMods()
		{
			string[] files = Directory.GetFiles(Globals.ModManagerDirectory + "SMPCMods\\");
			foreach (string text in files)
			{
				SMPCMod smpcmod = this.ExtractSMPCMod(text, Globals.TemporaryDirectory + Path.GetFileNameWithoutExtension(text) + "\\");
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000A3E0 File Offset: 0x000085E0
		private void ReloadListBox()
		{
			bool flag = !File.Exists(Globals.ModManagerDirectory + "ModManager.txt");
			if (!flag)
			{
				this.modsListBox.Items.Clear();
				string[] array = File.ReadAllLines(Globals.ModManagerDirectory + "ModManager.txt");
				foreach (string text in array)
				{
					string[] array3 = text.Split(new char[]
					{
						','
					});
					string path = Globals.ModManagerDirectory + "SMPCMods\\" + array3[0];
					bool flag2 = array3[1] != "0";
					int index = this.modsListBox.Items.Add(Path.GetFileName(path));
					bool flag3 = flag2;
					if (flag3)
					{
						this.modsListBox.SetItemChecked(index, flag2);
					}
				}
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000A4B8 File Offset: 0x000086B8
		private void LoadInstalledMods()
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000A4BC File Offset: 0x000086BC
		private void VortexInstallCMDFunction()
		{
			bool vortexInstallCMD = Globals.VortexInstallCMD;
			if (vortexInstallCMD)
			{
				this.saveButton_Click(null, null);
				Application.Exit();
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000A4E4 File Offset: 0x000086E4
		private void ModManagerForm_Load(object sender, EventArgs e)
		{
			this.loadOrderUpButton.Text = char.ConvertFromUtf32(8593);
			this.loadOrderDownButton.Text = char.ConvertFromUtf32(8595);
			this.LoadModManager();
			this.ReloadListBox();
			this.LoadInstalledMods();
			this.VortexInstallCMDFunction();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000A53C File Offset: 0x0000873C
		private void UpdateModManagerText()
		{
			bool flag = File.Exists(Globals.ModManagerDirectory + "ModManager.txt");
			if (flag)
			{
				File.Delete(Globals.ModManagerDirectory + "ModManager.txt");
			}
			for (int i = 0; i < this.modsListBox.Items.Count; i++)
			{
				string text = this.modsListBox.Items[i].ToString() + ",";
				bool itemChecked = this.modsListBox.GetItemChecked(i);
				if (itemChecked)
				{
					text += "1\n";
				}
				else
				{
					text += "0\n";
				}
				File.AppendAllText(Globals.ModManagerDirectory + "ModManager.txt", text);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000A604 File Offset: 0x00008804
		private void modsListBox_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			base.BeginInvoke(new Action(delegate()
			{
				this.UpdateModManagerText();
			}));
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000A61C File Offset: 0x0000881C
		private void loadOrderUpButton_Click(object sender, EventArgs e)
		{
			int selectedIndex = this.modsListBox.SelectedIndex;
			bool flag = selectedIndex == -1;
			if (!flag)
			{
				this.modsListBox.SelectedIndex = -1;
				object item = this.modsListBox.Items[selectedIndex];
				bool itemChecked = this.modsListBox.GetItemChecked(selectedIndex);
				int num = selectedIndex - 1;
				bool flag2 = num < 0;
				if (flag2)
				{
					num = 0;
				}
				this.modsListBox.Items.RemoveAt(selectedIndex);
				this.modsListBox.Items.Insert(num, item);
				this.modsListBox.SelectedIndex = num;
				this.modsListBox.SetItemChecked(num, itemChecked);
				this.UpdateModManagerText();
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000A6C8 File Offset: 0x000088C8
		private void loadOrderDownButton_Click(object sender, EventArgs e)
		{
			int selectedIndex = this.modsListBox.SelectedIndex;
			bool flag = selectedIndex == -1;
			if (!flag)
			{
				this.modsListBox.SelectedIndex = -1;
				object item = this.modsListBox.Items[selectedIndex];
				bool itemChecked = this.modsListBox.GetItemChecked(selectedIndex);
				int num = selectedIndex + 1;
				bool flag2 = num > this.modsListBox.Items.Count - 1;
				if (flag2)
				{
					num = this.modsListBox.Items.Count - 1;
				}
				this.modsListBox.Items.RemoveAt(selectedIndex);
				this.modsListBox.Items.Insert(num, item);
				this.modsListBox.SelectedIndex = num;
				this.modsListBox.SetItemChecked(num, itemChecked);
				this.UpdateModManagerText();
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000A798 File Offset: 0x00008998
		private SMPCMod ExtractSMPCMod(string fileName, string dir)
		{
			bool flag = Directory.Exists(dir);
			if (flag)
			{
				this.ModManagerForm_FormClosing(null, null);
				Directory.Delete(dir, true);
			}
			Directory.CreateDirectory(dir);
			ZipFile.ExtractToDirectory(fileName, dir);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			string text = File.ReadAllText(dir + "SMPCMod.info");
			string[] array = text.Split(new char[]
			{
				'\n'
			});
			for (int i = 0; i < array.Length; i++)
			{
				bool flag2 = array[i].StartsWith("//");
				if (!flag2)
				{
					bool flag3 = !array[i].Contains("=");
					if (!flag3)
					{
						bool flag4 = string.IsNullOrWhiteSpace(array[i]);
						if (!flag4)
						{
							dictionary.Add(array[i].Split(new char[]
							{
								'='
							})[0], array[i].Split(new char[]
							{
								'='
							})[1]);
						}
					}
				}
			}
			SMPCMod smpcmod = new SMPCMod();
			smpcmod.FileName = fileName;
			smpcmod.Title = dictionary["Title"];
			smpcmod.Author = dictionary["Author"];
			smpcmod.Description = dictionary["Description"];
			foreach (string text2 in Directory.GetFiles(dir + "ModFiles\\"))
			{
				string[] array2 = Path.GetFileName(text2).Split(new char[]
				{
					'_'
				});
				int assetArchiveIndex = int.Parse(array2[0]);
				ulong assetHash = ulong.Parse(array2[1], NumberStyles.HexNumber);
				ModFile item = default(ModFile);
				item.FileName = text2;
				item.AssetArchiveIndex = assetArchiveIndex;
				item.AssetHash = assetHash;
				smpcmod.ModFiles.Add(item);
			}
			return smpcmod;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000A968 File Offset: 0x00008B68
		private SMPCMod GetSMPCModToInstall()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "SMPC Mod (*.smpcmod)|*.smpcmod";
			openFileDialog.RestoreDirectory = true;
			bool flag = openFileDialog.ShowDialog() != DialogResult.OK;
			SMPCMod result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.ExtractSMPCMod(openFileDialog.FileName, Globals.TemporaryDirectory + "TempMod\\");
			}
			return result;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000A9C4 File Offset: 0x00008BC4
		private void installButton_Click(object sender, EventArgs e)
		{
			SMPCMod smpcmodToInstall = this.GetSMPCModToInstall();
			bool flag = smpcmodToInstall == null;
			if (!flag)
			{
				string path = Globals.ModManagerDirectory + "SMPCMods\\" + Path.GetFileName(smpcmodToInstall.FileName);
				bool flag2 = File.Exists(path);
				if (flag2)
				{
					MessageBox.Show("Mod is already added!", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					File.Copy(smpcmodToInstall.FileName, Globals.ModManagerDirectory + "SMPCMods\\" + Path.GetFileName(smpcmodToInstall.FileName));
					MessageBox.Show("Done adding mod!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					File.AppendAllText(Globals.ModManagerDirectory + "ModManager.txt", Path.GetFileName(smpcmodToInstall.FileName) + ",0\n");
					this.ReloadListBox();
					for (int i = 0; i < this.modsListBox.Items.Count; i++)
					{
						bool flag3 = this.modsListBox.Items[i].ToString() == Path.GetFileName(smpcmodToInstall.FileName);
						if (flag3)
						{
							this.modsListBox.SelectedIndex = i;
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000AAF4 File Offset: 0x00008CF4
		private void ClearStuff()
		{
			this.titleTextBox.Text = "";
			this.authorTextBox.Text = "";
			this.descriptionTextBox.Text = "";
			bool flag = this.thumbnailPictureBox.Image != null;
			if (flag)
			{
				this.thumbnailPictureBox.Image.Dispose();
				this.thumbnailPictureBox.Image = null;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000AB68 File Offset: 0x00008D68
		private void uninstallButton_Click(object sender, EventArgs e)
		{
			int selectedIndex = this.modsListBox.SelectedIndex;
			bool flag = selectedIndex != -1;
			if (flag)
			{
				string str = this.modsListBox.Items[selectedIndex].ToString();
				string path = Globals.ModManagerDirectory + "SMPCMods\\" + str;
				File.Delete(path);
				this.modsListBox.Items.RemoveAt(selectedIndex);
				this.UpdateModManagerText();
				this.ReloadListBox();
				this.LoadInstalledMods();
				this.ClearStuff();
				MessageBox.Show("Mod removed", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000AC00 File Offset: 0x00008E00
		private void modsListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ModManagerForm_FormClosing(null, null);
			int selectedIndex = this.modsListBox.SelectedIndex;
			bool flag = selectedIndex == -1;
			if (!flag)
			{
				string text = this.modsListBox.Text;
				SMPCMod smpcmod = this.ExtractSMPCMod(Globals.ModManagerDirectory + "SMPCMods\\" + text, Globals.TemporaryDirectory + "TempMod\\");
				this.titleTextBox.Text = smpcmod.Title;
				this.authorTextBox.Text = smpcmod.Author;
				this.descriptionTextBox.Text = smpcmod.Description;
				this.descriptionTextBox.Text = this.descriptionTextBox.Text.Replace("~n~", "\r\n");
				bool flag2 = File.Exists(Globals.TemporaryDirectory + "TempMod\\Thumbnail.png");
				if (flag2)
				{
					Image image = Image.FromFile(Globals.TemporaryDirectory + "TempMod\\Thumbnail.png");
					Image image2 = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
					Graphics graphics = Graphics.FromImage(image2);
					graphics.DrawImage(image, 0, 0);
					image.Dispose();
					this.thumbnailPictureBox.Image = image2;
				}
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000AD38 File Offset: 0x00008F38
		private void ModManagerForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool flag = this.thumbnailPictureBox.Image != null;
			if (flag)
			{
				this.thumbnailPictureBox.Image.Dispose();
				this.thumbnailPictureBox.Image = null;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000AD78 File Offset: 0x00008F78
		private void saveButton_Click(object sender, EventArgs e)
		{
			MainForm.DisplayWaitForm(this, "Installing Mods");
			foreach (string path in Directory.GetFiles(Globals.AssetArchivesDirectory + "mods\\"))
			{
				File.Delete(path);
			}
			Globals.TOC = new TOC(Globals.AssetArchivesDirectory + "toc.BAK");
			Globals.TOC.Decompress(Globals.TemporaryDirectory + "toc.mod.dec");
			Globals.TOC.ParseDecompressed();
			int num = 0;
			for (int j = 0; j < this.modsListBox.Items.Count; j++)
			{
				bool itemChecked = this.modsListBox.GetItemChecked(j);
				if (itemChecked)
				{
					string str = this.modsListBox.Items[j].ToString();
					SMPCMod smpcmod = this.ExtractSMPCMod(Globals.ModManagerDirectory + "SMPCMods\\" + str, Globals.TemporaryDirectory + "InstallMods\\");
					string text = smpcmod.Title.Replace(" ", "");
					text = text.Replace("_", "");
					text = text.Replace("/", "");
					text = text.Replace("\\", "");
					string text2 = Globals.AssetArchivesDirectory + "mods\\mod" + num.ToString();
					ModManagerForm.SMPCModArchive smpcmodArchive = new ModManagerForm.SMPCModArchive();
					smpcmodArchive.FileName = text2;
					smpcmodArchive.AssetArchiveName = "mods\\" + Path.GetFileName(smpcmodArchive.FileName);
					smpcmodArchive.AssetArchiveIndex = Globals.TOC.ArchiveFiles.Count + num;
					BinaryWriter binaryWriter = new BinaryWriter(new FileStream(text2, FileMode.Append, FileAccess.Write));
					foreach (ModFile modFile in smpcmod.ModFiles)
					{
						ModManagerForm.SMPCModArchiveFile item = default(ModManagerForm.SMPCModArchiveFile);
						item.OriginalAssetArchiveIndex = modFile.AssetArchiveIndex;
						item.AssetHash = modFile.AssetHash;
						item.Offset = (int)binaryWriter.BaseStream.Position;
						byte[] array = File.ReadAllBytes(modFile.FileName);
						item.Size = array.Length;
						binaryWriter.Write(array);
						smpcmodArchive.SMPCModArchiveFiles.Add(item);
					}
					binaryWriter.Close();
					binaryWriter.Dispose();
					binaryWriter = new BinaryWriter(new FileStream(Globals.TOC.decompressedFileName, FileMode.Append, FileAccess.Write));
					binaryWriter.Seek(Globals.TOC.ArchiveFileSectionOffset + Globals.TOC.ArchiveFileSectionLen + 72 * num, SeekOrigin.Begin);
					for (int k = 0; k < 72; k++)
					{
						binaryWriter.Write(0);
					}
					binaryWriter.Close();
					binaryWriter.Dispose();
					binaryWriter = new BinaryWriter(File.OpenWrite(Globals.TOC.decompressedFileName));
					binaryWriter.BaseStream.Position = (long)Globals.TOC.ArchiveFileSectionLenOffset;
					binaryWriter.Write(Globals.TOC.ArchiveFileSectionLen + 72 * (num + 1));
					binaryWriter.BaseStream.Position = 8L;
					binaryWriter.Write((int)binaryWriter.BaseStream.Length);
					binaryWriter.BaseStream.Position = (long)(Globals.TOC.ArchiveFileSectionOffset + Globals.TOC.ArchiveFileSectionLen + 72 * num);
					binaryWriter.Write(0);
					binaryWriter.Write(0);
					binaryWriter.Write(0);
					binaryWriter.Write(0);
					binaryWriter.Write(0);
					binaryWriter.Write(Encoding.UTF8.GetBytes(smpcmodArchive.AssetArchiveName));
					foreach (ModManagerForm.SMPCModArchiveFile smpcmodArchiveFile in smpcmodArchive.SMPCModArchiveFiles)
					{
						for (int l = 0; l < Globals.TOC.TOCMaps.Length; l++)
						{
							for (int m = 0; m < Globals.TOC.TOCMaps[l].TOCMapEntries.Count; m++)
							{
								bool flag = Globals.TOC.TOCMaps[l].TOCMapEntries[m].ArchiveIndex == smpcmodArchiveFile.OriginalAssetArchiveIndex;
								if (flag)
								{
									bool flag2 = Globals.TOC.TOCMaps[l].TOCMapEntries[m].FileAssetID == smpcmodArchiveFile.AssetHash;
									if (flag2)
									{
										binaryWriter.BaseStream.Position = (long)Globals.TOC.TOCMaps[l].TOCMapEntries[m].FileOffsetTOCOffset;
										binaryWriter.Write(smpcmodArchiveFile.Offset);
										binaryWriter.BaseStream.Position = (long)Globals.TOC.TOCMaps[l].TOCMapEntries[m].FileSizeTOCOffset;
										binaryWriter.Write(smpcmodArchiveFile.Size);
										binaryWriter.BaseStream.Position = (long)Globals.TOC.TOCMaps[l].TOCMapEntries[m].ArchiveIndexTOCOffset;
										binaryWriter.Write(smpcmodArchive.AssetArchiveIndex);
										break;
									}
								}
							}
						}
					}
					binaryWriter.Close();
					binaryWriter.Dispose();
					num++;
				}
			}
			File.Delete(Globals.AssetArchivesDirectory + "toc");
			Globals.TOC.Compress(Globals.AssetArchivesDirectory + "toc");
			Globals.TOC = new TOC(Globals.AssetArchivesDirectory + "toc.BAK");
			Globals.TOC.Decompress(Globals.TemporaryDirectory + "toc.dec");
			Globals.TOC.ParseDecompressed();
			MainForm.RemoveWaitForm();
			this.modsListBox_SelectedIndexChanged(null, null);
			MessageBox.Show(this, "Successfully installed Spider-Man mods!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x02000028 RID: 40
		public struct SMPCModArchiveFile
		{
			// Token: 0x040000DA RID: 218
			public int OriginalAssetArchiveIndex;

			// Token: 0x040000DB RID: 219
			public ulong AssetHash;

			// Token: 0x040000DC RID: 220
			public int Offset;

			// Token: 0x040000DD RID: 221
			public int Size;
		}

		// Token: 0x02000029 RID: 41
		public class SMPCModArchive
		{
			// Token: 0x040000DE RID: 222
			public string AssetArchiveName;

			// Token: 0x040000DF RID: 223
			public string FileName;

			// Token: 0x040000E0 RID: 224
			public int AssetArchiveIndex;

			// Token: 0x040000E1 RID: 225
			public List<ModManagerForm.SMPCModArchiveFile> SMPCModArchiveFiles = new List<ModManagerForm.SMPCModArchiveFile>();
		}
	}
}
