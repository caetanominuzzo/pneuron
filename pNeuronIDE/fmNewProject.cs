using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace primeira.pNeuron
{
    public partial class fmNewProject : Form
    {

        bool OkClick = false;


        public fmNewProject()
        {
            InitializeComponent();
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txLocation.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        public static string Show()
        {
            fmNewProject fm = new fmNewProject();
            fm.ShowDialog();

            if (!fm.OkClick)
                return "";

            if (fm.ckCreateDir.Checked)
            {
                System.IO.Directory.CreateDirectory(fm.txLocation.Text + "\\" + fm.txName.Text);
                return fm.txLocation.Text + "\\" + fm.txName.Text + "\\" + fm.txName.Text + ".pnp";
            }

            return fm.txLocation.Text + "\\" + fm.txName.Text + ".pnp";
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (txName.Text == "")
            {
                MessageBox.Show("Please set the project name field.");
                txName.Focus();
                return;
            }

            if (txLocation.Text == "")
            {
                MessageBox.Show("Please set the project location field.");
                txLocation.Focus();
                return;
            }
            OkClick = true;
            Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            txLocation.Text = "";
            Close();
        }

        private void fmNewProject_Load(object sender, EventArgs e)
        {
            txLocation.Text = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\pNeuron Projects";
        }

        private void fmNewProject_FormClosing(object sender, FormClosingEventArgs e)
        {
        //    MessageBox.Show("1");
        }
    }
}