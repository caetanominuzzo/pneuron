namespace pShortcutManager.Components
{
    partial class fmShortcutConfig
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
            this.cbEscope = new System.Windows.Forms.ComboBox();
            this.btCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lbDescription = new System.Windows.Forms.Label();
            this.btOk = new System.Windows.Forms.Button();
            this.txtShortcut = new pShortcutTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbCurrently = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btAssign = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btRemove = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbShortcut = new System.Windows.Forms.ComboBox();
            this.lsCommand = new System.Windows.Forms.ListBox();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbEscope
            // 
            this.cbEscope.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEscope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEscope.FormattingEnabled = true;
            this.cbEscope.Location = new System.Drawing.Point(15, 198);
            this.cbEscope.Name = "cbEscope";
            this.cbEscope.Size = new System.Drawing.Size(162, 21);
            this.cbEscope.TabIndex = 15;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(374, 9);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Use this shortcut in";
            // 
            // lbDescription
            // 
            this.lbDescription.AutoSize = true;
            this.lbDescription.Location = new System.Drawing.Point(12, 123);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(60, 13);
            this.lbDescription.TabIndex = 13;
            this.lbDescription.Text = "Description";
            // 
            // btOk
            // 
            this.btOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btOk.Location = new System.Drawing.Point(293, 9);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 0;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // txtShortcut
            // 
            this.txtShortcut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtShortcut.Location = new System.Drawing.Point(183, 199);
            this.txtShortcut.Name = "txtShortcut";
            this.txtShortcut.Size = new System.Drawing.Size(179, 20);
            this.txtShortcut.TabIndex = 12;
            this.txtShortcut.TextChanged += new System.EventHandler(this.txtShortcut_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btCancel);
            this.panel1.Controls.Add(this.btOk);
            this.panel1.Location = new System.Drawing.Point(-8, 287);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(470, 55);
            this.panel1.TabIndex = 11;
            // 
            // cbCurrently
            // 
            this.cbCurrently.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCurrently.FormattingEnabled = true;
            this.cbCurrently.Location = new System.Drawing.Point(15, 238);
            this.cbCurrently.Name = "cbCurrently";
            this.cbCurrently.Size = new System.Drawing.Size(428, 21);
            this.cbCurrently.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Shortcut currently used by";
            // 
            // btAssign
            // 
            this.btAssign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btAssign.Location = new System.Drawing.Point(368, 199);
            this.btAssign.Name = "btAssign";
            this.btAssign.Size = new System.Drawing.Size(75, 23);
            this.btAssign.TabIndex = 8;
            this.btAssign.Text = "Assign";
            this.btAssign.UseVisualStyleBackColor = true;
            this.btAssign.Click += new System.EventHandler(this.btAssign_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(180, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Press new shortcut key";
            // 
            // btRemove
            // 
            this.btRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btRemove.Location = new System.Drawing.Point(368, 159);
            this.btRemove.Name = "btRemove";
            this.btRemove.Size = new System.Drawing.Size(75, 23);
            this.btRemove.TabIndex = 5;
            this.btRemove.Text = "&Remove";
            this.btRemove.UseVisualStyleBackColor = true;
            this.btRemove.Click += new System.EventHandler(this.btRemove_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Shorcut for selected command";
            // 
            // cbShortcut
            // 
            this.cbShortcut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbShortcut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbShortcut.FormattingEnabled = true;
            this.cbShortcut.Location = new System.Drawing.Point(15, 159);
            this.cbShortcut.Name = "cbShortcut";
            this.cbShortcut.Size = new System.Drawing.Size(347, 21);
            this.cbShortcut.TabIndex = 3;
            // 
            // lsCommand
            // 
            this.lsCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lsCommand.FormattingEnabled = true;
            this.lsCommand.Location = new System.Drawing.Point(15, 51);
            this.lsCommand.Name = "lsCommand";
            this.lsCommand.Size = new System.Drawing.Size(428, 69);
            this.lsCommand.TabIndex = 2;
            this.lsCommand.SelectedIndexChanged += new System.EventHandler(this.lsCommand_SelectedIndexChanged);
            // 
            // txtCommand
            // 
            this.txtCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommand.Location = new System.Drawing.Point(15, 25);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(428, 20);
            this.txtCommand.TabIndex = 1;
            this.txtCommand.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCommand_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Show commands containing";
            // 
            // fmShortcutConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 321);
            this.Controls.Add(this.cbEscope);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbDescription);
            this.Controls.Add(this.txtShortcut);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbCurrently);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btAssign);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btRemove);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbShortcut);
            this.Controls.Add(this.lsCommand);
            this.Controls.Add(this.txtCommand);
            this.Controls.Add(this.label1);
            this.Name = "fmShortcutConfig";
            this.Text = "fmShortcutConfig";
            this.Load += new System.EventHandler(this.pShortcutManagerEditor_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbEscope;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbDescription;
        private System.Windows.Forms.Button btOk;
        private pShortcutTextBox txtShortcut;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbCurrently;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btAssign;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btRemove;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbShortcut;
        private System.Windows.Forms.ListBox lsCommand;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.Label label1;

    }
}