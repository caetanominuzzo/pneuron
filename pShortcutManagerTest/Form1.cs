using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using pShortcutManager;
using pShortcutManager.Business;

namespace pShortcutManagerTest
{
    public partial class Form1 : Form, IpShorcutEscopeProvider
    {
        pShortcutManager.pShortcutManager p = new pShortcutManager.pShortcutManager();

        public Form1()
        {
            InitializeComponent();
             
            p.LoadFromForm(this);

            p.Provider = this;
        }

        [pShortcutManagerVisible("Main.ShowA", "Show an 'A' MessageBox", "Main", Keys.A, KeyModifiers.Control)]
        public void ShowA()
        {
            MessageBox.Show("A");
        }

        [pShortcutManagerVisible("Main.ShowB", "Show a 'B' MessageBox", "Main", Keys.B, KeyModifiers.Control)]
        public void ShowB()
        {
            MessageBox.Show("B");
        }

        [pShortcutManagerVisible("Main.ShowBB", "Show a 'BB' MessageBox", "Main", Keys.R, KeyModifiers.Control)]
        public void ShowBB()
        {
            MessageBox.Show("BB");
        }

        [pShortcutManagerVisible("Main.ShowC", "Show a 'C' MessageBox", "Main", Keys.C, KeyModifiers.Control)]
        public void ShowC()
        {
            MessageBox.Show("C");
        }

        [pShortcutManagerVisible("Editor1.PrintD", "Print a 'D'", "Editor1", Keys.D, KeyModifiers.Control)]
        public void Edit1PrintD()
        {
            textBox1.Text += "D";
        }

        [pShortcutManagerVisible("Editor2.PrintD", "Print a 'D'", "Editor2", Keys.D, KeyModifiers.Control)]
        public void Edit2PrintD()
        {
            textBox2.Text += "E";
        }


        #region IpShorcutEscopeProvider Members

        public bool IsAtiveByControl(string controlName)
        {
            throw new NotImplementedException();
        }

        public bool IsAtiveByEscope(string escope)
        {
            switch (escope)
            {
                case "Main": return this.ContainsFocus;
                case "Editor1": return textBox1.Focused;
                case "Editor2": return textBox2.Focused;
                default: return false;
            }
        }

        #endregion

        private void button4_Click_1(object sender, EventArgs e)
        {
            p.ShowConfig();
        }
    }
}