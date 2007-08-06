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

        public NeuralNetwork net = new NeuralNetwork();

        TextBox[][] t = new TextBox[3][];
           double high, mid, low;

        int gI = 0;



        private void pTrain_Load(object sender, EventArgs e)
        {
            high = .9999999999999999999;

            low = .00000000000000000001;

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
            //net.LearningRate = .5;
            
            Timer t = new Timer();
            t.Interval = 1;
            t.Tick += new EventHandler(t_Tick);
            t.Enabled = true;
            double dGlobalError = 0;
            double dTotalError = 0;
            do
            {

                count++;


                net.Train(input, output, 1);

                dTotalError = 0;
                foreach (Neuron n in net.Neuron)
                {
                    dTotalError += n.Error;
                }

                dGlobalError = dTotalError / net.Neuron.Count;


                // net.ApplyLearning();

                net.Neuron[0].Value = low;
                net.Neuron[1].Value = low;

                net.Pulse();
                ll = net.Neuron[net.Neuron.Count - 1].Value;


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

            //while (hh > (mid + low) / 2
            //    || lh < (mid + high) / 2
            //    || hl < (mid + low) / 2
            //    || ll > (mid + high) / 2);


            while ( dGlobalError  < -.0001 || dGlobalError > .0001);

            StringBuilder bld = new StringBuilder();

            bld.Remove(0, bld.Length);
            bld.Append((count * 5).ToString()).Append(" iterations required for training\n");

            bld.Append("hh: ").Append(hh.ToString()).Append(" < .5\n");
            bld.Append("ll: ").Append(ll.ToString()).Append(" < .5\n");

            bld.Append("hl: ").Append(hl.ToString()).Append(" > .5\n");
            bld.Append("lh: ").Append(lh.ToString()).Append(" > .5\n");
            bld.Append("GLOBAL " + dGlobalError.ToString());
            bld.Append(gI.ToString());
            MessageBox.Show(bld.ToString());

         //   net.Train(input, output, TrainingType.BackPropogation, 100);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            net.Neuron[0].Value = Convert.ToDouble(textBox1.Text);
            if (net.Neuron[1].NeuronType == NeuronTypes.Input)
                net.Neuron[1].Value = Convert.ToDouble(textBox2.Text);

            net.Pulse();

                MessageBox.Show(net.Neuron[4].Value.ToString());
        }

        void t_Tick(object sender, EventArgs e)
        {
            gI++;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double[][] input = new double[10][];
            input[0] = new double[] { 1 };
            input[1] = new double[] { 2 };
            input[2] = new double[] { 3 };
            input[3] = new double[] { 4 };
            input[4] = new double[] { 5 };
            input[5] = new double[] { 6 };
            input[6] = new double[] { 7 };
            input[7] = new double[] { 8 };
            input[8] = new double[] { 9 };
            input[9] = new double[] { 0 };

            double[][] output = new double[10][];
            output[0] = new double[] { low, high };
            output[1] = new double[] { high, low };
            output[2] = new double[] { low, high };
            output[3] = new double[] { high, low };
            output[4] = new double[] { low, high };
            output[5] = new double[] { high, low };
            output[6] = new double[] { low, high };
            output[7] = new double[] { high, low };
            output[8] = new double[] { low, high };
            output[9] = new double[] { high, low };

            //  net.Initialize(1, 0);

            double ll, lh, hl, hh;

            int count;

            count = 0;

            Timer t = new Timer();
            t.Interval = 1;
            t.Tick += new EventHandler(t_Tick);
            t.Enabled = true;

            do
            {

                count++;





                net.Train(input, output, 100);

                // net.ApplyLearning();

                bool bOk = true;

                for (int i = 1; i < 10; i++ )
                {
                    net.Neuron[0].Value = i;
                    net.Pulse();
                    if ((i % 2 == 0 && net.Neuron[net.Neuron.Count - 2].Value < net.Neuron[net.Neuron.Count - 1].Value)
                        ||
                        (i % 2 == 1 && net.Neuron[net.Neuron.Count - 2].Value > net.Neuron[net.Neuron.Count - 1].Value))
                    {
                        bOk = false;
                        break;
                    }

                }

                if (bOk)
                    break;
            }

            while (true);

            StringBuilder bld = new StringBuilder();

            bld.Remove(0, bld.Length);
            bld.Append((count * 5).ToString()).Append(" iterations required for training\n");

            //bld.Append("hh: ").Append(hh.ToString()).Append(" < .5\n");
            //bld.Append("ll: ").Append(ll.ToString()).Append(" < .5\n");

            //bld.Append("hl: ").Append(hl.ToString()).Append(" > .5\n");
            //bld.Append("lh: ").Append(lh.ToString()).Append(" > .5\n");
            bld.Append(gI.ToString());
            MessageBox.Show(bld.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            net.Neuron[0].Value = Convert.ToDouble(textBox3.Text);

            net.Pulse();

            if(net.Neuron[net.Neuron.Count-2].Value > net.Neuron[net.Neuron.Count-1].Value)
                MessageBox.Show("Impar");
            else MessageBox.Show("Par");

        }

    }
}