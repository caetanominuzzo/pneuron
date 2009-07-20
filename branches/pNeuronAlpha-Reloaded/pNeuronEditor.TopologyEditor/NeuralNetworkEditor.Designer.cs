using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using primeira.pNeuron.Core;
using System.IO;
using System.Threading;
using primeira.pRandom;
using primeira.pNeuron;

namespace pNeuronEditor.TopologyEditor
{
    public partial class NeuralNetworkEditor
    {
        private void InitializeComponent()
        {
            this.MainDisplay = new primeira.pNeuron.pDisplay();
            this.lblStatus = new System.Windows.Forms.Label();
            this.flowStatus = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnBottom = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnRight = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.flowStatus.SuspendLayout();
            this.pnBottom.SuspendLayout();
            this.pnRight.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainDisplay
            // 
            this.MainDisplay.AutoScroll = true;
            this.MainDisplay.BackColor = System.Drawing.Color.White;
            this.MainDisplay.Cursor = System.Windows.Forms.Cursors.Default;
            this.MainDisplay.DisplayStatus = primeira.pNeuron.pDisplay.pDisplayStatus.Idle;
            this.MainDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainDisplay.Location = new System.Drawing.Point(0, 0);
            this.MainDisplay.Name = "MainDisplay";
            this.MainDisplay.Size = new System.Drawing.Size(469, 213);
            this.MainDisplay.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(73, 6);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(30, 18);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Idle";
            // 
            // flowStatus
            // 
            this.flowStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.flowStatus.Controls.Add(this.label1);
            this.flowStatus.Controls.Add(this.lblStatus);
            this.flowStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.flowStatus.Location = new System.Drawing.Point(0, 379);
            this.flowStatus.Name = "flowStatus";
            this.flowStatus.Padding = new System.Windows.Forms.Padding(6);
            this.flowStatus.Size = new System.Drawing.Size(704, 32);
            this.flowStatus.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Status: ";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 378);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(704, 1);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(180, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1, 165);
            this.panel2.TabIndex = 5;
            // 
            // pnRight
            // 
            this.pnRight.Controls.Add(this.panel6);
            this.pnRight.Controls.Add(this.panel5);
            this.pnRight.Controls.Add(this.panel4);
            this.pnRight.Controls.Add(this.panel3);
            this.pnRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnRight.Location = new System.Drawing.Point(470, 0);
            this.pnRight.Name = "pnRight";
            this.pnRight.Size = new System.Drawing.Size(234, 213);
            this.pnRight.TabIndex = 8;
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 110);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(234, 103);
            this.panel6.TabIndex = 9;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 80);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(234, 30);
            this.panel5.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(10, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Properties";
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 30);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(234, 50);
            this.panel4.TabIndex = 7;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(234, 30);
            this.panel3.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(10, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Toolbox";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel7.Location = new System.Drawing.Point(469, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1, 213);
            this.panel7.TabIndex = 9;
            this.panel7.Margin = new Padding(0, 20, 0, 20);
            this.panel7.Paint += new PaintEventHandler(panel7_Paint);
            // 
            // pDocument
            // 
            this.Controls.Add(this.MainDisplay);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.pnRight);
            this.Controls.Add(this.pnBottom);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowStatus);
            this.Name = "pDocument";
            this.Size = new System.Drawing.Size(704, 411);
            this.flowStatus.ResumeLayout(false);
            this.flowStatus.PerformLayout();
            this.pnBottom.ResumeLayout(false);
            this.pnRight.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        SolidBrush b = new SolidBrush(Color.White);

        void panel7_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(b, 0, 0, 1, 20);
            e.Graphics.FillRectangle(b, 0, panel7.Height - 20, 1, 20);
        }

        public pDisplay MainDisplay;
        private Label lblStatus;
        private FlowLayoutPanel flowStatus;
        private Label label1;
        private Panel panel1;
        private Panel pnBottom;
        private Panel panel2;
        private Panel pnRight;
        private Label label2;
        private Panel panel4;
        private Panel panel3;
        private Panel panel6;
        private Panel panel5;
        private Label label3;
        private Panel panel7;

    }
}
