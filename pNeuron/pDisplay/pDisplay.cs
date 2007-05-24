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
    public class pDisplay : primeira.pExternal.CustomAutoScrollPanel.ScrollablePanel
    {

        #region Temp

        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            Refresh();
        }


        #endregion

        #region Enums

        public enum pDisplayStatus
        {
            Idle,
            Moving,
            Linking,
            Linking_Paused,
            Selecting
        }

        #endregion

        #region Fields

        private Random m_random = new Random(1);

        private List<pPanel>[] m_groups;

        private int m_gridDistance = 25;

        private bool m_allignToGrid = false;

        private bool m_shiftKey = false;

        private bool m_ctrlKey = false;

        private TextBox m_log = new TextBox();

        private Nullable<Point> m_selectSourcePoint;

        private Nullable<Rectangle> m_lastSelectRectangleDrow;

        private List<pPanel> m_lastSelectItems;

        private StatusBar statusBar;

        private pPanel m_activeControl = null;

        private Color m_gridLineColor = Color.LightGray;

        private bool m_gridShow = false;

        Image m;

        Image mGlow;

        private pDisplayStatus m_DisplayStatus;

        private Graphics g;

        #endregion

        #region Constructor

        public pDisplay()
        {

            m = Image.FromFile("ball.gif");
            mGlow = Image.FromFile("glow.gif");

           DoubleBuffered = true;

            m_log.Dock = DockStyle.Bottom;
            m_log.Multiline = true;
            m_log.Height = 100;
            Controls.Add(m_log);

            statusBar = new StatusBar();
            Controls.Add(statusBar);

            m_lastSelectItems = new List<pPanel>();
            m_groups = new List<pPanel>[10];
            DisplayStatus = pDisplayStatus.Idle;

        }

        #endregion

        #region Properties

        public pPanel ActiveControl
        {
            get { return m_activeControl; }
            set { m_activeControl = value; }
        }

        public bool ShiftKey
        {
            get { return m_shiftKey; }
            set { m_shiftKey = value; }
        }

        public bool CtrlKey
        {
            get { return m_ctrlKey; }
            set { m_ctrlKey = value; }
        }

        public Point DisplayMousePosition
        {
            get
            {
                return new Point(
                    PointToClient(
                        MousePosition).X + AutoScrollPosition.X,
                    PointToClient(
                        MousePosition).Y + AutoScrollPosition.Y);
            }
        }

        public pDisplayStatus DisplayStatus
        {
            get { return m_DisplayStatus; }
            set
            {
                m_DisplayStatus = value;
                switch (m_DisplayStatus)
                {
                    case pDisplayStatus.Moving: Cursor = Cursors.SizeAll;
                        break;
                    case pDisplayStatus.Linking: Cursor = Cursors.PanNW;
                        break;
                    default: Cursor = Cursors.Default;
                        break;
                }

                statusBar.Text = "Status: " + DisplayStatus.ToString().Replace('_', ' ');

            }
        }

        #endregion

        #region Parent Form KeyUp event.
        public void Initialize()
        {
            Parent.KeyUp += new KeyEventHandler(Parent_KeyUp);
            Parent.KeyDown += new KeyEventHandler(Parent_KeyDown);
            g = CreateGraphics();

        }

        void Parent_KeyUp(object sender, KeyEventArgs e)
        {

            
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    ActiveControl = null;
                    DisplayStatus = pDisplayStatus.Idle;
                    break;
                case Keys.L: //Link Mode
                    DisplayStatus = pDisplayStatus.Linking_Paused;
                    break;
                case Keys.G: //Show Grid
                    m_gridShow = !m_gridShow;
                    break;
                case Keys.A:
                    if (ShiftKey)
                    {
                        foreach (Control c in Controls)
                        {
                            if (c is pPanel)
                            {
                                ((pPanel)c).Location = new Point(Convert.ToInt32((c.Left) / m_gridDistance) * m_gridDistance, Convert.ToInt32((c.Top) / m_gridDistance) * m_gridDistance);
                            }

                        }
                        Invalidate();
                    }
                    else
                        m_allignToGrid = !m_allignToGrid;
                    break;
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                case Keys.D0:

                    int iKey = Convert.ToInt16(e.KeyCode.ToString().Replace("D", ""));

                    if (CtrlKey) //Create
                    {
                        if(m_groups[iKey] == null)
                            m_groups[iKey] = new List<pPanel>();
                        else if (!ShiftKey)
                                m_groups[iKey].Clear();

                        foreach(Control c in Controls)
                        {
                            if(c is pPanel)
                                if( ((pPanel)c).Selected)
                                    m_groups[iKey].Add((pPanel)c);
                        }

                    }
                    else
                    {

                        if(!ShiftKey)
                            foreach (Control d in Controls)
                            {
                                if (d is pPanel)
                                {
                                    ((pPanel)d).Selected = false;
                                    Invalidate(((pPanel)d).Bounds);
                                }
                            }

                        if (m_groups[iKey] != null)
                            foreach (pPanel p in m_groups[iKey])
                            {
                                p.Selected = true;
                                Invalidate(p.Bounds);
                            }
                    }
                  
                    break;
            }

            if(!e.Shift)
                ShiftKey = false;

            if (!e.Control)
                CtrlKey = false;
        }

        void Parent_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.ShiftKey)
            {
                ShiftKey = true;
            }

            if (e.KeyCode == Keys.ControlKey)
            {
                CtrlKey = true;
            }
        }

        #endregion

        #region Methods

        public int NextRandom(int MinValue, int MaxValue)
        {
            return m_random.Next(MinValue, MaxValue);
        }

        public void Log(string s)
        {
            m_log.Text = s + "\r\n" + m_log.Text;
        }

        public void Add(Neuron n)
        {
            primeira.pNeuron.pPanel p = new primeira.pNeuron.pPanel();

            p.Width = NextRandom(2, 5) * 15;
            p.Height = p.Width;

            Point point;
            do
            {
                point = new Point(NextRandom(1, this.Width), NextRandom(1, this.Height));
            } while (isUsed(point));

            p.Location = point;

            p.BackColor = Color.AliceBlue;
            p.Tag = n;
            Controls.Add(p);
            p.Visible = false;
        }

        public void DrawNeuron(pPanel c, Graphics g)
        {
            float[][] ptsArray ={
                            new float[] {1, 0, 0, 0, 0},
                            new float[] {0, 1, 0, 0, 0},
                            new float[] {0, 0, 1, 0, 0},
                            new float[] {0, 0, 0, .8f, 0},
                            new float[] {0, 0, 0, 0, 1}};

            ColorMatrix clrMatrix = new ColorMatrix(ptsArray);
            ImageAttributes imgAttributes = new ImageAttributes();

            if (c.Selected)
            {
                imgAttributes.SetGamma(1.5f);
            }
            else
            {
                imgAttributes.SetColorMatrix(clrMatrix,
                ColorMatrixFlag.Default,
                ColorAdjustType.Bitmap);

            }

            g.DrawImage(
                m,
                new Rectangle(
                    c.Bounds.Left,
                    c.Bounds.Top,
                    c.Bounds.Width + GetPerspective(c.Location, c.Parent.Size).Y,
                    c.Bounds.Height + GetPerspective(c.Location, c.Parent.Size).Y),
                0, 0,
                m.Width,
                m.Height,
                GraphicsUnit.Pixel, imgAttributes);
        }

        public void DrawNeuronGlow(pPanel c, Graphics g)
        {
            float[][] ptsArray ={
                            new float[] {1, 0, 0, 0, 0},
                            new float[] {0, 1, 0, 0, 0},
                            new float[] {0, 0, 1, 0, 0},
                            new float[] {0, 0, 0, .8f, 0},
                            new float[] {0, 0, 0, 0, 1}};

            ColorMatrix clrMatrix = new ColorMatrix(ptsArray);
            ImageAttributes imgAttributes = new ImageAttributes();

            if (c.Selected)
            {
                imgAttributes.SetGamma(1.5f);
            }
            else
            {
                imgAttributes.SetColorMatrix(clrMatrix,
                ColorMatrixFlag.Default,
                ColorAdjustType.Bitmap);

            }

            g.DrawImage(mGlow,


                //new Rectangle(
                //    c.Bounds.Left,
                //    c.Bounds.Top,
                //    c.Bounds.Width + GetPerspective(c.Location, c.Parent.Size).Y,
                //    c.Bounds.Height + GetPerspective(c.Location, c.Parent.Size).Y),

                       new Rectangle(c.Bounds.Left + GetPerspective(new Point(c.Bounds.Location.X, c.Bounds.Location.Y), c.Parent.Size).X,
                                     c.Bounds.Top + c.Bounds.Height + GetPerspective(c.Bounds.Location, c.Parent.Size).Y,
                                     c.Bounds.Width + GetPerspective(c.Bounds.Location, c.Parent.Size).Y,
                                     mGlow.Height + GetPerspective(c.Bounds.Location, c.Parent.Size).Y),


                           0, 0,
                           mGlow.Width,
                           mGlow.Height,
                           GraphicsUnit.Pixel, imgAttributes);

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
            Control dd = new Control();
            dd.Location = new Point(d.X - AutoScrollPosition.X, d.Y - AutoScrollPosition.Y);
            DrawSynapse(c, dd, g);
        }

        public void DrawSynapse(Control c, Control d)
        {
            DrawSynapse(c, d, g);
        }

        private void DrawSynapse(Control c, Control d, Graphics g)
        {

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

            Pen p = new Pen(Color.Gray, 1);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            g.DrawLine(p,
                new Point(c.Bounds.Left + (c.Bounds.Width / 2), c.Bounds.Top + (c.Bounds.Height / 2)),
                new Point(d.Bounds.Left + (d.Bounds.Width / 2), d.Bounds.Top + (d.Bounds.Height / 2)));


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

        public Point GetPerspective(Point p, Size c)
        {
            // return new Point(0, 0);

            int x = Math.Min(Width, AutoScrollMinSize.Width);
            int y = Math.Min(Height, AutoScrollMinSize.Height);

            return new Point(
                Convert.ToInt16(((p.X - (x / 2)) * 0.02)),
                Convert.ToInt16(((p.Y - (y / 2)) * 0.01))
                );
        }

        #endregion

        #endregion

        #region Transversal events


        protected override void OnMouseMove(MouseEventArgs e)
        {


            if (DisplayStatus == pDisplayStatus.Selecting)
            {

                if (m_selectSourcePoint.HasValue)
                {
                    foreach (Control c in Controls)
                    {
                        if (c is pPanel)
                        {

                            if (m_lastSelectItems.Contains((pPanel)c))
                                continue;

                            if (Contains(MakeRectanglePossible(new Rectangle(m_selectSourcePoint.Value,
                                new Size(
                                DisplayMousePosition.X - m_selectSourcePoint.Value.X,
                                DisplayMousePosition.Y - m_selectSourcePoint.Value.Y)

                                )), c.Bounds, true))
                            {

                                ((pPanel)c).Selected = true;
                                m_lastSelectItems.Add((pPanel)c);

                                Invalidate(c.Bounds);
                            }
                        }
                    }
                }

                if (m_lastSelectItems.Count > 0)
                {
                    List<pPanel> pToRemove = new List<pPanel>(m_lastSelectItems.Count - 1);

                    foreach (pPanel c in m_lastSelectItems)
                    {
                        if (!Contains(MakeRectanglePossible(new Rectangle(m_selectSourcePoint.Value,
                                    new Size(
                                    DisplayMousePosition.X - m_selectSourcePoint.Value.X,
                                    DisplayMousePosition.Y - m_selectSourcePoint.Value.Y)

                                    )), c.Bounds, true))
                        {
                            ((pPanel)c).Selected = false;
                            Invalidate(c.Bounds);

                            pToRemove.Add(c);
                        }
                    }

                    foreach (pPanel c in pToRemove)
                    {
                        m_lastSelectItems.Remove(c);
                    }

                    pToRemove = null;
                }

                if (m_selectSourcePoint.HasValue)
                {
                    Point p = DisplayMousePosition;

                    Rectangle cBounds = new Rectangle(m_selectSourcePoint.Value, (Size)p);

                    cBounds = new Rectangle(cBounds.X,
                        cBounds.Y,
                        cBounds.Width - cBounds.X,
                        cBounds.Height - cBounds.Y);

                    //DrawSelect(cBounds, g);
                    //cBounds.Inflate(100, 100);
                    Invalidate(cBounds);
                }

            }
            else
                if (DisplayStatus == pDisplayStatus.Moving)
                {
                    Point p = DisplayMousePosition;

                    if (ActiveControl != null)
                    {

                        pPanel c = (pPanel)ActiveControl;
                        if (c.MouseOperation == null)
                            c.MouseOperation = Operation.Move;

                        
                        int iGridDistance = m_gridDistance;

                        if (m_allignToGrid)
                            iGridDistance = 1;

                        //Point pDelta1 = new Point(
                            
                        //    Convert.ToInt32((p.X - (c.MousePositionOnDown.X)) / iGridDistance) * iGridDistance,
                            
                        //    Convert.ToInt32((p.Y - (c.MousePositionOnDown.Y)) / iGridDistance) * iGridDistance);

                        //Point pDelta = new Point(
                        //        c.Location.X - pDelta1.X,
                        //        c.Location.Y - pDelta1.Y);

                        //c.Location = new Point(
                        //    c.Location.X + pDelta.X,
                        //    c.Location.Y + pDelta.Y);



                        //foreach (Control d in Controls)
                        //    if (d is pPanel)
                        //    {
                        //        if (((pPanel)d).Selected)
                        //            d.Location = new Point(Convert.ToInt32((p.X - (c.MousePositionOnDown.X)) / iGridDistance) * iGridDistance, Convert.ToInt32((p.Y - (c.MousePositionOnDown.Y)) / iGridDistance) * iGridDistance);
                        //    }

                        c.Location = new Point(Convert.ToInt32((p.X - (c.MousePositionOnDown.X)) / iGridDistance) * iGridDistance, Convert.ToInt32((p.Y - (c.MousePositionOnDown.Y)) / iGridDistance) * iGridDistance);

                        Invalidate();
                    }

                }
                else if (DisplayStatus == pDisplayStatus.Linking || DisplayStatus == pDisplayStatus.Linking_Paused)
                {
                    if (ActiveControl != null)
                    {
                        Invalidate();
                        DisplayStatus = pDisplayStatus.Linking;
                    }
                }

        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Point p = DisplayMousePosition;


            bool bFound = false;

            if (DisplayStatus != pDisplayStatus.Selecting)
                foreach (Control c in Controls)
                {

                    Rectangle cBounds = c.Bounds;

                    cBounds = new Rectangle(cBounds.X + AutoScrollPosition.X,
                        cBounds.Y + AutoScrollPosition.Y,
                        cBounds.Width,
                        cBounds.Height);

                    if (cBounds.Contains(p))
                    {
                        bFound = true;

                        if (c is pPanel)
                        {

                            ((Control)c).BringToFront();


                            switch (DisplayStatus)
                            {
                                case pDisplayStatus.Linking_Paused:
                                case pDisplayStatus.Linking:

                                    if (ActiveControl != null)
                                    {


                                        if (((pPanel)ActiveControl).Selected)
                                        {
                                            foreach (Control cc in Controls)
                                            {
                                                if (cc is pPanel)
                                                    if (((pPanel)cc).Selected)
                                                    {
                                                        Neuron n = (Neuron)cc.Tag;
                                                        Neuron target = (Neuron)((Control)c).Tag;

                                                        if (!n.Input.ContainsKey(target))
                                                        {
                                                            n.Input.Add(target, new NeuralFactor(new Random(1).NextDouble()));
                                                            target.Output.Add(n);
                                                            DrawSynapse(ActiveControl, (Control)c);
                                                        }
                                                        else
                                                        {
                                                            n.Input.Remove(target);
                                                            target.Output.Remove(n);
                                                        }
                                                    }


                                            }
                                        }
                                        else
                                        {
                                            Neuron n = (Neuron)ActiveControl.Tag;
                                            Neuron target = (Neuron)((Control)c).Tag;

                                            if (!n.Input.ContainsKey(target))
                                            {
                                                n.Input.Add(target, new NeuralFactor(new Random(1).NextDouble()));
                                                target.Output.Add(n);
                                                DrawSynapse(ActiveControl, (Control)c);
                                            }
                                            else
                                            {
                                                n.Input.Remove(target);
                                                target.Output.Remove(n);
                                            }
                                        }



                                        ActiveControl = (pPanel)c;
                                    }
                                    else
                                    {
                                        ActiveControl = (pPanel)c;
                                    }
                                    break;

                                case pDisplayStatus.Idle:


                                    ((pPanel)c).MousePositionOnDown = DisplayMousePosition;
                                    Log("DisplayMousePosition: " + DisplayMousePosition.ToString());
                                    Log("PointToClient(DisplayMousePosition): " + c.PointToClient(DisplayMousePosition).ToString());
                                    DisplayStatus = pDisplayStatus.Moving;
                                    ActiveControl = (pPanel)c;

                                    //if (!ShiftKey)
                                    //{
                                    //    foreach (Control d in Controls)
                                    //    {
                                    //        if (d is pPanel)
                                    //            ((pPanel)d).Selected = false;
                                    //    }
                                    //}

                                    ((pPanel)c).Selected = !((pPanel)c).Selected;
                                    Invalidate(c.Bounds);
                                    break;

                            }

                        }
                        break;
                    }

                }


            if (!bFound)
            {
                ActiveControl = null;

                if (DisplayStatus == pDisplayStatus.Linking)
                    DisplayStatus = pDisplayStatus.Linking_Paused;

                if (DisplayStatus == pDisplayStatus.Idle)
                {
                    m_selectSourcePoint = DisplayMousePosition;
                    DisplayStatus = pDisplayStatus.Selecting;
                }

                if (!ShiftKey)
                {
                    foreach (Control c in Controls)
                    {
                        if (c is pPanel)
                            ((pPanel)c).Selected = false;
                    }
                }
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (DisplayStatus == pDisplayStatus.Moving)
                if (ActiveControl != null)
                {
                    pPanel c = ((pPanel)ActiveControl);
                    DisplayStatus = pDisplayStatus.Idle;

                    c.MouseOperation = null;
                    ActiveControl = null;
                }
            if (DisplayStatus == pDisplayStatus.Selecting)
            {
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

        }





        #endregion

        #region Paint

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawLines(e);

            //g.FillRectangle(Brushes.White, 0, 0, 200, 30);

            //g.DrawString(DisplayMousePosition.X.ToString() + ":" + DisplayMousePosition.Y.ToString(), SystemFonts.DefaultFont, Brushes.Black, 0, 0);
            //if (m_selectSourcePoint.HasValue)
            //    g.DrawString(m_selectSourcePoint.Value.X.ToString() + ":" + m_selectSourcePoint.Value.Y.ToString(), SystemFonts.DefaultFont, Brushes.Black, 100, 0);

            Rectangle r = e.ClipRectangle;


            #region GLOW
            if(false)
            foreach (Control c in Controls)
            {
               

                if (Contains(r, c.Bounds, true))
                {


                    DrawNeuronGlow((pPanel)c, g);

                    //ImageAttributes imgAttributes = new ImageAttributes();




                    //e.Graphics.DrawImage(mGlow,
                    //    new Rectangle(cBounds.Left + GetPerspective(new Point(cBounds.Location.X, cBounds.Location.Y), c.Parent.Size).X,
                    //                  cBounds.Top + cBounds.Height + GetPerspective(cBounds.Location, c.Parent.Size).Y,
                    //                  cBounds.Width + GetPerspective(cBounds.Location, c.Parent.Size).Y,
                    //                  mGlow.Height + GetPerspective(cBounds.Location, c.Parent.Size).Y),


                    //        0, 0,
                    //        mGlow.Width,
                    //        mGlow.Height,
                    //        GraphicsUnit.Pixel, imgAttributes);


                   

                }
            }
          #endregion

            foreach (Control c in Controls)
            {
                Rectangle cBounds = c.Bounds;

                if (c is primeira.pNeuron.pPanel)
                {
                    foreach (Neuron n in ((Neuron)c.Tag).Input.Keys)
                    {
                        foreach (Control d in Controls)
                        {
                            if (n == ((Neuron)d.Tag))
                            {
                                if (Contains(r, MakeRectanglePossible(c.Bounds, d.Bounds), true))
                                    DrawSynapse(c, d, e.Graphics);
                            }
                        }
                    }
                }

            }



            if (DisplayStatus == pDisplayStatus.Linking)
            {
                DrawSynapse(ActiveControl, DisplayMousePosition, e.Graphics);
            }

            foreach (Control c in Controls)
            {
                if (Contains(r, c.Bounds, true))
                {
                    if (c is pPanel)
                        DrawNeuron((pPanel)c, e.Graphics);
                }
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
                    Invalidate(); //TODO:BOUNDS
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
                Pen p = new Pen(m_gridLineColor);
                int w = e.ClipRectangle.Width;
                int h = e.ClipRectangle.Height;

                Graphics g = e.Graphics;

                for (int i = w / 2; i < w; i += m_gridDistance)
                {
                    g.DrawLine(p, i - (GetPerspective(new Point(i, 0), Parent.Size).X * 1), 0, i + (GetPerspective(new Point(i, 0), Parent.Size).X * 1), Height);

                    g.DrawLine(p, w / 2 - i + w / 2 + (GetPerspective(new Point(i, 0), Parent.Size).X * 1), 0, w / 2 - i + w / 2 - (GetPerspective(new Point(i, 0), Parent.Size).X * 1), Height);
                }


                for (int j = 0; j < h; j += m_gridDistance)
                {

                    g.DrawLine(p, 0, j, Width, j);
                }

                //  DrawCentralLines(g);
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

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            Invalidate();
        }

        #endregion
    }
}
