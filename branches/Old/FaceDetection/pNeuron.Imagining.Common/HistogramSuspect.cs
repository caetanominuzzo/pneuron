using System;
using System.Collections.Generic;
using System.Text;

namespace pNeuron.Imagining.Common
{
    public class HistogramSuspect
    {
        #region Fields

        private List<HistogramArea> area;
        private HistogramArea current;

        #endregion

        #region Properties

        public List<HistogramArea> Area
        {
            get { return area; }
            set { area = value; }
        }

        public HistogramArea Current
        {
            get { return current; }
            set { current = value; }
        }

        public int Count
        {
            get { return area==null? 0 : area.Count; }
        }

        #endregion

        #region Methods

        public HistogramSuspect()
        {
            area = new List<HistogramArea>();
        }

        public void NewArea()
        {
            current = new HistogramArea();
            area.Add(current);
        }

        public void CloseArea()
        {
            current = null;
        }

        public HistogramArea[] Filter(AreaFilter filter, int max)
        {
            if(filter == AreaFilter.Area)
                Area.Sort(new AreaComparer());
            else if (filter == AreaFilter.AreaByWidth)
                Area.Sort(new AreaByWidthComparer());

            if (Area.Count > max)
                Area.RemoveRange(max, Area.Count - max);
            return Area.ToArray();
        }

        #endregion

    }

    #region Filter Enum & Comparer Classes

    public enum AreaFilter
    {
        Area,
        AreaByWidth,
    }

    internal class AreaComparer : IComparer<HistogramArea>
    {

        public int Compare(HistogramArea x, HistogramArea y)
        {
            if (x.Area > y.Area)
                return 1;
            else if (x.Area < y.Area)
                return -1;
            else return 0;

        }
    }

    internal class AreaByWidthComparer : IComparer<HistogramArea>
    {

        public int Compare(HistogramArea x, HistogramArea y)
        {
            if (x.AreaByWidth > y.AreaByWidth)
                return -1;
            else if (x.AreaByWidth < y.AreaByWidth)
                return 1;
            else return 0;

        }
    }

    #endregion
}
