using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SMPCTool
{
	// Token: 0x02000008 RID: 8
	public partial class InstallModForm : Form
	{
		// Token: 0x06000021 RID: 33 RVA: 0x0000363C File Offset: 0x0000183C
		public InstallModForm(SMPCMod mod)
		{
			this.modRef = mod;
			this.InitializeComponent();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000365C File Offset: 0x0000185C
		private void InstallModForm_Load(object sender, EventArgs e)
		{
			this.titleTextBox.Text = this.modRef.Title;
			this.authorTextBox.Text = this.modRef.Author;
			this.descriptionTextBox.Text = this.modRef.Description;
			foreach (ModFile modFile in this.modRef.ModFiles)
			{
				string str = "0x";
				int assetArchiveIndex = modFile.AssetArchiveIndex;
				string str2 = assetArchiveIndex.ToString("X2");
				string str3 = ", 0x";
				ulong assetHash = modFile.AssetHash;
				string item = str + str2 + str3 + assetHash.ToString("X2");
				for (int i = 0; i < Globals.TOC.TOCMaps.Length; i++)
				{
					foreach (TOCMapEntry tocmapEntry in Globals.TOC.TOCMaps[i].TOCMapEntries)
					{
						bool flag = tocmapEntry.ArchiveIndex == modFile.AssetArchiveIndex && tocmapEntry.FileAssetID == modFile.AssetHash;
						if (flag)
						{
							item = tocmapEntry.FileName;
							break;
						}
					}
				}
				this.listBox1.Items.Add(item);
			}
			bool flag2 = File.Exists(Globals.ModInstallDirectory + "Thumbnail.png");
			if (flag2)
			{
				this.thumbnailPictureBox.Image = Image.FromFile(Globals.ModInstallDirectory + "Thumbnail.png");
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003828 File Offset: 0x00001A28
		private void installButton_Click(object sender, EventArgs e)
		{
			MainForm.DisplayWaitForm(this, "Installing Mod");
			int num = 0;
			foreach (ModFile modFile in this.modRef.ModFiles)
			{
				BinaryReader binaryReader = new BinaryReader(File.OpenRead(modFile.FileName));
				TOCMapEntry tocmapEntry = null;
				for (int i = 0; i < Globals.TOC.TOCMaps.Length; i++)
				{
					foreach (TOCMapEntry tocmapEntry2 in Globals.TOC.TOCMaps[i].TOCMapEntries)
					{
						bool flag = tocmapEntry2.ArchiveIndex == modFile.AssetArchiveIndex && tocmapEntry2.FileAssetID == modFile.AssetHash;
						if (flag)
						{
							tocmapEntry = tocmapEntry2;
							break;
						}
					}
				}
				MainForm.SetWaitFormText(string.Concat(new string[]
				{
					"Installing Mod File (",
					(num + 1).ToString(),
					"/",
					this.modRef.ModFiles.Count.ToString(),
					")\n0x",
					tocmapEntry.FileAssetID.ToString("X2")
				}));
				int fileSize = tocmapEntry.FileSize;
				int num2 = (int)binaryReader.BaseStream.Length;
				byte[] buffer = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
				binaryReader.Close();
				binaryReader.Dispose();
				int num3 = -1;
				for (int j = 0; j < Globals.TOC.TOCMaps.Length; j++)
				{
					for (int k = 0; k < Globals.TOC.TOCMaps[j].TOCMapEntries.Count; k++)
					{
						bool flag2 = Globals.TOC.TOCMaps[j].TOCMapEntries[k].ArchiveIndex == tocmapEntry.ArchiveIndex;
						if (flag2)
						{
							bool flag3 = Globals.TOC.TOCMaps[j].TOCMapEntries[k].FileAssetID == tocmapEntry.FileAssetID;
							if (flag3)
							{
								bool flag4 = num2 > Globals.TOC.TOCMaps[j].TOCMapEntries[k].FileSize;
								if (flag4)
								{
									num3 = 2;
								}
								else
								{
									bool flag5 = num2 < Globals.TOC.TOCMaps[j].TOCMapEntries[k].FileSize;
									if (flag5)
									{
										num3 = 1;
									}
									else
									{
										bool flag6 = num2 == Globals.TOC.TOCMaps[j].TOCMapEntries[k].FileSize;
										if (flag6)
										{
											num3 = 0;
										}
									}
								}
								Globals.TOC.TOCMaps[j].TOCMapEntries[k].FileSize = num2;
								break;
							}
						}
					}
				}
				for (int l = 0; l < Globals.TOC.TOCMaps.Length; l++)
				{
					for (int m = 0; m < Globals.TOC.TOCMaps[l].TOCMapEntries.Count; m++)
					{
						bool flag7 = Globals.TOC.TOCMaps[l].TOCMapEntries[m].ArchiveIndex == tocmapEntry.ArchiveIndex;
						if (flag7)
						{
							bool flag8 = num3 == 2;
							if (flag8)
							{
								bool flag9 = Globals.TOC.TOCMaps[l].TOCMapEntries[m].FileOffset > tocmapEntry.FileOffset;
								if (flag9)
								{
									Globals.TOC.TOCMaps[l].TOCMapEntries[m].FileOffset += (uint)(num2 - fileSize);
								}
							}
							else
							{
								bool flag10 = num3 == 1;
								if (flag10)
								{
									bool flag11 = Globals.TOC.TOCMaps[l].TOCMapEntries[m].FileOffset > tocmapEntry.FileOffset;
									if (flag11)
									{
										Globals.TOC.TOCMaps[l].TOCMapEntries[m].FileOffset -= (uint)(fileSize - num2);
									}
								}
							}
						}
					}
				}
				BinaryWriter binaryWriter = new BinaryWriter(File.OpenWrite(Globals.TOC.decompressedFileName));
				binaryWriter.BaseStream.Position = (long)tocmapEntry.FileSizeTOCOffset;
				binaryWriter.Write(num2);
				for (int n = 0; n < Globals.TOC.TOCMaps.Length; n++)
				{
					for (int num4 = 0; num4 < Globals.TOC.TOCMaps[n].TOCMapEntries.Count; num4++)
					{
						binaryWriter.BaseStream.Position = (long)Globals.TOC.TOCMaps[n].TOCMapEntries[num4].FileOffsetTOCOffset;
						binaryWriter.Write((int)Globals.TOC.TOCMaps[n].TOCMapEntries[num4].FileOffset);
					}
				}
				binaryWriter.Close();
				binaryWriter.Dispose();
				binaryReader = new BinaryReader(File.OpenRead(Globals.AssetArchivesDirectory + tocmapEntry.ArchiveName));
				uint num5 = binaryReader.ReadUInt32();
				List<MainForm.PaddingBlock> list = new List<MainForm.PaddingBlock>();
				binaryWriter = new BinaryWriter(File.OpenWrite(Globals.TemporaryDirectory + tocmapEntry.ArchiveName));
				bool flag12 = num5 == 1380012868U;
				if (!flag12)
				{
					binaryReader.BaseStream.Position = 0L;
					binaryWriter.BaseStream.Position = 0L;
					bool flag13 = false;
					while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
					{
						byte[] buffer2 = binaryReader.ReadBytes(4096);
						binaryWriter.Write(buffer2);
						bool flag14 = binaryWriter.BaseStream.Position >= (long)((ulong)tocmapEntry.FileOffset) && !flag13;
						if (flag14)
						{
							binaryWriter.BaseStream.Position = (long)((ulong)tocmapEntry.FileOffset);
							binaryWriter.Write(buffer);
							binaryReader.BaseStream.Position = (long)((ulong)tocmapEntry.FileOffset + (ulong)((long)fileSize));
							flag13 = true;
						}
					}
					binaryReader.Close();
					binaryReader.Dispose();
					binaryWriter.Close();
					binaryWriter.Dispose();
					File.Delete(Globals.AssetArchivesDirectory + tocmapEntry.ArchiveName);
					File.Move(Globals.TemporaryDirectory + tocmapEntry.ArchiveName, Globals.AssetArchivesDirectory + tocmapEntry.ArchiveName);
				}
				File.Delete(Globals.AssetArchivesDirectory + "toc");
				Globals.TOC.Compress(Globals.AssetArchivesDirectory + "toc");
				Globals.TOC = new TOC(Globals.AssetArchivesDirectory + "toc");
				Globals.TOC.Decompress(Globals.TemporaryDirectory + "toc.dec");
				Globals.TOC.ParseDecompressed();
				num++;
			}
			MainForm.RemoveWaitForm();
			MessageBox.Show(this, "Successfully installed Spider-Man Mod!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			base.Close();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003FC8 File Offset: 0x000021C8
		private void InstallModForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool flag = this.thumbnailPictureBox.Image != null;
			if (flag)
			{
				this.thumbnailPictureBox.Image.Dispose();
			}
			bool flag2 = Directory.Exists(Globals.ModInstallDirectory);
			if (flag2)
			{
				Directory.Delete(Globals.ModInstallDirectory, true);
			}
		}

		// Token: 0x04000024 RID: 36
		private SMPCMod modRef;
	}
}
