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
    public class pNetworkExplorer : DockContent, IpDocks
    {
        public pNetworkExplorer()
        {

            InitializeComponent();


        }

        private ImageList imageList1;
        private IContainer components;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem addTrainerSetToolStripMenuItem;
        private ToolStripMenuItem runToolStripMenuItem;

        public TreeView treeView1;
        #region IpDocks Members

        public pNeuronIDE Parent 
        {
            get { return ((pNeuronIDE)DockPanel.Parent); }
        }

        #endregion

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pNetworkExplorer));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addTrainerSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.ItemHeight = 20;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.ShowRootLines = false;
            this.treeView1.Size = new System.Drawing.Size(292, 273);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTrainerSetToolStripMenuItem,
            this.runToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 48);
            // 
            // addTrainerSetToolStripMenuItem
            // 
            this.addTrainerSetToolStripMenuItem.Name = "addTrainerSetToolStripMenuItem";
            this.addTrainerSetToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.addTrainerSetToolStripMenuItem.Text = "Add Trainer Set";
            this.addTrainerSetToolStripMenuItem.Click += new System.EventHandler(this.addTrainerSetToolStripMenuItem_Click);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.runToolStripMenuItem.Text = "Run";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder.JPG");
            this.imageList1.Images.SetKeyName(1, "funcionalidades.png");
            this.imageList1.Images.SetKeyName(2, "small_cal.gif");
            // 
            // pNetworkExplorer
            // 
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.treeView1);
            this.Name = "pNetworkExplorer";
            this.TabText = "Network Explorer";
            this.Text = "Network Explorer";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        public void AddNode(string sFilename)
        {


            string defaultDir = Path.GetDirectoryName(Parent.ProjectFilename);

            if (sFilename.IndexOf('\\') == -1) //New network added
            {
                treeView1.Nodes[Parent.ProjectFilename].Nodes.Add(sFilename, sFilename, 1, 1);
                treeView1.Nodes[Parent.ProjectFilename].Expand();
                return;
            }

            string thisDir = Path.GetDirectoryName(Path.GetFullPath(sFilename));

            if (defaultDir != thisDir.Substring(0, thisDir.Length))
            {
                //TODO:Want to add in the project dir?
            }

            if (defaultDir == thisDir)
            {
                if (sFilename == Parent.ProjectFilename)
                    treeView1.Nodes.Add(sFilename, Path.GetFileNameWithoutExtension(sFilename), 2, 2);
                else
                    treeView1.Nodes[Parent.ProjectFilename].Nodes.Add(sFilename, Path.GetFileNameWithoutExtension(sFilename), 1, 1);

                treeView1.Nodes[Parent.ProjectFilename].Expand();
            }
            else
            {
                string sDelta = thisDir.Substring(defaultDir.Length + 1);
                string[] aDelta = sDelta.Split(new char[] { '\\' });
                string pDir = defaultDir;
                foreach (string s in aDelta)
                {
                    if (treeView1.Nodes[pDir + "\\" + s] == null)
                    {
                        if (pDir == defaultDir)
                            treeView1.Nodes[Parent.ProjectFilename].Nodes.Add(pDir + "\\" + s, s, 0, 0);
                        else
                            treeView1.Nodes[Parent.ProjectFilename].Nodes[pDir].Nodes.Add(pDir + "\\" + s, s, 0, 0);

                    }

                    pDir += "\\" + s;
                }

                treeView1.Nodes[Parent.ProjectFilename].Nodes[pDir].Nodes.Add(sFilename, Path.GetFileNameWithoutExtension(sFilename), 1, 1);
                treeView1.Nodes[Parent.ProjectFilename].Nodes[pDir].Expand();

            }

           
        }

        public void RemoveNode(string sFilename)
        {

            if (treeView1.Nodes.ContainsKey(sFilename))
                treeView1.Nodes[sFilename].Remove();
            else
            {
                foreach (TreeNode n in treeView1.Nodes)
                    RemoveNode(n, sFilename);
            }
        }

        public void RemoveNode(TreeNode Node, string sFilename)
        {
            if (Node.Nodes.ContainsKey(sFilename))
                Node.Nodes[sFilename].Remove();
            else
            {
                foreach (TreeNode n in Node.Nodes)
                {
                    RemoveNode(n, sFilename);
                }
            }
        }

        public List<string> getFiles(TreeNode Node)
        {
            List<string> s = new List<string>();

            foreach (TreeNode n in Node.Nodes)
            {
                if (n.ImageIndex == 0) //Folder
                {
                    s.AddRange(getFiles(n));
                }
                else s.Add(n.Name);
            }

            return s;
        }

        public List<string> getFiles()
        {
            List<string> s = new List<string>();

            if(treeView1.Nodes.Count>0)
            foreach (TreeNode n in treeView1.Nodes[0].Nodes)
            {
                if (n.ImageIndex == 0) //Folder
                {
                    s.AddRange(getFiles(n));
                }
                else s.Add(n.Name);
            }

            return s;
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e.Node.ImageIndex != 0 && e.Node.ImageIndex!=2) //Folder && Project
                Parent.OpenAny(e.Node.Name);
        }

        private void addTrainerSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Parent.ProjectFilename == "")
            {
                Parent.NewProject();
            }

            if (Parent.ProjectFilename != "")
            {
                int i = Parent.fmDocuments.Count + 1;
                Parent.fmDocuments.Add(new pTrainerSet("TrainerSet " + i.ToString()));
                Parent.ActiveDocument = Parent.fmDocuments[Parent.fmDocuments.Count - 1];
                ((pTrainerSet)Parent.ActiveDocument).Show(Parent.dockPanel, DockState.Document);
                ((pTrainerSet)Parent.ActiveDocument).Modificated = true;
                Parent.fmNetworkExplorer.AddNode(((pTrainerSet)Parent.ActiveDocument).Filename);
                Parent.Modificated = true;
            }
        }

    }
}
