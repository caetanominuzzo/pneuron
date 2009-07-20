using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;

namespace primeira.pNeuron.Editor
{
    public class TabButton : Button
    {
        private Image m_image = Image.FromFile(@"C:\Users\caetano.CWIPOA\Pictures\button.bmp");

        public TabButton() : base()
        {
            InitializeComponent();

            this.Cursor = Cursors.Hand;
            this.Dock = DockStyle.Left;
            this.Width = 135;
            this.MaximumSize = new Size(200, 40);
            this.BackgroundImage = Unselectedimage;
        }

        public static Image Selectedimage = Image.FromFile(@"C:\Users\caetano.CWIPOA\Pictures\tab_selected.png");

        public static Image Unselectedimage = Image.FromFile(@"C:\Users\caetano.CWIPOA\Pictures\tab_unselected.png");

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TabButton
            // 
            this.FlatAppearance.BorderSize = 0;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
            this.Size = new System.Drawing.Size(150, 40);
            this.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.TextAlign = ContentAlignment.MiddleCenter;
            this.AutoEllipsis = true;
            this.ResumeLayout(false);

        }

    }
}
