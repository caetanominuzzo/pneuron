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
    public class pDocument : DockContent
    {
        public pDisplay pDisplay1;

        public pDocument()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }

        private void InitializeComponent()
        {
            this.pDisplay1 = new primeira.pNeuron.pDisplay();
            this.SuspendLayout();
            // 
            // pDisplay1
            // 
            this.pDisplay1.AutoScroll = true;
            this.pDisplay1.AutoScrollHorizontalMaximum = 100;
            this.pDisplay1.AutoScrollHorizontalMinimum = 0;
            this.pDisplay1.AutoScrollHPos = 0;
            this.pDisplay1.AutoScrollVerticalMaximum = 100;
            this.pDisplay1.AutoScrollVerticalMinimum = 0;
            this.pDisplay1.AutoScrollVPos = 0;
            this.pDisplay1.BackColor = System.Drawing.Color.White;
            this.pDisplay1.Bezier = true;
            this.pDisplay1.CtrlKey = false;
            this.pDisplay1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pDisplay1.DisplayStatus = primeira.pNeuron.pDisplay.pDisplayStatus.Idle;
            this.pDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pDisplay1.EnableAutoScrollHorizontal = true;
            this.pDisplay1.EnableAutoScrollVertical = true;
            this.pDisplay1.Location = new System.Drawing.Point(0, 0);
            this.pDisplay1.Name = "pDisplay1";
            this.pDisplay1.ShiftKey = false;
            this.pDisplay1.Size = new System.Drawing.Size(292, 273);
            this.pDisplay1.TabIndex = 0;
            this.pDisplay1.VisibleAutoScrollHorizontal = true;
            this.pDisplay1.VisibleAutoScrollVertical = true;
            this.pDisplay1.OnDisplayStatusChange += new primeira.pNeuron.pDisplay.DisplayStatusChangeDelegate(this.pDisplay1_OnDisplayStatusChange);
            this.pDisplay1.OnSelectedPanelsChange += new primeira.pNeuron.pDisplay.SelectedPanelsChangeDelegate(this.pDisplay1_OnSelectedPanelsChange);
            this.pDisplay1.OnTreeViewChange += new primeira.pNeuron.pDisplay.TreeViewChangeDelegate(this.pDisplay1_OnTreeViewChange);
            // 
            // pDocument
            // 
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.pDisplay1);
            this.KeyPreview = true;
            this.Name = "pDocument";
            this.TabText = "[NeuralNetwork1]";
            this.Text = "[NeuralNetwork1]";
            this.Activated += new System.EventHandler(this.pDocument_Activated);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.pDocument_KeyUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pDocument_KeyDown);
            this.ResumeLayout(false);

        }

        private void pDisplay1_OnDisplayStatusChange()
        {
            ((pNeuronIDE)DockPanel.Parent).status.Items[0].Text = "Status: " + pDisplay1.DisplayStatus.ToString().Replace("_", " ");
        }

        private void pDocument_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.B: pDisplay1.Bezier = !pDisplay1.Bezier;
                    Invalidate();
                    break;
                case Keys.K: //Log ShiftB
                    pDisplay1.Logger.Visible = !pDisplay1.Logger.Visible;
                    break;
                case Keys.Escape:
                    pDisplay1.UnSelect();
                    pDisplay1.DisplayStatus = pDisplay.pDisplayStatus.Idle;
                    break;
                case Keys.L: //Link Mode
                    pDisplay1.DisplayStatus = pDisplay.pDisplayStatus.Linking_Paused;
                    break;
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                case Keys.D0:

                    int iKey = Convert.ToInt16(e.KeyCode.ToString().Replace("D", "")) - 1;
                    if (iKey == -1)
                        iKey = 10;

                    if (pDisplay1.CtrlKey) //Create
                    {
                        if (pDisplay1.SelectedpPanels.Length == 0)
                        {
                            pDisplay1.GroupFree(iKey);
                        }

                        if (!pDisplay1.ShiftKey)
                            pDisplay1.GroupFree(iKey);

                        foreach (pPanel p in pDisplay1.SelectedpPanels)
                        {
                            pDisplay1.Add(p, iKey);
                        }

                    }
                    else
                    {

                        if (!pDisplay1.ShiftKey)
                            pDisplay1.UnSelect();


                        pDisplay1.GroupSelect(iKey);
                    }

                    break;
            }

            if (!e.Shift)
                pDisplay1.ShiftKey = false;

            if (!e.Control)
                pDisplay1.CtrlKey = false;
        }

        private void pDocument_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                pDisplay1.ShiftKey = true;
            }

            if (e.KeyCode == Keys.ControlKey)
            {
                pDisplay1.CtrlKey = true;
            }
        }

        private void pDisplay1_OnSelectedPanelsChange()
        {
            if (pDisplay1.SelectedpPanels.Length == 0)
            {
                ((pNeuronIDE)DockPanel.Parent).property.propertyGrid1.SelectedObject = pDisplay1;
            }
            else
            {
                ((pNeuronIDE)DockPanel.Parent).property.propertyGrid1.SelectedObjects = pDisplay1.SelectedpPanels;
            }
        }

        private void pDisplay1_OnTreeViewChange(int iGroup)
        {

            int i = 0;
            int j = 0;

            foreach (List<pPanel> l in pDisplay1.Groups())
            {
                ((pNeuronIDE)DockPanel.Parent).treeview.treeView1.Nodes[i].Text = "Group " + i.ToString() + " [" + l.Count.ToString() + "]";
                ((pNeuronIDE)DockPanel.Parent).treeview.treeView1.Nodes[i].Nodes.Clear();

                foreach (pPanel p in l)
                {
                    ((pNeuronIDE)DockPanel.Parent).treeview.treeView1.Nodes[i].Nodes.Add(p.Name);
                    j++;
                }

                i++;
            }
        }

        private void pDocument_Activated(object sender, EventArgs e)
        {
            ((pNeuronIDE)DockPanel.Parent).ActiveDocument = this;

            switch(pDisplay1.DisplayStatus)
            {
                case pDisplay.pDisplayStatus.AddNeuron:
                    ((pNeuronIDE)DockPanel.Parent).toolbox.rNeuron.Checked = true;
                    break;
                case pDisplay.pDisplayStatus.Linking_Paused:
                case pDisplay.pDisplayStatus.Linking:
                    ((pNeuronIDE)DockPanel.Parent).toolbox.rSynapse.Checked = true;
                    break;
                default :
                    ((pNeuronIDE)DockPanel.Parent).toolbox.rCursor.Checked = true;
                    break;

            }

            ((pNeuronIDE)DockPanel.Parent).treeview.treeView1.Nodes.Clear();

                ((pNeuronIDE)DockPanel.Parent).property.propertyGrid1.SelectedObject = ((pNeuronIDE)DockPanel.Parent).ActiveDocument.pDisplay1;
                int i = 0;
                foreach (List<pPanel> l in ((pNeuronIDE)DockPanel.Parent).ActiveDocument.pDisplay1.Groups())
                {
                    ((pNeuronIDE)DockPanel.Parent).treeview.treeView1.Nodes.Add("Group " + i.ToString() + " [" + l.Count.ToString() + "]");
                    ((pNeuronIDE)DockPanel.Parent).treeview.treeView1.Nodes[i].ImageIndex = i;
                    i++;
                }
            }


    }
}
