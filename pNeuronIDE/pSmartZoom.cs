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

        public pSmartZoom()
        {
            DoubleBuffered = true;
            InitializeComponent();

            ZoomDisplay.MouseMove += new MouseEventHandler(ZoomDisplay_MouseMove);
            ZoomDisplay.MouseDown += new MouseEventHandler(ZoomDisplay_MouseDown);
            ZoomDisplay.MouseUp += new MouseEventHandler(ZoomDisplay_MouseUp);

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

                Parent.ActiveDocument.MainDisplay.OffsetX = p.X;
                Parent.ActiveDocument.MainDisplay.OffsetY = p.Y;
                
                Parent.ActiveDocument.MainDisplay.Invalidate();
            }

            DownPoint = null;
            m_moving = false;
        }

        void ZoomDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            if (!Parent.ThereIsAnActiveDocument())
                return;

            DownPoint = ZoomDisplay.PointToClient(MousePosition);
        }

        void ZoomDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (!Parent.ThereIsAnActiveDocument())
                return;
            
            if (DownPoint.HasValue)
            {
                m_moving = true;

                Point p = Point.Subtract(ZoomDisplay.PointToClient(MousePosition), (Size)DownPoint.Value);

                Parent.ActiveDocument.MainDisplay.Offset = new Point
                    (Parent.ActiveDocument.MainDisplay.UnMagnify(p.X, 0.1f),
                    Parent.ActiveDocument.MainDisplay.UnMagnify(p.Y, 0.1f));
                  

                DownPoint = ZoomDisplay.PointToClient(MousePosition);

       

                Parent.ActiveDocument.MainDisplay.Invalidate();
            }
        }

        public Size ZoomSize
        {
            get { return ZoomDisplay.Size; }
        }

        public Graphics ZoomGraphics()
        {
            return ZoomDisplay.CreateGraphics();
        }

        public Rectangle ZoomRectangle()
        {
            return ZoomDisplay.Bounds;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Parent.PaintMiniMap();
           
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if(Parent!=null)
                Parent.PaintMiniMap();
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

            Parent.ActiveDocument.MainDisplay.OffsetX = 0;
            Parent.ActiveDocument.MainDisplay.OffsetY = 0;

            Parent.ActiveDocument.MainDisplay.Invalidate();


        }


    }
}
