using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace primeira.pNeuron
{
    public abstract partial class pXMLEditor : UserControl
    {
        public pXMLEditor()
        {
            InitializeComponent();
        }

        public abstract void AddItem(string defaultItemName);

        public abstract string DefaultItemName { get; }

        private void btNewTrainingSet_Click(object sender, EventArgs e)
        {
            AddItem(DefaultItemName);
        }


    }
}
