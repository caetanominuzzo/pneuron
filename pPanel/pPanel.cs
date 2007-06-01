using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using primeira.pNeuron;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;
using primeira.pNeuron.Core;

namespace primeira.pNeuron
{

    public enum Operation
    {
        Move,
        Resize
    }

    public struct pStructNeuron
    {
        public double bias;
    }



    public class pPanel : Control
    {

        #region Browsable

        [Browsable(true)]
        public Dictionary<INeuron, NeuralFactor> Input { get { return ((pNeuron.Core.Neuron)Tag).Input; } } 

        [Browsable(true)]
        public List<Neuron> Output { get { return ((pNeuron.Core.Neuron)Tag).Output; } }

        [Browsable(true)]
        public String Name { get { return Text; } set {

                foreach (pPanel p in ((IpPanels)Parent).pPanels)
                {
                    if(p != this)
                        if (p.Name == value)
                            throw new Exception("Names must be unique.");
                }

            Text = value;
        } }

        [Browsable(false)]
        public String Text { get { return base.Text; } set { base.Text = value; } }


        [Browsable(true)]
        public double[] SynapseIN
        {
            get
            {
                List<double> d = new List<double>();
                foreach (Neuron n in ((pNeuron.Core.Neuron)Tag).Input.Keys)
                {
                    d.Add(((pNeuron.Core.Neuron)Tag).Input[n].Weight);
                }

                return d.ToArray();
            }
        }

        [Browsable(true)]
        public double[] SynapseOUT
        {
            get
            {
                List<double> d = new List<double>();
                foreach (Neuron n in ((pNeuron.Core.Neuron)Tag).Output)
                {
                    d.Add(n.Input[((pNeuron.Core.Neuron)Tag)].Weight);
                }

                return d.ToArray();
            }
        }

        #endregion

        #region Fuck!

        [Browsable(false)]
        public Size MinimumSize { get { return base.MinimumSize; } set { base.MinimumSize = value; } }

        [Browsable(false)]
        public Size MaximumSize { get { return base.MaximumSize; } set { base.MaximumSize = value; } }
        
        [Browsable(false)]
        public Padding Margin { get { return base.Margin; } set { base.Margin = value; } }

        [Browsable(false)]
        public bool CausesValidation { get { return base.CausesValidation; } set { base.CausesValidation = value; } }

        [Browsable(false)]
        public virtual Cursor Cursor { get { return base.Cursor; } set { base.Cursor = value; } }

        [Browsable(false)]
        public string AccessibleDescription { get { return base.AccessibleDescription; } set { base.AccessibleDescription = value; } }

        [Browsable(false)]
        public string AccessibleName { get { return base.AccessibleName; } set { base.AccessibleName = value; } }

        [Browsable(false)]
        public AccessibleRole AccessibleRole { get { return base.AccessibleRole; } set { base.AccessibleRole = value; } }

        [Browsable(false)]
        public virtual bool AllowDrop { get { return base.AllowDrop; } set { base.AllowDrop = value; } }


        [Browsable(false)]
        public virtual AnchorStyles Anchor { get { return base.Anchor; } set { base.Anchor = value; } }

        [Browsable(false)]
        public virtual Color BackColor { get { return base.BackColor; } set { base.BackColor = value; } }

        [Browsable(false)]
        public virtual Image BackgroundImage { get { return base.BackgroundImage; } set { base.BackgroundImage = value; } }

        [Browsable(false)]
        public virtual ImageLayout BackgroundImageLayout { get { return base.BackgroundImageLayout; } set { base.BackgroundImageLayout = value; } }

        [Browsable(false)]
        protected override bool CanRaiseEvents { get { return base.CanRaiseEvents; } }

        [Browsable(false)]
        public bool Capture { get { return base.Capture; } set { base.Capture = value; } }

        [Browsable(false)]
        public virtual ContextMenu ContextMenu { get { return base.ContextMenu; } set { base.ContextMenu = value; } }

        [Browsable(false)]
        public virtual ContextMenuStrip ContextMenuStrip { get { return base.ContextMenuStrip; } set { base.ContextMenuStrip = value; } }

        [Browsable(false)]
        protected virtual CreateParams CreateParams { get { return base.CreateParams; } }

        [Browsable(false)]
        public ControlBindingsCollection DataBindings { get { return base.DataBindings; } }

        [Browsable(false)]
        public static Color DefaultBackColor { get { return Color.White; } }

        [Browsable(false)]
        protected virtual Cursor DefaultCursor { get { return base.DefaultCursor; } }

        [Browsable(false)]
        public static Font DefaultFont { get { return DefaultFont; } }

        [Browsable(false)]
        public static Color DefaultForeColor { get { return DefaultForeColor; } }

        [Browsable(false)]
        protected virtual ImeMode DefaultImeMode { get { return base.DefaultImeMode; } }

        [Browsable(false)]
        protected virtual Padding DefaultMargin { get { return base.DefaultMargin; } }

        [Browsable(false)]
        protected virtual Size DefaultMaximumSize { get { return base.DefaultMaximumSize; } }

        [Browsable(false)]
        protected virtual Size DefaultMinimumSize { get { return base.DefaultMinimumSize; } }

        [Browsable(false)]
        protected virtual Padding DefaultPadding { get { return base.DefaultPadding; } }

        [Browsable(false)]
        protected virtual Size DefaultSize { get { return base.DefaultSize; } }

        [Browsable(false)]
        public virtual Rectangle DisplayRectangle { get { return base.DisplayRectangle; } }

        [Browsable(false)]
        public bool Disposing { get { return base.Disposing; } }

