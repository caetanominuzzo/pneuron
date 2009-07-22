using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pNeuronEditor.Business
{
    public class TabManager
    {
        #region Singleton

        private TabManager() { }

        private static TabManager _tabManager;

        public static TabManager GetInstance()
        {
            if (_tabManager == null)
                _tabManager = new TabManager();

            return _tabManager;
        }

        #endregion

        #region TabControl

        private ITabControl _tabcontrol;

        public void SetTabControl(ITabControl tabcontrol)
        {
            _tabcontrol = tabcontrol;
        }

        public ITabControl TabControl
        {
            get { return _tabcontrol; }
        }

        #endregion

        #region ActiveDocument

        private IEditorBase _activeEditor = null;

        private List<IEditorBase> _openEditors = new List<IEditorBase>();

        int iChangeEditorIndex = -1;

        public IEditorBase ActiveEditor
        {
            get
            {
                return _activeEditor;
            }
            internal set
            {
                if (!_openEditors.Contains(value))
                    _openEditors.Insert(0, value);
                else
                {
                    //To control z-order
                    _openEditors.Remove(value);
                    _openEditors.Insert(0, value);
                }

                iChangeEditorIndex = _openEditors.IndexOf(ActiveEditor);

                _activeEditor = value;

                if (iChangeEditorIndex != -1)
                    _openEditors[iChangeEditorIndex].Selected = false;
                
            }
        }

        #endregion

        #region Open, Close & Select

        public void CloseEditor()
        {
            if (ActiveEditor != null && ActiveEditor.ShowCloseButton)
            {
                _tabcontrol.HideTab(ActiveEditor);
                DocumentManager.ToXml(ActiveEditor.Document, ActiveEditor.Filename);
                _tabcontrol.CloseHidedTabs();

                _openEditors.Remove(ActiveEditor);

                //One more than filebrowser
                if (_openEditors.Count > 1)
                    _openEditors[1].Selected = true;
                else
                    _openEditors[0].Selected = true;
            }
        }

        public void SelectNext()
        {
            if (_openEditors.Count > 1)
                _openEditors[1].Selected = true;
        }
        
        public void SelectPrior()
        {
            if (_openEditors.Count > 1)
                _openEditors[_openEditors.Count-1].Selected = true;
        }

        public IEditorBase GetDocumentByFilename(string filename)
        {
            foreach (IEditorBase d in _openEditors)
            {
                if (d.Filename.Equals(filename))
                    return d;
            }

            return null;
        }

        #endregion



    }
}
