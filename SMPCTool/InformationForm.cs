using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SMPCTool
{
	// Token: 0x02000009 RID: 9
	public partial class InformationForm : Form
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00003C08 File Offset: 0x00001E08
		public InformationForm(string text)
		{
			this.InitializeComponent();
			text = text.Replace("\n", Environment.NewLine);
			this.textBox1.Text = text;
			this.textBox1.Select(0, 0);
		}
	}
}
