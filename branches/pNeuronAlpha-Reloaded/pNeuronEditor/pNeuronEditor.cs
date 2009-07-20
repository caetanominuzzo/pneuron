using System.Windows.Forms;
using primeira.pNeuron.Editor.Business;
using primeira.pNeuron.Editor.Components;

namespace primeira.pNeuron.Editor
{
    public partial class pNeuronEditor : Form
    {
        public pNeuronEditor()
        {
            InitializeComponent();

            EditorManager.RegisterEditors();

            TabManager.GetInstance().SetTabControl(this.tbMain);

            IRecentFileManager i = (IRecentFileManager)DocumentManager.LoadDocument("default.filebrowser");

            FileManager.SetRecentManager(i);

            //FileManager.SetOpenManager(tbMain);

            //DocumentManager.LoadDocument(@"c:\topology 23.pne");
        }
    }
}