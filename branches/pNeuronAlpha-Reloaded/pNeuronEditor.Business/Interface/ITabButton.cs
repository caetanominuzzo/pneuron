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
    }
}
