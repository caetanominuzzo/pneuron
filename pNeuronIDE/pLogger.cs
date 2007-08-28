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
            this.txtLogger = new System.Windows.Forms.TextBox();
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
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtLogger);
            this.Name = "fmLooger";
            this.Text = "pLogger";
            this.TabText = "pLogger";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TextBox txtLogger;
        private StringBuilder sb = new StringBuilder();

        #region IpDocks Members

        public pNeuronIDE Parent
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
            txtLogger.Text = text;
        }


        public void Flush()
        {
            this.Invoke(new Assinc(assincFlush), new object[] { sb.ToString() });
        }

        #endregion
    }
}