        [Browsable(false)]
        public virtual DockStyle Dock { get { return base.Dock; } set { base.Dock = value; } }

        [Browsable(false)]
        protected virtual bool DoubleBuffered { get { return base.DoubleBuffered; } set { base.DoubleBuffered = value; } }

        [Browsable(false)]
        public bool Enabled { get { return base.Enabled; } set { base.Enabled = value; } }

        [Browsable(false)]
        public virtual bool Focused { get { return base.Focused; } }

        [Browsable(false)]
        public virtual Font Font { get { return base.Font; } set { base.Font = value; } }

        [Browsable(false)]
        protected int FontHeight { get { return base.FontHeight; } set { base.FontHeight = value; } }

        [Browsable(false)]
        public virtual Color ForeColor { get { return base.ForeColor; } set { base.ForeColor = value; } }

        [Browsable(false)]
        public bool HasChildren { get { return base.HasChildren; } }

        [Browsable(false)]
        public ImeMode ImeMode { get { return base.ImeMode; } set { base.ImeMode = value; } }

        [Browsable(false)]
        public bool InvokeRequired { get { return base.InvokeRequired; } }

        [Browsable(false)]
        public Padding Padding { get { return base.Padding; } set { base.Padding = value; } }


        [Browsable(false)]
        protected bool ResizeRedraw { get { return base.ResizeRedraw; } set { base.ResizeRedraw = value; } }

        [Browsable(false)]
        public virtual RightToLeft RightToLeft { get { return base.RightToLeft; } set { base.RightToLeft = value; } }

        [Browsable(false)]
        protected virtual bool ScaleChildren { get { return base.ScaleChildren; } }

        [Browsable(false)]
        public override ISite Site { get { return base.Site; } set { base.Site = value; } }

        [Browsable(false)]
        public Size Size { get { return base.Size; } set { base.Size = value; } }

        [Browsable(false)]
        public int TabIndex { get { return base.TabIndex; } set { base.TabIndex = value; } }

        [Browsable(false)]
        public bool TabStop { get { return base.TabStop; } set { base.TabStop = value; } }

        [Browsable(false)]
        public object Tag { get { return base.Tag; } set { base.Tag = value; } }

        [Browsable(false)]
        public bool UseWaitCursor { get { return base.UseWaitCursor; } set { base.UseWaitCursor = value; } }

        [Browsable(false)]
        public bool Visible { get { return base.Visible; } set { base.Visible = value; } }
        
        
        #endregion

        private const int MAX_GROUP_NUMBER = 10; //0 -- 9
        private Point m_mousePositionOnDown;
        private int m_groups;
        private Graphics m_graphics;
        private bool m_highlighed = false;
        private bool m_selected;
        private List<pLine> m_synapses;

        public pPanel(Graphics g)
        {
            m_groups = 0;
            m_graphics = g;
            m_synapses = new List<pLine>();
        }

        public Pen GetPenStyle()
        {
            pColorBase p;

            pColorBase lp = null;
            Pen lpp = null;

            int i = Groups;
            lp = pColors.Colors[i];
            lpp = pColors.Colors[i].Pen;
         

            if (Selected)
            {
                lpp = lp.SelectedPen;
            }
            else
            {
                lpp = lp.Pen;
            }

            return lpp;
        }

        public Brush GetBrushtyle()
        {
            pColorBase p;

            pColorBase lp = null;
            Brush lpp = null;

            int i = Groups;
            lp = pColors.Colors[i];
            lpp= pColors.Colors[i].Brush;

            if (Highlighted)
            {
                lpp = lp.SelectedBrush;
            }
            else
            {
                lpp = lp.Brush;
            }

            return lpp;
        }

        public void Draw()
        {
            Draw(m_graphics);
        }

        public void Draw(Graphics g)
        {

            Brush brush = GetBrushtyle();
            Pen pen = GetPenStyle();



            int iCount = 1;

            int iRad = 360 / iCount;

  

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

                g.DrawPie(pen, Bounds.Left + (pen.Width),
                                 Bounds.Top + (pen.Width),
                                 Bounds.Width - (pen.Width * 2),
                                 Bounds.Height - (pen.Width * 2),
                                                             0,
                         iRad);



                //g.FillEllipse(new SolidBrush(Color.FromArgb(200, Color.White)),
                //        Bounds.Left + 4,
                //        Bounds.Top + 4,
                //        Bounds.Width - 8,
                //        Bounds.Height - 8);


                g.FillPie(brush,
                           Bounds.Left,
                           Bounds.Top,
                           Bounds.Width,
                           Bounds.Height,
                           0,
                           iRad);




        

            string s = Text;
            Font f = new Font(SystemFonts.MenuFont.SystemFontName, 10, FontStyle.Regular, GraphicsUnit.Pixel, 1, true);

            //g.TextContrast = 5;
            g.DrawString(s, f, new SolidBrush(Color.Black),
                -(g.MeasureString(s, f).Width / 2) + Bounds.Left + Bounds.Width / 2 + 2,
                -(g.MeasureString(s, f).Height / 2) + Bounds.Top + Bounds.Height / 2);


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

        [Browsable(false)]
        public int Groups
        {
            get { return m_groups; }
            set { m_groups = value; }
        }

        [Browsable(false)]
        public bool Highlighted
        {
            get { return m_highlighed; }
            set { m_highlighed = value; }
        }

        [Browsable(false)]
        public bool Selected
        {
            get { return m_selected; }
            set { m_selected = value; }
        }

        [Browsable(false)]
        public Point MousePositionOnDown
        {
            get { return m_mousePositionOnDown; }
            set { m_mousePositionOnDown = PointToClient(Parent.PointToScreen(value)); }
        }

    }
}
