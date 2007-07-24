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

namespace primeira.pNeuron.pProject
{

    public enum pProjectItemTypes
    {
        Project,
        NeuralNetwork,
        TrainingSet,
        Domain
    }

    class pProject : IpProjectItem
    {

        #region fields

        private string m_filename;
        private bool m_defaultNamed;
        private bool m_modified;
        private pProjectItemTypes m_type;
        private List<IpProjectItem> m_item;

        #endregion

        #region IpProject Members

        public string Filename
        {
            get { return m_filename; }
        }

        public bool DefaultNamed
        {
            get { return m_defaultNamed; }
        }

        public bool Modified
        {
            get { return m_modified; }
        }

        public DialogResult Save()
        {
            return DialogResult.OK;
        }

        public DialogResult Load()
        {
            return DialogResult.OK;
        }

        public pProjectItemTypes Type
        {
            get { return m_type; }
        }

        #endregion

        #region pProject Members

        public List<IpProjectItem> Item
        {
            get { return m_item; }
        }

        #endregion
    }
     


}
