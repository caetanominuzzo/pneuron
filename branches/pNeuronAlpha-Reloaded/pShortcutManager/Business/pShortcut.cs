using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace pShortcutManager.Business
{
    internal class pShortcut
    {
        internal int AtomID;
        internal Keys Key;
        internal KeyModifiers KeyModifier;
        internal string Escope;
        internal pShortcutCommand Command;

        public new string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.KeyModifier.ToString());
            sb.Append("+");
            sb.Append(this.Key.ToString());
            sb.Append(" (");
            sb.Append(Escope);
            sb.Append(")");

            return sb.ToString();
        }

        public void Register(IntPtr ParentHandle)
        {
            pShortcutManager.RegisterHotKey(ParentHandle, AtomID, KeyModifier, Key);
        }
    }

    [Flags()]
    public enum KeyModifiers
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        Windows = 8

    }
}
