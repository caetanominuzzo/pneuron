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
        public Form1()
        {
            InitializeComponent();
            pShortcutManager.pShortcutManager p = new pShortcutManager.pShortcutManager();
            p.LoadFromForm(this);
        }

        [pShortcutManagerVisible("Main.ShowA", "Show an 'A' MessageBox", Keys.A, KeyModifiers.Control)]
        public void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("A");
        }

        [pShortcutManagerVisible("Main.ShowA", "Show a 'B' MessageBox", Keys.B, KeyModifiers.Control)]
        public void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("B");
        }
    }
}