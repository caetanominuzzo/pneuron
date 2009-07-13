using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;

namespace primeira.pNeuron
{
    public class EditorBaseButton : Button
    {
        private Image m_image = Image.FromFile(@"C:\Users\caetano.CWIPOA\Pictures\button.bmp");
        private bool m_showLabel = false;
        private bool m_showFocus = true;

        [Browsable(true)]
        public bool ShowFocus
        {
            get { return m_showFocus; }
            set { m_showFocus = value; }
        }

        [Browsable(true)]
        public bool ShowLabel
        {
            get { return m_showLabel; }
            set
            {
                m_showLabel = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        public Image Image
        {
            get { return m_image; }
            set { m_image = value; }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Size = m_image.Size;

            pevent.Graphics.Clear(Color.White);

            pevent.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            ImageAttributes attr = new ImageAttributes();

            attr.SetColorKey(Color.Fuchsia, Color.Fuchsia);
            Rectangle r = new Rectangle(0, 0, this.Width, this.Height);

            pevent.Graphics.DrawImage((Bitmap)m_image, r, 0, m_image.Height / 2 - this.Height / 2, this.Width, this.Height, GraphicsUnit.Pixel, attr);

            if (ShowLabel)
                pevent.Graphics.DrawString(Text, this.Font, new SolidBrush(this.ForeColor), new Point(10, this.Height / 2 - 20 / 2));

            if (this.ShowFocus && this.Focused)
            {
                r = pevent.ClipRectangle;
                r.Inflate(-1, -1);
                ControlPaint.DrawFocusRectangle(pevent.Graphics, r);
            }

        }
    }

}
