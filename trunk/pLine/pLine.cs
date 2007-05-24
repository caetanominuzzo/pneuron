using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using primeira.pNeuron;



namespace primeira.pNeuron
{
    public class pLine : Control
    {
        private pPanel m_inputPanel;
        private pPanel m_outputPanel;

        public pLine(pPanel input, pPanel output)
        {
            m_inputPanel = input;
            m_outputPanel = output;


            this.BackColor = Color.Transparent;
            SetBounds();
            BackColor = Color.Tomato;

        }

        public pPanel InputPanel
        {
            get { return m_inputPanel; }
        }
        
        public pPanel OutputPanel
        {
            get { return m_outputPanel; }
        }

        public void DrawSynapse(Control c, Point d, Graphics g)
        {
            pPanel dd = new pPanel(g);
            dd.Location = new Point(d.X - AutoScrollPosition.X, d.Y - AutoScrollPosition.Y);
            DrawSynapse(dd, c, g);
        }

        public void DrawSynapse(Control c, Control d)
        {
            DrawSynapse(c, d, m_graphics);
        }

        private void DrawSynapse(Control d, Control c, Graphics g)
        {

            if (c.Location == d.Location)
                return;

            Rectangle cBounds = c.Bounds;

            cBounds = new Rectangle(cBounds.X + AutoScrollPosition.X,
                cBounds.Y + AutoScrollPosition.Y,
                cBounds.Width,
                cBounds.Height);


            Rectangle dBounds = d.Bounds;


            dBounds = new Rectangle(dBounds.X + AutoScrollPosition.X,
                dBounds.Y + AutoScrollPosition.Y,
                dBounds.Width,
                dBounds.Height);

            Pen p = ((pPanel)c).GetPenStyle()[0];
            p.Width = 1;
            SolidBrush b = new SolidBrush(Color.Red);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


            //hipotenusa
            double catA;
            double catB;
            double hyp;

            int signX = d.Bounds.Left + (d.Bounds.Width / 2) > c.Bounds.Left + (c.Bounds.Width / 2) ? 1 : -1;
            int signY = d.Bounds.Top + (d.Bounds.Height / 2) > c.Bounds.Top + (c.Bounds.Height / 2) ? 1 : -1;

            catA = c.Top - d.Top;
            catB = c.Left - d.Left;
            hyp = Convert.ToInt32(Math.Sqrt(Math.Pow(catA, 2) + Math.Pow(catB, 2)));

            double cos = -catA / hyp;
            double sen = -catB / hyp;

            double radXC = c.Bounds.Left + (c.Bounds.Width / 2) + (sen * c.Width / 2);
            double radYC = c.Bounds.Top + (c.Bounds.Height / 2) + (cos * c.Width / 2);

            double radXD = d.Bounds.Left + (d.Bounds.Width / 2) + (sen * d.Width / 2);
            double radYD = d.Bounds.Top + (d.Bounds.Height / 2) + (cos * d.Width / 2);

            if (m_bezier)
            {




                g.DrawBezier(p,
                    new Point((int)radXC + (-1 * signX), (int)radYC + (-1 * signY)),
                    new Point(c.Bounds.Left + (c.Bounds.Width) * signX, c.Bounds.Top + (c.Bounds.Height) * signY),

                    new Point(d.Bounds.Left + (d.Bounds.Width / 2) * -signX, d.Bounds.Top + (d.Bounds.Height / 2) * -signY),
                    new Point(d.Bounds.Left + (d.Bounds.Width / 2), d.Bounds.Top + (d.Bounds.Height / 2))

                    );
            }
            else
            {
                g.DrawLine(p,
                    new Point((int)radXC, (int)radYC),
                    new Point(d.Bounds.Left + (d.Bounds.Width / 2), d.Bounds.Top + (d.Bounds.Height / 2))
                    );
            }



            //g.DrawLine(new Pen(Color.Red, 5), 
            //    new Point(c.Bounds.Left + (c.Bounds.Width / 2), c.Bounds.Top + (c.Bounds.Height / 2)),
            //    new Point((int)sen, (int)cos));

            //g.FillEllipse(b,
            //    new Rectangle(
            //        new Point((int)radX - (signX * 4), (int)radY - (signY * 4)),
            //        new Size(signX*4, signY*4)));

            //double ArrowBaseX = d.Bounds.Left + (d.Bounds.Width / 2) + (sen * (d.Width - 15));
            //double ArrowBaseY = d.Bounds.Top + (d.Bounds.Height / 2) + (cos * (d.Width - 15));

            //for (int i = -2; i < 3; i++)
            //    for (int j = -2; j < 3; j++)
            //    {
            //        g.DrawLine(p,
            //            new Point((int)radXD, (int)radYD),
            //            new Point((int)ArrowBaseX + i, (int)ArrowBaseY + j));
            //    }


            //            g.RotateTransform(50);
            //            g.DrawLine(p, new Point(10,10), new Point(100,100));

        }
    }
}
