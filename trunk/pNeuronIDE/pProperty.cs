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
        public ComboBox cbItems;
        public PropertyGrid Property;

        public pProperty()
        {

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Property = new System.Windows.Forms.PropertyGrid();
            this.cbItems = new System.Windows.Forms.ComboBox();
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
            // cbItems
            // 
            this.cbItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItems.FormattingEnabled = true;
            this.cbItems.Location = new System.Drawing.Point(0, 1);
            this.cbItems.Name = "cbItems";
            this.cbItems.Size = new System.Drawing.Size(424, 21);
            this.cbItems.TabIndex = 1;
            this.cbItems.SelectedIndexChanged += new System.EventHandler(this.cbItems_SelectedIndexChanged);
            // 
            // pProperty
            // 
            this.ClientSize = new System.Drawing.Size(424, 376);
            this.Controls.Add(this.cbItems);
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
                if (Property.SelectedObject is pPanel)
                    ((pDocument)Parent.ActiveDocument).MainDisplay.Invalidate(((pPanel)Property.SelectedObject).Bounds);
            }

        }

        #region IpDocks Members

        public new pNeuronIDE Parent
        {
            get { return ((pNeuronIDE)DockPanel.Parent); }
        }

        #endregion

        private void cbItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbItems.SelectedItem.ToString() == Parent.ActiveDocument.MainDisplay.Net.ToString())
            {
                Property.SelectedObject = Parent.ActiveDocument.MainDisplay.Net;
            }
            else
            {
                foreach (pNeuron.Core.Neuron n in Parent.ActiveDocument.MainDisplay.Net.Neuron)
                {
                    if (cbItems.SelectedText == n.ToString())
                    {
                        Property.SelectedObject = n;
                    }
                }
            }


        }

    }
}
