using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SMPCTool
{
	// Token: 0x02000013 RID: 19
	public partial class StringToHashForm : Form
	{
		// Token: 0x0600008C RID: 140 RVA: 0x0000BE62 File Offset: 0x0000A062
		public StringToHashForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000BE7C File Offset: 0x0000A07C
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

		// Token: 0x0600008E RID: 142 RVA: 0x0000BF24 File Offset: 0x0000A124
		private void generateButton_Click(object sender, EventArgs e)
		{
			ulong num = this.StringToHash(this.stringTextBox.Text);
			this.hashTextBox.Text = "0x" + num.ToString("X2");
		}
	}
}
