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
using pShortcutManager;
using primeira.pTypes;

namespace primeira.pNeuron
{

    public partial class pDisplay : Panel, primeira.pNeuron.IpPanels
    {

        #region events

        public delegate void NetworkChangeDelegate(pChangeEscope escope);
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
            Remove_Neuron,
            Training
        }

        #endregion

        #region Fields
      
        private NeuralNetwork m_net;

        private Graphics m_graphics;

        private int m_offsetX = 0;
       
        private int m_offsetY = 0;

        #region Enviroment options fields

        /// <summary>
        /// To draw grid and snap neurons to grid.
        /// </summary>
        private int m_gridDistance = 25;

        /// <summary>
        /// Indicates when snap to grid mode is enabled.
        /// </summary>
        private bool m_allignToGrid = false;

        /// <summary>
        /// Grid Color.
        /// </summary>
        private Color m_gridLineColor = Color.LightGray;

        /// <summary>
        /// Indicates to draw grid at onPaint.
        /// </summary>
        private bool m_gridShow = false;

        /// <summary>
        /// Indicates Bezier mode enabled. Has a get/set on Bezier.
        /// </summary>
        private bool m_bezier = true;

        #endregion

        #region Status/Flags/Pressed Keys/Zoom and Offset fields

        private bool m_shiftKey = false;
        private bool m_ctrlKey = false;
        
        private pDisplayStatus m_displayStatus = pDisplayStatus.Idle;

        /// <summary>
        /// Keep initial point of the select rectangle otherwise null.
        /// </summary>
        private Nullable<Point> m_selectSourcePoint;

        /// <summary>
        /// Used to invalidate when the select rectangle closes.
        /// </summary>
        private Nullable<Rectangle> m_lastSelectRectangleDrow;

        /// <summary>
        /// Store the neurons selected til previous action.
        /// </summary>
        private List<pPanel> m_lastSelectItems;

        private float m_zoom = 1;

        private Point m_offset = new Point(0, 0);

        /// <summary>
        /// The SmartZoom associated with this 
        /// </summary>
        private IpSmartZoom m_smartZoom = null;

        private Timer tmMove = new Timer();

        public IpSmartZoom SmartZoom
        {
            get { return m_smartZoom; }
            set { m_smartZoom = value; }
        }

        public Point Offset
        {
            get
            {
                return new Point(OffsetX, OffsetY);
            }
            set
            {
                m_offsetX = value.X;
                m_offsetY = value.Y;

                Invalidate();

                if (OnNetworkChange != null)
                    OnNetworkChange(pChangeEscope.File | pChangeEscope.ZoomDisplayMask); 
            }
        }

        /// <summary>
        /// Zoom of pDisplay. Has a get/set on Zoom.
        /// </summary>
        public float Zoom
        {
            get { return m_zoom; }
            set {
                return;
                m_zoom = value;

            if (OnNetworkChange != null)
                OnNetworkChange(pChangeEscope.File | pChangeEscope.ZoomDisplayMask); 
        }
        }

        #endregion

        #region Group fields. Implemented on pDisplayControls

        /// <summary>
        /// Neuron groups. Has a public get on Groups.
        /// </summary>
        private List<pPanel>[] m_groups;

        /// <summary>
        /// All pPanels on pDisplay. Has a get on pPanels.
        /// </summary>
        private List<pPanel> m_pPanels;

        #endregion

        #endregion

        #region Constructor

        public pDisplay()
        {

            //Makes all this stuff slow =(
            DoubleBuffered = true;

            //Initialize neuron list
            m_pPanels = new List<pPanel>();

            //Initialize with no neurons selected
            m_lastSelectItems = new List<pPanel>();

            //Initialize with pColors.Colors.Length
            m_groups = new List<pPanel>[pColors.Colors.Length];

            m_graphics = CreateGraphics();

            //Initialize all groups
            for (int i = 0; i < m_groups.Length; i++)
                m_groups[i] = new List<pPanel>();

            //Initialize NeuralNet
            m_net = new NeuralNetwork();

            AutoScroll = false;

            tmMove.Tick += new EventHandler(tmMove_Tick);
            


        }

