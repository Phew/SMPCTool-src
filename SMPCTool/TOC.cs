using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ionic.Zlib;

namespace SMPCTool
{
	// Token: 0x0200001C RID: 28
	public class TOC
	{
		// Token: 0x0600009A RID: 154 RVA: 0x0000D70C File Offset: 0x0000B90C
		public TOC(string fileName)
		{
			this.Filename = fileName;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000D728 File Offset: 0x0000B928
		public void Compress(string fileName)
		{
			BinaryReader binaryReader = new BinaryReader(File.OpenRead(this.decompressedFileName));
			List<byte> list = ZlibStream.CompressBuffer(binaryReader.ReadBytes((int)binaryReader.BaseStream.Length)).ToList<byte>();
			list.InsertRange(0, BitConverter.GetBytes((int)binaryReader.BaseStream.Length));
			list.InsertRange(0, new byte[]
			{
				175,
				18,
				175,
				119
			});
			File.WriteAllBytes(fileName, list.ToArray());
			binaryReader.Close();
			binaryReader.Dispose();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000D7B4 File Offset: 0x0000B9B4
		public void Decompress(string fileName)
		{
			Console.WriteLine("Decompressing TOC...");
			BinaryReader binaryReader = new BinaryReader(File.OpenRead(this.Filename));
			uint num = binaryReader.ReadUInt32();
			int count = binaryReader.ReadInt32();
			byte[] bytes = ZlibStream.UncompressBuffer(binaryReader.ReadBytes(count));
			binaryReader.Close();
			binaryReader.Dispose();
			File.WriteAllBytes(fileName, bytes);
			this.decompressedFileName = fileName;
			Console.WriteLine("Done decompressing TOC!");
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000D824 File Offset: 0x0000BA24
		public void ParseDecompressed()
		{
			bool flag = !File.Exists(this.decompressedFileName);
			if (flag)
			{
				Console.WriteLine("ERROR: Trying to access non-existing decompressed TOC!!!");
			}
			else
			{
				Console.WriteLine("Parsing decompressed TOC...");
				BinaryReader binaryReader = new BinaryReader(File.OpenRead(this.decompressedFileName));
				string text = "";
				int num = binaryReader.ReadInt32();
				int num2 = binaryReader.ReadInt32();
				int num3 = binaryReader.ReadInt32();
				int num4 = binaryReader.ReadInt32();
				text += "TOC\n{\n\t";
				text = text + "Filename: " + this.Filename + "\n\n\t";
				text += "Header\n\t{\n\t\t";
				text = text + "Magic: 0x" + num.ToString("X2") + "\n\t\t";
				text = text + "Hash: 0x" + num2.ToString("X2") + "\n\t\t";
				text = text + "Length: 0x" + num3.ToString("X2") + "\n\t\t";
				text = text + "Number of Sections: " + num4.ToString();
				text += "\n\t}\n}\n\n";
				int hash = binaryReader.ReadInt32();
				this.ArchiveFileSectionOffsetOffset = (int)binaryReader.BaseStream.Position;
				int num5 = binaryReader.ReadInt32();
				this.ArchiveFileSectionLenOffset = (int)binaryReader.BaseStream.Position;
				int num6 = binaryReader.ReadInt32();
				this.ArchiveFileSectionOffset = num5;
				this.ArchiveFileSectionLen = num6;
				this.Sections.Add(new Section
				{
					Hash = hash,
					Offset = num5,
					Size = num6
				});
				this.ParseArchiveFiles(num5, num6);
				text += "ArchiveFiles\n{\n\t";
				text += "Header\n\t{\n\t\t";
				text = text + "Hash: 0x" + hash.ToString("X2") + "\n\t\t";
				text = text + "Offset: 0x" + num5.ToString("X2") + "\n\t\t";
				text = text + "Length: 0x" + num6.ToString("X2") + "\n\t\t";
				text = text + "Count: " + this.ArchiveFiles.Count.ToString() + "\n";
				text += "\t}\n\n\t";
				text += "ArchiveFileEntries\n\t{\n";
				foreach (ArchiveFile archiveFile in this.ArchiveFiles)
				{
					text += "\t\tArchiveFileEntry\n\t\t{\n\t\t\t";
					string str = text;
					string str2 = "InstallBucket: ";
					byte installBucket = archiveFile.InstallBucket;
					text = str + str2 + installBucket.ToString() + "\n\t\t\t";
					string str3 = text;
					string str4 = "Chunkmap: ";
					uint chunkmap = archiveFile.Chunkmap;
					text = str3 + str4 + chunkmap.ToString() + "\n\t\t\t";
					text = text + "Filename: " + archiveFile.Filename + "\n\t\t\t";
					text += "\n\t\t}\n\n";
				}
				text += "\t}\n}\n\n";
				int hash2 = binaryReader.ReadInt32();
				int offset = binaryReader.ReadInt32();
				int num7 = binaryReader.ReadInt32();
				this.Sections.Add(new Section
				{
					Hash = hash2,
					Offset = offset,
					Size = num7
				});
				this.ParseAssetIDs(offset, num7);
				text += "AssetIDs\n{\n\t";
				text += "Header\n\t{\n\t\t";
				text = text + "Hash: 0x" + hash2.ToString("X2") + "\n\t\t";
				text = text + "Offset: 0x" + offset.ToString("X2") + "\n\t\t";
				text = text + "Length: 0x" + num7.ToString("X2") + "\n\t\t";
				text = text + "Count: " + this.AssetIDs.Count.ToString() + "\n";
				text += "\t}\n}\n\n";
				int hash3 = binaryReader.ReadInt32();
				int offset2 = binaryReader.ReadInt32();
				int num8 = binaryReader.ReadInt32();
				this.Sections.Add(new Section
				{
					Hash = hash3,
					Offset = offset2,
					Size = num8
				});
				this.ParseSizeEntries(offset2, num8);
				text += "SizeEntries\n{\n\t";
				text += "Header\n\t{\n\t\t";
				text = text + "Hash: 0x" + hash3.ToString("X2") + "\n\t\t";
				text = text + "Offset: 0x" + offset2.ToString("X2") + "\n\t\t";
				text = text + "Length: 0x" + num8.ToString("X2") + "\n\t\t";
				text = text + "Count: " + this.SizeEntries.Count.ToString() + "\n";
				text += "\t}\n}\n\n";
				int hash4 = binaryReader.ReadInt32();
				int offset3 = binaryReader.ReadInt32();
				int num9 = binaryReader.ReadInt32();
				this.Sections.Add(new Section
				{
					Hash = hash4,
					Offset = offset3,
					Size = num9
				});
				this.ParseKeyAssets(offset3, num9);
				text += "KeyAssets\n{\n\t";
				text += "Header\n\t{\n\t\t";
				text = text + "Hash: 0x" + hash4.ToString("X2") + "\n\t\t";
				text = text + "Offset: 0x" + offset3.ToString("X2") + "\n\t\t";
				text = text + "Length: 0x" + num9.ToString("X2") + "\n\t\t";
				text = text + "Count: " + this.KeyAssets.Count.ToString() + "\n";
				text += "\t}\n}\n\n";
				int hash5 = binaryReader.ReadInt32();
				int offset4 = binaryReader.ReadInt32();
				int num10 = binaryReader.ReadInt32();
				this.Sections.Add(new Section
				{
					Hash = hash5,
					Offset = offset4,
					Size = num10
				});
				this.ParseOffsetEntries(offset4, num10);
				text += "OffsetEntries\n{\n\t";
				text += "Header\n\t{\n\t\t";
				text = text + "Hash: 0x" + hash5.ToString("X2") + "\n\t\t";
				text = text + "Offset: 0x" + offset4.ToString("X2") + "\n\t\t";
				text = text + "Length: 0x" + num10.ToString("X2") + "\n\t\t";
				text = text + "Count: " + this.OffsetEntries.Count.ToString() + "\n";
				text += "\t}\n}\n\n";
				int hash6 = binaryReader.ReadInt32();
				int offset5 = binaryReader.ReadInt32();
				int num11 = binaryReader.ReadInt32();
				this.Sections.Add(new Section
				{
					Hash = hash6,
					Offset = offset5,
					Size = num11
				});
				this.ParseSpansEntries(offset5, num11);
				text += "SpansEntries\n{\n\t";
				text += "Header\n\t{\n\t\t";
				text = text + "Hash: 0x" + hash6.ToString("X2") + "\n\t\t";
				text = text + "Offset: 0x" + offset5.ToString("X2") + "\n\t\t";
				text = text + "Length: 0x" + num11.ToString("X2") + "\n\t\t";
				text = text + "Count: " + this.SpansEntries.Count.ToString() + "\n";
				text += "\t}\n}\n\n";
				string[] array = File.ReadAllLines("AssetHashes.txt");
				Dictionary<ulong, string> dictionary = new Dictionary<ulong, string>();
				foreach (string text2 in array)
				{
					string[] array3 = text2.Split(new char[]
					{
						','
					});
					string text3 = array3[0];
					ulong key = Convert.ToUInt64(array3[1]);
					string value = array3[0];
					bool flag2 = !dictionary.ContainsKey(key);
					if (flag2)
					{
						dictionary.Add(key, value);
					}
				}
				uint[] array4 = new uint[this.SizeEntries.Count];
				for (int j = 0; j < this.SizeEntries.Count; j++)
				{
					array4[j] = this.SizeEntries[j].FileCtr;
				}
				this.TOCMaps = new TOCMap[this.ArchiveFiles.Count];
				for (int k = 0; k < this.SizeEntries.Count; k++)
				{
					int archiveIndex = this.OffsetEntries[(int)array4[k]].ArchiveIndex;
					int archiveIndexTOCOffset = this.OffsetEntries[(int)array4[k]].ArchiveIndexTOCOffset;
					string filename = this.ArchiveFiles[archiveIndex].Filename;
					uint archiveOffset = this.OffsetEntries[(int)array4[k]].ArchiveOffset;
					int archiveOffsetTOCOffset = this.OffsetEntries[(int)array4[k]].ArchiveOffsetTOCOffset;
					uint fileSize = this.SizeEntries[k].FileSize;
					int fileSizeTOCOffset = this.SizeEntries[k].FileSizeTOCOffset;
					ulong fileAssetID = this.AssetIDs[k];
					TOCMapEntry tocmapEntry = new TOCMapEntry();
					tocmapEntry.ArchiveIndex = archiveIndex;
					tocmapEntry.ArchiveIndexTOCOffset = archiveIndexTOCOffset;
					tocmapEntry.ArchiveName = filename;
					tocmapEntry.FileOffset = archiveOffset;
					tocmapEntry.FileOffsetTOCOffset = archiveOffsetTOCOffset;
					tocmapEntry.FileSize = (int)fileSize;
					tocmapEntry.FileSizeTOCOffset = fileSizeTOCOffset;
					tocmapEntry.FileAssetID = fileAssetID;
					bool flag3 = !dictionary.TryGetValue(tocmapEntry.FileAssetID, out tocmapEntry.FileName);
					if (flag3)
					{
						tocmapEntry.FileName = "0x" + fileAssetID.ToString("X2");
					}
					bool flag4 = this.TOCMaps[archiveIndex] == null;
					if (flag4)
					{
						this.TOCMaps[archiveIndex] = new TOCMap();
					}
					this.TOCMaps[archiveIndex].TOCMapEntries.Add(tocmapEntry);
				}
				File.WriteAllText(Globals.TemporaryDirectory + "toc.txt", text);
				binaryReader.Close();
				binaryReader.Dispose();
				Console.WriteLine("Done parsing decompressed TOC!");
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000E26C File Offset: 0x0000C46C
		private string BinaryReaderReadNTString(BinaryReader br)
		{
			string text = "";
			for (;;)
			{
				byte b = br.ReadByte();
				bool flag = b == 0;
				if (flag)
				{
					break;
				}
				string str = text;
				char c = (char)b;
				text = str + c.ToString();
			}
			return text;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000E2B4 File Offset: 0x0000C4B4
		private void ParseArchiveFiles(int offset, int length)
		{
			BinaryReader binaryReader = new BinaryReader(File.OpenRead(this.decompressedFileName));
			binaryReader.BaseStream.Position = (long)offset;
			this.ArchiveFiles = new List<ArchiveFile>();
			for (int i = 0; i < length / 72; i++)
			{
				ArchiveFile item = default(ArchiveFile);
				binaryReader.ReadBytes(3);
				item.InstallBucket = binaryReader.ReadByte();
				item.Chunkmap = binaryReader.ReadUInt32();
				long position = binaryReader.BaseStream.Position;
				item.Filename = this.BinaryReaderReadNTString(binaryReader);
				binaryReader.BaseStream.Position = position + 7L;
				binaryReader.ReadBytes(57);
				this.ArchiveFiles.Add(item);
			}
			binaryReader.Close();
			binaryReader.Dispose();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000E37C File Offset: 0x0000C57C
		private void ParseAssetIDs(int offset, int length)
		{
			BinaryReader binaryReader = new BinaryReader(File.OpenRead(this.decompressedFileName));
			binaryReader.BaseStream.Position = (long)offset;
			this.AssetIDs = new List<ulong>();
			for (int i = 0; i < length / 8; i++)
			{
				ulong item = binaryReader.ReadUInt64();
				this.AssetIDs.Add(item);
			}
			binaryReader.Close();
			binaryReader.Dispose();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000E3EC File Offset: 0x0000C5EC
		private void ParseSizeEntries(int offset, int length)
		{
			BinaryReader binaryReader = new BinaryReader(File.OpenRead(this.decompressedFileName));
			binaryReader.BaseStream.Position = (long)offset;
			this.SizeEntries = new List<SizeEntry>();
			for (int i = 0; i < length / 12; i++)
			{
				SizeEntry item = default(SizeEntry);
				item.FileCtrInc = binaryReader.ReadUInt32();
				item.FileSizeTOCOffset = (int)binaryReader.BaseStream.Position;
				item.FileSize = binaryReader.ReadUInt32();
				item.FileCtr = binaryReader.ReadUInt32();
				this.SizeEntries.Add(item);
			}
			binaryReader.Close();
			binaryReader.Dispose();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000E498 File Offset: 0x0000C698
		private void ParseKeyAssets(int offset, int length)
		{
			BinaryReader binaryReader = new BinaryReader(File.OpenRead(this.decompressedFileName));
			binaryReader.BaseStream.Position = (long)offset;
			this.KeyAssets = new List<ulong>();
			for (int i = 0; i < length / 8; i++)
			{
				ulong item = binaryReader.ReadUInt64();
				this.KeyAssets.Add(item);
			}
			binaryReader.Close();
			binaryReader.Dispose();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000E508 File Offset: 0x0000C708
		private void ParseOffsetEntries(int offset, int length)
		{
			BinaryReader binaryReader = new BinaryReader(File.OpenRead(this.decompressedFileName));
			binaryReader.BaseStream.Position = (long)offset;
			this.OffsetEntries = new List<OffsetEntry>();
			for (int i = 0; i < length / 8; i++)
			{
				OffsetEntry item = default(OffsetEntry);
				item.ArchiveIndexTOCOffset = (int)binaryReader.BaseStream.Position;
				item.ArchiveIndex = binaryReader.ReadInt32();
				item.ArchiveOffsetTOCOffset = (int)binaryReader.BaseStream.Position;
				item.ArchiveOffset = binaryReader.ReadUInt32();
				this.OffsetEntries.Add(item);
			}
			binaryReader.Close();
			binaryReader.Dispose();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000E5B8 File Offset: 0x0000C7B8
		private void ParseSpansEntries(int offset, int length)
		{
			BinaryReader binaryReader = new BinaryReader(File.OpenRead(this.decompressedFileName));
			binaryReader.BaseStream.Position = (long)offset;
			this.SpansEntries = new List<SpansEntry>();
			for (int i = 0; i < length / 8; i++)
			{
				SpansEntry item = default(SpansEntry);
				item.AssetIndex = binaryReader.ReadUInt32();
				item.Count = binaryReader.ReadUInt32();
				this.SpansEntries.Add(item);
			}
			binaryReader.Close();
			binaryReader.Dispose();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000E644 File Offset: 0x0000C844
		public void GenerateCSV(string fileName)
		{
			List<string> list = new List<string>();
			list.Add("Asset Path,Asset ID,Archive File,Segment Offset,File Size");
			for (int i = 0; i < this.TOCMaps.Length; i++)
			{
				bool flag = this.TOCMaps[i] == null;
				if (!flag)
				{
					foreach (TOCMapEntry tocmapEntry in this.TOCMaps[i].TOCMapEntries)
					{
						list.Add(string.Concat(new string[]
						{
							"\"",
							tocmapEntry.FileName,
							"\",0x",
							tocmapEntry.FileAssetID.ToString("X2"),
							",",
							tocmapEntry.ArchiveName,
							",0x",
							tocmapEntry.FileOffset.ToString("X2"),
							",0x",
							tocmapEntry.FileSize.ToString("X2")
						}));
					}
				}
			}
			File.WriteAllLines(fileName, list);
		}

		// Token: 0x040000AA RID: 170
		public string Filename;

		// Token: 0x040000AB RID: 171
		public int ArchiveFileSectionOffset;

		// Token: 0x040000AC RID: 172
		public int ArchiveFileSectionOffsetOffset;

		// Token: 0x040000AD RID: 173
		public int ArchiveFileSectionLen;

		// Token: 0x040000AE RID: 174
		public int ArchiveFileSectionLenOffset;

		// Token: 0x040000AF RID: 175
		public List<ArchiveFile> ArchiveFiles;

		// Token: 0x040000B0 RID: 176
		public List<ulong> AssetIDs;

		// Token: 0x040000B1 RID: 177
		public List<SizeEntry> SizeEntries;

		// Token: 0x040000B2 RID: 178
		public List<ulong> KeyAssets;

		// Token: 0x040000B3 RID: 179
		public List<OffsetEntry> OffsetEntries;

		// Token: 0x040000B4 RID: 180
		public List<SpansEntry> SpansEntries;

		// Token: 0x040000B5 RID: 181
		public List<Section> Sections = new List<Section>();

		// Token: 0x040000B6 RID: 182
		public TOCMap[] TOCMaps;

		// Token: 0x040000B7 RID: 183
		public string decompressedFileName;
	}
}
