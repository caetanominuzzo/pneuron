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
            bool old = p.Selected;
            if (!ShiftKey)
                p.Selected = true;
            else p.Selected = !p.Selected;

            if (old != p.Selected && OnSelectedPanelsChange!=null)
                OnSelectedPanelsChange();
        }

        public void Select(pPanel[] t)
        {
            foreach (pPanel p in t)
                Select(p);
        }

        public void Select(pPanel p)
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

        public void UnSelect()
        {
            while (SelectedpPanels.Length > 0)
            {
                UnSelect(SelectedpPanels[0]);
            }
        }

        public void UnSelect(pPanel p)
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

        public void Shift(pPanel p)
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


        public void HighLight(pPanel p)
        {
            p.Highlighted = true;

            Invalidate(p.Bounds);
        }

        public void UnHighLight()
        {
            while (HighlightedpPanels.Length > 0)
            {
                UnHighLight(HighlightedpPanels[0]);
            }
        }

        public void UnHighLight(pPanel p)
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

        int Contains(pPanel[] p1, pPanel[] p2)
        {
            int Has = 0;
            foreach (pPanel p in p1)
            {
                foreach (pPanel pp in p2)
                {
                    if (p == pp)
                    {
                        Has++;
                    }
                }
            }

            return Has;

        }

        #region Utils

      

        #endregion

        #region Groups

        public void Add(pPanel p, int GroupIndex)
        {
            m_groups[GroupIndex].Add(p);
            if(!p.Groups.Contains(GroupIndex))
                p.Groups.Add(GroupIndex);
            Invalidate(p.Bounds);

            if (OnTreeViewChange != null)
                OnTreeViewChange(GroupIndex);
        }

        public bool GroupIsSet(int i)
        {
            return m_groups[i] != null;
        }

        public pPanel[] GroupGetPanel(int i)
        {
            return m_groups[i].ToArray();
        }

        public void GroupFree(int iKey)
        {
            foreach (pPanel p in GroupGetPanel(iKey))
            {
                p.Groups.Remove(iKey);
                Invalidate(p.Bounds);
            }

            m_groups[iKey].Clear();

            if(OnTreeViewChange!=null)
                OnTreeViewChange(iKey);
        }

        public void GroupSelect(int i)
        {
            foreach (pPanel p in GroupGetPanel(i))
                Select(p);
        }

        public List<pPanel>[] Groups()
        {
            List<pPanel>[] d = new List<pPanel>[11];

            int i = 0;
            d[i] = new List<pPanel>();
            foreach (pPanel p in pPanels)
                if (p.Groups.Count == 0)
                    d[0].Add(p);


            foreach (List<pPanel> l in m_groups)
            {
                i++;
                d[i] = new List<pPanel>();
                foreach (pPanel p in l)
                    d[i].Add(p);
            }

            return d;
        }

        #endregion

    }
}
