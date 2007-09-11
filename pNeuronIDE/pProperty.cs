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
        private ComboBox comboBox1;
        public PropertyGrid Property;

        public pProperty()
        {

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Property = new System.Windows.Forms.PropertyGrid();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Property
            // 
            this.Property.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Property.Location = new System.Drawing.Point(0, 26);
            this.Property.Name = "Property";
            this.Property.Size = new System.Drawing.Size(424, 350);
            this.Property.TabIndex = 0;
            this.Property.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(0, 1);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(424, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // pProperty
            // 
            this.ClientSize = new System.Drawing.Size(424, 376);
            this.Controls.Add(this.comboBox1);
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

        public new pNeuronIDE Parent
        {
            get { return ((pNeuronIDE)DockPanel.Parent); }
        }

        #endregion

    }
}
