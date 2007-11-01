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
    public partial class pSmartZoom : DockContent, IpDocks
    {



        public pSmartZoom()
        {
            DoubleBuffered = true;
            InitializeComponent();

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
            base.OnPaint(e);
            Parent.d_OnDisplayChange();
        }


        #region IpDocks Members

        public new pNeuronIDE Parent
        {
            get { return (pNeuronIDE)DockPanel.Parent; }
        }

        #endregion

        private void ZoomDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            tmMove.Start();
        }

        private void tmMove_Tick(object sender, EventArgs e)
        {

            if (!Parent.ThereIsAnActiveDocument())
                return;

           
            Point p = ZoomDisplay.PointToClient(MousePosition);

            if (!ZoomDisplay.Bounds.Contains(p))
                return;

            if (p.X > Width - 25)
                Parent.ActiveDocument.MainDisplay.OffsetX += 5;
            if (p.X <  25)
                Parent.ActiveDocument.MainDisplay.OffsetX -= 5;
            if (p.Y > Height - 25)
                Parent.ActiveDocument.MainDisplay.OffsetY += 5;
            if (p.Y < 25)
                Parent.ActiveDocument.MainDisplay.OffsetY -= 5;

            Parent.ActiveDocument.MainDisplay.Invalidate();
        }

        private void ZoomDisplay_Leave(object sender, EventArgs e)
        {
            tmMove.Stop();
        }
    }
}
