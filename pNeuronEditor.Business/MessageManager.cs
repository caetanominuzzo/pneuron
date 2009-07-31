using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pNeuronEditor.Business
{
    public static class MessageManager
    {
        private static IMessageControl _messageControl;

        public static void SetMessageManager(IMessageControl messageControl)
        {
            _messageControl = messageControl;
        }

        public static void Alert(params string[] message)
        {

            _messageControl.Show(string.Join(string.Empty, message), MessageOptions.Alert); 
        }

    }
}
