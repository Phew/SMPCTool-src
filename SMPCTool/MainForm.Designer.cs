namespace SMPCTool
{
	// Token: 0x0200000B RID: 11
	public partial class MainForm : global::System.Windows.Forms.Form
	{
		// Token: 0x0600006F RID: 111 RVA: 0x000092DC File Offset: 0x000074DC
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00009314 File Offset: 0x00007514
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::SMPCTool.MainForm));
			this.menuStrip1 = new global::System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.selectAssetArchiveFolderToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new global::System.Windows.Forms.ToolStripSeparator();
			this.saveProjectToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.loadProjectToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.searchToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.findToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.hashGeneratorToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.generateLayoutCSVToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.getNamedHashFilesCountToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.textureToolToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.createModToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.modManagerToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.joinDiscordServerToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.archiveTreeView = new global::System.Windows.Forms.TreeView();
			this.contextMenuStrip2 = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.extractFolderToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.fileListView = new global::System.Windows.Forms.ListView();
			this.fileNameHeader = new global::System.Windows.Forms.ColumnHeader();
			this.sizeHeader = new global::System.Windows.Forms.ColumnHeader();
			this.typeHeader = new global::System.Windows.Forms.ColumnHeader();
			this.contextMenuStrip1 = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.exportAssetToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.replaceAssetToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new global::System.Windows.Forms.ToolStripSeparator();
			this.assetInformationToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.launchGameWithModsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.contextMenuStrip2.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.menuStrip1.BackColor = global::System.Drawing.Color.FromArgb(90, 90, 90);
			this.menuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.fileToolStripMenuItem,
				this.searchToolStripMenuItem,
				this.toolsToolStripMenuItem,
				this.createModToolStripMenuItem,
				this.modManagerToolStripMenuItem,
				this.launchGameWithModsToolStripMenuItem,
				this.joinDiscordServerToolStripMenuItem,
				this.aboutToolStripMenuItem
			});
			this.menuStrip1.Location = new global::System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new global::System.Drawing.Size(837, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			this.fileToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.selectAssetArchiveFolderToolStripMenuItem,
				this.toolStripSeparator3,
				this.saveProjectToolStripMenuItem,
				this.loadProjectToolStripMenuItem,
				this.toolStripSeparator1,
				this.exitToolStripMenuItem
			});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new global::System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			this.selectAssetArchiveFolderToolStripMenuItem.Name = "selectAssetArchiveFolderToolStripMenuItem";
			this.selectAssetArchiveFolderToolStripMenuItem.Size = new global::System.Drawing.Size(224, 22);
			this.selectAssetArchiveFolderToolStripMenuItem.Text = "Select Asset Archive Folder...";
			this.selectAssetArchiveFolderToolStripMenuItem.Click += new global::System.EventHandler(this.selectAssetArchiveFolderToolStripMenuItem_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new global::System.Drawing.Size(221, 6);
			this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
			this.saveProjectToolStripMenuItem.Size = new global::System.Drawing.Size(224, 22);
			this.saveProjectToolStripMenuItem.Text = "Save Project...";
			this.saveProjectToolStripMenuItem.Click += new global::System.EventHandler(this.saveProjectToolStripMenuItem_Click);
			this.loadProjectToolStripMenuItem.Name = "loadProjectToolStripMenuItem";
			this.loadProjectToolStripMenuItem.Size = new global::System.Drawing.Size(224, 22);
			this.loadProjectToolStripMenuItem.Text = "Load Project...";
			this.loadProjectToolStripMenuItem.Click += new global::System.EventHandler(this.loadProjectToolStripMenuItem_Click);
			this.toolStripSeparator1.ForeColor = global::System.Drawing.SystemColors.ControlText;
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(221, 6);
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new global::System.Drawing.Size(224, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new global::System.EventHandler(this.exitToolStripMenuItem_Click);
			this.searchToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.findToolStripMenuItem
			});
			this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
			this.searchToolStripMenuItem.Size = new global::System.Drawing.Size(54, 20);
			this.searchToolStripMenuItem.Text = "Search";
			this.findToolStripMenuItem.Name = "findToolStripMenuItem";
			this.findToolStripMenuItem.Size = new global::System.Drawing.Size(106, 22);
			this.findToolStripMenuItem.Text = "Find...";
			this.findToolStripMenuItem.Click += new global::System.EventHandler(this.findToolStripMenuItem_Click);
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.hashGeneratorToolStripMenuItem,
				this.generateLayoutCSVToolStripMenuItem,
				this.getNamedHashFilesCountToolStripMenuItem,
				this.textureToolToolStripMenuItem
			});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new global::System.Drawing.Size(46, 20);
			this.toolsToolStripMenuItem.Text = "Tools";
			this.hashGeneratorToolStripMenuItem.Name = "hashGeneratorToolStripMenuItem";
			this.hashGeneratorToolStripMenuItem.Size = new global::System.Drawing.Size(226, 22);
			this.hashGeneratorToolStripMenuItem.Text = "String To Hash Generator";
			this.hashGeneratorToolStripMenuItem.Click += new global::System.EventHandler(this.hashGeneratorToolStripMenuItem_Click);
			this.generateLayoutCSVToolStripMenuItem.Name = "generateLayoutCSVToolStripMenuItem";
			this.generateLayoutCSVToolStripMenuItem.Size = new global::System.Drawing.Size(226, 22);
			this.generateLayoutCSVToolStripMenuItem.Text = "Generate Layout CSV";
			this.generateLayoutCSVToolStripMenuItem.Click += new global::System.EventHandler(this.generateLayoutCSVToolStripMenuItem_Click);
			this.getNamedHashFilesCountToolStripMenuItem.Name = "getNamedHashFilesCountToolStripMenuItem";
			this.getNamedHashFilesCountToolStripMenuItem.Size = new global::System.Drawing.Size(226, 22);
			this.getNamedHashFilesCountToolStripMenuItem.Text = "Get Named Hash Files Count";
			this.getNamedHashFilesCountToolStripMenuItem.Click += new global::System.EventHandler(this.getNamedHashFilesCountToolStripMenuItem_Click);
			this.textureToolToolStripMenuItem.Name = "textureToolToolStripMenuItem";
			this.textureToolToolStripMenuItem.Size = new global::System.Drawing.Size(226, 22);
			this.textureToolToolStripMenuItem.Text = "Texture Tool (BETA)";
			this.textureToolToolStripMenuItem.Click += new global::System.EventHandler(this.textureToolToolStripMenuItem_Click);
			this.createModToolStripMenuItem.Name = "createModToolStripMenuItem";
			this.createModToolStripMenuItem.Size = new global::System.Drawing.Size(81, 20);
			this.createModToolStripMenuItem.Text = "Create Mod";
			this.createModToolStripMenuItem.Click += new global::System.EventHandler(this.createModToolStripMenuItem_Click);
			this.modManagerToolStripMenuItem.Name = "modManagerToolStripMenuItem";
			this.modManagerToolStripMenuItem.Size = new global::System.Drawing.Size(94, 20);
			this.modManagerToolStripMenuItem.Text = "Mod Manager";
			this.modManagerToolStripMenuItem.Click += new global::System.EventHandler(this.modManagerToolStripMenuItem_Click);
			this.joinDiscordServerToolStripMenuItem.Name = "joinDiscordServerToolStripMenuItem";
			this.joinDiscordServerToolStripMenuItem.Size = new global::System.Drawing.Size(118, 20);
			this.joinDiscordServerToolStripMenuItem.Text = "Join Discord Server";
			this.joinDiscordServerToolStripMenuItem.Click += new global::System.EventHandler(this.joinDiscordServerToolStripMenuItem_Click);
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new global::System.Drawing.Size(52, 20);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new global::System.EventHandler(this.aboutToolStripMenuItem_Click);
			this.archiveTreeView.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.archiveTreeView.BackColor = global::System.Drawing.Color.FromArgb(50, 50, 50);
			this.archiveTreeView.ContextMenuStrip = this.contextMenuStrip2;
			this.archiveTreeView.ForeColor = global::System.Drawing.Color.Silver;
			this.archiveTreeView.HideSelection = false;
			this.archiveTreeView.Location = new global::System.Drawing.Point(12, 36);
			this.archiveTreeView.Name = "archiveTreeView";
			this.archiveTreeView.Size = new global::System.Drawing.Size(264, 473);
			this.archiveTreeView.TabIndex = 1;
			this.archiveTreeView.AfterSelect += new global::System.Windows.Forms.TreeViewEventHandler(this.archiveTreeView_AfterSelect);
			this.archiveTreeView.DoubleClick += new global::System.EventHandler(this.archiveTreeView_DoubleClick);
			this.contextMenuStrip2.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.extractFolderToolStripMenuItem
			});
			this.contextMenuStrip2.Name = "contextMenuStrip2";
			this.contextMenuStrip2.Size = new global::System.Drawing.Size(156, 26);
			this.extractFolderToolStripMenuItem.Name = "extractFolderToolStripMenuItem";
			this.extractFolderToolStripMenuItem.Size = new global::System.Drawing.Size(155, 22);
			this.extractFolderToolStripMenuItem.Text = "Extract Folder...";
			this.extractFolderToolStripMenuItem.Click += new global::System.EventHandler(this.extractFolderToolStripMenuItem_Click);
			this.fileListView.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.fileListView.BackColor = global::System.Drawing.Color.FromArgb(50, 50, 50);
			this.fileListView.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.fileNameHeader,
				this.sizeHeader,
				this.typeHeader
			});
			this.fileListView.ContextMenuStrip = this.contextMenuStrip1;
			this.fileListView.ForeColor = global::System.Drawing.Color.Silver;
			this.fileListView.HideSelection = false;
			this.fileListView.Location = new global::System.Drawing.Point(283, 36);
			this.fileListView.MultiSelect = false;
			this.fileListView.Name = "fileListView";
			this.fileListView.Size = new global::System.Drawing.Size(542, 473);
			this.fileListView.TabIndex = 2;
			this.fileListView.UseCompatibleStateImageBehavior = false;
			this.fileListView.View = global::System.Windows.Forms.View.Details;
			this.fileListView.ColumnClick += new global::System.Windows.Forms.ColumnClickEventHandler(this.fileListView_ColumnClick);
			this.fileListView.DoubleClick += new global::System.EventHandler(this.fileListView_DoubleClick);
			this.fileNameHeader.Text = "Filename";
			this.fileNameHeader.Width = 250;
			this.sizeHeader.Text = "Size";
			this.sizeHeader.Width = 100;
			this.typeHeader.Text = "Asset Type";
			this.typeHeader.Width = 150;
			this.contextMenuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.exportAssetToolStripMenuItem,
				this.replaceAssetToolStripMenuItem,
				this.toolStripSeparator2,
				this.assetInformationToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new global::System.Drawing.Size(197, 76);
			this.exportAssetToolStripMenuItem.Name = "exportAssetToolStripMenuItem";
			this.exportAssetToolStripMenuItem.Size = new global::System.Drawing.Size(196, 22);
			this.exportAssetToolStripMenuItem.Text = "Extract Asset...";
			this.exportAssetToolStripMenuItem.Click += new global::System.EventHandler(this.exportAssetToolStripMenuItem_Click);
			this.replaceAssetToolStripMenuItem.Name = "replaceAssetToolStripMenuItem";
			this.replaceAssetToolStripMenuItem.Size = new global::System.Drawing.Size(196, 22);
			this.replaceAssetToolStripMenuItem.Text = "Replace Asset In Mod...";
			this.replaceAssetToolStripMenuItem.Click += new global::System.EventHandler(this.replaceAssetToolStripMenuItem_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new global::System.Drawing.Size(193, 6);
			this.assetInformationToolStripMenuItem.Name = "assetInformationToolStripMenuItem";
			this.assetInformationToolStripMenuItem.Size = new global::System.Drawing.Size(196, 22);
			this.assetInformationToolStripMenuItem.Text = "Asset Information";
			this.assetInformationToolStripMenuItem.Click += new global::System.EventHandler(this.assetInformationToolStripMenuItem_Click);
			this.launchGameWithModsToolStripMenuItem.Name = "launchGameWithModsToolStripMenuItem";
			this.launchGameWithModsToolStripMenuItem.Size = new global::System.Drawing.Size(140, 20);
			this.launchGameWithModsToolStripMenuItem.Text = "Launch Game Modded";
			this.launchGameWithModsToolStripMenuItem.Click += new global::System.EventHandler(this.launchGameWithModsToolStripMenuItem_Click_1);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.Color.FromArgb(40, 40, 40);
			base.ClientSize = new global::System.Drawing.Size(837, 521);
			base.Controls.Add(this.fileListView);
			base.Controls.Add(this.archiveTreeView);
			base.Controls.Add(this.menuStrip1);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MainMenuStrip = this.menuStrip1;
			base.Name = "MainForm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Spider-Man PC Tool";
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			base.Load += new global::System.EventHandler(this.MainForm_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.contextMenuStrip2.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000043 RID: 67
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000044 RID: 68
		private global::System.Windows.Forms.MenuStrip menuStrip1;

		// Token: 0x04000045 RID: 69
		private global::System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;

		// Token: 0x04000046 RID: 70
		private global::System.Windows.Forms.ToolStripMenuItem selectAssetArchiveFolderToolStripMenuItem;

		// Token: 0x04000047 RID: 71
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x04000048 RID: 72
		private global::System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;

		// Token: 0x04000049 RID: 73
		private global::System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;

		// Token: 0x0400004A RID: 74
		private global::System.Windows.Forms.ToolStripMenuItem generateLayoutCSVToolStripMenuItem;

		// Token: 0x0400004B RID: 75
		private global::System.Windows.Forms.TreeView archiveTreeView;

		// Token: 0x0400004C RID: 76
		private global::System.Windows.Forms.ListView fileListView;

		// Token: 0x0400004D RID: 77
		private global::System.Windows.Forms.ColumnHeader fileNameHeader;

		// Token: 0x0400004E RID: 78
		private global::System.Windows.Forms.ColumnHeader sizeHeader;

		// Token: 0x0400004F RID: 79
		private global::System.Windows.Forms.ColumnHeader typeHeader;

		// Token: 0x04000050 RID: 80
		private global::System.Windows.Forms.ToolStripMenuItem hashGeneratorToolStripMenuItem;

		// Token: 0x04000051 RID: 81
		private global::System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;

		// Token: 0x04000052 RID: 82
		private global::System.Windows.Forms.ToolStripMenuItem joinDiscordServerToolStripMenuItem;

		// Token: 0x04000053 RID: 83
		private global::System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;

		// Token: 0x04000054 RID: 84
		private global::System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;

		// Token: 0x04000055 RID: 85
		private global::System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

		// Token: 0x04000056 RID: 86
		private global::System.Windows.Forms.ToolStripMenuItem exportAssetToolStripMenuItem;

		// Token: 0x04000057 RID: 87
		private global::System.Windows.Forms.ToolStripMenuItem replaceAssetToolStripMenuItem;

		// Token: 0x04000058 RID: 88
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

		// Token: 0x04000059 RID: 89
		private global::System.Windows.Forms.ToolStripMenuItem assetInformationToolStripMenuItem;

		// Token: 0x0400005A RID: 90
		private global::System.Windows.Forms.ToolStripMenuItem createModToolStripMenuItem;

		// Token: 0x0400005B RID: 91
		private global::System.Windows.Forms.ToolStripMenuItem getNamedHashFilesCountToolStripMenuItem;

		// Token: 0x0400005C RID: 92
		private global::System.Windows.Forms.ToolStripMenuItem textureToolToolStripMenuItem;

		// Token: 0x0400005D RID: 93
		private global::System.Windows.Forms.ToolStripMenuItem modManagerToolStripMenuItem;

		// Token: 0x0400005E RID: 94
		private global::System.Windows.Forms.ContextMenuStrip contextMenuStrip2;

		// Token: 0x0400005F RID: 95
		private global::System.Windows.Forms.ToolStripMenuItem extractFolderToolStripMenuItem;

		// Token: 0x04000060 RID: 96
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator3;

		// Token: 0x04000061 RID: 97
		private global::System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;

		// Token: 0x04000062 RID: 98
		private global::System.Windows.Forms.ToolStripMenuItem loadProjectToolStripMenuItem;

		// Token: 0x04000063 RID: 99
		private global::System.Windows.Forms.ToolStripMenuItem launchGameWithModsToolStripMenuItem;
	}
}
