namespace pShortcutManagerTest
{
    partial class Form1
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node5");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node10");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Node11");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node14");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Node15");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Node12", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Node13");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Node9", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Node7", new System.Windows.Forms.TreeNode[] {
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Node8");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Node6", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Node2");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Node4");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Node3", new System.Windows.Forms.TreeNode[] {
            treeNode14});
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Node1", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode15});
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Node20");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Node21");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Node22");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Node23");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Node24");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Node15", new System.Windows.Forms.TreeNode[] {
            treeNode17,
            treeNode18,
            treeNode19,
            treeNode20,
            treeNode21});
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Node16");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Node17");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Node18");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Node19");
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Node10", new System.Windows.Forms.TreeNode[] {
            treeNode22,
            treeNode23,
            treeNode24,
            treeNode25,
            treeNode26});
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("Node11");
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("Node12");
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("Node13");
            System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("Node14");
            System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("Node6", new System.Windows.Forms.TreeNode[] {
            treeNode27,
            treeNode28,
            treeNode29,
            treeNode30,
            treeNode31});
            System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("Node7");
            System.Windows.Forms.TreeNode treeNode34 = new System.Windows.Forms.TreeNode("Node8");
            System.Windows.Forms.TreeNode treeNode35 = new System.Windows.Forms.TreeNode("Node9");
            System.Windows.Forms.TreeNode treeNode36 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode32,
            treeNode33,
            treeNode34,
            treeNode35});
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button5 = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.pHistoryManager1 = new primeira.pHistory.pHistoryManager();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Show A";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Show B";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(205, 235);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "Config";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 70);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Show C";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 99);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 105);
            this.textBox1.TabIndex = 4;
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(93, 12);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 105);
            this.textBox2.TabIndex = 5;
            this.textBox2.Enter += new System.EventHandler(this.textBox2_Enter);
            this.textBox2.Leave += new System.EventHandler(this.textBox2_Leave);
            // 
            // listBox1
            // 
            this.listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 50;
            this.listBox1.Items.AddRange(new object[] {
            "asd",
            "asd",
            "aaa"});
            this.listBox1.Location = new System.Drawing.Point(316, 41);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(120, 204);
            this.listBox1.TabIndex = 6;
            this.listBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox1_DrawItem);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(208, 159);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 7;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // treeView1
            // 
            this.treeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.treeView1.FullRowSelect = true;
            this.treeView1.Indent = 15;
            this.treeView1.ItemHeight = 25;
            this.treeView1.Location = new System.Drawing.Point(93, 20);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node5";
            treeNode1.Text = "Node5";
            treeNode2.Name = "Node10";
            treeNode2.Text = "Node10";
            treeNode3.Name = "Node11";
            treeNode3.Text = "Node11";
            treeNode4.Name = "Node14";
            treeNode4.Text = "Node14";
            treeNode5.Name = "Node15";
            treeNode5.Text = "Node15";
            treeNode6.Name = "Node12";
            treeNode6.Text = "Node12";
            treeNode7.Name = "Node13";
            treeNode7.Text = "Node13";
            treeNode8.Name = "Node9";
            treeNode8.Text = "Node9";
            treeNode9.Name = "Node7";
            treeNode9.Text = "Node7";
            treeNode10.Name = "Node8";
            treeNode10.Text = "Node8";
            treeNode11.Name = "Node6";
            treeNode11.Text = "Node6";
            treeNode12.Name = "Node0";
            treeNode12.Text = "Node0";
            treeNode13.Name = "Node2";
            treeNode13.Text = "Node2";
            treeNode14.Name = "Node4";
            treeNode14.Text = "Node4";
            treeNode15.Name = "Node3";
            treeNode15.Text = "Node3";
            treeNode16.Name = "Node1";
            treeNode16.Text = "Node1";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode16});
            this.treeView1.ShowLines = false;
            this.treeView1.ShowPlusMinus = false;
            this.treeView1.ShowRootLines = false;
            this.treeView1.Size = new System.Drawing.Size(170, 209);
            this.treeView1.TabIndex = 9;
            this.treeView1.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treeView1_DrawNode);
            // 
            // pHistoryManager1
            // 
            this.pHistoryManager1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.pHistoryManager1.FullRowSelect = true;
            this.pHistoryManager1.Indent = 15;
            this.pHistoryManager1.ItemHeight = 25;
            this.pHistoryManager1.Location = new System.Drawing.Point(279, 3);
            this.pHistoryManager1.Name = "pHistoryManager1";
            treeNode17.Name = "Node20";
            treeNode17.Text = "Node20";
            treeNode18.Name = "Node21";
            treeNode18.Text = "Node21";
            treeNode19.Name = "Node22";
            treeNode19.Text = "Node22";
            treeNode20.Name = "Node23";
            treeNode20.Text = "Node23";
            treeNode21.Name = "Node24";
            treeNode21.Text = "Node24";
            treeNode22.Name = "Node15";
            treeNode22.Text = "Node15";
            treeNode23.Name = "Node16";
            treeNode23.Text = "Node16";
            treeNode24.Name = "Node17";
            treeNode24.Text = "Node17";
            treeNode25.Name = "Node18";
            treeNode25.Text = "Node18";
            treeNode26.Name = "Node19";
            treeNode26.Text = "Node19";
            treeNode27.Name = "Node10";
            treeNode27.Text = "Node10";
            treeNode28.Name = "Node11";
            treeNode28.Text = "Node11";
            treeNode29.Name = "Node12";
            treeNode29.Text = "Node12";
            treeNode30.Name = "Node13";
            treeNode30.Text = "Node13";
            treeNode31.Name = "Node14";
            treeNode31.Text = "Node14";
            treeNode32.Name = "Node6";
            treeNode32.Text = "Node6";
            treeNode33.Name = "Node7";
            treeNode33.Text = "Node7";
            treeNode34.Name = "Node8";
            treeNode34.Text = "Node8";
            treeNode35.Name = "Node9";
            treeNode35.Text = "Node9";
            treeNode36.Name = "Node0";
            treeNode36.Text = "Node0";
            this.pHistoryManager1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode36});
            this.pHistoryManager1.ShowLines = false;
            this.pHistoryManager1.ShowPlusMinus = false;
            this.pHistoryManager1.ShowRootLines = false;
            this.pHistoryManager1.Size = new System.Drawing.Size(215, 255);
            this.pHistoryManager1.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 270);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.pHistoryManager1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button5;
        private primeira.pHistory.pHistoryManager pHistoryManager1;
        private System.Windows.Forms.TreeView treeView1;
    }
}

