using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using primeira.pNeuron.Core;
using System.Drawing.Imaging;
using System.Reflection;

namespace primeira.pNeuron
{
    public partial class pDisplay : primeira.pExternal.CustomAutoScrollPanel.ScrollablePanel
    {

        #region Temp

        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            Refresh();
        }


        #endregion

        #region events

        public delegate void SelectedPanelsChangeDelegate();
        public event SelectedPanelsChangeDelegate OnSelectedPanelsChange;

        public delegate void DisplayStatusChangeDelegate();
        public event DisplayStatusChangeDelegate OnDisplayStatusChange;

        public delegate void TreeViewChangeDelegate(int iKey);
        public event TreeViewChangeDelegate OnTreeViewChange;

        #endregion

        #region Enums

        public enum pDisplayStatus
        {
            Idle,
            Moving,
            Linking,
            Linking_Paused,
            Selecting,
            AddNeuron
        }

        #endregion

        #region Fields

        private Random m_random = new Random(1);

        private List<pPanel>[] m_groups;

        private int m_gridDistance = 25;

        private bool m_allignToGrid = false;

        private bool m_shiftKey = false;

        private bool m_ctrlKey = false;

        public TextBox Logger = new TextBox();

        private Nullable<Point> m_selectSourcePoint; //Initial point of the select rectangle

        private Nullable<Rectangle> m_lastSelectRectangleDrow; //Used to invalidate when the select rectangle closes

        private List<pPanel> m_lastSelectItems;

        private Color m_gridLineColor = Color.LightGray;

        private bool m_gridShow = false;

        private bool m_bezier = true;

        public bool Bezier
        {
            get { return m_bezier; }
            set { m_bezier = value; }
        }

        private pDisplayStatus m_DisplayStatus;

        private Graphics m_graphics;

        #endregion

        #region Constructor

        public pDisplay()
        {

            BackColor = Color.White;

            DoubleBuffered = true;

            Logger.Dock = DockStyle.Bottom;
            Logger.Multiline = true;
            Logger.Height = 100;
            Logger.Visible = false;
            Controls.Add(Logger);


            m_lastSelectItems = new List<pPanel>();
            m_groups = new List<pPanel>[10];
            DisplayStatus = pDisplayStatus.Idle;

            pPanels = new List<pPanel>();
            m_graphics = CreateGraphics();

            for(int i=0;i<m_groups.Length;i++)
                m_groups[i] = new List<pPanel>();

        }

        #endregion

        #region Properties

        public bool ShiftKey
        {
            get { return m_shiftKey; }
            set { m_shiftKey = value; }
        }

        public bool CtrlKey
        {
            get { return m_ctrlKey; }
            set
            {
                m_ctrlKey = value;
                RefreshHighlight();
                Invalidate();
            }
        }

        public Point DisplayMousePosition
        {
            get
            {
                return new Point(
                    PointToClient(
                        MousePosition).X,
                    PointToClient(
                        MousePosition).Y);
            }
        }

        public pDisplayStatus DisplayStatus
        {
            get { return m_DisplayStatus; }
            set
            {
                pDisplayStatus old = m_DisplayStatus;
                m_DisplayStatus = value;
                switch (m_DisplayStatus)
                {
                    case pDisplayStatus.Moving: Cursor = Cursors.SizeAll;
                        break;
                    case pDisplayStatus.Linking: Cursor = Cursors.Cross;
                        break;
                    default: Cursor = Cursors.Default;
                        break;
                }
                if (old != m_DisplayStatus && OnDisplayStatusChange!=null)
                    OnDisplayStatusChange();
            }
        }

        #endregion

        #region Parent Form KeyUp event.
        public void Initialize()
        {

        }



        #endregion

        #region Methods

        public int NextRandom(int MinValue, int MaxValue)
        {
            return m_random.Next(MinValue, MaxValue);
        }

        public void Log(string s)
        {
            Logger.Text = s + "\r\n" + Logger.Text;
        }

