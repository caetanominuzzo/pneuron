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
    public partial class pNeuronIDE : Form
    {
        public pProperty fmProperty;
        public pToolbox fmToolbox = new pToolbox();
        public pGroupExplorer fmGroupExplorer = new pGroupExplorer();
        public pNetworkExplorer fmNetworkExplorer = new pNetworkExplorer();
        public List<pDoc> fmDocuments = new List<pDoc>();
        private pDoc fActiveDocument;

        public pDoc GetDocByName(String sName)
        {
            foreach (pDoc p in fmDocuments)
                if (p.Filename == sName)
                    return p;

            return null;
        }

        public pDoc ActiveDocument
        {
            get
            {
                if (fActiveDocument == null)
                {
                    pMessage.Alert("Please create a new document or open one before try this.");
                    return null;
                }

                return fActiveDocument;
            }
            internal set
            {
                fmToolbox.SetToolSet(value);
                fActiveDocument = value;
            }
        }


        public void SetActiveDocument(pDoc aActiveDocument)
        {
            ActiveDocument = aActiveDocument;
        }

        /// <summary>
        /// This dont really closes the pDoc object since this method is called by it.
        /// Just change de ActiveDocument to an open or set it to null and closes subitems.
        /// </summary>
        /// <param name="aRemoveDocument"></param>
        public void PreRemoveDocument(pDoc aRemoveDocument)
        {
            fmDocuments.Remove(aRemoveDocument);
            foreach (TreeNode n in fmNetworkExplorer.treeView1.Nodes[aRemoveDocument.Filename].Nodes)
            {
                pDoc p = GetDocByName(n.Name);
                p.QueryOnClose = false;
                p.Close();
            }

            fmNetworkExplorer.RemoveNode(aRemoveDocument.Filename);

            if (fmDocuments.Count > 0)
            {
                ActiveDocument = fmDocuments[fmDocuments.Count - 1];
            }
            else
                ActiveDocument = null;

        }


        public pNeuronIDE()
        {


            InitializeComponent();
            fmProperty = new pProperty();

            fmToolbox.Show(dockPanel, DockState.DockLeft);


            fmGroupExplorer.Show(dockPanel, DockState.DockRight);

            fmNetworkExplorer.Show(dockPanel, DockState.DockRight);
            fmNetworkExplorer.DockTo(fmGroupExplorer.Pane, DockStyle.Fill, 0);


            fmProperty.Show(dockPanel, DockState.DockRight);
            fmProperty.DockTo(fmGroupExplorer.Pane, DockStyle.Bottom, 0);

            //Just to keep consistently
            ActiveDocument = null;

        }

        public void OpenAny(string sFilename, TreeNode FilenameParent)
        {
            foreach (pDoc p in fmDocuments)
            {
                if (p.Filename == sFilename)
                {
                    p.Show();
                    return;
                }
            }

            switch (Path.GetExtension(sFilename))
            {
                case ".pne": fmDocuments.Add(new pDocDisplay(sFilename));
                    ActiveDocument = fmDocuments[fmDocuments.Count - 1];
                    ((pDocDisplay)ActiveDocument).Show(dockPanel, DockState.Document);
                    ((pDocDisplay)ActiveDocument).internalLoad(((pDocDisplay)ActiveDocument).Filename);
                    break;
                    

            }
            
            ActiveDocument.Modificated = false;
            ActiveDocument.DefaultNamedFile = false;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //Avoid base
        }

        #region ToolStrip

        //FILE

        private void newNetworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewNetwork();
        }

        private void openNetworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenNetwork();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveNetwork();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveNetworkAs();
        }

        //VIEW
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

        //TRAIN
        private void trainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pTrain fmTrain = new pTrain();
            fmTrain.net = ((pDocDisplay)ActiveDocument).pDisplay1.Net;
            fmTrain.Show();
        }

        #endregion

        private void pNeuronIDE_Load(object sender, EventArgs e)
        {
            //Create Environment
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\pNeuron Projects"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\pNeuron Projects");
            }
        }

        #region New/Open/Save network

        public void NewNetwork()
        {
            int i = fmDocuments.Count + 1;
            fmDocuments.Add(new pDocDisplay("NeuralNetwork " + i.ToString()));
            ActiveDocument = fmDocuments[fmDocuments.Count - 1];
            ((pDocDisplay)ActiveDocument).Show(dockPanel, DockState.Document);
            ((pDocDisplay)ActiveDocument).Modificated = true;
            fmNetworkExplorer.AddNode(((pDocDisplay)ActiveDocument).Filename);
        }

        public void OpenNetwork()
        {
            pDocDisplay p = new pDocDisplay();
            p.Parent = this;
            p.Load();

            fmDocuments.Add(p);
            ActiveDocument = fmDocuments[fmDocuments.Count - 1];
            ((pDocDisplay)ActiveDocument).Show(dockPanel, DockState.Document);
            ((pDocDisplay)ActiveDocument).Modificated = false;
        }
        
        public void SaveNetwork()
        {
            ActiveDocument.Save();
        }

        public void SaveNetworkAs()
        {
            bool bDefaul = ActiveDocument.DefaultNamedFile;
            string old = ActiveDocument.Filename;

            ActiveDocument.DefaultNamedFile = true;
            if (ActiveDocument.Save() == DialogResult.Cancel)
            {
                ActiveDocument.DefaultNamedFile = bDefaul;
            }

        }

        

        #endregion
        

    }
}