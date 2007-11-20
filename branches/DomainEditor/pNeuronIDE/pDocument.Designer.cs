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
        private List<pTrainingSet> fpTrainingSet = new List<pTrainingSet>();
        private System.Windows.Forms.Timer refreshTimer;
        private IContainer components;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tcDesigner = new System.Windows.Forms.TabControl();
            this.tbDesigner = new System.Windows.Forms.TabPage();
            this.MainDisplay = new primeira.pNeuron.pDisplay();
            this.tspDesigner = new System.Windows.Forms.ToolStrip();
            this.tspAutoRefresh = new System.Windows.Forms.ToolStripButton();
            this.tbTrainingSet = new System.Windows.Forms.TabPage();
            this.pTrainingSetEditor1 = new primeira.pNeuron.pTrainingSetEditor();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.tcDesigner.SuspendLayout();
            this.tbDesigner.SuspendLayout();
            this.tspDesigner.SuspendLayout();
            this.tbTrainingSet.SuspendLayout();
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
            this.tbDesigner.Controls.Add(this.tspDesigner);
            this.tbDesigner.Location = new System.Drawing.Point(4, 4);
            this.tbDesigner.Name = "tbDesigner";
            this.tbDesigner.Padding = new System.Windows.Forms.Padding(3);
            this.tbDesigner.Size = new System.Drawing.Size(736, 396);
            this.tbDesigner.TabIndex = 0;
            this.tbDesigner.Text = "Network Designer";
            this.tbDesigner.UseVisualStyleBackColor = true;
            // 
            // MainDisplay
            // 
            this.MainDisplay.BackColor = System.Drawing.Color.White;
            this.MainDisplay.Bezier = true;
            this.MainDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainDisplay.CtrlKey = false;
            this.MainDisplay.Cursor = System.Windows.Forms.Cursors.Default;
            this.MainDisplay.DisplayStatus = primeira.pNeuron.pDisplay.pDisplayStatus.Idle;
            this.MainDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainDisplay.Location = new System.Drawing.Point(3, 28);
            this.MainDisplay.Name = "MainDisplay";
            this.MainDisplay.Offset = new System.Drawing.Point(0, 0);
            this.MainDisplay.OffsetX = 0;
            this.MainDisplay.OffsetY = 0;
            this.MainDisplay.ShiftKey = false;
            this.MainDisplay.Size = new System.Drawing.Size(730, 365);
            this.MainDisplay.SmartZoom = null;
            this.MainDisplay.TabIndex = 0;
            this.MainDisplay.Zoom = 1F;
            this.MainDisplay.OnNetworkChange += new primeira.pNeuron.pDisplay.NetworkChangeDelegate(this.MainDisplay_OnNetworkChange);
            // 
            // tspDesigner
            // 
            this.tspDesigner.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspAutoRefresh});
            this.tspDesigner.Location = new System.Drawing.Point(3, 3);
            this.tspDesigner.Name = "tspDesigner";
            this.tspDesigner.Size = new System.Drawing.Size(730, 25);
            this.tspDesigner.TabIndex = 1;
            this.tspDesigner.Text = "toolStrip2";
            // 
            // tspAutoRefresh
            // 
            this.tspAutoRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspAutoRefresh.Name = "tspAutoRefresh";
            this.tspAutoRefresh.Size = new System.Drawing.Size(76, 22);
            this.tspAutoRefresh.Text = "Start Refresh";
            this.tspAutoRefresh.Click += new System.EventHandler(this.tspAutoRefresh_Click);
            // 
            // tbTrainingSet
            // 
            this.tbTrainingSet.Controls.Add(this.pTrainingSetEditor1);
            this.tbTrainingSet.Location = new System.Drawing.Point(4, 4);
            this.tbTrainingSet.Margin = new System.Windows.Forms.Padding(0);
            this.tbTrainingSet.Name = "tbTrainingSet";
            this.tbTrainingSet.Padding = new System.Windows.Forms.Padding(3);
            this.tbTrainingSet.Size = new System.Drawing.Size(736, 396);
            this.tbTrainingSet.TabIndex = 1;
            this.tbTrainingSet.Text = "Training Sets";
            this.tbTrainingSet.UseVisualStyleBackColor = true;
            // 
            // pTrainingSetEditor1
            // 
            this.pTrainingSetEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pTrainingSetEditor1.Location = new System.Drawing.Point(3, 3);
            this.pTrainingSetEditor1.Name = "pTrainingSetEditor1";
            this.pTrainingSetEditor1.Size = new System.Drawing.Size(730, 390);
            this.pTrainingSetEditor1.TabIndex = 0;
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
            this.tbDesigner.PerformLayout();
            this.tspDesigner.ResumeLayout(false);
            this.tspDesigner.PerformLayout();
            this.tbTrainingSet.ResumeLayout(false);
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

        private ToolStrip tspDesigner;
        private ToolStripButton tspAutoRefresh;
        private pTrainingSetEditor pTrainingSetEditor1;

    }
}
