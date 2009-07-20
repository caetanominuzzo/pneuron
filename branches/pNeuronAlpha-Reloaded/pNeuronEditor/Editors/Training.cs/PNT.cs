using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace primeira.pNeuron
{
    public partial class PNT : EditorBase
    {
        private string filename;
        public PNT(string Filename, DocumentBase data)
            : base(Filename, data, typeof(TrainingData))
        {
            InitializeComponent();
            filename = Filename;
        }

        protected override event EditorBase.ChangedDelegate OnChanged;



    }
}
