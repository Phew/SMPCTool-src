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
		// Token: 0x06000094 RID: 148 RVA: 0x0000C56A File Offset: 0x0000A76A
		public StringToHashForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000C584 File Offset: 0x0000A784
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

		// Token: 0x06000096 RID: 150 RVA: 0x0000C62C File Offset: 0x0000A82C
		private void generateButton_Click(object sender, EventArgs e)
		{
			ulong num = this.StringToHash(this.stringTextBox.Text);
			this.hashTextBox.Text = "0x" + num.ToString("X2");
		}
	}
}
