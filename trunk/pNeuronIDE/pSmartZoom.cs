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
            if (DownPoint.Value == ZoomDisplay.PointToClient(MousePosition))
            {
                Point p = ZoomDisplay.PointToClient(MousePosition);
                p = new Point(Parent.ActiveDocument.MainDisplay.UnMagnify(-p.X + ZoomDisplay.Width / 2, 0.1f),
                              Parent.ActiveDocument.MainDisplay.UnMagnify(-p.Y + ZoomDisplay.Height / 2, 0.1f));

                Parent.ActiveDocument.MainDisplay.OffsetX = p.X;
                Parent.ActiveDocument.MainDisplay.OffsetY = p.Y;
                
                Parent.ActiveDocument.MainDisplay.Invalidate();
            }

            DownPoint = null;
        }

        void ZoomDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            DownPoint = ZoomDisplay.PointToClient(MousePosition);
        }

        void ZoomDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (DownPoint.HasValue)
            {
                Point p = Point.Subtract(ZoomDisplay.PointToClient(MousePosition), (Size)DownPoint.Value);

                Parent.ActiveDocument.MainDisplay.OffsetX -= Parent.ActiveDocument.MainDisplay.UnMagnify(p.X, 0.1f);
                Parent.ActiveDocument.MainDisplay.OffsetY -= Parent.ActiveDocument.MainDisplay.UnMagnify(p.Y, 0.1f);

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
            Parent.d_OnDisplayChange();
           
        }


        #region IpDocks Members

        public new pNeuronIDE Parent
        {
            get { return (pNeuronIDE)DockPanel.Parent; }
        }

        #endregion


    }
}
