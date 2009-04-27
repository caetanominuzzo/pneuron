using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using primeira.pNeuron.Core;
using System.IO;

namespace primeira.pNeuron.pNeuronIDE
{
    public interface IpProjectItem
    {
        string Filename { get; }
        bool DefaultNamed { get; }
        bool Modified { get; }
        pProjectItemTypes Type { get; }

        DialogResult Save();
        DialogResult Load();
    }
}
