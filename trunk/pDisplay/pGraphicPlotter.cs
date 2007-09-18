using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace primeira.pNeuron
{
    public class pGraphicPlotter : Control
    {
        private List<Double> fData;
        private Double fZoom = 1;

        private double fMaxValue = double.NegativeInfinity;
        private double fMinValue = double.PositiveInfinity;

        public Double[] Data
        {
            get { return fData.ToArray(); }
        }

        public pGraphicPlotter()
        {
            fData = new List<double>(100);
            DoubleBuffered = true;
        }

        private delegate void Assinc();

        public void AddData(double aData)
        {
            //if (fData.Count > 0)
            //    if (aData < fData[fData.Count - 1] / 5 || aData > fData[fData.Count - 1] * 5)
            //        aData = fData[fData.Count - 1] - aData;

            if (fData.Count == 100)
                fData.RemoveAt(0);
            fData.Add(aData);

            fMaxValue = double.NegativeInfinity;
            fMinValue = double.PositiveInfinity;


            foreach(double d in fData)
            {
                if (d > fMaxValue)
                    fMaxValue = d;
                if (d < fMinValue)
                    fMinValue = d;
            }

            double dMaxY = Height;
            double dMaxX = Width;

            fZoom = dMaxY / fMaxValue;

            if (double.IsInfinity(fZoom))
            {
                fZoom = 1;
            }

            this.Invoke(new Assinc(Refresh));
        }

        public void ClearData()
        {
            fData.Clear();

            fZoom = 1;

            fMaxValue = double.NegativeInfinity;
            fMinValue = double.PositiveInfinity;
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            e.Graphics.Clear(Color.White);

            DrawLines(e.Graphics);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            double dMaxY = Height;
            double dMaxX = Width;

            Point pLast = new Point(0,  Convert.ToInt32(dMaxY));
            Point pNew;

            Point pLastMedia = new Point(0, Convert.ToInt32(dMaxY));
            Point pNewMedia;

            double i = 0;

            double dMedia = 0;

            foreach(double d in fData)
            {
                //e.Graphics.DrawString(d.ToString("0.000000"), SystemFonts.DefaultFont, SystemBrushes.ControlDark,
                //    new Point(
                //            Convert.ToInt32(dMaxX * i / 100),
                //            Convert.ToInt32(d * fZoom + dMaxY)));

                pNew = new Point(
                            Convert.ToInt32(dMaxX * i / 100),
                            Convert.ToInt32(d * fZoom));

                e.Graphics.DrawLine(
                        new Pen(Color.Black),
                        pLast,
                        pNew);

                pLast = pNew;

                dMedia += d;
                
                i++;

                if (i % 20 == 0)
                {
                    dMedia = dMedia / 20;

                    pNewMedia = new Point(
                            Convert.ToInt32(dMaxX * i / 100),
                            Convert.ToInt32(dMedia * fZoom));

                    e.Graphics.DrawLine(
                            new Pen(Color.FromArgb(70, Color.Red), 3),
                            pLastMedia,
                            pNewMedia);

                    e.Graphics.DrawString(dMedia.ToString("0.000000"), SystemFonts.DefaultFont, new SolidBrush(Color.FromArgb(90, Color.Black)),
                        pNewMedia);


                    pLastMedia  = pNewMedia;

                    dMedia = 0;

                }

               


            }
        }

        private void DrawLines(Graphics g)
        {
            if (double.IsInfinity(fMaxValue))
                return;
            if(fMaxValue == 0)
                return;

            

            for(int i= 0; i<100;i+=10 )
            {
                g.DrawLine(new Pen(Color.LightGray),
                        new Point(0,
                            Convert.ToInt32(i * Height / 100)),

                        new Point(Width,
                            Convert.ToInt32(i * Height / 100)));

            }

            g.DrawString("0", SystemFonts.DefaultFont, new SolidBrush(Color.Black), new Point(10, (Height / 2) - 12));
            g.DrawString("0", SystemFonts.DefaultFont, new SolidBrush(Color.Black), new Point(Width - 20, (Height / 2) - 12));
        }
    }
}
