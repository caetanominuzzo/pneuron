using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Threading;

namespace primeira.pNeuron
{
    public class pPlotter : DockContent, IpDocks
    {
        private System.Threading.Timer tmRefresh;
        private IContainer components;
        private pGraphicPlotter pPlot;

        public pPlotter()
        {
            InitializeComponent();
        }


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
           
            this.pPlot = new primeira.pNeuron.pGraphicPlotter();
            this.SuspendLayout();
            // 
            // pPlot
            // 
            this.pPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pPlot.Location = new System.Drawing.Point(0, 0);
            this.pPlot.Name = "pPlot";
            this.pPlot.Size = new System.Drawing.Size(536, 180);
            this.pPlot.TabIndex = 0;
            this.pPlot.Text = "pGraphicPlotter1";
            // 
            // pPlotter
            // 
            this.ClientSize = new System.Drawing.Size(536, 180);
            this.Controls.Add(this.pPlot);
            this.Name = "pPlotter";
            this.TabText = "Plotter";
            this.Text = "Plotter";
            this.ResumeLayout(false);

        }

        public void StartTimer()
        {
            this.tmRefresh = new System.Threading.Timer(new TimerCallback(tmRefresh_Tick), null, 0, 250);
        }

        public void StopTimer()
        {
            this.tmRefresh.Dispose();
        }

        public void ClearData()
        {
            pPlot.ClearData();
        }

        private void tmRefresh_Tick(object state)
        {
            pPlot.AddData(Parent.ActiveDocument.MainDisplay.Net.GlobalError);
        }

        #region IpDocks Members

        public new pNeuronIDE Parent
        {
            get { return ((pNeuronIDE)DockPanel.Parent); }
        }

        #endregion

    }
}
