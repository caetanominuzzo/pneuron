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
        public pProperty fmProperty = new pProperty();
        public pToolbox fmToolbox = new pToolbox();
        public pGroupExplorer fmGroupExplorer = new pGroupExplorer();
        //DEPRECATEDpublic pNetworkExplorer fmNetworkExplorer = new pNetworkExplorer();
        public pPlotter fmPlotter = new pPlotter();


        public List<pDocument> fmDocuments = new List<pDocument>();
        private pDocument fActiveDocument;

        public pDocument GetDocByName(String sName)
        {
            foreach (pDocument p in fmDocuments)
                if (p.Filename == sName)
                    return p;

            return null;
        }

        public pDocument ActiveDocument
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

        public bool ActiveDocumentExists()
        {
            return fActiveDocument != null;
        }


        public void SetActiveDocument(pDocument aActiveDocument)
        {
            ActiveDocument = aActiveDocument;
        }

        /// <summary>
        /// This don't really closes the pDoc object since this method is called by it.
        /// Just change de ActiveDocument to an open or set it to null and closes sub items.
        /// </summary>
        /// <param name="aRemoveDocument"></param>
        public void PreRemoveDocument(pDocument aRemoveDocument)
        {
            fmDocuments.Remove(aRemoveDocument);
            fmGroupExplorer.treeView1.Clear();

            //DEPRECATED
            //if(fmNetworkExplorer.treeView1.Nodes!=null)
            //    if(fmNetworkExplorer.treeView1.Nodes[aRemoveDocument.Filename]!=null)
            //        foreach (TreeNode n in fmNetworkExplorer.treeView1.Nodes[aRemoveDocument.Filename].Nodes)
            //        {
            //            pDocument p = GetDocByName(n.Name);
            //            p.QueryOnClose = false;
            //            p.Close();
            //        }

            //fmNetworkExplorer.RemoveNode(aRemoveDocument.Filename);

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

            fmToolbox.Show(dockPanel, DockState.DockLeft);

            fmPlotter.Show(dockPanel, DockState.DockBottomAutoHide);

            fmGroupExplorer.Show(dockPanel, DockState.DockRight);

            //DEPRECATEDfmNetworkExplorer.Show(dockPanel, DockState.DockRight);
            //DEPRECATEDfmNetworkExplorer.DockTo(fmGroupExplorer.Pane, DockStyle.Fill, 0);


            fmProperty.Show(dockPanel, DockState.DockRight);
            fmProperty.DockTo(fmGroupExplorer.Pane, DockStyle.Bottom, 0);

            //Just to keep consistently
            ActiveDocument = null;

        }

        public void OpenAny(string sFilename, TreeNode FilenameParent)
        {
            foreach (pDocument p in fmDocuments)
            {
                if (p.Filename == sFilename)
                {
                    p.Show();
                    return;
                }
            }

            switch (Path.GetExtension(sFilename))
            {
                case ".pne": fmDocuments.Add(new pDocument(sFilename));
                    ActiveDocument = fmDocuments[fmDocuments.Count - 1];
                    ((pDocument)ActiveDocument).Show(dockPanel, DockState.Document);
                    ((pDocument)ActiveDocument).internalLoad(((pDocument)ActiveDocument).Filename);
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
            fmTrain.net = ((pDocument)ActiveDocument).pDisplay1.Net;
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
            fmDocuments.Add(new pDocument("NeuralNetwork " + i.ToString()));
            ActiveDocument = fmDocuments[fmDocuments.Count - 1];
            ((pDocument)ActiveDocument).Show(dockPanel, DockState.Document);
            ((pDocument)ActiveDocument).Modificated = true;
            //DEPRECATEDfmNetworkExplorer.AddNode(((pDocument)ActiveDocument).Filename);
        }

        public void OpenNetwork()
        {
            pDocument p = new pDocument();
            p.Parent = this;
            p.Load();

            fmDocuments.Add(p);
            ActiveDocument = fmDocuments[fmDocuments.Count - 1];
            ((pDocument)ActiveDocument).Show(dockPanel, DockState.Document);
            ((pDocument)ActiveDocument).Modificated = false;
            //DEPRECATEDfmNetworkExplorer.AddNode(((pDocument)ActiveDocument).Filename);
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