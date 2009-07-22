using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using pShortcutManager.Business;

namespace pShortcutManager.Components
{
    public partial class fmShortcutConfig : Form
    {
        private pShortcutManager _shorcutManager;

        public fmShortcutConfig(pShortcutManager shorcutManager)
        {
            _shorcutManager = shorcutManager;
            InitializeComponent();
        }

        private void pShortcutManagerEditor_Load(object sender, EventArgs e)
        {
            foreach (pShortcutCommand p in _shorcutManager.Commands)
            {
                if (!lsCommand.Items.Contains(p.Name))
                    lsCommand.Items.Add(p.Name);
            }

            foreach (pShortcutCommand p in _shorcutManager.Commands)
            {
                if (!cbEscope.Items.Contains(p.Escope))
                    cbEscope.Items.Add(p.Escope);
            }
        }

        private void lsCommand_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbShortcut.Items.Clear();
            foreach (pShortcutCommand p in _shorcutManager.Commands)
            {
                if (p.Name == lsCommand.SelectedItem.ToString())
                {
                    lbDescription.Text = p.Description;
                    foreach (pShortcut pp in _shorcutManager.Shorcuts)
                    {
                        if (pp.Command == p)
                            cbShortcut.Items.Add(pp.ToString());
                    }
                }
            }

            if (cbShortcut.Items.Count > 0)
                cbShortcut.SelectedIndex = 0;
        }

        private void btRemove_Click(object sender, EventArgs e)
        {
            if (cbShortcut.SelectedItem != null)
                foreach (pShortcut p in _shorcutManager.Shorcuts)
                {
                    if (p.ToString() == cbShortcut.SelectedItem.ToString())
                    {
                        _shorcutManager.Unassign(p);
                        break;
                    }
                }

            lsCommand_SelectedIndexChanged(null, null);
        }

        private void btAssign_Click(object sender, EventArgs e)
        {
            _shorcutManager.Assign(lsCommand.SelectedItem.ToString(), cbEscope.Text, txtShortcut.Key, txtShortcut.Modifiers);
            lsCommand_SelectedIndexChanged(null, null);
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtShortcut_TextChanged(object sender, EventArgs e)
        {
            cbCurrently.Items.Clear();
            foreach (pShortcut p in _shorcutManager.Shorcuts)
            {
                if (p.Key == txtShortcut.Key && p.KeyModifier == txtShortcut.Modifiers && p.Escope == cbEscope.Text)
                {
                    cbCurrently.Items.Add(p.Command.Name);
                }
            }

            if (cbCurrently.Items.Count > 0)
                cbCurrently.SelectedIndex = 0;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCommand_KeyPress(object sender, KeyPressEventArgs e)
        {
            lsCommand.Items.Clear();

            foreach (pShortcutCommand p in _shorcutManager.Commands)
            {
                if (!lsCommand.Items.Contains(p.Name) && p.Name.ToUpper().Contains(txtCommand.Text.ToUpper()))
                    lsCommand.Items.Add(p.Name);
            }

        }
    }
}
