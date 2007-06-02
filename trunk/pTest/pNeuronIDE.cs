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
        public pProperty fmProperty;
        public pToolbox fmToolbox = new pToolbox();
        public pTreeview fmTreeview = new pTreeview();
        public List<pDocument> fmDocuments = new List<pDocument>();
        public pDocument ActiveDocument;

        public pNeuronIDE()
        {
            

            InitializeComponent();
            fmProperty = new pProperty();

            fmToolbox.Show(dockPanel, DockState.DockLeft);

            fmTreeview.Show(dockPanel, DockState.DockRight);
            

            fmProperty.Show(dockPanel, DockState.DockRight);
            fmProperty.DockTo(fmTreeview.Pane, DockStyle.Bottom, 0);
           
        }

        private void newNeuralNetworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = fmDocuments.Count+1;
            fmDocuments.Add(new pDocument("NeuralNetwork " + i.ToString()));
            ActiveDocument = fmDocuments[fmDocuments.Count - 1];
            ActiveDocument.Show(dockPanel, DockState.Document);
        }

        private void propertyWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmProperty.Show();
        }

        private void networkExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmTreeview.Show();
        }

        private void toolBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmToolbox.Show();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //base.OnFormClosing(e);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
            ActiveDocument.Save();

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int i = fmDocuments.Count + 1;
            fmDocuments.Add(new pDocument("NeuralNetwork " + i.ToString()));
            ActiveDocument = fmDocuments[fmDocuments.Count - 1];
            ActiveDocument.Show(dockPanel, DockState.Document);

            if (ActiveDocument.Load() != DialogResult.OK)
                ActiveDocument.Close();
        }
    }
}