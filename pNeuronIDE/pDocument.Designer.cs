using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using primeira.pNeuron.Core;
using System.IO;
using System.Threading;
using primeira.pRandom;

namespace primeira.pNeuron
{
    public partial class pDocument
    {
        public pDisplay MainDisplay;

        private TabControl tcDesigner;
        private TabPage tbDesigner;
        private TabPage tbTrainingSet;
        private DataGridView dgTrainingSet;
        private List<pTrainingSet> fpTrainingSet = new List<pTrainingSet>();
        private FlowLayoutPanel flowLayoutPanel1;
        private ToolStrip tspTrainingSet;
        private ToolStripComboBox cbTrainingSets;
        private ToolStripButton btNewTrainingSet;
        private ToolStripButton btRemoveTrainingSet;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btImport;
        private ToolStripButton btExport;

        private ToolStrip tspDesigner;
        private ToolStripButton tspAutoRefresh;
        private System.Windows.Forms.Timer refreshTimer;
        private IContainer components;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pDocument));
            this.tcDesigner = new System.Windows.Forms.TabControl();
            this.tbDesigner = new System.Windows.Forms.TabPage();
            this.MainDisplay = new primeira.pNeuron.pDisplay(this.cache==null?new pTrueRandomGenerator(1) : this.cache );
            this.tspDesigner = new System.Windows.Forms.ToolStrip();
            this.tspAutoRefresh = new System.Windows.Forms.ToolStripButton();
            this.tbTrainingSet = new System.Windows.Forms.TabPage();
            this.dgTrainingSet = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tspTrainingSet = new System.Windows.Forms.ToolStrip();
            this.cbTrainingSets = new System.Windows.Forms.ToolStripComboBox();
            this.btNewTrainingSet = new System.Windows.Forms.ToolStripButton();
            this.btRemoveTrainingSet = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btImport = new System.Windows.Forms.ToolStripButton();
            this.btExport = new System.Windows.Forms.ToolStripButton();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.tcDesigner.SuspendLayout();
            this.tbDesigner.SuspendLayout();
            this.MainDisplay.SuspendLayout();
            this.tspDesigner.SuspendLayout();
            this.tbTrainingSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTrainingSet)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.tspTrainingSet.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcDesigner
            // 
            this.tcDesigner.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tcDesigner.Controls.Add(this.tbDesigner);
            this.tcDesigner.Controls.Add(this.tbTrainingSet);
            this.tcDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcDesigner.Location = new System.Drawing.Point(0, 0);
            this.tcDesigner.Margin = new System.Windows.Forms.Padding(0);
            this.tcDesigner.Name = "tcDesigner";
            this.tcDesigner.SelectedIndex = 0;
            this.tcDesigner.Size = new System.Drawing.Size(744, 422);
            this.tcDesigner.TabIndex = 1;
            this.tcDesigner.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tcDesigner_Selecting);
            // 
            // tbDesigner
            // 
            this.tbDesigner.Controls.Add(this.MainDisplay);
            this.tbDesigner.Location = new System.Drawing.Point(4, 4);
            this.tbDesigner.Name = "tbDesigner";
            this.tbDesigner.Padding = new System.Windows.Forms.Padding(3);
            this.tbDesigner.Size = new System.Drawing.Size(736, 396);
            this.tbDesigner.TabIndex = 0;
            this.tbDesigner.Text = "Network Designer";
            this.tbDesigner.UseVisualStyleBackColor = true;
            // 
            // pDisplay1
            // 
            this.MainDisplay.AutoScroll = true;
            this.MainDisplay.AutoScrollHorizontalMaximum = 100;
            this.MainDisplay.AutoScrollHorizontalMinimum = 0;
            this.MainDisplay.AutoScrollHPos = 0;
            this.MainDisplay.AutoScrollVerticalMaximum = 100;
            this.MainDisplay.AutoScrollVerticalMinimum = 0;
            this.MainDisplay.AutoScrollVPos = 0;
            this.MainDisplay.BackColor = System.Drawing.Color.White;
            this.MainDisplay.Bezier = true;
            this.MainDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainDisplay.Controls.Add(this.tspDesigner);
            this.MainDisplay.CtrlKey = false;
            this.MainDisplay.Cursor = System.Windows.Forms.Cursors.Default;
            this.MainDisplay.DisplayStatus = primeira.pNeuron.pDisplay.pDisplayStatus.Idle;
            this.MainDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainDisplay.EnableAutoScrollHorizontal = true;
            this.MainDisplay.EnableAutoScrollVertical = true;
            this.MainDisplay.Location = new System.Drawing.Point(3, 3);
            this.MainDisplay.Name = "pDisplay1";
            this.MainDisplay.ShiftKey = false;
            this.MainDisplay.Size = new System.Drawing.Size(730, 390);
            this.MainDisplay.TabIndex = 0;
            this.MainDisplay.VisibleAutoScrollHorizontal = true;
            this.MainDisplay.VisibleAutoScrollVertical = true;
            this.MainDisplay.OnDisplayStatusChange += new primeira.pNeuron.pDisplay.DisplayStatusChangeDelegate(this.pDisplay1_OnDisplayStatusChange);
            this.MainDisplay.OnSelectedPanelsChange += new primeira.pNeuron.pDisplay.SelectedPanelsChangeDelegate(this.pDisplay1_OnSelectedPanelsChange);
            this.MainDisplay.OnNetworkChange += new primeira.pNeuron.pDisplay.NetworkChangeDelegate(this.pDisplay1_OnNetworkChange);
            // 
            // toolStrip2
            // 
            this.tspDesigner.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspAutoRefresh});
            this.tspDesigner.Location = new System.Drawing.Point(0, 0);
            this.tspDesigner.Name = "toolStrip2";
            this.tspDesigner.Size = new System.Drawing.Size(712, 25);
            this.tspDesigner.TabIndex = 0;
            this.tspDesigner.Text = "toolStrip2";
            // 
            // tspAttach
            // 
            this.tspAutoRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tspAttach.Image")));
            this.tspAutoRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspAutoRefresh.Name = "tspAttach";
            this.tspAutoRefresh.Size = new System.Drawing.Size(59, 22);
            this.tspAutoRefresh.Text = "Attach";
            this.tspAutoRefresh.Click += new System.EventHandler(this.tspAutoRefresh_Click);
            // 
            // tbTrainingSet
            // 
            this.tbTrainingSet.Controls.Add(this.dgTrainingSet);
            this.tbTrainingSet.Controls.Add(this.flowLayoutPanel1);
            this.tbTrainingSet.Location = new System.Drawing.Point(4, 4);
            this.tbTrainingSet.Margin = new System.Windows.Forms.Padding(0);
            this.tbTrainingSet.Name = "tbTrainingSet";
            this.tbTrainingSet.Padding = new System.Windows.Forms.Padding(3);
            this.tbTrainingSet.Size = new System.Drawing.Size(736, 396);
            this.tbTrainingSet.TabIndex = 1;
            this.tbTrainingSet.Text = "Training Sets";
            this.tbTrainingSet.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dgTrainingSet.BackgroundColor = System.Drawing.Color.White;
            this.dgTrainingSet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTrainingSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTrainingSet.Location = new System.Drawing.Point(3, 28);
            this.dgTrainingSet.Margin = new System.Windows.Forms.Padding(0);
            this.dgTrainingSet.Name = "dataGridView1";
            this.dgTrainingSet.Size = new System.Drawing.Size(730, 365);
            this.dgTrainingSet.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.tspTrainingSet);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(730, 25);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // toolStrip1
            // 
            this.tspTrainingSet.AllowDrop = true;
            this.tspTrainingSet.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbTrainingSets,
            this.btNewTrainingSet,
            this.btRemoveTrainingSet,
            this.toolStripSeparator2,
            this.btImport,
            this.btExport});
            this.tspTrainingSet.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tspTrainingSet.Location = new System.Drawing.Point(0, 0);
            this.tspTrainingSet.Name = "toolStrip1";
            this.tspTrainingSet.Size = new System.Drawing.Size(402, 25);
            this.tspTrainingSet.TabIndex = 6;
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
            this.btNewTrainingSet.Image = ((System.Drawing.Image)(resources.GetObject("btNewTrainingSet.Image")));
            this.btNewTrainingSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btNewTrainingSet.Name = "btNewTrainingSet";
            this.btNewTrainingSet.Size = new System.Drawing.Size(48, 22);
            this.btNewTrainingSet.Text = "&New";
            this.btNewTrainingSet.Click += new System.EventHandler(this.btNewTrainingSet_Click);
            // 
            // btRemoveTrainingSet
            // 
            this.btRemoveTrainingSet.Image = ((System.Drawing.Image)(resources.GetObject("btRemoveTrainingSet.Image")));
            this.btRemoveTrainingSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRemoveTrainingSet.Name = "btRemoveTrainingSet";
            this.btRemoveTrainingSet.Size = new System.Drawing.Size(66, 22);
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
            this.btImport.Image = ((System.Drawing.Image)(resources.GetObject("btImport.Image")));
            this.btImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btImport.Name = "btImport";
            this.btImport.Size = new System.Drawing.Size(59, 22);
            this.btImport.Text = "Import";
            this.btImport.Click += new System.EventHandler(this.btImport_Click);
            // 
            // btExport
            // 
            this.btExport.Image = ((System.Drawing.Image)(resources.GetObject("btExport.Image")));
            this.btExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(59, 22);
            this.btExport.Text = "Export";
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 500;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // pDocument
            // 
            this.ClientSize = new System.Drawing.Size(744, 422);
            this.Controls.Add(this.tcDesigner);
            this.KeyPreview = true;
            this.Name = "pDocument";
            this.TabText = "[NeuralNetwork1]";
            this.Text = "[NeuralNetwork1]";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.pDocument_KeyUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pDocument_KeyDown);
            this.tcDesigner.ResumeLayout(false);
            this.tbDesigner.ResumeLayout(false);
            this.MainDisplay.ResumeLayout(false);
            this.MainDisplay.PerformLayout();
            this.tspDesigner.ResumeLayout(false);
            this.tspDesigner.PerformLayout();
            this.tbTrainingSet.ResumeLayout(false);
            this.tbTrainingSet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTrainingSet)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tspTrainingSet.ResumeLayout(false);
            this.tspTrainingSet.PerformLayout();
            this.ResumeLayout(false);

        }

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

    }
}
