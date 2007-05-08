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
        public List<pPanel> SelectedpPanels;
        public List<pPanel> HighLightedpPanels;

        void Select(pPanel p)
        {
            if(DisplayStatus != pDisplayStatus.Selecting && !ShiftKey)
                UnSelect();
            
            p.Selected = true;
            SelectedpPanels.Add(p);
            if (p.Groups.Count > 0)
            {
                foreach (int iGroup in p.Groups)
                {
                    foreach (pPanel pp in m_groups[iGroup])
                    {
                        pp.Selected = true;
                        SelectedpPanels.Add(pp);
                        Invalidate(pp.Bounds);
                    }
                }
            }

            Invalidate(p.Bounds);
        }

        void UnSelect()
        {
            while (SelectedpPanels.Count > 0)
            {
                UnSelect(SelectedpPanels[0]);
            }
        }

        void UnSelect(pPanel p)
        {
            p.Selected = false;
            SelectedpPanels.Remove(p);

            Invalidate(p.Bounds);
        }

        void Shift(pPanel p)
        {
            p.Selected = !p.Selected;

            if (p.Selected)
                SelectedpPanels.Add(p);
            else
                SelectedpPanels.Remove(p);

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
            HighLightedpPanels.Add(p);

            Invalidate(p.Bounds);
        }

        void UnHighLight()
        {
            while (HighLightedpPanels.Count > 0)
            {
                UnHighLight(HighLightedpPanels[0]);
            }
        }

        void UnHighLight(pPanel p)
        {
            p.Highlighted = false;
            HighLightedpPanels.Remove(p);

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
