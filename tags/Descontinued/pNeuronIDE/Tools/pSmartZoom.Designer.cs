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
            this.ZoomDisplay = new primeira.pNeuron.DoubleBufferedPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btCenter = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ZoomDisplay
            // 
            this.ZoomDisplay.BackColor = System.Drawing.Color.White;
            this.ZoomDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ZoomDisplay.Location = new System.Drawing.Point(1, 26);
            this.ZoomDisplay.Name = "ZoomDisplay";
            this.ZoomDisplay.Size = new System.Drawing.Size(290, 246);
            this.ZoomDisplay.TabIndex = 0;
            this.ZoomDisplay.Paint += new System.Windows.Forms.PaintEventHandler(this.ZoomDisplay_Paint);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btCenter});
            this.toolStrip1.Location = new System.Drawing.Point(1, 1);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(290, 25);
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
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.ZoomDisplay);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "pSmartZoom";
            this.Padding = new System.Windows.Forms.Padding(1);
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
