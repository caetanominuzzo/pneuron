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
using primeira.pRandom;
using primeira.pTypes;

namespace primeira.pNeuron
{
    public partial class pDocument : DockContent, IpDocks
    {

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

        public delegate void OnNetworkChangeDelegate(pChangeEscope escope);
        public event OnNetworkChangeDelegate OnNetworkChange;

        public delegate void OnDisplayStatusChangedDelegate();
        public event OnDisplayStatusChangedDelegate OnDisplayStatusChanged;

        public delegate void OnSelectedObjectChangedDelegate();
        public event OnSelectedObjectChangedDelegate OnSelectedObjectChanged;

        public delegate void OnStartTraingDelegate();
        public event OnStartTraingDelegate OnStartTraing;

        public delegate void OnStopTraingDelegate();
        public event OnStopTraingDelegate OnStopTraing;

        public delegate void OnResetLearningDelegate();
        public event OnResetLearningDelegate OnResetLearning;

        public delegate void OnResetKnowledgementDelegate();
        public event OnResetKnowledgementDelegate OnResetKnowledgement;





        #endregion

        #region Constructors

        public pDocument(pTrueRandomGenerator cache, string sFileName) : this(cache)
        {
            Filename = sFileName;
        }

        public pDocument(pTrueRandomGenerator cache)
        {
            InitializeComponent();
            MainDisplay.Net.SetRandomGenerator(cache);
            MainDisplay.Net.OnStartTraing += new NeuralNetwork.OnStartTraingDelegate(Net_OnStartTraing);
            MainDisplay.Net.OnStopTraing += new NeuralNetwork.OnStopTraingDelegate(Net_OnStopTraing);
            MainDisplay.Net.OnResetLearning += new NeuralNetwork.OnResetLearningDelegate(Net_OnResetLearning);
            MainDisplay.Net.OnResetKnowledgement += new NeuralNetwork.OnResetKnowledgementDelegate(Net_OnResetKnowledgement);

            
        }

        public pDocument()
        {

            InitializeComponent();

           
        }

    

  

        
        #endregion

        #region Net Events

        private void Net_OnStopTraing()
        {
            MainDisplay.DisplayStatus = pDisplay.pDisplayStatus.Idle;

            if (OnStopTraing != null)
                OnStopTraing();

        }

        private void Net_OnStartTraing()
        {
            MainDisplay.DisplayStatus = pDisplay.pDisplayStatus.Training;
            if (OnStartTraing != null)
                OnStartTraing();
        }

        private void Net_OnResetLearning()
        {
            if (OnResetLearning != null)
                OnResetLearning();
        }

        private void Net_OnResetKnowledgement()
        {
            if (OnResetKnowledgement != null)
                OnResetKnowledgement();
        }


        #endregion

        #region MainDisplay Events

        private void MainDisplay_OnDisplayStatusChange()
        {
            if (OnDisplayStatusChanged != null)
                OnDisplayStatusChanged();
        }

        private void MainDisplay_OnSelectedPanelsChange()
        {
            if (OnSelectedObjectChanged != null)
                OnSelectedObjectChanged();

        }

        private void MainDisplay_OnNetworkChange(pChangeEscope escope)
        {
            this.Modificated = true;

            if (OnNetworkChange != null)
                OnNetworkChange(escope);

        }

        #endregion

        #region Key Up/Down Events

        private void pDocument_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
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

        private void pDocument_KeyDown(object sender, KeyEventArgs e)
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

        #endregion

        #region pDocument Events

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (MainDisplay.DisplayStatus == pDisplay.pDisplayStatus.Training)
            {
                pMessage.Error("Invalid operation on training status.");
                e.Cancel = true;
                return;
            }
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
                        return;
                        
