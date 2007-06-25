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
    public class pNetworkExplorer : DockContent, IpDocks
    {
        #region IpDocks Members

        public pNeuronIDE Parent 
        {
            get { return ((pNeuronIDE)DockPanel.Parent); }
        }

        #endregion
    }
}
