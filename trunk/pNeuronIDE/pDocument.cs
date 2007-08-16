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
using System.Threading;

namespace primeira.pNeuron
{
    public class pDocument : DockContent, IpDocks
    {

        #region 

        #region IpDocks Members

        private pNeuronIDE fParent;

        private bool fQueryOnClose = true;

        public bool QueryOnClose
        {
            get { return fQueryOnClose; }
            set { fQueryOnClose = value; }
        }
        public pNeuronIDE Parent
        {
            get { return DockPanel == null ? fParent : ((pNeuronIDE)DockPanel.Parent); }
            set { fParent = value; }
        }

        #endregion

        private bool m_modificated = false;

        private string m_filename;
        private bool m_defaultNamedFile = true;

        public bool Modificated
        {
            get { return m_modificated; }
            set
            {
                if (value != m_modificated)
                {
                    if (!value)
                    {
                        this.TabText = this.TabText.Substring(0, TabText.Length - 1);
                    }
                    else
                    {
                        this.TabText = this.TabText + "*";
                    }
                    m_modificated = value;
                }

            }
        }

        public string Filename
        {
            get { return m_filename; }
            set
            {
                //DEPRECATED
                //if (Parent != null)
                //{
                //    Parent.fmNetworkExplorer.RenameNode(m_filename, value);
                //}
                m_filename = value;
                this.TabText = value;
            }
        }

        public bool DefaultNamedFile
        {
            get { return m_defaultNamedFile; }
            set { m_defaultNamedFile = value; }
        }


        #endregion


        internal class pTrainingSet
        {
            public String Name;
            public DataTable fDataTable = new DataTable();
            private List<pPanel> fpPanel;
            private string fParentFilename;

            public pTrainingSet(List<pPanel> apPanel, String aName, string aParentFilename)
            {
                Name = aName;
                fpPanel = apPanel;
                fParentFilename = aParentFilename;
            }

            public DataTable NewDataTable()
            {
                fDataTable = new DataTable(Name);

                foreach (pPanel p in fpPanel)
                {
                    if( (p.Neuron).NeuronType == NeuronTypes.Input)
                    {
                        fDataTable.Columns.Add(p.Text, typeof(double));
                    }
                }

                foreach (pPanel p in fpPanel)
                {
                    if ((p.Neuron).NeuronType == NeuronTypes.Output)
                    {
                        fDataTable.Columns.Add(p.Text, typeof(double));
                    }
                }

                return fDataTable;
            }

            public DataTable LoadDataTable()
            {
                DataSet ds = new DataSet();
                ds.ReadXml(fParentFilename);

                fDataTable = ds.Tables[this.Name];
                return fDataTable;
            }
        }

        private IContainer components;
        public pDisplay pDisplay1;
        private TabControl tcDesigner;
        private TabPage tbDesigner;
        private TabPage tbTrainingSet;
        private DataGridView dataGridView1;
        private List<pTrainingSet> fpTrainingSet = new List<pTrainingSet>();
        private FlowLayoutPanel flowLayoutPanel1;
        private ToolStrip toolStrip1;
        private ToolStripComboBox cbTrainingSets;
        private ToolStripButton btTrain;
        private ToolStripButton btReset;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btNewTrainingSet;
        private ToolStripButton btRemoveTrainingSet;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btImport;
        private ToolStripButton btExport;

        private const int INNER_TRAINING_TIMES = 100;

        public pDocument(string sFileName) : this()
        {
            Filename = sFileName;
        }

