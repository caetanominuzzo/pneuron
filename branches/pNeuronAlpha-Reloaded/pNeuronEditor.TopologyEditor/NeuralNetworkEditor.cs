using System.Windows.Forms;
using primeira.pNeuron;
using primeira.pNeuron.Core;
using pNeuronEditor.Business;
using pNeuronEditor.Components;


namespace pNeuronEditor.TopologyEditor
{
    public partial class NeuralNetworkEditor : EditorBase
    {
        #region Ctor

        public NeuralNetworkEditor(string filename, DocumentBase data)
            : base(filename, data, typeof(NeuralNetworkDocument))
        {

            InitializeComponent();

            MainDisplay.OnDisplayStatusChange += new pDisplay.DisplayStatusChangeDelegate(MainDisplay_OnDisplayStatusChange);
            MainDisplay.OnNetworkChange += new pDisplay.NetworkChangeDelegate(MainDisplay_OnNetworkChange);

            Dock = DockStyle.Fill;

            TopologyPropertyGrid pp = new TopologyPropertyGrid();
            pp.SetDocument(this);
            pp.Dock = DockStyle.Fill;
            panel6.Controls.Add(pp);

            TopologyToolbox p = new TopologyToolbox();
            p.SetDocument(this);
            p.Dock = DockStyle.Fill;
            panel4.Controls.Add(p);

            if (((NeuralNetworkDocument)Document).NeuralNetwork == null)
                ((NeuralNetworkDocument)Document).NeuralNetwork = new NeuralNetwork();

            MainDisplay.SetNeuralNetwork(((NeuralNetworkDocument)Document).NeuralNetwork);

            this.OnSelected += new SelectedDelegate(NeuralNetworkEditor_OnSelected);
        }

        void NeuralNetworkEditor_OnSelected(IEditorBase sender)
        {
            //MainDisplay.SetNeuralNetwork(((NeuralNetworkDocument)this.Document).NeuralNetwork);
        }

        #endregion

        #region MainDisplay Events

        private void MainDisplay_OnDisplayStatusChange()
        {
            lblStatus.Text = MainDisplay.DisplayStatus.ToString();
        }

        private void MainDisplay_OnNetworkChange()
        {
            Changed();
        }

        #endregion

    }
}
