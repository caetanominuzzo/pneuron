using System;
using System.Collections.Generic;
using System.Text;
using primeira.Components;
using System.Windows.Forms;
using System.Data;
using System.Globalization;

namespace primeira.pNeuron
{
    public class pTrainingSetEditor : pXMLEditor
    {

        private pDocument m_parentDocument;

        public pTrainingSetEditor() : base()
        {

        }

        public void SetParent(pDocument ParentDocument)
        {
            m_parentDocument = ParentDocument;
        }

        public override void AddNewItem(string DefaultItemName)
        {
            AddItem(DefaultItemName, m_parentDocument.Filename, m_parentDocument.MainDisplay.pPanels);
        }

        public void AddItem(string DefaultItemName, string pNeFilename, List<pPanel> pPanels)
        {

            using (fmTrainingSetName f = new fmTrainingSetName(DefaultItemName))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    foreach (object o in cbTrainingSets.Items)
                    {
                        if (o.ToString() == f.txtName.Text)
                        {
                            pMessage.Error("Invalid duplicated name.");
                            AddItem(f.txtName.Text, pNeFilename, pPanels);
                            return;
                        }
                    }

                    pTrainingSet fm = m_parentDocument.AddTrainingSet(new pTrainingSet(pPanels, f.txtName.Text, pNeFilename));
                    
                    dgTrainingSet.DataSource = fm.NewDataTable();

                    cbTrainingSets.Items.Add(fm);
                    cbTrainingSets.SelectedIndex = cbTrainingSets.Items.Count - 1;

                    btRemoveTrainingSet.Enabled = true;
                }

            }


            //DEPRECATEDParent.fmNetworkExplorer.AddNode(fm.Name, ((pDocument)Parent.ActiveDocument).Filename);
        }

        public override void RemoveItem(int Index)
        {

            cbTrainingSets.Items.RemoveAt(Index);
            m_parentDocument.RemoveTrainingSet(Index);

            if (cbTrainingSets.Items.Count > 0)
                cbTrainingSets.SelectedIndex = 0;
            else
            {
                dgTrainingSet.DataSource = null;
                btRemoveTrainingSet.Enabled = false;
            }
        }


        public void ClearItems()
        {
            cbTrainingSets.Items.Clear();
        }

        public override void ImportItem()
        {
            OpenFileDialog s = new OpenFileDialog();
            s.DefaultExt = ".xml";
            s.Filter = "pNeuron Network Training Set (*.xml)|*.xml|All files (*.*)|*.*";
            if (s.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataTable trainingSet = (DataTable)dgTrainingSet.DataSource;

                    DataTable importedTrainingSet = trainingSet.Clone();
                    importedTrainingSet.Locale = CultureInfo.InvariantCulture;
                    importedTrainingSet.TableName = "pTrainingSet";
                    importedTrainingSet.ReadXml(s.FileName);

                    if (trainingSet.Columns.Count != importedTrainingSet.Columns.Count)
                        throw new Exception();

                    foreach (DataRow row in importedTrainingSet.Rows)
                        trainingSet.Rows.Add(row.ItemArray);
                }
                catch
                {
                    pMessage.Error("Invalid or corrupt file.");
                }
            }
        }

        public override void ExportItem()
        {
            SaveFileDialog s = new SaveFileDialog();
            s.DefaultExt = ".xml";
            s.FileName = ".xml";
            s.Filter = "pNeuron Network Training Set (*.xml)|*.xml|All files (*.*)|*.*";
            if (s.ShowDialog() == DialogResult.OK)
            {
                DataTable trainingSet = (DataTable)dgTrainingSet.DataSource;

                DataTable exportTrainingSet = new DataTable("pTrainingSet");
                exportTrainingSet.Locale = CultureInfo.InvariantCulture;
                exportTrainingSet.Merge(trainingSet);
                exportTrainingSet.WriteXml(s.FileName);
            }
        }

        public override string DefaultItemName
        {
            get { return "New Training Set"; }
        }

        public override void SelectItem(int Index)
        {
            pTrainingSet p = (pTrainingSet)cbTrainingSets.SelectedItem;

            if (m_parentDocument.MainDisplay.Net.InputNeuronCount + m_parentDocument.MainDisplay.Net.OutputNeuronCount != p.fDataTable.Columns.Count)
                pMessage.Error("This Training Set are out of date.");
            else dgTrainingSet.DataSource = p.fDataTable;

        }
    }
}
