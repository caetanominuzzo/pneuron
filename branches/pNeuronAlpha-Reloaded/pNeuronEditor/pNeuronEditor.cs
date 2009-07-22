using System;
using System.Windows.Forms;
using pNeuronEditor.Business;
using pNeuronEditor.Components;
using pShortcutManager.Business;

namespace pNeuronEditor
{
    public partial class pNeuronEditor : Form, IpShorcutEscopeProvider
    {
        FileBrowserEditor  _fileBrowser;

        pShortcutManager.pShortcutManager _shortcutManager;

        public pNeuronEditor()
        {
            InitializeComponent();

            EditorManager.RegisterEditors();

            TabManager.GetInstance().SetTabControl(this.tbMain);

            _fileBrowser = (FileBrowserEditor)DocumentManager.LoadDocument("default.filebrowser");

            FileManager.SetRecentManager(_fileBrowser);

            _shortcutManager = new pShortcutManager.pShortcutManager();

            _shortcutManager.LoadFromForm(this);

            _shortcutManager.Provider = this;
        }

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
    }
}