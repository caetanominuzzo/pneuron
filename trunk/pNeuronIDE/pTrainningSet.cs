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

namespace primeira.pNeuron
{
    public partial class pTrainningSet : pDoc
    {
        public pTrainningSet()
        {
            InitializeComponent();
        }

        public pTrainningSet(string sFileName, pDocDisplay pParentNetwork) : this()
        {
            Filename = sFileName;
            fParentNetwork = pParentNetwork;

            DataTable dt = new DataTable(sFileName);

            foreach(pPanel p in fParentNetwork.pDisplay1.pPanels)
            {
                if(p.NeuronType != NeuronTypes.Hidden)
                {
                    dt.Columns.Add(p.NeuronType.ToString() + " [" +p.Name+"]", typeof(double));
                }
            }

            this.dataGridView1.DataSource = dt;

            
        }

        private pDocDisplay fParentNetwork;
        

        #region Save/Load

        public DialogResult Save()
        {
            if (DefaultNamedFile)
            {
                SaveFileDialog s = new SaveFileDialog();
                s.DefaultExt = ".pts";
                s.FileName = System.IO.Path.GetFileNameWithoutExtension(Filename) + ".pts";
                s.Filter = "pNeuron Trainer Set (*.pts)|*.pts|All files (*.*)|*.*";
//                s.InitialDirectory = Path.GetDirectoryName(Parent.ProjectFilename);
                if (s.ShowDialog() == DialogResult.OK)
                {
                    Filename = s.FileName;
                    internalSave();
                    Modificated = false;
                    DefaultNamedFile = false;
                    return DialogResult.OK;
                }
                else
                {
                    return DialogResult.Cancel;
                }
            }
            else
            {
                internalSave();
                Modificated = false;
                return DialogResult.OK;
            }

        }

        private void internalSave()
        {
            ((DataTable)dataGridView1.DataSource).WriteXml(Filename);
        }

        public DialogResult Load()
        {
            if (Modificated)
                Save();

            OpenFileDialog s = new OpenFileDialog();
            s.DefaultExt = ".pts";
            s.Filter = "pNeuron Training Set (*.pts)|*.pts|All files (*.*)|*.*";
            if (s.ShowDialog() == DialogResult.OK)
            {
                internalLoad(s.FileName);
                Filename = s.FileName;
                Modificated = false;
                DefaultNamedFile = false;
                return DialogResult.OK;
            }
            else
            {
                return DialogResult.Cancel;
            }
        }

        public void internalLoad(string aFilename)
        {
            Filename = aFilename;
            DataTable dt = new DataTable(Filename);
            dt.ReadXml(Filename);
            dataGridView1.DataSource = dt;



        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!QueryOnClose)
            {
                Parent.fmDocuments.Remove(this);
                Parent.fmNetworkExplorer.RemoveNode(this.Filename);
                return;
            }

            if (Modificated)
            {
                switch (MessageBox.Show("Save changes on " + this.Filename + "?", "Don't you like to save your work?", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.No:
                        if (DefaultNamedFile)
                        {
                            Parent.fmDocuments.Remove(this);
                            Parent.fmNetworkExplorer.RemoveNode(this.Filename);
                        }
                        return;
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    case DialogResult.Yes:
                        Save();
                        break;

                }
            }

            if (DefaultNamedFile)
            {
                Parent.fmDocuments.Remove(this);
                Parent.fmNetworkExplorer.RemoveNode(this.Filename);
            }
            Parent.fmDocuments.Remove(this);

        }

        #endregion
    }
}