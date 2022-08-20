namespace SMPCTool
{
	// Token: 0x0200000E RID: 14
	public partial class SearchFindForm : global::System.Windows.Forms.Form
	{
		// Token: 0x0600008E RID: 142 RVA: 0x0000C040 File Offset: 0x0000A240
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000C078 File Offset: 0x0000A278
		private void InitializeComponent()
		{
			this.searchTextbox = new global::System.Windows.Forms.TextBox();
			this.resultsListBox = new global::System.Windows.Forms.ListBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.searchButton = new global::System.Windows.Forms.Button();
			this.filtersTextBox = new global::System.Windows.Forms.TextBox();
			this.filtersLabel = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.searchTextbox.Location = new global::System.Drawing.Point(12, 25);
			this.searchTextbox.Name = "searchTextbox";
			this.searchTextbox.Size = new global::System.Drawing.Size(506, 20);
			this.searchTextbox.TabIndex = 0;
			this.resultsListBox.FormattingEnabled = true;
			this.resultsListBox.HorizontalScrollbar = true;
			this.resultsListBox.Location = new global::System.Drawing.Point(12, 141);
			this.resultsListBox.Name = "resultsListBox";
			this.resultsListBox.Size = new global::System.Drawing.Size(506, 225);
			this.resultsListBox.TabIndex = 1;
			this.resultsListBox.DoubleClick += new global::System.EventHandler(this.resultsListBox_DoubleClick);
			this.label1.AutoSize = true;
			this.label1.ForeColor = global::System.Drawing.Color.Silver;
			this.label1.Location = new global::System.Drawing.Point(9, 9);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(62, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Search For:";
			this.label2.AutoSize = true;
			this.label2.ForeColor = global::System.Drawing.Color.Silver;
			this.label2.Location = new global::System.Drawing.Point(9, 125);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(45, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Results:";
			this.searchButton.Location = new global::System.Drawing.Point(13, 99);
			this.searchButton.Name = "searchButton";
			this.searchButton.Size = new global::System.Drawing.Size(507, 23);
			this.searchButton.TabIndex = 4;
			this.searchButton.Text = "SEARCH";
			this.searchButton.UseVisualStyleBackColor = true;
			this.searchButton.Click += new global::System.EventHandler(this.searchButton_Click);
			this.filtersTextBox.Location = new global::System.Drawing.Point(12, 64);
			this.filtersTextBox.Name = "filtersTextBox";
			this.filtersTextBox.Size = new global::System.Drawing.Size(506, 20);
			this.filtersTextBox.TabIndex = 5;
			this.filtersLabel.AutoSize = true;
			this.filtersLabel.ForeColor = global::System.Drawing.Color.Silver;
			this.filtersLabel.Location = new global::System.Drawing.Point(10, 48);
			this.filtersLabel.Name = "filtersLabel";
			this.filtersLabel.Size = new global::System.Drawing.Size(37, 13);
			this.filtersLabel.TabIndex = 6;
			this.filtersLabel.Text = "Filters:";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.Color.FromArgb(40, 40, 40);
			base.ClientSize = new global::System.Drawing.Size(531, 378);
			base.Controls.Add(this.filtersLabel);
			base.Controls.Add(this.filtersTextBox);
			base.Controls.Add(this.searchButton);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.resultsListBox);
			base.Controls.Add(this.searchTextbox);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SearchFindForm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Find...";
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.SearchFindForm_FormClosed);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000073 RID: 115
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000074 RID: 116
		private global::System.Windows.Forms.TextBox searchTextbox;

		// Token: 0x04000075 RID: 117
		private global::System.Windows.Forms.ListBox resultsListBox;

		// Token: 0x04000076 RID: 118
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000077 RID: 119
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000078 RID: 120
		private global::System.Windows.Forms.Button searchButton;

		// Token: 0x04000079 RID: 121
		private global::System.Windows.Forms.TextBox filtersTextBox;

		// Token: 0x0400007A RID: 122
		private global::System.Windows.Forms.Label filtersLabel;
	}
}
