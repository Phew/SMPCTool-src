using System;
using System.Collections.Generic;

namespace SMPCTool
{
	// Token: 0x02000010 RID: 16
	public class SMPCMod
	{
		// Token: 0x0400007E RID: 126
		public string FileName;

		// Token: 0x0400007F RID: 127
		public string Title;

		// Token: 0x04000080 RID: 128
		public string Author;

		// Token: 0x04000081 RID: 129
		public string Description;

		// Token: 0x04000082 RID: 130
		public List<ModFile> ModFiles = new List<ModFile>();
	}
}
