using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using pNeuronEditor.Business;

namespace pNeuronEditor.Components
{
    public partial class MessageControl : UserControl, IMessageControl
    {
        public MessageControl()
        {
            InitializeComponent();
        }

        #region IMessageControl Members

        public void Show(string message, MessageOptions options)
        {
            MessageBox.Show(message);
        }

        #endregion
    }
}
