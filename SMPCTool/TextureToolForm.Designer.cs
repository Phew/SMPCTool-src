namespace SMPCTool
{
	// Token: 0x02000014 RID: 20
	public partial class TextureToolForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000095 RID: 149 RVA: 0x0000D350 File Offset: 0x0000B550
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000D388 File Offset: 0x0000B588
		private void InitializeComponent()
		{
			this.menuStrip1 = new global::System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.openTextureToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.saveTextureAssetToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.textureBox = new global::System.Windows.Forms.PictureBox();
			this.menuStrip1.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.textureBox).BeginInit();
			base.SuspendLayout();
			this.menuStrip1.BackColor = global::System.Drawing.Color.FromArgb(90, 90, 90);
			this.menuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.fileToolStripMenuItem
			});
			this.menuStrip1.Location = new global::System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new global::System.Drawing.Size(800, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			this.fileToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.openTextureToolStripMenuItem,
				this.saveTextureAssetToolStripMenuItem
			});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new global::System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			this.openTextureToolStripMenuItem.Name = "openTextureToolStripMenuItem";
			this.openTextureToolStripMenuItem.Size = new global::System.Drawing.Size(184, 22);
			this.openTextureToolStripMenuItem.Text = "Open Texture Asset...";
			this.openTextureToolStripMenuItem.Click += new global::System.EventHandler(this.openTextureToolStripMenuItem_Click);
			this.saveTextureAssetToolStripMenuItem.Name = "saveTextureAssetToolStripMenuItem";
			this.saveTextureAssetToolStripMenuItem.Size = new global::System.Drawing.Size(184, 22);
			this.saveTextureAssetToolStripMenuItem.Text = "Save Texture Asset";
			this.saveTextureAssetToolStripMenuItem.Click += new global::System.EventHandler(this.saveTextureAssetToolStripMenuItem_Click);
			this.textureBox.BackColor = global::System.Drawing.Color.FromArgb(50, 50, 50);
			this.textureBox.Location = new global::System.Drawing.Point(12, 36);
			this.textureBox.Name = "textureBox";
			this.textureBox.Size = new global::System.Drawing.Size(775, 466);
			this.textureBox.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.textureBox.TabIndex = 1;
			this.textureBox.TabStop = false;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.Color.FromArgb(40, 40, 40);
			base.ClientSize = new global::System.Drawing.Size(800, 514);
			base.Controls.Add(this.textureBox);
			base.Controls.Add(this.menuStrip1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MainMenuStrip = this.menuStrip1;
			base.Name = "TextureToolForm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Texture Tool";
			base.Load += new global::System.EventHandler(this.TextureToolForm_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.textureBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400008A RID: 138
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x0400008B RID: 139
		private global::System.Windows.Forms.MenuStrip menuStrip1;

		// Token: 0x0400008C RID: 140
		private global::System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;

		// Token: 0x0400008D RID: 141
		private global::System.Windows.Forms.ToolStripMenuItem openTextureToolStripMenuItem;

		// Token: 0x0400008E RID: 142
		private global::System.Windows.Forms.PictureBox textureBox;

		// Token: 0x0400008F RID: 143
		private global::System.Windows.Forms.ToolStripMenuItem saveTextureAssetToolStripMenuItem;
	}
}
