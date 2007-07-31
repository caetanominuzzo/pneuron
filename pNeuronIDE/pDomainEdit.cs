using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using primeira.pNeuron.Core;
using System.IO;
using System.Threading;

namespace primeira.pNeuron
{
    public class pDomainEdit : DockContent
    {
        private string fDomainDir = "";

        public pDomainEdit()
        {
            InitializeComponent();
            fDomainDir = Path.GetDirectoryName(Application.ExecutablePath) + @"\Domain\";
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pDomainEdit));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cbDomain = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btNewDomain = new System.Windows.Forms.ToolStripButton();
            this.btRemovDomain = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btImport = new System.Windows.Forms.ToolStripButton();
            this.btExport = new System.Windows.Forms.ToolStripButton();
            this.dgDomain = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDomain)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowDrop = true;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbDomain,
            this.toolStripSeparator1,
            this.btNewDomain,
            this.btRemovDomain,
            this.toolStripSeparator2,
            this.btImport,
            this.btExport});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(570, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cbDomain
            // 
            this.cbDomain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDomain.Name = "cbDomain";
            this.cbDomain.Size = new System.Drawing.Size(121, 25);
            this.cbDomain.SelectedIndexChanged += new System.EventHandler(this.cbDomain_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btNewDomain
            // 
            this.btNewDomain.Image = ((System.Drawing.Image)(resources.GetObject("btNewDomain.Image")));
            this.btNewDomain.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btNewDomain.Name = "btNewDomain";
            this.btNewDomain.Size = new System.Drawing.Size(48, 22);
            this.btNewDomain.Text = "&New";
            // 
            // btRemovDomain
            // 
            this.btRemovDomain.Image = ((System.Drawing.Image)(resources.GetObject("btRemovDomain.Image")));
            this.btRemovDomain.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRemovDomain.Name = "btRemovDomain";
            this.btRemovDomain.Size = new System.Drawing.Size(66, 22);
            this.btRemovDomain.Text = "Remove";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btImport
            // 
            this.btImport.Image = ((System.Drawing.Image)(resources.GetObject("btImport.Image")));
            this.btImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btImport.Name = "btImport";
            this.btImport.Size = new System.Drawing.Size(59, 22);
            this.btImport.Text = "Import";
            // 
            // btExport
            // 
            this.btExport.Image = ((System.Drawing.Image)(resources.GetObject("btExport.Image")));
            this.btExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(59, 22);
            this.btExport.Text = "Export";
            // 
            // dgDomain
            // 
            this.dgDomain.BackgroundColor = System.Drawing.Color.White;
            this.dgDomain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDomain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDomain.Location = new System.Drawing.Point(0, 25);
            this.dgDomain.Margin = new System.Windows.Forms.Padding(0);
            this.dgDomain.Name = "dgDomain";
            this.dgDomain.Size = new System.Drawing.Size(570, 360);
            this.dgDomain.TabIndex = 8;
            // 
            // pDomainEdit
            // 
            this.ClientSize = new System.Drawing.Size(570, 385);
            this.Controls.Add(this.dgDomain);
            this.Controls.Add(this.toolStrip1);
            this.Name = "pDomainEdit";
            this.ShowInTaskbar = false;
            this.TabText = "Domains";
            this.Load += new System.EventHandler(this.pDomainEdit_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDomain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private ToolStrip toolStrip1;
        private ToolStripComboBox cbDomain;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btNewDomain;
        private ToolStripButton btRemovDomain;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btImport;
        private DataGridView dgDomain;
        private ToolStripButton btExport;

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //To avoid default behavior
            //base.OnFormClosing(e);
        }

        private void pDomainEdit_Load(object sender, EventArgs e)
        {

            string[] files = Directory.GetFiles(fDomainDir, "*.xml");
            foreach (string file in files)
                cbDomain.Items.Add(Path.GetFileNameWithoutExtension(file));

            if(cbDomain.Items.Count>0)
                cbDomain.SelectedIndex = 0;

        }

        private void cbDomain_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dt = new DataSet();
            dt.ReadXml(fDomainDir + cbDomain.Text + ".xml");
            dgDomain.DataSource = dt.Tables[0];
        }

    }
}
