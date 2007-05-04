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
            this.pDisplay1 = new primeira.pNeuron.pDisplay();
            this.SuspendLayout();
            // 
            // pDisplay1
            // 
            this.pDisplay1.ActiveControl = null;
            this.pDisplay1.AllowDrop = true;
            this.pDisplay1.AutoScroll = true;
            this.pDisplay1.AutoScrollMinSize = new System.Drawing.Size(1024, 768);
            this.pDisplay1.BackColor = System.Drawing.Color.White;
            this.pDisplay1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pDisplay1.DisplayStatus = primeira.pNeuron.pDisplay.pDisplayStatus.Idle;
            this.pDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pDisplay1.Location = new System.Drawing.Point(0, 0);
            this.pDisplay1.Name = "pDisplay1";
            this.pDisplay1.Size = new System.Drawing.Size(785, 569);
            this.pDisplay1.TabIndex = 0;
            // 
            // pTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 569);
            this.Controls.Add(this.pDisplay1);
            this.KeyPreview = true;
            this.Name = "pTest";
            this.Text = "pTest";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.pTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private primeira.pNeuron.pDisplay pDisplay1;

    }
}

