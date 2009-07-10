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

        public delegate void OnDisplayStatusChangedDelegate();
        public event OnDisplayStatusChangedDelegate OnDisplayStatusChanged;

        public delegate void OnSelectedObjectChangedDelegate();
        public event OnSelectedObjectChangedDelegate OnSelectedObjectChanged;

        public delegate void OnStartTraingDelegate();
        public event OnStartTraingDelegate OnStartTraing;

        public delegate void OnStopTraingDelegate();
        public event OnStopTraingDelegate OnStopTraing;

        public delegate void OnRefreshCyclesSecDelegate(int Times);
        public event OnRefreshCyclesSecDelegate OnRefreshCyclesSec;

        public delegate void OnResetLearningDelegate();
        public event OnResetLearningDelegate OnResetLearning;

        public delegate void OnResetKnowledgementDelegate();
        public event OnResetKnowledgementDelegate OnResetKnowledgement;



        #endregion

        #region Constructors

        public pDocument(string sFileName) : this()
        {
            Filename = sFileName;
        }

        public pDocument()
        {
            InitializeComponent();

            MainDisplay.OnSelectedPanelsChange += new pDisplay.SelectedPanelsChangeDelegate(MainDisplay_OnSelectedPanelsChange);
            MainDisplay.OnDisplayStatusChange += new pDisplay.DisplayStatusChangeDelegate(MainDisplay_OnDisplayStatusChange);
            MainDisplay.OnNetworkChange += new pDisplay.NetworkChangeDelegate(MainDisplay_OnNetworkChange);
        }

        
        #endregion

        #region Net Events

        private void Net_OnRefreshCyclesSec(int Times)
        {
            if (OnRefreshCyclesSec != null)
                OnRefreshCyclesSec(Times);
        }

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

        private void MainDisplay_OnNetworkChange()
        {
            this.Modificated = true;
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
                        break;
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

       

      

        #endregion

        #region Save/Load & Add Training Set

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
            MainDisplay.Net.ToXml(Path.GetDirectoryName(Filename) + "\\_" + Path.GetFileName(Filename));
        }

        public DialogResult LoadFile()
        {
            if (Modificated)
                Save();

            OpenFileDialog s = new OpenFileDialog();
            s.DefaultExt = ".pne";
            s.Filter = "pNeuron Network (*.pne)|*.pne|All files (*.*)|*.*";
            if (s.ShowDialog() == DialogResult.OK)
            {
                Filename = s.FileName;

                MainDisplay.SetNeuralNetwork(NeuralNetwork.ToObject(s.FileName));
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


        #endregion

        #region Menu

        public void StartTrain()
        {
            if (MainDisplay.DisplayStatus != pDisplay.pDisplayStatus.Training)
            {

                DataTable dt = null;

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



        #endregion


       

    }

}
