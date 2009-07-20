namespace primeira.pNeuron.Editor
{
    partial class FileBrowserEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgRecentFiles = new System.Windows.Forms.DataGridView();
            this.ColIcon = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColFilename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColLastWriteTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColHardFilename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColFileVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblRecent = new System.Windows.Forms.Label();
            this.lblQuickLauch = new System.Windows.Forms.Label();
            this.dgQuickLauch = new System.Windows.Forms.DataGridView();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnSeparator = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgRecentFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgQuickLauch)).BeginInit();
            this.SuspendLayout();
            // 
            // dgRecentFiles
            // 
            this.dgRecentFiles.AllowUserToAddRows = false;
            this.dgRecentFiles.AllowUserToDeleteRows = false;
            this.dgRecentFiles.AllowUserToResizeColumns = false;
            this.dgRecentFiles.AllowUserToResizeRows = false;
            this.dgRecentFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgRecentFiles.BackgroundColor = System.Drawing.Color.White;
            this.dgRecentFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgRecentFiles.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgRecentFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgRecentFiles.ColumnHeadersVisible = false;
            this.dgRecentFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColIcon,
            this.ColFilename,
            this.ColLastWriteTime,
            this.ColOrder,
            this.ColHardFilename,
            this.ColFileVersion});
            this.dgRecentFiles.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgRecentFiles.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgRecentFiles.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.dgRecentFiles.Location = new System.Drawing.Point(340, 50);
            this.dgRecentFiles.MultiSelect = false;
            this.dgRecentFiles.Name = "dgRecentFiles";
            this.dgRecentFiles.ReadOnly = true;
            this.dgRecentFiles.RowHeadersVisible = false;
            this.dgRecentFiles.RowTemplate.Height = 40;
            this.dgRecentFiles.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgRecentFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgRecentFiles.Size = new System.Drawing.Size(440, 389);
            this.dgRecentFiles.TabIndex = 2;
            this.dgRecentFiles.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellMouseLeave);
            this.dgRecentFiles.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellMouseEnter);
            this.dgRecentFiles.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // ColIcon
            // 
            this.ColIcon.FillWeight = 40F;
            this.ColIcon.HeaderText = "";
            this.ColIcon.Name = "ColIcon";
            this.ColIcon.ReadOnly = true;
            this.ColIcon.Width = 40;
            // 
            // ColFilename
            // 
            this.ColFilename.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColFilename.HeaderText = "Filename";
            this.ColFilename.Name = "ColFilename";
            this.ColFilename.ReadOnly = true;
            // 
            // ColLastWriteTime
            // 
            this.ColLastWriteTime.HeaderText = "LastWriteTime";
            this.ColLastWriteTime.Name = "ColLastWriteTime";
            this.ColLastWriteTime.ReadOnly = true;
            this.ColLastWriteTime.Width = 140;
            // 
            // ColOrder
            // 
            this.ColOrder.HeaderText = "Order";
            this.ColOrder.Name = "ColOrder";
            this.ColOrder.ReadOnly = true;
            this.ColOrder.Visible = false;
            // 
            // ColHardFilename
            // 
            this.ColHardFilename.HeaderText = "HardFilename";
            this.ColHardFilename.Name = "ColHardFilename";
            this.ColHardFilename.ReadOnly = true;
            this.ColHardFilename.Visible = false;
            // 
            // ColFileVersion
            // 
            this.ColFileVersion.HeaderText = "";
            this.ColFileVersion.Name = "ColFileVersion";
            this.ColFileVersion.ReadOnly = true;
            this.ColFileVersion.Visible = false;
            // 
            // lblRecent
            // 
            this.lblRecent.AutoSize = true;
            this.lblRecent.BackColor = System.Drawing.Color.Transparent;
            this.lblRecent.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecent.ForeColor = System.Drawing.Color.LightGray;
            this.lblRecent.Location = new System.Drawing.Point(340, 21);
            this.lblRecent.Name = "lblRecent";
            this.lblRecent.Size = new System.Drawing.Size(113, 33);
            this.lblRecent.TabIndex = 3;
            this.lblRecent.Text = "Recent";
            // 
            // lblQuickLauch
            // 
            this.lblQuickLauch.AutoSize = true;
            this.lblQuickLauch.BackColor = System.Drawing.Color.Transparent;
            this.lblQuickLauch.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuickLauch.ForeColor = System.Drawing.Color.LightGray;
            this.lblQuickLauch.Location = new System.Drawing.Point(30, 20);
            this.lblQuickLauch.Name = "lblQuickLauch";
            this.lblQuickLauch.Size = new System.Drawing.Size(180, 33);
            this.lblQuickLauch.TabIndex = 4;
            this.lblQuickLauch.Text = "Quick lauch";
            // 
            // dgQuickLauch
            // 
            this.dgQuickLauch.AllowUserToAddRows = false;
            this.dgQuickLauch.AllowUserToDeleteRows = false;
            this.dgQuickLauch.AllowUserToResizeColumns = false;
            this.dgQuickLauch.AllowUserToResizeRows = false;
            this.dgQuickLauch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dgQuickLauch.BackgroundColor = System.Drawing.Color.White;
            this.dgQuickLauch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgQuickLauch.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgQuickLauch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgQuickLauch.ColumnHeadersVisible = false;
            this.dgQuickLauch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewImageColumn1,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.dgQuickLauch.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgQuickLauch.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgQuickLauch.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.dgQuickLauch.Location = new System.Drawing.Point(30, 49);
            this.dgQuickLauch.MultiSelect = false;
            this.dgQuickLauch.Name = "dgQuickLauch";
            this.dgQuickLauch.ReadOnly = true;
            this.dgQuickLauch.RowHeadersVisible = false;
            this.dgQuickLauch.RowTemplate.Height = 40;
            this.dgQuickLauch.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgQuickLauch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgQuickLauch.Size = new System.Drawing.Size(270, 389);
            this.dgQuickLauch.TabIndex = 5;
            this.dgQuickLauch.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellMouseLeave);
            this.dgQuickLauch.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellMouseEnter);
            this.dgQuickLauch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.FillWeight = 40F;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Width = 40;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "Filename";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "LastWriteTime";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Order";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Visible = false;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "HardFilename";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // pnSeparator
            // 
            this.pnSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pnSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.pnSeparator.Location = new System.Drawing.Point(320, 60);
            this.pnSeparator.Name = "pnSeparator";
            this.pnSeparator.Size = new System.Drawing.Size(1, 369);
            this.pnSeparator.TabIndex = 6;
            // 
            // FileBrowserEditor
            // 
            this.Controls.Add(this.pnSeparator);
            this.Controls.Add(this.dgQuickLauch);
            this.Controls.Add(this.dgRecentFiles);
            this.Controls.Add(this.lblQuickLauch);
            this.Controls.Add(this.lblRecent);
            this.Name = "FileBrowserEditor";
            this.Size = new System.Drawing.Size(790, 446);
            ((System.ComponentModel.ISupportInitialize)(this.dgRecentFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgQuickLauch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgRecentFiles;
        private System.Windows.Forms.Label lblRecent;
        private System.Windows.Forms.Label lblQuickLauch;
        private System.Windows.Forms.DataGridView dgQuickLauch;
        private System.Windows.Forms.Panel pnSeparator;

        private System.Windows.Forms.DataGridViewImageColumn ColIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFilename;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColLastWriteTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColHardFilename;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFileVersion;

        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

    }
}
