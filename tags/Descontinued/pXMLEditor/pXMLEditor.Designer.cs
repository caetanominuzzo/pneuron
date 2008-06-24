namespace primeira.Components
{
    partial class pXMLEditor
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
            this.tspTrainingSet = new System.Windows.Forms.ToolStrip();
            this.cbTrainingSets = new System.Windows.Forms.ToolStripComboBox();
            this.btNewTrainingSet = new System.Windows.Forms.ToolStripButton();
            this.btRemoveTrainingSet = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btImport = new System.Windows.Forms.ToolStripButton();
            this.btExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tspSQL = new System.Windows.Forms.ToolStripButton();
            this.dgTrainingSet = new primeira.Components.pDataGridView();
            this.tspTrainingSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTrainingSet)).BeginInit();
            this.SuspendLayout();
            // 
            // tspTrainingSet
            // 
            this.tspTrainingSet.AllowDrop = true;
            this.tspTrainingSet.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbTrainingSets,
            this.btNewTrainingSet,
            this.btRemoveTrainingSet,
            this.toolStripSeparator2,
            this.btImport,
            this.btExport,
            this.toolStripSeparator1,
            this.tspSQL});
            this.tspTrainingSet.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tspTrainingSet.Location = new System.Drawing.Point(0, 0);
            this.tspTrainingSet.Name = "tspTrainingSet";
            this.tspTrainingSet.Size = new System.Drawing.Size(507, 25);
            this.tspTrainingSet.TabIndex = 8;
            this.tspTrainingSet.Text = "toolStrip1";
            // 
            // cbTrainingSets
            // 
            this.cbTrainingSets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTrainingSets.Name = "cbTrainingSets";
            this.cbTrainingSets.Size = new System.Drawing.Size(121, 25);
            this.cbTrainingSets.SelectedIndexChanged += new System.EventHandler(this.cbTrainingSets_SelectedIndexChanged);
            // 
            // btNewTrainingSet
            // 
            this.btNewTrainingSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btNewTrainingSet.Name = "btNewTrainingSet";
            this.btNewTrainingSet.Size = new System.Drawing.Size(32, 22);
            this.btNewTrainingSet.Text = "&New";
            this.btNewTrainingSet.Click += new System.EventHandler(this.btNewTrainingSet_Click);
            // 
            // btRemoveTrainingSet
            // 
            this.btRemoveTrainingSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRemoveTrainingSet.Name = "btRemoveTrainingSet";
            this.btRemoveTrainingSet.Size = new System.Drawing.Size(50, 22);
            this.btRemoveTrainingSet.Text = "Remove";
            this.btRemoveTrainingSet.Click += new System.EventHandler(this.btRemoveTrainingSet_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btImport
            // 
            this.btImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btImport.Name = "btImport";
            this.btImport.Size = new System.Drawing.Size(43, 22);
            this.btImport.Text = "Import";
            this.btImport.Click += new System.EventHandler(this.btImport_Click);
            // 
            // btExport
            // 
            this.btExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(43, 22);
            this.btExport.Text = "Export";
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tspSQL
            // 
            this.tspSQL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspSQL.Name = "tspSQL";
            this.tspSQL.Size = new System.Drawing.Size(90, 22);
            this.tspSQL.Text = "Import from SQL";
            // 
            // dgTrainingSet
            // 
            this.dgTrainingSet.BackgroundColor = System.Drawing.Color.White;
            this.dgTrainingSet.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgTrainingSet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTrainingSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTrainingSet.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgTrainingSet.Location = new System.Drawing.Point(0, 25);
            this.dgTrainingSet.Margin = new System.Windows.Forms.Padding(0);
            this.dgTrainingSet.Name = "dgTrainingSet";
            this.dgTrainingSet.Size = new System.Drawing.Size(507, 393);
            this.dgTrainingSet.TabIndex = 7;
            // 
            // pXMLEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgTrainingSet);
            this.Controls.Add(this.tspTrainingSet);
            this.Name = "pXMLEditor";
            this.Size = new System.Drawing.Size(507, 418);
            this.tspTrainingSet.ResumeLayout(false);
            this.tspTrainingSet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTrainingSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tspTrainingSet;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripComboBox cbTrainingSets;
        public pDataGridView dgTrainingSet;
        public System.Windows.Forms.ToolStripButton btNewTrainingSet;
        public System.Windows.Forms.ToolStripButton btRemoveTrainingSet;
        public System.Windows.Forms.ToolStripButton btImport;
        public System.Windows.Forms.ToolStripButton btExport;
        public System.Windows.Forms.ToolStripButton tspSQL;
    }
}
