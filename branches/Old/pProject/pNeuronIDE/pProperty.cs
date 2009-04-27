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
    public class pProperty : DockContent, IpDocks
    {
        public PropertyGrid Property;

        public pProperty()
        {

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Property = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // Property
            // 
            this.Property.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Property.Location = new System.Drawing.Point(0, 0);
            this.Property.Name = "Property";
            this.Property.Size = new System.Drawing.Size(292, 273);
            this.Property.TabIndex = 0;
            this.Property.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // pProperty
            // 
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.Property);
            this.Name = "pProperty";
            this.TabText = "Properties";
            this.Text = "Properties";
            this.ResumeLayout(false);

        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if ((pDocument)Parent.ActiveDocument!=null)
            {
                ((pDocument)Parent.ActiveDocument).pDisplay1_OnTreeViewChange(null, pTreeviewRefresh.pFullRefreh);
                ((pDocument)Parent.ActiveDocument).pDisplay1.Invalidate(((pPanel)Property.SelectedObject).Bounds);
            }

        }



        #region IpDocks Members

        public pNeuronIDE Parent
        {
            get { return ((pNeuronIDE)DockPanel.Parent); }
        }

        #endregion

    }
}
