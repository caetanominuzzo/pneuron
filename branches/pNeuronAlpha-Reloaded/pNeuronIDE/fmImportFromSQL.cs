using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace primeira.pNeuron
{
    public partial class fmImportFromSQL : Form
    {
        private DataGridView m_datagrid;

        private fmImportFromSQL()
        {
            InitializeComponent();
        }

        public fmImportFromSQL(DataGridView datagrid) : this()
        {
            m_datagrid = datagrid;
        }

        private void btOk_Click(object sender, EventArgs e)
        {

            SqlDataAdapter da = new SqlDataAdapter(txtSQL.Text, txtConn.Text);

            DataSet ds = new DataSet();

            da.SelectCommand.CommandTimeout = 300000;

            da.Fill(ds);

            if(ds.Tables[0].Columns.Count != m_datagrid.ColumnCount)
                throw new Exception("Invalid field count.");

            m_datagrid.DataSource = ds.Tables[0];
        }

    }
}