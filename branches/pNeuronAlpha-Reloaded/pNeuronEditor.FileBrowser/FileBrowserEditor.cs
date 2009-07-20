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
    public partial class FileBrowserEditor :  EditorBase
    {
        #region Fields

        private Image m_file;

        private TabControl ParentTabControl;

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
                            "", 0, "", def });
                }
            }

            if (dgQuickLauch.SelectedRows.Count == 1)
                dgQuickLauch.SelectedRows[0].Selected = false;
        }

        private void createRecent()
        {
            dgRecentFiles.Rows.Clear();

            string[] files = ((FileBrowserDocument)Data).Recent;

            Size s = new Size(dgRecentFiles.Columns[1].Width, dgRecentFiles.RowTemplate.Height);
            Font f = dgRecentFiles.DefaultCellStyle.Font;
            DateTime d = DateTime.Now;

            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    //TimeSpan t = d.Subtract(File.GetLastWriteTime(file));
                    //dgRecentFiles.Rows.Add(
                    //new object[] { m_file, file, FileManager.LastWrite(t), (int)t.TotalSeconds, file, NeuralNetworkDocument.DocumentDefinition });
                }
            }

            dgRecentFiles.Sort(ColOrder, ListSortDirection.Ascending);

            if (dgRecentFiles.SelectedRows.Count == 1)
                dgRecentFiles.SelectedRows[0].Selected = false;
        }

        #endregion

        #region Event Handlers


        private void FileBrowser_OnSelected(EditorBase sender)
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
                ParentTabControl.LoadDocument(dgRecentFiles.Rows[e.RowIndex].Cells[4].Value.ToString());
            else
            {
                if (e.RowIndex == 0 || e.RowIndex == 1)
                {
                    string s = FileManager.GetNewFile((DocumentDefinition)dgQuickLauch.Rows[e.RowIndex].Cells[5].Value, ParentTabControl.PublicBaseDir);
                    File.Create(s).Close();
                    ParentTabControl.LoadDocument(s);
                }
                else
                {
                    ParentTabControl.NewDocument((DocumentDefinition)dgQuickLauch.Rows[e.RowIndex].Cells[5].Value);
                }
            }
        }

        #endregion

        #region Methods

        public void SetParentTabControl(TabControl parent)
        {
            ParentTabControl = parent;
        }

        public void AddRecent(string filename)
        {
            ((FileBrowserDocument)Data).AddRecent(filename);

            Changed();
        }

        #endregion
    }
}