                    case DialogResult.Yes:
                        Save();
                        break;

                }
            }

           

            Parent.PreRemoveDocument(this);


        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.Focus();
            base.OnMouseEnter(e);
        }

        private void tcDesigner_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (Modificated)
            {
                DialogResult r = pMessage.Confirm("You must save the network design before add or edit a training set.\nDo you want to save now?", MessageBoxButtons.YesNoCancel);
                if (r == DialogResult.Yes)
                {
                    if (Save() != DialogResult.OK)
                    {
                        e.Cancel = true;
                        return;
                    }
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

        private void cbTrainingSets_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (pTrainingSet p in fpTrainingSet)
                if (p.Name == cbTrainingSets.SelectedItem.ToString())
                {
                    dgTrainingSet.DataSource = p.fDataTable;



                    if (MainDisplay.Net.InputNeuronCount + MainDisplay.Net.OutputNeuronCount != p.fDataTable.Columns.Count)
                        pMessage.Error("This Training Set are out of date.");

                }
        }

        #endregion

        #region Save/Load & Add Training Set

        public void AddTrainingSet(string InitialName)
        {
            using (fmTrainingSetName f = new fmTrainingSetName(InitialName))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    foreach (object o in cbTrainingSets.Items)
                    {
                        if (o.ToString() == f.txtName.Text)
                        {
                            pMessage.Error("Invalid duplicated name.");
                            AddTrainingSet(f.txtName.Text);
                            return;
                        }
                    }

                    fpTrainingSet.Add(new pTrainingSet(MainDisplay.pPanels, f.txtName.Text, this.Filename));
                    pTrainingSet fm = fpTrainingSet[fpTrainingSet.Count - 1];
                    dgTrainingSet.DataSource = fm.NewDataTable();

                    if (tcDesigner.SelectedTab == tbDesigner)
                    {
                        tcDesigner.SelectedTab = tbTrainingSet;
                    }

                    cbTrainingSets.Items.Add(fm.Name);
                    cbTrainingSets.SelectedIndex = cbTrainingSets.Items.Count - 1;

                    btRemoveTrainingSet.Enabled = true;
                }

            }


            //DEPRECATEDParent.fmNetworkExplorer.AddNode(fm.Name, ((pDocument)Parent.ActiveDocument).Filename);
        }

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

                if (OnNetworkChange != null)
                    OnNetworkChange(pChangeEscope.ZoomDisplayCache); 

                return DialogResult.OK;
            }
            else
            {
                return DialogResult.Cancel;
            } 
        }

        private void internalLoad(string aFilename)
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

                p.Neuron.NeuralNetwork.AdjustGeneratorID(p.Name);
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

        cbTrainingSets.Items.Clear();
            foreach(DataTable dt in ds.Tables)
            {
                if (dt.TableName == "pSynapse" || dt.TableName == "pNeuron")
                    continue;

                fpTrainingSet.Add(new pTrainingSet(this.MainDisplay.pPanels, dt.TableName, this.Filename));
               
                cbTrainingSets.Items.Add(dt.TableName);

                fpTrainingSet[fpTrainingSet.Count - 1].LoadDataTable();
            }




        }

        #endregion

        #region Menu

        public void StartTrain()
        {
            if (MainDisplay.DisplayStatus != pDisplay.pDisplayStatus.Training)
            {

                if (dgTrainingSet.DataSource == null)
                {
                    if (cbTrainingSets.Items.Count == 0)
                        throw new Exception("Please add a new Training Set to train.");
                    else
                        cbTrainingSets.SelectedIndex = 0;
                }

                

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
                        dOut[iYPosition - iPerceptionNeuronCount] =Convert.ToDouble(r[c], System.Globalization.CultureInfo.InvariantCulture);
                        iYPosition++;
                    }

                    output[iXPosition++] = dOut;

                }

                if (input[0].Length != MainDisplay.Net.InputNeuronCount)
                {
                    pMessage.Error("The number of input must be equal to number of perception neurons.");
                    return;
                }

                if (output[0].Length != MainDisplay.Net.OutputNeuronCount)
                {
                     pMessage.Error("The number of output must be equal to number of output neurons.");
                    return;
                }

                ThreadStart starter2 = delegate { MainDisplay.Net.Train(input, output); };
                new Thread(starter2).Start();

            }
            else
            {
                MainDisplay.Net.StopOnNextCycle();

            }

            


        }

        public void ResetLearning()
        {
            MainDisplay.Net.ResetKnowledgement();
            MainDisplay.Net.ResetLearning();
        }

        public void ResetMemory()
        {
            MainDisplay.Net.ResetMemory();
        }

        public void Pulse(double[] input)
        {
            MainDisplay.Net.SetInputData(input);
            MainDisplay.Net.Pulse();
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

        private void btNewTrainingSet_Click(object sender, EventArgs e)
        {
            AddTrainingSet("New Training Set");
        }

        private void btRemoveTrainingSet_Click(object sender, EventArgs e)
        {
            int i = cbTrainingSets.SelectedIndex;
            cbTrainingSets.Items.RemoveAt(i);
            fpTrainingSet.RemoveAt(i);
            if (cbTrainingSets.Items.Count > 0)
                cbTrainingSets.SelectedIndex = 0;
            else
            {
                dgTrainingSet.DataSource = null;
                btRemoveTrainingSet.Enabled = false;
            }
        }

        private void  tspAutoRefresh_Click(object sender, EventArgs e)
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


        private void tspSQL_Click(object sender, EventArgs e)
        {
            fmImportFromSQL f = new fmImportFromSQL(this.dgTrainingSet);
            f.ShowDialog();
        }


        #endregion

     

    }

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
                if ((p.Neuron).NeuronType == NeuronTypes.Input)
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
}
