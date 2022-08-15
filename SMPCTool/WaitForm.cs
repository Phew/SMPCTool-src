using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SMPCTool
{
	// Token: 0x02000017 RID: 23
	public partial class WaitForm : Form
	{
		// Token: 0x06000076 RID: 118 RVA: 0x0000996C File Offset: 0x00007B6C
		public WaitForm()
		{
			this.InitializeComponent();
			MainForm.WaitForm = this;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00009991 File Offset: 0x00007B91
		private void WaitForm_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00009994 File Offset: 0x00007B94
		public void SetText(string text)
		{
			this.label1.Text = text;
			this.timer1.Enabled = true;
			this.timer1.Interval = 250;
			this.saveText = this.label1.Text;
			this.saveText = this.saveText.Replace("...", "");
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000099FC File Offset: 0x00007BFC
		private void timer1_Tick(object sender, EventArgs e)
		{
			switch (this.state)
			{
			case 0:
				this.label1.Text = (this.saveText ?? "");
				break;
			case 1:
				this.label1.Text = this.saveText + ".";
				break;
			case 2:
				this.label1.Text = this.saveText + "..";
				break;
			case 3:
				this.label1.Text = this.saveText + "...";
				break;
			}
			this.state++;
			bool flag = this.state > 3;
			if (flag)
			{
				this.state = 0;
			}
		}

		// Token: 0x04000085 RID: 133
		private string saveText;

		// Token: 0x04000086 RID: 134
		private int state = 0;
	}
}
