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
    }
}
