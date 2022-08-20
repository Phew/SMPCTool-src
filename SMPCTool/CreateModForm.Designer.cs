namespace SMPCTool
{
	// Token: 0x02000006 RID: 6
	public partial class CreateModForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000023 RID: 35 RVA: 0x0000301C File Offset: 0x0000121C
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003054 File Offset: 0x00001254
		private void InitializeComponent()
		{
			this.listBox1 = new global::System.Windows.Forms.ListBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.createButton = new global::System.Windows.Forms.Button();
			this.titleTextBox = new global::System.Windows.Forms.TextBox();
			this.authorTextBox = new global::System.Windows.Forms.TextBox();
			this.descriptionTextBox = new global::System.Windows.Forms.TextBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.thumbnailPictureBox = new global::System.Windows.Forms.PictureBox();
			this.label5 = new global::System.Windows.Forms.Label();
			this.clearModifiedFilesBtn = new global::System.Windows.Forms.Button();
			this.deleteModifiedFile = new global::System.Windows.Forms.Button();
			((global::System.ComponentModel.ISupportInitialize)this.thumbnailPictureBox).BeginInit();
			base.SuspendLayout();
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new global::System.Drawing.Point(15, 151);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new global::System.Drawing.Size(589, 212);
			this.listBox1.TabIndex = 0;
			this.label1.AutoSize = true;
			this.label1.ForeColor = global::System.Drawing.Color.Silver;
			this.label1.Location = new global::System.Drawing.Point(12, 135);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(74, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Modified Files:";
			this.createButton.Location = new global::System.Drawing.Point(70, 515);
			this.createButton.Name = "createButton";
			this.createButton.Size = new global::System.Drawing.Size(259, 39);
			this.createButton.TabIndex = 2;
			this.createButton.Text = "CREATE MOD";
			this.createButton.UseVisualStyleBackColor = true;
			this.createButton.Click += new global::System.EventHandler(this.createButton_Click);
			this.titleTextBox.Location = new global::System.Drawing.Point(15, 39);
			this.titleTextBox.Name = "titleTextBox";
			this.titleTextBox.Size = new global::System.Drawing.Size(191, 20);
			this.titleTextBox.TabIndex = 3;
			this.titleTextBox.TextChanged += new global::System.EventHandler(this.titleTextBox_TextChanged);
			this.authorTextBox.Location = new global::System.Drawing.Point(15, 78);
			this.authorTextBox.Name = "authorTextBox";
			this.authorTextBox.Size = new global::System.Drawing.Size(191, 20);
			this.authorTextBox.TabIndex = 4;
			this.authorTextBox.TextChanged += new global::System.EventHandler(this.authorTextBox_TextChanged);
			this.descriptionTextBox.Location = new global::System.Drawing.Point(229, 39);
			this.descriptionTextBox.Multiline = true;
			this.descriptionTextBox.Name = "descriptionTextBox";
			this.descriptionTextBox.ScrollBars = global::System.Windows.Forms.ScrollBars.Both;
			this.descriptionTextBox.Size = new global::System.Drawing.Size(375, 77);
			this.descriptionTextBox.TabIndex = 5;
			this.descriptionTextBox.TextChanged += new global::System.EventHandler(this.descriptionTextBox_TextChanged);
			this.label2.AutoSize = true;
			this.label2.ForeColor = global::System.Drawing.Color.Silver;
			this.label2.Location = new global::System.Drawing.Point(12, 22);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(27, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Title";
			this.label3.AutoSize = true;
			this.label3.ForeColor = global::System.Drawing.Color.Silver;
			this.label3.Location = new global::System.Drawing.Point(12, 62);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(38, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Author";
			this.label4.AutoSize = true;
			this.label4.ForeColor = global::System.Drawing.Color.Silver;
			this.label4.Location = new global::System.Drawing.Point(226, 23);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(60, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Description";
			this.thumbnailPictureBox.BackColor = global::System.Drawing.Color.FromArgb(60, 60, 60);
			this.thumbnailPictureBox.Location = new global::System.Drawing.Point(401, 405);
			this.thumbnailPictureBox.Name = "thumbnailPictureBox";
			this.thumbnailPictureBox.Size = new global::System.Drawing.Size(200, 200);
			this.thumbnailPictureBox.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.thumbnailPictureBox.TabIndex = 9;
			this.thumbnailPictureBox.TabStop = false;
			this.thumbnailPictureBox.Click += new global::System.EventHandler(this.pictureBox1_Click);
			this.label5.AutoSize = true;
			this.label5.ForeColor = global::System.Drawing.Color.Silver;
			this.label5.Location = new global::System.Drawing.Point(398, 389);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(169, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "Upload Thumbnail (200x200 PNG)";
			this.clearModifiedFilesBtn.Location = new global::System.Drawing.Point(70, 460);
			this.clearModifiedFilesBtn.Name = "clearModifiedFilesBtn";
			this.clearModifiedFilesBtn.Size = new global::System.Drawing.Size(259, 39);
			this.clearModifiedFilesBtn.TabIndex = 11;
			this.clearModifiedFilesBtn.Text = "CLEAR ALL MODIFIED FILES";
			this.clearModifiedFilesBtn.UseVisualStyleBackColor = true;
			this.clearModifiedFilesBtn.Click += new global::System.EventHandler(this.clearModifiedFilesBtn_Click);
			this.deleteModifiedFile.Location = new global::System.Drawing.Point(70, 405);
			this.deleteModifiedFile.Name = "deleteModifiedFile";
			this.deleteModifiedFile.Size = new global::System.Drawing.Size(259, 39);
			this.deleteModifiedFile.TabIndex = 12;
			this.deleteModifiedFile.Text = "DELETE MODIFIED FILE";
			this.deleteModifiedFile.UseVisualStyleBackColor = true;
			this.deleteModifiedFile.Click += new global::System.EventHandler(this.deleteModifiedFile_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.Color.FromArgb(40, 40, 40);
			base.ClientSize = new global::System.Drawing.Size(656, 617);
			base.Controls.Add(this.deleteModifiedFile);
			base.Controls.Add(this.clearModifiedFilesBtn);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.thumbnailPictureBox);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.descriptionTextBox);
			base.Controls.Add(this.authorTextBox);
			base.Controls.Add(this.titleTextBox);
			base.Controls.Add(this.createButton);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.listBox1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CreateModForm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Create Mod...";
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.CreateModForm_FormClosing);
			base.Load += new global::System.EventHandler(this.CreateModForm_Load);
			((global::System.ComponentModel.ISupportInitialize)this.thumbnailPictureBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000010 RID: 16
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000011 RID: 17
		private global::System.Windows.Forms.ListBox listBox1;

		// Token: 0x04000012 RID: 18
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000013 RID: 19
		private global::System.Windows.Forms.Button createButton;

		// Token: 0x04000014 RID: 20
		private global::System.Windows.Forms.TextBox titleTextBox;

		// Token: 0x04000015 RID: 21
		private global::System.Windows.Forms.TextBox authorTextBox;

		// Token: 0x04000016 RID: 22
		private global::System.Windows.Forms.TextBox descriptionTextBox;

		// Token: 0x04000017 RID: 23
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000018 RID: 24
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000019 RID: 25
		private global::System.Windows.Forms.Label label4;

		// Token: 0x0400001A RID: 26
		private global::System.Windows.Forms.PictureBox thumbnailPictureBox;

		// Token: 0x0400001B RID: 27
		private global::System.Windows.Forms.Label label5;

		// Token: 0x0400001C RID: 28
		private global::System.Windows.Forms.Button clearModifiedFilesBtn;

		// Token: 0x0400001D RID: 29
		private global::System.Windows.Forms.Button deleteModifiedFile;
	}
}
