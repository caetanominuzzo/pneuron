using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using primeira.pNeuron;

namespace pNeuronEditor.Topology
{
    public partial class TopologyToolbox : UserControl
    {
        public TopologyToolbox()
        {
            InitializeComponent();
            Dock = DockStyle.Top;
        }

        #region ITabDocument Members

        public bool ShowCloseButton
        {
            get { return false; }
        }

        public string TabCaption
        {
            get { return "Toolbox"; }
        }

        #endregion

        private NeuralNetworkEditor _document;

        public void SetDocument(NeuralNetworkEditor document)
        {
            _document = document;
        }

        private void btCursor_Click(object sender, EventArgs e)
        {
            _document.MainDisplay.DisplayStatus = pDisplay.pDisplayStatus.Idle;
        }

        private void btAddNeuron_Click(object sender, EventArgs e)
        {
            _document.MainDisplay.DisplayStatus = pDisplay.pDisplayStatus.Add_Neuron;
        }

        private void btAddSynapse_Click(object sender, EventArgs e)
        {
            if (_document.MainDisplay.SelectedpPanels.Count() == 0)
                _document.MainDisplay.DisplayStatus = pDisplay.pDisplayStatus.Linking_Paused;
            else
                _document.MainDisplay.DisplayStatus = pDisplay.pDisplayStatus.Linking;
        }

        private void btDelNeuron_Click(object sender, EventArgs e)
        {
            
                _document.MainDisplay.DisplayStatus = pDisplay.pDisplayStatus.Remove_Neuron;
        }
    }
}
 