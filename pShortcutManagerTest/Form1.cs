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

            p.LoadConfigFile("");
        }

        [pShortcutManagerVisible("Main.ShowA", "Show an 'A' MessageBox", Keys.A, KeyModifiers.Control)]
        public void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("A");
        }

        [pShortcutManagerVisible("Main.ShowB", "Show a 'B' MessageBox", Keys.B, KeyModifiers.Control)]
        public void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("B");
        }

        [pShortcutManagerVisible("Main.ShowC", "Show a 'C' MessageBox", Keys.C, KeyModifiers.Control)]
        public void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("C");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            p.ShowConfig();
        }
    }
}