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


namespace primeira.pNeuron.pNeuronIDE
{
    public partial class pNeuronIDE : Form
    {

        #region Tools & Project

        public pProperty fmProperty = new pProperty();
        public pToolbox fmToolbox = new pToolbox();
        public pGroupExplorer fmGroupExplorer = new pGroupExplorer();
        public pNetworkExplorer fmNetworkExplorer = new pNetworkExplorer();
        public pPlotter fmPlotter = new pPlotter();

        public pProject Project;

        #endregion

        public pNeuronIDE()
        {
            InitializeComponent();

            fmToolbox.Show(dockPanel, DockState.DockLeft);

            fmPlotter.Show(dockPanel, DockState.DockBottomAutoHide);

            fmGroupExplorer.Show(dockPanel, DockState.DockRight);

            fmNetworkExplorer.Show(dockPanel, DockState.DockRight);
            fmNetworkExplorer.DockTo(fmGroupExplorer.Pane, DockStyle.Fill, 0);

            fmProperty.Show(dockPanel, DockState.DockRight);
            fmProperty.DockTo(fmGroupExplorer.Pane, DockStyle.Bottom, 0);
        }
    }
}