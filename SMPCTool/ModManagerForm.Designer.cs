namespace SMPCTool
{
	// Token: 0x0200000C RID: 12
	public partial class ModManagerForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000086 RID: 134 RVA: 0x0000B3C4 File Offset: 0x000095C4
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000B3FC File Offset: 0x000095FC
		private void InitializeComponent()
		{
			this.label2 = new global::System.Windows.Forms.Label();
			this.uninstallButton = new global::System.Windows.Forms.Button();
			this.installButton = new global::System.Windows.Forms.Button();
			this.modsListBox = new global::System.Windows.Forms.CheckedListBox();
			this.saveButton = new global::System.Windows.Forms.Button();
			this.titleTextBox = new global::System.Windows.Forms.TextBox();
			this.descriptionTextBox = new global::System.Windows.Forms.TextBox();
			this.thumbnailPictureBox = new global::System.Windows.Forms.PictureBox();
			this.authorTextBox = new global::System.Windows.Forms.TextBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.loadOrderUpButton = new global::System.Windows.Forms.Button();
			this.loadOrderDownButton = new global::System.Windows.Forms.Button();
			((global::System.ComponentModel.ISupportInitialize)this.thumbnailPictureBox).BeginInit();
			base.SuspendLayout();
			this.label2.AutoSize = true;
			this.label2.ForeColor = global::System.Drawing.Color.Silver;
			this.label2.Location = new global::System.Drawing.Point(12, 25);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(87, 13);
			this.label2.TabIndex = 18;
			this.label2.Text = "Mod Load Order:";
			this.uninstallButton.Location = new global::System.Drawing.Point(252, 15);
			this.uninstallButton.Name = "uninstallButton";
			this.uninstallButton.Size = new global::System.Drawing.Size(113, 23);
			this.uninstallButton.TabIndex = 19;
			this.uninstallButton.Text = "Remove";
			this.uninstallButton.UseVisualStyleBackColor = true;
			this.uninstallButton.Click += new global::System.EventHandler(this.uninstallButton_Click);
			this.installButton.Location = new global::System.Drawing.Point(133, 15);
			this.installButton.Name = "installButton";
			this.installButton.Size = new global::System.Drawing.Size(113, 23);
			this.installButton.TabIndex = 20;
			this.installButton.Text = "Add";
			this.installButton.UseVisualStyleBackColor = true;
			this.installButton.Click += new global::System.EventHandler(this.installButton_Click);
			this.modsListBox.CheckOnClick = true;
			this.modsListBox.FormattingEnabled = true;
			this.modsListBox.Location = new global::System.Drawing.Point(15, 41);
			this.modsListBox.Name = "modsListBox";
			this.modsListBox.Size = new global::System.Drawing.Size(350, 379);
			this.modsListBox.TabIndex = 21;
			this.modsListBox.ItemCheck += new global::System.Windows.Forms.ItemCheckEventHandler(this.modsListBox_ItemCheck);
			this.modsListBox.SelectedIndexChanged += new global::System.EventHandler(this.modsListBox_SelectedIndexChanged);
			this.saveButton.Location = new global::System.Drawing.Point(15, 426);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new global::System.Drawing.Size(350, 23);
			this.saveButton.TabIndex = 22;
			this.saveButton.Text = "Install Mods To Game";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new global::System.EventHandler(this.saveButton_Click);
			this.titleTextBox.Location = new global::System.Drawing.Point(437, 25);
			this.titleTextBox.Name = "titleTextBox";
			this.titleTextBox.ReadOnly = true;
			this.titleTextBox.Size = new global::System.Drawing.Size(341, 20);
			this.titleTextBox.TabIndex = 23;
			this.descriptionTextBox.Location = new global::System.Drawing.Point(437, 102);
			this.descriptionTextBox.Multiline = true;
			this.descriptionTextBox.Name = "descriptionTextBox";
			this.descriptionTextBox.ReadOnly = true;
			this.descriptionTextBox.Size = new global::System.Drawing.Size(341, 131);
			this.descriptionTextBox.TabIndex = 24;
			this.thumbnailPictureBox.BackColor = global::System.Drawing.Color.FromArgb(50, 50, 50);
			this.thumbnailPictureBox.Location = new global::System.Drawing.Point(506, 249);
			this.thumbnailPictureBox.Name = "thumbnailPictureBox";
			this.thumbnailPictureBox.Size = new global::System.Drawing.Size(200, 200);
			this.thumbnailPictureBox.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.thumbnailPictureBox.TabIndex = 25;
			this.thumbnailPictureBox.TabStop = false;
			this.authorTextBox.Location = new global::System.Drawing.Point(437, 65);
			this.authorTextBox.Name = "authorTextBox";
			this.authorTextBox.ReadOnly = true;
			this.authorTextBox.Size = new global::System.Drawing.Size(341, 20);
			this.authorTextBox.TabIndex = 26;
			this.label1.AutoSize = true;
			this.label1.ForeColor = global::System.Drawing.Color.Silver;
			this.label1.Location = new global::System.Drawing.Point(434, 9);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(27, 13);
			this.label1.TabIndex = 27;
			this.label1.Text = "Title";
			this.label3.AutoSize = true;
			this.label3.ForeColor = global::System.Drawing.Color.Silver;
			this.label3.Location = new global::System.Drawing.Point(434, 49);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(38, 13);
			this.label3.TabIndex = 28;
			this.label3.Text = "Author";
			this.label4.AutoSize = true;
			this.label4.ForeColor = global::System.Drawing.Color.Silver;
			this.label4.Location = new global::System.Drawing.Point(434, 88);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(60, 13);
			this.label4.TabIndex = 29;
			this.label4.Text = "Description";
			this.loadOrderUpButton.Location = new global::System.Drawing.Point(371, 125);
			this.loadOrderUpButton.Name = "loadOrderUpButton";
			this.loadOrderUpButton.Size = new global::System.Drawing.Size(19, 64);
			this.loadOrderUpButton.TabIndex = 30;
			this.loadOrderUpButton.Text = "u";
			this.loadOrderUpButton.UseVisualStyleBackColor = true;
			this.loadOrderUpButton.Click += new global::System.EventHandler(this.loadOrderUpButton_Click);
			this.loadOrderDownButton.Location = new global::System.Drawing.Point(371, 195);
			this.loadOrderDownButton.Name = "loadOrderDownButton";
			this.loadOrderDownButton.Size = new global::System.Drawing.Size(19, 64);
			this.loadOrderDownButton.TabIndex = 31;
			this.loadOrderDownButton.Text = "d";
			this.loadOrderDownButton.UseVisualStyleBackColor = true;
			this.loadOrderDownButton.Click += new global::System.EventHandler(this.loadOrderDownButton_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.Color.FromArgb(40, 40, 40);
			base.ClientSize = new global::System.Drawing.Size(817, 460);
			base.Controls.Add(this.loadOrderDownButton);
			base.Controls.Add(this.loadOrderUpButton);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.authorTextBox);
			base.Controls.Add(this.thumbnailPictureBox);
			base.Controls.Add(this.descriptionTextBox);
			base.Controls.Add(this.titleTextBox);
			base.Controls.Add(this.saveButton);
			base.Controls.Add(this.modsListBox);
			base.Controls.Add(this.installButton);
			base.Controls.Add(this.uninstallButton);
			base.Controls.Add(this.label2);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ModManagerForm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Mod Manager";
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.ModManagerForm_FormClosing);
			base.Load += new global::System.EventHandler(this.ModManagerForm_Load);
			((global::System.ComponentModel.ISupportInitialize)this.thumbnailPictureBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000063 RID: 99
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000064 RID: 100
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000065 RID: 101
		private global::System.Windows.Forms.Button uninstallButton;

		// Token: 0x04000066 RID: 102
		private global::System.Windows.Forms.Button installButton;

		// Token: 0x04000067 RID: 103
		private global::System.Windows.Forms.CheckedListBox modsListBox;

		// Token: 0x04000068 RID: 104
		private global::System.Windows.Forms.Button saveButton;

		// Token: 0x04000069 RID: 105
		private global::System.Windows.Forms.TextBox titleTextBox;

		// Token: 0x0400006A RID: 106
		private global::System.Windows.Forms.TextBox descriptionTextBox;

		// Token: 0x0400006B RID: 107
		private global::System.Windows.Forms.PictureBox thumbnailPictureBox;

		// Token: 0x0400006C RID: 108
		private global::System.Windows.Forms.TextBox authorTextBox;

		// Token: 0x0400006D RID: 109
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400006E RID: 110
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400006F RID: 111
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000070 RID: 112
		private global::System.Windows.Forms.Button loadOrderUpButton;

		// Token: 0x04000071 RID: 113
		private global::System.Windows.Forms.Button loadOrderDownButton;
	}
}
