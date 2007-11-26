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
using System.Text.RegularExpressions;
using primeira.pTypes;
using primeira.pHistory;
using Aced.Compression;


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
        private pSmartZoom fmSmartZoom = new pSmartZoom();
        private pHistory fmHistory = new pHistory();

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
                
            //    if (fActiveDocument != null) m_shortcut.UnloadFromForm(fActiveDocument.MainDisplay);

                fActiveDocument = value;

                tspStartTrain.Enabled = tspKnowledgement.Enabled = (fActiveDocument != null);

                fmToolbox.SetToolSet(fActiveDocument);

                SetMenus();

                fmGroupExplorer.Clear();

                fmProperty.cbItems.Items.Clear();

                fmHistory.pHistoryManager.Nodes.Clear();
                

                if (fActiveDocument != null)
                {

                    m_shortcut.AddEscope("Design");

                    fmProperty.cbItems.Items.Add(fActiveDocument.MainDisplay.Net);

                    fmProperty.Property.SelectedObject = fActiveDocument.MainDisplay.Net;

                    fmProperty.cbItems.SelectedItem = fmProperty.Property.SelectedObjects[0];

                    foreach (pPanel p in fActiveDocument.MainDisplay.pPanels)
                    {
                        fmProperty.cbItems.Items.Add(p);
                    }

                    PaintMiniMap(pChangeEscope.ZoomDisplayCache);

                    fmHistory.pHistoryManager.Load(fActiveDocument.History);

                    
                    //m_shortcut.LoadFromForm(fActiveDocument.MainDisplay);
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

        public List<pHistoryItem> ActiveDocumentHistory()
        {
            return fmHistory.pHistoryManager.GetHistory();
        }

        /// <summary>
        /// To avoid the "Please create a new document or open one before try this." message on ActiveDocument property.
        /// </summary>
        /// <returns>True if there is an active document and its really opened.</returns>
        public bool ThereIsAnActiveDocument()
        {
            return fActiveDocument != null && fActiveDocument.Parent != null;
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
            
            document.OnStartTraing += new pDocument.OnStartTraingDelegate(document_OnStartTraing);
            document.OnStopTraing += new pDocument.OnStopTraingDelegate(document_OnStopTraing);
            document.OnResetLearning += new pDocument.OnResetLearningDelegate(document_OnResetLearning);
            document.OnResetKnowledgement += new pDocument.OnResetKnowledgementDelegate(document_OnResetKnowledgement);
            document.OnNetworkChange += new pDocument.OnNetworkChangeDelegate(document_OnNetworkChange);
            document.Enter += new EventHandler(document_Enter);
            
            document.Parent = this;
            document.MainDisplay.SmartZoom = fmSmartZoom;
            
            ActiveDocument = fmDocuments[fmDocuments.Count - 1];

            ActiveDocument.Show(dockPanel, DockState.Document);

            ActiveDocument.Modificated = false;
        }

        private void RefreshPropertyWindowCombo()
        {
            while (fmProperty.cbItems.Items.Count != fActiveDocument.MainDisplay.pPanels.Count + 1)
            {
                foreach (pPanel p in fActiveDocument.MainDisplay.pPanels)
                {
                    if (!fmProperty.cbItems.Items.Contains(p))
                    {
                        fmProperty.cbItems.Items.Add(p);
                    }
                }
            }

            List<pPanel> pToRemove = new List<pPanel>();

            while (fmProperty.cbItems.Items.Count != fActiveDocument.MainDisplay.pPanels.Count + 1)
            {
                foreach (pPanel p in fmProperty.cbItems.Items)
                {
                    if (!fActiveDocument.MainDisplay.pPanels.Contains(p))
                    {
                        pToRemove.Add(p);
                    }
                }
            }

            foreach (pPanel p in pToRemove)
            {
                fmProperty.cbItems.Items.Remove(p);
            }
        }

        void document_OnNetworkChange(pChangeEscope escope)
        {
            if ((escope & pChangeEscope.DisplayStatus) == pChangeEscope.DisplayStatus)
            {
                if (ActiveDocument.MainDisplay.DisplayStatus != pDisplay.pDisplayStatus.Training)
                    fmToolbox.SetToolSet(ActiveDocument);

                status.Items[0].Text = "Status: " + ActiveDocument.MainDisplay.DisplayStatus.ToString().Replace("_", " ");

                if (ActiveDocument.MainDisplay.DisplayStatus == pDisplay.pDisplayStatus.Training)
                {
                    fmPlotter.StartTimer();
                }
                else
                    fmPlotter.StopTimer();
            }

            if ((escope & pChangeEscope.SelectedItem) == pChangeEscope.SelectedItem)
            {
                if (ActiveDocument.MainDisplay.SelectedpPanels.Length < 2)
                {
                    if (ActiveDocument.MainDisplay.SelectedpPanels.Length == 0)
                        fmProperty.Property.SelectedObject = ActiveDocument.MainDisplay.Net;
                    else fmProperty.Property.SelectedObjects = ActiveDocument.MainDisplay.SelectedpPanels;

                    fmProperty.cbItems.SelectedItem = fmProperty.Property.SelectedObjects[0];
                }
                else
                    fmProperty.cbItems.SelectedItem = null;
            }

            if ((escope & pChangeEscope.File) == pChangeEscope.File)
            {
                if (fmHistory.pHistoryManager.LowGranulatity)
                {
                    fmHistory.pHistoryManager.AddHistory(GiveMeAHistory());
                }
            }

            PaintMiniMap(escope);
        }

        //TODO:MOVE
        private static byte[] StrToByteArray(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }

        private static string ByteArrayToStr(byte[] bytes)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetString(bytes);
        }



        public pHistoryItem GiveMeAHistory()
        {
            pHistoryItem p = new pHistoryItem();
            p.Cache = fmSmartZoom.PreferedCache();
            

            StringBuilder sb = new StringBuilder();
            System.Xml.XmlWriter xml = System.Xml.XmlWriter.Create(sb);
            ActiveDocument.Render(xml);


            byte[] _originalData = StrToByteArray(sb.ToString());
            int _originalSize = _originalData.Length;
            byte[] _compData;

            _compData = AcedDeflator.Instance.Compress(_originalData, 0, _originalSize,
                AcedCompressionLevel.Normal, 0, 0);

            p.Content = _compData;

            return p;
        }

        public Stream GiveMeAHistory(byte[] history)
        {
            byte[] dest = AcedInflator.Instance.Decompress(history, 0, 0, 0);
            Stream s = new MemoryStream();

         // s.Write(dest, 0, dest.Length);
            TextWriter ts = new StreamWriter(s, Encoding.Unicode);
            ts.Write(ByteArrayToStr(dest));

            ts.Flush();
            
            //ts.Write();
            //ts.f
            
            return s;
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

        public void PaintMiniMap(pChangeEscope escope)
        {
            if (!ThereIsAnActiveDocument() || !fmSmartZoom.Visible)
                return;

            Graphics g;
            if ((escope & pChangeEscope.ZoomDisplayCache) == pChangeEscope.ZoomDisplayCache)
            {
                g = fmSmartZoom.ZoomCacheGraphics();
                g.Clear(Color.White);
                ActiveDocument.MainDisplay.Render(new PaintEventArgs(g, fmSmartZoom.ZoomRectangle()), 0, 0, .1f, true);
            }

            g = fmSmartZoom.ZoomGraphics();
            fmSmartZoom.ZoomInvalidate();
            ActiveDocument.MainDisplay.DrawMask(g);
            

        }

        void document_Enter(object sender, EventArgs e)
        {
            if (ActiveDocument != sender)
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

            fmHistory.Show(dockPanel, DockState.DockLeft);
            fmHistory.DockTo(fmToolbox.Pane, DockStyle.Bottom, 0);

            fmHistory.RevertHistory += new pHistory.RevertHistoryDelegate(fmHistory_RevertHistory);

            
            
            //fmLogger.Show(dockPanel, DockState.Document);
            //fmLogger.DockTo(fmPlotter.Pane, DockStyle.Fill, 0);

            fmSmartZoom.Show(dockPanel, DockState.DockRight);
            fmProperty.Show(dockPanel, DockState.DockRight);
            fmProperty.DockTo(fmSmartZoom.Pane, DockStyle.Bottom, 0);
          
            ActiveDocument = null;            

            #if RELEASE
              this.Invoke(new Assinc(Splasher.CloseSplash));
            #endif
        }

        void fmHistory_RevertHistory(byte[] history)
        {
            ActiveDocument.Revert(GiveMeAHistory(history));
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

        private void miniMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmSmartZoom.Show();
            if (ThereIsAnActiveDocument())
                PaintMiniMap(pChangeEscope.ZoomDisplayCache);
        }

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmHistory.Show();
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