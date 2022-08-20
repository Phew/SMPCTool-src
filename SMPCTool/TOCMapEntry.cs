using System;

namespace SMPCTool
{
	// Token: 0x02000019 RID: 25
	public class TOCMapEntry
	{
		// Token: 0x040000A0 RID: 160
		public int ArchiveIndex;

		// Token: 0x040000A1 RID: 161
		public int ArchiveIndexTOCOffset;

		// Token: 0x040000A2 RID: 162
		public string ArchiveName;

		// Token: 0x040000A3 RID: 163
		public uint FileOffset;

		// Token: 0x040000A4 RID: 164
		public int FileOffsetTOCOffset;

		// Token: 0x040000A5 RID: 165
		public int FileSize;

		// Token: 0x040000A6 RID: 166
		public int FileSizeTOCOffset;

		// Token: 0x040000A7 RID: 167
		public ulong FileAssetID;

		// Token: 0x040000A8 RID: 168
		public string FileName;
	}
}
