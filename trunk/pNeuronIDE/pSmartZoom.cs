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

            p = new Point(Parent.ActiveDocument.MainDisplay.UnMagnify(-p.X + ZoomDisplay.Width / 2, 0.1f),
                          Parent.ActiveDocument.MainDisplay.UnMagnify(-p.Y + ZoomDisplay.Height / 2, 0.1f));

            Parent.ActiveDocument.MainDisplay.Offset = p;

            DownPoint = ZoomDisplay.PointToClient(MousePosition);
        }

        void ZoomDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (!Parent.ThereIsAnActiveDocument())
                return;
            
            if (DownPoint.HasValue)
            {
                m_moving = true;

                Point mouse = ZoomDisplay.PointToClient(MousePosition);

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

            Parent.ActiveDocument.MainDisplay.Offset = new Point(0, 0);
        }


    }
}
