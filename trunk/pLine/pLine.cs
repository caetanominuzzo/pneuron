using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using primeira.pNeuron;



namespace primeira.pNeuron
{
    public class pLine : PictureBox
    {
        private pPanel m_inputPanel;
        private pPanel m_outputPanel;

        horizontal h;
        vertical v;

        private enum directions
        {
            left = 1,
            right = 2,
            top =3,
            bottom = 4
        }

        private enum vertical
        {
            top = 0,
            middle = 25,
            bottom = 50
        }

        private enum horizontal
        {
            left = 0,
            middle = 25,
            right = 50
        }

        const int MINWIDTH = 30;
        const int MINHEIGHT = 30;

        public pLine(pPanel input, pPanel output)
        {
            m_inputPanel = input;
            m_outputPanel = output;


            this.BackColor = Color.Transparent;
            SetBounds();
            BackColor = Color.Tomato;

            /*
            Bitmap b = CreateBitmap(30, 30, Color.Violet);
            b.MakeTransparent(Color.White);
            BackgroundImageLayout = ImageLayout.Stretch;
            this.BackgroundImage = b;
             */

        }

        public pPanel InputPanel
        {
            get { return m_inputPanel; }
        }
        
        public pPanel OutputPanel
        {
            get { return m_outputPanel; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            return;
            Graphics g = e.Graphics;
            base.OnPaint(e);

            Point p1 = new Point();
            Point p2 = new Point();

            if (h == horizontal.left)
            {
                p1.X = this.Width;
                p2.X = 0;

            }
            else if(h == horizontal.middle)
            {
                p1.X = 2;
                p2.X = 2;
            }
            else if (h == horizontal.right)
            {
                p1.X = 0;
                p2.X = Width;
            }


            if (v == vertical.top)
            {
                p1.Y = this.Height;
                p2.Y = 0;

            }
            else if (v == vertical.middle)
            {
                p1.Y = 2;
                p2.Y = 2;
            }
            else if (v == vertical.bottom)
            {
                p1.Y = 0;
                p2.Y = Height;
            }



        }

        public  void SetBounds()
        {

                
                
            if (InputPanel.Top > OutputPanel.Top + OutputPanel.Height/2)
            {
                v = vertical.top;
            }
            else
                if (InputPanel.Top < OutputPanel.Top - OutputPanel.Height)
                {
                    v = vertical.bottom;
                }
                else
                {
                    v = vertical.middle;
                }


                if (InputPanel.Left > OutputPanel.Left + OutputPanel.Width /2)
                {
                    h = horizontal.left;
                }
                else
                    if (InputPanel.Left < OutputPanel.Left - OutputPanel.Width / 2)
                    {
                        h = horizontal.right;
                    }
                    else
                    {
                        h = horizontal.middle;
                    }


            switch (h)
            {
                case horizontal.middle:
                    {
                        this.Width = MINWIDTH;
                        this.Left = m_inputPanel.Left + (m_inputPanel.Width / 2);
                        break;
                    }
                case horizontal.left:
                    {
                        this.Width = (m_inputPanel.Left - m_inputPanel.Width/2) - (m_outputPanel.Left + m_outputPanel.Width);
                        this.Left = m_outputPanel.Left - m_outputPanel.Width/2;
                        break;
                    }
                case horizontal.right:
                    {
                        this.Width = (m_outputPanel.Left + m_outputPanel.Width / 2) - (m_inputPanel.Left + m_inputPanel.Width);
                        this.Left = m_inputPanel.Left + m_inputPanel.Width/2;
                        break;
                    }
            }

            switch (v)
            {
                case vertical.middle:
                    {
                        this.Height = MINHEIGHT;
                        this.Top = m_inputPanel.Top + (m_inputPanel.Height / 2);
                        break;
                    }
                case vertical.top:
                    {
                        this.Height = m_inputPanel.Top - (m_outputPanel.Top + m_outputPanel.Height);
                        this.Top = m_outputPanel.Top - m_outputPanel.Height/2;
                        break;
                    }
                case vertical.bottom:
                    {
                        this.Height = m_outputPanel.Top - (m_inputPanel.Top + m_inputPanel.Height);
                        this.Top = m_inputPanel.Top + m_inputPanel.Height/2;
                        break;
                    }
            }
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            SetBounds();
            
        }


        public Bitmap CreateBitmap(int width, int height, Color clr)
        {
            try
            {
                Bitmap bmp = new Bitmap(width, height);

                    for (int x = 0; x < bmp.Width; x++)
                    {
                        bmp.SetPixel(x, 15, clr);
                    }

                return bmp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
