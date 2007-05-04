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
            this.components = new System.ComponentModel.Container();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pDisplay1 = new primeira.pNeuron.pDisplay();
            this.pDisplayBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pDisplayStatusBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pDisplayBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pDisplayStatusBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Tag";
            this.dataGridViewTextBoxColumn1.HeaderText = "Tag";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // pDisplay1
            // 
            this.pDisplay1.ActiveControl = null;
            this.pDisplay1.AutoScroll = true;
            this.pDisplay1.AutoScrollHorizontalMaximum = 100;
            this.pDisplay1.AutoScrollHorizontalMinimum = 0;
            this.pDisplay1.AutoScrollHPos = 0;
            this.pDisplay1.AutoScrollVerticalMaximum = 100;
            this.pDisplay1.AutoScrollVerticalMinimum = 0;
            this.pDisplay1.AutoScrollVPos = 0;
            this.pDisplay1.CtrlKey = false;
            this.pDisplay1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pDisplay1.DisplayStatus = primeira.pNeuron.pDisplay.pDisplayStatus.Idle;
            this.pDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pDisplay1.EnableAutoScrollHorizontal = true;
            this.pDisplay1.EnableAutoScrollVertical = true;
            this.pDisplay1.Location = new System.Drawing.Point(0, 0);
            this.pDisplay1.Name = "pDisplay1";
            this.pDisplay1.ShiftKey = false;
            this.pDisplay1.Size = new System.Drawing.Size(785, 569);
            this.pDisplay1.TabIndex = 0;
            this.pDisplay1.VisibleAutoScrollHorizontal = true;
            this.pDisplay1.VisibleAutoScrollVertical = true;
            // 
            // pDisplayBindingSource
            // 
            this.pDisplayBindingSource.DataSource = typeof(primeira.pNeuron.pDisplay);
            // 
            // pDisplayStatusBindingSource
            // 
            this.pDisplayStatusBindingSource.DataSource = typeof(primeira.pNeuron.pDisplay.pDisplayStatus);
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
            ((System.ComponentModel.ISupportInitialize)(this.pDisplayBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pDisplayStatusBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private primeira.pNeuron.pDisplay pDisplay1;
        private System.Windows.Forms.BindingSource pDisplayBindingSource;
        private System.Windows.Forms.BindingSource pDisplayStatusBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

       // private primeira.pNeuron.pDisplay pDisplay1;

    }
}

