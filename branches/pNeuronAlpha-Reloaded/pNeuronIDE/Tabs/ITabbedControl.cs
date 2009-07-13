using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace primeira.pNeuron
{
    public interface ITabbedControl
    {
        bool ShowClose { get; }

        void BringToFront();

        string TabCaption { get; }
    }
}
