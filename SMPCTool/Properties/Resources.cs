using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace SMPCTool.Properties
{
	// Token: 0x0200001E RID: 30
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x0000F24B File Offset: 0x0000D44B
		internal Resources()
		{
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000F258 File Offset: 0x0000D458
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
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000F2A0 File Offset: 0x0000D4A0
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x0000F2B7 File Offset: 0x0000D4B7
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

		// Token: 0x040000C1 RID: 193
		private static ResourceManager resourceMan;

		// Token: 0x040000C2 RID: 194
		private static CultureInfo resourceCulture;
	}
}
