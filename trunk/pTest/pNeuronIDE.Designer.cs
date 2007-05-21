namespace primeira.pNeuron
{
    partial class pNeuronIDE
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pNeuronIDE));
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.toolBox1 = new Silver.UI.ToolBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pDisplay1 = new primeira.pNeuron.pDisplay();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Tag";
            this.dataGridViewTextBoxColumn1.HeaderText = "Tag";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(63, 17);
            this.toolStripStatusLabel1.Text = "Status: Idle";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pDisplay1);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(4, 4, 2, 4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(2, 4, 4, 4);
            this.splitContainer1.Size = new System.Drawing.Size(588, 422);
            this.splitContainer1.SplitterDistance = 420;
            this.splitContainer1.TabIndex = 8;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(2, 4);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.treeView1);
            this.splitContainer2.Panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.splitContainer2.Size = new System.Drawing.Size(158, 414);
            this.splitContainer2.SplitterDistance = 179;
            this.splitContainer2.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.ItemHeight = 18;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(158, 177);
            this.treeView1.TabIndex = 0;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 2);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.propertyGrid1.Size = new System.Drawing.Size(158, 229);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.ToolbarVisible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 422);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(631, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.toolBox1);
            this.splitContainer3.Panel1.Padding = new System.Windows.Forms.Padding(4, 4, 0, 4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer3.Size = new System.Drawing.Size(631, 422);
            this.splitContainer3.SplitterDistance = 39;
            this.splitContainer3.TabIndex = 10;
            // 
            // toolBox1
            // 
            this.toolBox1.AllowDrop = true;
            this.toolBox1.AllowSwappingByDragDrop = true;
            this.toolBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolBox1.InitialScrollDelay = 500;
            this.toolBox1.ItemBackgroundColor = System.Drawing.Color.Empty;
            this.toolBox1.ItemBorderColor = System.Drawing.Color.Empty;
            this.toolBox1.ItemHeight = 20;
            this.toolBox1.ItemHoverColor = System.Drawing.SystemColors.Control;
            this.toolBox1.ItemHoverTextColor = System.Drawing.SystemColors.ControlText;
            this.toolBox1.ItemNormalColor = System.Drawing.SystemColors.Control;
            this.toolBox1.ItemNormalTextColor = System.Drawing.SystemColors.ControlText;
            this.toolBox1.ItemSelectedColor = System.Drawing.Color.White;
            this.toolBox1.ItemSelectedTextColor = System.Drawing.SystemColors.ControlText;
            this.toolBox1.ItemSpacing = 2;
            this.toolBox1.LargeItemSize = new System.Drawing.Size(64, 64);
            this.toolBox1.LayoutDelay = 10;
            this.toolBox1.Location = new System.Drawing.Point(4, 4);
            this.toolBox1.Name = "toolBox1";
            this.toolBox1.ScrollDelay = 60;
            this.toolBox1.SelectAllTextWhileRenaming = true;
            this.toolBox1.SelectedTabIndex = -1;
            this.toolBox1.ShowOnlyOneItemPerRow = false;
            this.toolBox1.Size = new System.Drawing.Size(35, 414);
            this.toolBox1.SmallItemSize = new System.Drawing.Size(32, 32);
            this.toolBox1.TabHeight = 18;
            this.toolBox1.TabHoverTextColor = System.Drawing.SystemColors.ControlText;
            this.toolBox1.TabIndex = 1;
            this.toolBox1.TabNormalTextColor = System.Drawing.SystemColors.ControlText;
            this.toolBox1.TabSelectedTextColor = System.Drawing.SystemColors.ControlText;
            this.toolBox1.TabSpacing = 1;
            this.toolBox1.UseItemColorInRename = false;
            this.toolBox1.Click += new System.EventHandler(this.toolBox1_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "0.gif");
            this.imageList1.Images.SetKeyName(1, "1.gif");
            this.imageList1.Images.SetKeyName(2, "2.gif");
            this.imageList1.Images.SetKeyName(3, "3.gif");
            this.imageList1.Images.SetKeyName(4, "4.gif");
            this.imageList1.Images.SetKeyName(5, "5.gif");
            this.imageList1.Images.SetKeyName(6, "6.gif");
            this.imageList1.Images.SetKeyName(7, "7.gif");
            this.imageList1.Images.SetKeyName(8, "8.gif");
            this.imageList1.Images.SetKeyName(9, "9.gif");
            this.imageList1.Images.SetKeyName(10, "10.gif");
            // 
            // pDisplay1
            // 
            this.pDisplay1.AutoScroll = true;
            this.pDisplay1.AutoScrollHorizontalMaximum = 100;
            this.pDisplay1.AutoScrollHorizontalMinimum = 0;
            this.pDisplay1.AutoScrollHPos = 0;
            this.pDisplay1.AutoScrollVerticalMaximum = 100;
            this.pDisplay1.AutoScrollVerticalMinimum = 0;
            this.pDisplay1.AutoScrollVPos = 0;
            this.pDisplay1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pDisplay1.BackColor = System.Drawing.Color.White;
            this.pDisplay1.Bezier = true;
            this.pDisplay1.CtrlKey = false;
            this.pDisplay1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pDisplay1.DisplayStatus = primeira.pNeuron.pDisplay.pDisplayStatus.Idle;
            this.pDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pDisplay1.EnableAutoScrollHorizontal = true;
            this.pDisplay1.EnableAutoScrollVertical = true;
            this.pDisplay1.Location = new System.Drawing.Point(4, 4);
            this.pDisplay1.Name = "pDisplay1";
            this.pDisplay1.ShiftKey = false;
            this.pDisplay1.Size = new System.Drawing.Size(414, 414);
            this.pDisplay1.TabIndex = 0;
            this.pDisplay1.VisibleAutoScrollHorizontal = true;
            this.pDisplay1.VisibleAutoScrollVertical = true;
            this.pDisplay1.OnDisplayStatusChange += new primeira.pNeuron.pDisplay.DisplayStatusChangeDelegate(this.pDisplay1_OnDisplayStatusChange);
            this.pDisplay1.OnSelectedPanelsChange += new primeira.pNeuron.pDisplay.SelectedPanelsChangeDelegate(this.pDisplay1_OnSelectedPanelsChange);
            this.pDisplay1.OnTreeViewChange += new primeira.pNeuron.pDisplay.TreeViewChangeDelegate(this.pDisplay1_OnTreeViewChange);
            // 
            // pNeuronIDE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer3);
            this.Controls.Add(this.statusStrip1);
            this.Name = "pNeuronIDE";
            this.Size = new System.Drawing.Size(631, 444);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
//        private primeira.pNeuron.pDisplay pDisplay1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private Silver.UI.ToolBox toolBox1;
        private pDisplay pDisplay1;
        private System.Windows.Forms.ImageList imageList1;
    }
}
