using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SMPCTool
{
	// Token: 0x0200001D RID: 29
	public partial class WaitForm : Form
	{
		// Token: 0x060000AE RID: 174 RVA: 0x0000EE88 File Offset: 0x0000D088
		public WaitForm()
		{
			this.InitializeComponent();
			MainForm.WaitForm = this;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000EEAD File Offset: 0x0000D0AD
		private void WaitForm_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000EEB0 File Offset: 0x0000D0B0
		public void SetText(string text)
		{
			this.label1.Text = text;
			this.timer1.Enabled = true;
			this.timer1.Interval = 250;
			this.saveText = this.label1.Text;
			this.saveText = this.saveText.Replace("...", "");
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000EF18 File Offset: 0x0000D118
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

		// Token: 0x040000BB RID: 187
		private string saveText;

		// Token: 0x040000BC RID: 188
		private int state = 0;
	}
}
