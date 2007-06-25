using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using primeira.pNeuron.Core;

namespace primeira.pNeuron
{
    public partial class pNeuronIDE : Form
    {
        public pProperty fmProperty;
        public pToolbox fmToolbox = new pToolbox();
        public pGroupExplorer fmGroupExplorer = new pGroupExplorer();
        public pNetworkExplorer fmNetworkExplorer = new pNetworkExplorer();
        public List<pDocDisplay> fmDocuments = new List<pDocDisplay>();
        public pDocDisplay ActiveDocument;

        private string m_projectFilename = "";
        private bool m_modificated = false;

        public bool Modificated
        {
            get { return m_modificated; }
            set
            {
                if (value != m_modificated)
                {
                    if (!value)
                    {
                        this.Text = this.Text.Substring(0, Text.Length - 2) + "";
                    }
                    else
                    {
                        this.Text = this.Text.Substring(0, Text.Length - 1) + "*";
                    }
                    m_modificated = value;
                }

            }
        }

        public string ProjectFilename
        {
            get { return m_projectFilename; }
        }

        public void Unload()
        {
            foreach(pDocDisplay p in fmDocuments)
            {
                p.Close();
            }

            fmNetworkExplorer.treeView1.Nodes.Clear();

            Modificated = false;
            m_projectFilename = "";

        }

        public DialogResult Load()
        {
            if (Modificated)
                Save();

            OpenFileDialog s = new OpenFileDialog();
            s.DefaultExt = ".pnp";
            s.Filter = "pNeuron Network Project (*.pnp)|*.pnp|All files (*.*)|*.*";
            if (s.ShowDialog() == DialogResult.OK)
            {
                m_projectFilename = s.FileName;
                internalLoad();
                
                Modificated = false;

                //TODO:Refresh Network Explorer

                

                return DialogResult.OK;
            }
            else
            {
                return DialogResult.Cancel;
            } 

        }

        private DialogResult internalLoad()
        {
            DataTable dt = new DataTable();

            dt.ReadXml(m_projectFilename);

            //TODO:Refresh Network Explorer

            return DialogResult.OK;
        }

        public DialogResult Save()
        {
            //if (m_defaultNamedProjectFile)
            //{
            //    SaveFileDialog s = new SaveFileDialog();
            //    s.DefaultExt = ".pnp";
            //    s.FileName = System.IO.Path.GetFileNameWithoutExtension(m_projectFilename) + ".pnp";
            //    s.Filter = "pNeuron Network Project (*.pnp)|*.pnp|All files (*.*)|*.*";
            //    if (s.ShowDialog() == DialogResult.OK)
            //    {
            //        m_projectFilename = s.FileName;
            //        internalSave();
            //        Modificated = false;
            //        return DialogResult.OK;
            //    }
            //    else
            //    {
            //        return DialogResult.Cancel;
            //    }
            //}
            //else
            //{
                internalSave();
                Modificated = false;
                return DialogResult.OK;
            //}
        }

        private void internalSave()
        {
            DataSet ds = new DataSet(System.IO.Path.GetFileNameWithoutExtension(m_projectFilename));

            DataTable tNeurons = new DataTable("pNeuronNetworkProject");
            tNeurons.Columns.Add("File", typeof(String));

            ds.Tables.Add(tNeurons);

            ds.WriteXml(m_projectFilename);
        }



        public pNeuronIDE()
        {
            

            InitializeComponent();
            fmProperty = new pProperty();

            fmToolbox.Show(dockPanel, DockState.DockLeft);


            fmGroupExplorer.Show(dockPanel, DockState.DockRight);
            
            fmNetworkExplorer.Show(dockPanel, DockState.DockRight);
            fmNetworkExplorer.DockTo(fmGroupExplorer.Pane, DockStyle.Fill,0);
            

            fmProperty.Show(dockPanel, DockState.DockRight);
            fmProperty.DockTo(fmGroupExplorer.Pane, DockStyle.Bottom, 0);
           
        }

        private void newNeuralNetworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = fmDocuments.Count+1;
            fmDocuments.Add(new pDocDisplay("NeuralNetwork " + i.ToString()));
            ActiveDocument = fmDocuments[fmDocuments.Count - 1];
            ActiveDocument.Show(dockPanel, DockState.Document);

            fmNetworkExplorer.treeView1.Nodes.Add(ActiveDocument.Text);
        }

        private void propertyWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmProperty.Show();
        }

        private void networkExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmGroupExplorer.Show();
        }

        private void toolBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmToolbox.Show();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //base.OnFormClosing(e);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
            ActiveDocument.Save();

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int i = fmDocuments.Count + 1;
            fmDocuments.Add(new pDocDisplay("NeuralNetwork " + i.ToString()));
            ActiveDocument = fmDocuments[fmDocuments.Count - 1];
            ActiveDocument.Show(dockPanel, DockState.Document);

            if (ActiveDocument.Load() != DialogResult.OK)
                ActiveDocument.Close();
        }

        private void trainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pTrain fmTrain = new pTrain();
            fmTrain.net.Initialize(1, 1);
            foreach(pPanel p in ActiveDocument.pDisplay1.pPanels)
            {
                fmTrain.net.Neuron.Add((Neuron)p.Tag);
            }
            fmTrain.Show();
        }

        private void dataEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void projectFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_projectFilename != "")
                Unload();

            Load();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Unload();
        }

        private void dockPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripMenuItem3_Click_1(object sender, EventArgs e)
        {
            if (m_projectFilename != "")
                Unload();

            SaveFileDialog s = new SaveFileDialog();
            s.DefaultExt = ".pnp";
            s.FileName = System.IO.Path.GetFileNameWithoutExtension(m_projectFilename) + ".pnp";
            s.Filter = "pNeuron Network Project (*.pnp)|*.pnp|All files (*.*)|*.*";
            if (s.ShowDialog() == DialogResult.OK)
            {
                m_projectFilename = s.FileName;
                internalSave();
                Modificated = false;
            }
            else
            {
            }


        }
    }
}