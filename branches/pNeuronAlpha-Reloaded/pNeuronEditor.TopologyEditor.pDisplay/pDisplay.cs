using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using primeira.pNeuron.Core;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections;
using primeira.pRandom;
using System.Linq;
using pNeuronEditor.TopologyEditor;

namespace pNeuronEditor.TopologyEditor
{
    public partial class pDisplay : Panel, IpPanels
    {
        
        NeuralNetwork m_net ;

        public NeuralNetwork Net
        {
            get { return m_net; }
        }

        #region events

        public delegate void SelectedPanelsChangeDelegate();
        public event SelectedPanelsChangeDelegate OnSelectedPanelsChange;

        public delegate void DisplayStatusChangeDelegate();
        public event DisplayStatusChangeDelegate OnDisplayStatusChange;

        public delegate void NetworkChangeDelegate();
        public event NetworkChangeDelegate OnNetworkChange;

        #endregion

        #region Enums

        public enum pDisplayStatus
        {
            Idle,
            Moving,
            Linking,
            Linking_Paused,
            Selecting,
            Add_Neuron,
            Remove_Neuron
        }

        #endregion

        #region Fields

        #region Utils

        /// <summary>
        /// Generate randoms to neuron internal values.
        /// </summary>
        private Random m_random = new Random(1);

        /// <summary>
        /// A global graphics to avoid use CreateGraphics()
        /// </summary>
        private Graphics m_graphics;

        #endregion

        #region Enviroment options

        /// <summary>
        /// To draw grid and snap neurons to grid.
        /// </summary>
        private int m_gridDistance = 25;

        /// <summary>
        /// Indicates when snap to grid mode is enabled.
        /// </summary>
        private bool m_allignToGrid = false;

        #endregion

        #region Status/Flags/Pressed Keys

        /// <summary>
        /// Keep initial point of the select rectangle otherwise null.
        /// </summary>
        private Nullable<Point> m_selectSourcePoint;

        /// <summary>
        /// Used to invalidate when the select rectangle closes.
        /// </summary>
        private Nullable<Rectangle> m_lastSelectRectangleDrow;

        /// <summary>
        /// Main status of pDisplay. Has a get/set on DisplayStatus.
        /// </summary>
        private pDisplayStatus m_displayStatus = pDisplayStatus.Idle;

        #endregion

        #region Groups. Implemented on pDisplayControls
        
        /// <summary>
        /// All pPanels on pDisplay. Has a get on pPanels.
        /// </summary>
        private List<pPanel> m_pPanels;

        #endregion

        #endregion

        #region Constructor

        public pDisplay()
        {
            InitializeComponent();
            
            //Makes all this stuff slow =(
            DoubleBuffered = true;

            //Initialize neuron list
            m_pPanels = new List<pPanel>();

            m_graphics = CreateGraphics();

            //Initialize NeuralNet
            SetNeuralNetwork(new NeuralNetwork());
        }

        #endregion

        #region Properties

        public Point DisplayMousePosition
        {
            get
            {
                return PointToClient(MousePosition);
            }
        }


        public pDisplayStatus DisplayStatus
        {
            get { return m_displayStatus; }
            set
            {

                if (m_displayStatus == value)
                    return;

                m_displayStatus = value;

                switch (m_displayStatus)
                {
                    case pDisplayStatus.Moving: Cursor = Cursors.SizeAll;
                        break;
                    case pDisplayStatus.Linking: Cursor = Cursors.Cross;
                        break;
                    case pDisplayStatus.Linking_Paused: if (SelectedpPanels.Count() > 0) DisplayStatus = pDisplayStatus.Linking;
                        break;
                    default: Cursor = Cursors.Default;
                        break;

                }
                
                if (OnDisplayStatusChange != null)
                    OnDisplayStatusChange();
            }
        }

        #endregion

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

        /// <summary>
        /// Deprecated. Use ExpandRectangle.
        /// </summary>
        /// <param name="cBounds"></param>
        /// <param name="dBounds"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Indicates if a rectangle contains another.
        /// </summary>
        /// <param name="r">Rectangle</param>
        /// <param name="i">Child retangle</param>
        /// <param name="AnyPoint">Indicate if any point of child is enough</param>
        /// <returns></returns>
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

        #endregion

        #region Transversal events

