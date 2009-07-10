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
        private System.Windows.Forms.Timer refreshTimer;
        private IContainer components;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pDocument));
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.MainDisplay = new primeira.pNeuron.pDisplay();
            this.tspDesigner = new System.Windows.Forms.ToolStrip();
            this.tspAutoRefresh = new System.Windows.Forms.ToolStripButton();
            this.MainDisplay.SuspendLayout();
            this.tspDesigner.SuspendLayout();
            this.SuspendLayout();
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 500;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // MainDisplay
            // 
            this.MainDisplay.AutoScroll = true;
            this.MainDisplay.BackColor = System.Drawing.Color.White;
            this.MainDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainDisplay.Controls.Add(this.tspDesigner);
            this.MainDisplay.Cursor = System.Windows.Forms.Cursors.Default;
            this.MainDisplay.DisplayStatus = primeira.pNeuron.pDisplay.pDisplayStatus.Idle;
            this.MainDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainDisplay.Location = new System.Drawing.Point(0, 0);
            this.MainDisplay.Name = "MainDisplay";
            this.MainDisplay.Size = new System.Drawing.Size(744, 422);
            this.MainDisplay.TabIndex = 2;
            // 
            // tspDesigner
            // 
            this.tspDesigner.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspAutoRefresh});
            this.tspDesigner.Location = new System.Drawing.Point(0, 0);
            this.tspDesigner.Name = "tspDesigner";
            this.tspDesigner.Size = new System.Drawing.Size(742, 25);
            this.tspDesigner.TabIndex = 0;
            this.tspDesigner.Text = "toolStrip2";
            // 
            // tspAutoRefresh
            // 
            this.tspAutoRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tspAutoRefresh.Image")));
            this.tspAutoRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspAutoRefresh.Name = "tspAutoRefresh";
            this.tspAutoRefresh.Size = new System.Drawing.Size(92, 22);
            this.tspAutoRefresh.Text = "Start Refresh";
            // 
            // pDocument
            // 
            this.ClientSize = new System.Drawing.Size(744, 422);
            this.Controls.Add(this.MainDisplay);
            this.KeyPreview = true;
            this.Name = "pDocument";
            this.TabText = "[NeuralNetwork1]";
            this.Text = "]";
            this.MainDisplay.ResumeLayout(false);
            this.MainDisplay.PerformLayout();
            this.tspDesigner.ResumeLayout(false);
            this.tspDesigner.PerformLayout();
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

        public pDisplay MainDisplay;
        private ToolStrip tspDesigner;
        private ToolStripButton tspAutoRefresh;

    }
}
