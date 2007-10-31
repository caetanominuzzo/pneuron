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
            this.tmMove = new System.Windows.Forms.Timer(this.components);
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
            this.ZoomDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ZoomDisplay_MouseMove);
            this.ZoomDisplay.Leave += new System.EventHandler(this.ZoomDisplay_Leave);
            // 
            // tmMove
            // 
            this.tmMove.Interval = 50;
            this.tmMove.Tick += new System.EventHandler(this.tmMove_Tick);
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
        private System.Windows.Forms.Timer tmMove;
        private System.ComponentModel.IContainer components;
    }
}
