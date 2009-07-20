using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace primeira.pNeuron.Editor.Business
{
    public interface ITabControl
    {
        void HideTab(IEditorBase tab);

        void CloseHidedTabs();

        void AddTab(IEditorBase tab);

    }
}