        public pDocument()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pDocument));
            this.tcDesigner = new System.Windows.Forms.TabControl();
            this.tbDesigner = new System.Windows.Forms.TabPage();
            this.pDisplay1 = new primeira.pNeuron.pDisplay();
            this.tbTrainingSet = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cbTrainingSets = new System.Windows.Forms.ToolStripComboBox();
            this.btTrain = new System.Windows.Forms.ToolStripButton();
            this.btReset = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btNewTrainingSet = new System.Windows.Forms.ToolStripButton();
            this.btRemoveTrainingSet = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btImport = new System.Windows.Forms.ToolStripButton();
            this.btExport = new System.Windows.Forms.ToolStripButton();
            this.tcDesigner.SuspendLayout();
            this.tbDesigner.SuspendLayout();
            this.tbTrainingSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcDesigner
            // 
            this.tcDesigner.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tcDesigner.Controls.Add(this.tbDesigner);
            this.tcDesigner.Controls.Add(this.tbTrainingSet);
            this.tcDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcDesigner.Location = new System.Drawing.Point(0, 0);
            this.tcDesigner.Margin = new System.Windows.Forms.Padding(0);
            this.tcDesigner.Name = "tcDesigner";
            this.tcDesigner.SelectedIndex = 0;
            this.tcDesigner.Size = new System.Drawing.Size(744, 422);
            this.tcDesigner.TabIndex = 1;
            this.tcDesigner.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tcDesigner_Selecting);
            // 
            // tbDesigner
            // 
            this.tbDesigner.Controls.Add(this.pDisplay1);
            this.tbDesigner.Location = new System.Drawing.Point(4, 4);
            this.tbDesigner.Name = "tbDesigner";
            this.tbDesigner.Padding = new System.Windows.Forms.Padding(3);
            this.tbDesigner.Size = new System.Drawing.Size(736, 396);
            this.tbDesigner.TabIndex = 0;
            this.tbDesigner.Text = "Network Designer";
            this.tbDesigner.UseVisualStyleBackColor = true;
            // 
            // pDisplay1
            // 
            this.pDisplay1.AutoScroll = true;
            this.pDisplay1.AutoScrollHorizontalMaximum = 100;
            this.pDisplay1.AutoScrollHorizontalMinimum = 0;
            this.pDisplay1.AutoScrollHPos = 0;
            this.pDisplay1.AutoScrollVerticalMaximum = 100;
            this.pDisplay1.AutoScrollVerticalMinimum = 0;
            this.pDisplay1.AutoScrollVPos = 0;
            this.pDisplay1.BackColor = System.Drawing.Color.White;
            this.pDisplay1.Bezier = true;
            this.pDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pDisplay1.CtrlKey = false;
            this.pDisplay1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pDisplay1.DisplayStatus = primeira.pNeuron.pDisplay.pDisplayStatus.Idle;
            this.pDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pDisplay1.EnableAutoScrollHorizontal = true;
            this.pDisplay1.EnableAutoScrollVertical = true;
            this.pDisplay1.Location = new System.Drawing.Point(3, 3);
            this.pDisplay1.Name = "pDisplay1";
            this.pDisplay1.ShiftKey = false;
            this.pDisplay1.Size = new System.Drawing.Size(730, 390);
            this.pDisplay1.TabIndex = 0;
            this.pDisplay1.VisibleAutoScrollHorizontal = true;
            this.pDisplay1.VisibleAutoScrollVertical = true;
            this.pDisplay1.OnDisplayStatusChange += new primeira.pNeuron.pDisplay.DisplayStatusChangeDelegate(this.pDisplay1_OnDisplayStatusChange);
            this.pDisplay1.OnTreeViewChange += new primeira.pNeuron.pDisplay.TreeViewChangeDelegate(this.pDisplay1_OnTreeViewChange);
            this.pDisplay1.OnSelectedPanelsChange += new primeira.pNeuron.pDisplay.SelectedPanelsChangeDelegate(this.pDisplay1_OnSelectedPanelsChange);
            this.pDisplay1.OnNetworkChange += new primeira.pNeuron.pDisplay.NetworkChangeDelegate(this.pDisplay1_OnNetworkChange);
            this.pDisplay1.OnNewDomain += new primeira.pNeuron.pDisplay.NewDomainDelegate(this.pDisplay1_OnNewDomain);
            // 
            // tbTrainingSet
            // 
            this.tbTrainingSet.Controls.Add(this.dataGridView1);
            this.tbTrainingSet.Controls.Add(this.flowLayoutPanel1);
            this.tbTrainingSet.Location = new System.Drawing.Point(4, 4);
            this.tbTrainingSet.Margin = new System.Windows.Forms.Padding(0);
            this.tbTrainingSet.Name = "tbTrainingSet";
            this.tbTrainingSet.Padding = new System.Windows.Forms.Padding(3);
            this.tbTrainingSet.Size = new System.Drawing.Size(736, 396);
            this.tbTrainingSet.TabIndex = 1;
            this.tbTrainingSet.Text = "Training Sets";
            this.tbTrainingSet.UseVisualStyleBackColor = true;
            this.tbTrainingSet.Enter += new System.EventHandler(this.tbTrainingSet_Enter);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 28);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(730, 365);
            this.dataGridView1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.toolStrip1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(730, 25);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowDrop = true;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbTrainingSets,
            this.btTrain,
            this.btReset,
            this.toolStripSeparator1,
            this.btNewTrainingSet,
            this.btRemoveTrainingSet,
            this.toolStripSeparator2,
            this.btImport,
            this.btExport});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(553, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cbTrainingSets
            // 
            this.cbTrainingSets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTrainingSets.Name = "cbTrainingSets";
            this.cbTrainingSets.Size = new System.Drawing.Size(121, 25);
            this.cbTrainingSets.SelectedIndexChanged += new System.EventHandler(this.cbTrainingSets_SelectedIndexChanged);
            // 
            // btTrain
            // 
            this.btTrain.Image = ((System.Drawing.Image)(resources.GetObject("btTrain.Image")));
            this.btTrain.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btTrain.Name = "btTrain";
            this.btTrain.Size = new System.Drawing.Size(92, 22);
            this.btTrain.Text = "Start Training";
            this.btTrain.Click += new System.EventHandler(this.btTrain_Click);
            // 
            // btReset
            // 
            this.btReset.Image = ((System.Drawing.Image)(resources.GetObject("btReset.Image")));
            this.btReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(82, 22);
            this.btReset.Text = "Reset Train";
            this.btReset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btNewTrainingSet
            // 
            this.btNewTrainingSet.Image = ((System.Drawing.Image)(resources.GetObject("btNewTrainingSet.Image")));
            this.btNewTrainingSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btNewTrainingSet.Name = "btNewTrainingSet";
            this.btNewTrainingSet.Size = new System.Drawing.Size(48, 22);
            this.btNewTrainingSet.Text = "&New";
            this.btNewTrainingSet.Click += new System.EventHandler(this.btNewTrainingSet_Click);
            // 
            // btRemoveTrainingSet
            // 
            this.btRemoveTrainingSet.Image = ((System.Drawing.Image)(resources.GetObject("btRemoveTrainingSet.Image")));
            this.btRemoveTrainingSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRemoveTrainingSet.Name = "btRemoveTrainingSet";
            this.btRemoveTrainingSet.Size = new System.Drawing.Size(66, 22);
            this.btRemoveTrainingSet.Text = "Remove";
            this.btRemoveTrainingSet.Click += new System.EventHandler(this.btRemoveTrainingSet_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btImport
            // 
            this.btImport.Image = ((System.Drawing.Image)(resources.GetObject("btImport.Image")));
            this.btImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btImport.Name = "btImport";
            this.btImport.Size = new System.Drawing.Size(59, 22);
            this.btImport.Text = "Import";
            this.btImport.Click += new System.EventHandler(this.btImport_Click);
            // 
            // btExport
            // 
            this.btExport.Image = ((System.Drawing.Image)(resources.GetObject("btExport.Image")));
            this.btExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(59, 22);
            this.btExport.Text = "Export";
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // pDocument
            // 
            this.ClientSize = new System.Drawing.Size(744, 422);
            this.Controls.Add(this.tcDesigner);
            this.KeyPreview = true;
            this.Name = "pDocument";
            this.TabText = "[NeuralNetwork1]";
            this.Text = "[NeuralNetwork1]";
            this.Activated += new System.EventHandler(this.pDocument_Activated);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.pDocument_KeyUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pDocument_KeyDown);
            this.tcDesigner.ResumeLayout(false);
            this.tbDesigner.ResumeLayout(false);
            this.tbTrainingSet.ResumeLayout(false);
            this.tbTrainingSet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        private void pDisplay1_OnDisplayStatusChange()
        {
            Parent.status.Items[0].Text = "Status: " + pDisplay1.DisplayStatus.ToString().Replace("_", " ");

            switch (pDisplay1.DisplayStatus)
            {
                case pDisplay.pDisplayStatus.Add_Neuron:
                    Parent.fmToolbox.rNeuron.Checked = true;
                    break;
                case pDisplay.pDisplayStatus.Linking:
                case pDisplay.pDisplayStatus.Linking_Paused:
                    Parent.fmToolbox.rSynapse.Checked = true;
                    break;
                case pDisplay.pDisplayStatus.Remove_Neuron:
                    Parent.fmToolbox.rRemove.Checked = true;
                    break;
                default:
                    Parent.fmToolbox.rCursor.Checked = true;
                    break;
            }

            if (pDisplay1.DisplayStatus == pDisplay.pDisplayStatus.Training)
            {
                Parent.fmPlotter.StartTimer();
                Parent.fmPlotter.DockState = DockState.DockBottom;
            }
            else
            {
                Parent.fmPlotter.StopTimer();
                Parent.fmPlotter.DockState = DockState.DockBottomAutoHide;
            }

        }

        public void pDocument_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    pDisplay1.DisplayStatus = pDisplay.pDisplayStatus.Remove_Neuron;
                    break;
                case Keys.B: pDisplay1.Bezier = !pDisplay1.Bezier;
                    Invalidate();
                    break;
                case Keys.K: //Log ShiftB
                    pDisplay1.Logger.Visible = !pDisplay1.Logger.Visible;
                    break;
                case Keys.Escape:
                    pDisplay1.UnSelect();
                    pDisplay1.DisplayStatus = pDisplay.pDisplayStatus.Idle;
                    break;
                case Keys.L: //Link Mode
                    pDisplay1.DisplayStatus = pDisplay.pDisplayStatus.Linking_Paused;
                    break;
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                case Keys.D0:

                    int iKey = Convert.ToInt16(e.KeyCode.ToString().Replace("D", ""));

                    if (e.Control) //Create
                    {
                        if (pDisplay1.SelectedpPanels.Length == 0)
                        {
                            pDisplay1.GroupFree(iKey);
                        }

                        if (!e.Shift)
                            pDisplay1.GroupFree(iKey);

                        foreach (pPanel p in pDisplay1.SelectedpPanels)
                        {
                            pDisplay1.Add(p, iKey);
                        }

                    }
                    else
                    {

                        if (!e.Shift)
                            pDisplay1.UnSelect();


                        pDisplay1.GroupSelect(iKey);
                    }

                    break;
            }

            if (!e.Shift)
                pDisplay1.ShiftKey = false;

            if (!e.Control)
                pDisplay1.CtrlKey = false;
        }

        public void pDocument_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                pDisplay1.ShiftKey = true;
            }

            if (e.KeyCode == Keys.ControlKey)
            {
                pDisplay1.CtrlKey = true;
            }
        }

        private void pDisplay1_OnSelectedPanelsChange()
        {
            if (pDisplay1.SelectedpPanels.Length == 0)
            {
                Parent.fmProperty.Property.SelectedObject = pDisplay1;
            }
            else
            {
                Parent.fmProperty.Property.SelectedObjects = pDisplay1.SelectedpPanels;
            }


        }

        public void pDisplay1_OnTreeViewChange(object o, pTreeviewRefresh mode)
        {


            if (mode == pTreeviewRefresh.pPanelAdd)
            {
                pPanel p = (pPanel)o;

                int iGroup = p.Groups;

                ListViewItem lvi = new ListViewItem(p.Name, (Parent.fmGroupExplorer.treeView1.Groups[iGroup ]));

                Parent.fmGroupExplorer.treeView1.Items.Add(lvi);

                int k = Parent.fmGroupExplorer.treeView1.Groups[iGroup ].Items.Count - 1;

                Parent.fmGroupExplorer.treeView1.Groups[iGroup ].Items[k].Tag = p;
            }
            else if (mode == pTreeviewRefresh.pPanelRemove)
            {
                pPanel p = (pPanel)o;
                ListViewItem toDelete = null;
                foreach (ListViewGroup lg in Parent.fmGroupExplorer.treeView1.Groups)
                    foreach (ListViewItem lv in lg.Items)
                    {
                        if (((pPanel)lv.Tag) == p)
                        {
                            toDelete = lv;
                            
                        }
                    }
                if (toDelete != null)
                {
                    toDelete.Group = null;
                    Parent.fmGroupExplorer.treeView1.Items.Remove(toDelete);
                }
            }
            else if(mode == pTreeviewRefresh.pGroupClear)
            {
                if( ((int)o) == 0)
                    return;
                
                List<ListViewItem> toDelete = new List<ListViewItem>();

                foreach (ListViewItem lv in Parent.fmGroupExplorer.treeView1.Groups[(int)o].Items)
                {
                    toDelete.Add(lv);
                }

                foreach (ListViewItem lv in toDelete)
                {
                    lv.Group = null;
                    Parent.fmGroupExplorer.treeView1.Items.Remove(lv);
                }
            }
            else if (mode == pTreeviewRefresh.pFullRefreh)
            {
                foreach (pPanel p in pDisplay1.pPanels)
                {
                    pDisplay1_OnTreeViewChange(p, pTreeviewRefresh.pPanelAdd);
                }
            }
        
        }

        private void pDocument_Activated(object sender, EventArgs e)
        {
            Parent.SetActiveDocument(this);

            switch (pDisplay1.DisplayStatus)
            {
                case pDisplay.pDisplayStatus.Add_Neuron:
                    Parent.fmToolbox.rNeuron.Checked = true;
                    break;
                case pDisplay.pDisplayStatus.Linking_Paused:
                case pDisplay.pDisplayStatus.Linking:
                    Parent.fmToolbox.rSynapse.Checked = true;
                    break;
                default:
                    Parent.fmToolbox.rCursor.Checked = true;
                    break;

            }

            Parent.fmGroupExplorer.treeView1.Items.Clear();

            pDisplay1_OnTreeViewChange(null, pTreeviewRefresh.pFullRefreh);


        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (Modificated)
            {
                DialogResult r = pMessage.Confirm("Save changes on " + this.Filename + "?", MessageBoxButtons.YesNoCancel);

                switch (r)
                {
                    case DialogResult.No:
                        if (DefaultNamedFile)
                        {

                            Parent.PreRemoveDocument(this);
                        }
                        return;
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    case DialogResult.Yes:
                        Save();
                        break;

                }
            }



            Parent.PreRemoveDocument(this);
  

        }

        private void pDisplay1_OnNetworkChange()
        {
            this.Modificated = true;
        }

        public void AddTrainingSet()
        {
            int i = fpTrainingSet.Count + 1;
            fpTrainingSet.Add(new pTrainingSet(pDisplay1.pPanels, "Training Set " + i.ToString(), this.Filename));
            pTrainingSet fm = fpTrainingSet[fpTrainingSet.Count - 1];
            dataGridView1.DataSource = fm.NewDataTable();

            if(tcDesigner.SelectedTab == tbDesigner)
            {
                tcDesigner.SelectedTab = tbTrainingSet;
            }

            cbTrainingSets.Items.Add(fm.Name);
            cbTrainingSets.SelectedIndex = cbTrainingSets.Items.Count - 1;

            btRemoveTrainingSet.Enabled = true;

            //DEPRECATEDParent.fmNetworkExplorer.AddNode(fm.Name, ((pDocument)Parent.ActiveDocument).Filename);
        }

        #region Save/Load

        public DialogResult Save()
        {
            if (DefaultNamedFile)
            {
                SaveFileDialog s = new SaveFileDialog();
                s.DefaultExt = ".pne";
                s.FileName = System.IO.Path.GetFileNameWithoutExtension(Filename) + ".pne";
                s.Filter = "pNeuron Network (*.pne)|*.pne|All files (*.*)|*.*";
                if (s.ShowDialog() == DialogResult.OK)
                {
                    Filename = s.FileName;
                    internalSave();
                    Modificated = false;
                    DefaultNamedFile = false;
                    return DialogResult.OK;
                }
                else
                {
                    return DialogResult.Cancel;
                }
            }
            else
            {
                internalSave();
                Modificated = false;
                return DialogResult.OK;
            }

        }

        private void internalSave()
        {
            DataSet ds = new DataSet(System.IO.Path.GetFileNameWithoutExtension(Filename));

            DataTable tNeurons = new DataTable("pNeuron");
            tNeurons.Columns.Add("Name", typeof(String));
            tNeurons.Columns.Add("LocationX", typeof(int));
            tNeurons.Columns.Add("LocationY", typeof(int));
            tNeurons.Columns.Add("Group", typeof(Int32));
            tNeurons.Columns.Add("Bias", typeof(double));
            tNeurons.Columns.Add("Value", typeof(double));
            tNeurons.Columns.Add("NeuronType", typeof(NeuronTypes));


            DataTable tSynapse = new DataTable("pSynapse");
            tSynapse.Columns.Add("NeuronOut", typeof(string));
            tSynapse.Columns.Add("NeuronIn", typeof(string));
            tSynapse.Columns.Add("Value", typeof(double));

            
            DataTable tFiles = new DataTable("pTrainningSet");
            tFiles.Columns.Add("File", typeof(string));

            foreach (pPanel p in pDisplay1.pPanels)
            {

                    tNeurons.Rows.Add(
                        new object[]
                        {
                            p.Name,
                            p.Location.X,
                            p.Location.Y,
                            p.Groups,
                            (p.Neuron).Bias.Weight,
                            (p.Neuron).Value,
                            (p.Neuron).NeuronType
                        }
                    );
               


                foreach (pPanel pp in pDisplay1.pPanels)
                {
                    if (pp.Neuron.GetSynapseFrom(p.Neuron)!=null)
                    {
                        INeuron[] arNeuron = (pp.Neuron).GetInputNeurons();
                        foreach (INeuron nn in arNeuron)
                        {
                            if (nn == (p.Neuron))
                            {
                                
                                    tSynapse.Rows.Add(new object[]{
                                                        p.Name,
                                                        pp.Name,
                                                        (pp.Neuron).GetSynapseFrom(nn).Weight });
       
                                break;
                            }
                        }
                    }
                }


            }



            ds.Tables.Add(tNeurons);
            ds.Tables.Add(tSynapse);

            foreach (pTrainingSet p in fpTrainingSet)
            {
                if(p.fDataTable.DataSet!=null)
                    p.fDataTable.DataSet.Tables.Remove(p.fDataTable.TableName);
            }

            foreach (pTrainingSet p in fpTrainingSet)
            {
                ds.Tables.Add(p.fDataTable);
            }

            ds.WriteXml(Filename);

           
        }

        public DialogResult Load()
        {
            if (Modificated )
                Save();

            OpenFileDialog s = new OpenFileDialog();
            s.DefaultExt = ".pne";
            s.Filter = "pNeuron Network (*.pne)|*.pne|All files (*.*)|*.*";
            if (s.ShowDialog() == DialogResult.OK)
            {
                Filename = s.FileName;
                internalLoad(s.FileName);
                
                Modificated = false;
                DefaultNamedFile = false;
                pDisplay1.Invalidate();
                return DialogResult.OK;
            }
            else
            {
                return DialogResult.Cancel;
            } 
        }

        public void internalLoad(string aFilename)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(aFilename);

            if (ds.Tables["pNeuron"] != null)
            foreach (DataRow r in ds.Tables["pNeuron"].Rows)
            {
                pPanel p = pDisplay1.Add();
                p.Neuron.Bias.Weight = Convert.ToDouble(r["Bias"], System.Globalization.CultureInfo.InvariantCulture);
                p.Name = r["Name"].ToString();
                p.Location = new Point( Convert.ToInt32(r["LocationX"]), Convert.ToInt32(r["LocationY"]) );
                pDisplay1.Add(p, Convert.ToInt32(r["Group"]));

                (p.Neuron).Value = Convert.ToDouble(r["Value"], System.Globalization.CultureInfo.InvariantCulture);
                (p.Neuron).NeuronType = (NeuronTypes)Convert.ToInt16(r["NeuronType"]);

            }

            if(ds.Tables["pSynapse"]!=null)
            foreach (DataRow r in ds.Tables["pSynapse"].Rows)
            {
                foreach (pPanel p in pDisplay1.pPanels)
                {
                    if (r["NeuronOut"].ToString() == p.Name)
                    {
                        foreach (pPanel pp in pDisplay1.pPanels)
                        {
                            if (r["NeuronIn"].ToString() == pp.Name)
                            {
                                pp.Neuron.AddSynapse(p.Neuron,  Convert.ToDouble(r["Value"], System.Globalization.CultureInfo.InvariantCulture));
                                break;
                            }
                        }
                        break;
                    }
                }
            }

            foreach(DataTable dt in ds.Tables)
            {
                if (dt.TableName == "pSynapse" || dt.TableName == "pNeuron")
                    continue;

                fpTrainingSet.Add(new pTrainingSet(this.pDisplay1.pPanels, dt.TableName, this.Filename));
                cbTrainingSets.Items.Clear();
                cbTrainingSets.Items.Add(dt.TableName);

                fpTrainingSet[fpTrainingSet.Count - 1].LoadDataTable();
            }




        }

        #endregion


        protected override void OnMouseEnter(EventArgs e)
        {
            this.Focus();
            base.OnMouseEnter(e);
        }


        private void tcDesigner_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if(Modificated)
            {
                DialogResult r = pMessage.Confirm("You must save the network design before add or edit a training set.\nDo you want to save now?", MessageBoxButtons.YesNoCancel);
                if (r == DialogResult.Yes)
                {
                    Save();
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }

            if (fpTrainingSet.Count > 0)
            {
                cbTrainingSets.SelectedIndex = 0;
            }
            else
                btRemoveTrainingSet.Enabled = false;
        }

        private void tbTrainingSet_Enter(object sender, EventArgs e)
        {

        }

        private void cbTrainingSets_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (pTrainingSet p in fpTrainingSet)
                if (p.Name == cbTrainingSets.SelectedItem.ToString())
                {
                    dataGridView1.DataSource = p.fDataTable;
                  
                    int iCount = 0;
                    foreach(pPanel pp in pDisplay1.pPanels)
                    {
                        if( (pp.Neuron).NeuronType != NeuronTypes.Hidden)
                        {
                            iCount++;
                        }
                    }

                    if (iCount != p.fDataTable.Columns.Count)
                        throw new Exception("This Training Set are out of date.");

                }
        }

        private void btTrain_Click(object sender, EventArgs e)
        {
            if (Parent.ActiveDocument.pDisplay1.DisplayStatus != pDisplay.pDisplayStatus.Training)
            {
                btTrain.Text = "Stop Training";


                DataTable dt = (DataTable)dataGridView1.DataSource;

                double[][] input = new double[dt.Rows.Count][];

                double[][] output = new double[dt.Rows.Count][];

                int iPerceptionNeuronCount = pDisplay1.Net.InputNeuronCount;

                int iXPosition = 0;
                foreach (DataRow r in dt.Rows)
                {

                    int iYPosition = 0;
                    double[] dIn = new double[iPerceptionNeuronCount];
                    foreach (DataColumn c in dt.Columns)
                    {
                        if (iYPosition >= iPerceptionNeuronCount)
                            continue;
                        dIn[iYPosition++] = Convert.ToDouble(r[c], System.Globalization.CultureInfo.InvariantCulture);
                    }

                    input[iXPosition++] = dIn;

                }

                iXPosition = 0;
                foreach (DataRow r in dt.Rows)
                {

                    int iYPosition = 0;
                    double[] dOut = new double[dt.Columns.Count - iPerceptionNeuronCount];
                    foreach (DataColumn c in dt.Columns)
                    {
                        if (iYPosition < iPerceptionNeuronCount)
                        {
                            iYPosition++;
                            continue;
                        }
                        dOut[iYPosition - iPerceptionNeuronCount] = Convert.ToDouble(r[c], System.Globalization.CultureInfo.InvariantCulture);
                        iYPosition++;
                    }

                    output[iXPosition++] = dOut;

                }

                NeuralNetwork net = pDisplay1.Net;

                ThreadStart starter2 = delegate { internalTrain(ref net, input, output); };
                new Thread(starter2).Start();
            }
            else
            {
                Parent.ActiveDocument.pDisplay1.DisplayStatus = pDisplay.pDisplayStatus.Idle;

            }

            


        }

        delegate void AssincP(int aCount, double aGlobalError);
        delegate void Assinc();

        public void StartTrain()
        {
            pDisplay1.DisplayStatus = pDisplay.pDisplayStatus.Training;
            Parent.statusCycles.Visible = true;
            Parent.statusGlobalError.Visible = true;
        }

        public void StopTrain()
        {
            pDisplay1.DisplayStatus = pDisplay.pDisplayStatus.Idle;
            Parent.statusCycles.Visible = false;
            btTrain.Text = "Start Training";
            Parent.statusCycles.Tag = null;
        }

        public struct t_dates
        {
            public DateTime First;
        }

        public void RefreshCyclesSec(int aCount, double aGlobalError)
        {
            int aCycles = aCount;
            t_dates t;
            if (Parent.statusCycles.Tag == null)
            {
                t = new t_dates();
                t.First = DateTime.Now;
                Parent.statusCycles.Tag = t;
                return;
            }
            
            t = ((t_dates)Parent.statusCycles.Tag);




            TimeSpan m =  DateTime.Now.Subtract(t.First);
            int iFirst = m.Seconds;

            double vFirst = ((double)(aCycles)) / iFirst;

            Parent.statusCycles.Text = "Cycles/Sec.: "+vFirst.ToString("#0000");
            Parent.statusGlobalError.Text = "Global Error: " + aGlobalError.ToString("#0.0000000");

        }


        private void internalTrain(ref NeuralNetwork net, double[][] input, double[][] output)
        {
            this.Invoke(new Assinc(StartTrain));

            int count;

            count = 0;


            double dGlobalError = 1;
            double dTotalError = 1;


            while (dGlobalError < -.0000001 || dGlobalError > .0000001)
            {
                if(Parent.ActiveDocument.pDisplay1.DisplayStatus != pDisplay.pDisplayStatus.Training)
                {
                    this.Invoke(new Assinc(StopTrain));
                    return;
                }
                
                count++;

                net.Train(input, output, INNER_TRAINING_TIMES);

                if(count % INNER_TRAINING_TIMES == 0)
                    this.Invoke(new AssincP(RefreshCyclesSec), new object[] { count*INNER_TRAINING_TIMES, dGlobalError });

                dTotalError = 0;
                foreach (Neuron n in net.Neuron)
                {
                    dTotalError += n.Error;
                }

                dGlobalError = dTotalError / net.Neuron.Count;

            }

            this.Invoke(new Assinc(StopTrain));

            pMessage.Alert("Done with " + count.ToString() + " cycles.\nGlobal error: " + dGlobalError.ToString());
        }

        private void btImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog s = new OpenFileDialog();
            s.DefaultExt = ".xml";
            s.Filter = "pNeuron Network Training Set (*.xml)|*.xml|All files (*.*)|*.*";
            if (s.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ((DataTable)dataGridView1.DataSource).ReadXml(s.FileName);
                }
                catch
                {
                    pMessage.Error("Invalid or corrupt file.");
                }
            }
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            
                SaveFileDialog s = new SaveFileDialog();
                s.DefaultExt = ".xml";
                s.FileName = ".xml";
                s.Filter = "pNeuron Network Training Set (*.xml)|*.xml|All files (*.*)|*.*";
                if (s.ShowDialog() == DialogResult.OK)
                {
                    ((DataTable)dataGridView1.DataSource).WriteXml(s.FileName);
                }
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            pDisplay1.Net.ResetKnowledgment();
            Parent.fmPlotter.ClearData();
        }

        private void btNewTrainingSet_Click(object sender, EventArgs e)
        {
            AddTrainingSet();
        }

        private void btRemoveTrainingSet_Click(object sender, EventArgs e)
        {
            int i = cbTrainingSets.SelectedIndex;
            cbTrainingSets.Items.RemoveAt(i);
            fpTrainingSet.RemoveAt(i);
            if (cbTrainingSets.Items.Count > 0)
                if (i < cbTrainingSets.Items.Count - 1)
                    cbTrainingSets.SelectedIndex = i == 0 ? 1 : i;
                else
                    cbTrainingSets.SelectedIndex = i == 0 ? 1 : i - 1;
            else
            {
                dataGridView1.DataSource = null;
                btRemoveTrainingSet.Enabled = false;
            }



        }

        private void pDisplay1_OnNewDomain()
        {
            //fmDomainEdit f = new fmDomainEdit();
            //f.ShowDialog();
        }

        

    }
}
