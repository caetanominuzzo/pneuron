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
using System.Diagnostics;
using System.Threading;
using pShortcutManager;


namespace primeira.pNeuron
{
    

    public partial class pNeuronIDE : Form
    {
        public static int TRUE_RANDOM_GENERATOR_CACHE = 20;
        public  int PLOTTER_REFRESH_INTERVAL = 750;
        public  int STATUS_REFRESH_INTERVAL = 750;

        #region Fields

        private pProperty fmProperty = new pProperty();
        private pToolbox fmToolbox = new pToolbox();
        private pGroupExplorer fmGroupExplorer = new pGroupExplorer();
        private pPlotter fmPlotter = new pPlotter();
        private pLogger fmLogger = new pLogger();

        private List<pDocument> fmDocuments = new List<pDocument>();
        private pDocument fActiveDocument;

        private pTrueRandomGenerator m_cache = new pTrueRandomGenerator(TRUE_RANDOM_GENERATOR_CACHE);

        private Stopwatch m_refreshCycleTimer;

        private System.Threading.Timer tmRefresh;

        private pShortcutManager.pShortcutManager m_shortcut = new pShortcutManager.pShortcutManager();

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

            //    if (fActiveDocument != null) m_shortcut.UnloadFromForm(fActiveDocument.MainDisplay);

                fActiveDocument = value;

                tspStartTrain.Enabled = tspKnowledgement.Enabled = (fActiveDocument != null);

                fmToolbox.SetToolSet(null);

                SetMenus();

                fmGroupExplorer.Clear();

