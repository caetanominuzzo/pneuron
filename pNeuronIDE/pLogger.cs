using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace primeira.pNeuron
{
    public class pLogger : DockContent, IpDocks, primeira.pNeuron.Core.ILogger
    {
        public pLogger()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pLogger));
            this.txtLogger = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tspAttach = new System.Windows.Forms.ToolStripButton();
            this.tspClear = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLogger
            // 
            this.txtLogger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogger.Location = new System.Drawing.Point(0, 0);
            this.txtLogger.Multiline = true;
            this.txtLogger.Name = "txtLogger";
            this.txtLogger.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLogger.Size = new System.Drawing.Size(292, 273);
            this.txtLogger.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspAttach,
            this.tspClear});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(292, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tspAttach
            // 
            this.tspAttach.Image = ((System.Drawing.Image)(resources.GetObject("tspAttach.Image")));
            this.tspAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspAttach.Name = "tspAttach";
            this.tspAttach.Size = new System.Drawing.Size(59, 22);
            this.tspAttach.Text = "Attach";
            this.tspAttach.Click += new System.EventHandler(this.tspAttach_Click);
            // 
            // tspClear
            // 
            this.tspClear.Image = ((System.Drawing.Image)(resources.GetObject("tspClear.Image")));
            this.tspClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspClear.Name = "tspClear";
            this.tspClear.Size = new System.Drawing.Size(52, 22);
            this.tspClear.Text = "Clear";
            this.tspClear.Click += new System.EventHandler(this.tspClear_Click);
            // 
            // pLogger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.txtLogger);
            this.Name = "pLogger";
            this.TabText = "pLogger";
            this.Text = "pLogger";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TextBox txtLogger;
        private ToolStrip toolStrip1;
        private ToolStripButton tspAttach;
        private ToolStripButton tspClear;
        private StringBuilder sb = new StringBuilder();
        private int iNeuronCount = 0;

        #region IpDocks Members

        public new pNeuronIDE Parent
        {
            get { return ((pNeuronIDE)DockPanel.Parent); }
        }

        #endregion

        #region ILogger Members

        public void Log(string msg)
        {
            sb.Append(msg);
            sb.Append(Environment.NewLine);
        }

        private delegate void Assinc(string text);

        private void assincFlush(string text)
        {
            txtLogger.Text += text;
        }


        public void Flush()
        {
            this.Invoke(new Assinc(assincFlush), new object[] { sb.ToString() });
            sb.Length = 0;
        }

        #endregion

        delegate void AssincP(string msg);

        private void tspAttach_Click(object sender, EventArgs e)
        {
            if (tspAttach.Text == "Attach")
            {
                tspAttach.Text = "Dettach";
            }
            else
            {
                tspAttach.Text = "Attach";

                iNeuronCount = 0;
                
                Flush();
            }
        }

        void Net_OnNeuronPulseBack(primeira.pNeuron.Core.Neuron sender)
        {
//            this.Invoke(new AssincP(Log), new object[] { "Neuron pulseback: #" + sender.Index.ToString() });
  //          this.Invoke(new AssincP(Log), new object[] { "\tValue: " + sender.Value.ToString("0.000000000") });

            iNeuronCount++;

            if (iNeuronCount == sender.NeuralNetwork.Neuron.Count * 2)
                tspAttach_Click(null, null);

        }

        void Net_OnNeuronPulse(primeira.pNeuron.Core.Neuron sender)
        {
    //        this.Invoke(new AssincP(Log), new object[] { "Neuron pulse: #" + sender.Index.ToString() });
      //      this.Invoke(new AssincP(Log), new object[] { "\tValue: " + sender.Value.ToString("0.000000000") });

        }

        private void tspClear_Click(object sender, EventArgs e)
        {
            sb.Length = 0;
            txtLogger.Text = "";
        }
    }
}
