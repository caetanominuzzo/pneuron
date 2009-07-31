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
    public partial class TopologyPropertyGrid : UserControl
    {
        public TopologyPropertyGrid()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        private NeuralNetworkEditor _document;

        public void SetDocument(NeuralNetworkEditor document)
        {
            _document = document;

            propertyGrid1.SelectedObject = _document.MainDisplay.Net;

            _document.MainDisplay.OnSelectedPanelsChange += new pDisplay.SelectedPanelsChangeDelegate(MainDisplay_OnSelectedPanelsChange);
        }

        void MainDisplay_OnSelectedPanelsChange()
        {
            if(_document.MainDisplay.SelectedpPanels.Count() == 0)
                propertyGrid1.SelectedObject = _document.MainDisplay.Net;
            else if (_document.MainDisplay.SelectedpPanels.Count() == 1)
                propertyGrid1.SelectedObject = _document.MainDisplay.SelectedpPanels[0];
            else if (_document.MainDisplay.SelectedpPanels.Count() > 1)
                propertyGrid1.SelectedObjects = _document.MainDisplay.SelectedpPanels.ToArray();
        }

        #region ITabDocument Members

        public bool ShowCloseButton
        {
            get { return false; }
        }

        public string TabCaption
        {
            get { return "Properties"; }
        }

        #endregion
    }
}
