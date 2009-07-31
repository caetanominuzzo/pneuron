using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace pNeuronEditor.Business
{
    public interface ITabButton
    {
        Image SelectedImage { get; set; }

        Image UnselectedImage { get; set;  }

        Image BackgroundImage { get; set; }

        Image Image { get; set; }

        object Tag { get; set; }

        string Text { get; set; }

        Size Size { get; set; }

        ContentAlignment ImageAlign { get; set; }

        event EventHandler Click;

        void SetToolTip(string tooltip);
    }
}
