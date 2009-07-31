using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pNeuronEditor.Business
{
    public interface IRecentFileControl
    {
        void AddRecent(string filename);

        string[] GetRecent();
    }
}
