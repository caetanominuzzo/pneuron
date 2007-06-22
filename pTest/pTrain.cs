using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using primeira.pNeuron.Core;

namespace primeira.pNeuron
{
    public partial class pTrain : Form
    {
        public pTrain()
        {
            InitializeComponent();
        }

        public NeuralNet net = new NeuralNet();

        TextBox[][] t = new TextBox[3][];
           double high, mid, low;

        int gI = 0;



        private void pTrain_Load(object sender, EventArgs e)
        {
            high = .9;

            low = .1;

            mid = .5;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[][] input = new double[4][];
            input[0] = new double[] {high, high};
            input[1] = new double[] {low, high};
            input[2] = new double[] {high, low};
            input[3] = new double[] {low, low};

            double[][] output = new double[4][];
            output[0] = new double[] { low };
            output[1] = new double[] { high };
            output[2] = new double[] { high };
            output[3] = new double[] { low };

          //  net.Initialize(1, 0);
        
            double ll, lh, hl, hh;

            int count;

            count = 0;
            net.LearningRate = 3;
            
            net.InitializeLearning();

            Timer t = new Timer();
            t.Interval = 1;
            t.Tick += new EventHandler(t_Tick);
            t.Enabled = true;

            do
            {

                count++;


              


                net.Train(input, output, TrainingType.BackPropogation, 1);

                // net.ApplyLearning();

                net.Neuron[0].Value = low;
                net.Neuron[1].Value = low;

                net.Pulse();
                ll = net.Neuron[ net.Neuron.Count-1 ].Value;


                net.Neuron[0].Value = high;
                net.Neuron[1].Value = low;

                net.Pulse();
                hl = net.Neuron[net.Neuron.Count - 1].Value;


                net.Neuron[0].Value = low;
                net.Neuron[1].Value = high;

                net.Pulse();
                lh = net.Neuron[net.Neuron.Count - 1].Value;


                net.Neuron[0].Value = high;
                net.Neuron[1].Value = high;

                net.Pulse();
                hh = net.Neuron[net.Neuron.Count - 1].Value;

            }

            while (hh > (mid + low) / 2
                || lh < (mid + high) / 2
                || hl < (mid + low) / 2
                || ll > (mid + high) / 2);

            StringBuilder bld = new StringBuilder();

            bld.Remove(0, bld.Length);
            bld.Append((count * 5).ToString()).Append(" iterations required for training\n");

            bld.Append("hh: ").Append(hh.ToString()).Append(" < .5\n");
            bld.Append("ll: ").Append(ll.ToString()).Append(" < .5\n");

            bld.Append("hl: ").Append(hl.ToString()).Append(" > .5\n");
            bld.Append("lh: ").Append(lh.ToString()).Append(" > .5\n");
            bld.Append(gI.ToString());
            MessageBox.Show(bld.ToString());

         //   net.Train(input, output, TrainingType.BackPropogation, 100);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            net.Neuron[0].Value = Convert.ToDouble(textBox1.Text);
            net.Neuron[1].Value = Convert.ToDouble(textBox2.Text);

            net.Pulse();

            MessageBox.Show(net.Neuron[4].Value.ToString());
        }

        void t_Tick(object sender, EventArgs e)
        {
            gI++;
        }

    }
}