        void tmMove_Tick(object sender, EventArgs e)
        {

            Point p = this.PointToClient(MousePosition);

            if (!this.Bounds.Contains(p))
            {
                tmMove.Stop();
                return;
            }

            //Afinar variaveis
            if (p.X > Width - 50)
                OffsetX -=  Convert.ToInt32((5f/(Width - p.X))*5);

            if (p.X < 50)
                OffsetX += 5;
            if (p.Y > Height - 50)
                OffsetY -= 5;
            if (p.Y < 50)
                OffsetY += 5;

            Invalidate();
        }



        #endregion

        #region Properties

        /// <summary>
        /// X offset of display in pixelx.
        /// </summary>
        public int OffsetX
        {
            get { return m_offsetX; }
            set { m_offsetX = value; }
        }

        /// <summary>
        /// Y offset of display in pixelx.
        /// </summary>
        public int OffsetY
        {
            get { return m_offsetY; }
            set { m_offsetY = value; }
        }

        #region Enviroment Options

        /// <summary>
        /// Get or set if synapse draw will do bezier.
        /// </summary>
        public bool Bezier
        {
            get { return m_bezier; }
            set { m_bezier = value; }
        }

        #endregion

        #region Status/Flags/Pressed Keys

        /// <summary>
        /// The main Neural Network
        /// </summary>
        public NeuralNetwork Net
        {
            get { return m_net; }
        }

        /// <summary>
        /// Indicates shift key is pressed. Has a get/set on Shiftkey.
        /// </summary>
        public bool ShiftKey
        {
            get { return m_shiftKey; }
            set { m_shiftKey = value; }
        }

        /// <summary>
        /// Indicates ctrl key is pressed. Has a get/set on Ctrlkey.
        /// </summary>
        public bool CtrlKey
        {
            get { return m_ctrlKey; }
            set
            {
                m_ctrlKey = value;
                RefreshHighlight();
                //Invalidate();
            }
        }


        public Point DisplayMousePosition
        {
            get
            {
                return PointToClient(MousePosition);
            }
        }

        /// <summary>
        /// Main status of pDisplay. Has a get/set on DisplayStatus.
        /// </summary>
        public pDisplayStatus DisplayStatus
        {
            get { return m_displayStatus; }
            set
            {

                if (m_displayStatus == value)
                    return;

                if (m_displayStatus == pDisplayStatus.Training && (value != pDisplayStatus.Training && value != pDisplayStatus.Idle) && m_displayStatus != value)
                {
                    pMessage.Error("This operation is not valid at Training Status");
                }
                else
                {
                    m_displayStatus = value;

                    switch (m_displayStatus)
                    {
                        case pDisplayStatus.Moving: Cursor = Cursors.SizeAll;
                            break;
                        case pDisplayStatus.Linking: Cursor = Cursors.Cross;
                            break;
                        default: this.Invoke(new Assinc(SetCursorDefaultCrossThread));
                            break;

                    }
                }
                return;
                if (OnNetworkChange != null)
                    OnNetworkChange(pChangeEscope.DisplayStatus);
            }
        }

        private void SetCursorDefaultCrossThread()
        {
            Cursor = Cursors.Default;
        }

        #endregion

        /// <summary>
        /// Output logger.
        /// TODO: Create a fmLogger dockable window
        /// </summary>
        public TextBox Logger = new TextBox();

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

        /// <summary>
        /// Apply a zoom factor on an int
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int Magnify(int i, float m_zoom)
        {
            return Convert.ToInt32(i * m_zoom);
        }

        public int UnMagnify(int i, float m_zoom)
        {
            return Convert.ToInt32(i * (1 / m_zoom));
        }

