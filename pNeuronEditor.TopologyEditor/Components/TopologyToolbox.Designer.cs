using primeira.pNeuron.Editor;
using primeira.pNeuron.Editor.Components;
namespace pNeuronEditor.TopologyEditor
{
    partial class TopologyToolbox
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopologyToolbox));
            this.btDelNeuron = new primeira.pNeuron.Editor.Components.EditorBaseButton();
            this.btAddSynapse = new primeira.pNeuron.Editor.Components.EditorBaseButton();
            this.btAddNeuron = new primeira.pNeuron.Editor.Components.EditorBaseButton();
            this.btCursor = new primeira.pNeuron.Editor.Components.EditorBaseButton();
            this.SuspendLayout();
            // 
            // btDelNeuron
            // 
            this.btDelNeuron.Image = ((System.Drawing.Image)(resources.GetObject("btDelNeuron.Image")));
            this.btDelNeuron.Location = new System.Drawing.Point(130, 10);
            this.btDelNeuron.Name = "btDelNeuron";
            this.btDelNeuron.ShowFocus = true;
            this.btDelNeuron.ShowLabel = false;
            this.btDelNeuron.Size = new System.Drawing.Size(29, 27);
            this.btDelNeuron.TabIndex = 3;
            this.btDelNeuron.Text = "Remove Neuron";
            this.btDelNeuron.UseVisualStyleBackColor = true;
            this.btDelNeuron.Click += new System.EventHandler(this.btDelNeuron_Click);
            // 
            // btAddSynapse
            // 
            this.btAddSynapse.Image = ((System.Drawing.Image)(resources.GetObject("btAddSynapse.Image")));
            this.btAddSynapse.Location = new System.Drawing.Point(90, 10);
            this.btAddSynapse.Name = "btAddSynapse";
            this.btAddSynapse.ShowFocus = true;
            this.btAddSynapse.ShowLabel = false;
            this.btAddSynapse.Size = new System.Drawing.Size(29, 27);
            this.btAddSynapse.TabIndex = 2;
            this.btAddSynapse.Text = "Add Synapse";
            this.btAddSynapse.UseVisualStyleBackColor = true;
            this.btAddSynapse.Click += new System.EventHandler(this.btAddSynapse_Click);
            // 
            // btAddNeuron
            // 
            this.btAddNeuron.Image = ((System.Drawing.Image)(resources.GetObject("btAddNeuron.Image")));
            this.btAddNeuron.Location = new System.Drawing.Point(50, 10);
            this.btAddNeuron.Name = "btAddNeuron";
            this.btAddNeuron.ShowFocus = true;
            this.btAddNeuron.ShowLabel = false;
            this.btAddNeuron.Size = new System.Drawing.Size(29, 27);
            this.btAddNeuron.TabIndex = 1;
            this.btAddNeuron.Text = "Add Neuron";
            this.btAddNeuron.UseVisualStyleBackColor = true;
            this.btAddNeuron.Click += new System.EventHandler(this.btAddNeuron_Click);
            // 
            // btCursor
            // 
            this.btCursor.Image = ((System.Drawing.Image)(resources.GetObject("btCursor.Image")));
            this.btCursor.Location = new System.Drawing.Point(10, 10);
            this.btCursor.Name = "btCursor";
            this.btCursor.ShowFocus = true;
            this.btCursor.ShowLabel = false;
            this.btCursor.Size = new System.Drawing.Size(29, 27);
            this.btCursor.TabIndex = 0;
            this.btCursor.Text = "None";
            this.btCursor.UseVisualStyleBackColor = true;
            this.btCursor.Click += new System.EventHandler(this.btCursor_Click);
            // 
            // pToolbox
            // 
            this.Controls.Add(this.btDelNeuron);
            this.Controls.Add(this.btAddSynapse);
            this.Controls.Add(this.btAddNeuron);
            this.Controls.Add(this.btCursor);
            this.Name = "pToolbox";
            this.Size = new System.Drawing.Size(170, 52);
            this.ResumeLayout(false);

        }

        #endregion

        private EditorBaseButton btCursor;
        private EditorBaseButton btAddNeuron;
        private EditorBaseButton btDelNeuron;
        private EditorBaseButton btAddSynapse;
    }
}
