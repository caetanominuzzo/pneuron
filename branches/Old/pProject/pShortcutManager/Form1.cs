using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace pShortcutManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pShortcutManager1.LoadFromForm(this);
        }

        [pShortcutManagerVisible("General.ShowMessage.A", "", Keys.A)]
        public void MessagemA(object sender, EventArgs e)
        {
            MessageBox.Show("A");
        }

        [pShortcutManagerVisible("General.ShowMessage.B", "", Keys.B, KeyModifiers.Control)]
        public void MessagemB(object sender, EventArgs e)
        {
            MessageBox.Show("B");
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }



    }
}