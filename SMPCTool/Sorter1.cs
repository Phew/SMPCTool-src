using System;
using System.Collections;
using System.Windows.Forms;

namespace SMPCTool
{
	// Token: 0x02000012 RID: 18
	public class Sorter1 : IComparer
	{
		// Token: 0x0600008A RID: 138 RVA: 0x0000BE0C File Offset: 0x0000A00C
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
