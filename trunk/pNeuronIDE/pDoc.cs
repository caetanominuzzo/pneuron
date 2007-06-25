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

        public pNeuronIDE Parent
        {
            get { return DockPanel == null ? null : ((pNeuronIDE)DockPanel.Parent); }
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
                        this.TabText = this.TabText.Substring(0, TabText.Length - 2) + "]";
                    }
                    else
                    {
                        this.TabText = this.TabText.Substring(0, TabText.Length - 1) + "*]";
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
                    Parent.fmNetworkExplorer.RemoveNode(m_filename);
                    Parent.fmNetworkExplorer.AddNode(value);
                }
                m_filename = value;
                this.TabText = "[" + value + "]";
            }
        }

        public bool DefaultNamedFile
        {
            get { return m_defaultNamedFile; }
            set { m_defaultNamedFile = value; }
        }
    }

}
