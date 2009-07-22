namespace pNeuronEditor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileBrowserEditor));
            this.lblQuickLauch = new System.Windows.Forms.Label();
            this.lblRecent = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgDirFiles = new System.Windows.Forms.DataGridView();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgQuickLauch = new System.Windows.Forms.DataGridView();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgRecentFiles = new System.Windows.Forms.DataGridView();
            this.ColIcon = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColFilename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColLastWriteTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColHardFilename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColFileVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabButton1 = new pNeuronEditor.Components.TabButton();
            this.folderBrowser1 = new pNeuronEditor.Editors.FileBrowser.FolderBrowser();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDirFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgQuickLauch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgRecentFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // lblQuickLauch
            // 
            this.lblQuickLauch.AutoSize = true;
            this.lblQuickLauch.BackColor = System.Drawing.Color.Transparent;
            this.lblQuickLauch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuickLauch.ForeColor = System.Drawing.Color.DarkGray;
            this.lblQuickLauch.Location = new System.Drawing.Point(7, 20);
            this.lblQuickLauch.Name = "lblQuickLauch";
            this.lblQuickLauch.Size = new System.Drawing.Size(91, 20);
            this.lblQuickLauch.TabIndex = 4;
            this.lblQuickLauch.Text = "Quick lauch";
            // 
            // lblRecent
            // 
            this.lblRecent.AutoSize = true;
            this.lblRecent.BackColor = System.Drawing.Color.Transparent;
            this.lblRecent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecent.ForeColor = System.Drawing.Color.DarkGray;
            this.lblRecent.Location = new System.Drawing.Point(290, 20);
            this.lblRecent.Name = "lblRecent";
            this.lblRecent.Size = new System.Drawing.Size(61, 20);
            this.lblRecent.TabIndex = 3;
            this.lblRecent.Text = "Recent";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Controls.Add(this.folderBrowser1);
            this.panel4.Controls.Add(this.dgDirFiles);
            this.panel4.Location = new System.Drawing.Point(530, 50);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(10, 373);
            this.panel4.TabIndex = 13;
            this.panel4.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(20, 340);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Browse";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.panel3.Location = new System.Drawing.Point(10, 60);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(0, 1);
            this.panel3.TabIndex = 12;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.tabButton1);
            this.panel2.Location = new System.Drawing.Point(10, 273);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.panel2.Size = new System.Drawing.Size(0, 40);
            this.panel2.TabIndex = 11;
            // 
            // dgDirFiles
            // 
            this.dgDirFiles.AllowUserToAddRows = false;
            this.dgDirFiles.AllowUserToDeleteRows = false;
            this.dgDirFiles.AllowUserToResizeColumns = false;
            this.dgDirFiles.AllowUserToResizeRows = false;
            this.dgDirFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgDirFiles.BackgroundColor = System.Drawing.Color.White;
            this.dgDirFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgDirFiles.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgDirFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDirFiles.ColumnHeadersVisible = false;
            this.dgDirFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewImageColumn2,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10});
            this.dgDirFiles.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDirFiles.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgDirFiles.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.dgDirFiles.Location = new System.Drawing.Point(10, 70);
            this.dgDirFiles.MultiSelect = false;
            this.dgDirFiles.Name = "dgDirFiles";
            this.dgDirFiles.ReadOnly = true;
            this.dgDirFiles.RowHeadersVisible = false;
            this.dgDirFiles.RowHeadersWidth = 30;
            this.dgDirFiles.RowTemplate.Height = 35;
            this.dgDirFiles.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgDirFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDirFiles.Size = new System.Drawing.Size(0, 201);
            this.dgDirFiles.TabIndex = 9;
            this.dgDirFiles.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellMouseLeave);
            this.dgDirFiles.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellMouseEnter);
            this.dgDirFiles.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.FillWeight = 40F;
            this.dataGridViewImageColumn2.HeaderText = "";
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.ReadOnly = true;
            this.dataGridViewImageColumn2.Width = 40;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.HeaderText = "Filename";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "LastWriteTime";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Visible = false;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Order";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Visible = false;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "HardFilename";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Visible = false;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.panel1.Location = new System.Drawing.Point(280, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1, 363);
            this.panel1.TabIndex = 8;
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgQuickLauch.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgQuickLauch.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.dgQuickLauch.Location = new System.Drawing.Point(10, 50);
            this.dgQuickLauch.MultiSelect = false;
            this.dgQuickLauch.Name = "dgQuickLauch";
            this.dgQuickLauch.ReadOnly = true;
            this.dgQuickLauch.RowHeadersVisible = false;
            this.dgQuickLauch.RowHeadersWidth = 30;
            this.dgQuickLauch.RowTemplate.Height = 35;
            this.dgQuickLauch.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgQuickLauch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgQuickLauch.Size = new System.Drawing.Size(260, 380);
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgRecentFiles.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgRecentFiles.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.dgRecentFiles.Location = new System.Drawing.Point(290, 50);
            this.dgRecentFiles.MultiSelect = false;
            this.dgRecentFiles.Name = "dgRecentFiles";
            this.dgRecentFiles.ReadOnly = true;
            this.dgRecentFiles.RowHeadersVisible = false;
            this.dgRecentFiles.RowHeadersWidth = 30;
            this.dgRecentFiles.RowTemplate.Height = 35;
            this.dgRecentFiles.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgRecentFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgRecentFiles.Size = new System.Drawing.Size(250, 380);
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
            // tabButton1
            // 
            this.tabButton1.AutoEllipsis = true;
            this.tabButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabButton1.BackgroundImage")));
            this.tabButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tabButton1.FlatAppearance.BorderSize = 0;
            this.tabButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tabButton1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabButton1.ForeColor = System.Drawing.Color.DarkGray;
            this.tabButton1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.tabButton1.Location = new System.Drawing.Point(-80, 0);
            this.tabButton1.MaximumSize = new System.Drawing.Size(200, 40);
            this.tabButton1.Name = "tabButton1";
            this.tabButton1.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
            this.tabButton1.SelectedImage = ((System.Drawing.Image)(resources.GetObject("tabButton1.SelectedImage")));
            this.tabButton1.Size = new System.Drawing.Size(75, 40);
            this.tabButton1.TabIndex = 10;
            this.tabButton1.Text = "Open";
            this.tabButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.tabButton1.UnselectedImage = ((System.Drawing.Image)(resources.GetObject("tabButton1.UnselectedImage")));
            this.tabButton1.UseVisualStyleBackColor = true;
            // 
            // folderBrowser1
            // 
            this.folderBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.folderBrowser1.Location = new System.Drawing.Point(0, 0);
            this.folderBrowser1.Name = "folderBrowser1";
            this.folderBrowser1.Size = new System.Drawing.Size(10, 46);
            this.folderBrowser1.TabIndex = 7;
            this.folderBrowser1.OnDirectoryChange += new pNeuronEditor.Editors.FileBrowser.FolderBrowser.OnDirectoryChangeDelegate(this.folderBrowser1_OnDirectoryChange);
            // 
            // FileBrowserEditor
            // 
            this.Controls.Add(this.lblQuickLauch);
            this.Controls.Add(this.lblRecent);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgQuickLauch);
            this.Controls.Add(this.dgRecentFiles);
            this.Name = "FileBrowserEditor";
            this.Size = new System.Drawing.Size(548, 439);
            this.Load += new System.EventHandler(this.FileBrowserEditor_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDirFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgQuickLauch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgRecentFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgRecentFiles;
        private System.Windows.Forms.Label lblRecent;
        private System.Windows.Forms.Label lblQuickLauch;
        private System.Windows.Forms.DataGridView dgQuickLauch;

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
        private pNeuronEditor.Editors.FileBrowser.FolderBrowser folderBrowser1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgDirFiles;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private pNeuronEditor.Components.TabButton tabButton1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;

    }
}
