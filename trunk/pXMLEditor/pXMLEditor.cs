using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace primeira.Components
{
    public abstract partial class pXMLEditor : UserControl
    {
        public pXMLEditor()
        {
            InitializeComponent();
        }

        public abstract void AddNewItem(string DefaultItemName);

        public abstract void RemoveItem(int Index);

        public abstract void SelectItem(int Index);

        public abstract void ImportItem();

        public abstract void ExportItem();

        public abstract string DefaultItemName { get; }

        private void btNewTrainingSet_Click(object sender, EventArgs e)
        {
            AddNewItem(DefaultItemName);
        }

        private void btRemoveTrainingSet_Click(object sender, EventArgs e)
        {
            RemoveItem(cbTrainingSets.SelectedIndex);
        }

        private void cbTrainingSets_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectItem(cbTrainingSets.SelectedIndex);
        }

        public bool SelectOne()
        {
            if (cbTrainingSets.Items.Count > 0)
            {
                SelectItem(cbTrainingSets.SelectedIndex);

                return true;
            }
            else
            {
                cbTrainingSets.Enabled = false;
                cbTrainingSets.SelectedItem = null;
                btRemoveTrainingSet.Enabled = false;

                return false;
            }
        }

        public void AddExistingItem(object item)
        {
            cbTrainingSets.Items.Add(item);
        }

        private void btImport_Click(object sender, EventArgs e)
        {
            ImportItem();
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            ExportItem();
        }

    }
}
