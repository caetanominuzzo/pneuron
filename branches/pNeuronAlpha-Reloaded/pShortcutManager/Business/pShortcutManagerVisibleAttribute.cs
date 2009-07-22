using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace pShortcutManager.Business
{
    public class pShortcutManagerVisibleAttribute : Attribute
    {
        private string fName;
        private string fDescription;
        private Keys fDefaultKey;
        private KeyModifiers fDefaultKeyModifiers;
        private string fEscope;

        public string Escope
        {
            get { return fEscope; }
            set { fEscope = value; }
        }


        public Keys DefaultKey
        {
            get { return fDefaultKey; }
            set { fDefaultKey = value; }
        }

        public KeyModifiers DefaultKeyModifiers
        {
            get { return fDefaultKeyModifiers; }
            set { fDefaultKeyModifiers = value; }
        }

        public string Name
        {
            get { return fName; }
            set { fName = value; }
        }

        public string Description
        {
            get { return fDescription; }
            set { fDescription = value; }
        }

        public pShortcutManagerVisibleAttribute(string aName, string aDescription, string aEscope, Keys aDefaultKey)
        {
            Name = aName;
            Description = aDescription;
            DefaultKey = aDefaultKey;
            Escope = aEscope;
        }

        public pShortcutManagerVisibleAttribute(string aName, string aDescription, string aEscope, Keys aDefaultKey, KeyModifiers aDefaultKeyModifiers)
            : this(aName, aDescription, aEscope, aDefaultKey)
        {
            DefaultKeyModifiers = aDefaultKeyModifiers;
        }
    }
}
