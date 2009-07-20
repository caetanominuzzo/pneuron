using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;
using primeira.pNeuron.Editor.Business;

namespace primeira.pNeuron.Editor.Components
{
    public class TabButton : Button, ITabButton
    {
        private Image m_image = Image.FromFile(@"C:\Users\caetano.CWIPOA\Pictures\button.bmp");

        private static Image _defaultSelectedImage = Image.FromFile(@"C:\Users\caetano.CWIPOA\Pictures\tab_selected.png");

        private static Image _defaultUnselectedImage = Image.FromFile(@"C:\Users\caetano.CWIPOA\Pictures\tab_unselected.png");

        private Image _selectedImage = _defaultSelectedImage;

        private Image _unselectedImage = _defaultUnselectedImage;

        public Image SelectedImage
        {
            get { return _selectedImage; }
            set { _selectedImage = value; }
        }

        public Image UnselectedImage
        {
            get { return _unselectedImage; }
            set { _unselectedImage = value; }
        }



        public TabButton() : base()
        {
            InitializeComponent();

            this.Cursor = Cursors.Hand;
            this.Dock = DockStyle.Left;
            this.Width = 150;
            this.MaximumSize = new Size(150, 40);
            this.BackgroundImage = Unselectedimage;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Font = new Font(SystemFonts.CaptionFont.FontFamily, 9);
            this.ForeColor = Color.Gray;
            this.UseCompatibleTextRendering = false;
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
            this.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.TextAlign = ContentAlignment.MiddleCenter;
            this.AutoEllipsis = false;
            this.ResumeLayout(false);

        }

    }
}
