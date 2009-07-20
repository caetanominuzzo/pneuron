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
    public partial class FileBrowserEditor :  EditorBase, IRecentFileManager
    {
        #region Fields

        private Image m_file;

        #endregion

        #region Ctor

        public FileBrowserEditor(string filename, DocumentBase data)
            : base(filename, data, typeof(FileBrowserDocument))
        {
            InitializeComponent();

            this.TabButton.Size = new Size(40, 40);

            this.ShowCloseButton = false;

            this.OnSelected += new SelectedDelegate(FileBrowser_OnSelected);
            
            m_file = Image.FromFile(@"C:\Users\caetano.CWIPOA\Pictures\New.png");

        }

        private void createQuickLaunch()
        {
            dgQuickLauch.Rows.Clear();

            DocumentDefinition[] defs = EditorManager.GetAllDocumentDefinition();

            foreach (DocumentDefinition def in defs)
            {
                if ((def.Options & DocumentDefinitionOptions.Virtual) != DocumentDefinitionOptions.Virtual)
                {
                    dgQuickLauch.Rows.Add(
                            new object[] { m_file, 
                            string.Format("Scratch {0} File ", def.Name),
                            "scratch", 0, "", def });
                }
            }

            foreach (DocumentDefinition def in defs)
            {
                if ((def.Options & DocumentDefinitionOptions.Virtual) != DocumentDefinitionOptions.Virtual)
                {
                    dgQuickLauch.Rows.Add(
                            new object[] { m_file, 
                            string.Format("Create or Open {0} File ", def.Name),
                            "", 0, "", def });
                }
            }

            if (dgQuickLauch.SelectedRows.Count == 1)
                dgQuickLauch.SelectedRows[0].Selected = false;
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

            if (dgRecentFiles.SelectedRows.Count == 1)
                dgRecentFiles.SelectedRows[0].Selected = false;
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
            if (sender == dgRecentFiles)
                DocumentManager.LoadDocument(dgRecentFiles.Rows[e.RowIndex].Cells[4].Value.ToString());
            else
            {
                if(dgQuickLauch.Rows[e.RowIndex].Cells[2].Value == "scratch")
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
    }
}
