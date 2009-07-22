using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using pShortcutManager.Business;

namespace pShortcutManager.Components
{
    public class pShortcutTextBox : TextBox
    {

        public Keys Key;
        public KeyModifiers Modifiers;


        protected override void OnKeyDown(KeyEventArgs e)
        {
            Key = e.KeyCode;
            Modifiers = KeyModifiers.None;

            switch (e.Modifiers)
            {
                case Keys.Control: Modifiers = Modifiers | KeyModifiers.Control;
                    break;
                case Keys.Alt: Modifiers = Modifiers | KeyModifiers.Alt;
                    break;
                case Keys.Shift: Modifiers = Modifiers | KeyModifiers.Shift;
                    break;
            }

            this.Text = (Modifiers == KeyModifiers.None ? "" : Modifiers.ToString() + "+") + e.KeyCode.ToString();
            e.SuppressKeyPress = true;
        }

    }

}
