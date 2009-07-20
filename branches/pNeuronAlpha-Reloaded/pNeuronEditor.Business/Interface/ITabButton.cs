using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace primeira.pNeuron.Editor.Business
{
    public interface ITabButton
    {
        Image SelectedImage { get; set; }

        Image UnselectedImage { get; set;  }
    }
}
