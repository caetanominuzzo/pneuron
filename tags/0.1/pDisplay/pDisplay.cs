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

namespace primeira.pNeuron
{
    #region Enums

    public enum pTreeviewRefresh
    {
        pPanelAdd,
        pPanelRemove,
        pPanelGroupRemove,
        pGroupClear,
        pFullRefreh
    }

    #endregion

    public partial class pDisplay : Panel, primeira.pNeuron.IpPanels
    {
        
        [DllImport("Kernel32.dll")]
        public static extern bool Beep(UInt32 frequency, UInt32 duration);

        NeuralNetwork m_net ;

        public NeuralNetwork Net
        {
            get { return m_net; }
             //set { m_net = value; }
        }

        #region events

        public delegate void SelectedPanelsChangeDelegate();
        public event SelectedPanelsChangeDelegate OnSelectedPanelsChange;

        public delegate void DisplayStatusChangeDelegate();
        public event DisplayStatusChangeDelegate OnDisplayStatusChange;

        public delegate void TreeViewChangeDelegate(object o, pTreeviewRefresh mode);
        public event TreeViewChangeDelegate OnTreeViewChange;

        public delegate void NetworkChangeDelegate();
        public event NetworkChangeDelegate OnNetworkChange;

        public delegate void NewDomainDelegate();
        public event NewDomainDelegate OnNewDomain;


        #endregion

        #region Network Events

        public event NeuralNetwork.OnPulseDelegate OnPulse;

        public event NeuralNetwork.OnPulseBackDelegate OnPulseBack;

        public event NeuralNetwork.OnRefreshCyclesSecDelegate OnRefreshCyclesSec;

        public event NeuralNetwork.OnStartTraingDelegate OnStartTraing;

        public event NeuralNetwork.OnStopTraingDelegate OnStopTraing;

        public event NeuralNetwork.OnResetLearningDelegate OnResetLearning;

        public event NeuralNetwork.OnResetKnowledgementDelegate OnResetKnowledgement;

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

        #region Status/Flags/Pressed Keys

        /// <summary>
        /// Indicates shift key is pressed. Has a get/set on Shiftkey.
        /// </summary>
        private bool m_shiftKey = false;

        /// <summary>
        /// Indicates ctrl key is pressed. Has a get/set on Ctrlkey.
        /// </summary>
        private bool m_ctrlKey = false;

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

        /// <summary>
        /// Main status of pDisplay. Has a get/set on DisplayStatus.
        /// </summary>
        private pDisplayStatus m_displayStatus = pDisplayStatus.Idle;

        #endregion

        #region Groups. Implemented on pDisplayControls

        /// <summary>
        /// Neuron groups. Has a public get on Groups.
        /// </summary>
        private List<pPanel>[] m_groups;
        private System.ComponentModel.IContainer components;
        private ContextMenuStrip pPanelMenu;
        private ToolStripMenuItem tspNeuronType;
        private ToolStripMenuItem tspDataType;
        private ContextMenuStrip mspNeuronType;
        private ToolStripMenuItem tspNeuronTypeInput;
        private ToolStripMenuItem tspNeuronTypeHidden;
        private ToolStripMenuItem tspNeuronTypeOutput;
        private ContextMenuStrip mspDataType;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem toolStripMenuItem7;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem tspNewDomain;

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

            //Initialize with no neurons selected
            m_lastSelectItems = new List<pPanel>();

            //Initialize with pColors.Colors.Length
            m_groups = new List<pPanel>[pColors.Colors.Length];

            m_graphics = CreateGraphics();

            //Initialize all groups
            for (int i = 0; i < m_groups.Length; i++)
                m_groups[i] = new List<pPanel>();

