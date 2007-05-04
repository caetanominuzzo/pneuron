using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using primeira.pNeuron.Core;

namespace primeira.pNeuron.Core
{
    public partial class fmNNDesigner : Form
    {
        public NeuralNet net;
        public Panel pi, po;

        public Panel getP(string s)
        {
            foreach (object o in this.Controls)
                if (((Control)o).Tag.ToString() == s)
                    return (Panel)o;

            return null;
        }



        public fmNNDesigner( NeuralNet n )
        {
            InitializeComponent();
            this.net = n;
        }

        private void fmNNDesigner_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);


            foreach (Control c in this.Controls)
            {
                if (c is Panel)
                {
                    foreach (Neuron n in ((Neuron)c.Tag).Input.Keys)
                    {
                        foreach (Control d in this.Controls)
                        {
                            if (n == ((Neuron)d.Tag))
                            {
                                g.DrawLine(Pens.Gray,
                                    new Point(c.Left, c.Top),
                                    new Point(d.Left, d.Top));
                            }
                        }
                    }
                }
                
            }
                    
        }



        private void p_Click(object sender, EventArgs e)
        {
            if (pi != null)
            {
                ((Neuron)(pi.Tag)).Input.Add((Neuron)((Control)sender).Tag, new NeuralFactor(new Random(1).NextDouble()));

                pi = null;
            }
            else
            {
                pi = (Panel)sender;
            }
        }

        public bool isUsed(Point p)
        {
            foreach (Control c in this.Controls)
            {
                if (c.Bounds.Contains(p))
                    return true;
            }
            return false;
        }

        private void fmNNDesigner_Load(object sender, EventArgs e)
        {

            foreach (Neuron n in net.Neuron)
            {
                Panel p = new Panel();
                p.Width = 50;
                p.Height = 50;

                Random r = new Random(1);

                Point point;
                do
                {
                    point = new Point(r.Next(this.Width), r.Next(this.Height));
                } while (isUsed(point));


                p.Location = point;

                p.BackColor = Color.AliceBlue;
                p.Click += new EventHandler(p_Click);
                p.Tag = n;
                this.Controls.Add(p);
            }

        }



    }
}