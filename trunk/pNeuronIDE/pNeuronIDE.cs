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
using primeira.pRandom;
using primeira.pNeuron;

namespace primeira.pNeuron
{
    

    public partial class pNeuronIDE : Form
    {
        static int TRUE_RANDOM_GENERATOR_CACHE = 20;

        #region Fields

        private pProperty fmProperty = new pProperty();
        private pToolbox fmToolbox = new pToolbox();
        private pGroupExplorer fmGroupExplorer = new pGroupExplorer();
        private pPlotter fmPlotter = new pPlotter();
        private pLogger fmLogger = new pLogger();

        private List<pDocument> fmDocuments = new List<pDocument>();
        private pDocument fActiveDocument;

        private pTrueRandomGenerator m_cache = new pTrueRandomGenerator(TRUE_RANDOM_GENERATOR_CACHE);

        private DateTime m_timeOnPreviousRefreshCycle = DateTime.Now;

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

                fmToolbox.SetToolSet(null);

                fmGroupExplorer.Clear();
            }
        }

        #endregion

        #region Document Events

        private void document_OnStopTraing()
        {
            tspStartTrain.Text = "Start Training";
        }

        private void document_OnStartTraing()
        {
            tspStartTrain.Text = "Stop Training";
            m_timeOnPreviousRefreshCycle = DateTime.Now;
        }

        private void document_OnRefreshCyclesSec(int Times)
        {
            int aCycles = Times;
            DateTime t;

            TimeSpan m = DateTime.Now.Subtract(m_timeOnPreviousRefreshCycle);
            double dFirst = m.TotalSeconds;

            double vFirst = ((double)(aCycles)) / dFirst;

            this.Invoke(new Assinc(delegate { statusCycles.Text = "Cycles/Sec.: " + vFirst.ToString("#0000"); }));
            this.Invoke(new Assinc(delegate { statusGlobalError.Text = "Global Error: " + ActiveDocument.MainDisplay.Net.GlobalError.ToString("#0.0000000000000"); }));
            
            //TODO:statusMediaError.Text = "Media Error: " + media.ToString("#0.0000000000000");
        }

        private void document_OnSelectedObjectChanged()
        {
            if (ActiveDocument.MainDisplay.SelectedpPanels.Length == 0)
                fmProperty.Property.SelectedObject = ActiveDocument.MainDisplay;
            else fmProperty.Property.SelectedObjects = ActiveDocument.MainDisplay.SelectedpPanels;
        }

        #endregion

        #region Methods

        /// <summary>
        /// To avoid the "Please create a new document or open one before try this." message on ActiveDocument property.
        /// </summary>
        /// <returns>True if there is an active document.</returns>
        public bool ThereIsAnActiveDocument()
        {
            return fActiveDocument != null;
        }

        /// <summary>
        /// This don't really closes the pDoc object since this method is called by it.
        /// Just change de ActiveDocument to an open or set it to null and closes sub items.
        /// </summary>
        /// <param name="aRemoveDocument"></param>
        public void PreRemoveDocument(pDocument aRemoveDocument)
        {
            fmDocuments.Remove(aRemoveDocument);
            fmGroupExplorer.Clear();

            if (fmDocuments.Count > 0)
            {
                ActiveDocument = fmDocuments[fmDocuments.Count - 1];
            }
            else
                ActiveDocument = null;

        }

        #endregion

        #region New/Open/Save Document

        private void AddDocument(pDocument document)
        {
            fmDocuments.Add(document);
            ActiveDocument = fmDocuments[fmDocuments.Count - 1];
            document.OnDisplayStatusChanged += new pDocument.OnDisplayStatusChangedDelegate(p_OnDisplayStatusChanged);
            document.OnSelectedObjectChanged += new pDocument.OnSelectedObjectChangedDelegate(document_OnSelectedObjectChanged);
            document.OnStartTraing += new pDocument.OnStartTraingDelegate(document_OnStartTraing);
            document.OnStopTraing += new pDocument.OnStopTraingDelegate(document_OnStopTraing);
            document.OnRefreshCyclesSec += new pDocument.OnRefreshCyclesSecDelegate(document_OnRefreshCyclesSec);
            document.Parent = this;

            ActiveDocument.Show(dockPanel, DockState.Document);
            ActiveDocument.Modificated = false;

            fmToolbox.SetToolSet(ActiveDocument);
        }
        
        private pDocument AddDocument()
        {
             int i = 1;
             foreach (pDocument doc in fmDocuments)
             {
                 if (doc.DefaultNamedFile)
                     i++;
             }

            pDocument d = new pDocument(m_cache, "NeuralNetwork " + i.ToString());
            AddDocument(d);

            return d;
        }

        private void OpenDocument()
        {
            pDocument p = new pDocument(this.m_cache);

            if (p.Load() != DialogResult.OK)
            {
                p.Close();
            }
            else AddDocument(p);
        }

        private void p_OnDisplayStatusChanged()
        {
            status.Items[0].Text = "Status: " + ActiveDocument.MainDisplay.DisplayStatus.ToString().Replace("_", " ");

            if (ActiveDocument.MainDisplay.DisplayStatus == pDisplay.pDisplayStatus.Training)
            {
                fmPlotter.StartTimer();
            }
            else
                fmPlotter.StopTimer();
        }

        private void SaveDocument()
        {
            ActiveDocument.Save();
        }

        private void SaveDocumentAs()
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

        #region Menus Events

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

            fmToolbox.SetToolSet(null);

            this.Invoke(new Assinc(Splasher.CloseSplash));

        }

        private void pNeuronIDE_FormClosing(object sender, FormClosingEventArgs e)
        {
            

            //To save non used cache
            m_cache.Dispose();
        }

        #endregion

        #region ToolStrip

        //FILE

        private void newNetworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddDocument();
        }

        private void openNetworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDocument();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveDocument();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDocumentAs();
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

        #endregion

        private void learninRate_MouseHover(object sender, EventArgs e)
        {
            pnTrackBar.Top = status.Top - pnTrackBar.Height;
            pnTrackBar.Visible = true;
            pnTrackBar.BringToFront();
        }

        private void trackLR_MouseLeave(object sender, EventArgs e)
        {
            pnTrackBar.Visible = false;
        }

        private void trackLR_Scroll(object sender, EventArgs e)
        {
            learninRate.Text = "Learning Rate: " + trackLR.Value;
            ActiveDocument.MainDisplay.Net.LearningRate = trackLR.Value;
        }
    }
}