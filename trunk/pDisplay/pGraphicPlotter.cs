using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using primeira.pNeuron.Core;

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

        public void AddData(double aData)
        {
            if (aData == 0)
                return; 
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

            

            double dMaxY = Height;
            double dMaxX = Width;

            Point pLast = new Point(0, Convert.ToInt32(dMaxY));
            Point pNew;

            Point pLastMedia = new Point(0, Convert.ToInt32(dMaxY));
            Point pNewMedia;

            double i = 0;

            double dMedia = 0;

            double[] dd = fData.ToArray();

            foreach (double d in dd)
            {
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

                    pLastMedia = pNewMedia;

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

        }
    }
}
