using System;
using System.Collections;
using System.Windows.Forms;

// Token: 0x02000002 RID: 2
public class ListViewColumnSorter : IComparer
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002048 File Offset: 0x00000248
	public ListViewColumnSorter()
	{
		this.ColumnToSort = 0;
		this.OrderOfSort = SortOrder.None;
		this.ObjectCompare = new CaseInsensitiveComparer();
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000206C File Offset: 0x0000026C
	public int Compare(object x, object y)
	{
		ListViewItem listViewItem = (ListViewItem)x;
		ListViewItem listViewItem2 = (ListViewItem)y;
		decimal d = 0m;
		bool flag = decimal.TryParse(listViewItem.SubItems[this.ColumnToSort].Text, out d) && this.ColumnToSort != 2;
		int num;
		if (flag)
		{
			try
			{
				num = decimal.Compare(d, Convert.ToDecimal(listViewItem2.SubItems[this.ColumnToSort].Text));
			}
			catch
			{
				num = this.ObjectCompare.Compare(listViewItem.SubItems[this.ColumnToSort].Text, listViewItem2.SubItems[this.ColumnToSort].Text);
			}
		}
		else
		{
			num = this.ObjectCompare.Compare(listViewItem.SubItems[this.ColumnToSort].Text, listViewItem2.SubItems[this.ColumnToSort].Text);
		}
		bool flag2 = this.OrderOfSort == SortOrder.Ascending;
		int result;
		if (flag2)
		{
			result = num;
		}
		else
		{
			bool flag3 = this.OrderOfSort == SortOrder.Descending;
			if (flag3)
			{
				result = -num;
			}
			else
			{
				result = 0;
			}
		}
		return result;
	}

	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000004 RID: 4 RVA: 0x000021B4 File Offset: 0x000003B4
	// (set) Token: 0x06000003 RID: 3 RVA: 0x000021A8 File Offset: 0x000003A8
	public int SortColumn
	{
		get
		{
			return this.ColumnToSort;
		}
		set
		{
			this.ColumnToSort = value;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000006 RID: 6 RVA: 0x000021D8 File Offset: 0x000003D8
	// (set) Token: 0x06000005 RID: 5 RVA: 0x000021CC File Offset: 0x000003CC
	public SortOrder Order
	{
		get
		{
			return this.OrderOfSort;
		}
		set
		{
			this.OrderOfSort = value;
		}
	}

	// Token: 0x04000001 RID: 1
	private int ColumnToSort;

	// Token: 0x04000002 RID: 2
	private SortOrder OrderOfSort;

	// Token: 0x04000003 RID: 3
	private CaseInsensitiveComparer ObjectCompare;
}
