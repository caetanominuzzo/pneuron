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

namespace pNeuronEditor.Topology
{

    public class pPanel
    {

        #region Fields

        private string m_text;
        private Control m_parent;
        private Neuron m_neuron;
        private int m_height;
        private int m_width;

        //Its 20% quickly to store the bounds in a rectabgle than do a "new Rectangle" every time get_Bounds is called.
        private Rectangle _bounds;

        [Browsable(false)]
        public Rectangle Bounds
        {
            get {
                    return _bounds;
                }
            set {
                    _bounds = value;
                    Neuron.Left = _bounds.Left;
                    Neuron.Top = _bounds.Top;
                }
        }

        [Browsable(false)]
        public Point Location
        {
            get { return _bounds.Location; }
            set {
                    Neuron.Left = value.X;
                    Neuron.Top = value.Y;
                    _bounds.Location = value;
                }
        }

        [Browsable(false)]
        public Size Size
        {
            get { return _bounds.Size; }
            set { _bounds.Size = value; }
        }

        #endregion

        #region Browsable

        [Browsable(false)]
        public INeuron[] Input { get { return Neuron.GetInputNeurons(); } }

        [Browsable(false)]
        public INeuron[] Output { get { return Neuron.GetOutputNeurons(); } }

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
        public String Text { get { return m_text; } set { m_text = value; } }

        [Browsable(true)]
        public double[] SynapseIN
        {
            get
            {
                INeuron[] arNeuron = Neuron.GetInputNeurons();
                List<double> d = new List<double>();
                foreach (Neuron n in arNeuron)
                {
                    d.Add(Neuron.GetSynapseFrom(n).Weight);
                }

                return d.ToArray();
            }
        }

        [Browsable(true)]
        public double[] SynapseOUT
        {
            get
            {
                INeuron[] arNeuron = Neuron.GetOutputNeurons();
                List<double> d = new List<double>();
                foreach (Neuron n in arNeuron)
                {
                    d.Add(Neuron.GetSynapseTo(n).Weight);
                }

                return d.ToArray();
            }
        }

        [Browsable(true)]
        public double Bias
        {
            get
            {
                return Neuron.Bias.Weight;
            }
        }

        [Browsable(true)]
        public double Value
        {
            get
            {
                return Neuron.Value;
            }
        }

        [Browsable(true)]
        public double UnsigmoidValue
        {
            get
            {
                return SigmoidUtils.UnSigmoid(Neuron.Value);
            }
        }

        [Browsable(false)]
        public double Command
        {
            get
            {
                return Neuron.Command;
            }
        }

     

        #endregion

        [Browsable(true)]
        public NeuronTypes NeuronType
        {
            get { return Neuron.NeuronType; }
            set { Neuron.NeuronType = value; }
        }

        [Browsable(false)]
        public Control Parent
        {
            get { return m_parent; }
            set { m_parent = value; }
        }

        [Browsable(false)]
        public Neuron Neuron
        {
            get { return m_neuron; }
            set { m_neuron = value; }
        }

        private Point m_mousePositionOnDown;
        private int m_groups;
        private Graphics m_graphics;
        private bool m_highlighed = false;
        private bool m_selected = false;

        [Browsable(false)]
        public pPanel(Graphics g)
        {
            m_groups = 0;
            m_graphics = g;
        }

        [Browsable(false)]
        public Pen GetPenStyle()
        {
            if (Selected)
            {
                return _selectedPen;
            }
            else
            {
                return _pen;
            }
        }
        [Browsable(false)]
        public Brush GetBrushtyle()
        {
            return _brush;
        }

        private static SolidBrush _brush = new SolidBrush(Color.FromArgb(50, Color.Gray));

        private static Pen _pen = new Pen(Color.FromArgb(100, Color.Gray), 1.0f); //pColors.Colors[0].SelectedPen;
        private static Pen _selectedPen = new Pen(Color.FromArgb(100, Color.Gray), 2.0f); //pColors.Colors[0].SelectedPen;


        [Browsable(false)]
        public void Draw(Point AutoScrollPosition)
        {
            Draw(m_graphics, AutoScrollPosition);
        }

        public void Draw(Graphics g, Point AutoScrollPosition)
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

            Rectangle cBounds = new Rectangle(Bounds.X + AutoScrollPosition.X,
                Bounds.Y + AutoScrollPosition.Y,
                Bounds.Width,
                Bounds.Height);

                g.DrawEllipse(pen, cBounds.Left + (pen.Width),
                                 cBounds.Top + (pen.Width),
                                 cBounds.Width - (pen.Width * 2),
                                 cBounds.Height - (pen.Width * 2));

                g.DrawEllipse(pen, cBounds.Left + (pen.Width),
                     cBounds.Top + (pen.Width),
                     cBounds.Width - (pen.Width * 2),
                     cBounds.Height - (pen.Width * 2));



                //g.FillEllipse(new SolidBrush(Color.FromArgb(200, Color.White)),
                //        Bounds.Left + 4,
                //        Bounds.Top + 4,
                //        Bounds.Width - 8,
                //        Bounds.Height - 8);


                g.FillPie(brush,
                           cBounds.Left,
                           cBounds.Top,
                           cBounds.Width,
                           cBounds.Height,
                           0,
                           iRad);


            string s = Text;
            Font f = new Font(SystemFonts.MenuFont.SystemFontName, 10, FontStyle.Regular, GraphicsUnit.Pixel, 1, true);

            //g.TextContrast = 5;
            g.DrawString(s, f, new SolidBrush(Color.Black),
                -(g.MeasureString(s, f).Width / 2) + cBounds.Left + cBounds.Width / 2 + 2,
                -(g.MeasureString(s, f).Height / 2) + cBounds.Top + cBounds.Height / 2);


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
            set {

                Control t = new Control();
                this.Parent.Controls.Add(t);
                t.Location = this.Location;


                
                m_mousePositionOnDown = t.PointToClient( Parent.PointToScreen(value));
                    this.Parent.Controls.Remove(t);
            
            }

        }

    }
}