                if (fActiveDocument != null)
                {

                    m_shortcut.AddEscope("Design");

                    fmToolbox.SetToolSet(fActiveDocument);

                    fmProperty.Property.SelectedObject = fActiveDocument.MainDisplay.Net;

                    fmProperty.cbItems.Items.Clear();

                    fmProperty.cbItems.Items.Add(fActiveDocument.MainDisplay.Net.ToString());

                    foreach (pNeuron.Core.Neuron n in fActiveDocument.MainDisplay.Net.Neuron)
                    {
                        fmProperty.cbItems.Items.Add(n.ToString());
                    }

                    
                    m_shortcut.LoadFromForm(fActiveDocument.MainDisplay);
                }
                else
                    m_shortcut.RemoveEscope("Design");

                


            }
        }

        #endregion

        #region Document Events

        private void document_OnResetLearning()
        {
            
        }

        private void document_OnResetKnowledgement()
        {
            fmPlotter.ClearData();
        }

        private void document_OnStopTraing()
        {
            tspStartTrain.Text = "Start Training";

            StopTimer();
        }

        private void document_OnStartTraing()
        {
            tspStartTrain.Text = "Stop Training";
            if (m_refreshCycleTimer == null)
                m_refreshCycleTimer = Stopwatch.StartNew();
            else
            {
                m_refreshCycleTimer.Reset();
                m_refreshCycleTimer.Start();
            }

            StartTimer();
        }

        private void document_OnSelectedObjectChanged()
        {
            if (ActiveDocument.MainDisplay.SelectedpPanels.Length == 0)
                fmProperty.Property.SelectedObject = ActiveDocument.MainDisplay.Net;
            else fmProperty.Property.SelectedObjects = ActiveDocument.MainDisplay.SelectedpPanels;
        }

        #endregion

        #region Methods

        private void SetMenus()
        {

            tspStartTrain.Enabled =
                tspKnowledgement.Enabled =
                tspResetMemory.Enabled =
                tspPulse.Enabled =
                saveAsToolStripMenuItem.Enabled = 
                saveToolStripMenuItem.Enabled = ThereIsAnActiveDocument();
            
        }

        public void StartTimer()
        {
            this.tmRefresh = new System.Threading.Timer(new TimerCallback(tmRefresh_Tick), null, 0, STATUS_REFRESH_INTERVAL);
        }

        public void StopTimer()
        {
            if (tmRefresh != null)
                this.tmRefresh.Dispose();
        }

        private void tmRefresh_Tick(object state)
        {
            DateTime t;
            double vFirst = ((double)(ActiveDocument.MainDisplay.Net.Cycles )) / (double)(m_refreshCycleTimer.ElapsedMilliseconds / 1000);

            this.Invoke(new Assinc(delegate { statusCycles.Text = "Cycles/Sec.: " + vFirst.ToString("#00000"); }));
            this.Invoke(new Assinc(delegate { statusGlobalError.Text = "Global Error: " + ActiveDocument.MainDisplay.Net.LastCalculatedGlobalError.ToString(); }));
            this.Invoke(new Assinc(delegate { fmProperty.Refresh(); }));

            
        }

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

            //m_shortcut.LoadFromForm

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
            document.OnResetLearning += new pDocument.OnResetLearningDelegate(document_OnResetLearning);
            document.OnResetKnowledgement += new pDocument.OnResetKnowledgementDelegate(document_OnResetKnowledgement);
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
            d.Enter += new EventHandler(d_Enter);

            AddDocument(d);

            return d;
        }

        void d_Enter(object sender, EventArgs e)
        {
            ActiveDocument = (pDocument)sender;
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
            fmToolbox.SetToolSet(ActiveDocument);

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
            if(ThereIsAnActiveDocument())
                ActiveDocument.Save();
        }

        private void SaveDocumentAs()
        {
            if (ThereIsAnActiveDocument())
            {
                bool bDefaul = ActiveDocument.DefaultNamedFile;
                string old = ActiveDocument.Filename;

                ActiveDocument.DefaultNamedFile = true;
                if (ActiveDocument.Save() == DialogResult.Cancel)
                {
                    ActiveDocument.DefaultNamedFile = bDefaul;
                }
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
            if(ThereIsAnActiveDocument())
                ActiveDocument.StartTrain();
        }

        private void tspResetKnowledgement_Click(object sender, EventArgs e)
        {
            //its calls ResetKnowledgement too
            if(ThereIsAnActiveDocument())
                ActiveDocument.ResetLearning();
        }

        private void tspResetMemory_Click(object sender, EventArgs e)
        {
            if (ThereIsAnActiveDocument())
             ActiveDocument.ResetMemory();
        }

        private void tspPulse_Click(object sender, EventArgs e)
        {
            if (ThereIsAnActiveDocument())
            {
                List<double> input = new List<double>();

                using (fmInputData f = new fmInputData(ActiveDocument.MainDisplay.pPanels.ToArray()))
                {
                    int iInput = 0;
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        foreach (Control c in f.Controls)
                        {
                            if (c is TextBox)
                            {
                                input.Add(Util.Sigmoid(double.Parse(c.Text, System.Globalization.CultureInfo.InvariantCulture)));
                                iInput++;
                            }
                        }

                        ActiveDocument.Pulse(input.ToArray());
                    }
                }

                
            }
        }
          
        #endregion

        #region Constructors


        public pNeuronIDE()
        {

            InitializeComponent();
            fmPlotter.Show(dockPanel, DockState.DockBottom);
            fmToolbox.Show(dockPanel, DockState.DockLeft);

            fmProperty.Show(dockPanel);

            fmProperty.DockTo(dockPanel, DockStyle.Bottom);
            
            //fmLogger.Show(dockPanel, DockState.Document);
            //fmLogger.DockTo(fmPlotter.Pane, DockStyle.Fill, 0);

          //  fmGroupExplorer.Show(dockPanel, DockState.DockRight);
            fmProperty.Show(dockPanel, DockState.DockRight);
         //   fmProperty.DockTo(fmGroupExplorer.Pane, DockStyle.Bottom, 0);



            
            fmToolbox.SetToolSet(null);


            #if RELEASE
              this.Invoke(new Assinc(Splasher.CloseSplash));
            #endif
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

        private void plotterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmPlotter.Show();
        }

        private void networkExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmGroupExplorer.Show();
        }

        private void toolBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmToolbox.Show();
        }

        private void starterGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtendedWebBrowser2.Launcher.ShowBrowser(dockPanel, true);
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