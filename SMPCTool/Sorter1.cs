using System;
using System.Collections;
using System.Windows.Forms;

namespace SMPCTool
{
	// Token: 0x0200000E RID: 14
	public class Sorter1 : IComparer
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00008524 File Offset: 0x00006724
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
