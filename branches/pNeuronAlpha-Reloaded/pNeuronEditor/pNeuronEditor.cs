using System.Windows.Forms;
using primeira.pNeuron.Editor.Business;

namespace primeira.pNeuron.Editor
{
    public partial class pNeuronEditor : Form
    {
        public pNeuronEditor()
        {
            InitializeComponent();

            TabManager.GetInstance().SetTabControl(this.tbMain);

            IRecentFileManager i = (IRecentFileManager)DocumentManager.LoadDocument("default.filebrowser");

            FileManager.SetRecentManager(i);

            //FileManager.SetOpenManager(tbMain);

            //DocumentManager.LoadDocument(@"c:\topology 23.pne");
        }
    }
}