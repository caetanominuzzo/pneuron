using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace primeira.pNeuron
{
    public partial class fmInputData : Form
    {

        private int m_inputCount;

        public int InputCount
        {
            get { return m_inputCount; }
            set { m_inputCount = value; }
        }


        public fmInputData()
        {
            InitializeComponent();
        }

        public fmInputData(int InputCount)
        {
            DialogResult = DialogResult.Cancel;
            m_inputCount = InputCount;

            InitializeComponent();
            SuspendLayout();
            for (int i = 0; i < InputCount; i++)
            {
                TextBox t = new TextBox();

                t.Location = new System.Drawing.Point(12, 12+(i*30));
                t.Size = new System.Drawing.Size(100, 20);
                t.TabIndex = i;

                Controls.Add(t);

                if (Height < 12 + (i * 30) + 50)
                    Height = 12 + (i * 30) + 50;
            }


            ResumeLayout(false);
            PerformLayout();
        }



        private void bOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }

    public static class GetInputData
    {
        public static double[] Show(int InputCount)
        {
            double[] result = new double[InputCount ];

            using (fmInputData f = new fmInputData(InputCount))
            {
                int iInput = 0;
                if(f.ShowDialog() == DialogResult.OK);
                    foreach (Control c in f.Controls)
                    {
                        if (c is TextBox)
                        {
                            result[iInput++] = double.Parse(c.Text, System.Globalization.CultureInfo.InvariantCulture);
                        }
                    }
            }
            return result;
        }
    }
}