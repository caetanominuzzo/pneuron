using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AForge.Imaging;
using AForge.Video.DirectShow;
using pNeuron.Imagining.Common;

namespace pNeuron.Imagining.ObjectDetection
{
    public class Face
    {
        private int min, max, med;
        private int iEmCasa = 1;
        private HistogramSuspect suspect;
        private Rectangle result;

        public Rectangle Detect(Bitmap image)
        {
            #region Horizontal Area

            suspect = new HistogramSuspect();

            HorizontalIntensityStatistics horizontal = new HorizontalIntensityStatistics(image);

            //Find max & min values on histrogram
            min = image.Width * 255;
            max = 0;

            for (int i = 0; i < image.Width; i++)
            {
                if (min > horizontal.Gray.Values[i])
                    min = horizontal.Gray.Values[i];

                if (max < horizontal.Gray.Values[i])
                    max = horizontal.Gray.Values[i];
            }

            med = (min + max) / 2;

            //Find n suspects
            for (int i = 0; i < image.Width; i++)
            {

                if (horizontal.Gray.Values[i] * iEmCasa > med && suspect.Current == null)
                {
                    suspect.NewArea();
                    suspect.Current.StartPosition = i;
                }

                if (horizontal.Gray.Values[i] * iEmCasa < med && suspect.Current != null)
                {
                    suspect.Current.EndPosition = i;
                    suspect.CloseArea();
                }

                if (suspect.Current != null)
                    suspect.Current.Area += med - horizontal.Gray.Values[i] * -iEmCasa;
            }

            if (suspect.Current != null)
                suspect.Current.EndPosition = image.Width;

            if (suspect.Count > 0)
            {
                result = new Rectangle();
                HistogramArea r = suspect.Filter(AreaFilter.AreaByWidth, 1)[0];
                result.X = r.StartPosition;
                result.Width = r.EndPosition - r.StartPosition;
            }
            else result = new Rectangle();

            #endregion

            #region Vertical Area

            suspect = new HistogramSuspect();

            VerticalIntensityStatistics vertical = new VerticalIntensityStatistics(image);

            //Find max & min values on histrogram
            min = image.Height * 255;
            max = 0;

            for (int i = 0; i < image.Height; i++)
            {
                if (min > vertical.Gray.Values[i])
                    min = vertical.Gray.Values[i];

                if (max < vertical.Gray.Values[i])
                    max = vertical.Gray.Values[i];
            }

            med = (min + max) / 2;

            //Find n suspects
            for (int i = 0; i < image.Height; i++)
            {

                if (vertical.Gray.Values[i] * iEmCasa > med && suspect.Current == null)
                {
                    suspect.NewArea();
                    suspect.Current.StartPosition = i;
                }

                if (vertical.Gray.Values[i] * iEmCasa < med && suspect.Current != null)
                {
                    suspect.Current.EndPosition = i;
                    suspect.CloseArea();
                }

                if (suspect.Current != null)
                    suspect.Current.Area += med - vertical.Gray.Values[i] * -iEmCasa;
            }

            if (suspect.Current != null)
                suspect.Current.EndPosition = image.Height;

            if (suspect.Count > 0 && !result.IsEmpty)
            {
                result = new Rectangle();
                HistogramArea r = suspect.Filter(AreaFilter.AreaByWidth, 1)[0];
                result.Y = r.StartPosition;
                result.Height = r.EndPosition - r.StartPosition;
            }
            else result = new Rectangle();

            #endregion

            return result;
        }
    }
}
