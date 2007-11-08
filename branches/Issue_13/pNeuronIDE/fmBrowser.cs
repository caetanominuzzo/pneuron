using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using primeira.pNeuron.Core;

namespace primeira.pNeuron
{
    public partial class fmBrowser : DockContent, IpDocks
    {
        public fmBrowser()
        {
            InitializeComponent();
        }

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
    }
}