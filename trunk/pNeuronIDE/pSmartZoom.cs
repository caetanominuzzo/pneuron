using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using primeira.pNeuron.Core;
using System.IO;
using System.Threading;
using primeira.pRandom;


namespace primeira.pNeuron
{
    public partial class pSmartZoom : DockContent, IpDocks, pISmartZoom
    {

        private Point? DownPoint = null;
        private bool m_moving = false;

        private Bitmap m_cache = null;
        private Graphics m_zoomGraphics;
        private Graphics m_zoomChaceGraphics;


        public pSmartZoom()
        {
            InitializeComponent();

            m_cache = new Bitmap(Width, Height);
            m_zoomGraphics = ZoomDisplay.CreateGraphics();
            m_zoomChaceGraphics = Graphics.FromImage(m_cache);

            ZoomDisplay.MouseMove += new MouseEventHandler(ZoomDisplay_MouseMove);
            ZoomDisplay.MouseDown += new MouseEventHandler(ZoomDisplay_MouseDown);
            ZoomDisplay.MouseUp += new MouseEventHandler(ZoomDisplay_MouseUp);
            m_zoomGraphics = ZoomDisplay.CreateGraphics();

        }

        void ZoomDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            if (!Parent.ThereIsAnActiveDocument())
                return;

            if (DownPoint.Value == ZoomDisplay.PointToClient(MousePosition) && !m_moving)
            {
                Point p = ZoomDisplay.PointToClient(MousePosition);

                p = new Point(Parent.ActiveDocument.MainDisplay.UnMagnify(-p.X + ZoomDisplay.Width / 2, 0.1f),
                              Parent.ActiveDocument.MainDisplay.UnMagnify(-p.Y + ZoomDisplay.Height / 2, 0.1f));

                Parent.ActiveDocument.MainDisplay.Offset = p;
            }

            DownPoint = null;
            m_moving = false;
        }

        void ZoomDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            if (!Parent.ThereIsAnActiveDocument())
                return;

            Point p = ZoomDisplay.PointToClient(MousePosition);
            DownPoint = p;

            p = new Point(Parent.ActiveDocument.MainDisplay.UnMagnify(-p.X + ZoomDisplay.Width / 2, 0.1f),
                          Parent.ActiveDocument.MainDisplay.UnMagnify(-p.Y + ZoomDisplay.Height / 2, 0.1f));

            Parent.ActiveDocument.MainDisplay.Offset = p;

            
        }

        void ZoomDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (!Parent.ThereIsAnActiveDocument())
                return;
            
            if (DownPoint.HasValue)
            {
                Point mouse = ZoomDisplay.PointToClient(MousePosition);

                if (DownPoint.Value == mouse)
                    return;

                m_moving = true;

                Point p = new Point(mouse.X - DownPoint.Value.X, mouse.Y - DownPoint.Value.Y);

                Parent.ActiveDocument.MainDisplay.Offset = Point.Subtract(Parent.ActiveDocument.MainDisplay.Offset,
                    new Size(Parent.ActiveDocument.MainDisplay.UnMagnify(p.X, 0.1f), Parent.ActiveDocument.MainDisplay.UnMagnify(p.Y, 0.1f) ));

                DownPoint = mouse;

            }
        }

        public Size ZoomSize
        {
            get { return ZoomDisplay.Size; }
        }

        public Graphics ZoomCacheGraphics()
        {
            return m_zoomChaceGraphics;
        }

        public Graphics ZoomGraphics()
        {
            return m_zoomGraphics;
        }

        public void ZoomInvalidate()
        {
            m_zoomGraphics.DrawImage(m_cache, 0, 0);
          //  ZoomDisplay.Invalidate();
        }

        public Rectangle ZoomRectangle()
        {
            return ZoomDisplay.Bounds;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
           

            
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (Parent != null)
                ZoomDisplay.Invalidate();
        }

        #region IpDocks Members

        public new pNeuronIDE Parent
        {
            get { return DockPanel.Parent==null?null:(pNeuronIDE)DockPanel.Parent; }
        }

        #endregion

        private void btCenter_Click(object sender, EventArgs e)
        {
            if (!Parent.ThereIsAnActiveDocument())
                return;

            Parent.ActiveDocument.MainDisplay.Offset = new Point(0, 0);
        }

        private void ZoomDisplay_Paint(object sender, PaintEventArgs e)
        {
            
            // if (m_cache == null)
            //{
            //    m_zoomGraphics.Clear(SystemColors.ControlDark);
            //}
            //else
            //{
            ////    m_zoomGraphics.Clear(Color.White);

            //}
        }


    }

    internal class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel() : base()
        {
            base.DoubleBuffered = true;
        }

    }
}
