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
        private Label lbDescription;
        private Label label5;
        private ComboBox cbEscope;

        pShortcutManager fpShorcutManager;

        public pShortcutManagerEditor(pShortcutManager aShorcutManager)
        {
            fpShorcutManager = aShorcutManager;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
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
            this.lbDescription = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbEscope = new System.Windows.Forms.ComboBox();
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
            this.lsCommand.SelectedIndexChanged += new System.EventHandler(this.lsCommand_SelectedIndexChanged);
            // 
            // cbShortcut
            // 
            this.cbShortcut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbShortcut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbShortcut.FormattingEnabled = true;
            this.cbShortcut.Location = new System.Drawing.Point(15, 159);
            this.cbShortcut.Name = "cbShortcut";
            this.cbShortcut.Size = new System.Drawing.Size(347, 21);
            this.cbShortcut.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Shorcut for selected command";
            // 
            // btRemove
            // 
            this.btRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btRemove.Location = new System.Drawing.Point(368, 159);
            this.btRemove.Name = "btRemove";
            this.btRemove.Size = new System.Drawing.Size(75, 23);
            this.btRemove.TabIndex = 5;
            this.btRemove.Text = "&Remove";
            this.btRemove.UseVisualStyleBackColor = true;
            this.btRemove.Click += new System.EventHandler(this.btRemove_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(180, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Press new shortcut key";
            // 
            // btAssign
            // 
            this.btAssign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btAssign.Location = new System.Drawing.Point(368, 199);
            this.btAssign.Name = "btAssign";
            this.btAssign.Size = new System.Drawing.Size(75, 23);
            this.btAssign.TabIndex = 8;
            this.btAssign.Text = "Assign";
            this.btAssign.UseVisualStyleBackColor = true;
            this.btAssign.Click += new System.EventHandler(this.btAssign_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 222);
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
            this.cbCurrently.Location = new System.Drawing.Point(15, 238);
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
            this.panel1.Location = new System.Drawing.Point(-8, 287);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(470, 55);
            this.panel1.TabIndex = 11;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(374, 9);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
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
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // txtShortcut
            // 
            this.txtShortcut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtShortcut.Location = new System.Drawing.Point(183, 199);
            this.txtShortcut.Name = "txtShortcut";
            this.txtShortcut.Size = new System.Drawing.Size(179, 20);
            this.txtShortcut.TabIndex = 12;
            this.txtShortcut.TextChanged += new System.EventHandler(this.txtShortcut_TextChanged);
            // 
            // lbDescription
            // 
            this.lbDescription.AutoSize = true;
            this.lbDescription.Location = new System.Drawing.Point(12, 123);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(60, 13);
            this.lbDescription.TabIndex = 13;
            this.lbDescription.Text = "Description";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Use this shortcut in";
            // 
            // cbEscope
            // 
            this.cbEscope.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEscope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEscope.FormattingEnabled = true;
            this.cbEscope.Location = new System.Drawing.Point(15, 198);
            this.cbEscope.Name = "cbEscope";
            this.cbEscope.Size = new System.Drawing.Size(162, 21);
            this.cbEscope.TabIndex = 15;
            // 
            // pShortcutManagerEditor
            // 
            this.AcceptButton = this.btOk;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(455, 331);
            this.Controls.Add(this.cbEscope);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbDescription);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "pShortcutManagerEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shortcut Manager";
            this.Load += new System.EventHandler(this.pShortcutManagerEditor_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void pShortcutManagerEditor_Load(object sender, EventArgs e)
        {
            foreach (pShortcutCommand p in fpShorcutManager.Command)
            {
                    if (!lsCommand.Items.Contains(p.Name))
                        lsCommand.Items.Add(p.Name);

                
            }

            foreach (pShortcut p in fpShorcutManager.pShorcut)
            {
                if (!cbEscope.Items.Contains(p.Escope))
                    cbEscope.Items.Add(p.Escope);
            }

        }

        private void lsCommand_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbShortcut.Items.Clear();
            foreach (pShortcutCommand p in fpShorcutManager.Command)
            {
                if(p.Name == lsCommand.SelectedItem.ToString())
                {
                    lbDescription.Text = p.Description;
                    foreach (pShortcut pp in fpShorcutManager.pShorcut)
                    {
                        if (pp.Command == p && pp.AtomID != 0)
                            cbShortcut.Items.Add(pp.ToString());
                    }
                }
            }

            if (cbShortcut.Items.Count > 0)
                cbShortcut.SelectedIndex = 0;
        }

        private void btRemove_Click(object sender, EventArgs e)
        {
            if(cbShortcut.SelectedItem != null)
                foreach (pShortcut p in fpShorcutManager.pShorcut)
                {
                    if (p.ToString() == cbShortcut.SelectedItem.ToString())
                    {
                        fpShorcutManager.Unassign(p);
                       
                    }
                }

            lsCommand_SelectedIndexChanged(null, null);
        }

        private void btAssign_Click(object sender, EventArgs e)
        {
            fpShorcutManager.Assign(lsCommand.SelectedItem.ToString(), cbEscope.Text, txtShortcut.Key, txtShortcut.Modifiers);
            lsCommand_SelectedIndexChanged(null, null);
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            fpShorcutManager.WriteConfigFile("");
            this.Close();
        }

        private void txtShortcut_TextChanged(object sender, EventArgs e)
        {
            cbCurrently.Items.Clear();
            foreach (pShortcut p in fpShorcutManager.pShorcut)
            {
                if (p.Key == txtShortcut.Key && p.KeyModifier == txtShortcut.Modifiers && p.AtomID != 0 && p.Escope == cbEscope.Text)
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
    }

    #endregion

    #region pShortcutTextBox

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

            this.Text = (Modifiers == KeyModifiers.None? "" : Modifiers.ToString() + "+") + e.KeyCode.ToString();
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

        public pShortcutManagerVisible(string aName, string aDescription, string aEscope, Keys aDefaultKey)
        {
            Name = aName;
            Description = aDescription;
            DefaultKey = aDefaultKey;
            Escope = aEscope;
        }

        public pShortcutManagerVisible(string aName, string aDescription, string aEscope, Keys aDefaultKey, KeyModifiers aDefaultKeyModifiers)
            : this(aName, aDescription, aEscope, aDefaultKey)
        {
            DefaultKeyModifiers = aDefaultKeyModifiers;
        }
    }

    #endregion

    internal class pShortcutCommand
    {
        internal string Name;
        internal string Description;
        internal MethodInfo Method;
        internal object Object;
    }

    #region pShortcut

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

    #endregion

    public class pShortcutManager : Component, IMessageFilter
    {
        private const int WM_HOTKEY = 0x0312;
        private IntPtr fParenthandle = (IntPtr)0;
        private List<string> fEscope = new List<string>();

        private List<pShortcut> fpShorcut = new List<pShortcut>();

        private List<pShortcutCommand> fCommand = new List<pShortcutCommand>();

        internal List<pShortcutCommand> Command
        {
            get { return fCommand; }
            set { fCommand = value; }
        }

        internal List<pShortcut> pShorcut
        {
            get { return fpShorcut; }
            set { fpShorcut = value; }
        }

        #region Native win32 API

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, Keys vk);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int GlobalAddAtom(string LPCTSTR);

        #endregion


        public bool PreFilterMessage(ref Message m)
        {
            bool KeepMessage = false;

            if (fEscope.Count == 0)
                return false;

            switch (m.Msg)
            {
                case WM_HOTKEY:
                    foreach (pShortcut p in fpShorcut)
                    {
                        if (p.AtomID == (int)m.WParam && fEscope.Contains(p.Escope))
                        {
                            //p.Command.Method.Invoke(p.Command.Object, new object[] { this, new EventArgs() });
                            p.Command.Method.Invoke(p.Command.Object, new object[] { });
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

        internal pShortcutCommand AddCommand(string Name, string Description, MethodInfo aMethod, object aObject)
        {
            pShortcutCommand c = new pShortcutCommand();
            c.Name = Name;
            c.Description = Description;
            c.Method = aMethod;
            c.Object = aObject;
            fCommand.Add(c);
            return c;
        }

        internal void AddShortcut(string aEscope, Keys aKey, KeyModifiers aKeyModifiers, pShortcutCommand aCommand)
        {
            pShortcut s = new pShortcut();
            s.Key = aKey;
            s.KeyModifier = aKeyModifiers;
            s.AtomID = GlobalAddAtom(s.ToString());
            s.Escope = aEscope;
            s.Command = aCommand;
            fpShorcut.Add(s);
            s.Register(fParenthandle);
        }

        public void WriteConfigFile(string aFilename)
        {
            if (fParenthandle == (IntPtr)0)
                throw new InvalidOperationException("Use LoadFromForm before try it.");

            string Filename = "";
            if (aFilename == "")
                Filename = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\shortcuts.xml";
            else Filename = aFilename;

            DataTable dt = new DataTable("pShorCut", "pShortcutManager");
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Key", typeof(Keys));
            dt.Columns.Add("KeyModifier", typeof(KeyModifiers));
            dt.Columns.Add("Escope", typeof(string));


            foreach (pShortcut p in fpShorcut)
            {
                if (p.AtomID == 0)
                    continue;
                dt.Rows.Add(
                    new object[]
                {
                    p.Command.Name,
                    p.Key,
                    p.KeyModifier,
                    p.Escope
                });
            }

            dt.WriteXml(Filename);
            dt.Dispose();

        }

        public void LoadConfigFile(string aFilename)
        {
            if (fParenthandle == (IntPtr)0)
                throw new InvalidOperationException("Use LoadFromForm before try it.");

            foreach (pShortcut p in fpShorcut)
                Unassign(p);

            DataSet ds = new DataSet();

             string Filename = "";
            if (aFilename == "")
                Filename = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\shortcuts.xml";
            else Filename = aFilename;

            ds.ReadXml(Filename);

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                foreach (pShortcutCommand p in fCommand)
                {
                    if(p.Name == r["Name"].ToString())
                    {
                        AddShortcut(r["Escope"].ToString(), (Keys)int.Parse(r["Key"].ToString()), (KeyModifiers)int.Parse(r["KeyModifier"].ToString()), p);
                        break;
                    }
                }
            }

        }

        public void UnloadFromForm(Control aControl)
        {
            this.fParenthandle = aControl.Handle;
            foreach (MethodInfo m in aControl.GetType().GetMethods())
            {
                foreach (object o in m.GetCustomAttributes(false))
                    if (o is pShortcutManagerVisible)
                    {
                        pShortcutManagerVisible v = (pShortcutManagerVisible)o;

                        pShortcutCommand command = null;

                        foreach (pShortcutCommand cc in fCommand)
                        {
                            if (cc.Name == v.Name)
                            {
                                command = cc;
                                break;
                            }
                        }

                        List<pShortcut> ToRemove = new List<pShortcut>();

                        foreach (pShortcut pp in fpShorcut)
                        {
                            if (pp.Command == command)
                            {
                                UnregisterHotKey(fParenthandle, pp.AtomID);
                                ToRemove.Add(pp);
                            }
                        }


                        while (ToRemove.Count > 0)
                        {
                            
                            fpShorcut.Remove(ToRemove[0]);
                        }
                            

                            
                    }
            }
           
        }

        public void LoadFromForm(Control aControl)
        {
            

            this.fParenthandle = aControl.Handle;
            foreach (MethodInfo m in aControl.GetType().GetMethods())
            {
                foreach (object o in m.GetCustomAttributes(false))
                    if (o is pShortcutManagerVisible)
                    {
                        pShortcutManagerVisible v = (pShortcutManagerVisible)o;

                        pShortcutCommand command = null;

                        foreach (pShortcutCommand cc in fCommand)
                        {
                            if (cc.Name == v.Name)
                            {
                                command = cc;
                                break;
                            }
                        }

                        if(command == null) 
                            command = AddCommand(v.Name, v.Description, m, aControl);

                        AddShortcut(v.Escope, v.DefaultKey, v.DefaultKeyModifiers, command);
                    }
            }

        }


        public void Assign(string aName, string aEscope, Keys aKey, KeyModifiers aKeyModifiers)
        {
            pShortcut shortcut = new pShortcut();

            foreach (pShortcut p in fpShorcut)
            {
                if (p.Key == aKey && p.KeyModifier == aKeyModifiers && p.AtomID != 0 && p.Escope == aEscope)
                {
                    Unassign(p);
                    shortcut = p;
                    break;
                }
            }

            pShortcutCommand command = new pShortcutCommand();

            foreach (pShortcutCommand p in fCommand)
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
            UnregisterHotKey(fParenthandle, aShorcut.AtomID);
            aShorcut.AtomID = 0;
        }

        public void ShowConfig()
        {
            List<string> pEscope = this.fEscope;
            fEscope.Clear();
            pShortcutManagerEditor p = new pShortcutManagerEditor(this);
            p.ShowDialog();
            fEscope.AddRange(pEscope);
        }

        public void AddEscope(string aEscope)
        {
            fEscope.Add(aEscope);
        }

        public void RemoveEscope(string aEscope)
        {
            fEscope.Remove(aEscope);
        }
    }

}
