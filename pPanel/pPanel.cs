using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using primeira.pNeuron;

namespace primeira.pNeuron
{

    public enum Operation
    {
        Move,
        Resize
    }


    public class pPanel : PictureBox
    {

        private const int MAX_GROUP_NUMBER = 10; //0 -- 9

        private Point m_mousePositionOnDown;
        public Nullable<Operation> MouseOperation = null;
        private List<int> m_groups;


        public pPanel()
        {
            m_groups = new List<int>(MAX_GROUP_NUMBER);
        }

        public List<int> Groups
        {
            get { return m_groups; }
            set { m_groups = value; }
        }

        private bool m_highlighed = false;

        public bool Highlighted
        {
            get { return m_highlighed; }
            set { m_highlighed = value; }
        }

        private bool m_selected;

        public bool Selected
        {
            get { return m_selected; }
            set { m_selected = value; }
        }

        public Point MousePositionOnDown
        {
            get { return m_mousePositionOnDown; }
            set { m_mousePositionOnDown = PointToClient(Parent.PointToScreen(value));  }
        }



/*
        protected override void OnPaint(PaintEventArgs e)
        {
             base.OnPaint(e);
             return;
            Graphics g = e.Graphics;
        //    g.FillRectangle(new SolidBrush(Color.Yellow), Bounds); 


            string sPath= @"C:\Documents and Settings\Caetano\My Documents\Visual Studio 2005\Projects\Primeira\pNeuron\pImages\pPanel\";
            
            g.DrawImage(Image.FromFile(sPath + "leftTop.gif"), 0, 0);
            g.DrawImage(Image.FromFile(sPath + "rightTop.gif"), Width - 10, 0);

            g.DrawImage(Image.FromFile(sPath + "leftBottom.gif"), 0, Height - 25);
            g.DrawImage(Image.FromFile(sPath + "rightBottom.gif"), Width - 10, Height - 25);


            Image m = Image.FromFile(sPath + "Top.gif");

            g.DrawImage(m, new Rectangle(new Point(m.Width, 0),
          new Size(Width, m.Height)));

            m = Image.FromFile(sPath + "Bottom.gif");

            g.DrawImage(m, new Rectangle(new Point(m.Width, Height-m.Height),
          new Size(Width, m.Height)));

            m = Image.FromFile(sPath + "left.gif");

            g.DrawImage(m, new Rectangle(new Point(0, m.Height),
          new Size(m.Width, Height - m.Height * 2)));

            m = Image.FromFile(sPath + "right.gif");

            g.DrawImage(m, new Rectangle(new Point(Width - m.Width , m.Height),
          new Size(10, Height - m.Height * 2)));


            if (m_displayName != "")
            {
                Size s = new Size(pUtils.MeasureDisplayString(g, m_displayName, SystemFonts.MenuFont));
                g.DrawString(m_displayName, SystemFonts.MenuFont,
              new LinearGradientBrush(
         new Rectangle(new Point(10, 7), s),
         Color.DarkGray, Color.Gray, 90),
              10, 7);
            }
    
         }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            Invalidate();
        }
*/
    }
}