        public pPanel Add(Neuron n)
        {
            primeira.pNeuron.pPanel p = new primeira.pNeuron.pPanel(m_graphics);

            p.Width = 40;// NextRandom(2, 5) * 15;
            p.Height = p.Width;

            //if (pPanels.Count == 0)
            //{
            //    p.Left = 100;
            //    p.Top = 400;
            //}
            //else
            //{
            //    p.Left = 500;
            //    p.Top = 100;
            //}

            //Point point;
            //do
            //{
            //    point = new Point(NextRandom(1, this.Width), NextRandom(1, this.Height));
            //} while (isUsed(point));
            //p.Location = new Point(Convert.ToInt32((point.X) / m_gridDistance) * m_gridDistance, Convert.ToInt32((point.Y) / m_gridDistance) * m_gridDistance);
            //p.Parent = this;
      

            p.BackColor = Color.AliceBlue;
            p.Tag = n;
            Controls.Add(p);
            pPanels.Add(p);
            p.Visible = false;
            return p;
        }

        public void DrawSelect(Rectangle cBounds, Graphics gg)
        {
            m_lastSelectRectangleDrow = cBounds;

            gg.FillRectangle(new SolidBrush(Color.FromArgb(5, Color.Blue)),

               cBounds);

            gg.DrawRectangle(new Pen(Color.FromArgb(50, Color.Blue), 1),
              cBounds);

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
                    new Point(c.Bounds.Left + (c.Bounds.Width) * signX , c.Bounds.Top + (c.Bounds.Height) * signY),

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

        #region Utils

        private Rectangle MakeRectanglePossible(Rectangle r)
        {


            int Left, Width, Top, Height;

            if (r.Width < 0)
            {
                Left = r.Left + r.Width;
                Width = r.Width * -1;
            }
            else
            {
                Left = r.Left;
                Width = r.Width;
            }
            if (r.Height < 0)
            {
                Top = r.Top + r.Height;
                Height = r.Height * -1;
            }
            else
            {
                Top = r.Top;
                Height = r.Height;
            }
            return new Rectangle(Left, Top, Width, Height);

        }

        private Rectangle ExpandRectangle(Rectangle cBounds, Rectangle dBounds)
        {

            Rectangle result = new Rectangle(cBounds.X, cBounds.Y, cBounds.Width, cBounds.Height);
            if (Contains(cBounds, dBounds, true))
                return cBounds;

            if (result.Left > dBounds.Left)
            {
                int i = dBounds.Left - result.Left;
                result = MakeRectanglePossible(new Rectangle(
                    result.Left + i,
                    result.Top,
                    result.Width - i,
                    result.Height));
            }

            if (result.Top > dBounds.Top)
            {
                int i = dBounds.Top - result.Top;
                result = MakeRectanglePossible(new Rectangle(
                    result.Left,
                    result.Top + i,
                    result.Width,
                    result.Height - i));
            }


            if (result.Left + result.Width < dBounds.Left + dBounds.Width)
            {
                int i = result.Left + result.Width - dBounds.Left - dBounds.Width;

                result = MakeRectanglePossible(new Rectangle(
                    result.X,
                    result.Y,
                    result.Width - i,
                    result.Height));
            }

            if (result.Top + result.Height < dBounds.Top + dBounds.Height)
            {
                int i = result.Top + result.Height - dBounds.Top - dBounds.Height;
                result = MakeRectanglePossible(new Rectangle(
                    result.X,
                    result.Y,
                    result.Width,
                    result.Height - i));
            }

            return result;
        }

        private Rectangle MakeRectanglePossible(Rectangle cBounds, Rectangle dBounds)
        {

            Rectangle r = new Rectangle(

                                new Point(cBounds.Left + cBounds.Width / 2,
                                          cBounds.Top + cBounds.Height / 2),

                                new Size(dBounds.Left + dBounds.Width / 2 - cBounds.Left - cBounds.Width / 2,
                                         dBounds.Top + dBounds.Height / 2 - cBounds.Top - cBounds.Height / 2)

                                         );

            int Left, Width, Top, Height;

            if (r.Width < 0)
            {
                Left = r.Left + r.Width;
                Width = r.Width * -1;
            }
            else
            {
                Left = r.Left;
                Width = r.Width;
            }
            if (r.Height < 0)
            {
                Top = r.Top + r.Height;
                Height = r.Height * -1;
            }
            else
            {
                Top = r.Top;
                Height = r.Height;
            }
            return new Rectangle(Left, Top, Width, Height);
        }

        private bool Contains(Rectangle r, Rectangle i, bool AnyPoint)
        {

            if (AnyPoint)
            {
                if (r.Contains(i.X, i.Y) ||
                    r.Contains(i.X + i.Width, i.Y) ||
                    r.Contains(i.X, i.Y + i.Height) ||
                    r.Contains(i.X + i.Width, i.Y + i.Height))

                    return true;


                for (int x = i.X; x < i.Width + i.X; x++)
                    for (int y = i.Y; y < i.Height + i.Y; y++)
                    {
                        if (r.Contains(x, y))
                            return true;
                    }

                return false;
            }
            else
                return r.Contains(i);
        }

        private MouseEventArgs GetAtControl(Control c, MouseEventArgs e)
        {
            Point p = c.PointToClient(e.Location);

            MouseEventArgs m = new MouseEventArgs(e.Button, e.Clicks, p.X, p.Y, e.Delta);
            return m;
        }

        public bool isUsed(Point p)
        {
            foreach (Control c in Controls)
            {
                if (c.Bounds.Contains(p))
                    return true;
            }
            return false;
        }


        #endregion

        #endregion

        #region Transversal events


        protected override void OnMouseMove(MouseEventArgs e)
        {

            RefreshHighlight();

            if (DisplayStatus == pDisplayStatus.Selecting)
            {

                if (m_selectSourcePoint.HasValue)
                {

                    foreach (pPanel p in pPanels)
                    {

                        if (m_lastSelectItems.Contains(p))
                            continue;

                        if (p.Selected)
                            continue;

                        if (Contains(MakeRectanglePossible(new Rectangle(m_selectSourcePoint.Value,
                            new Size(
                            DisplayMousePosition.X - m_selectSourcePoint.Value.X,
                            DisplayMousePosition.Y - m_selectSourcePoint.Value.Y)

                            )), p.Bounds, true))
                        {

                            Select(p);
                        }
                    }
                }

                if (m_lastSelectItems.Count > 0)
                {
                    foreach (pPanel p in pPanels)
                    {
                        if (!Contains(MakeRectanglePossible(new Rectangle(m_selectSourcePoint.Value,
                                    new Size(
                                    DisplayMousePosition.X - m_selectSourcePoint.Value.X,
                                    DisplayMousePosition.Y - m_selectSourcePoint.Value.Y)

                                    )), p.Bounds, true) && m_lastSelectItems.Contains(p))
                        {
                            UnSelect(p);
                            m_lastSelectItems.Remove(p);
                        }
                    }

                }

                if (m_selectSourcePoint.HasValue)
                {
                    Point p = DisplayMousePosition;

                    Rectangle cBounds = new Rectangle(m_selectSourcePoint.Value, (Size)p);

                    cBounds = new Rectangle(cBounds.X,
                        cBounds.Y,
                        cBounds.Width - cBounds.X,
                        cBounds.Height - cBounds.Y);

                    Invalidate(cBounds);
                }

            }
            else
                if (DisplayStatus == pDisplayStatus.Moving)
                {
                    Point p = DisplayMousePosition;

                    if (SelectedpPanels.Length > 0)
                    {

                        int iGridDistance = m_gridDistance;

                        if (m_allignToGrid)
                            iGridDistance = 1;

                        foreach (pPanel pp in SelectedpPanels)
                        {
                            Rectangle r = pp.Bounds;
                            //                            r.Inflate(10, 10);
                            Invalidate(r);
                            pp.Location = new Point(Convert.ToInt32(Math.Round(((double)p.X - ((double)pp.MousePositionOnDown.X)) / (double)iGridDistance)) * iGridDistance, Convert.ToInt32(Math.Round(((double)p.Y - ((double)pp.MousePositionOnDown.Y)) / (double)iGridDistance)) * iGridDistance);

                        }

                        Rectangle rArea = pPanels[0].Bounds;

                        foreach (pPanel pp in pPanels)
                        {
                            rArea = ExpandRectangle(rArea, pp.Bounds);
                        }

                        AutoScrollMinSize = new Size(
                            rArea.Left + rArea.Width,
                            rArea.Top + rArea.Height);
                        
                    }

                    Invalidate();

                }
                else if (DisplayStatus == pDisplayStatus.Linking || DisplayStatus == pDisplayStatus.Linking_Paused)
                {
                    if (SelectedpPanels.Length > 0)
                    {
                        Invalidate();
                        DisplayStatus = pDisplayStatus.Linking;
                    }
                }
                else if (DisplayStatus == pDisplayStatus.Idle)
                {
                    if (MouseButtons == MouseButtons.Left && SelectedpPanels.Length > 0)
                    {
                        DisplayStatus = pDisplayStatus.Moving;
                    }
                    else
                    {

                        RefreshHighlight();
                    }

                }

        }

        private void RefreshHighlight()
        {
            UnHighLight();

            bool bGroupHighlight = false;
            if (!CtrlKey)
            {
                foreach (List<pPanel> lp in m_groups)
                {
                    if (lp != null)
                        if (lp.Count > 0)
                            if (GetGroupRectangle(lp).Contains(DisplayMousePosition))
                            {
                                foreach (pPanel pp in lp)
                                {
                                    HighLight(pp);
                                    Invalidate(pp.Bounds);
                                }
                                bGroupHighlight = true;
                            }
                }
            }
            if (!bGroupHighlight)
            {
                foreach (pPanel p in pPanels)
                {
                    if (p.Bounds.Contains(DisplayMousePosition))
                    {
                        HighLight(p);
                        Invalidate(p.Bounds);
                        break;
                    }
                }
            }


        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Point p = DisplayMousePosition;

            #region Add Neuron

            if (DisplayStatus == pDisplayStatus.AddNeuron)
            {
                pPanel pp = this.Add(new Neuron(0));
                pp.Location = new Point(DisplayMousePosition.X - pp.Width / 2,
                                        DisplayMousePosition.Y - pp.Height / 2);
                Invalidate(pp.Bounds);
                return;
            }
                        
            #endregion

            bool bFound = false;

            if (DisplayStatus != pDisplayStatus.Selecting)
            {
                if (HighlightedpPanels.Length > 0)
                {

                    //TODO:TargetpPanel.BringToFront();
                    bFound = true;


                    switch (DisplayStatus)
                    {

                        case pDisplayStatus.Linking_Paused:
                        case pDisplayStatus.Linking:

                            if (DisplayStatus == pDisplayStatus.Linking_Paused)
                            {
                                Select(HighlightedpPanels);
                            }


                            if (SelectedpPanels.Length > 0)
                            {
                                if (HighlightedpPanels.Length > 0)
                                {
                                    foreach (pPanel pp in SelectedpPanels)
                                    {
                                        Neuron n = (Neuron)pp.Tag;

                                        foreach (pPanel ppp in HighlightedpPanels)
                                        {

                                            Neuron target = (Neuron)ppp.Tag;

                                            if (!target.Input.ContainsKey(n))
                                            {
                                                n.Output.Add(target);// .Input.Add(target, new NeuralFactor(new Random(1).NextDouble()));
                                                target.Input.Add(n, new NeuralFactor(m_random.NextDouble())); //Output.Add(n);
                                                DrawSynapse(pp, ppp);
                                            }
                                            else
                                            {
                                                target.Input.Remove(n);
                                                n.Output.Remove(target);
                                            }
                                        }
                                    }
                                }
                            }

                            break;

                        case pDisplayStatus.Idle:

                            if (!ShiftKey && Contains(HighlightedpPanels, SelectedpPanels)!=HighlightedpPanels.Length)
                                UnSelect();

                            Select(HighlightedpPanels); 

                            foreach (pPanel pp in SelectedpPanels)
                                pp.MousePositionOnDown = DisplayMousePosition;

                            break;

                    }
                }

            }

            if (!bFound)
            {
                if (DisplayStatus == pDisplayStatus.Linking)
                    DisplayStatus = pDisplayStatus.Linking_Paused;

                if (DisplayStatus == pDisplayStatus.Idle)
                {
                    m_selectSourcePoint = DisplayMousePosition;
                    DisplayStatus = pDisplayStatus.Selecting;
                }

                if (!ShiftKey)
                    UnSelect();

                UnHighLight();
            }
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (DisplayStatus == pDisplayStatus.Moving)
            {
                DisplayStatus = pDisplayStatus.Idle;
            }
            else

                if (DisplayStatus == pDisplayStatus.Selecting)
                {

                    //TODO:Review
                    if (m_selectSourcePoint.HasValue)
                    {
                        Rectangle cBounds = new Rectangle(m_selectSourcePoint.Value, (Size)DisplayMousePosition);
                        cBounds = new Rectangle(cBounds.X,
                            cBounds.Y,
                            cBounds.Width - cBounds.X,
                            cBounds.Height - cBounds.Y);

                        Invalidate(cBounds);
                    }

                    m_selectSourcePoint = null;
                    m_lastSelectItems.Clear();

                    DisplayStatus = pDisplayStatus.Idle;
                }
                else
                    if (DisplayStatus == pDisplayStatus.Idle)
                    {
//                        if (!ShiftKey)
//                            UnSelect();

//                        Select(HighlightedpPanels);
                    }

        }





        #endregion

        #region Paint

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            base.OnPaint(e);
            DrawLines(e);



            //g.FillRectangle(Brushes.White, 0, 0, 200, 30);

            //g.DrawString(DisplayMousePosition.X.ToString() + ":" + DisplayMousePosition.Y.ToString(), SystemFonts.DefaultFont, Brushes.Black, 0, 0);
            //if (m_selectSourcePoint.HasValue)
            //    g.DrawString(m_selectSourcePoint.Value.X.ToString() + ":" + m_selectSourcePoint.Value.Y.ToString(), SystemFonts.DefaultFont, Brushes.Black, 100, 0);

            Rectangle r = e.ClipRectangle;


            foreach (pPanel p in pPanels)
            {
                foreach (Neuron n in ((Neuron)p.Tag).Input.Keys)
                {
                    foreach (pPanel pp in pPanels)
                    {
                        if (n == ((Neuron)pp.Tag))
                        {
                            if (Contains(r, MakeRectanglePossible(p.Bounds, pp.Bounds), true))
                                DrawSynapse(p, pp, e.Graphics);
                        }
                    }
                }
            }



            if (DisplayStatus == pDisplayStatus.Linking)
            {

                foreach (pPanel p in SelectedpPanels)
                {

                    if (HighlightedpPanels.Length > 0)
                    {
                        foreach (pPanel pp in HighlightedpPanels)
                        {
                            DrawSynapse(pp, p, e.Graphics);
                        }
                    }
                    else
                        DrawSynapse(p, DisplayMousePosition, e.Graphics);
                }
            }


            foreach (pPanel c in pPanels)
            {
                if (Contains(r, c.Bounds, true))
                    c.Draw(e.Graphics);
            }

            if (m_selectSourcePoint.HasValue)
            {

                Point p = new Point(
                        DisplayMousePosition.X - m_selectSourcePoint.Value.X,
                        DisplayMousePosition.Y - m_selectSourcePoint.Value.Y);

                Rectangle xBounds = MakeRectanglePossible(new Rectangle(m_selectSourcePoint.Value, (Size)p));


                if (m_lastSelectRectangleDrow.HasValue)
                {

                    Rectangle rr = m_lastSelectRectangleDrow.Value;

                    Point pp = m_selectSourcePoint.Value;
                    m_selectSourcePoint = null;
                    m_lastSelectRectangleDrow = null;
                    rr.Inflate(5, 5);
                    Invalidate(rr);
                    m_selectSourcePoint = pp;
                }

                if (Contains(r, xBounds, true))
                {
                    DrawSelect(xBounds, e.Graphics);

                }
            }



        }


        private void DrawLines(PaintEventArgs e)
        {
            if (m_gridShow)
            {
                Pen pp = pColors.Red.Pen;

                Pen p = new Pen(m_gridLineColor);
                int w = e.ClipRectangle.Width;
                int h = e.ClipRectangle.Height;

                Graphics g = e.Graphics;

                for (int i = w / 2; i < w; i += m_gridDistance)
                {
                    g.DrawLine(p, i, 0, i, Height);

                    g.DrawLine(p, w / 2 - i + w / 2, 0, w / 2 - i + w / 2, Height);
                }


                for (int j = 0; j < h; j += m_gridDistance)
                {

                    g.DrawLine(p, 0, j, Width, j);
                }

                DrawCentralLines(g);
            }


        }

        private void DrawCentralLines(Graphics g)
        {

            Pen p = new Pen(Color.Red);
            int w = Width;
            int h = Height;
            g.DrawLine(p, w / 2, 0, w / 2, Height);
            g.DrawLine(p, 0, h / 2, Width, h / 2);
        }


        #endregion
    }
}

