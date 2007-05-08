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

        private Graphics m_graphics;

        #endregion

        #region Constructor

        public pDisplay()
        {

            BackColor = Color.White;

            //m = Image.FromFile("ball.gif");
            //mGlow = Image.FromFile("glow.gif");

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

            pPanels = new List<pPanel>();
            SelectedpPanels = new List<pPanel>();
            HighLightedpPanels = new List<pPanel>();

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
                ValueType
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
            m_graphics = CreateGraphics();

        }

        void Parent_KeyUp(object sender, KeyEventArgs e)
        {

            
            switch (e.KeyCode)
            {
                case Keys.S:
                    if(e.Alt)
                    foreach (pPanel p in SelectedpPanels)
                        Log(pPanels.IndexOf(p).ToString());
                    break;
                case Keys.H:
                    if (e.Alt)
                        foreach (pPanel p in HighLightedpPanels)
                            Log(pPanels.IndexOf(p).ToString());
                    break;
                case Keys.K: //Log Shift
                    m_log.Visible = !m_log.Visible;
                    break;
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
                        foreach (pPanel p in pPanels)
                            p.Location = new Point(Convert.ToInt32((p.Left) / m_gridDistance) * m_gridDistance, Convert.ToInt32((p.Top) / m_gridDistance) * m_gridDistance);
                        
                        Invalidate(); //TODO:Remove
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
                        if (SelectedpPanels.Count == 0)
                            m_groups[iKey] = null;

                        if(m_groups[iKey] == null)
                            m_groups[iKey] = new List<pPanel>();
                        else if (!ShiftKey)
                                m_groups[iKey].Clear();

                        foreach (pPanel p in SelectedpPanels)
                        {
                            m_groups[iKey].Add(p);
                            if (p.Groups.IndexOf(iKey)==-1 )
                                p.Groups.Add(iKey);

                            Invalidate(p.Bounds);

                        }

                    }
                    else
                    {

                        if (!ShiftKey)
                            UnSelect();

                        if (m_groups[iKey] != null)
                            foreach (pPanel p in m_groups[iKey])
                                Select(p);
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
            primeira.pNeuron.pPanel p = new primeira.pNeuron.pPanel(m_graphics);

            p.Width = 40;// NextRandom(2, 5) * 15;
            p.Height = p.Width;

            Point point;
            do
            {
                point = new Point(NextRandom(1, this.Width), NextRandom(1, this.Height));
            } while (isUsed(point));
            
            p.Parent = this;
            p.CalculatedLocation = point;
            p.CalculatedLocation = new Point(Convert.ToInt32((p.Left) / m_gridDistance) * m_gridDistance, Convert.ToInt32((p.Top) / m_gridDistance) * m_gridDistance);
           
            p.BackColor = Color.AliceBlue;
            p.Tag = n;
            Controls.Add(p);
            pPanels.Add(p);
            p.Visible = false;
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
            DrawSynapse(c, d, m_graphics);
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
                result =MakeRectanglePossible( new Rectangle(
                    result.Left,
                    result.Top + i,
                    result.Width,
                    result.Height - i));
            }


            if (result.Left + result.Width < dBounds.Left + dBounds.Width)
            {
                int i = result.Left + result.Width - dBounds.Left - dBounds.Width;

                result = MakeRectanglePossible( new Rectangle(
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


            if (DisplayStatus == pDisplayStatus.Selecting)
            {

                if (m_selectSourcePoint.HasValue)
                {

                    foreach(pPanel p in pPanels)
                    {
                        
                            if (m_lastSelectItems.Contains(p))
                                continue;

                            if (Contains(MakeRectanglePossible(new Rectangle(m_selectSourcePoint.Value,
                                new Size(
                                DisplayMousePosition.X - m_selectSourcePoint.Value.X,
                                DisplayMousePosition.Y - m_selectSourcePoint.Value.Y)

                                )), p.Bounds, true))
                            {

                                Select(p);
                                m_lastSelectItems.Add(p);
                            }
                    }
                }

                if (m_lastSelectItems.Count > 0)
                {
                    //To remove just the pPanels was selected in m_lastSelectItems
                    //we need a auxiliar(?) List pToRemove.

                    List<pPanel> pToRemove = new List<pPanel>(m_lastSelectItems.Count - 1);

                    foreach (pPanel p in m_lastSelectItems)
                    {
                        if (!Contains(MakeRectanglePossible(new Rectangle(m_selectSourcePoint.Value,
                                    new Size(
                                    DisplayMousePosition.X - m_selectSourcePoint.Value.X,
                                    DisplayMousePosition.Y - m_selectSourcePoint.Value.Y)

                                    )), p.Bounds, true))
                        {
                            UnSelect(p);
                            pToRemove.Add(p);
                        }
                    }

                    foreach (pPanel p in pToRemove)
                    {
                        m_lastSelectItems.Remove(p);
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

                        foreach (pPanel pp in SelectedpPanels)
                        {
                            pp.CalculatedLocation = new Point(Convert.ToInt32(Math.Round(((double)p.X - ((double)pp.MousePositionOnDown.X)) / (double)iGridDistance)) * iGridDistance, Convert.ToInt32(Math.Round(((double)p.Y - ((double)pp.MousePositionOnDown.Y)) / (double)iGridDistance)) * iGridDistance);
                          //  pp.Draw();
                        }
                    }

                }
                else if (DisplayStatus == pDisplayStatus.Linking || DisplayStatus == pDisplayStatus.Linking_Paused)
                {
                    if (ActiveControl != null)
                    {

                        UnHighLight();


                        foreach (List<pPanel> lp in m_groups)
                        {
                            if(lp!=null)
                            if (GetGroupRectangle(lp).Contains(DisplayMousePosition))
                            {
                                foreach (pPanel pp in lp)
                                {
                                    HighLight(pp);
                                }
                            }
                        }

                        Invalidate();
                        DisplayStatus = pDisplayStatus.Linking;
                    }
                }
                else if (DisplayStatus == pDisplayStatus.Idle)
                {
                    if (MouseButtons == MouseButtons.Left && ActiveControl != null)
                    {
                        DisplayStatus = pDisplayStatus.Moving;
                    }

                }

        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Point p = DisplayMousePosition;


            bool bFound = false;

            

            if (DisplayStatus != pDisplayStatus.Selecting)
            {

                pPanel TargetpPanel = GetpPanelAt(p);
                

                if (TargetpPanel != null)
                {
                    TargetpPanel.BringToFront();
                    bFound = true;

                    switch (DisplayStatus)
                    {
                        case pDisplayStatus.Linking_Paused:
                        case pDisplayStatus.Linking:

                            if (ActiveControl != null)
                            {


                                if (HighLightedpPanels.Count > 0)
                                {
                                    foreach (pPanel pp in SelectedpPanels)
                                    {
                                        Neuron n = (Neuron)pp.Tag;

                                        foreach (pPanel ppp in HighLightedpPanels)
                                        {

                                            Neuron target = (Neuron)ppp.Tag;

                                            if (!n.Input.ContainsKey(target))
                                            {
                                                n.Input.Add(target, new NeuralFactor(new Random(1).NextDouble()));
                                                target.Output.Add(n);
                                                DrawSynapse(pp, ppp);
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
                                    Neuron target = (Neuron)TargetpPanel.Tag;

                                    if (!n.Input.ContainsKey(target))
                                    {
                                        n.Input.Add(target, new NeuralFactor(new Random(1).NextDouble()));
                                        target.Output.Add(n);
                                        DrawSynapse(ActiveControl, TargetpPanel);
                                    }
                                    else
                                    {
                                        n.Input.Remove(target);
                                        target.Output.Remove(n);
                                    }
                                }


                            }
                            else ActiveControl = TargetpPanel;
                            
                            break;

                        case pDisplayStatus.Idle:

                            if (!ShiftKey && !TargetpPanel.Selected)
                                UnSelect();

                            Select(TargetpPanel);

                            foreach (pPanel pp in SelectedpPanels)
                                pp.MousePositionOnDown = DisplayMousePosition;

                            ActiveControl = TargetpPanel;
                            
                            break;

                    }
                }

            }
            
            if(!bFound)
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
                    UnSelect();

                UnHighLight();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (DisplayStatus == pDisplayStatus.Moving)
                if (ActiveControl != null)
                {
                    DisplayStatus = pDisplayStatus.Idle;
                    ActiveControl.MouseOperation = null;
                    ActiveControl = null;
                }
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

        }





        #endregion

        #region Paint

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
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
                DrawSynapse(ActiveControl, DisplayMousePosition, e.Graphics);
            }


            foreach(pPanel c in pPanels)
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
                Pen pp = pColors.Red.Pen;

                Pen p = new Pen(m_gridLineColor);
                int w = e.ClipRectangle.Width;
                int h = e.ClipRectangle.Height;

                Graphics g = e.Graphics;

                for (int i = w / 2; i < w; i += m_gridDistance)
                {
//                    g.DrawLine(p, i - (GetPerspective(new Point(i, 0), Parent.Size).X * 1), 0, i + (GetPerspective(new Point(i, 0), Parent.Size).X * 1), Height);

//                    g.DrawLine(p, w / 2 - i + w / 2 + (GetPerspective(new Point(i, 0), Parent.Size).X * 1), 0, w / 2 - i + w / 2 - (GetPerspective(new Point(i, 0), Parent.Size).X * 1), Height);
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

