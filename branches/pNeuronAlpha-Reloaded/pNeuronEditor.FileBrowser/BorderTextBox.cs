using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace primeira.pNeuron.Editor.Editors.FileBrowser
{
    public partial class BorderTextBox : UserControl
    {
        public BorderTextBox()
        {
            InitializeComponent();
        }

        public new string Text
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        private void textBox1_Resize(object sender, EventArgs e)
        {
            this.Height = textBox1.Height + 2;
        }

        private void BorderTextBox_Enter(object sender, EventArgs e)
        {
            textBox1.Focus();
            this.BackColor = Color.FromArgb(219, 225, 255);
        }

        private void BorderTextBox_Leave(object sender, EventArgs e)
        {
            this.BackColor = Color.Transparent;
        }
    }
}
