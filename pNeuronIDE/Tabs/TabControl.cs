using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace primeira.pNeuron
{
    public partial class TabControl : UserControl
    {
        public TabControl()
        {
            InitializeComponent();
        }

        public delegate void SelectedPanelsChangeDelegate();
        public event SelectedPanelsChangeDelegate OnSelectedPanelsChange;

        public void AddTab(ITabbedControl document)
        {
            this.SuspendLayout();

            EditorBaseTabButton b = new EditorBaseTabButton();
 
            b.MaximumSize = new Size(200, 40);
            //b.MinimumSize = new Size(60, 40);

            if(document is pDocument)
                ((pDocument)document).MainDisplay.OnSelectedPanelsChange += new pDisplay.SelectedPanelsChangeDelegate(MainDisplay_OnSelectedPanelsChange);

            b.Dock = DockStyle.Left;
            b.Tag = document;

            this.pnDocArea.Controls.Add((Control)document);

            this.pnTabArea.Controls.Add(b);

            b.BringToFront();

            b.Click += new EventHandler(TabSelect_Click);
            TabSelect_Click(b, null);

            b.Font = new Font(SystemFonts.CaptionFont.FontFamily, 13);
            b.ForeColor = Color.DarkGray;

            b.Text = MeasureFolderPath(document.TabCaption, b.Font, new Size(b.Width-10, 20));

            if (document.ShowClose && !pnCloseArea.Visible)
                pnCloseArea.Visible = true;

            this.ResumeLayout(true);
        }

        void MainDisplay_OnSelectedPanelsChange()
        {
            if (OnSelectedPanelsChange != null)
                OnSelectedPanelsChange();
        }

        public static string MeasureFolderPath(string value, Font f, Size Size)
        {
            char[] ss = new char[value.Length];

            value.CopyTo(0, ss, 0, value.Length);

            string s = new string(ss);

            TextRenderer.MeasureText(s, f, Size,
            TextFormatFlags.ModifyString | TextFormatFlags.PathEllipsis);

            return s;
        }

        [Browsable(true)]
        public bool ShowFileBrowser { get; set; }

        void TabSelect_Click(object sender, EventArgs e)
        {
            
            ITabbedControl tab = (ITabbedControl)((Control)sender).Tag;

            ActiveDocument = tab;

            tab.BringToFront();

            foreach (Control c in pnTabArea.Controls)
            {
                if (c != sender)
                    ((EditorBaseTabButton)c).Selected = false;
            }

            ((EditorBaseTabButton)sender).Selected = true;

            pnCloseArea.Visible = tab.ShowClose;
        }

        #region ActiveDocument

        ITabbedControl fActiveDocument = null;
        private List<ITabbedControl> fmDocuments = new List<ITabbedControl>();

        public ITabbedControl ActiveDocument
        {
            get
            {
                return fActiveDocument;
            }
            internal set
            {

                //To control z-order
                fmDocuments.Remove(value);
                fmDocuments.Insert(0, value);

                fActiveDocument = value;
                fActiveDocument.BringToFront();
                pnCloseArea.Visible = (value.ShowClose);
            }
        }

        #endregion

        #region New/Open/Save Document

        public void AddDocument(ITabbedControl document)
        {
            fmDocuments.Add(document);

            

            pnTabArea.Controls.Add((Control)document);

            ActiveDocument = fmDocuments[fmDocuments.Count - 1];

            if (document is pDocument)
            {
                AddTab(document);
            }
            else
                AddTab(document);
        }

        internal void NewOrOpenDocument(bool NewFile)
        {
            pDocument p = new pDocument();

            if (p.LoadFile(NewFile) != DialogResult.OK)
            {
                p.Dispose();
            }
            else AddDocument(p);
        }

        private void p_OnDisplayStatusChanged()
        {
            //status.Items[0].Text = "Status: " + ActiveDocument.MainDisplay.DisplayStatus.ToString().Replace("_", " ");
        }


        #endregion

        private void TabControl_Load(object sender, EventArgs e)
        {
            if (ShowFileBrowser)
            {
                FileBrowser f = new FileBrowser();

                AddDocument(f);
                
                f.SetParentTabControl(this);

            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            ITabbedControl toRemove = ActiveDocument;
            
            foreach (Control c in pnTabArea.Controls)
            {
                //see set_ActiveDocument 
                if (c.Tag == fmDocuments[1])
                {
                    TabSelect_Click(c, null);
                    break;
                }
            }

            fmDocuments.Remove(toRemove);

            if (toRemove is pDocument)
            {
                ((pDocument)toRemove).PrepareToClose();
            }
            
            Control tab = null;
            foreach (Control c in pnTabArea.Controls)
            {
                if (c.Tag == toRemove)
                {
                    tab = c;
                    break;
                }
            }

            if (tab != null)
            {
                pnTabArea.Controls.Remove(tab);
                tab.Dispose();
            }

            ((Control)toRemove).Dispose();

        }

    }
}
