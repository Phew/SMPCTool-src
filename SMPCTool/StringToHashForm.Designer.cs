namespace SMPCTool
{
	// Token: 0x0200000F RID: 15
	public partial class StringToHashForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00008680 File Offset: 0x00006880
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000086B8 File Offset: 0x000068B8
		private void InitializeComponent()
		{
			this.stringTextBox = new global::System.Windows.Forms.TextBox();
			this.hashTextBox = new global::System.Windows.Forms.TextBox();
			this.generateButton = new global::System.Windows.Forms.Button();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.stringTextBox.Location = new global::System.Drawing.Point(12, 27);
			this.stringTextBox.Name = "stringTextBox";
			this.stringTextBox.Size = new global::System.Drawing.Size(457, 20);
			this.stringTextBox.TabIndex = 0;
			this.hashTextBox.Location = new global::System.Drawing.Point(12, 98);
			this.hashTextBox.Name = "hashTextBox";
			this.hashTextBox.ReadOnly = true;
			this.hashTextBox.Size = new global::System.Drawing.Size(457, 20);
			this.hashTextBox.TabIndex = 1;
			this.hashTextBox.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Center;
			this.generateButton.Location = new global::System.Drawing.Point(155, 53);
			this.generateButton.Name = "generateButton";
			this.generateButton.Size = new global::System.Drawing.Size(171, 23);
			this.generateButton.TabIndex = 2;
			this.generateButton.Text = "GENERATE";
			this.generateButton.UseVisualStyleBackColor = true;
			this.generateButton.Click += new global::System.EventHandler(this.generateButton_Click);
			this.label1.AutoSize = true;
			this.label1.ForeColor = global::System.Drawing.Color.Silver;
			this.label1.Location = new global::System.Drawing.Point(11, 11);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(34, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "String";
			this.label2.AutoSize = true;
			this.label2.ForeColor = global::System.Drawing.Color.Silver;
			this.label2.Location = new global::System.Drawing.Point(9, 82);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(32, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Hash";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.Color.FromArgb(40, 40, 40);
			base.ClientSize = new global::System.Drawing.Size(482, 130);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.generateButton);
			base.Controls.Add(this.hashTextBox);
			base.Controls.Add(this.stringTextBox);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "StringToHashForm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "String To Hash Generator";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400005F RID: 95
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000060 RID: 96
		private global::System.Windows.Forms.TextBox stringTextBox;

		// Token: 0x04000061 RID: 97
		private global::System.Windows.Forms.TextBox hashTextBox;

		// Token: 0x04000062 RID: 98
		private global::System.Windows.Forms.Button generateButton;

		// Token: 0x04000063 RID: 99
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000064 RID: 100
		private global::System.Windows.Forms.Label label2;
	}
}
