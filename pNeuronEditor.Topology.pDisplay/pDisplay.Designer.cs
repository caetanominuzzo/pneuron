using System.Windows.Forms;


namespace pNeuronEditor.Topology
{
    partial class pDisplay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private ContextMenuStrip pPanelMenu;
        private ToolStripMenuItem tspNeuronType;
        private ToolStripMenuItem tspDataType;
        private ContextMenuStrip mspNeuronType;
        private ToolStripMenuItem tspNeuronTypeInput;
        private ToolStripMenuItem tspNeuronTypeHidden;
        private ToolStripMenuItem tspNeuronTypeOutput;
        private ContextMenuStrip mspDataType;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem toolStripMenuItem7;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem tspNewDomain;


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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pDisplay));
            this.pPanelMenu = new System.Windows.Forms.ContextMenuStrip();
            this.tspNeuronType = new System.Windows.Forms.ToolStripMenuItem();
            this.tspDataType = new System.Windows.Forms.ToolStripMenuItem();
            this.mspNeuronType = new System.Windows.Forms.ContextMenuStrip();
            this.mspDataType = new System.Windows.Forms.ContextMenuStrip();
            this.tspNeuronTypeInput = new System.Windows.Forms.ToolStripMenuItem();
            this.tspNeuronTypeHidden = new System.Windows.Forms.ToolStripMenuItem();
            this.tspNeuronTypeOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tspNewDomain = new System.Windows.Forms.ToolStripMenuItem();
            this.pPanelMenu.SuspendLayout();
            this.mspNeuronType.SuspendLayout();
            this.mspDataType.SuspendLayout();
            this.SuspendLayout();
            // 
            // pPanelMenu
            // 
            this.pPanelMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspNeuronType,
            this.tspDataType});
            this.pPanelMenu.Name = "contextMenuStrip1";
            this.pPanelMenu.Size = new System.Drawing.Size(148, 48);
            // 
            // tspNeuronType
            // 
            this.tspNeuronType.DropDown = this.mspNeuronType;
            this.tspNeuronType.Name = "tspNeuronType";
            this.tspNeuronType.Size = new System.Drawing.Size(147, 22);
            this.tspNeuronType.Text = "NeuronType";
            // 
            // tspDataType
            // 
            this.tspDataType.DropDown = this.mspDataType;
            this.tspDataType.Name = "tspDataType";
            this.tspDataType.Size = new System.Drawing.Size(147, 22);
            this.tspDataType.Text = "tspDataType";
            // 
            // mspNeuronType
            // 
            this.mspNeuronType.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspNeuronTypeInput,
            this.tspNeuronTypeHidden,
            this.tspNeuronTypeOutput});
            this.mspNeuronType.Name = "mspNeuronType";
            this.mspNeuronType.Size = new System.Drawing.Size(120, 70);
            // 
            // mspDataType
            // 
            this.mspDataType.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripSeparator1,
            this.tspNewDomain});
            this.mspDataType.Name = "mspDataType";
            this.mspDataType.Size = new System.Drawing.Size(180, 98);
            // 
            // tspNeuronTypeInput
            // 
            this.tspNeuronTypeInput.Name = "tspNeuronTypeInput";
            this.tspNeuronTypeInput.Size = new System.Drawing.Size(119, 22);
            this.tspNeuronTypeInput.Text = "Input";
            // 
            // tspNeuronTypeHidden
            // 
            this.tspNeuronTypeHidden.Name = "tspNeuronTypeHidden";
            this.tspNeuronTypeHidden.Size = new System.Drawing.Size(119, 22);
            this.tspNeuronTypeHidden.Text = "Hidden";
            // 
            // tspNeuronTypeOutput
            // 
            this.tspNeuronTypeOutput.Name = "tspNeuronTypeOutput";
            this.tspNeuronTypeOutput.Size = new System.Drawing.Size(119, 22);
            this.tspNeuronTypeOutput.Text = "Output";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItem5.Text = "toolStripMenuItem5";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItem6.Text = "toolStripMenuItem6";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItem7.Text = "toolStripMenuItem7";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(176, 6);
            // 
            // tspNewDomain
            // 
            this.tspNewDomain.Name = "tspNewDomain";
            this.tspNewDomain.Size = new System.Drawing.Size(179, 22);
            this.tspNewDomain.Text = "New Domain";
            this.pPanelMenu.ResumeLayout(false);
            this.mspNeuronType.ResumeLayout(false);
            this.mspDataType.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}