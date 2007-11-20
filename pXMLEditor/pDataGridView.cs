using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace primeira.Components
{
    public class pDataGridView : DataGridView
    {
        public pDataGridView()
        {
            this.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.EditMode = DataGridViewEditMode.EditOnF2;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }


        protected override void OnKeyUp(KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.C)
            {

                StringBuilder sb = new StringBuilder();
                foreach(DataGridViewRow r in this.Rows)
                    foreach(DataGridViewColumn c in this.Columns)
                        if (SelectedCells.Contains(r.Cells[c.Name]))
                        {
                            sb.AppendFormat("{0},{1}:{2}\n", c.Index, r.Index, r.Cells[c.Name].Value.ToString());
                        }

                Clipboard.SetText(sb.ToString());
            }
            else if (e.KeyData == Keys.V)
            {

                Clipboard.SetDataObject(
                    this.GetClipboardContent());
                return;

                string clip = Clipboard.GetText();
                string[] lines = clip.Split('\n');

                int minx = int.MaxValue;
                int miny = int.MaxValue;

                Array.Sort(lines);

                foreach(string line in lines)
                {
                    string[] parts = line.Split(':');

                    string[] point = parts[0].Split(',');

                    int x = int.Parse(point[0]);
                    int y = int.Parse(point[1]);

                    string text = parts[1];

                    if (x < minx)
                        minx = x;
                    if (y < miny)
                        miny = y;

                    foreach (DataGridViewRow r in this.Rows)
                        foreach (DataGridViewColumn c in this.Columns)
                            if (SelectedCells.Contains(r.Cells[c.Name]))
                            {
                                if (x - minx == c.Index && y - miny == r.Index)
                                    r.Cells[c.Name].Value = text;

                                break;
                            }
                }


               
                
            }

            base.OnKeyUp(e);
        }
    }
}
