using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace primeira.pXMLEditor
{
    public class pXMLEditor : Panel
    {
        private DataSet fDataSet;

        public pXMLEditor(DataSet ds)
        {
            fDataSet = ds;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }
}
