using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace primeira.pNeuron
{
    class pPlot : DockContent
    {

        public pPlot()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // pPlot
            // 
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Name = "pPlot";
            this.TabText = "Plotter";
            this.Text = "Plotter";
            this.ResumeLayout(false);

        }
    }
}
