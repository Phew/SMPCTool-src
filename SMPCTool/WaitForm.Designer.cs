namespace SMPCTool
{
	// Token: 0x0200001D RID: 29
	public partial class WaitForm : global::System.Windows.Forms.Form
	{
		// Token: 0x060000AA RID: 170 RVA: 0x0000E8DC File Offset: 0x0000CADC
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000E914 File Offset: 0x0000CB14
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			this.progressBar1 = new global::System.Windows.Forms.ProgressBar();
			this.label1 = new global::System.Windows.Forms.Label();
			this.timer1 = new global::System.Windows.Forms.Timer(this.components);
			base.SuspendLayout();
			this.progressBar1.Location = new global::System.Drawing.Point(12, 229);
			this.progressBar1.MarqueeAnimationSpeed = 50;
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new global::System.Drawing.Size(612, 33);
			this.progressBar1.Style = global::System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBar1.TabIndex = 0;
			this.label1.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.ForeColor = global::System.Drawing.Color.Silver;
			this.label1.Location = new global::System.Drawing.Point(12, 79);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(613, 120);
			this.label1.TabIndex = 1;
			this.label1.Text = "Loading Screen...";
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.timer1.Interval = 1000;
			this.timer1.Tick += new global::System.EventHandler(this.timer1_Tick);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.Color.FromArgb(70, 70, 70);
			base.ClientSize = new global::System.Drawing.Size(637, 339);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.progressBar1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "WaitForm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Waiting...";
			base.Load += new global::System.EventHandler(this.WaitForm_Load);
			base.ResumeLayout(false);
		}

		// Token: 0x040000BA RID: 186
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x040000BB RID: 187
		private global::System.Windows.Forms.ProgressBar progressBar1;

		// Token: 0x040000BC RID: 188
		private global::System.Windows.Forms.Label label1;

		// Token: 0x040000BD RID: 189
		private global::System.Windows.Forms.Timer timer1;
	}
}
