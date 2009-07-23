using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Design;
using System.Reflection;
using pShortcutManager.Business;
using pShortcutManager.Components;

namespace pShortcutManager
{
    public class pShortcutManager : Component, IMessageFilter
    {

        #region Native win32 API

        private const int WM_HOTKEY = 0x0312;

        private IntPtr _parentHandle = (IntPtr)0;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, Keys vk);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int GlobalAddAtom(string LPCTSTR);

        #endregion
        
        private List<pShortcut> _shortcuts = new List<pShortcut>();

        private List<pShortcutCommand> _commands = new List<pShortcutCommand>();

        public IpShorcutEscopeProvider EscopeProvider { get; set; }

        internal pShortcutCommand[] Commands
        {
            get { return _commands.ToArray(); }
        }

        internal pShortcut[] Shorcuts
        {
            get { return _shortcuts.ToArray(); }
        }

        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    foreach (pShortcut p in _shortcuts)
                    {
                        if (p.AtomID == (int)m.WParam && EscopeProvider != null && EscopeProvider.IsAtiveByEscope(p.Escope))
                        {
                            p.Command.Method.Invoke(p.Command.Object, new object[0]);
                            return true;
                        }
                    }
                    break;
            }
            return false;
        }

        public pShortcutManager()
        {
            Application.AddMessageFilter(this);
        }

        internal pShortcutCommand AddCommand(string name, string description, MethodInfo method, Object aObject, string escope)
        {
            pShortcutCommand c = new pShortcutCommand();
            
            c.Name = name;
            c.Description = description;
            c.Method = method;
            c.Object = aObject;
            c.Escope = escope;
            _commands.Add(c);

            return c;
        }

        internal void AddShortcut(string escope, Keys key, KeyModifiers keyModifiers, pShortcutCommand command)
        {
            pShortcut s = new pShortcut();

            s.Key = key;
            s.KeyModifier = keyModifiers;
            s.AtomID = GlobalAddAtom(s.ToString());
            s.Escope = escope;
            s.Command = command;
            _shortcuts.Add(s);

            s.Register(_parentHandle);
        }

        public void LoadFromForm(Form aForm)
        {
            this._parentHandle = aForm.Handle;
            foreach (MethodInfo m in aForm.GetType().GetMethods())
            {
                foreach (object o in m.GetCustomAttributes(false))
                    if (o is pShortcutManagerVisibleAttribute)
                    {
                        pShortcutManagerVisibleAttribute v = (pShortcutManagerVisibleAttribute)o;

                        pShortcutCommand command = null;

                        foreach (pShortcutCommand cc in _commands)
                        {
                            if (cc.Name == v.Name)
                            {
                                command = cc;
                                break;
                            }
                        }

                        if(command == null) 
                            command = AddCommand(v.Name, v.Description, m, aForm, v.Escope);

                        AddShortcut(v.Escope, v.DefaultKey, v.DefaultKeyModifiers, command);
                    }
            }

        }

        public void Assign(string aName, string aEscope, Keys aKey, KeyModifiers aKeyModifiers)
        {
            pShortcut shortcut = new pShortcut();

            foreach (pShortcut p in _shortcuts)
            {
                if (p.Key == aKey && p.KeyModifier == aKeyModifiers && p.Escope == aEscope)
                {
                    Unassign(p);
                    shortcut = p;
                    break;
                }
            }

            pShortcutCommand command = new pShortcutCommand();

            foreach (pShortcutCommand p in _commands)
            {
                if (p.Name == aName)
                {
                    command = p;
                    break;
                    
                }
            }

            AddShortcut(aEscope, aKey, aKeyModifiers, command);

        }

        internal void Unassign(pShortcut aShorcut)
        {

            _shortcuts.Remove(aShorcut);

            //Verify if another command is using that atomID.
            //If not unregister key
            foreach (pShortcut p in _shortcuts)
            {
                if (p.AtomID == aShorcut.AtomID)
                    return;
            }

            UnregisterHotKey(_parentHandle, aShorcut.AtomID);

        }

        public void ShowConfig()
        {
            fmShortcutConfig p = new fmShortcutConfig(this);
            p.ShowDialog();
        }

    }
    
}
