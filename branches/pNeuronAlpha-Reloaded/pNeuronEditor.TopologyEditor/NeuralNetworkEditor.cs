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
using primeira.pNeuron.Editor.Business;
using primeira.pNeuron.Editor;
using primeira.pNeuron;


namespace pNeuronEditor.TopologyEditor
{
    public partial class NeuralNetworkEditor : EditorBase
    {
        #region Ctor

        public NeuralNetworkEditor(string filename, DocumentBase data)
            : base(filename, data, typeof(NeuralNetworkDocument))
        {

            InitializeComponent();

            MainDisplay.OnDisplayStatusChange += new pDisplay.DisplayStatusChangeDelegate(MainDisplay_OnDisplayStatusChange);
            MainDisplay.OnNetworkChange += new pDisplay.NetworkChangeDelegate(MainDisplay_OnNetworkChange);

            Dock = DockStyle.Fill;

            TopologyPropertyGrid pp = new TopologyPropertyGrid();
            pp.SetDocument(this);
            pp.Dock = DockStyle.Fill;
            panel6.Controls.Add(pp);

            TopologyToolbox p = new TopologyToolbox();
            p.SetDocument(this);
            p.Dock = DockStyle.Fill;
            panel4.Controls.Add(p);

            if (((NeuralNetworkDocument)Data).NeuralNetwork == null)
                ((NeuralNetworkDocument)Data).NeuralNetwork = new NeuralNetwork();

            MainDisplay.SetNeuralNetwork(((NeuralNetworkDocument)Data).NeuralNetwork);

        }

        #endregion

        #region MainDisplay Events

        private void MainDisplay_OnDisplayStatusChange()
        {
            lblStatus.Text = MainDisplay.DisplayStatus.ToString();
        }

        private void MainDisplay_OnNetworkChange()
        {
            Changed();
        }

        #endregion

    }
}
