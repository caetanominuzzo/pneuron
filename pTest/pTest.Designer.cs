namespace pTest
{
    partial class pTest
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pNeuronIDE1 = new primeira.pNeuron.pNeuronIDE();
            this.SuspendLayout();
            // 
            // pNeuronIDE1
            // 
            this.pNeuronIDE1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pNeuronIDE1.Location = new System.Drawing.Point(0, 0);
            this.pNeuronIDE1.Name = "pNeuronIDE1";
            this.pNeuronIDE1.Size = new System.Drawing.Size(585, 408);
            this.pNeuronIDE1.TabIndex = 0;
            // 
            // pTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 408);
            this.Controls.Add(this.pNeuronIDE1);
            this.KeyPreview = true;
            this.Name = "pTest";
            this.Text = "pTest";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private primeira.pNeuron.pNeuronIDE pNeuronIDE1;




        // private primeira.pNeuron.pDisplay pDisplay1;

    }
}

