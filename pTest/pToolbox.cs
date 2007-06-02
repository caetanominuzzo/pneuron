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
    public class pToolbox : DockContent
    {
        public RadioButton rCursor;
        public RadioButton rSynapse;
        public RadioButton rRemove;
        private Button button1;
        public RadioButton rNeuron;
    
        public pToolbox()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.rCursor = new System.Windows.Forms.RadioButton();
            this.rSynapse = new System.Windows.Forms.RadioButton();
            this.rNeuron = new System.Windows.Forms.RadioButton();
            this.rRemove = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rCursor
            // 
            this.rCursor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rCursor.Appearance = System.Windows.Forms.Appearance.Button;
            this.rCursor.AutoEllipsis = true;
            this.rCursor.Location = new System.Drawing.Point(0, 4);
            this.rCursor.Name = "rCursor";
            this.rCursor.Size = new System.Drawing.Size(192, 22);
            this.rCursor.TabIndex = 0;
            this.rCursor.TabStop = true;
            this.rCursor.Text = "Cursor";
            this.rCursor.UseVisualStyleBackColor = true;
            this.rCursor.CheckedChanged += new System.EventHandler(this.rCursor_CheckedChanged);
            // 
            // rSynapse
            // 
            this.rSynapse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rSynapse.Appearance = System.Windows.Forms.Appearance.Button;
            this.rSynapse.AutoEllipsis = true;
            this.rSynapse.Location = new System.Drawing.Point(0, 28);
            this.rSynapse.Name = "rSynapse";
            this.rSynapse.Size = new System.Drawing.Size(192, 22);
            this.rSynapse.TabIndex = 1;
            this.rSynapse.TabStop = true;
            this.rSynapse.Text = "Add Synapse";
            this.rSynapse.UseVisualStyleBackColor = true;
            this.rSynapse.CheckedChanged += new System.EventHandler(this.rCursor_CheckedChanged);
            // 
            // rNeuron
            // 
            this.rNeuron.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rNeuron.Appearance = System.Windows.Forms.Appearance.Button;
            this.rNeuron.AutoEllipsis = true;
            this.rNeuron.Location = new System.Drawing.Point(0, 52);
            this.rNeuron.Name = "rNeuron";
            this.rNeuron.Size = new System.Drawing.Size(192, 22);
            this.rNeuron.TabIndex = 2;
            this.rNeuron.TabStop = true;
            this.rNeuron.Text = "Add Neuron";
            this.rNeuron.UseVisualStyleBackColor = true;
            this.rNeuron.CheckedChanged += new System.EventHandler(this.rCursor_CheckedChanged);
            // 
            // rRemove
            // 
            this.rRemove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rRemove.Appearance = System.Windows.Forms.Appearance.Button;
            this.rRemove.AutoEllipsis = true;
            this.rRemove.Location = new System.Drawing.Point(0, 77);
            this.rRemove.Name = "rRemove";
            this.rRemove.Size = new System.Drawing.Size(192, 22);
            this.rRemove.TabIndex = 3;
            this.rRemove.TabStop = true;
            this.rRemove.Text = "Remove Neuron";
            this.rRemove.UseVisualStyleBackColor = true;
            this.rRemove.CheckedChanged += new System.EventHandler(this.rCursor_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 339);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pToolbox
            // 
            this.ClientSize = new System.Drawing.Size(192, 581);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rRemove);
            this.Controls.Add(this.rNeuron);
            this.Controls.Add(this.rSynapse);
            this.Controls.Add(this.rCursor);
            this.Name = "pToolbox";
            this.TabText = "Toolbox";
            this.Text = "Toolbox";
            this.ResumeLayout(false);

        }

        private void rCursor_CheckedChanged(object sender, EventArgs e)
        {

            if (rCursor.Checked)
                ((pNeuronIDE)DockPanel.Parent).ActiveDocument.pDisplay1.DisplayStatus = pDisplay.pDisplayStatus.Idle;

            if (rSynapse.Checked)
                ((pNeuronIDE)DockPanel.Parent).ActiveDocument.pDisplay1.DisplayStatus = pDisplay.pDisplayStatus.Linking_Paused;

            if (rNeuron.Checked)
                ((pNeuronIDE)DockPanel.Parent).ActiveDocument.pDisplay1.DisplayStatus = pDisplay.pDisplayStatus.Add_Neuron;

            if (rRemove.Checked)
                ((pNeuronIDE)DockPanel.Parent).ActiveDocument.pDisplay1.DisplayStatus = pDisplay.pDisplayStatus.Remove_Neuron;

            ((pNeuronIDE)DockPanel.Parent).ActiveDocument.pDisplay1.Focus();// Select();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ((pNeuronIDE)DockPanel.Parent).ActiveDocument.Trainned = true;
        }
    }
}
