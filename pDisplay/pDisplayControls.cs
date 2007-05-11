using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using primeira.pNeuron.Core;
using System.Drawing.Imaging;
using System.Reflection;

namespace primeira.pNeuron
{
    partial class  pDisplay
    {
        public List<pPanel> pPanels;
        public pPanel[] SelectedpPanels
        {
            get
            {
                List<pPanel> t = new List<pPanel>();
                foreach (pPanel p in pPanels)
                    if(p.Selected)
                        t.Add(p);

                return t.ToArray();
            }
        }

        public pPanel[] HighlightedpPanels
        {
            get
            {
                List<pPanel> t = new List<pPanel>();
                foreach (pPanel p in pPanels)
                    if (p.Highlighted)
                        t.Add(p);
                if (t.Count == 2)
                {
                    System.Diagnostics.Debug.Assert(t.Count == 2, "break");
                }
                    
                return t.ToArray();
            }
        }

        private void SelectCore(pPanel[] t)
        {
            foreach (pPanel p in t)
                SelectCore(p);
        }

        private void SelectCore(pPanel p)
        {
            if (!ShiftKey)
                p.Selected = true;
            else p.Selected = !p.Selected;
        }

        void Select(pPanel[] t)
        {
            foreach (pPanel p in t)
                Select(p);
        }

        void Select(pPanel p)
        {
//            if(DisplayStatus != pDisplayStatus.Selecting && !ShiftKey)
//                UnSelect();

            SelectCore(p);

            if (!CtrlKey)
            {
                if (p.Groups.Count > 0)
                {
                    foreach (int iGroup in p.Groups)
                    {
                        foreach (pPanel pp in m_groups[iGroup])
                        {
//                            if (DisplayStatus == pDisplayStatus.Selecting)
//                                m_lastSelectItems.Add(p);
                            if (pp == p)
                                continue;
                            SelectCore(pp);
                            Invalidate(pp.Bounds);
                        }
                    }
                }
            }
            if (DisplayStatus == pDisplayStatus.Selecting)
                m_lastSelectItems.Add(p);
            Invalidate(p.Bounds);
        }

        void UnSelect()
        {
            while (SelectedpPanels.Length > 0)
            {
                UnSelect(SelectedpPanels[0]);
            }
        }

        void UnSelect(pPanel p)
        {

            if (!CtrlKey)
            {
                if (p.Groups.Count > 0)
                {
                    foreach (int iGroup in p.Groups)
                    {
                        foreach (pPanel pp in m_groups[iGroup])
                        {
                            if (pp == p)
                                continue;
                            pp.Selected = false;
                            Invalidate(pp.Bounds);
                        }
                    }
                }
            }

            p.Selected = false;
            Invalidate(p.Bounds);
        }

        void Shift(pPanel p)
        {
            p.Selected = !p.Selected;
            Invalidate(p.Bounds);
        }

        pPanel GetpPanelAt(Point p)
        {
            Rectangle pBounds = new Rectangle();

            foreach (pPanel pp in pPanels)
            {

                pBounds = pp.Bounds;

                pBounds = new Rectangle(pBounds.X + AutoScrollPosition.X,
                    pBounds.Y + AutoScrollPosition.Y,
                    pBounds.Width,
                    pBounds.Height);

                if (pBounds.Contains(p))
                {
                    return pp;
                }
            }

            return null;
        }


        void HighLight(pPanel p)
        {
            p.Highlighted = true;

            Invalidate(p.Bounds);
        }

        void UnHighLight()
        {
            while (HighlightedpPanels.Length > 0)
            {
                UnHighLight(HighlightedpPanels[0]);
            }
        }

        void UnHighLight(pPanel p)
        {
            p.Highlighted = false;
            Invalidate(p.Bounds);
        }

        Rectangle GetGroupRectangle(List<pPanel> lp)
        {
            Rectangle r = lp[0].Bounds;

            foreach (pPanel p in lp)
            {
                r = ExpandRectangle(r, p.Bounds);
            }

            return r;
        }

    }
}
