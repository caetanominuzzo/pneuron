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

namespace pNeuronEditor
{
    public partial class FileBrowserEditor :  EditorBase, IRecentFileManager
    {
        #region Fields

        private Image m_file;

        #endregion

        #region Ctor

        public FileBrowserEditor(string filename, DocumentBase data)
            : base("File Tab", data, typeof(FileBrowserDocument))
        {
            InitializeComponent();

            ((TabButton)this.TabButton).Size = new Size(40, 40);

            this.ShowCloseButton = false;

            this.OnSelected += new SelectedDelegate(FileBrowser_OnSelected);

            this.TabButton.SelectedImage = Image.FromFile(@"C:\Users\caetano.CWIPOA\Pictures\tab_selected_N.png");
            this.TabButton.UnselectedImage = Image.FromFile(@"C:\Users\caetano.CWIPOA\Pictures\tab_unselected_N.png");
            
            m_file = Image.FromFile(@"C:\Users\caetano.CWIPOA\Pictures\New24.png");

        }

        private void createQuickLaunch()
        {
            dgQuickLauch.Rows.Clear();

            DocumentDefinition[] defs = EditorManager.GetAllDocumentDefinition();

            foreach (DocumentDefinition def in defs)
            {
                if ((def.Options & DocumentDefinitionOptions.Virtual) != DocumentDefinitionOptions.Virtual)
                {
                    int i = dgQuickLauch.Rows.Add(
                            new object[] { m_file, 
                            string.Format("Draft {0} File ", def.Name),
                            "draft", 0, "", def });

                    dgQuickLauch.Rows[i].Selected = false;
                }
            }

            foreach (DocumentDefinition def in defs)
            {
                if ((def.Options & DocumentDefinitionOptions.Virtual) != DocumentDefinitionOptions.Virtual)
                {
                    int i = dgQuickLauch.Rows.Add(
                            new object[] { m_file, 
                            string.Format("Open or Create {0} File ", def.Name),
                            "", 0, "", def });

                    dgQuickLauch.Rows[i].Selected = false;
                }
            }


        }

        private void createRecent()
        {
            dgRecentFiles.Rows.Clear();

            string[] files = ((FileBrowserDocument)Document).Recent;

            Size s = new Size(dgRecentFiles.Columns[1].Width, dgRecentFiles.RowTemplate.Height);
            Font f = dgRecentFiles.DefaultCellStyle.Font;
            DateTime d = DateTime.Now;

            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    TimeSpan t = d.Subtract(File.GetLastWriteTime(file));
                    dgRecentFiles.Rows.Add(
                    new object[] { m_file, file, FileManager.LastWrite(t), (int)t.TotalSeconds, file, null });
                }
            }

            dgRecentFiles.Sort(ColOrder, ListSortDirection.Ascending);

          
        }

        #endregion

        #region Event Handlers

        private void FileBrowser_OnSelected(IEditorBase sender)
        {
            createQuickLaunch();

            createRecent();
        }

        #endregion

        #region DataGrids Event Handlers

        private void dataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            ((DataGridView)sender).Rows[e.RowIndex].Selected = true;
        }

        private void dataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            ((DataGridView)sender).Rows[e.RowIndex].Selected = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {   
            if(sender == dgDirFiles)
            {
                DocumentManager.LoadDocument(dgDirFiles.Rows[e.RowIndex].Cells[4].Value.ToString());
            }
            else if (sender == dgRecentFiles)
                DocumentManager.LoadDocument(dgRecentFiles.Rows[e.RowIndex].Cells[4].Value.ToString());
            else
            {
                if(dgQuickLauch.Rows[e.RowIndex].Cells[2].Value.ToString() == "draft")
                {
                    string s = FileManager.GetNewFile((DocumentDefinition)dgQuickLauch.Rows[e.RowIndex].Cells[5].Value, DocumentManager.BaseDir);
                    File.Create(s).Close();
                    DocumentManager.LoadDocument(s);
                }
                else
                {
                    DocumentManager.NewDocument((DocumentDefinition)dgQuickLauch.Rows[e.RowIndex].Cells[5].Value);
                }
            }
        }

        #endregion

        #region Methods

        public void AddRecent(string filename)
        {
            ((FileBrowserDocument)Document).AddRecent(filename);

            Changed();
        }

        public string[] GetRecent()
        {
            return ((FileBrowserDocument)Document).Recent;
        }

        #endregion

        private void folderBrowser1_OnDirectoryChange(string directoryPath)
        {
            dgDirFiles.Rows.Clear();

            DocumentDefinition[] defs = EditorManager.GetAllDocumentDefinition();
            string[] files;
            DateTime d = DateTime.Now;

            foreach(DocumentDefinition def in defs)
            {
                files = Directory.GetFiles(directoryPath,"*"+ def.Extension);

                foreach (string file in files)
                {

                    TimeSpan t = d.Subtract(File.GetLastWriteTime(file));
                    dgDirFiles.Rows.Add(
                    new object[] { m_file, file, FileManager.LastWrite(t), (int)t.TotalSeconds, file, null });
                }

            }


        }

        private void FileBrowserEditor_Load(object sender, EventArgs e)
        {
            if (dgRecentFiles.SelectedRows.Count == 1)
                dgRecentFiles.SelectedRows[0].Selected = false;

            if (dgQuickLauch.SelectedRows.Count == 1)
                dgQuickLauch.SelectedRows[0].Selected = false;
        }
    }
}
