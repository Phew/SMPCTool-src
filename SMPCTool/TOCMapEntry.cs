using System;

namespace SMPCTool
{
	// Token: 0x02000014 RID: 20
	public class TOCMapEntry
	{
		// Token: 0x04000072 RID: 114
		public int ArchiveIndex;

		// Token: 0x04000073 RID: 115
		public int ArchiveIndexTOCOffset;

		// Token: 0x04000074 RID: 116
		public string ArchiveName;

		// Token: 0x04000075 RID: 117
		public uint FileOffset;

		// Token: 0x04000076 RID: 118
		public int FileOffsetTOCOffset;

		// Token: 0x04000077 RID: 119
		public int FileSize;

		// Token: 0x04000078 RID: 120
		public int FileSizeTOCOffset;

		// Token: 0x04000079 RID: 121
		public ulong FileAssetID;

		// Token: 0x0400007A RID: 122
		public string FileName;
	}
}
