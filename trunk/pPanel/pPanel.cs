using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using primeira.pNeuron;
using System.ComponentModel;

namespace primeira.pNeuron
{

    public enum Operation
    {
        Move,
        Resize
    }


    public class pPanel : Control
    {

        private const int MAX_GROUP_NUMBER = 10; //0 -- 9
        private Point m_mousePositionOnDown;
        private List<int> m_groups;
        private Graphics m_graphics;

        public pPanel(Graphics g)
        {
            m_groups = new List<int>(MAX_GROUP_NUMBER);
            m_graphics = g;
        }

        public Pen[] GetPenStyle()
        {
            pColorBase p;

            List<pColorBase> lp = new List<pColorBase>();
            List<Pen> lpp = new List<Pen>();

            foreach (int i in Groups)
            {
                lp.Add(pColors.Colors[i]);
                lpp.Add(pColors.Colors[i].Pen);
            }

            if (lp.Count == 0)
            {
                lp.Add(pColors.Colors[0]);
                lpp.Add(pColors.Colors[0].Pen);
            }

            for(int i = 0; i <lp.Count; i++)
            {
                if (Selected)
                {
                    lpp[i] = lp[i].SelectedPen;
                }
                else
                {
                    lpp[i] = lp[i].Pen;
                }
            }

            return lpp.ToArray();
        }

        public Brush[] GetBrushtyle()
        {
            pColorBase p;

            List<pColorBase> lp = new List<pColorBase>();
            List<Brush> lpp = new List<Brush>();

            foreach (int i in Groups)
            {
                lp.Add(pColors.Colors[i]);
                lpp.Add(pColors.Colors[i].Brush);
            }

            if (lp.Count == 0)
            {
                lp.Add(pColors.Colors[0]);
                lpp.Add(pColors.Colors[0].Brush);
            }

            for (int i = 0; i < lp.Count; i++)
            {
                if (Highlighted)
                {
                    lpp[i] = lp[i].SelectedBrush;
                }
                else
                {
                    lpp[i] = lp[i].Brush;
                }
            }

            return lpp.ToArray();
        }

        public void Draw()
        {
            Draw(m_graphics);
        }

        public void Draw(Graphics g)
        {

                Brush[] brush = GetBrushtyle();
                Pen[] pen = GetPenStyle();



                int iCount = pen.Length;

                int iRad = 360 / iCount;

                int iStart = 0;

                //g.FillEllipse(brush[0],
                //                   Bounds.Left,
                //                   Bounds.Top,
                //                   Bounds.Width,
                //                   Bounds.Height);
                //             //      10,
                //               //    160);

                //g.DrawEllipse(pen[0], Bounds.Left + (pen[0].Width),
                //                   Bounds.Top + (pen[0].Width),
                //                   Bounds.Width - (pen[0].Width * 2),
                //                   Bounds.Height - (pen[0].Width * 2));


                //return;
                for (int i = 0; i < pen.Length; i++)
                {
                    g.DrawPie(pen[i], Bounds.Left + (pen[i].Width),
                                     Bounds.Top + (pen[i].Width),
                                     Bounds.Width - (pen[i].Width * 2),
                                     Bounds.Height - (pen[i].Width * 2),
                                                                 iStart,
                             iRad);   



                    //g.FillEllipse(new SolidBrush(Color.FromArgb(200, Color.White)),
                    //        Bounds.Left + 4,
                    //        Bounds.Top + 4,
                    //        Bounds.Width - 8,
                    //        Bounds.Height - 8);


                    g.FillPie(brush[i],
                               Bounds.Left,
                               Bounds.Top,
                               Bounds.Width,
                               Bounds.Height,
                               iStart,
                               iRad);



                  
                    iStart += iRad;
                }
                //string s = "{}";
                //Font f = new Font("Arial", 20, FontStyle.Bold, GraphicsUnit.Pixel, 1, true);
                //g.DrawString(s, f, new SolidBrush(pen.Color),
                //    -(g.MeasureString(s, f).Width / 2) + Bounds.Left + Bounds.Width / 2,
                //    -(g.MeasureString(s, f).Height / 2) + Bounds.Top + Bounds.Height / 2);


        }

        public Point GetPerspective(Point p)
        {
            int x = Math.Min(Parent.Width, ((ScrollableControl)Parent).AutoScrollMinSize.Width);
            int y = Math.Min(Parent.Height, ((ScrollableControl)Parent).AutoScrollMinSize.Height);

            return new Point(
                Convert.ToInt16(((p.X - (x / 2)) * 0.02 * 5)),
                Convert.ToInt16(((p.Y - (y / 2)) * 0.02 * 5))
                );
        }

        public Point GetPerspective()
        {
           return GetPerspective(Location);
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
        //    m_graphics.FillRectangle(new SolidBrush(Color.Yellow), Bounds); 


            string sPath= @"C:\Documents and Settings\Caetano\My Documents\Visual Studio 2005\Projects\Primeira\pNeuron\pImages\pPanel\";
            
            m_graphics.DrawImage(Image.FromFile(sPath + "leftTop.gif"), 0, 0);
            m_graphics.DrawImage(Image.FromFile(sPath + "rightTop.gif"), Width - 10, 0);

            m_graphics.DrawImage(Image.FromFile(sPath + "leftBottom.gif"), 0, Height - 25);
            m_graphics.DrawImage(Image.FromFile(sPath + "rightBottom.gif"), Width - 10, Height - 25);


            Image m = Image.FromFile(sPath + "Top.gif");

            m_graphics.DrawImage(m, new Rectangle(new Point(m.Width, 0),
          new Size(Width, m.Height)));

            m = Image.FromFile(sPath + "Bottom.gif");

            m_graphics.DrawImage(m, new Rectangle(new Point(m.Width, Height-m.Height),
          new Size(Width, m.Height)));

            m = Image.FromFile(sPath + "left.gif");

            m_graphics.DrawImage(m, new Rectangle(new Point(0, m.Height),
          new Size(m.Width, Height - m.Height * 2)));

            m = Image.FromFile(sPath + "right.gif");

            m_graphics.DrawImage(m, new Rectangle(new Point(Width - m.Width , m.Height),
          new Size(10, Height - m.Height * 2)));


            if (m_displayName != "")
            {
                Size s = new Size(pUtils.MeasureDisplayString(g, m_displayName, SystemFonts.MenuFont));
                m_graphics.DrawString(m_displayName, SystemFonts.MenuFont,
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
