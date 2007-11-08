using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace primeira.pNeuron
{
    public partial class fmTrainingSetName : Form
    {
        public fmTrainingSetName()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            
        }

        public fmTrainingSetName(string InitialName)
            : this()
        {
            txtName.Text = InitialName;
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void fmTrainingSetName_Load(object sender, EventArgs e)
        {
            txtName.Focus();
        }
    }
}