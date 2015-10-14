namespace MonetDB.GUI.Tools
{
    internal partial class BulkLoaderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BulkLoaderForm));
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lsvSQLCols = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSQLConnect = new System.Windows.Forms.Button();
            this.cbxSQLSchema = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rtbxSQLConnectionString = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblCopyElapsed = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbrCopyStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.nudBatchSize = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxMonetSchema = new System.Windows.Forms.TextBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBatchSize)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 6);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 6);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(23, 23);
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(23, 23);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(23, 23);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(23, 23);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Column";
            this.columnHeader1.Width = 300;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Data type";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 200;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Nullable";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 200;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(1386, 621);
            this.splitContainer1.SplitterDistance = 807;
            this.splitContainer1.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lsvSQLCols);
            this.groupBox1.Controls.Add(this.btnSQLConnect);
            this.groupBox1.Controls.Add(this.cbxSQLSchema);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rtbxSQLConnectionString);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(807, 621);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source SQL Server DB";
            // 
            // lsvSQLCols
            // 
            this.lsvSQLCols.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvSQLCols.CheckBoxes = true;
            this.lsvSQLCols.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lsvSQLCols.GridLines = true;
            this.lsvSQLCols.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lsvSQLCols.LabelEdit = true;
            this.lsvSQLCols.Location = new System.Drawing.Point(37, 308);
            this.lsvSQLCols.Name = "lsvSQLCols";
            this.lsvSQLCols.ShowItemToolTips = true;
            this.lsvSQLCols.Size = new System.Drawing.Size(745, 269);
            this.lsvSQLCols.TabIndex = 5;
            this.lsvSQLCols.UseCompatibleStateImageBehavior = false;
            this.lsvSQLCols.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Column";
            this.columnHeader4.Width = 260;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Data Type";
            this.columnHeader5.Width = 160;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Nullable";
            this.columnHeader6.Width = 160;
            // 
            // btnSQLConnect
            // 
            this.btnSQLConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSQLConnect.Location = new System.Drawing.Point(661, 61);
            this.btnSQLConnect.Margin = new System.Windows.Forms.Padding(5);
            this.btnSQLConnect.Name = "btnSQLConnect";
            this.btnSQLConnect.Size = new System.Drawing.Size(122, 44);
            this.btnSQLConnect.TabIndex = 4;
            this.btnSQLConnect.Text = "Connect";
            this.btnSQLConnect.UseVisualStyleBackColor = true;
            this.btnSQLConnect.Click += new System.EventHandler(this.btnSQLConnect_Click);
            // 
            // cbxSQLSchema
            // 
            this.cbxSQLSchema.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxSQLSchema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSQLSchema.FormattingEnabled = true;
            this.cbxSQLSchema.Location = new System.Drawing.Point(38, 225);
            this.cbxSQLSchema.Margin = new System.Windows.Forms.Padding(5);
            this.cbxSQLSchema.Name = "cbxSQLSchema";
            this.cbxSQLSchema.Size = new System.Drawing.Size(613, 33);
            this.cbxSQLSchema.TabIndex = 3;
            this.cbxSQLSchema.SelectedIndexChanged += new System.EventHandler(this.cbxSQLSchema_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 280);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 25);
            this.label6.TabIndex = 2;
            this.label6.Text = "Table column:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 195);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Schema table:";
            // 
            // rtbxSQLConnectionString
            // 
            this.rtbxSQLConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbxSQLConnectionString.AutoWordSelection = true;
            this.rtbxSQLConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbxSQLConnectionString.ForeColor = System.Drawing.Color.Black;
            this.rtbxSQLConnectionString.Location = new System.Drawing.Point(37, 62);
            this.rtbxSQLConnectionString.Margin = new System.Windows.Forms.Padding(5);
            this.rtbxSQLConnectionString.Name = "rtbxSQLConnectionString";
            this.rtbxSQLConnectionString.Size = new System.Drawing.Size(614, 116);
            this.rtbxSQLConnectionString.TabIndex = 1;
            this.rtbxSQLConnectionString.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connection string:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.statusStrip1);
            this.groupBox2.Controls.Add(this.nudBatchSize);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tbxMonetSchema);
            this.groupBox2.Controls.Add(this.btnCopy);
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox2.Size = new System.Drawing.Size(575, 621);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Destination MonetDB";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pbrCopyStatus,
            this.lblCopyElapsed});
            this.statusStrip1.Location = new System.Drawing.Point(5, 583);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 22, 0);
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(565, 33);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblCopyElapsed
            // 
            this.lblCopyElapsed.Name = "lblCopyElapsed";
            this.lblCopyElapsed.Size = new System.Drawing.Size(86, 28);
            this.lblCopyElapsed.Text = "00:00:00";
            // 
            // pbrCopyStatus
            // 
            this.pbrCopyStatus.Name = "pbrCopyStatus";
            this.pbrCopyStatus.Size = new System.Drawing.Size(100, 27);
            this.pbrCopyStatus.Step = 1;
            this.pbrCopyStatus.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // nudBatchSize
            // 
            this.nudBatchSize.Location = new System.Drawing.Point(28, 158);
            this.nudBatchSize.Margin = new System.Windows.Forms.Padding(5);
            this.nudBatchSize.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudBatchSize.Name = "nudBatchSize";
            this.nudBatchSize.Size = new System.Drawing.Size(180, 30);
            this.nudBatchSize.TabIndex = 8;
            this.nudBatchSize.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 125);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 25);
            this.label5.TabIndex = 7;
            this.label5.Text = "Batch size:";
            // 
            // tbxMonetSchema
            // 
            this.tbxMonetSchema.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxMonetSchema.Location = new System.Drawing.Point(28, 78);
            this.tbxMonetSchema.Margin = new System.Windows.Forms.Padding(5);
            this.tbxMonetSchema.Name = "tbxMonetSchema";
            this.tbxMonetSchema.Size = new System.Drawing.Size(525, 30);
            this.tbxMonetSchema.TabIndex = 6;
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Location = new System.Drawing.Point(48, 214);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(5);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(113, 44);
            this.btnCopy.TabIndex = 4;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(171, 214);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(113, 44);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 45);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "Schema table:";
            // 
            // BulkLoaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1386, 621);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BulkLoaderForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bulk Loader";
            this.Load += new System.EventHandler(this.BulkLoaderForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBatchSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lsvSQLCols;
        private System.Windows.Forms.Button btnSQLConnect;
        private System.Windows.Forms.ComboBox cbxSQLSchema;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rtbxSQLConnectionString;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblCopyElapsed;
        private System.Windows.Forms.ToolStripProgressBar pbrCopyStatus;
        private System.Windows.Forms.NumericUpDown nudBatchSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxMonetSchema;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;


    }
}

