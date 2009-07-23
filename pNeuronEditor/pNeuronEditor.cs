using System;
using System.Windows.Forms;
using pNeuronEditor.Business;
using pNeuronEditor.Components;
using pShortcutManager.Business;

namespace pNeuronEditor
{
    public partial class pNeuronEditor : Form, IpShorcutEscopeProvider
    {
        TabControl.TabControlEditor _tabControl;

        FileBrowserEditor  _fileBrowser;

        pShortcutManager.pShortcutManager _shortcutManager;

        public pNeuronEditor()
        {
            InitializeComponent();
            
            EditorManager.RegisterEditors();
            
            
            _tabControl = (TabControl.TabControlEditor)EditorManager.LoadEditor("default.tabcontrol");

            this.Controls.Add(_tabControl);

            TabManager.GetInstance().SetTabControl(_tabControl);
            
            _fileBrowser = (FileBrowserEditor)EditorManager.LoadEditor("default.filebrowser");

            FileManager.SetRecentManager(_fileBrowser);

            _shortcutManager = new pShortcutManager.pShortcutManager();

            _shortcutManager.LoadFromForm(this);

            _shortcutManager.EscopeProvider = this;
        }

        #region Shortcuts

        [pShortcutManagerVisible("ShowFileTab", "Show the File tab", "Main", Keys.T, KeyModifiers.Control)]
        public void ShowFileTab()
        {
            _fileBrowser.Selected = true;
        }
        
        [pShortcutManagerVisible("CloseTab", "Close the current tab", "Main", Keys.F4, KeyModifiers.Control)]
        public void CloseTab()
        {
            TabManager.GetInstance().CloseEditor();
        }

        [pShortcutManagerVisible("NextTab", "Show the next tab", "Main", Keys.Tab, KeyModifiers.Control)]
        public void NextTab()
        {
            TabManager.GetInstance().SelectNext();
        }

        [pShortcutManagerVisible("PreviousTab", "Show the previous tab", "Main", Keys.Tab, KeyModifiers.Control | KeyModifiers.Shift)]
        public void PreviousTab()
        {
            TabManager.GetInstance().SelectPrior();
        }

        #endregion

        #region IpShorcutEscopeProvider Members

        public bool IsAtiveByControl(string controlName)
        {
            return true;
        }

        public bool IsAtiveByEscope(string escope)
        {
            return true;
        }

        #endregion

        #region Event Handlers

        private void pNeuronEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            _tabControl.Document.ToXml(_tabControl.Filename);
        }

        #endregion

    }
}