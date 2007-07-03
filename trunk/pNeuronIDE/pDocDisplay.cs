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
    public class pDocDisplay : pDoc
    {
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
                    if( ((Neuron)p.Tag).NeuronType == NeuronTypes.Perception)
                    {
                        fDataTable.Columns.Add(p.Text, typeof(double));
                    }
                }

                foreach (pPanel p in fpPanel)
                {
                    if (((Neuron)p.Tag).NeuronType == NeuronTypes.Output)
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
        private ToolStrip toolStrip1;
        private ToolStripComboBox cbTrainingSets;
        private ToolStripButton btTrain;
        private List<pTrainingSet> fpTrainingSet = new List<pTrainingSet>();

        public pDocDisplay(string sFileName) : this()
        {
            Filename = sFileName;
        }

        public pDocDisplay()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }

        private void InitializeComponent()
        {
            primeira.pNeuron.Core.NeuralNet neuralNet6 = new primeira.pNeuron.Core.NeuralNet();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pDocDisplay));
            this.pDisplay1 = new primeira.pNeuron.pDisplay();
            this.tcDesigner = new System.Windows.Forms.TabControl();
            this.tbDesigner = new System.Windows.Forms.TabPage();
            this.tbTrainingSet = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cbTrainingSets = new System.Windows.Forms.ToolStripComboBox();
            this.btTrain = new System.Windows.Forms.ToolStripButton();
            this.tcDesigner.SuspendLayout();
            this.tbDesigner.SuspendLayout();
            this.tbTrainingSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.pDisplay1.CtrlKey = false;
            this.pDisplay1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pDisplay1.DisplayStatus = primeira.pNeuron.pDisplay.pDisplayStatus.Idle;
            this.pDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pDisplay1.EnableAutoScrollHorizontal = true;
            this.pDisplay1.EnableAutoScrollVertical = true;
            this.pDisplay1.Location = new System.Drawing.Point(3, 3);
            this.pDisplay1.Name = "pDisplay1";
            neuralNet6.LearningRate = 0.5;
            this.pDisplay1.Net = neuralNet6;
            neuralNet6.Neuron = new List<Neuron>();
            this.pDisplay1.ShiftKey = false;
            this.pDisplay1.Size = new System.Drawing.Size(667, 390);
            this.pDisplay1.TabIndex = 0;
            this.pDisplay1.VisibleAutoScrollHorizontal = true;
            this.pDisplay1.VisibleAutoScrollVertical = true;
            this.pDisplay1.OnDisplayStatusChange += new primeira.pNeuron.pDisplay.DisplayStatusChangeDelegate(this.pDisplay1_OnDisplayStatusChange);
            this.pDisplay1.OnSelectedPanelsChange += new primeira.pNeuron.pDisplay.SelectedPanelsChangeDelegate(this.pDisplay1_OnSelectedPanelsChange);
            this.pDisplay1.OnNetworkChange += new primeira.pNeuron.pDisplay.NetworkChangeDelegate(this.pDisplay1_OnNetworkChange);
            this.pDisplay1.OnTreeViewChange += new primeira.pNeuron.pDisplay.TreeViewChangeDelegate(this.pDisplay1_OnTreeViewChange);
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
            this.tcDesigner.Size = new System.Drawing.Size(681, 422);
            this.tcDesigner.TabIndex = 1;
            this.tcDesigner.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tcDesigner_Selecting);
            // 
            // tbDesigner
            // 
            this.tbDesigner.Controls.Add(this.pDisplay1);
            this.tbDesigner.Location = new System.Drawing.Point(4, 4);
            this.tbDesigner.Name = "tbDesigner";
            this.tbDesigner.Padding = new System.Windows.Forms.Padding(3);
            this.tbDesigner.Size = new System.Drawing.Size(673, 396);
            this.tbDesigner.TabIndex = 0;
            this.tbDesigner.Text = "Network Designer";
            this.tbDesigner.UseVisualStyleBackColor = true;
            // 
            // tbTrainingSet
            // 
            this.tbTrainingSet.Controls.Add(this.dataGridView1);
            this.tbTrainingSet.Controls.Add(this.toolStrip1);
            this.tbTrainingSet.Location = new System.Drawing.Point(4, 4);
            this.tbTrainingSet.Margin = new System.Windows.Forms.Padding(0);
            this.tbTrainingSet.Name = "tbTrainingSet";
            this.tbTrainingSet.Padding = new System.Windows.Forms.Padding(3);
            this.tbTrainingSet.Size = new System.Drawing.Size(673, 396);
            this.tbTrainingSet.TabIndex = 1;
            this.tbTrainingSet.Text = "Training Sets";
            this.tbTrainingSet.UseVisualStyleBackColor = true;
            this.tbTrainingSet.Enter += new System.EventHandler(this.tbTrainingSet_Enter);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(667, 390);
            this.dataGridView1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbTrainingSets,
            this.btTrain});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(667, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
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
            this.btTrain.Size = new System.Drawing.Size(51, 22);
            this.btTrain.Text = "Train";
            this.btTrain.Click += new System.EventHandler(this.btTrain_Click);
            // 
            // pDocDisplay
            // 
            this.ClientSize = new System.Drawing.Size(681, 422);
            this.Controls.Add(this.tcDesigner);
            this.KeyPreview = true;
            this.Name = "pDocDisplay";
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
                Parent.fmGroupExplorer.treeView1.Focus();
            }
            return;
            /*
            int i = 0;
            int j = 0;

            //            ((pNeuonIDE)DockPanel.Parent).treeview.treeView1.Items.Clear();

            List<int> lToDelete = new List<int>();

            foreach (List<pPanel> l in pDisplay1.Groups())
            {

                lToDelete.Clear();

                foreach (ListViewItem lv in Parent.treeview.treeView1.Groups[i].Items)
                {

                    if(!pDisplay1.pPanels.Contains( ((pPanel)lv.Tag)))
                    {
                        lToDelete.Add(lv.Index);
                    } else

                    if (!((pPanel)lv.Tag).Groups.Contains(i) && (i != 0))
                    {
                        lToDelete.Add(lv.Index);
                    }
                }

                lToDelete.Sort();

                int y = 0;
                foreach (int lv in lToDelete)
                {
                    Parent.treeview.treeView1.Groups[i].Items.RemoveAt(lv - y);
                    y++;
                }

                foreach (pPanel p in l)
                {
                    if (
                        !Parent.treeview.Contains(
                            Parent.treeview.treeView1.Groups[i],
                            p))
                    {
                        ListViewItem lvi = new ListViewItem(p.Name, (Parent.treeview.treeView1.Groups[i]));

                        Parent.treeview.treeView1.Items.Add(lvi);

                        int k = Parent.treeview.treeView1.Groups[i].Items.Count - 1;

                        Parent.treeview.treeView1.Groups[i].Items[k].Tag = p;
                    }

                    j++;
                }

                i++;
            }
             * */
        }

        private void pDocument_Activated(object sender, EventArgs e)
        {
            Parent.ActiveDocument = this;

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


        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (Modificated)
            {
                switch (MessageBox.Show("Save changes on " + this.Filename + "?", "Don't you like to save your work?", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.No:
                        if (DefaultNamedFile)
                        {
                            Parent.fmDocuments.Remove(this);
                            foreach (TreeNode n in Parent.fmNetworkExplorer.treeView1.Nodes[this.Filename].Nodes)
                            {
                                pDoc p = Parent.GetDocByName(n.Name);
                                p.QueryOnClose = false;
                                p.Close();
                            }

                            Parent.fmNetworkExplorer.RemoveNode(this.Filename);

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

            

                Parent.fmDocuments.Remove(this);
                Parent.fmNetworkExplorer.RemoveNode(this.Filename);
  

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
                            ((Neuron)p.Tag).Bias.Weight,
                            ((Neuron)p.Tag).Value,
                            ((Neuron)p.Tag).NeuronType
                        }
                    );
               


                foreach (pPanel pp in pDisplay1.pPanels)
                {
                    if (((INeuron)pp.Tag).Input.ContainsKey((INeuron)p.Tag))
                    {
                        foreach (INeuron nn in ((INeuron)pp.Tag).Input.Keys)
                        {

                            if (nn == ((INeuron)p.Tag))
                            {
                                
                                    tSynapse.Rows.Add(new object[]{
                                                        p.Name,
                                                        pp.Name,
                                                        ((INeuron)pp.Tag).Input[nn].Weight });
       
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
            s.DefaultExt = ".pnu";
            s.Filter = "Untrained pNeuron Network (*.upn)|*.pnu|Trained pNeuron Network (*.pne)|*.pne|All files (*.*)|*.*";
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
                pPanel p = pDisplay1.Add(new Neuron(0));
                p.Name = r["Name"].ToString();
                p.Location = new Point( Convert.ToInt32(r["LocationX"]), Convert.ToInt32(r["LocationY"]) );
                pDisplay1.Add(p, Convert.ToInt32(r["Group"]));

                ((Neuron)p.Tag).Bias.Weight = Convert.ToDouble(r["Bias"]);
                ((Neuron)p.Tag).Value = Convert.ToDouble(r["Value"]);
                ((Neuron)p.Tag).NeuronType = (NeuronTypes)Convert.ToInt16(r["NeuronType"]);

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
                                p.Output.Add((Neuron)pp.Tag);
                                pp.Input.Add((Neuron)p.Tag, new NeuralFactor( Convert.ToDouble(r["Value"]) ));
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
                if (MessageBox.Show("You must save the network design before add or edit a training set?", "Network Design", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    Save();
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }
            if (fpTrainingSet.Count == 0)
            {
                if (MessageBox.Show("Add new training set?", "Training Set", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    AddTrainingSet();
                }
                else e.Cancel = true;
            }
            else if(fpTrainingSet.Count == 1)
            {
                cbTrainingSets.SelectedIndex = 0;
            }
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
                  
                    //TODO:IF COLS CHANGED
                    int iCount = 0;
                    foreach(pPanel pp in pDisplay1.pPanels)
                    {
                        if( ((Neuron)pp.Tag).NeuronType != NeuronTypes.Hidden)
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
            DataTable dt = (DataTable)dataGridView1.DataSource;

            double[][] input = new double[dt.Rows.Count][];

            double[][] output = new double[dt.Rows.Count][];

            int iPerceptionNeuronCount = 0;
            foreach(pPanel p in pDisplay1.pPanels)
            {
                if( ((Neuron)p.Tag).NeuronType == NeuronTypes.Perception)
                    iPerceptionNeuronCount ++;
            }


            int iXPosition = 0;
            foreach(DataRow r in dt.Rows)
            {

                int iYPosition = 0;
                double[] dIn = new double[iPerceptionNeuronCount];
                foreach(DataColumn c in dt.Columns)
                {
                    if (iYPosition >= iPerceptionNeuronCount)
                        continue;
                    dIn[iYPosition++] = Convert.ToDouble(r[c]);
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
                    dOut[iPerceptionNeuronCount - iYPosition++] = Convert.ToDouble(r[c]);
                }

                output[iXPosition++] = dOut;

            }

            NeuralNet net = pDisplay1.Net;

            int count;

            count = 0;
            net.LearningRate = .5;

            net.InitializeLearning();

            double dGlobalError = 0;
            double dTotalError = 0;

            do
            {

                count++;


                net.Train(input, output, TrainingType.BackPropogation, 1);

                dTotalError = 0;
                foreach (Neuron n in net.Neuron)
                {
                    dTotalError += n.Error;
                }

                dGlobalError = dTotalError / net.Neuron.Count;
            }
            while (dGlobalError < -.0000001 || dGlobalError > .0000001);
            MessageBox.Show("Done. \nGlobal error: " + dGlobalError.ToString());

        }


    }
}
