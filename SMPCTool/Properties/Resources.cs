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
		// Token: 0x060000AC RID: 172 RVA: 0x0000EB43 File Offset: 0x0000CD43
		internal Resources()
		{
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000EB50 File Offset: 0x0000CD50
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
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000EB98 File Offset: 0x0000CD98
		// (set) Token: 0x060000AF RID: 175 RVA: 0x0000EBAF File Offset: 0x0000CDAF
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

		// Token: 0x040000BE RID: 190
		private static ResourceManager resourceMan;

		// Token: 0x040000BF RID: 191
		private static CultureInfo resourceCulture;
	}
}
