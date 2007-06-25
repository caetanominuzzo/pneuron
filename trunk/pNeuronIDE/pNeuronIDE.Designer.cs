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
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.status = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tspAddNetwork = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.existingNetworkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.existingTrainnerSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tspNewProject = new System.Windows.Forms.ToolStripMenuItem();
            this.tspOpenProject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tspSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tspSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.tspSaveProject = new System.Windows.Forms.ToolStripMenuItem();
            this.tspSaveProjectAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tspUnloadProject = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tspViewToolbox = new System.Windows.Forms.ToolStripMenuItem();
            this.tspViewNetworkExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.tspViewPropertyWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.nNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tspTrain = new System.Windows.Forms.ToolStripMenuItem();
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
            this.dockPanel.Size = new System.Drawing.Size(767, 266);
            this.dockPanel.TabIndex = 0;
            // 
            // status
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2});
            this.status.Location = new System.Drawing.Point(0, 290);
            this.status.Name = "status";
            this.status.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.status.Size = new System.Drawing.Size(767, 22);
            this.status.TabIndex = 3;
            this.status.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(63, 17);
            this.toolStripStatusLabel2.Text = "Status: Idle";
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
            this.newNeuralNetworkToolStripMenuItem.Click += new System.EventHandler(this.tspAddNetwork_Click);
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
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.tspOpenProject_Click);
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
            this.fileToolStripMenuItem1,
            this.viewToolStripMenuItem1,
            this.nNToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(767, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.tspNewProject,
            this.tspOpenProject,
            this.toolStripMenuItem1,
            this.tspSave,
            this.tspSaveAs,
            this.tspSaveProject,
            this.tspSaveProjectAs,
            this.toolStripMenuItem2,
            this.tspUnloadProject,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem1.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspAddNetwork,
            this.toolStripMenuItem3,
            this.existingNetworkToolStripMenuItem,
            this.existingTrainnerSetToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.newToolStripMenuItem.Text = "Add";
            // 
            // tspAddNetwork
            // 
            this.tspAddNetwork.Name = "tspAddNetwork";
            this.tspAddNetwork.Size = new System.Drawing.Size(178, 22);
            this.tspAddNetwork.Text = "New Network";
            this.tspAddNetwork.Click += new System.EventHandler(this.tspAddNetwork_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(175, 6);
            // 
            // existingNetworkToolStripMenuItem
            // 
            this.existingNetworkToolStripMenuItem.Name = "existingNetworkToolStripMenuItem";
            this.existingNetworkToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.existingNetworkToolStripMenuItem.Text = "Existing Network";
            // 
            // existingTrainnerSetToolStripMenuItem
            // 
            this.existingTrainnerSetToolStripMenuItem.Name = "existingTrainnerSetToolStripMenuItem";
            this.existingTrainnerSetToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.existingTrainnerSetToolStripMenuItem.Text = "Existing Trainer Set";
            // 
            // tspNewProject
            // 
            this.tspNewProject.Name = "tspNewProject";
            this.tspNewProject.Size = new System.Drawing.Size(160, 22);
            this.tspNewProject.Text = "New Project";
            this.tspNewProject.Click += new System.EventHandler(this.tspNewProject_Click);
            // 
            // tspOpenProject
            // 
            this.tspOpenProject.Name = "tspOpenProject";
            this.tspOpenProject.Size = new System.Drawing.Size(160, 22);
            this.tspOpenProject.Text = "Open Project";
            this.tspOpenProject.Click += new System.EventHandler(this.tspOpenProject_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(157, 6);
            // 
            // tspSave
            // 
            this.tspSave.Name = "tspSave";
            this.tspSave.Size = new System.Drawing.Size(160, 22);
            this.tspSave.Text = "Save";
            this.tspSave.Click += new System.EventHandler(this.tspSave_Click);
            // 
            // tspSaveAs
            // 
            this.tspSaveAs.Name = "tspSaveAs";
            this.tspSaveAs.Size = new System.Drawing.Size(160, 22);
            this.tspSaveAs.Text = "Save as";
            this.tspSaveAs.Click += new System.EventHandler(this.tspSaveAs_Click);
            // 
            // tspSaveProject
            // 
            this.tspSaveProject.Name = "tspSaveProject";
            this.tspSaveProject.Size = new System.Drawing.Size(160, 22);
            this.tspSaveProject.Text = "Save Project";
            this.tspSaveProject.Click += new System.EventHandler(this.tspSaveProject_Click);
            // 
            // tspSaveProjectAs
            // 
            this.tspSaveProjectAs.Name = "tspSaveProjectAs";
            this.tspSaveProjectAs.Size = new System.Drawing.Size(160, 22);
            this.tspSaveProjectAs.Text = "Save Project as";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(157, 6);
            // 
            // tspUnloadProject
            // 
            this.tspUnloadProject.Name = "tspUnloadProject";
            this.tspUnloadProject.Size = new System.Drawing.Size(160, 22);
            this.tspUnloadProject.Text = "Unload Project";
            this.tspUnloadProject.Click += new System.EventHandler(this.tspUnloadProject_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // viewToolStripMenuItem1
            // 
            this.viewToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspViewToolbox,
            this.tspViewNetworkExplorer,
            this.tspViewPropertyWindow});
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
            // pNeuronIDE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 312);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.status);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "pNeuronIDE";
            this.Text = "pNeuronIDE";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tspOpenProject;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tspSaveProject;
        private System.Windows.Forms.ToolStripMenuItem tspSaveProjectAs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tspViewToolbox;
        private System.Windows.Forms.ToolStripMenuItem tspViewNetworkExplorer;
        private System.Windows.Forms.ToolStripMenuItem tspViewPropertyWindow;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem nNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tspTrain;
        private System.Windows.Forms.ToolStripMenuItem tspNewProject;
        private System.Windows.Forms.ToolStripMenuItem tspAddNetwork;
        private System.Windows.Forms.ToolStripMenuItem tspUnloadProject;
        private System.Windows.Forms.ToolStripMenuItem tspSave;
        private System.Windows.Forms.ToolStripMenuItem tspSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem existingNetworkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem existingTrainnerSetToolStripMenuItem;
    }
}