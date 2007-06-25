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

        public pNeuronIDE()
        {


            InitializeComponent();
            fmProperty = new pProperty();

            fmToolbox.Show(dockPanel, DockState.DockLeft);


            fmGroupExplorer.Show(dockPanel, DockState.DockRight);

            fmNetworkExplorer.Show(dockPanel, DockState.DockRight);
            fmNetworkExplorer.DockTo(fmGroupExplorer.Pane, DockStyle.Fill, 0);


            fmProperty.Show(dockPanel, DockState.DockRight);
            fmProperty.DockTo(fmGroupExplorer.Pane, DockStyle.Bottom, 0);

        }

        public bool Modificated
        {
            get { return m_modificated; }
            set { m_modificated = value; }
        }

        public string ProjectFilename
        {
            get { return m_projectFilename; }
        }

        public DialogResult Unload()
        {

            if (Modificated)
            {
                switch (MessageBox.Show("Save changes on " + this.ProjectFilename + "?", "Don't you like to save your work?", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        return DialogResult.Cancel;
                        break;
                    case DialogResult.Yes:
                        Save();
                        break;

                }
            }


            while(fmDocuments.Count>0)
            {
                fmDocuments[0].Close();
            }

            fmNetworkExplorer.treeView1.Nodes.Clear();

            Modificated = false;
            m_projectFilename = "";
            Text = "pNeuron IDE";

            return DialogResult.OK;

        }

        public DialogResult LoadProject()
        {
            if (Modificated)
                Save();

            OpenFileDialog s = new OpenFileDialog();
            s.DefaultExt = ".pnp";
            s.Filter = "pNeuron Network Project (*.pnp)|*.pnp|All files (*.*)|*.*";
            if (s.ShowDialog() == DialogResult.OK)
            {
                m_projectFilename = s.FileName;
                Text = m_projectFilename + " - pNeuron IDE";
                fmNetworkExplorer.AddNode(ProjectFilename);
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
            DataSet ds = new DataSet();

            ds.ReadXml(m_projectFilename);

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                fmNetworkExplorer.AddNode(r["File"].ToString());
            }

            //TODO:Refresh Network Explorer

            return DialogResult.OK;
        }

        public DialogResult Save()
        {
            internalSave();
            Modificated = false;
            return DialogResult.OK;
        }

        private void internalSave()
        {
            DataSet ds = new DataSet(System.IO.Path.GetFileNameWithoutExtension(m_projectFilename));

            DataTable tNeurons = new DataTable("pNeuronNetworkProject");
            tNeurons.Columns.Add("File", typeof(String));

            foreach (string s in fmNetworkExplorer.getFiles())
            {
                if (s.IndexOf('\\') == -1) //Unsaved network
                {
                    foreach (pDocDisplay p in fmDocuments)
                    {
                        if (p.Filename == s)
                        {
                            p.Save();
                        }
                    }
                }
                tNeurons.Rows.Add(s);
            }

            ds.Tables.Add(tNeurons);

            ds.WriteXml(m_projectFilename);
        }

        public void OpenNetwork(string sFilename)
        {
            foreach (pDocDisplay p in fmDocuments)
            {
                if (p.Filename == sFilename)
                {
                    p.Show();
                    return;
                }
            }

            fmDocuments.Add(new pDocDisplay(sFilename));
            ActiveDocument = fmDocuments[fmDocuments.Count - 1];
            ActiveDocument.Show(dockPanel, DockState.Document);
            ActiveDocument.internalLoad(ActiveDocument.Filename);
            ActiveDocument.Modificated = false;
            ActiveDocument.DefaultNamedFile = false;
        }

        public void NewProject()
        {
            DialogResult d = DialogResult.OK;
            if (m_projectFilename != "")
                d = Unload();

            if (d == DialogResult.OK)
            {
                string sFilename = fmNewProject.Show();

                if (sFilename != "")
                {
                    m_projectFilename = sFilename;
                    internalSave();
                    Modificated = false;
                    fmNetworkExplorer.AddNode(ProjectFilename);
                }

                
            }

        }

        public void AddNetwork()
        {
            if (ProjectFilename == "")
            {
                NewProject();
            }

            if (ProjectFilename != "")
            {
                int i = fmDocuments.Count + 1;
                fmDocuments.Add(new pDocDisplay("NeuralNetwork " + i.ToString()));
                ActiveDocument = fmDocuments[fmDocuments.Count - 1];
                ActiveDocument.Show(dockPanel, DockState.Document);
                ActiveDocument.Modificated = true;
                fmNetworkExplorer.AddNode(ActiveDocument.Filename);
                Modificated = true;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //Avoid base
        }

     

        #region ToolStrip

        //FILE
        private void tspAddNetwork_Click(object sender, EventArgs e)
        {
            AddNetwork();

        }

        private void tspNewProject_Click(object sender, EventArgs e)
        {
            NewProject();
            AddNetwork();
        }

        private void tspOpenProject_Click(object sender, EventArgs e)
        {
            LoadProject();
        }

        private void tspSaveProject_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void tspSave_Click(object sender, EventArgs e)
        {
            ActiveDocument.Save();
        }

        private void tspSaveAs_Click(object sender, EventArgs e)
        {
            bool bDefaul = ActiveDocument.DefaultNamedFile;
            string old = ActiveDocument.Filename;

            ActiveDocument.DefaultNamedFile = true;
            if (ActiveDocument.Save() == DialogResult.Cancel)
            {
                ActiveDocument.DefaultNamedFile = bDefaul;
            }

        }

        private void tspUnloadProject_Click(object sender, EventArgs e)
        {
            Unload();
        }


        //VIEW
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

        //TRAIN
        private void trainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pTrain fmTrain = new pTrain();
            fmTrain.net.Initialize(1, 1);
            foreach (pPanel p in ActiveDocument.pDisplay1.pPanels)
            {
                fmTrain.net.Neuron.Add((Neuron)p.Tag);
            }
            fmTrain.Show();
        }

        #endregion

        private void pNeuronIDE_Load(object sender, EventArgs e)
        {
            //Create Enviroment
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\pNeuron Projects"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\pNeuron Projects");
            }
        }





        
    }
}