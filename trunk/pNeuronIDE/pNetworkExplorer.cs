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

namespace primeira.pNeuron
{
    public class pNetworkExplorer : DockContent, IpDocks
    {
        public pNetworkExplorer()
        {

            InitializeComponent();


        }

        public TreeView treeView1;
        #region IpDocks Members

        public pNeuronIDE Parent 
        {
            get { return ((pNeuronIDE)DockPanel.Parent); }
        }

        #endregion

        private void InitializeComponent()
        {
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(292, 273);
            this.treeView1.TabIndex = 0;
            // 
            // pNetworkExplorer
            // 
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.treeView1);
            this.Name = "pNetworkExplorer";
            this.TabText = "Network Explorer";
            this.Text = "Network Explorer";
            this.ResumeLayout(false);

        }
    }
}
