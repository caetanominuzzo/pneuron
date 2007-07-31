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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dfghToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dfghToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dfhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dfghToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.dfghToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.dfghToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.dfghToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDomain)).BeginInit();
            this.menuStrip1.SuspendLayout();
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
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
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
            this.dgDomain.Location = new System.Drawing.Point(0, 49);
            this.dgDomain.Margin = new System.Windows.Forms.Padding(0);
            this.dgDomain.Name = "dgDomain";
            this.dgDomain.Size = new System.Drawing.Size(739, 497);
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sGToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(739, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sGToolStripMenuItem
            // 
            this.sGToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dfghToolStripMenuItem,
            this.dfghToolStripMenuItem1,
            this.dfhToolStripMenuItem});
            this.sGToolStripMenuItem.Name = "sGToolStripMenuItem";
            this.sGToolStripMenuItem.Size = new System.Drawing.Size(33, 20);
            this.sGToolStripMenuItem.Text = "s g";
            // 
            // dfghToolStripMenuItem
            // 
            this.dfghToolStripMenuItem.Name = "dfghToolStripMenuItem";
            this.dfghToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dfghToolStripMenuItem.Text = "dfgh ";
            // 
            // dfghToolStripMenuItem1
            // 
            this.dfghToolStripMenuItem1.Name = "dfghToolStripMenuItem1";
            this.dfghToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.dfghToolStripMenuItem1.Text = "dfgh";
            // 
            // dfhToolStripMenuItem
            // 
            this.dfhToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dfghToolStripMenuItem2,
            this.dfghToolStripMenuItem4});
            this.dfhToolStripMenuItem.Name = "dfhToolStripMenuItem";
            this.dfhToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dfhToolStripMenuItem.Text = "dfh";
            // 
            // dfghToolStripMenuItem2
            // 
            this.dfghToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dfghToolStripMenuItem3});
            this.dfghToolStripMenuItem2.Name = "dfghToolStripMenuItem2";
            this.dfghToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.dfghToolStripMenuItem2.Text = "dfgh";
            // 
            // dfghToolStripMenuItem3
            // 
            this.dfghToolStripMenuItem3.Name = "dfghToolStripMenuItem3";
            this.dfghToolStripMenuItem3.Size = new System.Drawing.Size(152, 22);
            this.dfghToolStripMenuItem3.Text = "dfgh";
            // 
            // dfghToolStripMenuItem4
            // 
            this.dfghToolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dfghToolStripMenuItem5});
            this.dfghToolStripMenuItem4.Name = "dfghToolStripMenuItem4";
            this.dfghToolStripMenuItem4.Size = new System.Drawing.Size(152, 22);
            this.dfghToolStripMenuItem4.Text = "dfgh";
            // 
            // dfghToolStripMenuItem5
            // 
            this.dfghToolStripMenuItem5.Name = "dfghToolStripMenuItem5";
            this.dfghToolStripMenuItem5.Size = new System.Drawing.Size(152, 22);
            this.dfghToolStripMenuItem5.Text = "dfgh";
            // 
            // pXMLEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dgDomain);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "pXMLEditor";
            this.Size = new System.Drawing.Size(739, 546);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDomain)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dfghToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dfghToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem dfhToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dfghToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem dfghToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem dfghToolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem dfghToolStripMenuItem5;
    }
}
