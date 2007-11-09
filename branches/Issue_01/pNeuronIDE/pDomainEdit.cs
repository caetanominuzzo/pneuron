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
        private pXMLEditor.pXMLEditor pXMLEditor1;
        private string fDomainDir = "";

        public pDomainEdit()
        {
            InitializeComponent();
            fDomainDir = Path.GetDirectoryName(Application.ExecutablePath) + @"\Domain\";
        }

        private void InitializeComponent()
        {
            this.pXMLEditor1 = new pXMLEditor.pXMLEditor();
            this.SuspendLayout();
            // 
            // pXMLEditor1
            // 
            this.pXMLEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pXMLEditor1.Location = new System.Drawing.Point(0, 0);
            this.pXMLEditor1.Name = "pXMLEditor1";
            this.pXMLEditor1.Size = new System.Drawing.Size(570, 385);
            this.pXMLEditor1.TabIndex = 0;
            // 
            // pDomainEdit
            // 
            this.ClientSize = new System.Drawing.Size(570, 385);
            this.Controls.Add(this.pXMLEditor1);
            this.Name = "pDomainEdit";
            this.ShowInTaskbar = false;
            this.TabText = "Domains";
            this.Load += new System.EventHandler(this.pDomainEdit_Load);
            this.ResumeLayout(false);

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //To avoid default behavior
            //base.OnFormClosing(e);
        }

        private void pDomainEdit_Load(object sender, EventArgs e)
        {

            //string[] files = Directory.GetFiles(fDomainDir, "*.xml");
            //foreach (string file in files)
            //    cbDomain.Items.Add(Path.GetFileNameWithoutExtension(file));

            //if(cbDomain.Items.Count>0)
            //    cbDomain.SelectedIndex = 0;

        }

        private void cbDomain_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataSet dt = new DataSet();
            //dt.ReadXml(fDomainDir + cbDomain.Text + ".xml");
            //dgDomain.DataSource = dt.Tables[0];
        }

    }
}
