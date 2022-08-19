using System;
using System.Windows.Forms;

namespace SMPCTool
{
	// Token: 0x0200000D RID: 13
	internal static class Program
	{
		// Token: 0x06000081 RID: 129 RVA: 0x0000B7C0 File Offset: 0x000099C0
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
