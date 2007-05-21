using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace primeira.pNeuron
{



    public partial class pNeuronIDE : UserControl
    {
        public pNeuronIDE()
        {
            InitializeComponent();
            toolBox1.AddTab("Neuron");
            toolBox1[0].AddItem("Cursor");
            toolBox1[0].AddItem("Neuron");
            toolBox1[0].AddItem("Synapse");
        }

        private void pDisplay1_OnDisplayStatusChange()
        {
            statusStrip1.Items[0].Text = "Status: " + pDisplay1.DisplayStatus.ToString().Replace("_", " ");
        }

        private void pDisplay1_OnSelectedPanelsChange()
        {

            if (pDisplay1.SelectedpPanels.Length == 0)
            {
                propertyGrid1.SelectedObject = pDisplay1;
            }
            else
            {
                propertyGrid1.SelectedObjects = pDisplay1.SelectedpPanels;
            }


        }

        private void toolBox1_Click(object sender, EventArgs e)
        {
            switch (toolBox1.SelectedTab.SelectedItem.Caption)
            {
                case "Neuron": pDisplay1.DisplayStatus = pDisplay.pDisplayStatus.AddNeuron;
                    break;
                case "Cursor": pDisplay1.DisplayStatus = pDisplay.pDisplayStatus.Idle;
                    break;
                case "Synapse": pDisplay1.DisplayStatus = pDisplay.pDisplayStatus.Linking_Paused;
                    break;
                    
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ((Form)Parent).KeyPreview = true;
            ((Form)Parent).KeyUp += new KeyEventHandler(Parent_KeyUp);
            ((Form)Parent).KeyDown += new KeyEventHandler(Parent_KeyDown);
        }

        void Parent_KeyUp(object sender, KeyEventArgs e)
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

                    int iKey = Convert.ToInt16(e.KeyCode.ToString().Replace("D", ""))-1;
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

        void Parent_KeyDown(object sender, KeyEventArgs e)
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

        private void pDisplay1_OnTreeViewChange(int iKey)
        {
            treeView1.Nodes.Clear();

            int i=0;
            int j = 0;
            foreach (List<pPanel> l in pDisplay1.Groups())
            {
                treeView1.Nodes.Add("Group " + i.ToString() + "["+l.Count.ToString());
                treeView1.Nodes[i].ImageIndex = i;
                foreach (pPanel p in l)
                {
                    treeView1.Nodes[i].Nodes.Add("Neuron " + j.ToString());
                    j++;
                }

                i++;
            }
        }


    }
}
