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
            this.status = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusGlobalError = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusCycles = new System.Windows.Forms.ToolStripStatusLabel();
            this.learninRate = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newNeuralNetworkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tspViewToolbox = new System.Windows.Forms.ToolStripMenuItem();
            this.tspViewPropertyWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.plotterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miniMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.starterGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tspStartTrain = new System.Windows.Forms.ToolStripButton();
            this.tspKnowledgement = new System.Windows.Forms.ToolStripButton();
            this.tspResetMemory = new System.Windows.Forms.ToolStripButton();
            this.tspPulse = new System.Windows.Forms.ToolStripButton();
            this.pnTrackBar = new System.Windows.Forms.Panel();
            this.trackLR = new System.Windows.Forms.TrackBar();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.trainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startTrainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetKnowledgementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetMemoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.pulseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.status.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pnTrackBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackLR)).BeginInit();
            this.SuspendLayout();
            // 
            // status
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.statusGlobalError,
            this.statusCycles,
            this.learninRate});
            this.status.Location = new System.Drawing.Point(0, 284);
            this.status.Name = "status";
            this.status.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.status.Size = new System.Drawing.Size(753, 22);
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
            this.statusGlobalError.Size = new System.Drawing.Size(200, 17);
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
            this.statusCycles.Size = new System.Drawing.Size(110, 17);
            this.statusCycles.Text = "Cycles/Sec.:";
            this.statusCycles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // learninRate
            // 
            this.learninRate.AutoSize = false;
            this.learninRate.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.learninRate.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.learninRate.Margin = new System.Windows.Forms.Padding(0, 3, 3, 2);
            this.learninRate.Name = "learninRate";
            this.learninRate.Size = new System.Drawing.Size(110, 17);
            this.learninRate.Text = "Learning Rate: 5";
            this.learninRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.learninRate.MouseHover += new System.EventHandler(this.learninRate_MouseHover);
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
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem1,
            this.trainToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(753, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newNetworkToolStripMenuItem,
            this.openNetworkToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(35, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // newNetworkToolStripMenuItem
            // 
            this.newNetworkToolStripMenuItem.Name = "newNetworkToolStripMenuItem";
            this.newNetworkToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newNetworkToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.newNetworkToolStripMenuItem.Text = "New Network...";
            this.newNetworkToolStripMenuItem.Click += new System.EventHandler(this.newNetworkToolStripMenuItem_Click);
            // 
            // openNetworkToolStripMenuItem
            // 
            this.openNetworkToolStripMenuItem.Name = "openNetworkToolStripMenuItem";
            this.openNetworkToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openNetworkToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.openNetworkToolStripMenuItem.Text = "Open Network...";
            this.openNetworkToolStripMenuItem.Click += new System.EventHandler(this.openNetworkToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(191, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.saveAsToolStripMenuItem.Text = "Save as...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(191, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem1
            // 
            this.viewToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspViewToolbox,
            this.tspViewPropertyWindow,
            this.plotterToolStripMenuItem,
            this.miniMapToolStripMenuItem,
            this.historyToolStripMenuItem});
            this.viewToolStripMenuItem1.Name = "viewToolStripMenuItem1";
            this.viewToolStripMenuItem1.Size = new System.Drawing.Size(41, 20);
            this.viewToolStripMenuItem1.Text = "View";
            // 
            // tspViewToolbox
            // 
            this.tspViewToolbox.Name = "tspViewToolbox";
            this.tspViewToolbox.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
                        | System.Windows.Forms.Keys.T)));
            this.tspViewToolbox.Size = new System.Drawing.Size(187, 22);
            this.tspViewToolbox.Text = "Toolbox";
            this.tspViewToolbox.Click += new System.EventHandler(this.toolBoxToolStripMenuItem_Click);
            // 
            // tspViewPropertyWindow
            // 
            this.tspViewPropertyWindow.Name = "tspViewPropertyWindow";
            this.tspViewPropertyWindow.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.tspViewPropertyWindow.Size = new System.Drawing.Size(187, 22);
            this.tspViewPropertyWindow.Text = "Property Window";
            this.tspViewPropertyWindow.Click += new System.EventHandler(this.propertyWindowToolStripMenuItem_Click);
            // 
            // plotterToolStripMenuItem
            // 
            this.plotterToolStripMenuItem.Name = "plotterToolStripMenuItem";
            this.plotterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
                        | System.Windows.Forms.Keys.P)));
            this.plotterToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.plotterToolStripMenuItem.Text = "Plotter";
            this.plotterToolStripMenuItem.Click += new System.EventHandler(this.plotterToolStripMenuItem_Click);
            // 
            // miniMapToolStripMenuItem
            // 
            this.miniMapToolStripMenuItem.Name = "miniMapToolStripMenuItem";
            this.miniMapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
                        | System.Windows.Forms.Keys.Z)));
            this.miniMapToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.miniMapToolStripMenuItem.Text = "Mini Map";
            this.miniMapToolStripMenuItem.Click += new System.EventHandler(this.miniMapToolStripMenuItem_Click);
            // 
            // historyToolStripMenuItem
            // 
            this.historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            this.historyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
                        | System.Windows.Forms.Keys.H)));
            this.historyToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.historyToolStripMenuItem.Text = "History";
            this.historyToolStripMenuItem.Click += new System.EventHandler(this.historyToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.starterGuideToolStripMenuItem,
            this.toolStripMenuItem4,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // starterGuideToolStripMenuItem
            // 
            this.starterGuideToolStripMenuItem.Name = "starterGuideToolStripMenuItem";
            this.starterGuideToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.starterGuideToolStripMenuItem.Text = "Starter Guide";
            this.starterGuideToolStripMenuItem.Click += new System.EventHandler(this.starterGuideToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspStartTrain,
            this.tspKnowledgement,
            this.tspResetMemory,
            this.tspPulse});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(753, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tspStartTrain
            // 
            this.tspStartTrain.Image = ((System.Drawing.Image)(resources.GetObject("tspStartTrain.Image")));
            this.tspStartTrain.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspStartTrain.Name = "tspStartTrain";
            this.tspStartTrain.Size = new System.Drawing.Size(78, 22);
            this.tspStartTrain.Text = "Start Train";
            this.tspStartTrain.Click += new System.EventHandler(this.tspStartTrain_Click);
            // 
            // tspKnowledgement
            // 
            this.tspKnowledgement.Image = ((System.Drawing.Image)(resources.GetObject("tspKnowledgement.Image")));
            this.tspKnowledgement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspKnowledgement.Name = "tspKnowledgement";
            this.tspKnowledgement.Size = new System.Drawing.Size(137, 22);
            this.tspKnowledgement.Text = "Reset Knowledgement ";
            this.tspKnowledgement.Click += new System.EventHandler(this.tspResetKnowledgement_Click);
            // 
            // tspResetMemory
            // 
            this.tspResetMemory.Image = ((System.Drawing.Image)(resources.GetObject("tspResetMemory.Image")));
            this.tspResetMemory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspResetMemory.Name = "tspResetMemory";
            this.tspResetMemory.Size = new System.Drawing.Size(96, 22);
            this.tspResetMemory.Text = "Reset Memory";
            this.tspResetMemory.Click += new System.EventHandler(this.tspResetMemory_Click);
            // 
            // tspPulse
            // 
            this.tspPulse.Image = ((System.Drawing.Image)(resources.GetObject("tspPulse.Image")));
            this.tspPulse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspPulse.Name = "tspPulse";
            this.tspPulse.Size = new System.Drawing.Size(56, 22);
            this.tspPulse.Text = "Pulse!";
            this.tspPulse.Click += new System.EventHandler(this.tspPulse_Click);
            // 
            // pnTrackBar
            // 
            this.pnTrackBar.Controls.Add(this.trackLR);
            this.pnTrackBar.Location = new System.Drawing.Point(511, 158);
            this.pnTrackBar.Name = "pnTrackBar";
            this.pnTrackBar.Size = new System.Drawing.Size(38, 125);
            this.pnTrackBar.TabIndex = 11;
            this.pnTrackBar.Visible = false;
            // 
            // trackLR
            // 
            this.trackLR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackLR.Location = new System.Drawing.Point(0, 0);
            this.trackLR.Maximum = 100;
            this.trackLR.Name = "trackLR";
            this.trackLR.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackLR.Size = new System.Drawing.Size(38, 125);
            this.trackLR.TabIndex = 0;
            this.trackLR.TickFrequency = 10;
            this.trackLR.Value = 5;
            this.trackLR.MouseLeave += new System.EventHandler(this.trackLR_MouseLeave);
            this.trackLR.Scroll += new System.EventHandler(this.trackLR_Scroll);
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
            this.dockPanel.Size = new System.Drawing.Size(753, 260);
            this.dockPanel.TabIndex = 0;
            // 
            // trainToolStripMenuItem
            // 
            this.trainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startTrainToolStripMenuItem,
            this.resetKnowledgementToolStripMenuItem,
            this.resetMemoryToolStripMenuItem,
            this.toolStripMenuItem3,
            this.pulseToolStripMenuItem});
            this.trainToolStripMenuItem.Name = "trainToolStripMenuItem";
            this.trainToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.trainToolStripMenuItem.Text = "Train";
            // 
            // startTrainToolStripMenuItem
            // 
            this.startTrainToolStripMenuItem.Name = "startTrainToolStripMenuItem";
            this.startTrainToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.startTrainToolStripMenuItem.Text = "Start Train";
            // 
            // resetKnowledgementToolStripMenuItem
            // 
            this.resetKnowledgementToolStripMenuItem.Name = "resetKnowledgementToolStripMenuItem";
            this.resetKnowledgementToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.resetKnowledgementToolStripMenuItem.Text = "Reset Knowledgement";
            // 
            // resetMemoryToolStripMenuItem
            // 
            this.resetMemoryToolStripMenuItem.Name = "resetMemoryToolStripMenuItem";
            this.resetMemoryToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.resetMemoryToolStripMenuItem.Text = "Reset Memory";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(189, 6);
            // 
            // pulseToolStripMenuItem
            // 
            this.pulseToolStripMenuItem.Name = "pulseToolStripMenuItem";
            this.pulseToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.pulseToolStripMenuItem.Text = "Pulse";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.Z)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(149, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // pNeuronIDE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 306);
            this.Controls.Add(this.pnTrackBar);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.status);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "pNeuronIDE";
            this.Text = "pNeuronIDE";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.pNeuronIDE_FormClosing);
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnTrackBar.ResumeLayout(false);
            this.pnTrackBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackLR)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newNeuralNetworkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolBoxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem networkExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertyWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tspViewToolbox;
        private System.Windows.Forms.ToolStripMenuItem tspViewPropertyWindow;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newNetworkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openNetworkToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel statusCycles;
        private System.Windows.Forms.ToolStripStatusLabel statusGlobalError;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tspStartTrain;
        private System.Windows.Forms.ToolStripButton tspKnowledgement;
        private System.Windows.Forms.ToolStripStatusLabel learninRate;
        private System.Windows.Forms.Panel pnTrackBar;
        private System.Windows.Forms.TrackBar trackLR;
        private System.Windows.Forms.ToolStripButton tspResetMemory;
        private System.Windows.Forms.ToolStripButton tspPulse;
        private System.Windows.Forms.ToolStripMenuItem plotterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem starterGuideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miniMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem historyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startTrainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetKnowledgementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetMemoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem pulseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}