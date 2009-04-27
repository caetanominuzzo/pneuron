using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using primeira.pNeuron.Core;

namespace pTemp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        TextBox[][] t = new TextBox[3][];

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < t.Length; i++)
            {
                t[i] = new TextBox[4];
            }

            for (int i = 0; i < t.Length; i++)
                for (int j = 0; j < t[i].Length; j++)
                {
                    t[i][j] = new TextBox();
                    Controls.Add(t[i][j]);
                    t[i][j].Location = new Point(i*120, j*25);
                }

            t[0][0].Text = "0.1";
            t[1][0].Text = "0.1";
            t[2][0].Text = "0.1";

            t[0][1].Text = "0.9";
            t[1][1].Text = "0.9";
            t[2][1].Text = "0.1";

            t[0][2].Text = "0.1";
            t[1][2].Text = "0.9";
            t[2][2].Text = "0.9";

            t[0][3].Text = "0.9";
            t[1][3].Text = "0.1";
            t[2][3].Text = "0.9";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NeuralNet net = new NeuralNet();
            
        }
    }
}