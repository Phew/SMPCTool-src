namespace SMPCTool
{
	// Token: 0x02000008 RID: 8
	public partial class InstallModForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00004018 File Offset: 0x00002218
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00004050 File Offset: 0x00002250
		private void InitializeComponent()
		{
			this.installButton = new global::System.Windows.Forms.Button();
			this.label5 = new global::System.Windows.Forms.Label();
			this.thumbnailPictureBox = new global::System.Windows.Forms.PictureBox();
			this.label4 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.descriptionTextBox = new global::System.Windows.Forms.TextBox();
			this.authorTextBox = new global::System.Windows.Forms.TextBox();
			this.titleTextBox = new global::System.Windows.Forms.TextBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.listBox1 = new global::System.Windows.Forms.ListBox();
			((global::System.ComponentModel.ISupportInitialize)this.thumbnailPictureBox).BeginInit();
			base.SuspendLayout();
			this.installButton.Location = new global::System.Drawing.Point(27, 454);
			this.installButton.Name = "installButton";
			this.installButton.Size = new global::System.Drawing.Size(259, 39);
			this.installButton.TabIndex = 3;
			this.installButton.Text = "INSTALL MOD";
			this.installButton.UseVisualStyleBackColor = true;
			this.installButton.Click += new global::System.EventHandler(this.installButton_Click);
			this.label5.AutoSize = true;
			this.label5.ForeColor = global::System.Drawing.Color.Silver;
			this.label5.Location = new global::System.Drawing.Point(398, 389);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(56, 13);
			this.label5.TabIndex = 21;
			this.label5.Text = "Thumbnail";
			this.thumbnailPictureBox.BackColor = global::System.Drawing.Color.FromArgb(60, 60, 60);
			this.thumbnailPictureBox.Location = new global::System.Drawing.Point(401, 405);
			this.thumbnailPictureBox.Name = "thumbnailPictureBox";
			this.thumbnailPictureBox.Size = new global::System.Drawing.Size(200, 200);
			this.thumbnailPictureBox.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.thumbnailPictureBox.TabIndex = 20;
			this.thumbnailPictureBox.TabStop = false;
			this.label4.AutoSize = true;
			this.label4.ForeColor = global::System.Drawing.Color.Silver;
			this.label4.Location = new global::System.Drawing.Point(226, 23);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(60, 13);
			this.label4.TabIndex = 19;
			this.label4.Text = "Description";
			this.label3.AutoSize = true;
			this.label3.ForeColor = global::System.Drawing.Color.Silver;
			this.label3.Location = new global::System.Drawing.Point(12, 62);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(38, 13);
			this.label3.TabIndex = 18;
			this.label3.Text = "Author";
			this.label2.AutoSize = true;
			this.label2.ForeColor = global::System.Drawing.Color.Silver;
			this.label2.Location = new global::System.Drawing.Point(12, 22);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(27, 13);
			this.label2.TabIndex = 17;
			this.label2.Text = "Title";
			this.descriptionTextBox.Location = new global::System.Drawing.Point(229, 39);
			this.descriptionTextBox.Multiline = true;
			this.descriptionTextBox.Name = "descriptionTextBox";
			this.descriptionTextBox.ReadOnly = true;
			this.descriptionTextBox.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
			this.descriptionTextBox.Size = new global::System.Drawing.Size(375, 77);
			this.descriptionTextBox.TabIndex = 16;
			this.authorTextBox.Location = new global::System.Drawing.Point(15, 78);
			this.authorTextBox.Name = "authorTextBox";
			this.authorTextBox.ReadOnly = true;
			this.authorTextBox.Size = new global::System.Drawing.Size(191, 20);
			this.authorTextBox.TabIndex = 15;
			this.titleTextBox.Location = new global::System.Drawing.Point(15, 39);
			this.titleTextBox.Name = "titleTextBox";
			this.titleTextBox.ReadOnly = true;
			this.titleTextBox.Size = new global::System.Drawing.Size(191, 20);
			this.titleTextBox.TabIndex = 14;
			this.label1.AutoSize = true;
			this.label1.ForeColor = global::System.Drawing.Color.Silver;
			this.label1.Location = new global::System.Drawing.Point(12, 135);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(74, 13);
			this.label1.TabIndex = 12;
			this.label1.Text = "Modified Files:";
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new global::System.Drawing.Point(15, 151);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new global::System.Drawing.Size(589, 212);
			this.listBox1.TabIndex = 11;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.Color.FromArgb(40, 40, 40);
			base.ClientSize = new global::System.Drawing.Size(656, 617);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.thumbnailPictureBox);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.descriptionTextBox);
			base.Controls.Add(this.authorTextBox);
			base.Controls.Add(this.titleTextBox);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.listBox1);
			base.Controls.Add(this.installButton);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "InstallModForm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Install Mod...";
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.InstallModForm_FormClosing);
			base.Load += new global::System.EventHandler(this.InstallModForm_Load);
			((global::System.ComponentModel.ISupportInitialize)this.thumbnailPictureBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000025 RID: 37
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000026 RID: 38
		private global::System.Windows.Forms.Button installButton;

		// Token: 0x04000027 RID: 39
		private global::System.Windows.Forms.Label label5;

		// Token: 0x04000028 RID: 40
		private global::System.Windows.Forms.PictureBox thumbnailPictureBox;

		// Token: 0x04000029 RID: 41
		private global::System.Windows.Forms.Label label4;

		// Token: 0x0400002A RID: 42
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400002B RID: 43
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400002C RID: 44
		private global::System.Windows.Forms.TextBox descriptionTextBox;

		// Token: 0x0400002D RID: 45
		private global::System.Windows.Forms.TextBox authorTextBox;

		// Token: 0x0400002E RID: 46
		private global::System.Windows.Forms.TextBox titleTextBox;

		// Token: 0x0400002F RID: 47
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000030 RID: 48
		private global::System.Windows.Forms.ListBox listBox1;
	}
}
