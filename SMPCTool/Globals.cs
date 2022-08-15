using System;
using System.Collections.Generic;

namespace SMPCTool
{
	// Token: 0x02000007 RID: 7
	public static class Globals
	{
		// Token: 0x0400001C RID: 28
		public static string Version = "1.0.0";

		// Token: 0x0400001D RID: 29
		public static string AssetArchivesDirectory;

		// Token: 0x0400001E RID: 30
		public static string TemporaryDirectory;

		// Token: 0x0400001F RID: 31
		public static string ModInstallDirectory;

		// Token: 0x04000020 RID: 32
		public static string ModCreateDirectory;

		// Token: 0x04000021 RID: 33
		public static TOC TOC;

		// Token: 0x04000022 RID: 34
		public static List<TOCMapEntry> ReplacedTOCMapEntries = new List<TOCMapEntry>();

		// Token: 0x04000023 RID: 35
		public static List<string> ReplacedTOCMapEntriesReplaceFileNames = new List<string>();
	}
}
