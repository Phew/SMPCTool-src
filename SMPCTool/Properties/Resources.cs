using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace SMPCTool.Properties
{
	// Token: 0x02000018 RID: 24
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00009D2F File Offset: 0x00007F2F
		internal Resources()
		{
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00009D3C File Offset: 0x00007F3C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("SMPCTool.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00009D84 File Offset: 0x00007F84
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00009D9B File Offset: 0x00007F9B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x0400008B RID: 139
		private static ResourceManager resourceMan;

		// Token: 0x0400008C RID: 140
		private static CultureInfo resourceCulture;
	}
}
