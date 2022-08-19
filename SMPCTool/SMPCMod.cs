using System;
using System.Collections.Generic;

namespace SMPCTool
{
	// Token: 0x02000010 RID: 16
	public class SMPCMod
	{
		// Token: 0x0400007B RID: 123
		public string FileName;

		// Token: 0x0400007C RID: 124
		public string Title;

		// Token: 0x0400007D RID: 125
		public string Author;

		// Token: 0x0400007E RID: 126
		public string Description;

		// Token: 0x0400007F RID: 127
		public List<ModFile> ModFiles = new List<ModFile>();
	}
}
