using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pNeuronEditor.Business
{
    public interface IMessageControl
    {
        void Show(string message, MessageOptions options);
    }

    [Flags()]
    public enum MessageOptions
    {
        None = 0,
        Alert = 1,
        NonModal = 2,
    }
}
