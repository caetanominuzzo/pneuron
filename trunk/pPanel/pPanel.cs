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
        private Graphics m_graphics;

        private bool m_moving = false;
        private bool m_drawing = false;

        private Point m_realLocation;

        public pPanel(Graphics g)
        {
            m_groups = new List<int>(MAX_GROUP_NUMBER);
            m_graphics = g;
        }

        public Pen GetPenStyle()
        {
            pColorBase p;

            if (Groups.Count > 0)
                p = pColors.Colors[Groups[0]];
            else
                p = pColors.Colors[0];

            Pen pen = p.Pen;

            if (Selected)
            {
                pen = p.SelectedPen;
            }
            else
            {
                pen = p.Pen;
            }

            return pen;
        }

        public Brush GetBrushtyle()
        {
            pColorBase p;

            if (Groups.Count > 0)
                p = pColors.Colors[Groups[0]];
            else
                p = pColors.Colors[0];

            Brush brush = p.Brush;

            if (Highlighted)
            {
                brush = p.SelectedBrush;
            }
            else
            {
                brush = p.Brush;
            }

            return brush;
        }

        //This method Clean the previous Location
        //Set the new Location thus calls Draw()
        public void MoveAndDraw(Point p)
        {
        }
        
        public void Draw()
        {
            Draw(m_graphics);
        }

        public void Draw(Graphics g)
        {
            if (!m_drawing)
            {
                m_drawing = true;
                Brush brush = GetBrushtyle();
                Pen pen = GetPenStyle();

                //g.FillEllipse(new SolidBrush(Color.FromArgb(200, Color.White)),
                //            Bounds.Left,
                //            Bounds.Top,
                //            Bounds.Width,
                //            Bounds.Height);

                g.FillEllipse(brush, Bounds.Left,
                               Bounds.Top,
                               Bounds.Width,
                               Bounds.Height);

                g.DrawEllipse(pen, Bounds.Left + (pen.Width / 2),
                                   Bounds.Top + (pen.Width / 2),
                                   Bounds.Width - (pen.Width),
                                   Bounds.Height - (pen.Width));

                string s = "{}";
                Font f = new Font("Arial", 20, FontStyle.Bold, GraphicsUnit.Pixel, 1, true);
                g.DrawString(s, f, new SolidBrush(pen.Color),
                    -(g.MeasureString(s, f).Width / 2) + Bounds.Left + Bounds.Width / 2,
                    -(g.MeasureString(s, f).Height / 2) + Bounds.Top + Bounds.Height / 2);

                m_drawing = false;
            }
        }


        public Point Location
        {
            get
            {
                return base.Location;
            }
            set
            {
                base.Location = value;
                base.Width += GetPerspective(Location).Y / 2;
                base.Height += GetPerspective(Location).Y / 2;
                base.Top -= GetPerspective(Location).Y / 2;
                base.Left -= GetPerspective(Location).Y / 2;
            }
        }

        public Point CalculatedLocation
        {
            get
            {
                return base.Location;
            }
            set
            {
                if (!m_moving)
                {
                    if (value == m_realLocation)
                        return;
                    m_moving = true;
                    Rectangle r = base.Bounds;
                    r.Inflate(1, 1);
                    
                    m_realLocation = value;

                    base.Location = value;

                    base.Width += GetPerspective().Y /2 - GetPerspective(r.Location).Y/2;
                    base.Height += GetPerspective().Y / 2 - GetPerspective(r.Location).Y / 2;
                    base.Top -= GetPerspective().Y / 2 + GetPerspective(r.Location).Y / 2;
                    base.Left -= GetPerspective().Y / 2 + GetPerspective(r.Location).Y / 2;


                    Parent.Invalidate(r);
                    Draw();
                    m_moving = false;
                }

                
            }
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
           return GetPerspective(m_realLocation);
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
