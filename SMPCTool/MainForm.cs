using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SMPCTool
{
	// Token: 0x02000009 RID: 9
	public partial class MainForm : Form
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00004764 File Offset: 0x00002964
		public MainForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00004784 File Offset: 0x00002984
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

		// Token: 0x06000029 RID: 41 RVA: 0x0000481C File Offset: 0x00002A1C
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

		// Token: 0x0600002A RID: 42 RVA: 0x000048C0 File Offset: 0x00002AC0
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

		// Token: 0x0600002B RID: 43 RVA: 0x00004B2C File Offset: 0x00002D2C
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

		// Token: 0x0600002C RID: 44 RVA: 0x00004C48 File Offset: 0x00002E48
		private static void SwapModTOC(ulong originalAssetID, ulong swapAssetID)
		{
			TOCMapEntry tocmapEntryByAssetID = MainForm.GetTOCMapEntryByAssetID(originalAssetID);
			TOCMapEntry tocmapEntryByAssetID2 = MainForm.GetTOCMapEntryByAssetID(swapAssetID);
			MainForm.SwapModTOC(tocmapEntryByAssetID, tocmapEntryByAssetID2);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00004C6C File Offset: 0x00002E6C
		private static void SwapModTOCSpecificArchiveIndex(ulong originalAssetID, ulong swapAssetID, int originalArchiveIndex, int swapArchiveIndex)
		{
			TOCMapEntry tocmapEntryByAssetIDSpecificArchiveIndex = MainForm.GetTOCMapEntryByAssetIDSpecificArchiveIndex(originalAssetID, originalArchiveIndex);
			TOCMapEntry tocmapEntryByAssetIDSpecificArchiveIndex2 = MainForm.GetTOCMapEntryByAssetIDSpecificArchiveIndex(swapAssetID, swapArchiveIndex);
			MainForm.SwapModTOC(tocmapEntryByAssetIDSpecificArchiveIndex, tocmapEntryByAssetIDSpecificArchiveIndex2);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00004C92 File Offset: 0x00002E92
		private void InitLVCS()
		{
			this.lvcs = new ListViewColumnSorter();
			this.fileListView.ListViewItemSorter = this.lvcs;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00004CB2 File Offset: 0x00002EB2
		private void DisposeLVCS()
		{
			this.lvcs = null;
			this.fileListView.ListViewItemSorter = null;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00004CC9 File Offset: 0x00002EC9
		private void SpiderRatTOC()
		{
			this.SwapModTOC("characters\\hero\\hero_spiderman\\hero_spiderman_body.model", "characters\\ambient\\amb_rat\\amb_rat.model", -1, -1);
			this.SwapModTOC("characters\\hero\\hero_spiderman_classic\\hero_spiderman_classic.model", "characters\\ambient\\amb_rat\\amb_rat.model", -1, -1);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00004CF4 File Offset: 0x00002EF4
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

		// Token: 0x06000032 RID: 50 RVA: 0x00004F2C File Offset: 0x0000312C
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

		// Token: 0x06000033 RID: 51 RVA: 0x0000505C File Offset: 0x0000325C
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

		// Token: 0x06000034 RID: 52 RVA: 0x000050A8 File Offset: 0x000032A8
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

		// Token: 0x06000035 RID: 53 RVA: 0x00005124 File Offset: 0x00003324
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

		// Token: 0x06000036 RID: 54 RVA: 0x00005164 File Offset: 0x00003364
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

		// Token: 0x06000037 RID: 55 RVA: 0x000051C4 File Offset: 0x000033C4
		private void MainForm_Load(object sender, EventArgs e)
		{
			this.MinimumSize = base.Size;
			this.Text = this.Text + " v" + Globals.Version;
			Globals.TemporaryDirectory = "temp\\";
			bool flag = !Directory.Exists(Globals.TemporaryDirectory);
			if (flag)
			{
				DirectoryInfo directoryInfo = Directory.CreateDirectory(Globals.TemporaryDirectory);
				directoryInfo.Attributes |= FileAttributes.Hidden;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00005234 File Offset: 0x00003434
		private void TestTocLayout()
		{
			TOC toc = new TOC("toc");
			toc.Decompress("toc.dec");
			toc.ParseDecompressed();
			toc.GenerateCSV("layout.csv");
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000526C File Offset: 0x0000346C
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
					string text = str + "\\toc";
					bool flag4 = !File.Exists(text);
					if (flag4)
					{
						MessageBox.Show("TOC (Table of Contents) file is missing!", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else
					{
						Globals.AssetArchivesDirectory = str + "\\";
						File.WriteAllText("assetArchiveDir.txt", Globals.AssetArchivesDirectory);
						MessageBox.Show("Asset Archives Successfully Set!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						Globals.TOC = new TOC(text);
						Globals.TOC.Decompress(Globals.TemporaryDirectory + "toc.dec");
						Globals.TOC.ParseDecompressed();
						this.archiveTreeView.BeginUpdate();
						for (int i = 0; i < Globals.TOC.ArchiveFiles.Count; i++)
						{
							this.archiveTreeView.Nodes.Add(Globals.TOC.ArchiveFiles[i].Filename);
						}
						this.archiveTreeView.EndUpdate();
						BinaryReader binaryReader = new BinaryReader(File.OpenRead(Globals.AssetArchivesDirectory + "g00s000"));
						uint num = binaryReader.ReadUInt32();
						bool flag5 = num == 1380012868U;
						if (flag5)
						{
							MessageBox.Show("WARNING: Asset Archives detected as PADDED, which is not suitable for modding. Please use Tools -> Convert Asset Archives to use this tool properly. This will take 15-20 minutes, and is a one time thing. Make sure to backup files when modding!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						}
						binaryReader.Close();
						binaryReader.Dispose();
					}
				}
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000545C File Offset: 0x0000365C
		private void hashGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			StringToHashForm stringToHashForm = new StringToHashForm();
			stringToHashForm.ShowDialog();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00005478 File Offset: 0x00003678
		private void generateLayoutCSVToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = Globals.TOC == null;
			if (!flag)
			{
				Globals.TOC.GenerateCSV("layout.csv");
				MessageBox.Show("Generated layout.csv", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000054B7 File Offset: 0x000036B7
		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000054C0 File Offset: 0x000036C0
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

		// Token: 0x0600003E RID: 62 RVA: 0x000055B8 File Offset: 0x000037B8
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

		// Token: 0x0600003F RID: 63 RVA: 0x00005608 File Offset: 0x00003808
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

		// Token: 0x06000040 RID: 64 RVA: 0x0000565C File Offset: 0x0000385C
		private TreeNode GetRootNode(TreeNode node)
		{
			TreeNode treeNode = node;
			while (treeNode.Parent != null)
			{
				treeNode = treeNode.Parent;
			}
			return treeNode;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000568C File Offset: 0x0000388C
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

		// Token: 0x06000042 RID: 66 RVA: 0x0000573C File Offset: 0x0000393C
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

		// Token: 0x06000043 RID: 67 RVA: 0x000058C8 File Offset: 0x00003AC8
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

		// Token: 0x06000044 RID: 68 RVA: 0x00005C34 File Offset: 0x00003E34
		private void archiveTreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			this.UpdateFileListView();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00005C40 File Offset: 0x00003E40
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

		// Token: 0x06000046 RID: 70 RVA: 0x00005E18 File Offset: 0x00004018
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

		// Token: 0x06000047 RID: 71 RVA: 0x00005EB0 File Offset: 0x000040B0
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

		// Token: 0x06000048 RID: 72 RVA: 0x00005FC4 File Offset: 0x000041C4
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
					binaryReader.BaseStream.Position = 92L;
					ushort num2 = binaryReader.ReadUInt16();
					ushort num3 = binaryReader.ReadUInt16();
					text = string.Concat(new string[]
					{
						text,
						"Resolution: ",
						num2.ToString(),
						"x",
						num3.ToString(),
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
				}
				else
				{
					bool flag4 = num == 2559601567U;
					if (flag4)
					{
						text += "Asset Type: Model\n\n";
					}
					else
					{
						text = text + "Asset Type: 0x" + num.ToString("X2") + "\n\n";
					}
				}
			}
			MessageBox.Show(text, "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			binaryReader.Close();
			binaryReader.Dispose();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000617E File Offset: 0x0000437E
		private void fileListView_DoubleClick(object sender, EventArgs e)
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00006181 File Offset: 0x00004381
		private void joinDiscordServerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("https://discord.gg/5mFxWWrSNH");
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000618F File Offset: 0x0000438F
		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Spider-Man PC Tool by jedijosh920\nVersion: v" + Globals.Version, "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000061B0 File Offset: 0x000043B0
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

		// Token: 0x0600004D RID: 77 RVA: 0x000061FC File Offset: 0x000043FC
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

		// Token: 0x0600004E RID: 78 RVA: 0x00006384 File Offset: 0x00004584
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

		// Token: 0x0600004F RID: 79 RVA: 0x0000677C File Offset: 0x0000497C
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

		// Token: 0x06000050 RID: 80 RVA: 0x00006814 File Offset: 0x00004A14
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

		// Token: 0x06000051 RID: 81 RVA: 0x00006C08 File Offset: 0x00004E08
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
					BinaryReader binaryReader = new BinaryReader(File.OpenRead(openFileDialog.FileName));
					int fileSize = (int)binaryReader.BaseStream.Length;
					for (int j = 0; j < Globals.TOC.TOCMaps.Length; j++)
					{
						for (int k = 0; k < Globals.TOC.TOCMaps[j].TOCMapEntries.Count; k++)
						{
							bool flag5 = Globals.TOC.TOCMaps[j].TOCMapEntries[k].ArchiveIndex == tocmapEntryFromFileListSelection.ArchiveIndex;
							if (flag5)
							{
								bool flag6 = Globals.TOC.TOCMaps[j].TOCMapEntries[k].FileAssetID == tocmapEntryFromFileListSelection.FileAssetID;
								if (flag6)
								{
									Globals.TOC.TOCMaps[j].TOCMapEntries[k].FileSize = fileSize;
									break;
								}
							}
						}
					}
					binaryReader.Close();
					binaryReader.Dispose();
					this.UpdateFileListView();
					MessageBox.Show("Asset Replaced", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00006E40 File Offset: 0x00005040
		private void assetInformationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TOCMapEntry tocmapEntryFromFileListSelection = this.GetTOCMapEntryFromFileListSelection();
			bool flag = tocmapEntryFromFileListSelection == null;
			if (!flag)
			{
				this.DisplayAssetInfo(tocmapEntryFromFileListSelection);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00006E68 File Offset: 0x00005068
		private void createModToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag = Globals.TOC == null;
			if (!flag)
			{
				CreateModForm createModForm = new CreateModForm();
				createModForm.ShowDialog(this);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00006E94 File Offset: 0x00005094
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

		// Token: 0x06000055 RID: 85 RVA: 0x000070E0 File Offset: 0x000052E0
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

		// Token: 0x06000056 RID: 86 RVA: 0x00007234 File Offset: 0x00005434
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

		// Token: 0x04000031 RID: 49
		private ListViewColumnSorter lvcs;

		// Token: 0x04000032 RID: 50
		public static WaitForm WaitForm;

		// Token: 0x04000033 RID: 51
		public static Form WaitFormReturnForm;

		// Token: 0x04000034 RID: 52
		private TreeNode findTreeNodeWithPathFound = null;

		// Token: 0x04000035 RID: 53
		public static SearchFindForm searchFindForm;

		// Token: 0x0200001C RID: 28
		public class PaddingBlock
		{
			// Token: 0x0400008F RID: 143
			public uint DecOffset;

			// Token: 0x04000090 RID: 144
			public uint DecOffsetOffset;

			// Token: 0x04000091 RID: 145
			public uint Offset;

			// Token: 0x04000092 RID: 146
			public uint OffsetOffset;

			// Token: 0x04000093 RID: 147
			public uint DecSize;

			// Token: 0x04000094 RID: 148
			public uint DecSizeOffset;

			// Token: 0x04000095 RID: 149
			public uint ComSize;

			// Token: 0x04000096 RID: 150
			public uint ComSizeOffset;
		}
	}
}
