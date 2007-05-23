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
    public partial class pNeuronIDE : Form
    {
        public pProperty property = new pProperty();
        public pToolbox toolbox = new pToolbox();
        public pTreeview treeview = new pTreeview();
        public List<pDocument> documents = new List<pDocument>();
        public pDocument ActiveDocument;


        public pNeuronIDE()
        {
            InitializeComponent();
            
            
            treeview.Show(dockPanel, DockState.DockRight);
            toolbox.Show(dockPanel, DockState.DockLeft);

            property.Show(dockPanel, DockState.DockRight);
            property.DockTo(treeview.Pane, DockStyle.Bottom, 0);
           
        }

        private void newNeuralNetworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            documents.Add(new pDocument());
            ActiveDocument = documents[documents.Count - 1];
            ActiveDocument.Show(dockPanel, DockState.Document);
        }
    }
}