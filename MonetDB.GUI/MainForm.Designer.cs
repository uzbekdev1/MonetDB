namespace MonetDB.GUI
{
    internal partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dgwDbList = new System.Windows.Forms.DataGridView();
            this.btnKillDb = new System.Windows.Forms.Button();
            this.btnCreateDb = new System.Windows.Forms.Button();
            this.btnLoadDb = new System.Windows.Forms.Button();
            this.btnExplore = new System.Windows.Forms.Button();
            this.btnDropDb = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgwDbList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgwDbList
            // 
            this.dgwDbList.AllowUserToAddRows = false;
            this.dgwDbList.AllowUserToDeleteRows = false;
            this.dgwDbList.AllowUserToResizeRows = false;
            this.dgwDbList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgwDbList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgwDbList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgwDbList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgwDbList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgwDbList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwDbList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column4,
            this.Column2});
            this.dgwDbList.Location = new System.Drawing.Point(12, 74);
            this.dgwDbList.MultiSelect = false;
            this.dgwDbList.Name = "dgwDbList";
            this.dgwDbList.ReadOnly = true;
            this.dgwDbList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwDbList.ShowCellErrors = false;
            this.dgwDbList.ShowEditingIcon = false;
            this.dgwDbList.ShowRowErrors = false;
            this.dgwDbList.Size = new System.Drawing.Size(1040, 337);
            this.dgwDbList.TabIndex = 1;
            this.dgwDbList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgwDbList_CellMouseDoubleClick);
            this.dgwDbList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgwDbList_RowPostPaint);
            // 
            // btnKillDb
            // 
            this.btnKillDb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnKillDb.Image = global::MonetDB.GUI.Properties.Resources.glyphicons_176_stop;
            this.btnKillDb.Location = new System.Drawing.Point(634, 14);
            this.btnKillDb.Margin = new System.Windows.Forms.Padding(5);
            this.btnKillDb.Name = "btnKillDb";
            this.btnKillDb.Size = new System.Drawing.Size(115, 45);
            this.btnKillDb.TabIndex = 6;
            this.btnKillDb.Text = "Kill";
            this.btnKillDb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnKillDb.UseVisualStyleBackColor = true;
            this.btnKillDb.Click += new System.EventHandler(this.btnKillDb_Click);
            // 
            // btnCreateDb
            // 
            this.btnCreateDb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCreateDb.Image = global::MonetDB.GUI.Properties.Resources.glyphicons_142_database_plus;
            this.btnCreateDb.Location = new System.Drawing.Point(260, 14);
            this.btnCreateDb.Margin = new System.Windows.Forms.Padding(5);
            this.btnCreateDb.Name = "btnCreateDb";
            this.btnCreateDb.Size = new System.Drawing.Size(115, 45);
            this.btnCreateDb.TabIndex = 6;
            this.btnCreateDb.Text = "Create";
            this.btnCreateDb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCreateDb.UseVisualStyleBackColor = true;
            this.btnCreateDb.Click += new System.EventHandler(this.btnCreateDb_Click);
            // 
            // btnLoadDb
            // 
            this.btnLoadDb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnLoadDb.Image = global::MonetDB.GUI.Properties.Resources.glyphicons_174_play;
            this.btnLoadDb.Location = new System.Drawing.Point(509, 14);
            this.btnLoadDb.Margin = new System.Windows.Forms.Padding(5);
            this.btnLoadDb.Name = "btnLoadDb";
            this.btnLoadDb.Size = new System.Drawing.Size(115, 45);
            this.btnLoadDb.TabIndex = 6;
            this.btnLoadDb.Text = "Load";
            this.btnLoadDb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLoadDb.UseVisualStyleBackColor = true;
            this.btnLoadDb.Click += new System.EventHandler(this.btnLoadDb_Click);
            // 
            // btnExplore
            // 
            this.btnExplore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExplore.Image = global::MonetDB.GUI.Properties.Resources.glyphicons_528_database;
            this.btnExplore.Location = new System.Drawing.Point(135, 14);
            this.btnExplore.Margin = new System.Windows.Forms.Padding(5);
            this.btnExplore.Name = "btnExplore";
            this.btnExplore.Size = new System.Drawing.Size(115, 45);
            this.btnExplore.TabIndex = 6;
            this.btnExplore.Text = "Explore";
            this.btnExplore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExplore.UseVisualStyleBackColor = true;
            this.btnExplore.Click += new System.EventHandler(this.btnExplore_Click);
            // 
            // btnDropDb
            // 
            this.btnDropDb.AccessibleDescription = "";
            this.btnDropDb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDropDb.Image = global::MonetDB.GUI.Properties.Resources.glyphicons_143_database_minus;
            this.btnDropDb.Location = new System.Drawing.Point(384, 14);
            this.btnDropDb.Margin = new System.Windows.Forms.Padding(5);
            this.btnDropDb.Name = "btnDropDb";
            this.btnDropDb.Size = new System.Drawing.Size(115, 45);
            this.btnDropDb.TabIndex = 6;
            this.btnDropDb.Text = "Drop";
            this.btnDropDb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDropDb.UseVisualStyleBackColor = true;
            this.btnDropDb.Click += new System.EventHandler(this.btnDropDb_Click);
            // 
            // btnReload
            // 
            this.btnReload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnReload.Image = global::MonetDB.GUI.Properties.Resources.glyphicons_82_refresh;
            this.btnReload.Location = new System.Drawing.Point(12, 14);
            this.btnReload.Margin = new System.Windows.Forms.Padding(5);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(115, 45);
            this.btnReload.TabIndex = 6;
            this.btnReload.Text = "Reload";
            this.btnReload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // Column1
            // 
            this.Column1.FillWeight = 200F;
            this.Column1.HeaderText = "Database";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.FillWeight = 50F;
            this.Column4.HeaderText = "Size";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 50F;
            this.Column2.HeaderText = "Status";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1064, 423);
            this.Controls.Add(this.btnKillDb);
            this.Controls.Add(this.btnCreateDb);
            this.Controls.Add(this.btnLoadDb);
            this.Controls.Add(this.btnExplore);
            this.Controls.Add(this.btnDropDb);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.dgwDbList);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.RightToLeftLayout = true;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MonetDB";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwDbList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwDbList;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnExplore;
        private System.Windows.Forms.Button btnCreateDb;
        private System.Windows.Forms.Button btnDropDb;
        private System.Windows.Forms.Button btnLoadDb;
        private System.Windows.Forms.Button btnKillDb;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}