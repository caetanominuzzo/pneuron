using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pNeuronEditor.Business
{
    public interface ITabControl
    {
        void HideTab(IEditorBase tab);

        void CloseHidedTabs();

        void AddTab(IEditorBase tab);

    }
}
