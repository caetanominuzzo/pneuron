using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using primeira.pNeuron.Core;
using System.IO;
using System.Threading;
using primeira.pRandom;
using Timer = System.Windows.Forms.Timer;

namespace primeira.pNeuron
{
    public partial class pDocument : UserControl, IDisposable, ITabbedControl
    {

        #region Fields

        private string m_filename;

        private Timer _saveTimer;

        #endregion

        #region Properties

        public string Filename
        {
            get { return m_filename; }
            set
            {
                m_filename = value;
            }
        }

        #endregion

        #region Constructors

        public pDocument(string sFileName)
            : this()
        {
            Filename = sFileName;

            MainDisplay.SetNeuralNetwork(NeuralNetwork.ToObject(sFileName));

        }

        public pDocument()
        {

            InitializeComponent();

            MainDisplay.OnSelectedPanelsChange += new pDisplay.SelectedPanelsChangeDelegate(MainDisplay_OnSelectedPanelsChange);
            MainDisplay.OnDisplayStatusChange += new pDisplay.DisplayStatusChangeDelegate(MainDisplay_OnDisplayStatusChange);
            MainDisplay.OnNetworkChange += new pDisplay.NetworkChangeDelegate(MainDisplay_OnNetworkChange);

            Dock = DockStyle.Fill;
            BorderStyle = BorderStyle.None;

            _saveTimer = new Timer();
            _saveTimer.Interval = 5000;
            _saveTimer.Tick += new EventHandler(_saveTimer_Tick);

            TopologyPropertyGrid pp = new TopologyPropertyGrid();
            pp.SetDocument(this);
            pp.Dock = DockStyle.Fill;
            panel6.Controls.Add(pp);

            TopologyToolbox p = new TopologyToolbox();
            p.SetDocument(this);
            p.Dock = DockStyle.Fill;
            panel4.Controls.Add(p);


                 

        }

        public void PrepareToClose()
        {
            internalSave();
        }

        void _saveTimer_Tick(object sender, EventArgs e)
        {
            internalSave();
            _saveTimer.Stop();
            _saveTimer.Start();
        }


        #endregion

        #region MainDisplay Events

        private void MainDisplay_OnDisplayStatusChange()
        {
            lblStatus.Text = MainDisplay.DisplayStatus.ToString();
        }

        private void MainDisplay_OnSelectedPanelsChange()
        {

        }

        private void MainDisplay_OnNetworkChange()
        {
            _saveTimer.Stop();
            _saveTimer.Start();

        }

        #endregion

        #region pDocument Events


        protected override void OnMouseEnter(EventArgs e)
        {
            this.Focus();
            base.OnMouseEnter(e);
        }

        #endregion

        #region Save/Load & Add Training Set

        private void internalSave()
        {
            MainDisplay.Net.ToXml(Path.GetDirectoryName(Filename) + "\\" + Path.GetFileName(Filename));
        }

        public DialogResult LoadFile(bool NewFile)
        {
            OpenFileDialog s = new OpenFileDialog();
            s.CheckFileExists = false;
            s.DefaultExt = ".pne";

            if (NewFile)
            {
                string filename = string.Empty;

                for (int i = 1; i < 100; i++)
                {
                    if (!File.Exists(string.Format("NeuralNetwork{0}.pne", i)))
                    {
                        filename = string.Format("NeuralNetwork{0}.pne", i);
                        break;
                    }
                }

                s.FileName = filename;
            }


            //if (NewFile)
            //{
            //    MainDisplay.SetNeuralNetwork(NeuralNetwork.ToObject(@"c:\_5.pne"));
            //    Filename = @"c:\_5.pne";
            //}
            //else Filename = s.FileName;

            //return DialogResult.OK;

            s.Filter = "pNeuron Network (*.pne)|*.pne|All files (*.*)|*.*";
            if (s.ShowDialog() == DialogResult.OK)
            {
                Filename = s.FileName;

                if (File.Exists(s.FileName))
                {
                    MainDisplay.SetNeuralNetwork(NeuralNetwork.ToObject(s.FileName));


                    FileBrowserRecentFileManager.Add(s.FileName);

                }

                return DialogResult.OK;
            }
            else
            {
                return DialogResult.Cancel;
            }
        }


        #endregion

        #region IpDocument Members

        public bool ShowClose
        {
            get { return true; }
        }

        public string TabCaption
        {
            get { return Filename; }
        }

        #endregion
    }

    

}