        /// <summary>
        /// Apply a zoom factor on a float
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public float Magnify(float f, float m_zoom)
        {
            return f * m_zoom;
        }

        public Rectangle Magnify(Rectangle value, float m_zoom)
        {
            return
                new Rectangle(
                     Magnify(value.Left, m_zoom),
                     Magnify(value.Top, m_zoom),
                    Magnify(value.Width, m_zoom),
                    Magnify(value.Height, m_zoom));


        }


        public int DoOffset(int value, int offset)
        {
            return value + offset;
        }

        public int DoOffset(float value, int offset)
        {
            return (int)value + offset;
        }

        public Point DoOffset(Point value, int OffsetX, int OffsetY)
        {
            return
                new Point(
                value.X + OffsetX,
                value.Y + OffsetY

                );
        }

        public Rectangle DoOffset(Rectangle value, int OffsetX, int OffsetY)
        {
            return 
                new Rectangle(
                DoOffset(value.Location, OffsetX, OffsetY),
                value.Size
                );

        }

        #endregion

        #region Transversal events

   

        protected override void OnMouseMove(MouseEventArgs e)
        {

            #region Moving Canvas

            tmMove.Start();

            #endregion

            RefreshHighlight();

            if (DisplayStatus == pDisplayStatus.Selecting)
            {

                if (m_selectSourcePoint.HasValue)
                {

                    foreach (pPanel p in m_pPanels)
                    {

                        if (m_lastSelectItems.Contains(p))
                            continue;

                        if (p.Selected)
                            continue;

                        if (
                            Contains(
                                MakeRectanglePossible(
                                    new Rectangle(m_selectSourcePoint.Value,
                           
                                    new Size(
                                    DisplayMousePosition.X - m_selectSourcePoint.Value.X,
                                    DisplayMousePosition.Y - m_selectSourcePoint.Value.Y)

                                    )
                                 ), 
                            
                           
                                Magnify(DoOffset(p.Bounds, OffsetX, OffsetY), Zoom)

                            , true))
                        {

                            Select(p);
                        }
                    }
                }

                if (m_lastSelectItems.Count > 0)
                {
                    foreach (pPanel p in m_pPanels)
                    {
                        if (!Contains(MakeRectanglePossible(new Rectangle(m_selectSourcePoint.Value,
                                    new Size(
                                    DisplayMousePosition.X - m_selectSourcePoint.Value.X,
                                    DisplayMousePosition.Y - m_selectSourcePoint.Value.Y)

                                    )),

                                    Magnify(DoOffset(p.Bounds, OffsetX, OffsetY), Zoom)

                                    , true)

                                    && m_lastSelectItems.Contains(p))
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

                    cBounds = new Rectangle(
                        new Point(
                        cBounds.X,
                        cBounds.Y),

                        new Size(
                        cBounds.Width - cBounds.X,
                        cBounds.Height - cBounds.Y));

                    Invalidate(Magnify(DoOffset(cBounds, OffsetX, OffsetY), Zoom));
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
                            Point tempP = new Point(
                                Convert.ToInt32(Math.Round(((double)p.X - ((double)pp.MousePositionOnDown.X)) / (double)iGridDistance)) * iGridDistance,
                                Convert.ToInt32(Math.Round(((double)p.Y - ((double)pp.MousePositionOnDown.Y)) / (double)iGridDistance)) * iGridDistance);

                            if (pp.Location != tempP)
                            {
//Or here                                Invalidate(Magnify(DoOffset(r, OffsetX, OffsetY), Zoom));
                                pp.Location = tempP;

                            }

                        }

                    }
//Or here
                    Invalidate();

                }
                else if (DisplayStatus == pDisplayStatus.Linking || DisplayStatus == pDisplayStatus.Linking_Paused)
                {
                    if (SelectedpPanels.Length > 0)
                    {
                        DisplayStatus = pDisplayStatus.Linking;
                        if (Contains(HighlightedpPanels, SelectedpPanels) > 0)
                        {
                            Cursor = Cursors.No;
                        }
                        else Cursor = Cursors.Cross;

                        Invalidate();

                    }
                }
                else if (DisplayStatus == pDisplayStatus.Idle && MouseButtons == MouseButtons.Left)
                {
                    if (SelectedpPanels.Length > 0)
                    {
                        DisplayStatus = pDisplayStatus.Moving;
                    }
                    else
                    {

                        RefreshHighlight();
                    }

                }

        }



