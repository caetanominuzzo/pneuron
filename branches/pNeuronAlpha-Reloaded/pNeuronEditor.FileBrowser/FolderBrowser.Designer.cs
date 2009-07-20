namespace primeira.pNeuron.Editor.Editors.FileBrowser
{
    partial class FolderBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderBrowser));
            this.borderTextBox1 = new primeira.pNeuron.Editor.Editors.FileBrowser.BorderTextBox();
            this.editorBaseButton1 = new primeira.pNeuron.Editor.EditorBaseButton();
            this.SuspendLayout();
            // 
            // borderTextBox1
            // 
            this.borderTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.borderTextBox1.Location = new System.Drawing.Point(50, 10);
            this.borderTextBox1.Name = "borderTextBox1";
            this.borderTextBox1.Padding = new System.Windows.Forms.Padding(1);
            this.borderTextBox1.Size = new System.Drawing.Size(260, 15);
            this.borderTextBox1.TabIndex = 2;
            // 
            // editorBaseButton1
            // 
            this.editorBaseButton1.Image = ((System.Drawing.Image)(resources.GetObject("editorBaseButton1.Image")));
            this.editorBaseButton1.Location = new System.Drawing.Point(10, 10);
            this.editorBaseButton1.Name = "editorBaseButton1";
            this.editorBaseButton1.ShowFocus = true;
            this.editorBaseButton1.ShowLabel = false;
            this.editorBaseButton1.Size = new System.Drawing.Size(29, 27);
            this.editorBaseButton1.TabIndex = 0;
            this.editorBaseButton1.Text = "editorBaseButton1";
            this.editorBaseButton1.UseVisualStyleBackColor = true;
            // 
            // FolderBrowser
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.borderTextBox1);
            this.Controls.Add(this.editorBaseButton1);
            this.Name = "FolderBrowser";
            this.Size = new System.Drawing.Size(322, 46);
            this.ResumeLayout(false);

        }

        #endregion

        private EditorBaseButton editorBaseButton1;
        private BorderTextBox borderTextBox1;
    }
}
