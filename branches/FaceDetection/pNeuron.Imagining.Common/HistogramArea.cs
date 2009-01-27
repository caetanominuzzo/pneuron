using System;
using System.Collections.Generic;
using System.Text;

namespace pNeuron.Imagining.Common
{
    public class HistogramArea
    {
        #region Fields

        private int startPosition;
        private int endPosition;
        private int area;

        #endregion

        #region Properties

        public int StartPosition
        {
            get { return startPosition; }
            set { startPosition = value; }
        }

        public int EndPosition
        {
            get { return endPosition; }
            set { endPosition = value; }
        }

        public int Area
        {
            get { return area; }
            set { area = value; }
        }

        public int AreaByWidth
        {
            get { return endPosition == 0 ? 0 : area / (endPosition - startPosition); }
        }

        #endregion
    }
}
