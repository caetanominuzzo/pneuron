namespace pXMLEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pXMLEditor));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cbTrainingSets = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btNewTrainingSet = new System.Windows.Forms.ToolStripButton();
            this.btRemoveTrainingSet = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btImport = new System.Windows.Forms.ToolStripButton();
            this.btExport = new System.Windows.Forms.ToolStripButton();
            this.dgDomain = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDomain)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowDrop = true;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbTrainingSets,
            this.toolStripSeparator1,
            this.btNewTrainingSet,
            this.btRemoveTrainingSet,
            this.toolStripSeparator2,
            this.btImport,
            this.btExport});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(739, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cbTrainingSets
            // 
            this.cbTrainingSets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTrainingSets.Name = "cbTrainingSets";
            this.cbTrainingSets.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btNewTrainingSet
            // 
            this.btNewTrainingSet.Image = ((System.Drawing.Image)(resources.GetObject("btNewTrainingSet.Image")));
            this.btNewTrainingSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btNewTrainingSet.Name = "btNewTrainingSet";
            this.btNewTrainingSet.Size = new System.Drawing.Size(48, 22);
            this.btNewTrainingSet.Text = "&New";
            // 
            // btRemoveTrainingSet
            // 
            this.btRemoveTrainingSet.Image = ((System.Drawing.Image)(resources.GetObject("btRemoveTrainingSet.Image")));
            this.btRemoveTrainingSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRemoveTrainingSet.Name = "btRemoveTrainingSet";
            this.btRemoveTrainingSet.Size = new System.Drawing.Size(66, 22);
            this.btRemoveTrainingSet.Text = "Remove";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btImport
            // 
            this.btImport.Image = ((System.Drawing.Image)(resources.GetObject("btImport.Image")));
            this.btImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btImport.Name = "btImport";
            this.btImport.Size = new System.Drawing.Size(59, 22);
            this.btImport.Text = "Import";
            // 
            // btExport
            // 
            this.btExport.Image = ((System.Drawing.Image)(resources.GetObject("btExport.Image")));
            this.btExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(59, 22);
            this.btExport.Text = "Export";
            // 
            // dgDomain
            // 
            this.dgDomain.BackgroundColor = System.Drawing.Color.White;
            this.dgDomain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDomain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDomain.Location = new System.Drawing.Point(0, 25);
            this.dgDomain.Margin = new System.Windows.Forms.Padding(0);
            this.dgDomain.Name = "dgDomain";
            this.dgDomain.Size = new System.Drawing.Size(739, 499);
            this.dgDomain.TabIndex = 9;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 524);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(739, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pXMLEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgDomain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "pXMLEditor";
            this.Size = new System.Drawing.Size(739, 546);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDomain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox cbTrainingSets;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btNewTrainingSet;
        private System.Windows.Forms.ToolStripButton btRemoveTrainingSet;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btImport;
        private System.Windows.Forms.ToolStripButton btExport;
        private System.Windows.Forms.DataGridView dgDomain;
        private System.Windows.Forms.StatusStrip statusStrip1;
    }
}
