using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using primeira.pNeuron.Core;
using primeira.pNeuron;

namespace pTest
{
    public partial class pTest : Form
    {

        NeuralNet net;

        public pTest()
        {
            InitializeComponent();
            net = new NeuralNet();
            net.Initialize(1, 10);
        }

        private void pTest_Load(object sender, EventArgs e)
        {

            pDisplay1.Initialize();

            foreach (Neuron n in net.Neuron)
            {
                pDisplay1.Add(n);
            }

          //  pDisplay1.pPanels[0].Location = new Point(100, 100);
          //  pDisplay1.pPanels[0].Location = new Point(100, 500);
        }

    }
}