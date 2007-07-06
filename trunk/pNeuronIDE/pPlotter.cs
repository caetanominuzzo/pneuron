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
    public class pPlotter : DockContent, IpDocks
    {
        private Timer tmRefresh;
        private IContainer components;
        private pGraphicPlotter pPlot;

        public pPlotter()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmRefresh = new System.Windows.Forms.Timer(this.components);
            this.pPlot = new primeira.pNeuron.pGraphicPlotter();
            this.SuspendLayout();
            // 
            // tmRefresh
            // 
            this.tmRefresh.Tick += new System.EventHandler(this.tmRefresh_Tick);
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
            tmRefresh.Start();
        }

        public void StopTimer()
        {
            tmRefresh.Stop();
        }

        private void tmRefresh_Tick(object sender, EventArgs e)
        {
            pPlot.AddData(Parent.ActiveDocument.pDisplay1.Net.GlobalError);
        }

        #region IpDocks Members

        public pNeuronIDE Parent
        {
            get { return ((pNeuronIDE)DockPanel.Parent); }
        }

        #endregion

    }
}
