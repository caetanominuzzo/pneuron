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
        #region Fields

        private pProperty fmProperty = new pProperty();
        private pToolbox fmToolbox = new pToolbox();
        private pGroupExplorer fmGroupExplorer = new pGroupExplorer();
        private pPlotter fmPlotter = new pPlotter();
        private pLogger fmLogger = new pLogger();

        private List<pDocument> fmDocuments = new List<pDocument>();
        private pDocument fActiveDocument;

        #endregion

        #region Properties

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

                tspStartTrain.Enabled = tspResetLearning.Enabled = (fActiveDocument != null);

                fmToolbox.SetToolSet(ActiveDocument);

                fmGroupExplorer.treeView1.Items.Clear();
            }
        }

        #endregion

        #region Methods

        public pDocument GetDocByName(String sName)
        {
            foreach (pDocument p in fmDocuments)
                if (p.Filename == sName)
                    return p;

            return null;
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

        private void pNeuronIDE_Load(object sender, EventArgs e)
        {
            //Create Environment
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\pNeuron Projects"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\pNeuron Projects");
            }
        }

        #region New/Open/Save network

        private void AddDocument(pDocument document)
        {
            ((pDocument)ActiveDocument).Show(dockPanel, DockState.Document);
            ((pDocument)ActiveDocument).Modificated = true;
            ActiveDocument = fmDocuments[fmDocuments.Count - 1];
            document.OnDisplayStatusChanged += new pDocument.OnDisplayStatusChangedDelegate(p_OnDisplayStatusChanged);
            document.OnSelectedObjectChanged += new pDocument.OnSelectedObjectChangedDelegate(document_OnSelectedObjectChanged);
            document.Parent = this;
        }

        void document_OnSelectedObjectChanged()
        {
            if (ActiveDocument.MainDisplay.SelectedpPanels.Length == 0)
                fmProperty.Property.SelectedObject = ActiveDocument.MainDisplay;
            else if (ActiveDocument.MainDisplay.SelectedpPanels.Length == 1)
                fmProperty.Property.SelectedObject = ActiveDocument.MainDisplay.SelectedpPanels;
            else if (ActiveDocument.MainDisplay.SelectedpPanels.Length > 1)
                fmProperty.Property.SelectedObjects = ActiveDocument.MainDisplay.SelectedpPanels;
        }

        private pDocument AddDocument()
        {
            int i = fmDocuments.Count + 1;
            pDocument d = new pDocument("NeuralNetwork " + i.ToString());
            AddDocument(d);

            return d;
        }

        public void NewNetwork()
        {
            AddDocument();
        }

        public void OpenNetwork()
        {
            pDocument p = AddDocument();

            if (p.Load() != DialogResult.OK)
            {
                p.Close();
            }
        }

        void p_OnDisplayStatusChanged()
        {
            status.Items[0].Text = "Status: " + ActiveDocument.MainDisplay.DisplayStatus.ToString().Replace("_", " ");



            if (ActiveDocument.MainDisplay.DisplayStatus == pDisplay.pDisplayStatus.Training)
            {
                fmPlotter.StartTimer();
            }
            else
                fmPlotter.StopTimer();
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

        private void domainEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pDomainEdit fmDomainEdit = new pDomainEdit();
            fmDomainEdit.ShowDialog();
        }

        private void tspStartTrain_Click(object sender, EventArgs e)
        {
            ActiveDocument.StartTrain();
        }

        private void tspResetLearning_Click(object sender, EventArgs e)
        {
            ActiveDocument.ResetLearning();
        }

        private void pNeuronIDE_OnDocumentContainerChange()
        {
            fmToolbox.SetToolSet(ActiveDocument);
        }

        public void RefreshErrorStatus()
        {
            statusGlobalError.Text = "Global Error: " + ActiveDocument.MainDisplay.Net.GlobalError.ToString("#0.0000000000000");
            //TODO:statusMediaError.Text = "Media Error: " + media.ToString("#0.0000000000000");
        }

        #endregion

        #region Constructors

        public pNeuronIDE()
        {

            InitializeComponent();

            fmToolbox.Show(dockPanel, DockState.DockLeft);

            fmPlotter.Show(dockPanel, DockState.DockBottomAutoHide);
            fmLogger.Show(dockPanel, DockState.Document);
            fmLogger.DockTo(fmPlotter.Pane, DockStyle.Fill, 0);

            fmGroupExplorer.Show(dockPanel, DockState.DockRight);
            fmProperty.Show(dockPanel, DockState.DockRight);
            fmProperty.DockTo(fmGroupExplorer.Pane, DockStyle.Bottom, 0);

            //DEPRECATEDfmNetworkExplorer.Show(dockPanel, DockState.DockRight);
            //DEPRECATEDfmNetworkExplorer.DockTo(fmGroupExplorer.Pane, DockStyle.Fill, 0);


            //Calling the 'set' on ActiveDocument to keep consistency
            ActiveDocument = null;

        }

        #endregion

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

            
        }

        #endregion

    }
}