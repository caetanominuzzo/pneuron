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



        public pGraphicPlotter()
        {
            fData = new List<double>(100);
        }


        public void AddData(double aData)
        {
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

            double dMaxY = Height / 2;
            double dMaxX = Width;

            fZoom = dMaxY / fMaxValue;

            if (double.IsInfinity(fZoom))
            {
                fZoom = 1;
            }

            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            DrawLines(e.Graphics);

            double dMaxY = Height / 2;
            double dMaxX = Width;

            Point pLast = new Point(0,  Convert.ToInt32(dMaxY));
            

            double i = 0;

            foreach(double d in fData)
            {
                e.Graphics.DrawLine(
                        new Pen(Color.Black),
                        pLast,
                        new Point(
                            Convert.ToInt32(dMaxX * i / 100),
                            Convert.ToInt32( d * fZoom + dMaxY)));

                pLast = new Point(
                            Convert.ToInt32(dMaxX * i / 100),
                            Convert.ToInt32(d * fZoom + dMaxY));

                i++;
            }
        }

        private void DrawLines(Graphics g)
        {
            if (double.IsInfinity(fMaxValue))
                return;
            for(int i= 0; i<Height;i+=10 )
            {
                g.DrawLine(new Pen(Color.Gray),
                        new Point(0,
                            Convert.ToInt32(i * fMaxValue * fZoom / 100)),
                        new Point(Width,
                            Convert.ToInt32(i * fMaxValue * fZoom / 100)));
            }
        }
    }
}
