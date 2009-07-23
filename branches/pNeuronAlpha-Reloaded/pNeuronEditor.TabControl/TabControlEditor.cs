using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using pNeuronEditor.Business;
using pNeuronEditor.Components;

namespace pNeuronEditor.TabControl
{
    public partial class TabControlEditor : EditorBase, ITabControl
    {
        public TabControlEditor(string filename, DocumentBase data)
            : base(filename, data, typeof(TabControlDocument))
        {
            InitializeComponent();
        }


        public void AddTab(IEditorBase editor)
        {
            this.pnDocArea.Controls.Add((Control)editor);

            this.pnTabArea.Controls.Add((TabButton)editor.TabButton);

            editor.OnSelected += new SelectedDelegate(editor_OnSelected);

            //Firsts to the left
            ((TabButton)editor.TabButton).BringToFront();

            this.ResumeLayout();
        }

        void editor_OnSelected(IEditorBase sender)
        {
            pnCloseArea.Visible = sender.ShowCloseButton;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            TabManager.GetInstance().CloseEditor();
        }

        public void HideTab(IEditorBase tab)
        {
            ((Control)tab).Visible = false;
            ((TabButton)tab.TabButton).Visible = false;
        }

        public void CloseHidedTabs()
        {
            foreach (Control c in pnTabArea.Controls)
            {
                if (!c.Visible)
                {
                    pnTabArea.Controls.Remove(c);
                    c.Dispose();
                    break;
                }
            }
        }

        public ITabButton CreateTabButton()
        {
            return new TabButton();
        }

        private void TabControlEditor_Load(object sender, EventArgs e)
        {
            foreach (string file in ((TabControl.TabControlDocument)Document).GetOpenTabsFilename())
            {
                EditorManager.LoadEditor(file);
            }

            string selectedTab = ((TabControl.TabControlDocument)Document).GetSelectedTab();
            if(selectedTab!=null)
                EditorManager.LoadEditor(selectedTab);
        }
    }
}
