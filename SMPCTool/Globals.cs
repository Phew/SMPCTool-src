using System;
using System.Collections.Generic;

namespace SMPCTool
{
	// Token: 0x02000008 RID: 8
	public static class Globals
	{
		// Token: 0x04000023 RID: 35
		public static string Version = "1.1.0";

		// Token: 0x04000024 RID: 36
		public static string AssetArchivesDirectory;

		// Token: 0x04000025 RID: 37
		public static string TemporaryDirectory;

		// Token: 0x04000026 RID: 38
		public static string ModManagerDirectory;

		// Token: 0x04000027 RID: 39
		public static string ModInstallDirectory;

		// Token: 0x04000028 RID: 40
		public static string ModCreateDirectory;

		// Token: 0x04000029 RID: 41
		public static TOC TOC;

		// Token: 0x0400002A RID: 42
		public static string ModTitle = "";

		// Token: 0x0400002B RID: 43
		public static string ModAuthor = "";

		// Token: 0x0400002C RID: 44
		public static string ModDescription = "";

		// Token: 0x0400002D RID: 45
		public static List<TOCMapEntry> ReplacedTOCMapEntries = new List<TOCMapEntry>();

		// Token: 0x0400002E RID: 46
		public static List<string> ReplacedTOCMapEntriesReplaceFileNames = new List<string>();
	}
}
