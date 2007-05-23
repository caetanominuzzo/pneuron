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
    public partial class pNeuronIDE : Form
    {
        public pProperty property = new pProperty();
        public pToolbox toolbox = new pToolbox();
        public pTreeview treeview = new pTreeview();
        public pDocument document = new pDocument();

        public pNeuronIDE()
        {
            InitializeComponent();
            document.Show(dockPanel, DockState.Document);
            
            treeview.Show(dockPanel, DockState.DockRight);
            toolbox.Show(dockPanel, DockState.DockLeft);

            property.Show(dockPanel, DockState.DockRight);
            property.DockTo(treeview.Pane, DockStyle.Bottom, 0);
           
        }
    }
}