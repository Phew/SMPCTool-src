using System;
using System.Windows.Forms;

namespace SMPCTool
{
	// Token: 0x0200000D RID: 13
	internal static class Program
	{
		// Token: 0x06000089 RID: 137 RVA: 0x0000BD38 File Offset: 0x00009F38
		[STAThread]
		private static void Main()
		{
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			bool flag = commandLineArgs.Length == 2;
			if (flag)
			{
				string a = commandLineArgs[1].ToLower();
				bool flag2 = a == "-install";
				if (flag2)
				{
					Globals.VortexInstallCMD = true;
				}
			}
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
