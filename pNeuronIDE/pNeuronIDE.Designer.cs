namespace primeira.pNeuron
{
    partial class pNeuronIDE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pNeuronIDE));
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.status = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusGlobalError = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusCycles = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newNeuralNetworkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.networkExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertyWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newNetworkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openNetworkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tspViewToolbox = new System.Windows.Forms.ToolStripMenuItem();
            this.tspViewNetworkExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.tspViewPropertyWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.domainEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tspTrain = new System.Windows.Forms.ToolStripMenuItem();
            this.statusMediaError = new System.Windows.Forms.ToolStripStatusLabel();
            this.status.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockPanel
            // 
            this.dockPanel.ActiveAutoHideContent = null;
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.DockLeftPortion = 0.1;
            this.dockPanel.DockRightPortion = 0.2;
            this.dockPanel.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.dockPanel.Location = new System.Drawing.Point(0, 24);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(689, 260);
            this.dockPanel.TabIndex = 0;
            // 
            // status
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.statusGlobalError,
            this.statusMediaError,
            this.statusCycles});
            this.status.Location = new System.Drawing.Point(0, 284);
            this.status.Name = "status";
            this.status.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.status.Size = new System.Drawing.Size(689, 22);
            this.status.TabIndex = 3;
            this.status.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.AutoSize = false;
            this.toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.toolStripStatusLabel2.Margin = new System.Windows.Forms.Padding(0, 3, 3, 2);
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(120, 17);
            this.toolStripStatusLabel2.Text = "Status: Idle";
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusGlobalError
            // 
            this.statusGlobalError.AutoSize = false;
            this.statusGlobalError.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusGlobalError.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.statusGlobalError.Margin = new System.Windows.Forms.Padding(0, 3, 3, 2);
            this.statusGlobalError.Name = "statusGlobalError";
            this.statusGlobalError.Size = new System.Drawing.Size(170, 17);
            this.statusGlobalError.Text = "Global Error:";
            this.statusGlobalError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusCycles
            // 
            this.statusCycles.AutoSize = false;
            this.statusCycles.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusCycles.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.statusCycles.Margin = new System.Windows.Forms.Padding(0, 3, 3, 2);
            this.statusCycles.Name = "statusCycles";
            this.statusCycles.Size = new System.Drawing.Size(100, 17);
            this.statusCycles.Text = "Cycles/Sec.:";
            this.statusCycles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(66, 17);
            this.toolStripStatusLabel1.Text = "Status : Idle";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newNeuralNetworkToolStripMenuItem
            // 
            this.newNeuralNetworkToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.newNeuralNetworkToolStripMenuItem.Name = "newNeuralNetworkToolStripMenuItem";
            this.newNeuralNetworkToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.newNeuralNetworkToolStripMenuItem.Text = "New Neural Network";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBoxToolStripMenuItem,
            this.networkExplorerToolStripMenuItem,
            this.propertyWindowToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // toolBoxToolStripMenuItem
            // 
            this.toolBoxToolStripMenuItem.Name = "toolBoxToolStripMenuItem";
            this.toolBoxToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.toolBoxToolStripMenuItem.Text = "ToolBox";
            this.toolBoxToolStripMenuItem.Click += new System.EventHandler(this.toolBoxToolStripMenuItem_Click);
            // 
            // networkExplorerToolStripMenuItem
            // 
            this.networkExplorerToolStripMenuItem.Name = "networkExplorerToolStripMenuItem";
            this.networkExplorerToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.networkExplorerToolStripMenuItem.Text = "Network Explorer";
            this.networkExplorerToolStripMenuItem.Click += new System.EventHandler(this.networkExplorerToolStripMenuItem_Click);
            // 
            // propertyWindowToolStripMenuItem
            // 
            this.propertyWindowToolStripMenuItem.Name = "propertyWindowToolStripMenuItem";
            this.propertyWindowToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.propertyWindowToolStripMenuItem.Text = "Property Window";
            this.propertyWindowToolStripMenuItem.Click += new System.EventHandler(this.propertyWindowToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.viewToolStripMenuItem1,
            this.nNToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(689, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newNetworkToolStripMenuItem,
            this.openNetworkToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem1,
            this.saveAsToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(35, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // newNetworkToolStripMenuItem
            // 
            this.newNetworkToolStripMenuItem.Name = "newNetworkToolStripMenuItem";
            this.newNetworkToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.newNetworkToolStripMenuItem.Text = "New Network";
            this.newNetworkToolStripMenuItem.Click += new System.EventHandler(this.newNetworkToolStripMenuItem_Click);
            // 
            // openNetworkToolStripMenuItem
            // 
            this.openNetworkToolStripMenuItem.Name = "openNetworkToolStripMenuItem";
            this.openNetworkToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.openNetworkToolStripMenuItem.Text = "Open Network";
            this.openNetworkToolStripMenuItem.Click += new System.EventHandler(this.openNetworkToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(151, 6);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(154, 22);
            this.saveToolStripMenuItem1.Text = "Save";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.saveAsToolStripMenuItem.Text = "Save as";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem1
            // 
            this.viewToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspViewToolbox,
            this.tspViewNetworkExplorer,
            this.tspViewPropertyWindow,
            this.domainEditToolStripMenuItem});
            this.viewToolStripMenuItem1.Name = "viewToolStripMenuItem1";
            this.viewToolStripMenuItem1.Size = new System.Drawing.Size(41, 20);
            this.viewToolStripMenuItem1.Text = "View";
            // 
            // tspViewToolbox
            // 
            this.tspViewToolbox.Name = "tspViewToolbox";
            this.tspViewToolbox.Size = new System.Drawing.Size(202, 22);
            this.tspViewToolbox.Text = "Toolbox";
            this.tspViewToolbox.Click += new System.EventHandler(this.toolBoxToolStripMenuItem_Click);
            // 
            // tspViewNetworkExplorer
            // 
            this.tspViewNetworkExplorer.Name = "tspViewNetworkExplorer";
            this.tspViewNetworkExplorer.Size = new System.Drawing.Size(202, 22);
            this.tspViewNetworkExplorer.Text = "Neural Network Explorer";
            this.tspViewNetworkExplorer.Click += new System.EventHandler(this.networkExplorerToolStripMenuItem_Click);
            // 
            // tspViewPropertyWindow
            // 
            this.tspViewPropertyWindow.Name = "tspViewPropertyWindow";
            this.tspViewPropertyWindow.Size = new System.Drawing.Size(202, 22);
            this.tspViewPropertyWindow.Text = "Property Window";
            this.tspViewPropertyWindow.Click += new System.EventHandler(this.propertyWindowToolStripMenuItem_Click);
            // 
            // domainEditToolStripMenuItem
            // 
            this.domainEditToolStripMenuItem.Name = "domainEditToolStripMenuItem";
            this.domainEditToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.domainEditToolStripMenuItem.Text = "Domain Edit";
            this.domainEditToolStripMenuItem.Click += new System.EventHandler(this.domainEditToolStripMenuItem_Click);
            // 
            // nNToolStripMenuItem
            // 
            this.nNToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspTrain});
            this.nNToolStripMenuItem.Name = "nNToolStripMenuItem";
            this.nNToolStripMenuItem.Size = new System.Drawing.Size(33, 20);
            this.nNToolStripMenuItem.Text = "NN";
            // 
            // tspTrain
            // 
            this.tspTrain.Name = "tspTrain";
            this.tspTrain.Size = new System.Drawing.Size(109, 22);
            this.tspTrain.Text = "Train";
            this.tspTrain.Click += new System.EventHandler(this.trainToolStripMenuItem_Click);
            // 
            // statusMediaError
            // 
            this.statusMediaError.AutoSize = false;
            this.statusMediaError.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusMediaError.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.statusMediaError.Margin = new System.Windows.Forms.Padding(0, 3, 3, 2);
            this.statusMediaError.Name = "statusMediaError";
            this.statusMediaError.Size = new System.Drawing.Size(170, 17);
            this.statusMediaError.Text = "Media Error:";
            this.statusMediaError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pNeuronIDE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 306);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.status);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "pNeuronIDE";
            this.Text = "pNeuronIDE";
            this.Load += new System.EventHandler(this.pNeuronIDE_Load);
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public  WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        public System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newNeuralNetworkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolBoxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem networkExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertyWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tspViewToolbox;
        private System.Windows.Forms.ToolStripMenuItem tspViewNetworkExplorer;
        private System.Windows.Forms.ToolStripMenuItem tspViewPropertyWindow;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem nNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tspTrain;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newNetworkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openNetworkToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        public System.Windows.Forms.ToolStripStatusLabel statusCycles;
        public System.Windows.Forms.ToolStripStatusLabel statusGlobalError;
        private System.Windows.Forms.ToolStripMenuItem domainEditToolStripMenuItem;
        public System.Windows.Forms.ToolStripStatusLabel statusMediaError;
    }
}