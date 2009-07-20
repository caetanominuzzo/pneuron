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

namespace primeira.pNeuron.Editor
{
    public partial class TabControl : UserControl
    {
        public TabControl()
        {
            InitializeComponent();
        }
     
        #region ActiveDocument

        EditorBase fActiveDocument = null;

        private List<EditorBase> fmDocuments = new List<EditorBase>();

        int iChangeDocumentIndex = -1;

        public EditorBase ActiveDocument
        {
            get
            {
                return fActiveDocument;
            }
            internal set
            {
                if (!fmDocuments.Contains(value))
                    fmDocuments.Insert(0, value);
                else
                {
                    //To control z-order
                    fmDocuments.Remove(value);
                    fmDocuments.Insert(0, value);
                }

                iChangeDocumentIndex = fmDocuments.IndexOf(ActiveDocument);

                fActiveDocument = value;
                //fActiveDocument.Selected = true;

                if (iChangeDocumentIndex != -1)
                    fmDocuments[iChangeDocumentIndex].Selected = false;
                
            }
        }

        #endregion

        #region New/Open/Save Document

        private void AddDocument(EditorBase document)
        {

            this.SuspendLayout();

            this.pnDocArea.Controls.Add((Control)document);

            this.pnTabArea.Controls.Add(document.TabButton);

            //Firsts to the left
            document.TabButton.BringToFront();

            document.BringToFront();

            //If not virtual add in recents
            //if ((document.Data.GetDefinition.Options & DocumentDefinitionOptions.Virtual) != DocumentDefinitionOptions.Virtual)
            //    fileBrowser.AddRecent(document.Filename);
            

            document.OnSelected += new EditorBase.SelectedDelegate(TabControl_OnSelected);

            document.Selected = true;

            this.ResumeLayout();

        }

        void TabControl_OnSelected(EditorBase sender)
        {
            ActiveDocument = sender;

            pnCloseArea.Visible = sender.ShowCloseButton;
        }


        public void OpenDocument(DocumentDefinition FileVersion)
        {
            openOrNewDocument(false, FileVersion);
        }

        public void NewDocument(DocumentDefinition FileVersion)
        {
            openOrNewDocument(true, FileVersion);
        }

        private void openOrNewDocument(bool NewFile, DocumentDefinition FileVersion)
        {
            OpenFileDialog s = new OpenFileDialog();

            s.CheckFileExists = false;

            if (NewFile)
                s.FileName = FileManager.GetNewFile(FileVersion, BaseDir);

            s.Filter = EditorManager.GetDialogFilterString();

            s.DefaultExt = FileVersion.Extension;

            s.FilterIndex = EditorManager.GetDialogFilterIndex(FileVersion);

            s.InitialDirectory = BaseDir;

            if (s.ShowDialog() == DialogResult.OK)
            {
                BaseDir = s.InitialDirectory;

                string ss = Path.Combine(BaseDir, s.FileName);

                if (!File.Exists(ss))
                    File.Create(ss).Close();

                LoadDocument(ss);
            }
        }

        private static string BaseDir = @"c:\";

        public string PublicBaseDir
        {
            get { return BaseDir; }
        }

        public EditorBase LoadDocument(string FileName)
        {
            EditorBase res = (EditorBase)EditorManager.GetEditorByFilename(FileName);

            if (res != null)
                AddDocument(res);

            return res;
         
        }

        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {

            Control tab = null;
            foreach (Control c in pnTabArea.Controls)
            {
                if (c.Tag == ActiveDocument)
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

            ActiveDocument.PrepareToClose();
            
            fmDocuments.Remove(ActiveDocument);

            fmDocuments[0].Selected = true;

        }

    }
}
