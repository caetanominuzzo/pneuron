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
using primeira.pHistory;

namespace primeira.pNeuron
{
    public partial class pHistory : DockContent, IpDocks
    {
        public delegate void RevertHistoryDelegate(byte[] history);
        public event RevertHistoryDelegate RevertHistory;

        public pHistory()
        {
            InitializeComponent();
            pHistoryManager.HistoryNeeded += new primeira.pHistory.pHistoryManager.HistoryNeededDelegate(pHistoryManager_HistoryNeeded);
        }

        primeira.pHistory.pHistoryItem pHistoryManager_HistoryNeeded()
        {
            return Parent.GiveMeAHistory();
        }

        void pHistoryManager_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && pHistoryManager.SelectedNode != null)
            {
                if (RevertHistory != null)
                    RevertHistory(((pHistoryItem)pHistoryManager.SelectedNode.Tag).Content);
            }
        }

        void pHistoryManager_NodeMouseDoubleClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            if (RevertHistory != null)
                RevertHistory(((pHistoryItem)pHistoryManager.SelectedNode.Tag).Content);
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