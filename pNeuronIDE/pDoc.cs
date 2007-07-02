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
    public class pDoc : DockContent, IpDocks
    {
        #region IpDocks Members

        private pNeuronIDE fParent;

        private bool fQueryOnClose = true;
	  
        public bool QueryOnClose
	    {
		    get { return fQueryOnClose; }
		    set { fQueryOnClose = value; }
	    }
        public pNeuronIDE Parent
        {
            get { return DockPanel == null ? fParent : ((pNeuronIDE)DockPanel.Parent); }
            set { fParent = value; }
        }

        #endregion

        private bool m_modificated = false;

        private string m_filename;
        private bool m_defaultNamedFile = true;

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
                if (Parent != null)
                {
                     Parent.fmNetworkExplorer.RenameNode(m_filename, value);
                }
                m_filename = value;
                this.TabText = value;
            }
        }

        public bool DefaultNamedFile
        {
            get { return m_defaultNamedFile; }
            set { m_defaultNamedFile = value; }
        }

        public DialogResult Save()
        {
            if (this is pDocDisplay)
                return ((pDocDisplay)this).Save();

            return DialogResult.OK;
        }

      
    }

}
