using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace primeira.pNeuron
{
    public class pGroupExplorer : DockContent, IpDocks
    {
        public ListView treeView1;
    
        public pGroupExplorer()
        {

            InitializeComponent();

        }

        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("All", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Group 1", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Group 2", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Group 3", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Group 4", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("Group 5", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup7 = new System.Windows.Forms.ListViewGroup("Group 6", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup8 = new System.Windows.Forms.ListViewGroup("Group 7", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup9 = new System.Windows.Forms.ListViewGroup("Group 8", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup10 = new System.Windows.Forms.ListViewGroup("Group 9", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup11 = new System.Windows.Forms.ListViewGroup("Group 10", System.Windows.Forms.HorizontalAlignment.Left);
            this.treeView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup1.Header = "All";
            listViewGroup1.Name = "listViewGroup11";
            listViewGroup2.Header = "Group 1";
            listViewGroup2.Name = "listViewGroup1";
            listViewGroup3.Header = "Group 2";
            listViewGroup3.Name = "listViewGroup2";
            listViewGroup4.Header = "Group 3";
            listViewGroup4.Name = "listViewGroup3";
            listViewGroup5.Header = "Group 4";
            listViewGroup5.Name = "listViewGroup4";
            listViewGroup6.Header = "Group 5";
            listViewGroup6.Name = "listViewGroup5";
            listViewGroup7.Header = "Group 6";
            listViewGroup7.Name = "listViewGroup6";
            listViewGroup8.Header = "Group 7";
            listViewGroup8.Name = "listViewGroup7";
            listViewGroup9.Header = "Group 8";
            listViewGroup9.Name = "listViewGroup8";
            listViewGroup10.Header = "Group 9";
            listViewGroup10.Name = "listViewGroup9";
            listViewGroup11.Header = "Group 10";
            listViewGroup11.Name = "listViewGroup10";
            this.treeView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4,
            listViewGroup5,
            listViewGroup6,
            listViewGroup7,
            listViewGroup8,
            listViewGroup9,
            listViewGroup10,
            listViewGroup11});
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(292, 273);
            this.treeView1.TabIndex = 0;
            this.treeView1.UseCompatibleStateImageBehavior = false;
            this.treeView1.View = System.Windows.Forms.View.SmallIcon;
            this.treeView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView1_KeyDown);
            this.treeView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.treeView1_ItemSelectionChanged);
            this.treeView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treeView1_KeyUp);
            // 
            // pGroupExplorer
            // 
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.treeView1);
            this.Name = "pGroupExplorer";
            this.TabText = "Group Explorer";
            this.Text = "Group Explorer";
            this.ResumeLayout(false);

        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            

        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            foreach (pPanel p in ((pDocDisplay)Parent.ActiveDocument).pDisplay1.SelectedpPanels)
            {

                int i = p.Groups;

                treeView1.SelectedItems.Clear();


                foreach (ListViewItem t in (treeView1.Groups[i].Items))
                {
                    if (((pPanel)t.Tag) == p)
                    {
                        t.Selected = true;
                    }
                }
            }

            foreach (ListViewItem lvi in treeView1.Items)
            {
                if(treeView1.Items[lvi.Index].Selected != ((pPanel)lvi.Tag).Selected)
                treeView1.Items[lvi.Index].Selected = ((pPanel)lvi.Tag).Selected;
            }

        }

        private void treeView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (((pPanel)e.Item.Tag).Selected != e.IsSelected)
            {
                ((pPanel)e.Item.Tag).Selected = e.IsSelected;
                ((pDocDisplay)Parent.ActiveDocument).pDisplay1.Invalidate(((pPanel)e.Item.Tag).Bounds);
            }


        }

        private void treeView1_KeyDown(object sender, KeyEventArgs e)
        {
            ((pDocDisplay)Parent.ActiveDocument).pDocument_KeyDown(sender, e);
        }

        private void treeView1_KeyUp(object sender, KeyEventArgs e)
        {
            ((pDocDisplay)Parent.ActiveDocument).pDocument_KeyUp(sender, e);
        }

        public bool Contains(ListViewGroup g, pPanel p)
        {
            foreach (ListViewItem l in g.Items)
            {
                if (((pPanel)l.Tag) == p)
                    return true;
            }
            return false;
        }

        #region IpDocks Members

        public pNeuronIDE Parent
        {
            get { return ((pNeuronIDE)DockPanel.Parent); }
        }

        #endregion

    }
}
