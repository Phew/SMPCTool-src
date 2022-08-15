using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ionic.Zlib;

namespace SMPCTool
{
	// Token: 0x02000016 RID: 22
	public class TOC
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00008A2A File Offset: 0x00006C2A
		public TOC(string fileName)
		{
			this.Filename = fileName;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00008A3C File Offset: 0x00006C3C
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

		// Token: 0x0600006C RID: 108 RVA: 0x00008AC8 File Offset: 0x00006CC8
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

		// Token: 0x0600006D RID: 109 RVA: 0x00008B38 File Offset: 0x00006D38
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
				int num5 = binaryReader.ReadInt32();
				int offset = binaryReader.ReadInt32();
				int length = binaryReader.ReadInt32();
				this.ParseArchiveFiles(offset, length);
				text += "ArchiveFiles\n{\n\t";
				text += "Header\n\t{\n\t\t";
				text = text + "Hash: 0x" + num5.ToString("X2") + "\n\t\t";
				text = text + "Offset: 0x" + offset.ToString("X2") + "\n\t\t";
				text = text + "Length: 0x" + length.ToString("X2") + "\n\t\t";
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
				int num6 = binaryReader.ReadInt32();
				int offset2 = binaryReader.ReadInt32();
				int length2 = binaryReader.ReadInt32();
				this.ParseAssetIDs(offset2, length2);
				text += "AssetIDs\n{\n\t";
				text += "Header\n\t{\n\t\t";
				text = text + "Hash: 0x" + num6.ToString("X2") + "\n\t\t";
				text = text + "Offset: 0x" + offset2.ToString("X2") + "\n\t\t";
				text = text + "Length: 0x" + length2.ToString("X2") + "\n\t\t";
				text = text + "Count: " + this.AssetIDs.Count.ToString() + "\n";
				text += "\t}\n}\n\n";
				int num7 = binaryReader.ReadInt32();
				int offset3 = binaryReader.ReadInt32();
				int length3 = binaryReader.ReadInt32();
				this.ParseSizeEntries(offset3, length3);
				text += "SizeEntries\n{\n\t";
				text += "Header\n\t{\n\t\t";
				text = text + "Hash: 0x" + num7.ToString("X2") + "\n\t\t";
				text = text + "Offset: 0x" + offset3.ToString("X2") + "\n\t\t";
				text = text + "Length: 0x" + length3.ToString("X2") + "\n\t\t";
				text = text + "Count: " + this.SizeEntries.Count.ToString() + "\n";
				text += "\t}\n}\n\n";
				int num8 = binaryReader.ReadInt32();
				int offset4 = binaryReader.ReadInt32();
				int length4 = binaryReader.ReadInt32();
				this.ParseKeyAssets(offset4, length4);
				text += "KeyAssets\n{\n\t";
				text += "Header\n\t{\n\t\t";
				text = text + "Hash: 0x" + num8.ToString("X2") + "\n\t\t";
				text = text + "Offset: 0x" + offset4.ToString("X2") + "\n\t\t";
				text = text + "Length: 0x" + length4.ToString("X2") + "\n\t\t";
				text = text + "Count: " + this.KeyAssets.Count.ToString() + "\n";
				text += "\t}\n}\n\n";
				int num9 = binaryReader.ReadInt32();
				int offset5 = binaryReader.ReadInt32();
				int length5 = binaryReader.ReadInt32();
				this.ParseOffsetEntries(offset5, length5);
				text += "OffsetEntries\n{\n\t";
				text += "Header\n\t{\n\t\t";
				text = text + "Hash: 0x" + num9.ToString("X2") + "\n\t\t";
				text = text + "Offset: 0x" + offset5.ToString("X2") + "\n\t\t";
				text = text + "Length: 0x" + length5.ToString("X2") + "\n\t\t";
				text = text + "Count: " + this.OffsetEntries.Count.ToString() + "\n";
				text += "\t}\n}\n\n";
				int num10 = binaryReader.ReadInt32();
				int offset6 = binaryReader.ReadInt32();
				int length6 = binaryReader.ReadInt32();
				this.ParseSpansEntries(offset6, length6);
				text += "SpansEntries\n{\n\t";
				text += "Header\n\t{\n\t\t";
				text = text + "Hash: 0x" + num10.ToString("X2") + "\n\t\t";
				text = text + "Offset: 0x" + offset6.ToString("X2") + "\n\t\t";
				text = text + "Length: 0x" + length6.ToString("X2") + "\n\t\t";
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

		// Token: 0x0600006E RID: 110 RVA: 0x00009458 File Offset: 0x00007658
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

		// Token: 0x0600006F RID: 111 RVA: 0x000094A0 File Offset: 0x000076A0
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

		// Token: 0x06000070 RID: 112 RVA: 0x00009568 File Offset: 0x00007768
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

		// Token: 0x06000071 RID: 113 RVA: 0x000095D8 File Offset: 0x000077D8
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

		// Token: 0x06000072 RID: 114 RVA: 0x00009684 File Offset: 0x00007884
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

		// Token: 0x06000073 RID: 115 RVA: 0x000096F4 File Offset: 0x000078F4
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

		// Token: 0x06000074 RID: 116 RVA: 0x000097A4 File Offset: 0x000079A4
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

		// Token: 0x06000075 RID: 117 RVA: 0x00009830 File Offset: 0x00007A30
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

		// Token: 0x0400007C RID: 124
		public string Filename;

		// Token: 0x0400007D RID: 125
		public List<ArchiveFile> ArchiveFiles;

		// Token: 0x0400007E RID: 126
		public List<ulong> AssetIDs;

		// Token: 0x0400007F RID: 127
		public List<SizeEntry> SizeEntries;

		// Token: 0x04000080 RID: 128
		public List<ulong> KeyAssets;

		// Token: 0x04000081 RID: 129
		public List<OffsetEntry> OffsetEntries;

		// Token: 0x04000082 RID: 130
		public List<SpansEntry> SpansEntries;

		// Token: 0x04000083 RID: 131
		public TOCMap[] TOCMaps;

		// Token: 0x04000084 RID: 132
		public string decompressedFileName;
	}
}