        protected override void OnMouseDown(MouseEventArgs e)
        {

            Parent.Focus();

            if(e.Button != MouseButtons.Left)
            {
                return;
            }

            Point p = DisplayMousePosition;

            if (Cursor == Cursors.No)
            {
                return;
            }

            #region Add Neuron

            if (DisplayStatus == pDisplayStatus.Add_Neuron)
            {
                pPanel pp = this.Add( );
                int x, y;
                x = pp.Location.X;
                y = pp.Location.Y;
                pp.Location = new Point(UnMagnify(((DisplayMousePosition.X - pp.Width / 2) / Magnify(m_gridDistance, Zoom)) * Magnify(m_gridDistance, Zoom), Zoom),
                                         UnMagnify(((DisplayMousePosition.Y - pp.Height / 2) / Magnify(m_gridDistance, Zoom)) * Magnify(m_gridDistance, Zoom), Zoom));


                pp.Location = DoOffset(pp.Location, -OffsetX, -OffsetY);

                //TODO:Fine tune
                //pp.Location = new Point(
                //    (pp.Location.X + pp.Width / 2 < x) ? pp.Location.X : pp.Location.X + pp.Width ,
                //    (pp.Location.Y + pp.Height / 2 < y) ? pp.Location.Y : pp.Location.Y + pp.Height );




                Invalidate(Magnify(DoOffset(pp.Bounds, OffsetX, OffsetY), Zoom));

                if (OnNetworkChange != null)
                    OnNetworkChange(pChangeEscope.File | pChangeEscope.ZoomDisplayCache); 

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
                        case pDisplayStatus.Training:
                        case pDisplayStatus.Linking_Paused:
                        case pDisplayStatus.Linking:

                            if (DisplayStatus == pDisplayStatus.Linking_Paused || DisplayStatus == pDisplayStatus.Training)
                            {
                                Select(HighlightedpPanels);
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

                                                Invalidate(Magnify(DoOffset(pp.Bounds, OffsetX, OffsetY), Zoom));

                                                if (OnNetworkChange != null)
                                                    OnNetworkChange(pChangeEscope.File | pChangeEscope.ZoomDisplayCache); 
                                            }
                                            else
                                            {
                                                target.RemoveSynapse(n);

                                                Invalidate(Magnify(DoOffset(pp.Bounds, OffsetX, OffsetY), Zoom));

                                                if (OnNetworkChange != null)
                                                    OnNetworkChange(pChangeEscope.File | pChangeEscope.ZoomDisplayCache); 
                                            }
                                        }
                                    }
                                }
                            }

                            break;

                        case pDisplayStatus.Idle:

                            if (!ShiftKey && Contains(HighlightedpPanels, SelectedpPanels) != HighlightedpPanels.Length)
                                UnSelect();

                            Select(HighlightedpPanels);

                            foreach (pPanel pp in SelectedpPanels)
                                pp.MousePositionOnDown = DisplayMousePosition;

                            break;
                        case pDisplayStatus.Remove_Neuron:
                            if (HighlightedpPanels.Length > 0)
                            {

                                Rectangle[] r = new Rectangle[HighlightedpPanels.Length];

                                int i = 0;
                                
                                foreach (pPanel panel in HighlightedpPanels)
                                {
                                    r[i++] = panel.Bounds;
                                }

                                Remove(HighlightedpPanels);

                                foreach (Rectangle rr in r)
                                {
                                    Invalidate(Magnify(DoOffset(rr, OffsetX, OffsetY), Zoom));
                                }

                                if (OnNetworkChange != null)
                                    OnNetworkChange(pChangeEscope.File | pChangeEscope.ZoomDisplayCache); 
                            }
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

            //Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (DisplayStatus == pDisplayStatus.Moving)
            {
                if (OnNetworkChange != null)
                    OnNetworkChange(pChangeEscope.File | pChangeEscope.ZoomDisplayCache); 

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

                        Invalidate(Magnify(DoOffset(cBounds, OffsetX, OffsetY), Zoom));
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
            Render(e, OffsetX, OffsetY, Zoom);
        }

        public void Render(PaintEventArgs e, int OffsetX, int OffsetY, float Zoom)
        {
            Render(e, OffsetX, OffsetY, Zoom, false);
        }

        public void Render(PaintEventArgs e, int OffsetX, int OffsetY, float Zoom, bool LittleOne)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            DrawLines(e);

            //Change neuron size on neuron value like synapses.
            if (DisplayStatus == pDisplayStatus.Training && false)
            {

                double dZoom = 1;
                double dMaxY = 50;

                double dMaxV = double.NegativeInfinity;

                foreach (pPanel p in pPanels)
                {
                    if (p.Neuron.Value > dMaxV)
                        dMaxV = p.Neuron.Value;
                }

                dZoom = dMaxY / dMaxV;

                if (double.IsInfinity(dZoom))
                {
                    dZoom = 1;
                }

                foreach (pPanel p in pPanels)
                    p.Size = new Size(
                                Convert.ToInt32(Math.Max(1, Math.Abs(p.Neuron.Value) * dZoom)),
                                Convert.ToInt32(Math.Max(1, Math.Abs(p.Neuron.Value) * dZoom)));
            }


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
                            if (LittleOne)
                            {
                                DrawSynapse(p, pp, e.Graphics, Zoom, Convert.ToInt32(-Width / 2) + OffsetX + Convert.ToInt32(SmartZoom.ZoomSize.Width / Zoom / 2), Convert.ToInt32(-Height / 2) + OffsetY + Convert.ToInt32(SmartZoom.ZoomSize.Height / Zoom / 2));
                            }
                            else
                                if (Contains(r, Magnify(DoOffset(ExpandRectangle(p.Bounds, pp.Bounds), OffsetX, OffsetY), Zoom), true))
                                    DrawSynapse(p, pp, e.Graphics, Zoom, OffsetX, OffsetY);
                        }
                    }
                }

            }



            if (DisplayStatus == pDisplayStatus.Linking && !LittleOne)
            {

                foreach (pPanel p in SelectedpPanels)
                {

                    if (HighlightedpPanels.Length > 0)
                    {
                        foreach (pPanel pp in HighlightedpPanels)
                        {
                            DrawSynapse(pp, p, e.Graphics, Zoom, OffsetX, OffsetY);
                        }
                    }
                    else
                        DrawSynapse(p, DisplayMousePosition, e.Graphics, Zoom, OffsetX, OffsetY);
                }
            }


            foreach (pPanel c in m_pPanels)
            {
                if (LittleOne)
                {
                        c.Draw(e.Graphics, Zoom, Convert.ToInt32(-Width / 2) + OffsetX + Convert.ToInt32(SmartZoom.ZoomSize.Width / Zoom / 2), Convert.ToInt32(-Height / 2) + OffsetY + Convert.ToInt32(SmartZoom.ZoomSize.Height / Zoom / 2));
                }
                else
                    if (Contains(r, Magnify(DoOffset(c.Bounds, OffsetX, OffsetY), Zoom), true))
                        c.Draw(e.Graphics, Zoom, OffsetX, OffsetY);
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
                    rr.Inflate(10, 10);
                    Invalidate(rr);
                    m_selectSourcePoint = pp;
                }

                if (Contains(r, xBounds, true))
                {
                    DrawSelect(xBounds, e.Graphics);

                }
            }

        }

        public void DrawMask(Graphics g)
        {

            int width = Magnify(UnMagnify(Width, Zoom), 0.1f);
            int height = Magnify(UnMagnify(Height, Zoom), 0.1f);

            Pen p = new Pen(Color.DarkGray, 1);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            g.DrawRectangle(p,
                new Rectangle(
                    Convert.ToInt32(-Magnify(UnMagnify(OffsetX, Zoom), 0.1f) - width / 2 + SmartZoom.ZoomSize.Width / 2),
                    Convert.ToInt32(-Magnify(UnMagnify(OffsetY,Zoom), 0.1f) - height / 2 + SmartZoom.ZoomSize.Height / 2),
                    Convert.ToInt32(width),
                    Convert.ToInt32(height)));

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

        public void DrawSelect(Rectangle cBounds, Graphics gg)
        {
            m_lastSelectRectangleDrow = cBounds;

            gg.FillRectangle(new SolidBrush(Color.FromArgb(10, Color.Blue)),

               cBounds);

            gg.DrawRectangle(new Pen(Color.FromArgb(50, Color.Blue), 1),
              cBounds);

        }

        public void DrawSynapse(pPanel c, Point d, Graphics g, float Zoom, int OffsetX, int OffsetY)
        {
            pPanel dd = new pPanel(g);
            dd.Location = new Point(d.X, d.Y);
            
            DrawSynapse(dd, c, g, Zoom, OffsetX, OffsetY);
        }

        public void DrawSynapse(pPanel c, pPanel d, float Zoom, int OffsetX, int OffsetY)
        {
            DrawSynapse(c, d, m_graphics, Zoom, OffsetX, OffsetY);
        }

        private void DrawSynapse(pPanel d, pPanel c, Graphics g, float Zoom, int OffsetX, int OffsetY)
        {

            if (c.Location == d.Location)
                return;

            double dZoom = 1;

            if (DisplayStatus == pDisplayStatus.Training)
            {
                
                double dMaxY = 10;

                double dMaxV = double.NegativeInfinity;

                foreach (pPanel pp in pPanels)
                    foreach (double dd in pp.SynapseIN)
                    {
                        if (dd > dMaxV)
                            dMaxV = dd;
                    }

                dZoom = dMaxY / dMaxV;

                if (double.IsInfinity(dZoom))
                {
                    dZoom = 1;
                }


            }

            Rectangle cBounds = c.Bounds;

            cBounds = new Rectangle(cBounds.X,
                cBounds.Y,
                cBounds.Width,
                cBounds.Height);


            Rectangle dBounds = d.Bounds;


            dBounds = new Rectangle(dBounds.X,
                dBounds.Y,
                dBounds.Width,
                dBounds.Height);

            Pen p = ((pPanel)c).GetPenStyle();
            
            p.Width = 1;

            if (Zoom > 0.1f)
                if (d.Neuron != null)
                    if (c.Neuron.GetSynapseTo(d.Neuron) != null)
                        p.Width = Convert.ToInt32(Math.Max(1, Math.Abs(c.Neuron.GetSynapseTo(d.Neuron).Weight) * dZoom));
            
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

                if (d.Neuron != null)
                {
                    if (d.Neuron.NeuronType == NeuronTypes.Memory)
                        p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                }


                int cX = (int)radXC + (-1 * signX);
                int cY = (int)radYC + (-1 * signY);
                int cX1 = c.Bounds.Left + (c.Bounds.Width) * signX;
                int cY1 = c.Bounds.Top + (c.Bounds.Height) * signY;

                int dX = d.Bounds.Left + (d.Bounds.Width / 2) * -signX;
                int dY = d.Bounds.Top + (d.Bounds.Height / 2) * -signY;
                int dX1 = d.Bounds.Left + (d.Bounds.Width / 2);
                int dY1 = d.Bounds.Top + (d.Bounds.Height / 2);


                cX = Magnify(DoOffset(cX, OffsetX), Zoom);
                cY = Magnify(DoOffset(cY, OffsetY), Zoom);
                cX1 = Magnify(DoOffset(cX1, OffsetX), Zoom);
                cY1 = Magnify(DoOffset(cY1, OffsetY), Zoom);


                if (d.Neuron != null)
                {
                    dX = Magnify(DoOffset(dX, OffsetX), Zoom);
                    dY = Magnify(DoOffset(dY, OffsetY), Zoom);
                    dX1 = Magnify(DoOffset(dX1, OffsetX), Zoom);
                    dY1 = Magnify(DoOffset(dY1, OffsetY), Zoom);
                }



                g.DrawBezier(p,
                    new Point(cX, cY),
                    new Point(cX1, cY1),

                    new Point(dX, dY),
                    new Point(dX1, dY1)

                    );
            }
            else
            {

                int cX = (int)radXC;
                int cY = (int)radYC;
                int dX = d.Bounds.Left + (d.Bounds.Width / 2);
                int dY = d.Bounds.Top + (d.Bounds.Height / 2);

                g.DrawLine(p,
                    new Point(cX, cY),
                    new Point(dX, dY)
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

        #endregion

        private void pPanelMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(HighlightedpPanels.Length == 1)
            {
            }
            else 
            {
                foreach(pPanel p in HighlightedpPanels)
                {
                  //  if(p.DataType != DataTypes.List)
                }
            }
        }

        private void pPanelMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //if(sender == tspNewDomain)
            //{
            //    fmDomainEdit f = new fmDomainEdit();
            //    f.ShowDialog();
            //}
            
        }

        #region Methods

        [pShortcutManagerVisible("Edit.Neuron.Remove", "Enables removing neuron state.", "Design", Keys.Delete)]
        public void kDelete()
        {
            DisplayStatus = pDisplayStatus.Remove_Neuron;
        }

        [pShortcutManagerVisible("Edit.Neuron.Add", "Enables add neuron state.", "Design", Keys.A)]
        public void kAdd()
        {
            DisplayStatus = pDisplayStatus.Add_Neuron;
        }


       [pShortcutManagerVisible("Edit.Neuron.Idle", "Enables idle state.", "Design", Keys.Escape)]
        public void kIdle()
        {
            UnSelect();
            DisplayStatus = pDisplay.pDisplayStatus.Idle;
        }

        [pShortcutManagerVisible("Edit.Neuron.Link", "Enables link state.", "Design", Keys.L)]
        public void kLink()
        {
            UnSelect();
            DisplayStatus = pDisplay.pDisplayStatus.Linking_Paused;
        }


        [pShortcutManagerVisible("Zoom.Out", "Zoom design out.", "Design", Keys.PageUp)]
        public void kZoomOut()
        {
            Zoom *= 1.1f;
        }

        [pShortcutManagerVisible("Zoom.In", "Zoom design in.", "Design", Keys.PageDown)]
        public void kZoomIn()
        {
            Zoom *= 0.9f;
        }

        [pShortcutManagerVisible("Design.Offset.Left", "Moves desing left.", "Design", Keys.Left)]
        public void kToLeft()
        {
           OffsetX += 20;
        }


        [pShortcutManagerVisible("Design.Offset.Right", "Moves desing right.", "Design", Keys.Right)]
        public void kToRight()
        {
            OffsetX -= 20;
        }

        [pShortcutManagerVisible("Design.Offset.Top", "Moves desing up.", "Design", Keys.Up)]
        public void kToUp()
        {
            OffsetY += 20;
        }


        [pShortcutManagerVisible("Design.Offset.Down", "Moves desing down.", "Design", Keys.Down)]
        public void kToDown()
        {
            OffsetY -= 20;
        }

        



        #endregion


    }
}

