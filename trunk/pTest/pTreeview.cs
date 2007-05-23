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
    public class pTreeview : DockContent
    {
        public TreeView treeView1;

        public pTreeview()
        {

            InitializeComponent();
            

        }

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
            // pTreeview
            // 
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.treeView1);
            this.Name = "pTreeview";
            this.TabText = "Network Explorer";
            this.Text = "Network Explorer";
            this.ResumeLayout(false);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            ((pNeuronIDE)DockPanel.Parent).property.propertyGrid1.SelectedObject = ((pNeuronIDE)DockPanel.Parent).document.pDisplay1;
            int i = 0;
            foreach (List<pPanel> l in ((pNeuronIDE)DockPanel.Parent).document.pDisplay1.Groups())
            {
                treeView1.Nodes.Add("Group " + i.ToString() + " [" + l.Count.ToString() + "]");
                treeView1.Nodes[i].ImageIndex = i;
                i++;
            }

        }
    }
}