            //Initialize NeuralNet
            SetNeuralNetwork(new NeuralNetwork());


        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pPanelMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tspNeuronType = new System.Windows.Forms.ToolStripMenuItem();
            this.tspDataType = new System.Windows.Forms.ToolStripMenuItem();
            this.mspNeuronType = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mspDataType = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tspNeuronTypeInput = new System.Windows.Forms.ToolStripMenuItem();
            this.tspNeuronTypeHidden = new System.Windows.Forms.ToolStripMenuItem();
            this.tspNeuronTypeOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tspNewDomain = new System.Windows.Forms.ToolStripMenuItem();
            this.pPanelMenu.SuspendLayout();
            this.mspNeuronType.SuspendLayout();
            this.mspDataType.SuspendLayout();
            this.SuspendLayout();
            // 
            // pPanelMenu
            // 
            this.pPanelMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspNeuronType,
            this.tspDataType});
            this.pPanelMenu.Name = "contextMenuStrip1";
            this.pPanelMenu.Size = new System.Drawing.Size(148, 48);
            this.pPanelMenu.Opening += new System.ComponentModel.CancelEventHandler(this.pPanelMenu_Opening);
            this.pPanelMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.pPanelMenu_ItemClicked);
            // 
            // tspNeuronType
            // 
            this.tspNeuronType.DropDown = this.mspNeuronType;
            this.tspNeuronType.Name = "tspNeuronType";
            this.tspNeuronType.Size = new System.Drawing.Size(147, 22);
            this.tspNeuronType.Text = "NeuronType";
            // 
            // tspDataType
            // 
            this.tspDataType.DropDown = this.mspDataType;
            this.tspDataType.Name = "tspDataType";
            this.tspDataType.Size = new System.Drawing.Size(147, 22);
            this.tspDataType.Text = "tspDataType";
            // 
            // mspNeuronType
            // 
            this.mspNeuronType.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspNeuronTypeInput,
            this.tspNeuronTypeHidden,
            this.tspNeuronTypeOutput});
            this.mspNeuronType.Name = "mspNeuronType";
            this.mspNeuronType.Size = new System.Drawing.Size(120, 70);
            // 
            // mspDataType
            // 
            this.mspDataType.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripSeparator1,
            this.tspNewDomain});
            this.mspDataType.Name = "mspDataType";
            this.mspDataType.Size = new System.Drawing.Size(180, 98);
            this.mspDataType.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mspDataType_ItemClicked);
            // 
            // tspNeuronTypeInput
            // 
            this.tspNeuronTypeInput.Name = "tspNeuronTypeInput";
            this.tspNeuronTypeInput.Size = new System.Drawing.Size(119, 22);
            this.tspNeuronTypeInput.Text = "Input";
            // 
            // tspNeuronTypeHidden
            // 
            this.tspNeuronTypeHidden.Name = "tspNeuronTypeHidden";
            this.tspNeuronTypeHidden.Size = new System.Drawing.Size(119, 22);
            this.tspNeuronTypeHidden.Text = "Hidden";
            // 
            // tspNeuronTypeOutput
            // 
            this.tspNeuronTypeOutput.Name = "tspNeuronTypeOutput";
            this.tspNeuronTypeOutput.Size = new System.Drawing.Size(119, 22);
            this.tspNeuronTypeOutput.Text = "Output";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItem5.Text = "toolStripMenuItem5";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItem6.Text = "toolStripMenuItem6";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItem7.Text = "toolStripMenuItem7";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(176, 6);
            // 
            // tspNewDomain
            // 
            this.tspNewDomain.Name = "tspNewDomain";
            this.tspNewDomain.Size = new System.Drawing.Size(179, 22);
            this.tspNewDomain.Text = "New Domain";
            this.pPanelMenu.ResumeLayout(false);
            this.mspNeuronType.ResumeLayout(false);
            this.mspDataType.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        #region Properties

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
            get { return m_displayStatus; }
            set
            {

//                if (m_displayStatus == value)
//                    return;

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
                if (OnDisplayStatusChange != null)
                    OnDisplayStatusChange();
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

        #endregion

        #region Transversal events

        protected override void OnMouseMove(MouseEventArgs e)
        {

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
                    foreach (pPanel p in m_pPanels)
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
                else if (DisplayStatus == pDisplayStatus.Linking || DisplayStatus == pDisplayStatus.Linking_Paused)
                {
                    if (SelectedpPanels.Length > 0)
                    {
                        DisplayStatus = pDisplayStatus.Linking;
                        if (Contains(HighlightedpPanels, SelectedpPanels) > 0)
                        {
                            Cursor = Cursors.No;
                        }

                        Invalidate();

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

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                if (HighlightedpPanels.Length > 0)
                    pPanelMenu.Show(PointToScreen(e.Location));

                return;
            }

            Point p = DisplayMousePosition;

            if (Cursor == Cursors.No)
            {
                Beep(456, 100);
                return;
            }

            #region Add Neuron

            if (DisplayStatus == pDisplayStatus.Add_Neuron)
            {
                pPanel pp = this.Add( );
                int x, y;
                x = pp.Location.X;
                y = pp.Location.Y;
                pp.Location = new Point((((DisplayMousePosition.X - pp.Width / 2) / m_gridDistance) * m_gridDistance),
                                        ((DisplayMousePosition.Y - pp.Height / 2) / m_gridDistance) * m_gridDistance);

                pp.Location = new Point(
                    (pp.Location.X + pp.Width / 2 < x) ? pp.Location.X : pp.Location.X + pp.Width / 2,
                    (pp.Location.Y + pp.Height / 2 < y) ? pp.Location.Y : pp.Location.Y + pp.Height / 2);



                Invalidate(pp.Bounds);

                if (OnNetworkChange != null)
                    OnNetworkChange();

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

                            if (!ShiftKey && Contains(HighlightedpPanels, SelectedpPanels) != HighlightedpPanels.Length)
                                UnSelect();

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

            if(DisplayStatus == pDisplayStatus.Training && false)
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


            //g.FillRectangle(Brushes.White, 0, 0, 200, 30);

            //g.DrawString(DisplayMousePosition.X.ToString() + ":" + DisplayMousePosition.Y.ToString(), SystemFonts.DefaultFont, Brushes.Black, 0, 0);
            //if (m_selectSourcePoint.HasValue)
            //    g.DrawString(m_selectSourcePoint.Value.X.ToString() + ":" + m_selectSourcePoint.Value.Y.ToString(), SystemFonts.DefaultFont, Brushes.Black, 100, 0);

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

        public void DrawSynapse(pPanel c, Point d, Graphics g)
        {
            pPanel dd = new pPanel(g);
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

        #endregion

        public void SetNeuralNetwork(NeuralNetwork aNet)
        {
            if (m_net != null)
                this.m_net.Dispose();

            this.m_net = aNet;

            foreach (pPanel p in pPanels)
            {
                foreach (Neuron n in aNet.Neuron)
                    if (p.Neuron.ID == n.ID)
                        p.Neuron = n;
            }

            #region Network Events

            this.Net.OnPulse += new NeuralNetwork.OnPulseDelegate(Net_OnPulse);

            this.Net.OnPulseBack += new NeuralNetwork.OnPulseBackDelegate(Net_OnPulseBack);
            
            this.Net.OnRefreshCyclesSec += new NeuralNetwork.OnRefreshCyclesSecDelegate(Net_OnRefreshCyclesSec);

            this.Net.OnStartTraing += new NeuralNetwork.OnStartTraingDelegate(Net_OnStartTraing);

            this.Net.OnStopTraing += new NeuralNetwork.OnStopTraingDelegate(Net_OnStopTraing);

            this.Net.OnResetLearning += new NeuralNetwork.OnResetLearningDelegate(Net_OnResetLearning);

            this.Net.OnResetKnowledgement += new NeuralNetwork.OnResetKnowledgementDelegate(Net_OnResetKnowledgement);

            #endregion
        }

        #region Network Events

        void Net_OnResetKnowledgement()
        {
            if (OnResetKnowledgement != null)
                OnResetKnowledgement();
        }

        void Net_OnResetLearning()
        {
            if (OnResetLearning != null)
                OnResetLearning();
        }

        void Net_OnStopTraing()
        {
            if (OnStopTraing != null)
                OnStopTraing();
        }

        void Net_OnStartTraing()
        {
            if (OnStartTraing != null)
                OnStartTraing();
        }

        void Net_OnRefreshCyclesSec(int Times)
        {
            if (OnRefreshCyclesSec != null)
                OnRefreshCyclesSec(Times);
        }

        void Net_OnPulseBack()
        {
            if (OnPulseBack != null)
                OnPulseBack();
        }

        void Net_OnPulse()
        {
            if (OnPulse != null)
                OnPulse();
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

        private void mspDataType_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == tspNewDomain)
            {
                if (OnNewDomain!=null)
                    OnNewDomain();
                
            }
        }


    }
}

