using System;
using System.Collections.Generic;
using System.Text;

namespace primeira.pNeuron
{
    public partial class pSmartZoom
    {

        void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ZoomDisplay = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // ZoomDisplay
            // 
            this.ZoomDisplay.BackColor = System.Drawing.Color.White;
            this.ZoomDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ZoomDisplay.Location = new System.Drawing.Point(0, 0);
            this.ZoomDisplay.Name = "ZoomDisplay";
            this.ZoomDisplay.Size = new System.Drawing.Size(272, 180);
            this.ZoomDisplay.TabIndex = 0;
            // 
            // pSmartZoom
            // 
            this.ClientSize = new System.Drawing.Size(272, 180);
            this.Controls.Add(this.ZoomDisplay);
            this.Name = "pSmartZoom";
            this.TabText = "pSmartZoom";
            this.Text = "pSmartZoom";
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel ZoomDisplay;
        private System.ComponentModel.IContainer components;
    }
}
