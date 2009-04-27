using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace primeira.pNeuron
{
    public static class pMessage
    {
        public static DialogResult Alert(String msg)
        {
            return MessageBox.Show(msg, "pNeuron", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static DialogResult Error(String msg)
        {
            return MessageBox.Show(msg, "pNeuron", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult Confirm(String msg, MessageBoxButtons aButtons)
        {
            return MessageBox.Show(msg, "pNeuron", aButtons, MessageBoxIcon.Question);
        }
    }
}
