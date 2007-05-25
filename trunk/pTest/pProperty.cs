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
    public class pProperty : DockContent
    {
        public PropertyGrid propertyGrid1;

        public pProperty()
        {

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(292, 273);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // pProperty
            // 
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.propertyGrid1);
            this.Name = "pProperty";
            this.TabText = "Properties";
            this.Text = "Properties";
            this.ResumeLayout(false);

        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {

            ((pNeuronIDE)DockPanel.Parent).ActiveDocument.pDisplay1_OnTreeViewChange(null, pTreeviewRefresh.pFullRefreh  );
            ((pNeuronIDE)DockPanel.Parent).ActiveDocument.pDisplay1.Invalidate(((pPanel)propertyGrid1.SelectedObject).Bounds);

//            if (OnTreeViewChange != null)
  //              OnTreeViewChange(0);
        }
    }
}
