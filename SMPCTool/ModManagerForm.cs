using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
		// Token: 0x06000071 RID: 113 RVA: 0x0000A168 File Offset: 0x00008368
		public ModManagerForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000A180 File Offset: 0x00008380
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

		// Token: 0x06000073 RID: 115 RVA: 0x0000A21C File Offset: 0x0000841C
		private void LoadMods()
		{
			string[] files = Directory.GetFiles(Globals.ModManagerDirectory + "SMPCMods\\");
			foreach (string text in files)
			{
				SMPCMod smpcmod = this.ExtractSMPCMod(text, Globals.TemporaryDirectory + Path.GetFileNameWithoutExtension(text) + "\\");
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000A274 File Offset: 0x00008474
		private void ReloadListBox()
		{
			this.modsListBox.Items.Clear();
			string[] files = Directory.GetFiles(Globals.ModManagerDirectory + "SMPCMods\\");
			foreach (string text in files)
			{
				bool flag = text.EndsWith(".smpcmod");
				if (flag)
				{
					this.modsListBox.Items.Add(Path.GetFileName(text));
				}
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000A2EC File Offset: 0x000084EC
		private void LoadInstalledMods()
		{
			bool flag = File.Exists(Globals.ModManagerDirectory + "Install.txt");
			if (flag)
			{
				string[] array = File.ReadAllLines(Globals.ModManagerDirectory + "Install.txt");
				foreach (string b in array)
				{
					for (int j = 0; j < this.modsListBox.Items.Count; j++)
					{
						bool flag2 = this.modsListBox.Items[j].ToString() == b;
						if (flag2)
						{
							this.modsListBox.SetItemChecked(j, true);
						}
					}
				}
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000A3A0 File Offset: 0x000085A0
		private void ModManagerForm_Load(object sender, EventArgs e)
		{
			this.LoadModManager();
			this.ReloadListBox();
			this.LoadInstalledMods();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000A3B8 File Offset: 0x000085B8
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

		// Token: 0x06000078 RID: 120 RVA: 0x0000A588 File Offset: 0x00008788
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

		// Token: 0x06000079 RID: 121 RVA: 0x0000A5E4 File Offset: 0x000087E4
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
					MessageBox.Show("Mod is already installed!", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					File.Copy(smpcmodToInstall.FileName, Globals.ModManagerDirectory + "SMPCMods\\" + Path.GetFileName(smpcmodToInstall.FileName));
					MessageBox.Show("Done installing mod!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
					this.LoadInstalledMods();
				}
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000A6F0 File Offset: 0x000088F0
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

		// Token: 0x0600007B RID: 123 RVA: 0x0000A764 File Offset: 0x00008964
		private void uninstallButton_Click(object sender, EventArgs e)
		{
			int selectedIndex = this.modsListBox.SelectedIndex;
			bool flag = selectedIndex != -1;
			if (flag)
			{
				string str = this.modsListBox.Items[selectedIndex].ToString();
				string path = Globals.ModManagerDirectory + "SMPCMods\\" + str;
				File.Delete(path);
				this.ReloadListBox();
				this.LoadInstalledMods();
				this.ClearStuff();
				MessageBox.Show("Mod uninstalled", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000A7E4 File Offset: 0x000089E4
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
					this.thumbnailPictureBox.Image = Image.FromFile(Globals.TemporaryDirectory + "TempMod\\Thumbnail.png");
				}
				else
				{
					this.thumbnailPictureBox.Image = null;
				}
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		private void ModManagerForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool flag = this.thumbnailPictureBox.Image != null;
			if (flag)
			{
				this.thumbnailPictureBox.Image.Dispose();
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000A924 File Offset: 0x00008B24
		private void saveButton_Click(object sender, EventArgs e)
		{
			MainForm.DisplayWaitForm(this, "Installing Mods");
			foreach (string path in Directory.GetFiles(Globals.AssetArchivesDirectory + "mods\\"))
			{
				File.Delete(path);
			}
			bool flag = File.Exists(Globals.ModManagerDirectory + "Install.txt");
			if (flag)
			{
				File.Delete(Globals.ModManagerDirectory + "Install.txt");
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
					string text = this.modsListBox.Items[j].ToString();
					SMPCMod smpcmod = this.ExtractSMPCMod(Globals.ModManagerDirectory + "SMPCMods\\" + text, Globals.TemporaryDirectory + "InstallMods\\");
					string text2 = smpcmod.Title.Replace(" ", "");
					text2 = text2.Replace("_", "");
					text2 = text2.Replace("/", "");
					text2 = text2.Replace("\\", "");
					string text3 = Globals.AssetArchivesDirectory + "mods\\mod" + num.ToString();
					ModManagerForm.SMPCModArchive smpcmodArchive = new ModManagerForm.SMPCModArchive();
					smpcmodArchive.FileName = text3;
					smpcmodArchive.AssetArchiveName = "mods\\" + Path.GetFileName(smpcmodArchive.FileName);
					smpcmodArchive.AssetArchiveIndex = Globals.TOC.ArchiveFiles.Count + num;
					BinaryWriter binaryWriter = new BinaryWriter(new FileStream(text3, FileMode.Append, FileAccess.Write));
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
								bool flag2 = Globals.TOC.TOCMaps[l].TOCMapEntries[m].ArchiveIndex == smpcmodArchiveFile.OriginalAssetArchiveIndex;
								if (flag2)
								{
									bool flag3 = Globals.TOC.TOCMaps[l].TOCMapEntries[m].FileAssetID == smpcmodArchiveFile.AssetHash;
									if (flag3)
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
					File.AppendAllText(Globals.ModManagerDirectory + "Install.txt", text + "\n");
				}
			}
			File.Delete(Globals.AssetArchivesDirectory + "toc");
			Globals.TOC.Compress(Globals.AssetArchivesDirectory + "toc");
			Globals.TOC = new TOC(Globals.AssetArchivesDirectory + "toc.BAK");
			Globals.TOC.Decompress(Globals.TemporaryDirectory + "toc.dec");
			Globals.TOC.ParseDecompressed();
			MainForm.RemoveWaitForm();
			MessageBox.Show(this, "Successfully installed Spider-Man mods!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x02000028 RID: 40
		public struct SMPCModArchiveFile
		{
			// Token: 0x040000D7 RID: 215
			public int OriginalAssetArchiveIndex;

			// Token: 0x040000D8 RID: 216
			public ulong AssetHash;

			// Token: 0x040000D9 RID: 217
			public int Offset;

			// Token: 0x040000DA RID: 218
			public int Size;
		}

		// Token: 0x02000029 RID: 41
		public class SMPCModArchive
		{
			// Token: 0x040000DB RID: 219
			public string AssetArchiveName;

			// Token: 0x040000DC RID: 220
			public string FileName;

			// Token: 0x040000DD RID: 221
			public int AssetArchiveIndex;

			// Token: 0x040000DE RID: 222
			public List<ModManagerForm.SMPCModArchiveFile> SMPCModArchiveFiles = new List<ModManagerForm.SMPCModArchiveFile>();
		}
	}
}
