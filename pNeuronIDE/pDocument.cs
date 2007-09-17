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
    public partial class pDocument : DockContent, IpDocks
    {

        private const int INNER_TRAINING_TIMES = 100;

        #region IpDocks Members

        private pNeuronIDE fParent;

        private bool fQueryOnClose = true;

        public bool QueryOnClose
        {
            get { return fQueryOnClose; }
            set { fQueryOnClose = value; }
        }
        public new pNeuronIDE Parent
        {
            get { return DockPanel == null ? fParent : ((pNeuronIDE)DockPanel.Parent); }
            set { fParent = value; }
        }

        #endregion

        #region Fields

        private bool m_modificated = false;

        private string m_filename;

        private bool m_defaultNamedFile = true;

        #endregion

        #region Properties

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

        #region Events

        public delegate void OnDisplayStatusChangedDelegate();
        public event OnDisplayStatusChangedDelegate OnDisplayStatusChanged;

        public delegate void OnSelectedObjectChangedDelegate();
        public event OnSelectedObjectChangedDelegate OnSelectedObjectChanged;

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

        #region Constructors

        public pDocument(string sFileName) : this()
        {
            Filename = sFileName;
        }

        public pDocument()
        {
            InitializeComponent();
        }

        #endregion

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }

        private void pDisplay1_OnDisplayStatusChange()
        {
            if (OnDisplayStatusChanged != null)
                OnDisplayStatusChanged();
        }

        public void pDocument_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    MainDisplay.DisplayStatus = pDisplay.pDisplayStatus.Remove_Neuron;
                    break;
                case Keys.B: MainDisplay.Bezier = !MainDisplay.Bezier;
                    Invalidate();
                    break;
                case Keys.K: //Log ShiftB
                    MainDisplay.Logger.Visible = !MainDisplay.Logger.Visible;
                    break;
                case Keys.Escape:
                    MainDisplay.UnSelect();
                    MainDisplay.DisplayStatus = pDisplay.pDisplayStatus.Idle;
                    break;
                case Keys.L: //Link Mode
                    MainDisplay.DisplayStatus = pDisplay.pDisplayStatus.Linking_Paused;
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
                        if (MainDisplay.SelectedpPanels.Length == 0)
                        {
                            MainDisplay.GroupFree(iKey);
                        }

                        if (!e.Shift)
                            MainDisplay.GroupFree(iKey);

                        foreach (pPanel p in MainDisplay.SelectedpPanels)
                        {
                            MainDisplay.Add(p, iKey);
                        }

                    }
                    else
                    {

                        if (!e.Shift)
                            MainDisplay.UnSelect();


                        MainDisplay.GroupSelect(iKey);
                    }

                    break;
            }

            if (!e.Shift)
                MainDisplay.ShiftKey = false;

            if (!e.Control)
                MainDisplay.CtrlKey = false;
        }

        public void pDocument_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                MainDisplay.ShiftKey = true;
            }

            if (e.KeyCode == Keys.ControlKey)
            {
                MainDisplay.CtrlKey = true;
            }
        }

        private void pDisplay1_OnSelectedPanelsChange()
        {
            if (OnSelectedObjectChanged != null)
                OnSelectedObjectChanged();

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
            fpTrainingSet.Add(new pTrainingSet(MainDisplay.pPanels, "Training Set " + i.ToString(), this.Filename));
            pTrainingSet fm = fpTrainingSet[fpTrainingSet.Count - 1];
            dgTrainingSet.DataSource = fm.NewDataTable();

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

            foreach (pPanel p in MainDisplay.pPanels)
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
               


                foreach (pPanel pp in MainDisplay.pPanels)
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

        public new DialogResult Load()
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
                MainDisplay.Invalidate();
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
                pPanel p = MainDisplay.Add();
                p.Neuron.Bias.Weight = Convert.ToDouble(r["Bias"], System.Globalization.CultureInfo.InvariantCulture);
                p.Name = r["Name"].ToString();
                p.Location = new Point( Convert.ToInt32(r["LocationX"]), Convert.ToInt32(r["LocationY"]) );
                MainDisplay.Add(p, Convert.ToInt32(r["Group"]));

                (p.Neuron).Value = Convert.ToDouble(r["Value"], System.Globalization.CultureInfo.InvariantCulture);
                (p.Neuron).NeuronType = (NeuronTypes)Convert.ToInt16(r["NeuronType"]);

            }

            if(ds.Tables["pSynapse"]!=null)
            foreach (DataRow r in ds.Tables["pSynapse"].Rows)
            {
                foreach (pPanel p in MainDisplay.pPanels)
                {
                    if (r["NeuronOut"].ToString() == p.Name)
                    {
                        foreach (pPanel pp in MainDisplay.pPanels)
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

                fpTrainingSet.Add(new pTrainingSet(this.MainDisplay.pPanels, dt.TableName, this.Filename));
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
                    dgTrainingSet.DataSource = p.fDataTable;
                  


                    if (MainDisplay.Net.InputNeuronCount + MainDisplay.Net.OutputNeuronCount != p.fDataTable.Columns.Count)
                        throw new Exception("This Training Set are out of date.");

                }
        }

        public void StartTrain()
        {
            if (Parent.ActiveDocument.MainDisplay.DisplayStatus != pDisplay.pDisplayStatus.Training)
            {
                Parent.tspStartTrain.Text = "Stop Training";

                if (dgTrainingSet.DataSource == null)
                    cbTrainingSets.SelectedIndex = 0;

                DataTable dt = (DataTable)dgTrainingSet.DataSource;

                double[][] input = new double[dt.Rows.Count][];

                double[][] output = new double[dt.Rows.Count][];

                int iPerceptionNeuronCount = MainDisplay.Net.InputNeuronCount;

                int iXPosition = 0;
                foreach (DataRow r in dt.Rows)
                {

                    int iYPosition = 0;
                    double[] dIn = new double[iPerceptionNeuronCount];
                    foreach (DataColumn c in dt.Columns)
                    {
                        if (iYPosition >= iPerceptionNeuronCount)
                            continue;
                        dIn[iYPosition++] = Util.Sigmoid(Convert.ToDouble(r[c], System.Globalization.CultureInfo.InvariantCulture));
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
                        dOut[iYPosition - iPerceptionNeuronCount] = Util.Sigmoid(Convert.ToDouble(r[c], System.Globalization.CultureInfo.InvariantCulture));
                        iYPosition++;
                    }

                    output[iXPosition++] = dOut;

                }

                net = MainDisplay.Net;
                //net.ResetKnowledgment();

             //   net.OnNeuronPulse += new NeuralNetwork.OnNeuronPulseDelegate(net_OnNeuronPulse);
             //   net.OnNeuronPulseBack += new NeuralNetwork.OnNeuronPulseBackDelegate(net_OnNeuronPulseBack);

                ThreadStart starter2 = delegate { internalTrain(ref net, input, output); };
                new Thread(starter2).Start();
            }
            else
            {
                Parent.ActiveDocument.MainDisplay.DisplayStatus = pDisplay.pDisplayStatus.Idle;

            }

            


        }

        delegate void AssincP(int aCount);
        delegate void Assinc();

        private void internalStartTrain()
        {
            MainDisplay.DisplayStatus = pDisplay.pDisplayStatus.Training;
            Parent.statusCycles.Visible = true;
        }

        

        public void StopTrain()
        {
            MainDisplay.DisplayStatus = pDisplay.pDisplayStatus.Idle;
            Parent.statusCycles.Visible = false;
            Parent.tspStartTrain.Text = "Start Training";
            Parent.statusCycles.Tag = null;
        }

        public struct t_dates
        {
            public DateTime First;
        }

        public void RefreshCyclesSec(int aCount)
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

        }


        private void internalTrain(ref NeuralNetwork net, double[][] input, double[][] output)
        {
            this.Invoke(new Assinc(internalStartTrain));

            int count;

            count = 0;


            double dGlobalError = 1;
            double dTotalError = 1;


            while (dGlobalError > .000000000000001)
            {
                if(Parent.ActiveDocument.MainDisplay.DisplayStatus != pDisplay.pDisplayStatus.Training)
                {
                    this.Invoke(new AssincP(RefreshCyclesSec), new object[] { count * INNER_TRAINING_TIMES });
                    
                    this.Invoke(new Assinc(StopTrain));
                    return;
                }
                
                count++;

                net.TrainSession(input, output, INNER_TRAINING_TIMES);

                if(count % INNER_TRAINING_TIMES == 0)
                    this.Invoke(new AssincP(RefreshCyclesSec), new object[] { count*INNER_TRAINING_TIMES });

                dTotalError = 0;
                foreach (Neuron n in net.Neuron)
                {
                    dTotalError += n.Error;
                }

                dGlobalError = net.GlobalError;

            }

            this.Invoke(new Assinc(StopTrain));

           // pMessage.Alert("Done with " + count.ToString() + " cycles.\nGlobal error: " + dGlobalError.ToString());
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
                    ((DataTable)dgTrainingSet.DataSource).ReadXml(s.FileName);
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
                    ((DataTable)dgTrainingSet.DataSource).WriteXml(s.FileName);
                }
        }

        public void ResetLearning()
        {
            MainDisplay.Net.ResetKnowledgment();
            MainDisplay.Net.ResetLearning();
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
                dgTrainingSet.DataSource = null;
                btRemoveTrainingSet.Enabled = false;
            }
        }

        private void tspAutoRefresh_Click(object sender, EventArgs e)
        {
            if (tspAutoRefresh.Text == "Start Refresh")
            {
                tspAutoRefresh.Text = "Stop Refresh";
                refreshTimer.Start();
            }
            else
            {
                tspAutoRefresh.Text = "Start Refresh";
                refreshTimer.Stop();
            }
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            MainDisplay.Refresh();
        }

    }
}