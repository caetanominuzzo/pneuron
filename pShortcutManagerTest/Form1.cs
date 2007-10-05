using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using pShortcutManager;

namespace pShortcutManagerTest
{
    public partial class Form1 : Form
    {
        pShortcutManager.pShortcutManager p = new pShortcutManager.pShortcutManager();
        public Form1()
        {
            InitializeComponent();
             
            p.LoadFromForm(this);

        //    p.LoadConfigFile("");

            p.AddEscope("Main");
        }

        [pShortcutManagerVisible("Main.ShowA", "Show an 'A' MessageBox", "Main", Keys.A, KeyModifiers.Control)]
        public void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("A");
        }

        [pShortcutManagerVisible("Main.ShowB", "Show a 'B' MessageBox", "Main", Keys.B, KeyModifiers.Control)]
        public void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("B");
        }

        [pShortcutManagerVisible("Main.ShowC", "Show a 'C' MessageBox", "Main", Keys.C, KeyModifiers.Control)]
        public void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("C");
        }

        [pShortcutManagerVisible("Editor1.PrintD", "Print a 'D'", "Editor1", Keys.D, KeyModifiers.Control)]
        public void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += "D";
        }

        [pShortcutManagerVisible("Editor2.PrintD", "Print a 'D'", "Editor2", Keys.D, KeyModifiers.Control)]
        public void button6_Click(object sender, EventArgs e)
        {
            textBox2.Text += "E";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            p.ShowConfig();
            
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            p.AddEscope("Editor1");
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            p.RemoveEscope("Editor1");
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            p.AddEscope("Editor2");
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            p.RemoveEscope("Editor2");
        }
    }
}