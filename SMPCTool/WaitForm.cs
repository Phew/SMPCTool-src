using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SMPCTool
{
	// Token: 0x0200001D RID: 29
	public partial class WaitForm : Form
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x0000E780 File Offset: 0x0000C980
		public WaitForm()
		{
			this.InitializeComponent();
			MainForm.WaitForm = this;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000E7A5 File Offset: 0x0000C9A5
		private void WaitForm_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000E7A8 File Offset: 0x0000C9A8
		public void SetText(string text)
		{
			this.label1.Text = text;
			this.timer1.Enabled = true;
			this.timer1.Interval = 250;
			this.saveText = this.label1.Text;
			this.saveText = this.saveText.Replace("...", "");
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000E810 File Offset: 0x0000CA10
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

		// Token: 0x040000B8 RID: 184
		private string saveText;

		// Token: 0x040000B9 RID: 185
		private int state = 0;
	}
}
