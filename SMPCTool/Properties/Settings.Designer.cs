using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace SMPCTool.Properties
{
	// Token: 0x0200001F RID: 31
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000F2C0 File Offset: 0x0000D4C0
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x040000C3 RID: 195
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
