namespace MonetDB.GUI.Tools
{
    partial class ExploreForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(ExploreForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.trwSchema = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tbxQuery = new System.Windows.Forms.TextBox();
            this.tbcResult = new System.Windows.Forms.TabControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblElapsed = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblRows = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.dumpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sqlConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bulkLoaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 36);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.trwSchema);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(984, 726);
            this.splitContainer1.SplitterDistance = 270;
            this.splitContainer1.TabIndex = 0;
            // 
            // trwSchema
            // 
            this.trwSchema.AllowDrop = true;
            this.trwSchema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trwSchema.Location = new System.Drawing.Point(0, 0);
            this.trwSchema.Name = "trwSchema";
            this.trwSchema.Size = new System.Drawing.Size(270, 726);
            this.trwSchema.TabIndex = 0;
            this.trwSchema.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.trwSchema_ItemDrag);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tbxQuery);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tbcResult);
            this.splitContainer2.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer2.Size = new System.Drawing.Size(710, 726);
            this.splitContainer2.SplitterDistance = 354;
            this.splitContainer2.TabIndex = 0;
            // 
            // tbxQuery
            // 
            this.tbxQuery.AllowDrop = true;
            this.tbxQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbxQuery.ForeColor = System.Drawing.Color.Blue;
            this.tbxQuery.Location = new System.Drawing.Point(0, 0);
            this.tbxQuery.Multiline = true;
            this.tbxQuery.Name = "tbxQuery";
            this.tbxQuery.Size = new System.Drawing.Size(710, 354);
            this.tbxQuery.TabIndex = 0;
            this.tbxQuery.Text = resources.GetString("tbxQuery.Text");
            this.tbxQuery.DragDrop += new System.Windows.Forms.DragEventHandler(this.tbxQuery_DragDrop);
            this.tbxQuery.DragEnter += new System.Windows.Forms.DragEventHandler(this.tbxQuery_DragEnter);
            // 
            // tbcResult
            // 
            this.tbcResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcResult.Location = new System.Drawing.Point(0, 0);
            this.tbcResult.Name = "tbcResult";
            this.tbcResult.SelectedIndex = 0;
            this.tbcResult.Size = new System.Drawing.Size(710, 343);
            this.tbcResult.TabIndex = 1;
            this.tbcResult.SelectedIndexChanged += new System.EventHandler(this.tbcResult_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblElapsed,
            this.toolStripStatusLabel1,
            this.lblRows});
            this.statusStrip1.Location = new System.Drawing.Point(0, 343);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(710, 25);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblElapsed
            // 
            this.lblElapsed.Name = "lblElapsed";
            this.lblElapsed.Size = new System.Drawing.Size(63, 20);
            this.lblElapsed.Text = "00:00:00";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(13, 20);
            this.toolStripStatusLabel1.Text = "|";
            // 
            // lblRows
            // 
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(52, 20);
            this.lblRows.Text = "0 rows";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 36);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restoreToolStripMenuItem,
            this.toolStripSeparator,
            this.dumpToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 32);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("restoreToolStripMenuItem.Image")));
            this.restoreToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(227, 32);
            this.restoreToolStripMenuItem.Text = "&Restore";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(224, 6);
            // 
            // dumpToolStripMenuItem
            // 
            this.dumpToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("dumpToolStripMenuItem.Image")));
            this.dumpToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dumpToolStripMenuItem.Name = "dumpToolStripMenuItem";
            this.dumpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.dumpToolStripMenuItem.Size = new System.Drawing.Size(227, 32);
            this.dumpToolStripMenuItem.Text = "&Dump";
            this.dumpToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sqlConsoleToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(58, 32);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // sqlConsoleToolStripMenuItem
            // 
            this.sqlConsoleToolStripMenuItem.Image = global::MonetDB.GUI.Properties.Resources.sqlcmd;
            this.sqlConsoleToolStripMenuItem.Name = "sqlConsoleToolStripMenuItem";
            this.sqlConsoleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.sqlConsoleToolStripMenuItem.Size = new System.Drawing.Size(262, 32);
            this.sqlConsoleToolStripMenuItem.Text = "Sql console";
            this.sqlConsoleToolStripMenuItem.Click += new System.EventHandler(this.sqlConsoleToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.executeToolStripMenuItem,
            this.toolStripSeparator1,
            this.bulkLoaderToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(69, 32);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // executeToolStripMenuItem
            // 
            this.executeToolStripMenuItem.Name = "executeToolStripMenuItem";
            this.executeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.executeToolStripMenuItem.Size = new System.Drawing.Size(260, 32);
            this.executeToolStripMenuItem.Text = "Execute";
            this.executeToolStripMenuItem.Click += new System.EventHandler(this.executeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(257, 6);
            // 
            // bulkLoaderToolStripMenuItem
            // 
            this.bulkLoaderToolStripMenuItem.Name = "bulkLoaderToolStripMenuItem";
            this.bulkLoaderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.bulkLoaderToolStripMenuItem.Size = new System.Drawing.Size(260, 32);
            this.bulkLoaderToolStripMenuItem.Text = "Bulk Loader";
            this.bulkLoaderToolStripMenuItem.Click += new System.EventHandler(this.bulkLoaderToolStripMenuItem_Click);
            // 
            // ExploreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 762);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ExploreForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Explore";
            this.Load += new System.EventHandler(this.ExploreForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView trwSchema;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem dumpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem executeToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl tbcResult;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblElapsed;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem bulkLoaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblRows;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sqlConsoleToolStripMenuItem;
        private System.Windows.Forms.TextBox tbxQuery;

    }
}