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
    public interface IpDoc
    {
        void Show();
        void Close();
        DialogResult Save();
        bool Modificated {get; set; }
        bool DefaultNamedFile { get; set; }
        string Filename { get; set; }
    }
}
