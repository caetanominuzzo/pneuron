using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using primeira.pNeuron.Editor.Business;

namespace primeira.pNeuron.Editor.Components
{
    public partial class TabControl : UserControl, ITabControl
    {
        public TabControl()
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

            ((Control)editor).BringToFront();
            
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


    }
}
