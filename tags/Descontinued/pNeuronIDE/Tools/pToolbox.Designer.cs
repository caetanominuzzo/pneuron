using System.Windows.Forms;

namespace primeira.pNeuron
{
    partial class pToolbox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private RadioButton rCursor;
        private RadioButton rSynapse;
        private RadioButton rRemove;
        private RadioButton rNeuron;


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

            this.rCursor = new System.Windows.Forms.RadioButton();
            this.rSynapse = new System.Windows.Forms.RadioButton();
            this.rNeuron = new System.Windows.Forms.RadioButton();
            this.rRemove = new System.Windows.Forms.RadioButton();
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
            // pToolbox
            // 
            this.ClientSize = new System.Drawing.Size(192, 581);

            this.Controls.Add(this.rRemove);
            this.Controls.Add(this.rNeuron);
            this.Controls.Add(this.rSynapse);
            this.Controls.Add(this.rCursor);
            this.Name = "fmToolbox";
            this.TabText = "Toolbox";
            this.Text = "Toolbox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}