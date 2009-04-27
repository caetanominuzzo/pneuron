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

namespace pShortcutManager
{

    [Flags()]
    public enum KeyModifiers
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        Windows = 8

    }

    #region Form

    public class pShortcutManagerEditor : Form
    {
        private Label label1;
        private TextBox txtCommand;
        private ListBox lsCommand;
        private ComboBox cbShortcut;
        private Label label2;
        private Button btRemove;
        private Label label3;
        private Button btAssign;
        private Label label4;
        private ComboBox cbCurrently;
        private Panel panel1;
        private Button btOk;
        private Button btCancel;
        private pShortcutTextBox txtShortcut;

        List<pShortcut> fpShortcut;

        public pShortcutManagerEditor(ref List<pShortcut> aShorcutList)
        {
            fpShortcut = aShorcutList;
        }

        private void InitializeComponent()
        {
            #region
            this.label1 = new System.Windows.Forms.Label();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.lsCommand = new System.Windows.Forms.ListBox();
            this.cbShortcut = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btRemove = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btAssign = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbCurrently = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            this.txtShortcut = new pShortcutTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Show commands containing";
            // 
            // txtCommand
            // 
            this.txtCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommand.Location = new System.Drawing.Point(15, 25);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(428, 20);
            this.txtCommand.TabIndex = 1;
            // 
            // lsCommand
            // 
            this.lsCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lsCommand.FormattingEnabled = true;
            this.lsCommand.Location = new System.Drawing.Point(15, 51);
            this.lsCommand.Name = "lsCommand";
            this.lsCommand.Size = new System.Drawing.Size(428, 69);
            this.lsCommand.TabIndex = 2;
            // 
            // cbShortcut
            // 
            this.cbShortcut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbShortcut.FormattingEnabled = true;
            this.cbShortcut.Location = new System.Drawing.Point(15, 139);
            this.cbShortcut.Name = "cbShortcut";
            this.cbShortcut.Size = new System.Drawing.Size(347, 21);
            this.cbShortcut.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Shorcut for selected command";
            // 
            // btRemove
            // 
            this.btRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btRemove.Location = new System.Drawing.Point(368, 139);
            this.btRemove.Name = "btRemove";
            this.btRemove.Size = new System.Drawing.Size(75, 23);
            this.btRemove.TabIndex = 5;
            this.btRemove.Text = "&Remove";
            this.btRemove.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Press new shortcut key";
            // 
            // btAssign
            // 
            this.btAssign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btAssign.Location = new System.Drawing.Point(368, 179);
            this.btAssign.Name = "btAssign";
            this.btAssign.Size = new System.Drawing.Size(75, 23);
            this.btAssign.TabIndex = 8;
            this.btAssign.Text = "Assign";
            this.btAssign.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 202);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Shortcut currently used by";
            // 
            // cbCurrently
            // 
            this.cbCurrently.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCurrently.FormattingEnabled = true;
            this.cbCurrently.Location = new System.Drawing.Point(15, 218);
            this.cbCurrently.Name = "cbCurrently";
            this.cbCurrently.Size = new System.Drawing.Size(428, 21);
            this.cbCurrently.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btCancel);
            this.panel1.Controls.Add(this.btOk);
            this.panel1.Location = new System.Drawing.Point(-8, 258);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(470, 55);
            this.panel1.TabIndex = 11;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.Location = new System.Drawing.Point(374, 9);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOk
            // 
            this.btOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btOk.Location = new System.Drawing.Point(293, 9);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 0;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // txtShortcut
            // 
            this.txtShortcut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtShortcut.Location = new System.Drawing.Point(15, 179);
            this.txtShortcut.Name = "txtShortcut";
            this.txtShortcut.Size = new System.Drawing.Size(347, 20);
            this.txtShortcut.TabIndex = 12;
            // 
            // pShortcutManagerEditor
            // 
            this.ClientSize = new System.Drawing.Size(455, 302);
            this.Controls.Add(this.txtShortcut);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbCurrently);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btAssign);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btRemove);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbShortcut);
            this.Controls.Add(this.lsCommand);
            this.Controls.Add(this.txtCommand);
            this.Controls.Add(this.label1);
            this.Name = "pShortcutManagerEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shortcut Manager";
            this.Load += new System.EventHandler(this.pShortcutManagerEditor_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

            #endregion
        }

        private void pShortcutManagerEditor_Load(object sender, EventArgs e)
        {
            foreach (pShortcut p in fpShortcut)
            {

            }
        }
    }

    #endregion

    #region pShortcutTextBox

    public class pShortcutTextBox : TextBox
    {

        public Keys Key;
        public KeyModifiers Modifiers;


        protected override void OnKeyDown(KeyEventArgs e)
        {
            Key = e.KeyData;
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

            this.Text = e.KeyData.ToString();
            e.SuppressKeyPress = true;
        }

    }


    #endregion

    #region pShortcutManagerVisible Attribute

    public class pShortcutManagerVisible : Attribute
    {
        private string fName;
        private string fDescription;
        private Keys fDefaultKey;
        private KeyModifiers fDefaultKeyModifiers;

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

        public pShortcutManagerVisible(string aName, string aDescription, Keys aDefaultKey)
        {
            Name = aName;
            Description = aDescription;
            DefaultKey = aDefaultKey;
            if (DefaultKeyModifiers == null)
                DefaultKeyModifiers = KeyModifiers.None;
        }

        public pShortcutManagerVisible(string aName, string aDescription, Keys aDefaultKey, KeyModifiers aDefaultKeyModifiers)
            : this(aName, aDescription, aDefaultKey)
        {
            DefaultKeyModifiers = aDefaultKeyModifiers;
        }
    }

    #endregion

    #region pShortcut

    public struct pShortcut
    {
        internal int AtomID;
        public Keys Key;
        public KeyModifiers KeyModifier;
        internal MethodInfo Method;
        internal object Object;

        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Key.ToString());
            sb.Append(this.KeyModifier.ToString());

            return sb.ToString();
        }
    }

    #endregion

    class pShortcutManager : Component, IMessageFilter
    {
        private const int WM_HOTKEY = 0x0312;
        private IntPtr fParenthandle;

        private List<pShortcut> fpShorcut = new List<pShortcut>();

        #region Native win32 API

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, Keys vk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int GlobalAddAtom(string LPCTSTR);

        #endregion


        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    foreach (pShortcut p in fpShorcut)
                    {
                        if (p.AtomID == (int)m.WParam)
                            p.Method.Invoke(p.Object, new object[] { this, new EventArgs() });
                    }
                    return true;
            }
            return false;
        }

        public pShortcutManager()
        {
            Application.AddMessageFilter(this);
        }

        public void AddShortcut(Keys aKey, KeyModifiers aKeyModifiers, MethodInfo aMethod, object aObject)
        {
            pShortcut s = new pShortcut();
            s.Key = aKey;
            s.KeyModifier = aKeyModifiers;
            s.AtomID = GlobalAddAtom(s.ToString());
            s.Method = aMethod;
            s.Object = aObject;
            fpShorcut.Add(s);
            RegisterHotKey(fParenthandle, s.AtomID, s.KeyModifier, s.Key);
        }

        public void LoadFromForm(Form aForm)
        {
            this.fParenthandle = aForm.Handle;
            foreach (MethodInfo m in aForm.GetType().GetMethods())
            {
                foreach (object o in m.GetCustomAttributes(false))
                    if (o is pShortcutManagerVisible)
                    {
                        pShortcutManagerVisible v = (pShortcutManagerVisible)o;
                        AddShortcut(v.DefaultKey, v.DefaultKeyModifiers, m, aForm);
                    }
            }

        }

    }
}
