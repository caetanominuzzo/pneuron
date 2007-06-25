using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using primeira.pNeuron.Core;

namespace primeira.pNeuron
{
    public partial class pNeuronIDE : Form
    {
        public pProperty fmProperty;
        public pToolbox fmToolbox = new pToolbox();
        public pGroupExplorer fmGroupExplorer = new pGroupExplorer();
        public pNetworkExplorer fmNetworkExplorer = new pNetworkExplorer();
        public List<pDocDisplay> fmDocuments = new List<pDocDisplay>();
        public pDocDisplay ActiveDocument;

        private string m_projectFilename;

        public string ProjectFilename
        {
            get { return m_projectFilename; }
        }

        public void Load()
        {
            OpenFileDialog s = new OpenFileDialog();
            s.DefaultExt = ".pnu";
            s.Filter = "pNeuron Network Project (*.pnp)|*.pnu|All files (*.*)|*.*";
            if (s.ShowDialog() == DialogResult.OK)
            {
                //TODO:If there is a project opened do the ord stuff
                internalLoad(s.FileName);
            }

        }

        private void internalLoad(string sFilename)
        {
            DataTable dt = new DataTable();
            
            try
            {
                dt.ReadXml(sFilename);
                
            }
        }

        public pNeuronIDE()
        {
            

            InitializeComponent();
            fmProperty = new pProperty();

            fmToolbox.Show(dockPanel, DockState.DockLeft);


            fmGroupExplorer.Show(dockPanel, DockState.DockRight);
            
            fmNetworkExplorer.Show(dockPanel, DockState.DockRight);
            fmNetworkExplorer.DockTo(fmGroupExplorer.Pane, DockStyle.Fill,0);
            

            fmProperty.Show(dockPanel, DockState.DockRight);
            fmProperty.DockTo(fmGroupExplorer.Pane, DockStyle.Bottom, 0);
           
        }

        private void newNeuralNetworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = fmDocuments.Count+1;
            fmDocuments.Add(new pDocDisplay("NeuralNetwork " + i.ToString()));
            ActiveDocument = fmDocuments[fmDocuments.Count - 1];
            ActiveDocument.Show(dockPanel, DockState.Document);
        }

        private void propertyWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmProperty.Show();
        }

        private void networkExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmGroupExplorer.Show();
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
            fmDocuments.Add(new pDocDisplay("NeuralNetwork " + i.ToString()));
            ActiveDocument = fmDocuments[fmDocuments.Count - 1];
            ActiveDocument.Show(dockPanel, DockState.Document);

            if (ActiveDocument.Load() != DialogResult.OK)
                ActiveDocument.Close();
        }

        private void trainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pTrain fmTrain = new pTrain();
            fmTrain.net.Initialize(1, 1);
            foreach(pPanel p in ActiveDocument.pDisplay1.pPanels)
            {
                fmTrain.net.Neuron.Add((Neuron)p.Tag);
            }
            fmTrain.Show();
        }

        private void dataEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}