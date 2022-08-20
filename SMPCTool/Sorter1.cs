using System;
using System.Collections;
using System.Windows.Forms;

namespace SMPCTool
{
	// Token: 0x02000012 RID: 18
	public class Sorter1 : IComparer
	{
		// Token: 0x06000092 RID: 146 RVA: 0x0000C514 File Offset: 0x0000A714
		public int Compare(object x, object y)
		{
			TreeNode treeNode = x as TreeNode;
			TreeNode treeNode2 = y as TreeNode;
			bool flag = treeNode.Parent == null || treeNode2.Parent == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = string.Compare(treeNode.Text, treeNode2.Text);
			}
			return result;
		}
	}
}
