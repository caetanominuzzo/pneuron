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
    public partial class pToolbox : DockContent, IpDocks
    {
        public pToolbox()
        {
            InitializeComponent();
        }

        private void rCursor_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == rCursor && rCursor.Checked)
            {
                if(Parent.ThereIsAnActiveDocument())
                    Parent.ActiveDocument.MainDisplay.DisplayStatus = pDisplay.pDisplayStatus.Idle;
                return;
            }

            if (((RadioButton)sender).Checked) //To pass here just one time.
            {

                if (Parent.ActiveDocument != null)
                {
                    if (rSynapse.Checked)
                        Parent.ActiveDocument.MainDisplay.DisplayStatus = pDisplay.pDisplayStatus.Linking_Paused;

                    if (rNeuron.Checked)
                        Parent.ActiveDocument.MainDisplay.DisplayStatus = pDisplay.pDisplayStatus.Add_Neuron;

                    if (rRemove.Checked)
                        Parent.ActiveDocument.MainDisplay.DisplayStatus = pDisplay.pDisplayStatus.Remove_Neuron;
                }


            }

        }

        public void SetToolSet(pDocument aActiveDocument)
        {
            if(aActiveDocument == null)
            {
                rCursor.Enabled = true;
                rCursor.Checked = true;

                rNeuron.Enabled = false;
                rSynapse.Enabled = false;
                rRemove.Enabled = false;
            }
            else 
            {
                rCursor.Enabled = true;
                rCursor.Checked = true;

                rNeuron.Enabled = true;
                rSynapse.Enabled = true;
                rRemove.Enabled = true;

                switch (aActiveDocument.MainDisplay.DisplayStatus)
                {

                    case pDisplay.pDisplayStatus.Add_Neuron:
                        rNeuron.Checked = true;
                        break;
                    case pDisplay.pDisplayStatus.Linking:
                    case pDisplay.pDisplayStatus.Linking_Paused:
                        rSynapse.Checked = true;
                        break;
                    case pDisplay.pDisplayStatus.Remove_Neuron:
                        rRemove.Checked = true;
                        break;
                    default:
                        rCursor.Checked = true;
                        break;
                }
            }
        }

        #region IpDocks Members

        private pNeuronIDE fParent;

        public new pNeuronIDE Parent
        {
            get { return (pNeuronIDE)DockPanel.Parent; }
        }

        #endregion



       
    }
}
