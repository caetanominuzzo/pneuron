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

    public class pPanel
    {

        #region Fields

        private string m_text;
        private Control m_parent;
        private Neuron m_neuron;
        private int m_height;
        private int m_width;
        private int m_left;
        private int m_top;
       

        public int Height
        {
            get { return m_height; }
            set { m_height = value; }
        }

        public int Width
        {
            get { return m_width; }
            set { m_width = value; }
        }

        public int Left
        {
            get { return m_left; }
            set { m_left = value; }
        }

        public int Top
        {
            get { return m_top; }
            set { m_top = value; }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle(Location, Size); }
            set
            {
                Location = value.Location;
                Size = value.Size;
            }
        }

        public Point Location
        {
            get { return new Point(Left, Top); }
            set { Left = value.X; Top = value.Y; }
        }

        public Size Size
        {
            get { return new Size(Width, Height); }
            set { Width = value.Width; Height = value.Height; }
        }

        #endregion

        #region Browsable

        [Browsable(true)]
        public INeuron[] Input { get { return Neuron.GetInputNeurons(); } } 

        [Browsable(true)]
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
        public double SigmoidedValue
        {
            get
            {
                return Util.UnSigmoid(Neuron.Value);
            }
        }

        [Browsable(true)]
        public double Command
        {
            get
            {
                return Neuron.Command;
            }
        }

        [Browsable(true)]
        public DataTypes DataType
        {
            get
            {
                return Neuron.DataType;
            }
            set 
            {
                Neuron.DataType = value;
            }
        }
      

        #endregion

        public NeuronTypes NeuronType
        {
            get { return Neuron.NeuronType; }
            set { Neuron.NeuronType = value; }
        }

        public Control Parent
        {
            get { return m_parent; }
            set { m_parent = value; }
        }

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

        public pPanel(Graphics g)
        {
            m_groups = 0;
            m_graphics = g;
        }

        public Pen GetPenStyle()
        {

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

        public void Draw(float iZoom, int offsetX, int offsetY)
        {
            Draw(m_graphics, iZoom, offsetX, offsetY);
        }

        public void Draw(Graphics g, float iZoom, int offsetX, int offsetY)
        {

            Brush brush = GetBrushtyle();

            Pen pen = GetPenStyle();

            int iCount = 1;

            int iRad = 360 / iCount;

            if (iZoom > 0.1f)
            {
                g.DrawEllipse(pen, Magnify(Offset(Bounds.Left + (pen.Width), offsetX), iZoom),
                                 Magnify(Offset(Bounds.Top + (pen.Width), offsetY), iZoom),
                                 Magnify(Bounds.Width - (pen.Width * 2), iZoom),
                                 Magnify(Bounds.Height - (pen.Width * 2), iZoom));

                g.DrawEllipse(pen, Magnify(Offset(Bounds.Left + (pen.Width), offsetX), iZoom),
                      Magnify(Offset(Bounds.Top + (pen.Width), offsetY), iZoom),
                      Magnify(Bounds.Width - (pen.Width * 2), iZoom),
                      Magnify(Bounds.Height - (pen.Width * 2), iZoom));


                g.FillPie(brush,
                           Magnify(Offset(Bounds.Left, offsetX), iZoom),
                           Magnify(Offset(Bounds.Top, offsetY), iZoom),
                           Magnify(Bounds.Width, iZoom),
                           Magnify(Bounds.Height, iZoom),
                           0,
                           iRad);






                string s = Text;
                Font f = new Font(SystemFonts.MenuFont.SystemFontName, Magnify(10, iZoom), FontStyle.Regular, GraphicsUnit.Pixel, 1, true);

                g.TextContrast = 2;
                g.DrawString(s, f, new SolidBrush(Color.Black),
                        Offset(-g.MeasureString(s, f).Width / 2 + Magnify(Bounds.Left + Bounds.Width / 2 + 1, iZoom), offsetX),
                        Offset(-g.MeasureString(s, f).Height / 2 + Magnify(Bounds.Top + Bounds.Height / 2 + 1, iZoom), offsetY)
                    );

            }
            else
            {
                g.FillPie(new SolidBrush( pColorBase.AddRGB(pColorBase.SetAlpha(pen.Color, 255), -50) ),
                          Magnify(Offset(Bounds.Left, offsetX), iZoom),
                          Magnify(Offset(Bounds.Top, offsetY), iZoom),
                          Magnify(Bounds.Width, iZoom),
                          Magnify(Bounds.Height, iZoom),
                          0,
                          iRad);
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

        /// <summary>
        /// Apply a zoom factor on an int
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int Magnify(int i, float Zoom)
        {
            return Convert.ToInt32(i * Zoom);
        }

        /// <summary>
        /// Apply a zoom factor on a float
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public float Magnify(float f,float Zoom)
        {
            return f * Zoom;
        }

        public int Offset(int value, int offset)
        {
            return value + offset;
        }

        public int Offset(float value, int offset)
        {
            return (int)value + offset;
        }
    }

}
