using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SMPCTool
{
	// Token: 0x0200000B RID: 11
	public partial class MainForm : Form
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00004F10 File Offset: 0x00003110
		public MainForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00004F30 File Offset: 0x00003130
		private static TOCMapEntry GetTOCMapEntryByAssetID(ulong assetID)
		{
			for (int i = 0; i < Globals.TOC.TOCMaps.Length; i++)
			{
				foreach (TOCMapEntry tocmapEntry in Globals.TOC.TOCMaps[i].TOCMapEntries)
				{
					bool flag = tocmapEntry.FileAssetID == assetID;
					if (flag)
					{
						return tocmapEntry;
					}
				}
			}
			return null;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00004FC8 File Offset: 0x000031C8
		private static TOCMapEntry GetTOCMapEntryByAssetIDSpecificArchiveIndex(ulong assetID, int archiveIndex)
		{
			for (int i = 0; i < Globals.TOC.TOCMaps.Length; i++)
			{
				foreach (TOCMapEntry tocmapEntry in Globals.TOC.TOCMaps[i].TOCMapEntries)
				{
					bool flag = tocmapEntry.FileAssetID == assetID && tocmapEntry.ArchiveIndex == archiveIndex;
					if (flag)
					{
						return tocmapEntry;
					}
				}
			}
			return null;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000506C File Offset: 0x0000326C
		private void SwapModTOC(string originalAssetFileName, string swapAssetFileName, int originalArchiveIndex = -1, int swapArchiveIndex = -1)
		{
			bool flag = Globals.TOC == null;
			if (!flag)
			{
				bool flag2 = false;
				ulong originalAssetID = 0UL;
				int num = 0;
				while (num < Globals.TOC.TOCMaps.Length && !flag2)
				{
					bool flag3 = Globals.TOC.TOCMaps[num] == null;
					if (!flag3)
					{
						foreach (TOCMapEntry tocmapEntry in Globals.TOC.TOCMaps[num].TOCMapEntries)
						{
							bool flag4 = tocmapEntry.FileName.ToLower() == originalAssetFileName.ToLower();
							if (flag4)
							{
								bool flag5 = originalArchiveIndex != -1;
								if (flag5)
								{
									bool flag6 = tocmapEntry.ArchiveIndex != originalArchiveIndex;
									if (flag6)
									{
										continue;
									}
								}
								originalAssetID = tocmapEntry.FileAssetID;
								originalArchiveIndex = tocmapEntry.ArchiveIndex;
								flag2 = true;
								break;
							}
						}
					}
					num++;
				}
				bool flag7 = !flag2;
				if (flag7)
				{
					MessageBox.Show("Original asset filename not found:\n" + originalAssetFileName, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					bool flag8 = false;
					ulong swapAssetID = 0UL;
					int num2 = 0;
					while (num2 < Globals.TOC.TOCMaps.Length && !flag8)
					{
						bool flag9 = Globals.TOC.TOCMaps[num2] == null;
						if (!flag9)
						{
							foreach (TOCMapEntry tocmapEntry2 in Globals.TOC.TOCMaps[num2].TOCMapEntries)
							{
								bool flag10 = tocmapEntry2.FileName.ToLower() == swapAssetFileName.ToLower();
								if (flag10)
								{
									bool flag11 = swapArchiveIndex != -1;
									if (flag11)
									{
										bool flag12 = tocmapEntry2.ArchiveIndex != swapArchiveIndex;
										if (flag12)
										{
											continue;
										}
									}
									swapAssetID = tocmapEntry2.FileAssetID;
									swapArchiveIndex = tocmapEntry2.ArchiveIndex;
									flag8 = true;
									break;
								}
							}
						}
						num2++;
					}
					bool flag13 = !flag8;
					if (flag13)
					{
						MessageBox.Show("Swap asset filename not found:\n" + swapAssetFileName, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else
					{
						MainForm.SwapModTOCSpecificArchiveIndex(originalAssetID, swapAssetID, originalArchiveIndex, swapArchiveIndex);
					}
				}
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000052D8 File Offset: 0x000034D8
		private static void SwapModTOC(TOCMapEntry originalEntry, TOCMapEntry swapEntry)
		{
			int archiveIndexTOCOffset = originalEntry.ArchiveIndexTOCOffset;
			int fileOffsetTOCOffset = originalEntry.FileOffsetTOCOffset;
			int fileSizeTOCOffset = originalEntry.FileSizeTOCOffset;
			int archiveIndexTOCOffset2 = swapEntry.ArchiveIndexTOCOffset;
			int fileOffsetTOCOffset2 = swapEntry.FileOffsetTOCOffset;
			int fileSizeTOCOffset2 = swapEntry.FileSizeTOCOffset;
			BinaryReader binaryReader = new BinaryReader(File.OpenRead(Globals.TOC.decompressedFileName));
			binaryReader.BaseStream.Position = (long)archiveIndexTOCOffset2;
			int value = binaryReader.ReadInt32();
			binaryReader.BaseStream.Position = (long)fileOffsetTOCOffset2;
			int value2 = binaryReader.ReadInt32();
			binaryReader.BaseStream.Position = (long)fileSizeTOCOffset2;
			int value3 = binaryReader.ReadInt32();
			binaryReader.Close();
			binaryReader.Dispose();
			BinaryWriter binaryWriter = new BinaryWriter(File.OpenWrite(Globals.TOC.decompressedFileName));
			binaryWriter.BaseStream.Position = (long)archiveIndexTOCOffset;
			binaryWriter.Write(value);
			binaryWriter.BaseStream.Position = (long)fileOffsetTOCOffset;
			binaryWriter.Write(value2);
			binaryWriter.BaseStream.Position = (long)fileSizeTOCOffset;
			binaryWriter.Write(value3);
			binaryWriter.Close();
			binaryWriter.Dispose();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000053F4 File Offset: 0x000035F4
		private static void SwapModTOC(ulong originalAssetID, ulong swapAssetID)
		{
			TOCMapEntry tocmapEntryByAssetID = MainForm.GetTOCMapEntryByAssetID(originalAssetID);
			TOCMapEntry tocmapEntryByAssetID2 = MainForm.GetTOCMapEntryByAssetID(swapAssetID);
			MainForm.SwapModTOC(tocmapEntryByAssetID, tocmapEntryByAssetID2);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00005418 File Offset: 0x00003618
		private static void SwapModTOCSpecificArchiveIndex(ulong originalAssetID, ulong swapAssetID, int originalArchiveIndex, int swapArchiveIndex)
		{
			TOCMapEntry tocmapEntryByAssetIDSpecificArchiveIndex = MainForm.GetTOCMapEntryByAssetIDSpecificArchiveIndex(originalAssetID, originalArchiveIndex);
			TOCMapEntry tocmapEntryByAssetIDSpecificArchiveIndex2 = MainForm.GetTOCMapEntryByAssetIDSpecificArchiveIndex(swapAssetID, swapArchiveIndex);
			MainForm.SwapModTOC(tocmapEntryByAssetIDSpecificArchiveIndex, tocmapEntryByAssetIDSpecificArchiveIndex2);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000543E File Offset: 0x0000363E
		private void InitLVCS()
		{
			this.lvcs = new ListViewColumnSorter();
			this.fileListView.ListViewItemSorter = this.lvcs;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000545E File Offset: 0x0000365E
		private void DisposeLVCS()
		{
			this.lvcs = null;
			this.fileListView.ListViewItemSorter = null;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00005475 File Offset: 0x00003675
		private void SpiderRatTOC()
		{
			this.SwapModTOC("characters\\hero\\hero_spiderman\\hero_spiderman_body.model", "characters\\ambient\\amb_rat\\amb_rat.model", -1, -1);
			this.SwapModTOC("characters\\hero\\hero_spiderman_classic\\hero_spiderman_classic.model", "characters\\ambient\\amb_rat\\amb_rat.model", -1, -1);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000054A0 File Offset: 0x000036A0
		private void AdvancedSuitBlackModTOC()
		{
			this.SwapModTOC("characters\\hero\\hero_spiderman_classic\\hero_spiderman_classic.model", "characters\\hero\\hero_spiderman\\hero_spiderman_body.model", -1, -1);
			this.SwapModTOC("characters\\hero\\hero_spiderman_classic\\hero_spiderman_classic_lightdamaged.model", "characters\\hero\\hero_spiderman\\hero_spiderman_body.model", -1, -1);
			this.SwapModTOC("characters\\hero\\hero_spiderman_classic\\hero_spiderman_classic_damaged.model", "characters\\hero\\hero_spiderman\\hero_spiderman_body.model", -1, -1);
			this.SwapModTOC("characters\\hero\\hero_spiderman\\textures\\hero_spiderman_head_c.texture", "characters\\hero\\hero_spiderman_black\\textures\\hero_spiderman_black_head_c.texture", 0, 1);
			this.SwapModTOC("characters\\hero\\hero_spiderman\\textures\\hero_spiderman_head_c.texture", "characters\\hero\\hero_spiderman_black\\textures\\hero_spiderman_black_head_c.texture", 14, 15);
			this.SwapModTOC("characters\\hero\\hero_spiderman\\textures\\hero_spiderman_head_g.texture", "characters\\hero\\hero_spiderman_black\\textures\\hero_spiderman_black_head_g.texture", 0, 1);
			this.SwapModTOC("characters\\hero\\hero_spiderman\\textures\\hero_spiderman_head_g.texture", "characters\\hero\\hero_spiderman_black\\textures\\hero_spiderman_black_head_g.texture", 14, 15);
			this.SwapModTOC("characters\\hero\\hero_spiderman\\textures\\hero_spiderman_body_c.texture", "characters\\hero\\hero_spiderman_black\\textures\\hero_spiderman_black_body_c.texture", 0, 1);
			this.SwapModTOC("characters\\hero\\hero_spiderman\\textures\\hero_spiderman_body_c.texture", "characters\\hero\\hero_spiderman_black\\textures\\hero_spiderman_black_body_c.texture", 14, 15);
			this.SwapModTOC("characters\\hero\\hero_spiderman\\textures\\hero_spiderman_body_g.texture", "characters\\hero\\hero_spiderman_black\\textures\\hero_spiderman_black_body_g.texture", 0, 1);
			this.SwapModTOC("characters\\hero\\hero_spiderman\\textures\\hero_spiderman_body_g.texture", "characters\\hero\\hero_spiderman_black\\textures\\hero_spiderman_black_body_g.texture", 14, 15);
			this.SwapModTOC("characters\\hero\\hero_spiderman\\textures\\hero_spiderman_leg_modcolor_c.texture", "characters\\hero\\hero_spiderman_black\\textures\\hero_spiderman_black_legs_c.texture", 0, 1);
			this.SwapModTOC("characters\\hero\\hero_spiderman\\textures\\hero_spiderman_leg_modcolor_c.texture", "characters\\hero\\hero_spiderman_black\\textures\\hero_spiderman_black_legs_c.texture", 14, 15);
			this.SwapModTOC("characters\\hero\\hero_spiderman\\textures\\hero_spiderman_leg_g.texture", "characters\\hero\\hero_spiderman_black\\textures\\hero_spiderman_black_legs_g.texture", 0, 1);
			this.SwapModTOC("characters\\hero\\hero_spiderman\\textures\\hero_spiderman_leg_g.texture", "characters\\hero\\hero_spiderman_black\\textures\\hero_spiderman_black_legs_g.texture", 14, 15);
			this.SwapModTOC("characters\\hero\\hero_spiderman\\textures\\hero_spiderman_glove_modcolor_c.texture", "characters\\hero\\hero_spiderman_black\\textures\\hero_spiderman_black_body_c.texture", 0, 1);
			this.SwapModTOC("characters\\hero\\hero_spiderman\\textures\\hero_spiderman_glove_modcolor_c.texture", "characters\\hero\\hero_spiderman_black\\textures\\hero_spiderman_black_body_c.texture", 14, 15);
			this.SwapModTOC("characters\\hero\\hero_spiderman\\textures\\hero_spiderman_glove_g.texture", "characters\\hero\\hero_spiderman_black\\textures\\hero_spiderman_black_body_g.texture", 0, 1);
			this.SwapModTOC("characters\\hero\\hero_spiderman\\textures\\hero_spiderman_glove_g.texture", "characters\\hero\\hero_spiderman_black\\textures\\hero_spiderman_black_body_g.texture", 14, 15);
			this.SwapModTOC("characters\\hero\\hero_spiderman\\textures\\hero_spiderman_shoe_modcolor_c.texture", "characters\\hero\\hero_spiderman_black\\textures\\hero_spiderman_black_shoes_c.texture", 0, 1);
			this.SwapModTOC("characters\\hero\\hero_spiderman\\textures\\hero_spiderman_shoe_modcolor_c.texture", "characters\\hero\\hero_spiderman_black\\textures\\hero_spiderman_black_shoes_c.texture", 14, 15);
			this.SwapModTOC("characters\\hero\\hero_spiderman\\textures\\hero_spiderman_shoe_g.texture", "characters\\hero\\hero_spiderman_black\\textures\\hero_spiderman_black_shoes_g.texture", 0, 1);
			this.SwapModTOC("characters\\hero\\hero_spiderman\\textures\\hero_spiderman_shoe_g.texture", "characters\\hero\\hero_spiderman_black\\textures\\hero_spiderman_black_shoes_g.texture", 14, 15);
			this.SwapModTOC("material\\characters\\hero\\hero_spiderman_shooter\\hero_spiderman_webshooter.material", "materials\\test_black\\test_black.material", -1, -1);
			this.SwapModTOC("characters\\hero\\hero_peterparker\\hero_peterparker_body.model", "characters\\hero\\hero_peterparker\\hero_peterparker_funeral.model", -1, -1);
			this.SwapModTOC("characters\\hero\\hero_peterparker\\hero_peterparker_tshirtcasual_cine.actor", "actors\\hrm_bdy_firsthero\\hero_spiderman_whitespider_nomask.actor", -1, -1);
			this.SwapModTOC("characters\\hero\\hero_peterparker\\hero_peterparker_body_seminudeonly.actor", "actors\\hrm_bdy_firsthero\\hero_spiderman_whitespider_nomask.actor", -1, -1);
			this.SwapModTOC("characters\\hero\\hero_peterparker\\heads\\hero_peterparker_head_spideysuit.model", "characters\\hero\\hero_peterparker\\heads\\hero_peterparker_head.model", -1, -1);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000056D8 File Offset: 0x000038D8
		private void MilesMoralesSuitV2ModToc()
		{
			this.SwapModTOC("configs\\vanitytor1\\vanitytor1aspiderman1.config", "configs\\vanitytor1\\vanitytor1amilesmorales1.config", -1, -1);
			this.SwapModTOC("configs\\vanitytor1\\vanity_spiderman_classic.config", "configs\\vanitytor1\\vanitytor1amilesmorales1.config", -1, -1);
			this.SwapModTOC("configs\\vanitytor1\\vanity_spiderman_classic_damaged.config", "configs\\vanitytor1\\vanitytor1amilesmorales1.config", -1, -1);
			this.SwapModTOC("configs\\vanitytor1\\vanity_spiderman_classic_lightdamaged.config", "configs\\vanitytor1\\vanitytor1amilesmorales1.config", -1, -1);
			this.SwapModTOC("configs\\vanitybodytype\\vanitybody_spiderman.config", "configs\\vanitybodytype\\vanitybody_milesmorales.config", -1, -1);
			MainForm.SwapModTOC(11660614329086415369UL, 10121741667616392492UL);
			this.SwapModTOC("materials\\hrm_bdy_raj_body\\hero_spiderman_eyeshutters.material", "materials\\ceg_test_chrome_01\\hero_spiderman_eye_base.material", -1, -1);
			this.SwapModTOC("material\\equipment\\gadget\\gdgt_ElectricWeb_Arc_Sphere_V2\\gdgt_ElectricWeb_Arc_Sphere_V2.material", "material\\equipment\\gadget\\gdgt_electricweb_arc_sphere_v2\\gdgt_jd_taser_arc_sphere.material", -1, -1);
			this.SwapModTOC("material\\visualeffect\\global\\gbl_electricity_arc_01\\gbl_junctionbox_arcs.material", "material\\equipment\\gadget\\gdgt_electricweb_arc_sphere_v2\\gdgt_jd_taser_arc_sphere.material", -1, -1);
			this.SwapModTOC("material\\visualeffect\\global\\gbl_Fx_Spark_Random_3d\\gbl_Fx_Spark_Random_3d.material", "material\\equipment\\gadget\\gdgt_electricweb_arc_sphere_v2\\gdgt_jd_taser_arc_sphere.material", -1, -1);
			this.SwapModTOC("materials\\fx_flare_star_concert_stage\\fx_flare_star_concert_stage.material", "material\\equipment\\gadget\\gdgt_electricweb_arc_sphere_v2\\gdgt_jd_taser_arc_sphere.material", -1, -1);
			this.SwapModTOC("visualeffect\\equipment\\gadget\\gdgt_electricweb_bodystatus_level0\\gdgt_electricweb_bodystatus_level0.visualeffect", "visualeffect\\equipment\\gadget\\gdgt_electricweb_bodystatus_level0\\gdgt_jd_taser_bodystatus_level0.visualeffect", -1, -1);
			this.SwapModTOC("visualeffect\\equipment\\gadget\\gdgt_electricweb_bodystatus_level0\\gdgt_electricweb_bodystatus_level1.visualeffect", "visualeffect\\equipment\\gadget\\gdgt_electricweb_bodystatus_level0\\gdgt_jd_taser_bodystatus_level0.visualeffect", -1, -1);
			this.SwapModTOC("visualeffect\\equipment\\gadget\\gdgt_electricweb_bodystatus_level0\\gdgt_electricweb_bodystatus_level2.visualeffect", "visualeffect\\equipment\\gadget\\gdgt_electricweb_bodystatus_level0\\gdgt_jd_taser_bodystatus_level0.visualeffect", -1, -1);
			this.SwapModTOC("visualeffect\\equipment\\gadget\\gdgt_electricweb_bodystatus_level0\\gdgt_electricweb_bodystatus_level3.visualeffect", "visualeffect\\equipment\\gadget\\gdgt_electricweb_bodystatus_level0\\gdgt_jd_taser_bodystatus_level0.visualeffect", -1, -1);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00005808 File Offset: 0x00003A08
		private void TOCSwapModTest()
		{
			TOC toc = new TOC("TOCSwapModTest\\toc");
			toc.Decompress("TOCSwapModTest\\toc.dec");
			toc.ParseDecompressed();
			Globals.TOC = toc;
			this.MilesMoralesSuitV2ModToc();
			toc.Compress("TOCSwapModTest\\toc.com");
			Globals.TOC = null;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00005854 File Offset: 0x00003A54
		public static void DisplayWaitForm(Form form, string text)
		{
			bool flag = MainForm.WaitForm != null;
			if (!flag)
			{
				Thread thread = new Thread(delegate()
				{
					new WaitForm().ShowDialog();
				});
				thread.Start();
				while (MainForm.WaitForm == null || !MainForm.WaitForm.IsHandleCreated)
				{
					Thread.Sleep(10);
				}
				MainForm.SetWaitFormText(text);
				MainForm.WaitFormReturnForm = form;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000058D0 File Offset: 0x00003AD0
		public static void SetWaitFormText(string text)
		{
			bool flag = MainForm.WaitForm == null;
			if (!flag)
			{
				MainForm.WaitForm.Invoke(new MethodInvoker(delegate()
				{
					MainForm.WaitForm.SetText(text);
				}));
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00005910 File Offset: 0x00003B10
		public static void RemoveWaitForm()
		{
			bool flag = MainForm.WaitForm == null;
			if (!flag)
			{
				MainForm.WaitForm.Invoke(new MethodInvoker(delegate()
				{
					MainForm.WaitForm.Close();
				}));
				MainForm.WaitForm = null;
				MainForm.WaitFormReturnForm.Activate();
				MainForm.WaitFormReturnForm = null;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000596D File Offset: 0x00003B6D
		private void UpdateTitle(string text)
		{
			this.Text = "Spider-Man PC Tool v" + Globals.Version + text;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00005988 File Offset: 0x00003B88
		private void MainForm_Load(object sender, EventArgs e)
		{
			this.MinimumSize = base.Size;
			this.UpdateTitle(" - NO PROJECT");
			Globals.TemporaryDirectory = "temp\\";
			bool flag = !Directory.Exists(Globals.TemporaryDirectory);
			if (flag)
			{
				DirectoryInfo directoryInfo = Directory.CreateDirectory(Globals.TemporaryDirectory);
				directoryInfo.Attributes |= FileAttributes.Hidden;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000059E8 File Offset: 0x00003BE8
		private void TestTocLayout()
		{
			TOC toc = new TOC("toc");
			toc.Decompress("toc.dec");
			toc.ParseDecompressed();
			toc.GenerateCSV("layout.csv");
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00005A20 File Offset: 0x00003C20
		private void ArchiveTreeViewReset()
		{
			this.archiveTreeView.BeginUpdate();
			for (int i = 0; i < Globals.TOC.ArchiveFiles.Count; i++)
			{
				this.archiveTreeView.Nodes.Add(Globals.TOC.ArchiveFiles[i].Filename);
			}
			this.archiveTreeView.EndUpdate();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00005A8C File Offset: 0x00003C8C
		private void selectAssetArchiveFolderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = Globals.TOC != null;
			if (!flag)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Title = "Select Asset Archive Folder...";
				saveFileDialog.FileName = "[SELECT ASSET ARCHIVE FOLDER]";
				saveFileDialog.RestoreDirectory = true;
				bool flag2 = File.Exists("assetArchiveDir.txt");
				if (flag2)
				{
					saveFileDialog.InitialDirectory = File.ReadAllText("assetArchiveDir.txt");
				}
				bool flag3 = saveFileDialog.ShowDialog() != DialogResult.OK;
				if (!flag3)
				{
					string str = saveFileDialog.FileName.Substring(0, saveFileDialog.FileName.LastIndexOf("\\"));
					string path = str + "\\toc";
					bool flag4 = !File.Exists(path);
					if (flag4)
					{
						MessageBox.Show("TOC (Table of Contents) file is missing!", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else
					{
						Globals.AssetArchivesDirectory = str + "\\";
						File.WriteAllText("assetArchiveDir.txt", Globals.AssetArchivesDirectory);
						MessageBox.Show("Asset Archives Successfully Set!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						bool flag5 = !File.Exists(Globals.AssetArchivesDirectory + "toc.BAK");
						if (flag5)
						{
							File.Copy(Globals.AssetArchivesDirectory + "toc", Globals.AssetArchivesDirectory + "toc.BAK");
						}
						Globals.TOC = new TOC(Globals.AssetArchivesDirectory + "toc.BAK");
						Globals.TOC.Decompress(Globals.TemporaryDirectory + "toc.dec");
						Globals.TOC.ParseDecompressed();
						this.ArchiveTreeViewReset();
					}
				}
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00005C18 File Offset: 0x00003E18
		private void hashGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StringToHashForm stringToHashForm = new StringToHashForm();
			stringToHashForm.ShowDialog();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00005C34 File Offset: 0x00003E34
		private void generateLayoutCSVToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = Globals.TOC == null;
			if (!flag)
			{
				Globals.TOC.GenerateCSV("layout.csv");
				MessageBox.Show("Generated layout.csv", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00005C73 File Offset: 0x00003E73
		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00005C7C File Offset: 0x00003E7C
		private void CreateNodePath(TreeNodeCollection nodeList, string path)
		{
			TreeNode treeNode = null;
			string text = string.Empty;
			int num = path.IndexOf('/');
			bool flag = num == -1;
			if (flag)
			{
				text = path;
				path = "";
			}
			else
			{
				text = path.Substring(0, num);
				path = path.Substring(num + 1, path.Length - (num + 1));
			}
			treeNode = null;
			foreach (object obj in nodeList)
			{
				TreeNode treeNode2 = (TreeNode)obj;
				bool flag2 = treeNode2.Text == text;
				if (flag2)
				{
					treeNode = treeNode2;
				}
			}
			bool flag3 = treeNode == null;
			if (flag3)
			{
				treeNode = new TreeNode(text);
				nodeList.Add(treeNode);
			}
			bool flag4 = path != "";
			if (flag4)
			{
				this.CreateNodePath(treeNode.Nodes, path);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00005D74 File Offset: 0x00003F74
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			MainForm.RemoveWaitForm();
			string processName = Process.GetCurrentProcess().ProcessName;
			bool flag = Process.GetProcessesByName(processName).Length <= 1;
			if (flag)
			{
				bool flag2 = Directory.Exists(Globals.TemporaryDirectory);
				if (flag2)
				{
					Directory.Delete(Globals.TemporaryDirectory, true);
				}
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00005DC4 File Offset: 0x00003FC4
		private void ClearRows(ListView lv)
		{
			while (lv.Items.Count > 1)
			{
				lv.Items.RemoveAt(1);
			}
			bool flag = lv.Items.Count > 0;
			if (flag)
			{
				lv.Items.RemoveAt(0);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00005E18 File Offset: 0x00004018
		private TreeNode GetRootNode(TreeNode node)
		{
			TreeNode treeNode = node;
			while (treeNode.Parent != null)
			{
				treeNode = treeNode.Parent;
			}
			return treeNode;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00005E48 File Offset: 0x00004048
		private TreeNode FindTreeNodeWithPath(TreeNodeCollection nodes, string path)
		{
			this.findTreeNodeWithPathFound = null;
			foreach (object obj in nodes)
			{
				TreeNode treeNode = (TreeNode)obj;
				bool flag = this.findTreeNodeWithPathFound != null;
				if (flag)
				{
					return this.findTreeNodeWithPathFound;
				}
				bool flag2 = treeNode.FullPath.ToLower() == path.ToLower();
				if (flag2)
				{
					this.findTreeNodeWithPathFound = treeNode;
					return treeNode;
				}
				this.FindTreeNodeWithPath(treeNode.Nodes, path);
			}
			return null;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00005EF8 File Offset: 0x000040F8
		public void SearchFind(TOCMapEntry tocMapEntry)
		{
			int archiveIndex = tocMapEntry.ArchiveIndex;
			this.archiveTreeView.SelectedNode = this.archiveTreeView.Nodes[archiveIndex];
			this.archiveTreeView.SelectedNode.Expand();
			this.archiveTreeView.Focus();
			this.archiveTreeView_DoubleClick(null, null);
			string text = tocMapEntry.ArchiveName + "\\" + tocMapEntry.FileName;
			text = text.Substring(0, text.LastIndexOf("\\"));
			TreeNode selectedNode = this.FindTreeNodeWithPath(this.archiveTreeView.Nodes, text);
			this.archiveTreeView.SelectedNode = selectedNode;
			this.archiveTreeView.SelectedNode.Expand();
			this.archiveTreeView.Focus();
			this.archiveTreeView_DoubleClick(null, null);
			for (int i = 0; i < this.fileListView.Items.Count; i++)
			{
				ulong num = ulong.Parse(this.fileListView.Items[i].SubItems[3].Text);
				bool flag = num == tocMapEntry.FileAssetID;
				if (flag)
				{
					this.fileListView.Items[i].Selected = true;
					this.fileListView.Select();
					this.fileListView.Focus();
					this.fileListView.Items[i].Focused = true;
					this.fileListView.Items[i].EnsureVisible();
					break;
				}
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00006084 File Offset: 0x00004284
		private void UpdateFileListView()
		{
			this.fileListView.BeginUpdate();
			this.ClearRows(this.fileListView);
			string fullPath = this.archiveTreeView.SelectedNode.FullPath;
			int index = this.GetRootNode(this.archiveTreeView.SelectedNode).Index;
			bool flag = fullPath.IndexOf("\\") == -1;
			if (flag)
			{
				this.DisposeLVCS();
				for (int i = 0; i < Globals.TOC.TOCMaps.Length; i++)
				{
					foreach (TOCMapEntry tocmapEntry in Globals.TOC.TOCMaps[i].TOCMapEntries)
					{
						bool flag2 = tocmapEntry.ArchiveIndex == index;
						if (flag2)
						{
							bool flag3 = tocmapEntry.FileName.StartsWith("0x") || !tocmapEntry.FileName.Contains("\\");
							if (flag3)
							{
								string[] items = new string[]
								{
									tocmapEntry.FileName,
									tocmapEntry.FileSize.ToString(),
									"Unknown",
									tocmapEntry.FileAssetID.ToString()
								};
								this.fileListView.Items.Add(new ListViewItem(items));
							}
						}
					}
				}
			}
			else
			{
				this.InitLVCS();
				string text = fullPath.Substring(fullPath.IndexOf("\\") + 1);
				for (int j = 0; j < Globals.TOC.TOCMaps.Length; j++)
				{
					foreach (TOCMapEntry tocmapEntry2 in Globals.TOC.TOCMaps[j].TOCMapEntries)
					{
						bool flag4 = tocmapEntry2.ArchiveIndex == index;
						if (flag4)
						{
							bool flag5 = tocmapEntry2.FileName.IndexOf(text) != -1;
							if (flag5)
							{
								bool flag6 = !tocmapEntry2.FileName.Substring(text.Length + 1).Contains("\\");
								if (flag6)
								{
									string text2 = Path.GetExtension(tocmapEntry2.FileName);
									text2 = text2.Substring(1);
									text2 = char.ToUpper(text2[0]).ToString() + text2.Substring(1);
									string[] items2 = new string[]
									{
										Path.GetFileNameWithoutExtension(tocmapEntry2.FileName),
										tocmapEntry2.FileSize.ToString(),
										text2,
										tocmapEntry2.FileAssetID.ToString()
									};
									this.fileListView.Items.Add(new ListViewItem(items2));
								}
							}
						}
					}
				}
			}
			bool flag7 = this.lvcs != null;
			if (flag7)
			{
				this.lvcs.Order = SortOrder.Ascending;
				this.lvcs.SortColumn = 0;
				this.fileListView.Sort();
			}
			this.fileListView.EndUpdate();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000063F0 File Offset: 0x000045F0
		private void archiveTreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			this.UpdateFileListView();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000063FC File Offset: 0x000045FC
		private void archiveTreeView_DoubleClick(object sender, EventArgs e)
		{
			int index = this.GetRootNode(this.archiveTreeView.SelectedNode).Index;
			bool flag = this.archiveTreeView.SelectedNode.Level == 0;
			if (flag)
			{
				bool flag2 = this.archiveTreeView.SelectedNode.Nodes.Count <= 0;
				if (flag2)
				{
					this.archiveTreeView.BeginUpdate();
					for (int i = 0; i < Globals.TOC.TOCMaps.Length; i++)
					{
						foreach (TOCMapEntry tocmapEntry in Globals.TOC.TOCMaps[i].TOCMapEntries)
						{
							bool flag3 = tocmapEntry.ArchiveIndex == index;
							if (flag3)
							{
								string text = tocmapEntry.FileName;
								text = text.Replace("\\", "/");
								string text2 = Path.GetDirectoryName(text);
								text2 = text2.Replace("\\", "/");
								this.CreateNodePath(this.archiveTreeView.Nodes, tocmapEntry.ArchiveName + "/" + text2);
							}
						}
					}
					this.archiveTreeView.TreeViewNodeSorter = new Sorter1();
					this.archiveTreeView.Sort();
					this.archiveTreeView.EndUpdate();
					this.archiveTreeView.SelectedNode = this.archiveTreeView.Nodes[index];
					this.archiveTreeView.SelectedNode.Expand();
					this.archiveTreeView.Focus();
				}
			}
			else
			{
				bool flag4 = this.archiveTreeView.SelectedNode.Level > 0;
				if (flag4)
				{
				}
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000065D4 File Offset: 0x000047D4
		private void fileListView_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			bool flag = this.lvcs == null;
			if (!flag)
			{
				bool flag2 = e.Column == this.lvcs.SortColumn;
				if (flag2)
				{
					bool flag3 = this.lvcs.Order == SortOrder.Ascending;
					if (flag3)
					{
						this.lvcs.Order = SortOrder.Descending;
					}
					else
					{
						this.lvcs.Order = SortOrder.Ascending;
					}
				}
				else
				{
					this.lvcs.SortColumn = e.Column;
					this.lvcs.Order = SortOrder.Ascending;
				}
				this.fileListView.Sort();
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000666C File Offset: 0x0000486C
		private TOCMapEntry GetTOCMapEntryFromFileListSelection()
		{
			int index = this.GetRootNode(this.archiveTreeView.SelectedNode).Index;
			bool flag = this.fileListView.SelectedItems.Count <= 0;
			TOCMapEntry result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ulong num = ulong.Parse(this.fileListView.SelectedItems[0].SubItems[3].Text);
				for (int i = 0; i < Globals.TOC.TOCMaps.Length; i++)
				{
					foreach (TOCMapEntry tocmapEntry in Globals.TOC.TOCMaps[i].TOCMapEntries)
					{
						bool flag2 = tocmapEntry.ArchiveIndex == index;
						if (flag2)
						{
							bool flag3 = tocmapEntry.FileAssetID == num;
							if (flag3)
							{
								return tocmapEntry;
							}
						}
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00006780 File Offset: 0x00004980
		private void DisplayAssetInfo(TOCMapEntry entry)
		{
			byte[] assetBytes = this.GetAssetBytes(entry);
			string text = "";
			text = text + "Asset Name: " + entry.FileName + "\n\n";
			text = text + "Asset ID: 0x" + entry.FileAssetID.ToString("X2") + "\n\n";
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(assetBytes));
			uint num = binaryReader.ReadUInt32();
			bool flag = Path.GetExtension(entry.FileName) == ".texture";
			if (flag)
			{
				text += "Asset Type: Texture\n\n";
				bool flag2 = num == 1548058809U;
				if (flag2)
				{
					binaryReader.BaseStream.Position = 88L;
					uint num2 = binaryReader.ReadUInt32();
					binaryReader.BaseStream.Position = 92L;
					ushort num3 = binaryReader.ReadUInt16();
					ushort num4 = binaryReader.ReadUInt16();
					text = string.Concat(new string[]
					{
						text,
						"Resolution: ",
						num3.ToString(),
						"x",
						num4.ToString(),
						"\n\n"
					});
					binaryReader.BaseStream.Position = 114L;
					text = text + "UNK Texture Type: " + binaryReader.ReadByte().ToString() + "\n\n";
				}
				else
				{
					text += "UNK This is the high resolution texture, another same named file will contain info and types\n\n";
				}
			}
			else
			{
				bool flag3 = num == 2082501152U;
				if (flag3)
				{
					text += "Asset Type: Actor\n\n";
					uint num5 = 0U;
					bool flag4 = false;
					while (num5 != 984745145U)
					{
						num5 = binaryReader.ReadUInt32();
						bool flag5 = binaryReader.BaseStream.Position >= 256L;
						if (flag5)
						{
							text += "Actor has no used assets section\n";
							flag4 = true;
							break;
						}
					}
					bool flag6 = !flag4;
					if (flag6)
					{
						text += "Assets:\n";
						int num6 = 36;
						long position = (long)num6 + (long)((ulong)binaryReader.ReadUInt32());
						uint num7 = binaryReader.ReadUInt32();
						binaryReader.BaseStream.Position = position;
						int num8 = 0;
						while ((long)num8 < (long)((ulong)(num7 / 16U)))
						{
							ulong num9 = binaryReader.ReadUInt64();
							long position2 = (long)num6 + (long)((ulong)binaryReader.ReadUInt32());
							long position3 = binaryReader.BaseStream.Position;
							binaryReader.BaseStream.Position = position2;
							string text2 = binaryReader.ReadNTString();
							binaryReader.BaseStream.Position = position3;
							uint num10 = binaryReader.ReadUInt32();
							text = string.Concat(new string[]
							{
								text,
								text2,
								",0x",
								num9.ToString("X2"),
								"\n"
							});
							num8++;
						}
					}
				}
				else
				{
					bool flag7 = num == 2559601567U;
					if (flag7)
					{
						text += "Asset Type: Model\n\n";
						uint num11 = 0U;
						bool flag8 = false;
						while (num11 != 844151680U)
						{
							num11 = binaryReader.ReadUInt32();
							bool flag9 = binaryReader.BaseStream.Position >= 256L;
							if (flag9)
							{
								text += "Model has no used materials section\n";
								flag8 = true;
								break;
							}
						}
						bool flag10 = !flag8;
						if (flag10)
						{
							text += "Materials:\n";
							int num12 = 36;
							long num13 = (long)num12 + (long)((ulong)binaryReader.ReadUInt32());
							uint num14 = binaryReader.ReadUInt32();
							binaryReader.BaseStream.Position = num13;
							List<string> list = new List<string>();
							List<ulong> list2 = new List<ulong>();
							while (binaryReader.BaseStream.Position < num13 + (long)((ulong)(num14 / 2U)))
							{
								ulong position4 = (ulong)((long)num12 + (long)binaryReader.ReadUInt64());
								long position5 = binaryReader.BaseStream.Position;
								binaryReader.BaseStream.Position = (long)position4;
								list.Add(binaryReader.ReadNTString());
								binaryReader.BaseStream.Position = position5;
							}
							while (binaryReader.BaseStream.Position < num13 + (long)((ulong)(num14 / 2U)) + (long)((ulong)(num14 / 2U)))
							{
								list2.Add(binaryReader.ReadUInt64());
							}
							for (int i = 0; i < list.Count; i++)
							{
								bool flag11 = !list[i].EndsWith(".material");
								if (!flag11)
								{
									text = string.Concat(new string[]
									{
										text,
										list[i],
										",0x",
										list2[i].ToString("X2"),
										"\n"
									});
								}
							}
						}
					}
					else
					{
						bool flag12 = num == 470085516U;
						if (flag12)
						{
							text += "Asset Type: Material\n\n";
							List<int> list3 = Utils.FindOffsetPattern(assetBytes, Encoding.UTF8.GetBytes(".texture"), 0, -1, -1, false);
							bool flag13 = list3.Count <= 0;
							if (flag13)
							{
								text += "Material has no used textures\n";
							}
							else
							{
								int num15 = list3[0];
								binaryReader.BaseStream.Position = (long)num15;
								for (;;)
								{
									Stream baseStream = binaryReader.BaseStream;
									long position6 = baseStream.Position;
									baseStream.Position = position6 - 1L;
									byte b = binaryReader.ReadByte();
									bool flag14 = !char.IsLetterOrDigit((char)b) && b != 95 && b != 47 && b != 92 && b != 46;
									if (flag14)
									{
										break;
									}
									binaryReader.BaseStream.Position -= 2L;
								}
								int num16 = (int)binaryReader.BaseStream.Position;
								text += "Textures:\n";
								for (;;)
								{
									byte b2 = binaryReader.ReadByte();
									bool flag15 = b2 == 0;
									if (flag15)
									{
										break;
									}
									binaryReader.BaseStream.Position -= 1L;
									string str = binaryReader.ReadNTString();
									text = text + str + "\n";
								}
							}
						}
						else
						{
							text = text + "Asset Type: 0x" + num.ToString("X2") + "\n\n";
						}
					}
				}
			}
			InformationForm informationForm = new InformationForm(text);
			informationForm.ShowDialog();
			binaryReader.Close();
			binaryReader.Dispose();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00006DA9 File Offset: 0x00004FA9
		private void fileListView_DoubleClick(object sender, EventArgs e)
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00006DAC File Offset: 0x00004FAC
		private void joinDiscordServerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("https://discord.gg/5mFxWWrSNH");
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00006DBA File Offset: 0x00004FBA
		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Spider-Man PC Tool by jedijosh920\nVersion: v" + Globals.Version, "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00006DDC File Offset: 0x00004FDC
		private void findToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = Globals.TOC == null;
			if (!flag)
			{
				bool flag2 = MainForm.searchFindForm != null;
				if (flag2)
				{
					MainForm.searchFindForm.Focus();
				}
				else
				{
					MainForm.searchFindForm = new SearchFindForm(this);
					MainForm.searchFindForm.Show();
				}
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00006E28 File Offset: 0x00005028
		private void ParsePaddingTest(string fileName)
		{
			BinaryReader binaryReader = new BinaryReader(File.OpenRead(fileName));
			uint num = binaryReader.ReadUInt32();
			List<MainForm.PaddingBlock> list = new List<MainForm.PaddingBlock>();
			bool flag = num == 1380012868U;
			if (flag)
			{
				MessageBox.Show("PADDING");
				binaryReader.BaseStream.Position = 12L;
				uint num2 = binaryReader.ReadUInt32();
				uint num3 = binaryReader.ReadUInt32();
				binaryReader.BaseStream.Position = 32L;
				while (binaryReader.BaseStream.Position < (long)((ulong)num2))
				{
					MainForm.PaddingBlock paddingBlock = new MainForm.PaddingBlock();
					uint decOffset = binaryReader.ReadUInt32();
					paddingBlock.DecOffset = decOffset;
					binaryReader.BaseStream.Position += 4L;
					uint offset = binaryReader.ReadUInt32();
					paddingBlock.Offset = offset;
					binaryReader.BaseStream.Position += 4L;
					uint decSize = binaryReader.ReadUInt32();
					paddingBlock.DecSize = decSize;
					uint comSize = binaryReader.ReadUInt32();
					paddingBlock.ComSize = comSize;
					binaryReader.BaseStream.Position += 8L;
					list.Add(paddingBlock);
				}
				MainForm.PaddingBlock paddingBlock2 = list[0];
				binaryReader.BaseStream.Position = (long)((ulong)paddingBlock2.Offset);
				byte[] bytes = binaryReader.ReadBytes((int)paddingBlock2.ComSize);
				File.WriteAllBytes("testPadBlockBytes.bin", bytes);
				new AssetDecompress2(File.ReadAllBytes("testPadBlockBytes.bin"), "testPadBlockBytes.dec", (int)paddingBlock2.DecSize, 0);
				binaryReader.Close();
				binaryReader.Dispose();
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00006FB0 File Offset: 0x000051B0
		private byte[] GetAssetBytes(TOCMapEntry entry)
		{
			BinaryReader binaryReader = new BinaryReader(File.OpenRead(Globals.AssetArchivesDirectory + entry.ArchiveName));
			uint num = binaryReader.ReadUInt32();
			List<MainForm.PaddingBlock> list = new List<MainForm.PaddingBlock>();
			bool flag = num == 1380012868U;
			byte[] result;
			if (flag)
			{
				binaryReader.BaseStream.Position = 12L;
				uint num2 = binaryReader.ReadUInt32();
				uint num3 = binaryReader.ReadUInt32();
				binaryReader.BaseStream.Position = 32L;
				while (binaryReader.BaseStream.Position < (long)((ulong)num2))
				{
					MainForm.PaddingBlock paddingBlock = new MainForm.PaddingBlock();
					paddingBlock.DecOffsetOffset = (uint)binaryReader.BaseStream.Position;
					uint decOffset = binaryReader.ReadUInt32();
					paddingBlock.DecOffset = decOffset;
					binaryReader.BaseStream.Position += 4L;
					paddingBlock.OffsetOffset = (uint)binaryReader.BaseStream.Position;
					uint offset = binaryReader.ReadUInt32();
					paddingBlock.Offset = offset;
					binaryReader.BaseStream.Position += 4L;
					paddingBlock.DecSizeOffset = (uint)binaryReader.BaseStream.Position;
					uint decSize = binaryReader.ReadUInt32();
					paddingBlock.DecSize = decSize;
					paddingBlock.ComSizeOffset = (uint)binaryReader.BaseStream.Position;
					uint comSize = binaryReader.ReadUInt32();
					paddingBlock.ComSize = comSize;
					binaryReader.BaseStream.Position += 8L;
					list.Add(paddingBlock);
				}
				MainForm.PaddingBlock paddingBlock2 = null;
				int num4 = 0;
				int num5 = 0;
				for (int i = 0; i < list.Count; i++)
				{
					bool flag2 = entry.FileOffset <= list[i].DecOffset;
					if (flag2)
					{
						int num6 = i - 1;
						bool flag3 = num6 < 0;
						if (flag3)
						{
							num6 = 0;
						}
						paddingBlock2 = list[num6];
						num4 = num6;
						break;
					}
				}
				for (int j = 0; j < list.Count; j++)
				{
					bool flag4 = (ulong)entry.FileOffset + (ulong)((long)entry.FileSize) <= (ulong)list[j].DecOffset;
					if (flag4)
					{
						int num7 = j - 1;
						bool flag5 = num7 < 0;
						if (flag5)
						{
							num7 = 0;
						}
						MainForm.PaddingBlock paddingBlock3 = list[num7];
						num5 = num7;
						break;
					}
				}
				for (int k = num4; k < num5 + 1; k++)
				{
					MainForm.PaddingBlock paddingBlock4 = list[k];
					binaryReader.BaseStream.Position = (long)((ulong)paddingBlock4.Offset);
					byte[] compressedBytes = binaryReader.ReadBytes((int)paddingBlock4.ComSize);
					new AssetDecompress2(compressedBytes, Globals.TemporaryDirectory + "paddingBlock_" + k.ToString() + ".bin", (int)paddingBlock4.DecSize, 0);
				}
				List<byte> list2 = new List<byte>();
				for (int l = num4; l < num5 + 1; l++)
				{
					BinaryReader binaryReader2 = new BinaryReader(File.OpenRead(Globals.TemporaryDirectory + "paddingBlock_" + l.ToString() + ".bin"));
					list2.AddRange(binaryReader2.ReadBytes((int)binaryReader2.BaseStream.Length).ToList<byte>());
					binaryReader2.Close();
					binaryReader2.Dispose();
				}
				File.WriteAllBytes(Globals.TemporaryDirectory + "paddingBlock.bin", list2.ToArray());
				BinaryReader binaryReader3 = new BinaryReader(File.OpenRead(Globals.TemporaryDirectory + "paddingBlock.bin"));
				binaryReader3.BaseStream.Position = (long)((ulong)(entry.FileOffset - paddingBlock2.DecOffset));
				byte[] array = binaryReader3.ReadBytes(entry.FileSize);
				binaryReader3.Close();
				binaryReader3.Dispose();
				binaryReader.Close();
				binaryReader.Dispose();
				result = array;
			}
			else
			{
				binaryReader.BaseStream.Position = (long)((ulong)entry.FileOffset);
				byte[] array2 = binaryReader.ReadBytes(entry.FileSize);
				binaryReader.Close();
				binaryReader.Dispose();
				result = array2;
			}
			return result;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000073A8 File Offset: 0x000055A8
		private void exportAssetToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TOCMapEntry tocmapEntryFromFileListSelection = this.GetTOCMapEntryFromFileListSelection();
			bool flag = tocmapEntryFromFileListSelection == null;
			if (!flag)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Title = "Extract Asset...";
				string fileName = tocmapEntryFromFileListSelection.FileName.Replace("\\", "_");
				saveFileDialog.FileName = fileName;
				saveFileDialog.RestoreDirectory = true;
				bool flag2 = saveFileDialog.ShowDialog() != DialogResult.OK;
				if (!flag2)
				{
					byte[] assetBytes = this.GetAssetBytes(tocmapEntryFromFileListSelection);
					File.WriteAllBytes(saveFileDialog.FileName, assetBytes);
					MessageBox.Show("Asset Extracted!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00007440 File Offset: 0x00005640
		private void ConvertPaddedAssetArchive(string fileName)
		{
			BinaryReader binaryReader = new BinaryReader(File.OpenRead(fileName));
			uint num = binaryReader.ReadUInt32();
			bool flag = num != 1380012868U;
			if (flag)
			{
				binaryReader.Close();
				binaryReader.Dispose();
			}
			else
			{
				MainForm.DisplayWaitForm(this, "Converting Asset Archive...");
				List<MainForm.PaddingBlock> list = new List<MainForm.PaddingBlock>();
				bool flag2 = num == 1380012868U;
				if (flag2)
				{
					binaryReader.BaseStream.Position = 12L;
					uint num2 = binaryReader.ReadUInt32();
					uint num3 = binaryReader.ReadUInt32();
					binaryReader.BaseStream.Position = 32L;
					while (binaryReader.BaseStream.Position < (long)((ulong)num2))
					{
						MainForm.PaddingBlock paddingBlock = new MainForm.PaddingBlock();
						paddingBlock.DecOffsetOffset = (uint)binaryReader.BaseStream.Position;
						uint decOffset = binaryReader.ReadUInt32();
						paddingBlock.DecOffset = decOffset;
						binaryReader.BaseStream.Position += 4L;
						paddingBlock.OffsetOffset = (uint)binaryReader.BaseStream.Position;
						uint offset = binaryReader.ReadUInt32();
						paddingBlock.Offset = offset;
						binaryReader.BaseStream.Position += 4L;
						paddingBlock.DecSizeOffset = (uint)binaryReader.BaseStream.Position;
						uint decSize = binaryReader.ReadUInt32();
						paddingBlock.DecSize = decSize;
						paddingBlock.ComSizeOffset = (uint)binaryReader.BaseStream.Position;
						uint comSize = binaryReader.ReadUInt32();
						paddingBlock.ComSize = comSize;
						binaryReader.BaseStream.Position += 8L;
						list.Add(paddingBlock);
					}
				}
				for (int i = 0; i < list.Count; i++)
				{
					MainForm.PaddingBlock paddingBlock2 = list[i];
					binaryReader.BaseStream.Position = (long)((ulong)paddingBlock2.Offset);
					byte[] compressedBytes = binaryReader.ReadBytes((int)paddingBlock2.ComSize);
					new AssetDecompress2(compressedBytes, Globals.TemporaryDirectory + "paddingBlock_" + i.ToString() + ".bin", (int)paddingBlock2.DecSize, 0);
					MainForm.SetWaitFormText(string.Concat(new string[]
					{
						Path.GetFileName(fileName),
						"\nPadding Block Dec: ",
						(i + 1).ToString(),
						"/",
						list.Count.ToString()
					}));
				}
				BinaryWriter binaryWriter = new BinaryWriter(new FileStream(Globals.TemporaryDirectory + Path.GetFileName(fileName), FileMode.Append, FileAccess.Write));
				for (int j = 0; j < list.Count; j++)
				{
					BinaryReader binaryReader2 = new BinaryReader(File.OpenRead(Globals.TemporaryDirectory + "paddingBlock_" + j.ToString() + ".bin"));
					binaryWriter.Write(binaryReader2.ReadBytes((int)binaryReader2.BaseStream.Length));
					binaryReader2.Close();
					binaryReader2.Dispose();
					Console.WriteLine("padding block append: " + j.ToString() + "/" + list.Count.ToString());
					MainForm.SetWaitFormText(string.Concat(new string[]
					{
						Path.GetFileName(fileName),
						"\nPadding Block Append: ",
						(j + 1).ToString(),
						"/",
						list.Count.ToString()
					}));
				}
				binaryWriter.Close();
				binaryWriter.Dispose();
				binaryReader.Close();
				binaryReader.Dispose();
				foreach (string path in Directory.GetFiles(Globals.TemporaryDirectory))
				{
					bool flag3 = Path.GetFileName(path).StartsWith("paddingBlock");
					if (flag3)
					{
						File.Delete(path);
					}
				}
				File.Delete(fileName);
				File.Move(Globals.TemporaryDirectory + Path.GetFileName(fileName), fileName);
				MainForm.RemoveWaitForm();
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00007834 File Offset: 0x00005A34
		private void replaceAssetToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TOCMapEntry tocmapEntryFromFileListSelection = this.GetTOCMapEntryFromFileListSelection();
			bool flag = tocmapEntryFromFileListSelection == null;
			if (!flag)
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Title = "Replace Asset...";
				string extension = Path.GetExtension(tocmapEntryFromFileListSelection.FileName);
				openFileDialog.Filter = "(*" + extension + ")| *" + extension;
				openFileDialog.RestoreDirectory = true;
				bool flag2 = openFileDialog.ShowDialog() != DialogResult.OK;
				if (!flag2)
				{
					for (int i = 0; i < Globals.ReplacedTOCMapEntries.Count; i++)
					{
						bool flag3 = Globals.ReplacedTOCMapEntries[i].ArchiveIndex == tocmapEntryFromFileListSelection.ArchiveIndex;
						if (flag3)
						{
							bool flag4 = Globals.ReplacedTOCMapEntries[i].FileAssetID == tocmapEntryFromFileListSelection.FileAssetID;
							if (flag4)
							{
								Globals.ReplacedTOCMapEntries.RemoveAt(i);
								Globals.ReplacedTOCMapEntriesReplaceFileNames.RemoveAt(i);
								break;
							}
						}
					}
					Globals.ReplacedTOCMapEntries.Add(tocmapEntryFromFileListSelection);
					Globals.ReplacedTOCMapEntriesReplaceFileNames.Add(openFileDialog.FileName);
					MessageBox.Show("Asset Replaced In Mod Queue!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000795C File Offset: 0x00005B5C
		private void assetInformationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TOCMapEntry tocmapEntryFromFileListSelection = this.GetTOCMapEntryFromFileListSelection();
			bool flag = tocmapEntryFromFileListSelection == null;
			if (!flag)
			{
				this.DisplayAssetInfo(tocmapEntryFromFileListSelection);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00007984 File Offset: 0x00005B84
		private void createModToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = Globals.TOC == null;
			if (!flag)
			{
				CreateModForm createModForm = new CreateModForm();
				createModForm.ShowDialog(this);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000079B0 File Offset: 0x00005BB0
		private void installModToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = Globals.TOC == null;
			if (!flag)
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = "SMPC Mod (*.smpcmod)|*.smpcmod";
				openFileDialog.RestoreDirectory = true;
				bool flag2 = openFileDialog.ShowDialog() != DialogResult.OK;
				if (!flag2)
				{
					Globals.ModInstallDirectory = Globals.TemporaryDirectory + "ModInstall\\";
					bool flag3 = Directory.Exists(Globals.ModInstallDirectory);
					if (flag3)
					{
						Directory.Delete(Globals.ModInstallDirectory, true);
					}
					Directory.CreateDirectory(Globals.ModInstallDirectory);
					ZipFile.ExtractToDirectory(openFileDialog.FileName, Globals.ModInstallDirectory);
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					string text = File.ReadAllText(Globals.ModInstallDirectory + "SMPCMod.info");
					string[] array = text.Split(new char[]
					{
						'\n'
					});
					for (int i = 0; i < array.Length; i++)
					{
						bool flag4 = array[i].StartsWith("//");
						if (!flag4)
						{
							bool flag5 = !array[i].Contains("=");
							if (!flag5)
							{
								bool flag6 = string.IsNullOrWhiteSpace(array[i]);
								if (!flag6)
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
					smpcmod.Title = dictionary["Title"];
					smpcmod.Author = dictionary["Author"];
					smpcmod.Description = dictionary["Description"];
					foreach (string text2 in Directory.GetFiles(Globals.ModInstallDirectory + "ModFiles\\"))
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
					InstallModForm installModForm = new InstallModForm(smpcmod);
					installModForm.ShowDialog(this);
					this.UpdateFileListView();
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00007BFC File Offset: 0x00005DFC
		private void getNamedHashFilesCountToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = Globals.TOC == null;
			if (!flag)
			{
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < Globals.TOC.TOCMaps.Length; i++)
				{
					foreach (TOCMapEntry tocmapEntry in Globals.TOC.TOCMaps[i].TOCMapEntries)
					{
						bool flag2 = !tocmapEntry.FileName.StartsWith("0x");
						if (flag2)
						{
							num++;
						}
						num2++;
					}
				}
				bool flag3 = num != num2;
				if (flag3)
				{
					MessageBox.Show(string.Concat(new string[]
					{
						"There are ",
						num.ToString(),
						"/",
						num2.ToString(),
						" named assets!\nGo find more hash names!!!"
					}), "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					MessageBox.Show(string.Concat(new string[]
					{
						"There are ",
						num.ToString(),
						"/",
						num2.ToString(),
						" named assets!\nHoly shit, you got em all?!"
					}), "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00007D50 File Offset: 0x00005F50
		private void convertPaddedAssetArchivesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = Globals.TOC == null;
			if (!flag)
			{
				string[] files = Directory.GetFiles(Globals.AssetArchivesDirectory);
				foreach (string text in files)
				{
					string fileName = Path.GetFileName(text);
					bool flag2 = fileName.StartsWith("g00s") && text != "dag" && text != "toc" && !text.EndsWith(".BAK");
					if (flag2)
					{
						this.ConvertPaddedAssetArchive(text);
					}
				}
				MessageBox.Show("Done Converting Asset Archives For Modding!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00007DFC File Offset: 0x00005FFC
		private void textureToolToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TextureToolForm textureToolForm = new TextureToolForm();
			textureToolForm.ShowDialog();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00007E18 File Offset: 0x00006018
		private void updateAssetHashestxtToolStripMenuItem_Click(object sender, EventArgs e)
		{
			List<string> list = new List<string>();
			string[] array = File.ReadAllLines("AssetHashes.txt");
			foreach (string text in array)
			{
				string[] array3 = text.Split(new char[]
				{
					','
				});
				string text2 = array3[0];
				ulong num = ulong.Parse(array3[1]);
				bool flag = false;
				foreach (string text3 in list)
				{
					string[] array4 = text3.Split(new char[]
					{
						','
					});
					string text4 = array4[0];
					ulong num2 = ulong.Parse(array4[1]);
					bool flag2 = num2 == num;
					if (flag2)
					{
						flag = true;
						break;
					}
				}
				bool flag3 = !flag;
				if (flag3)
				{
					list.Add(text);
				}
			}
			File.WriteAllLines("UpdatedAssetHashes.txt", list);
			MessageBox.Show("Done");
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00007F24 File Offset: 0x00006124
		private void modManagerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = Globals.TOC == null;
			if (!flag)
			{
				ModManagerForm modManagerForm = new ModManagerForm();
				modManagerForm.ShowDialog();
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00007F50 File Offset: 0x00006150
		private void extractFolderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = Globals.TOC == null;
			if (!flag)
			{
				bool flag2 = this.archiveTreeView.SelectedNode == null;
				if (!flag2)
				{
					SaveFileDialog saveFileDialog = new SaveFileDialog();
					saveFileDialog.Title = "Select Output Folder...";
					saveFileDialog.FileName = "[SELECT OUTPUT FOLDER]";
					saveFileDialog.RestoreDirectory = true;
					bool flag3 = saveFileDialog.ShowDialog() != DialogResult.OK;
					if (!flag3)
					{
						string str = saveFileDialog.FileName.Substring(0, saveFileDialog.FileName.LastIndexOf("\\")) + "\\";
						string fullPath = this.archiveTreeView.SelectedNode.FullPath;
						int index = this.GetRootNode(this.archiveTreeView.SelectedNode).Index;
						string text = fullPath.Substring(fullPath.IndexOf("\\") + 1) + "\\";
						MainForm.DisplayWaitForm(this, "Extracting " + text);
						for (int i = 0; i < Globals.TOC.TOCMaps.Length; i++)
						{
							for (int j = 0; j < Globals.TOC.TOCMaps[i].TOCMapEntries.Count; j++)
							{
								bool flag4 = Globals.TOC.TOCMaps[i].TOCMapEntries[j].ArchiveIndex == index;
								if (flag4)
								{
									TOCMapEntry tocmapEntry = Globals.TOC.TOCMaps[i].TOCMapEntries[j];
									string fileName = tocmapEntry.FileName;
									bool flag5 = fileName.StartsWith(text) || text == tocmapEntry.ArchiveName + "\\";
									if (flag5)
									{
										byte[] assetBytes = this.GetAssetBytes(tocmapEntry);
										string text2 = str + tocmapEntry.FileName.Replace("\\", "_");
										Console.WriteLine(text2);
										File.WriteAllBytes(text2, assetBytes);
									}
								}
							}
						}
						MainForm.RemoveWaitForm();
						MessageBox.Show("Successfully extracted folder!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00008176 File Offset: 0x00006376
		private void testToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000817C File Offset: 0x0000637C
		private void testToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			DAG dag = new DAG("dag");
			dag.DecompressDAG("dag.dec");
			dag.ParseDecompressedDAG();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000081A8 File Offset: 0x000063A8
		private SMPCMod ExtractSMPCMod(string fileName, string dir)
		{
			bool flag = Directory.Exists(dir);
			if (flag)
			{
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

		// Token: 0x0600006C RID: 108 RVA: 0x00008370 File Offset: 0x00006570
		private void launchGameWithModsToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			bool flag = Globals.TOC == null;
			if (!flag)
			{
				bool flag2 = Globals.ReplacedTOCMapEntries.Count <= 0;
				if (flag2)
				{
					MessageBox.Show(this, "Cannot launch game with no modified files!", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					MainForm.DisplayWaitForm(this, "Installing Temporary Mod");
					Globals.ModCreateDirectory = Globals.TemporaryDirectory + "ModCreate\\";
					bool flag3 = Directory.Exists(Globals.ModCreateDirectory);
					if (flag3)
					{
						Directory.Delete(Globals.ModCreateDirectory, true);
					}
					Directory.CreateDirectory(Globals.ModCreateDirectory);
					SMPCMod smpcmod = new SMPCMod();
					smpcmod.Title = "";
					smpcmod.Author = "";
					smpcmod.Description = "";
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
					bool flag4 = File.Exists(Globals.TemporaryDirectory + "temp.smpcmod");
					if (flag4)
					{
						File.Delete(Globals.TemporaryDirectory + "temp.smpcmod");
					}
					ZipFile.CreateFromDirectory(Globals.ModCreateDirectory, Globals.TemporaryDirectory + "temp.smpcmod", CompressionLevel.Fastest, false);
					bool flag5 = File.Exists(Globals.TemporaryDirectory + "tocTempBak");
					if (flag5)
					{
						File.Delete(Globals.TemporaryDirectory + "tocTempBak");
					}
					File.Copy(Globals.AssetArchivesDirectory + "toc", Globals.TemporaryDirectory + "tocTempBak");
					Globals.TOC = new TOC(Globals.AssetArchivesDirectory + "toc.BAK");
					Globals.TOC.Decompress(Globals.TemporaryDirectory + "toc.mod.dec");
					Globals.TOC.ParseDecompressed();
					SMPCMod smpcmod2 = this.ExtractSMPCMod(Globals.TemporaryDirectory + "temp.smpcmod", Globals.TemporaryDirectory + "InstallMods\\");
					string text2 = smpcmod2.Title.Replace(" ", "");
					text2 = text2.Replace("_", "");
					text2 = text2.Replace("/", "");
					text2 = text2.Replace("\\", "");
					int num = 0;
					bool flag6 = !Directory.Exists(Globals.AssetArchivesDirectory + "mods");
					if (flag6)
					{
						Directory.CreateDirectory(Globals.AssetArchivesDirectory + "mods");
					}
					string text3 = Globals.AssetArchivesDirectory + "mods\\tempMod";
					MainForm.SMPCModArchive smpcmodArchive = new MainForm.SMPCModArchive();
					smpcmodArchive.FileName = text3;
					smpcmodArchive.AssetArchiveName = "mods\\" + Path.GetFileName(smpcmodArchive.FileName);
					smpcmodArchive.AssetArchiveIndex = Globals.TOC.ArchiveFiles.Count + num;
					BinaryWriter binaryWriter = new BinaryWriter(new FileStream(text3, FileMode.Append, FileAccess.Write));
					foreach (ModFile modFile in smpcmod2.ModFiles)
					{
						MainForm.SMPCModArchiveFile item = default(MainForm.SMPCModArchiveFile);
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
					for (int j = 0; j < 72; j++)
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
					foreach (MainForm.SMPCModArchiveFile smpcmodArchiveFile in smpcmodArchive.SMPCModArchiveFiles)
					{
						for (int k = 0; k < Globals.TOC.TOCMaps.Length; k++)
						{
							for (int l = 0; l < Globals.TOC.TOCMaps[k].TOCMapEntries.Count; l++)
							{
								bool flag7 = Globals.TOC.TOCMaps[k].TOCMapEntries[l].ArchiveIndex == smpcmodArchiveFile.OriginalAssetArchiveIndex;
								if (flag7)
								{
									bool flag8 = Globals.TOC.TOCMaps[k].TOCMapEntries[l].FileAssetID == smpcmodArchiveFile.AssetHash;
									if (flag8)
									{
										binaryWriter.BaseStream.Position = (long)Globals.TOC.TOCMaps[k].TOCMapEntries[l].FileOffsetTOCOffset;
										binaryWriter.Write(smpcmodArchiveFile.Offset);
										binaryWriter.BaseStream.Position = (long)Globals.TOC.TOCMaps[k].TOCMapEntries[l].FileSizeTOCOffset;
										binaryWriter.Write(smpcmodArchiveFile.Size);
										binaryWriter.BaseStream.Position = (long)Globals.TOC.TOCMaps[k].TOCMapEntries[l].ArchiveIndexTOCOffset;
										binaryWriter.Write(smpcmodArchive.AssetArchiveIndex);
										break;
									}
								}
							}
						}
					}
					binaryWriter.Close();
					binaryWriter.Dispose();
					File.Delete(Globals.AssetArchivesDirectory + "toc");
					Globals.TOC.Compress(Globals.AssetArchivesDirectory + "toc");
					Globals.TOC = new TOC(Globals.AssetArchivesDirectory + "toc.BAK");
					Globals.TOC.Decompress(Globals.TemporaryDirectory + "toc.dec");
					Globals.TOC.ParseDecompressed();
					MainForm.RemoveWaitForm();
					bool flag9 = !File.Exists("gameExe.txt");
					if (flag9)
					{
						OpenFileDialog openFileDialog = new OpenFileDialog();
						openFileDialog.Title = "Spider-Man Exe";
						openFileDialog.Filter = "Spider-Man Exe (*.exe)|*.exe";
						openFileDialog.RestoreDirectory = true;
						bool flag10 = openFileDialog.ShowDialog() != DialogResult.OK;
						if (flag10)
						{
							return;
						}
						File.WriteAllText("gameExe.txt", openFileDialog.FileName);
					}
					string fileName = File.ReadAllText("gameExe.txt");
					Process process = Utils.RunProcessWithArgs(fileName, "");
					while (Process.GetProcessesByName("Spider-Man").Length == 0)
					{
						Thread.Sleep(10);
					}
					MessageBox.Show("Modded Game Launch Session Started...", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					while (Process.GetProcessesByName("Spider-Man").Length != 0)
					{
						Thread.Sleep(10);
					}
					Thread.Sleep(3000);
					File.Delete(Globals.AssetArchivesDirectory + "toc");
					File.Copy(Globals.TemporaryDirectory + "tocTempBak", Globals.AssetArchivesDirectory + "toc");
					File.Delete(Globals.AssetArchivesDirectory + "mods\\tempMod");
					MessageBox.Show("Modded Game Launch Session Complete!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00008CC4 File Offset: 0x00006EC4
		private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = Globals.TOC == null;
			if (!flag)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Title = "Save Project";
				saveFileDialog.Filter = "SMPC Project (*.smpcprj)|*.smpcprj";
				saveFileDialog.RestoreDirectory = true;
				bool flag2 = File.Exists("projectsDir.txt");
				if (flag2)
				{
					saveFileDialog.InitialDirectory = File.ReadAllText("projectsDir.txt");
				}
				bool flag3 = saveFileDialog.ShowDialog() != DialogResult.OK;
				if (!flag3)
				{
					File.WriteAllText("projectsDir.txt", Path.GetDirectoryName(saveFileDialog.FileName));
					bool flag4 = Directory.Exists(Globals.TemporaryDirectory + "ProjectSave");
					if (flag4)
					{
						Directory.Delete(Globals.TemporaryDirectory + "ProjectSave", true);
					}
					bool flag5 = File.Exists(saveFileDialog.FileName);
					if (flag5)
					{
						File.Delete(saveFileDialog.FileName);
					}
					bool flag6 = !Directory.Exists(Globals.TemporaryDirectory + "ProjectSave");
					if (flag6)
					{
						Directory.CreateDirectory(Globals.TemporaryDirectory + "ProjectSave");
					}
					bool flag7 = !Directory.Exists(Globals.TemporaryDirectory + "ProjectSave\\ReplaceFiles");
					if (flag7)
					{
						Directory.CreateDirectory(Globals.TemporaryDirectory + "ProjectSave\\ReplaceFiles");
					}
					string text = "";
					text = text + "Title=" + Globals.ModTitle + "\n";
					text = text + "Author=" + Globals.ModAuthor + "\n";
					text = text + "Description=" + Globals.ModDescription + "\n";
					File.WriteAllText(Globals.TemporaryDirectory + "ProjectSave\\SMPCProject.info", text);
					for (int i = 0; i < Globals.ReplacedTOCMapEntries.Count; i++)
					{
						TOCMapEntry tocmapEntry = Globals.ReplacedTOCMapEntries[i];
						string str = tocmapEntry.ArchiveIndex.ToString() + "_" + tocmapEntry.FileAssetID.ToString("X2");
						File.Copy(Globals.ReplacedTOCMapEntriesReplaceFileNames[i], Globals.TemporaryDirectory + "ProjectSave\\ReplaceFiles\\" + str);
					}
					ZipFile.CreateFromDirectory(Globals.TemporaryDirectory + "ProjectSave", saveFileDialog.FileName, CompressionLevel.Fastest, false);
					this.UpdateTitle(" - Project: " + Path.GetFileNameWithoutExtension(saveFileDialog.FileName));
					MessageBox.Show("Project Saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00008F38 File Offset: 0x00007138
		private void loadProjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = Globals.TOC == null;
			if (!flag)
			{
				Globals.ReplacedTOCMapEntries.Clear();
				Globals.ReplacedTOCMapEntriesReplaceFileNames.Clear();
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Title = "Load Project";
				openFileDialog.Filter = "SMPC Project (*.smpcprj)|*.smpcprj";
				openFileDialog.RestoreDirectory = true;
				bool flag2 = File.Exists("projectsDir.txt");
				if (flag2)
				{
					openFileDialog.InitialDirectory = File.ReadAllText("projectsDir.txt");
				}
				bool flag3 = openFileDialog.ShowDialog() != DialogResult.OK;
				if (!flag3)
				{
					File.WriteAllText("projectsDir.txt", Path.GetDirectoryName(openFileDialog.FileName));
					bool flag4 = Directory.Exists(Globals.TemporaryDirectory + "ProjectLoad");
					if (flag4)
					{
						Directory.Delete(Globals.TemporaryDirectory + "ProjectLoad", true);
					}
					bool flag5 = !Directory.Exists(Globals.TemporaryDirectory + "ProjectLoad");
					if (flag5)
					{
						Directory.CreateDirectory(Globals.TemporaryDirectory + "ProjectLoad");
					}
					Directory.CreateDirectory(Globals.TemporaryDirectory + "ProjectLoad");
					ZipFile.ExtractToDirectory(openFileDialog.FileName, Globals.TemporaryDirectory + "ProjectLoad");
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					string text = File.ReadAllText(Globals.TemporaryDirectory + "ProjectLoad\\SMPCProject.info");
					string[] array = text.Split(new char[]
					{
						'\n'
					});
					for (int i = 0; i < array.Length; i++)
					{
						bool flag6 = array[i].StartsWith("//");
						if (!flag6)
						{
							bool flag7 = !array[i].Contains("=");
							if (!flag7)
							{
								bool flag8 = string.IsNullOrWhiteSpace(array[i]);
								if (!flag8)
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
					Globals.ModTitle = dictionary["Title"];
					Globals.ModAuthor = dictionary["Author"];
					Globals.ModDescription = dictionary["Description"];
					foreach (string text2 in Directory.GetFiles(Globals.TemporaryDirectory + "ProjectLoad\\ReplaceFiles"))
					{
						int num = int.Parse(Path.GetFileNameWithoutExtension(text2).Split(new char[]
						{
							'_'
						})[0]);
						ulong num2 = ulong.Parse(Path.GetFileNameWithoutExtension(text2).Split(new char[]
						{
							'_'
						})[1], NumberStyles.HexNumber);
						TOCMapEntry tocmapEntry = null;
						int num3 = 0;
						while (num3 < Globals.TOC.TOCMaps.Length && tocmapEntry == null)
						{
							foreach (TOCMapEntry tocmapEntry2 in Globals.TOC.TOCMaps[num3].TOCMapEntries)
							{
								bool flag9 = tocmapEntry2.ArchiveIndex == num;
								if (flag9)
								{
									bool flag10 = tocmapEntry2.FileAssetID == num2;
									if (flag10)
									{
										tocmapEntry = tocmapEntry2;
										break;
									}
								}
							}
							num3++;
						}
						Globals.ReplacedTOCMapEntries.Add(tocmapEntry);
						Globals.ReplacedTOCMapEntriesReplaceFileNames.Add(text2);
					}
					this.UpdateTitle(" - Project: " + Path.GetFileNameWithoutExtension(openFileDialog.FileName));
					MessageBox.Show("Project Loaded!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}

		// Token: 0x0400003E RID: 62
		private ListViewColumnSorter lvcs;

		// Token: 0x0400003F RID: 63
		public static WaitForm WaitForm;

		// Token: 0x04000040 RID: 64
		public static Form WaitFormReturnForm;

		// Token: 0x04000041 RID: 65
		private TreeNode findTreeNodeWithPathFound = null;

		// Token: 0x04000042 RID: 66
		public static SearchFindForm searchFindForm;

		// Token: 0x02000023 RID: 35
		public class PaddingBlock
		{
			// Token: 0x040000C3 RID: 195
			public uint DecOffset;

			// Token: 0x040000C4 RID: 196
			public uint DecOffsetOffset;

			// Token: 0x040000C5 RID: 197
			public uint Offset;

			// Token: 0x040000C6 RID: 198
			public uint OffsetOffset;

			// Token: 0x040000C7 RID: 199
			public uint DecSize;

			// Token: 0x040000C8 RID: 200
			public uint DecSizeOffset;

			// Token: 0x040000C9 RID: 201
			public uint ComSize;

			// Token: 0x040000CA RID: 202
			public uint ComSizeOffset;
		}

		// Token: 0x02000024 RID: 36
		public struct SMPCModArchiveFile
		{
			// Token: 0x040000CB RID: 203
			public int OriginalAssetArchiveIndex;

			// Token: 0x040000CC RID: 204
			public ulong AssetHash;

			// Token: 0x040000CD RID: 205
			public int Offset;

			// Token: 0x040000CE RID: 206
			public int Size;
		}

		// Token: 0x02000025 RID: 37
		public class SMPCModArchive
		{
			// Token: 0x040000CF RID: 207
			public string AssetArchiveName;

			// Token: 0x040000D0 RID: 208
			public string FileName;

			// Token: 0x040000D1 RID: 209
			public int AssetArchiveIndex;

			// Token: 0x040000D2 RID: 210
			public List<MainForm.SMPCModArchiveFile> SMPCModArchiveFiles = new List<MainForm.SMPCModArchiveFile>();
		}
	}
}