        protected override void OnMouseMove(MouseEventArgs e)
        {

            if (DisplayStatus == pDisplayStatus.Selecting)
            {

                if (m_selectSourcePoint.HasValue)
                {

                    foreach (pPanel p in m_pPanels)
                    {

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

                            Point tempP = new Point(Convert.ToInt32(Math.Round(((double)p.X - ((double)pp.MousePositionOnDown.X)) / (double)iGridDistance)) * iGridDistance, Convert.ToInt32(Math.Round(((double)p.Y - ((double)pp.MousePositionOnDown.Y)) / (double)iGridDistance)) * iGridDistance);

                            if (pp.Location != tempP)
                            {
                                Invalidate(r);
                                pp.Location = tempP;

                                if (OnNetworkChange != null)
                                    OnNetworkChange();

                            }

                        }

                        Rectangle rArea = m_pPanels[0].Bounds;

                        foreach (pPanel pp in m_pPanels)
                        {
                            rArea = ExpandRectangle(rArea, pp.Bounds);
                        }

                        AutoScrollMinSize = new Size(
                            rArea.Left + rArea.Width,
                            rArea.Top + rArea.Height);

                    }

                    Invalidate();

                }
                else if (DisplayStatus == pDisplayStatus.Idle)
                {
                    if (MouseButtons == MouseButtons.Left && SelectedpPanels.Length > 0)
                    {
                        DisplayStatus = pDisplayStatus.Moving;
                    }

                }
                else if (DisplayStatus == pDisplayStatus.Linking)
                {
                    Invalidate();
                }
                
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Point p = DisplayMousePosition;

            #region Add Neuron

            if (DisplayStatus == pDisplayStatus.Add_Neuron)
            {
                Neuron n = this.Net.AddNeuron();
                
                n.Left = (DisplayMousePosition.X/m_gridDistance) * m_gridDistance;
                n.Top = (DisplayMousePosition.Y/m_gridDistance) * m_gridDistance;

                pPanel pp = this.Add(n);

                Invalidate(pp.Bounds);

                if (OnNetworkChange != null)
                    OnNetworkChange();

                return;
            }

            #endregion

            bool bFound = false;

            pPanel[] HighlightedpPanels = (from x in this.pPanels where Contains(x.Bounds, new Rectangle(p, new Size(1, 1)), true) select x).ToArray();

            if (DisplayStatus != pDisplayStatus.Selecting)
            {
                if (HighlightedpPanels.Length > 0)
                {
                    bFound = true;

                    switch (DisplayStatus)
                    {

                        case pDisplayStatus.Linking_Paused:
                        case pDisplayStatus.Linking:

                            if (DisplayStatus == pDisplayStatus.Linking_Paused)
                            {
                                Select(HighlightedpPanels);
                                DisplayStatus = pDisplayStatus.Linking;
                                Invalidate();
                                return;
                            }

                            if (SelectedpPanels.Length > 0)
                            {
                                if (HighlightedpPanels.Length > 0)
                                {
                                    foreach (pPanel pp in SelectedpPanels)
                                    {
                                        Neuron n = pp.Neuron;

                                        foreach (pPanel ppp in HighlightedpPanels)
                                        {

                                            Neuron target = ppp.Neuron;

                                            if (target.GetSynapseFrom(n) == null)
                                            {
                                                target.AddSynapse(n);
                                                Invalidate();

                                                if (OnNetworkChange != null)
                                                    OnNetworkChange();
                                            }
                                            else
                                            {
                                                target.RemoveSynapse(n);

                                                if (OnNetworkChange != null)
                                                    OnNetworkChange();
                                            }
                                        }
                                    }
                                }
                            }

                            break;

                        case pDisplayStatus.Idle:

                            Select(HighlightedpPanels);

                            foreach (pPanel pp in SelectedpPanels)
                                pp.MousePositionOnDown = DisplayMousePosition;

                            break;
                        case pDisplayStatus.Remove_Neuron:
                            if (HighlightedpPanels.Length > 0)
                            {
                                Remove(HighlightedpPanels);
                            }
                            break;

                    }
                }



            }

            if (!bFound)
            {
                UnSelect();

                if (DisplayStatus == pDisplayStatus.Linking)
                    DisplayStatus = pDisplayStatus.Linking_Paused;

                if (DisplayStatus == pDisplayStatus.Idle)
                {
                    m_selectSourcePoint = DisplayMousePosition;
                    DisplayStatus = pDisplayStatus.Selecting;
                }
                
                
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

            Rectangle r = e.ClipRectangle;

            foreach (pPanel p in m_pPanels)
            {
                INeuron[] arNeuron = p.Neuron.GetInputNeurons();
                foreach (Neuron n in arNeuron)
                {
                    foreach (pPanel pp in m_pPanels)
                    {
                        if (n == (pp.Neuron))
                        {
                            if (Contains(r, ExpandRectangle(p.Bounds, pp.Bounds), true))
                                DrawSynapse(p, pp, e.Graphics);
                        }
                    }
                }

            }

            if (DisplayStatus == pDisplayStatus.Linking)
            {

                foreach (pPanel p in SelectedpPanels)
                {
                    pPanel[] HighlightedpPanels = (from x in this.pPanels where Contains(x.Bounds, new Rectangle(DisplayMousePosition, new Size(1, 1)), true) select x).ToArray();

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


            foreach (pPanel c in m_pPanels)
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

        public void DrawSelect(Rectangle cBounds, Graphics gg)
        {
            m_lastSelectRectangleDrow = cBounds;

            gg.FillRectangle(new SolidBrush(Color.FromArgb(10, Color.Blue)),

               cBounds);

            gg.DrawRectangle(new Pen(Color.FromArgb(50, Color.Blue), 1),
              cBounds);

        }

        public void DrawSynapse(pPanel c, Point d, Graphics g)
        {
            pPanel dd = new pPanel(g);

            //just to keep the location data
            dd.Neuron = new Neuron();
            dd.Location = new Point(d.X - AutoScrollPosition.X, d.Y - AutoScrollPosition.Y);

            DrawSynapse(dd, c, g);
        }

        public void DrawSynapse(pPanel c, pPanel d)
        {
            DrawSynapse(c, d, m_graphics);
        }

        private void DrawSynapse(pPanel d, pPanel c, Graphics g)
        {

            if (c.Location == d.Location)
                return;

            double dZoom = 1;

            #region Draw Large Synapses

            //if (DisplayStatus == pDisplayStatus.Training)
            //{
                
            //    double dMaxY = 10;

            //    double dMaxV = double.NegativeInfinity;

            //    foreach (pPanel pp in pPanels)
            //        foreach (double dd in pp.SynapseIN)
            //        {
            //            if (dd > dMaxV)
            //                dMaxV = dd;
            //        }

            //    dZoom = dMaxY / dMaxV;

            //    if (double.IsInfinity(dZoom))
            //    {
            //        dZoom = 1;
            //    }
            //}

            #endregion

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

            Pen p = ((pPanel)c).GetPenStyle();
            
            p.Width = 1;
            try
            {
                if (d.Neuron != null)
                    if (c.Neuron.GetSynapseTo(d.Neuron) != null)
                        p.Width = Convert.ToInt32(Math.Max(1, Math.Abs(c.Neuron.GetSynapseTo(d.Neuron).Weight) * dZoom));
            }
            catch { }

            SolidBrush b = new SolidBrush(Color.Red);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


            //hipotenusa
            double catA;
            double catB;
            double hyp;

            int signX = d.Bounds.Left + (d.Bounds.Width / 2) > c.Bounds.Left + (c.Bounds.Width / 2) ? 1 : -1;
            int signY = d.Bounds.Top + (d.Bounds.Height / 2) > c.Bounds.Top + (c.Bounds.Height / 2) ? 1 : -1;

            catA = c.Location.Y - d.Location.Y;
            catB = c.Location.X - d.Location.X;
            hyp = Convert.ToInt32(Math.Sqrt(Math.Pow(catA, 2) + Math.Pow(catB, 2)));

            double cos = -catA / hyp;
            double sen = -catB / hyp;

            double radXC = c.Bounds.Left + (c.Bounds.Width / 2) + (sen * c.Size.Width / 2);
            double radYC = c.Bounds.Top + (c.Bounds.Height / 2) + (cos * c.Size.Width / 2);

            double radXD = d.Bounds.Left + (d.Bounds.Width / 2) + (sen * d.Size.Width / 2);
            double radYD = d.Bounds.Top + (d.Bounds.Height / 2) + (cos * d.Size.Width / 2);


                g.DrawBezier(p,
                    new Point((int)radXC + (-1 * signX), (int)radYC + (-1 * signY)),
                    new Point(c.Bounds.Left + (c.Bounds.Width) * signX, c.Bounds.Top + (c.Bounds.Height) * signY),

                    new Point(d.Bounds.Left + (d.Bounds.Width / 2) * -signX, d.Bounds.Top + (d.Bounds.Height / 2) * -signY),
                    new Point(d.Bounds.Left + (d.Bounds.Width / 2), d.Bounds.Top + (d.Bounds.Height / 2))

                    );



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

        #endregion

        public void SetNeuralNetwork(NeuralNetwork aNet)
        {
            if (m_net != null)
                this.m_net.Dispose();

            this.m_net = aNet;

            pPanels.Clear();

            foreach (Neuron n in aNet.Neuron)
                Add(n);

            Invalidate();
        }

    }
}

