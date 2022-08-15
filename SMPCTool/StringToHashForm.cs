using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SMPCTool
{
	// Token: 0x0200000F RID: 15
	public partial class StringToHashForm : Form
	{
		// Token: 0x06000063 RID: 99 RVA: 0x0000857A File Offset: 0x0000677A
		public StringToHashForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00008594 File Offset: 0x00006794
		private ulong StringToHash(string text)
		{
			Process process = new Process();
			process.StartInfo.FileName = "SMPS4HashTool.exe";
			process.StartInfo.Arguments = "\"" + text + "\"";
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.RedirectStandardOutput = true;
			process.Start();
			ulong hash = 0UL;
			process.OutputDataReceived += delegate(object sender2, DataReceivedEventArgs e2)
			{
				string data = e2.Data;
				bool flag = string.IsNullOrWhiteSpace(data);
				if (!flag)
				{
					bool flag2 = data.Contains(" ");
					if (!flag2)
					{
						hash = ulong.Parse(data);
					}
				}
			};
			process.BeginOutputReadLine();
			process.WaitForExit();
			return hash;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000863C File Offset: 0x0000683C
		private void generateButton_Click(object sender, EventArgs e)
		{
			ulong num = this.StringToHash(this.stringTextBox.Text);
			this.hashTextBox.Text = "0x" + num.ToString("X2");
		}
	}
}
