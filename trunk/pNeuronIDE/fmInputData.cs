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

        public fmInputData(pPanel[] InputPanels)
        {
            DialogResult = DialogResult.Cancel;
            m_inputCount = InputCount;

            InitializeComponent();
            SuspendLayout();
            int i = 0;
            foreach (pPanel p in InputPanels)
            {
                if(p.Neuron.NeuronType != NeuronTypes.Input)
                    continue;

                TextBox t = new TextBox();
                Label l = new Label();

                t.Location = new System.Drawing.Point(72, 12+(i*30));
                t.Size = new System.Drawing.Size(100, 20);
                t.TabIndex = i;

                l.Location = new System.Drawing.Point(12, 15 + (i * 30));
                l.Size = new System.Drawing.Size(50, 20);
                l.Text = p.Text;

                Controls.Add(t);
                Controls.Add(l);

                if (Height < 12 + (i * 30) + 90)
                    Height = 12 + (i * 30) + 90;

                i++;
            }

            btOk.Top = Height - 60;
            btCancel.Top = Height - 60;


            btOk.TabIndex = i++;
            btCancel.TabIndex = i;



            ResumeLayout(false);
            PerformLayout();
        }



        private void bOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }

    
}