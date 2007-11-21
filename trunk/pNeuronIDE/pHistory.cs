using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using primeira.pNeuron.Core;
using System.IO;
using System.Threading;
using primeira.pRandom;
using primeira.pTypes;
using primeira.Components;

namespace primeira.pNeuron
{
    public partial class pHistory : DockContent, IpDocks
    {
        public pHistory()
        {
            InitializeComponent();
        }

        #region IpDocks Members

        public new pNeuronIDE Parent
        {
            get { return ((pNeuronIDE)DockPanel.Parent); }
        }

        #endregion

        private void btClear_Click(object sender, EventArgs e)
        {
            pHistoryManager.Nodes.Clear();
            GC.Collect();
            GC.GetTotalMemory(true);
        }
    }
}