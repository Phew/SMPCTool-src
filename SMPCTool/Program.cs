using System;
using System.Windows.Forms;

namespace SMPCTool
{
	// Token: 0x0200000A RID: 10
	internal static class Program
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00007F1C File Offset: 0x0000611C
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
