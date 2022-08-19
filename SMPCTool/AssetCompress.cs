using System;
using System.Collections.Generic;
using System.IO;

namespace SMPCTool
{
	// Token: 0x02000004 RID: 4
	public class AssetCompress
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002750 File Offset: 0x00000950
		public AssetCompress(byte[] bytes, string fileName)
		{
			List<byte> list = new List<byte>();
			int i = bytes.Length;
			list.Add(240);
			for (i -= 15; i > 0; i -= 255)
			{
				list.Add((byte)Math.Min(i, 255));
			}
			list.AddRange(bytes);
			File.WriteAllBytes(fileName, list.ToArray());
		}
	}
}
