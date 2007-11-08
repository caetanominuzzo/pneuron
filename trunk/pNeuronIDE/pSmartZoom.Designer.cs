using System;
using System.Collections.Generic;
using System.Text;

namespace primeira.pNeuron
{
    public partial class pSmartZoom
    {

        void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pSmartZoom));
            this.ZoomDisplay = new DoubleBufferedPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btCenter = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ZoomDisplay
            // 
            this.ZoomDisplay.BackColor = System.Drawing.Color.White;
            this.ZoomDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ZoomDisplay.Location = new System.Drawing.Point(0, 25);
            this.ZoomDisplay.Name = "ZoomDisplay";
            this.ZoomDisplay.Size = new System.Drawing.Size(272, 155);
            this.ZoomDisplay.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btCenter});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(272, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btCenter
            // 
            this.btCenter.Image = ((System.Drawing.Image)(resources.GetObject("btCenter.Image")));
            this.btCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btCenter.Name = "btCenter";
            this.btCenter.Size = new System.Drawing.Size(60, 22);
            this.btCenter.Text = "&Center";
            this.btCenter.Click += new System.EventHandler(this.btCenter_Click);
            // 
            // pSmartZoom
            // 
           // this.ClientSize = new System.Drawing.Size(272, 180);
            this.Controls.Add(this.ZoomDisplay);
            this.Controls.Add(this.toolStrip1);
            this.Name = "pSmartZoom";
            this.TabText = "pSmartZoom";
            this.Text = "pSmartZoom";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private DoubleBufferedPanel ZoomDisplay;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btCenter;
    }
}
