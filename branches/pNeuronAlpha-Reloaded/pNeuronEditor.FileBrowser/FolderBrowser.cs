using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using pNeuronEditor.Components;

namespace pNeuronEditor.Editors.FileBrowser
{
    public partial class FolderBrowser : UserControl
    {
        private string dirOnEnter = string.Empty;

        public FolderBrowser()
        {
            InitializeComponent();

            this.Enter += new EventHandler(FolderBrowser_Enter);
            this.Leave += new EventHandler(FolderBrowser_Leave);
        }

        void FolderBrowser_Enter(object sender, EventArgs e)
        {
            dirOnEnter = Text;
        }

        void FolderBrowser_Leave(object sender, EventArgs e)
        {
            if (!Directory.Exists(Text))
            {
                Text = dirOnEnter;
            }
            else
                if (Text != dirOnEnter && OnDirectoryChange != null)
                    OnDirectoryChange(Text);
        }

        public new string Text
        {
            get { return borderTextBox1.Text; }
            set { borderTextBox1.Text = value; }
        }

        public delegate void OnDirectoryChangeDelegate(string directoryPath);

        [Browsable(true)]
        public event OnDirectoryChangeDelegate OnDirectoryChange;

    }
}
