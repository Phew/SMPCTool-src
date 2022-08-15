using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace SMPCTool.Properties
{
	// Token: 0x02000019 RID: 25
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00009DA4 File Offset: 0x00007FA4
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x0400008D RID: 141
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
