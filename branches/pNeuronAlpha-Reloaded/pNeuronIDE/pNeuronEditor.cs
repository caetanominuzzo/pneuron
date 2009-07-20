using System.Windows.Forms;
using primeira.pNeuron.Editor.Business;

namespace primeira.pNeuron.Editor
{
    public partial class pNeuronEditor : Form
    {
        public pNeuronEditor()
        {
            InitializeComponent();

            tbMain.LoadDocument("default.filebrowser");

            tbMain.LoadDocument(@"c:\topology 23.pne");


        }
    }
}