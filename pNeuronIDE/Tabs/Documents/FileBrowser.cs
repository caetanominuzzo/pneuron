using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace primeira.pNeuron
{
    public partial class FileBrowser : UserControl, ITabbedControl
    {
        public FileBrowser()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            m_file = Image.FromFile(@"C:\Users\caetano.CWIPOA\Pictures\New.bmp");
        }

        private TabControl ParentTabControl;
        private Image m_file;

        public void SetParentTabControl(TabControl Parent)
        {
            ParentTabControl = Parent;
        }


        public bool ShowClose
        {
            get { return false; }
        }

        public string TabCaption
        {
            get { return "New/Open File"; } 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ParentTabControl.NewOrOpenDocument(false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ParentTabControl.NewOrOpenDocument(true);
        }

        private void FileBrowser_Load(object sender, EventArgs e)
        {

            dataGridView2.Rows.Add(
                    new object[] { m_file, "New Neural Network Scratch", "", 0, "" });

            dataGridView2.Rows.Add(
                    new object[] { m_file, "Open Neural Network", "", 0, "" });

            dataGridView2.Rows.Add(
                    new object[] { m_file, "Create Neural Network", "", 0, "" });

            string[] files = FileBrowserRecentFileManager.Get();

            Size s = new Size(dataGridView1.Columns[1].Width, dataGridView1.RowTemplate.Height);
            Font f = dataGridView1.DefaultCellStyle.Font;
            DateTime d = DateTime.Now;

            foreach(string file in files)
            {
                if(File.Exists(file))
                {
                    TimeSpan t = d.Subtract(File.GetLastWriteTime(file));
                    dataGridView1.Rows.Add(
                    new object[] { m_file, TabControl.MeasureFolderPath(file, f, s), LastWrite(t) , (int)t.TotalSeconds, file});
                }
            }

            dataGridView1.Sort(ColOrder, ListSortDirection.Ascending);

            dataGridView1.SelectedRows[0].Selected = false;
            dataGridView2.SelectedRows[0].Selected = false;
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            ((DataGridView)sender).Rows[e.RowIndex].Selected = true;
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            ((DataGridView)sender).Rows[e.RowIndex].Selected = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender == dataGridView1)
                ParentTabControl.AddDocument(new pDocument(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()));
            else
            {
                if (e.RowIndex == 0)
                {
                    File.Create(@"Neural Network.pne");

                    pDocument p = new pDocument();
                    p.Filename = @"Neural Network.pne";

                    ParentTabControl.AddDocument(p);
                }
                else if (e.RowIndex == 1)
                    ParentTabControl.NewOrOpenDocument(false);
                else if (e.RowIndex == 2)
                    ParentTabControl.NewOrOpenDocument(true);
            }
        }

        private string LastWrite(TimeSpan time)
        {
            if (time.TotalDays > 2)
                return string.Format("{0} days ago", (int)time.TotalDays);
            else if (time.TotalHours > 2)
                return string.Format("{0} hours ago", (int)time.TotalHours);
            else if (time.TotalMinutes > 2)
                return string.Format("{0} minutes ago", (int)time.TotalMinutes);
            else 
                return "recently closed";
        }

    }
}
