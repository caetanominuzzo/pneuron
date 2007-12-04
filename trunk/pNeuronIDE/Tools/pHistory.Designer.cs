namespace primeira.pNeuron
{
    partial class pHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pHistory));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btClear = new System.Windows.Forms.ToolStripButton();
            this.pHistoryManager = new primeira.pHistory.pHistoryManager();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btClear});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(292, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btClear
            // 
            this.btClear.Image = ((System.Drawing.Image)(resources.GetObject("btClear.Image")));
            this.btClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(52, 22);
            this.btClear.Text = "&Clear";
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // pHistoryManager
            // 
            this.pHistoryManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pHistoryManager.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.pHistoryManager.FullRowSelect = true;
            this.pHistoryManager.Indent = 15;
            this.pHistoryManager.ItemHeight = 35;
            this.pHistoryManager.Location = new System.Drawing.Point(0, 25);
            this.pHistoryManager.Name = "pHistoryManager";
            this.pHistoryManager.ShowLines = false;
            this.pHistoryManager.ShowPlusMinus = false;
            this.pHistoryManager.ShowRootLines = false;
            this.pHistoryManager.Size = new System.Drawing.Size(292, 241);
            this.pHistoryManager.TabIndex = 0;
            this.pHistoryManager.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.pHistoryManager_NodeMouseDoubleClick);
            this.pHistoryManager.KeyUp += new System.Windows.Forms.KeyEventHandler(this.pHistoryManager_KeyUp);
            // 
            // pHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.pHistoryManager);
            this.Controls.Add(this.toolStrip1);
            this.Name = "pHistory";
            this.TabText = "pHistory";
            this.Text = "pHistory";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }





        #endregion

        public primeira.pHistory.pHistoryManager pHistoryManager;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btClear;

    }
}