namespace pNeuronEditor.Components
{
    partial class TabControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabControl));
            this.pnTabArea = new System.Windows.Forms.Panel();
            this.pnDocArea = new System.Windows.Forms.Panel();
            this.pnTop = new System.Windows.Forms.Panel();
            this.pnCloseArea = new System.Windows.Forms.Panel();
            this.btnClose = new pNeuronEditor.Components.EditorBaseButton();
            this.pnTop.SuspendLayout();
            this.pnCloseArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnTabArea
            // 
            this.pnTabArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnTabArea.Location = new System.Drawing.Point(0, 0);
            this.pnTabArea.Name = "pnTabArea";
            this.pnTabArea.Size = new System.Drawing.Size(360, 40);
            this.pnTabArea.TabIndex = 2;
            // 
            // pnDocArea
            // 
            this.pnDocArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnDocArea.Location = new System.Drawing.Point(0, 40);
            this.pnDocArea.Name = "pnDocArea";
            this.pnDocArea.Size = new System.Drawing.Size(400, 360);
            this.pnDocArea.TabIndex = 3;
            // 
            // pnTop
            // 
            this.pnTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.pnTop.Controls.Add(this.pnTabArea);
            this.pnTop.Controls.Add(this.pnCloseArea);
            this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTop.Location = new System.Drawing.Point(0, 0);
            this.pnTop.Name = "pnTop";
            this.pnTop.Size = new System.Drawing.Size(400, 40);
            this.pnTop.TabIndex = 4;
            // 
            // pnCloseArea
            // 
            this.pnCloseArea.Controls.Add(this.btnClose);
            this.pnCloseArea.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnCloseArea.Location = new System.Drawing.Point(360, 0);
            this.pnCloseArea.Name = "pnCloseArea";
            this.pnCloseArea.Size = new System.Drawing.Size(40, 40);
            this.pnCloseArea.TabIndex = 2;
            this.pnCloseArea.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(7, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.ShowFocus = true;
            this.btnClose.ShowLabel = false;
            this.btnClose.Size = new System.Drawing.Size(24, 24);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "editorBaseButton1";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // TabControl
            // 
            this.Controls.Add(this.pnDocArea);
            this.Controls.Add(this.pnTop);
            this.Name = "TabControl";
            this.Size = new System.Drawing.Size(400, 400);
            this.pnTop.ResumeLayout(false);
            this.pnCloseArea.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnTabArea;
        private System.Windows.Forms.Panel pnDocArea;
        private System.Windows.Forms.Panel pnTop;
        private System.Windows.Forms.Panel pnCloseArea;
        private EditorBaseButton btnClose;
    }
}
