using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using primeira.pNeuron.Core;

namespace Temp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //NeuralNetwork net = NeuralNetwork.ToObject(@"c:\_5.pne");
            //pDisplay1.SetNeuralNetwork(net);
            //net.to
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pDisplay1.DisplayStatus = primeira.pNeuron.pDisplay.pDisplayStatus.Add_Neuron;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pDisplay1.DisplayStatus = primeira.pNeuron.pDisplay.pDisplayStatus.Idle;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pDisplay1.DisplayStatus = primeira.pNeuron.pDisplay.pDisplayStatus.Linking;
        }

        private void pDisplay1_OnDisplayStatusChange()
        {
            Text = pDisplay1.DisplayStatus.ToString();
        }
    }
}
