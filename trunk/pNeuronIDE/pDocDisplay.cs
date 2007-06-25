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
    public class pDocDisplay : pDoc, IpDoc
    {
        
        public pDisplay pDisplay1;

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
            primeira.pNeuron.Core.NeuralNet neuralNet22 = new primeira.pNeuron.Core.NeuralNet();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pDocDisplay));
            this.pDisplay1 = new primeira.pNeuron.pDisplay();
            this.SuspendLayout();
            // 
            // pDisplay
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
            this.pDisplay1.Location = new System.Drawing.Point(0, 0);
            this.pDisplay1.Name = "pDisplay";
            neuralNet22.LearningRate = 0.5;
            this.pDisplay1.Net = neuralNet22;
            this.pDisplay1.ShiftKey = false;
            this.pDisplay1.Size = new System.Drawing.Size(354, 155);
            this.pDisplay1.TabIndex = 0;
            this.pDisplay1.VisibleAutoScrollHorizontal = true;
            this.pDisplay1.VisibleAutoScrollVertical = true;
            this.pDisplay1.OnDisplayStatusChange += new primeira.pNeuron.pDisplay.DisplayStatusChangeDelegate(this.pDisplay1_OnDisplayStatusChange);
            this.pDisplay1.OnSelectedPanelsChange += new primeira.pNeuron.pDisplay.SelectedPanelsChangeDelegate(this.pDisplay1_OnSelectedPanelsChange);
            this.pDisplay1.OnNetworkChange += new primeira.pNeuron.pDisplay.NetworkChangeDelegate(this.pDisplay1_OnNetworkChange);
            this.pDisplay1.OnTreeViewChange += new primeira.pNeuron.pDisplay.TreeViewChangeDelegate(this.pDisplay1_OnTreeViewChange);
            // 
            // pDocument
            // 
            this.ClientSize = new System.Drawing.Size(354, 155);
            this.Controls.Add(this.pDisplay1);
            this.KeyPreview = true;
            this.Name = "pDocument";
            this.TabText = "[NeuralNetwork1]";
            this.Text = "[NeuralNetwork1]";
            this.Activated += new System.EventHandler(this.pDocument_Activated);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.pDocument_KeyUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pDocument_KeyDown);
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

            if (DefaultNamedFile)
            {
                Parent.fmDocuments.Remove(this);
                Parent.fmNetworkExplorer.RemoveNode(this.Filename);
            }
            Parent.fmDocuments.Remove(this);

        }

        private void pDisplay1_OnNetworkChange()
        {
            this.Modificated = true;
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
                s.InitialDirectory = Path.GetDirectoryName(Parent.ProjectFilename);
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
                internalLoad(s.FileName);
                Filename = s.FileName;
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



        }

        #endregion


        protected override void OnMouseEnter(EventArgs e)
        {
            this.Focus();
            base.OnMouseEnter(e);
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            string sDataFile = Path.GetFileNameWithoutExtension(this.Filename)+".dat";

            if (File.Exists(sDataFile))
            {
                DataTable dt = new DataTable();
                dt.ReadXml(sDataFile);
               // dataGridView1.DataSource = dt;
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("N1");
                dt.Columns.Add("N2");
                dt.Columns.Add("R1");
            }
        }

    }
}
