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

        #region Select/Highlight

        /// <summary>
        /// Highligh pPanels.
        /// </summary>
        public void RefreshHighlight()
        {

            List<pPanel> toHighlight = new List<pPanel>();

            bool bGroupHighlight = false;
            if (!CtrlKey)
            {
                foreach (List<pPanel> lp in m_groups)
                {
                    if (lp == m_groups[0])
                        break;
                    if (lp != null)
                        if (lp.Count > 0)
                            if (GetGroupRectangle(lp).Contains(DisplayMousePosition))
                            {
                                foreach (pPanel pp in lp)
                                {
                                    toHighlight.Add(pp);
                                }
                                bGroupHighlight = true;
                            }
                }
            }

            if (!bGroupHighlight)
            {
                foreach (pPanel p in m_pPanels)
                {
                    if (p.Bounds.Contains(DisplayMousePosition))
                    {
                        toHighlight.Add(p);
                        break;
                    }
                }
            }


            foreach (pPanel p in m_pPanels)
            {
                if (toHighlight.Contains(p))
                {
                    if (!p.Highlighted)
                    {
                        HighLight(p);
                        Invalidate(p.Bounds);
                    }
                }
                else
                {
                    if (p.Highlighted)
                    {
                        UnHighLight(p);
                        Invalidate(p.Bounds);
                    }
                }
            }


        }

        /// <summary>
        /// Get an array with all selected pPanel.
        /// </summary>
        public pPanel[] SelectedpPanels
        {
            get
            {
                List<pPanel> t = new List<pPanel>();
                foreach (pPanel p in m_pPanels)
                    if(p.Selected)
                        t.Add(p);

                return t.ToArray();
            }
        }

        /// <summary>
        /// Get an array with all highlighted pPanel.
        /// </summary>
        public pPanel[] HighlightedpPanels
        {
            get
            {
                List<pPanel> t = new List<pPanel>();
                foreach (pPanel p in m_pPanels)
                    if (p.Highlighted)
                        t.Add(p);
                return t.ToArray();
            }
        }

        /// <summary>
        /// Select am array of pPanel. If ShiftKey switch select.
        /// </summary>
        /// <param name="t">Array of pPanels to select.</param>
        private void SelectCore(pPanel[] pPanels)
        {
            foreach (pPanel p in pPanels)
                SelectCore(p);
        }

        /// <summary>
        /// Select a pPanel. If ShiftKey switch select.
        /// </summary>
        /// <param name="t">pPanel to select.</param>
        private void SelectCore(pPanel p)
        {
            bool old = p.Selected;
            if (!ShiftKey)
                p.Selected = true;
            else p.Selected = !p.Selected;

            if (old != p.Selected && OnSelectedPanelsChange!=null)
                OnSelectedPanelsChange();
        }

        /// <summary>
        /// Select an array of pPanel. If !CtrlKey select all pPanels in same group.
        /// </summary>
        /// <param name="p">Array of pPanel to select.</param>
        public void Select(pPanel[] t)
        {
            foreach (pPanel p in t)
                Select(p);
        }

        /// <summary>
        /// Select a pPanel. If !CtrlKey select all pPanels in same group.
        /// </summary>
        /// <param name="p">pPanel to select.</param>
        public void Select(pPanel p)
        {

            SelectCore(p);

            if (!CtrlKey)
            {
                if (p.Groups != 0)
                {
                    foreach (pPanel pp in m_groups[p.Groups])
                        {
                            if (pp == p)
                                continue;
                            SelectCore(pp);
                            Invalidate(pp.Bounds);
                        }
                }
            }
            if (DisplayStatus == pDisplayStatus.Selecting)
                m_lastSelectItems.Add(p);
            Invalidate(p.Bounds);
        }

        /// <summary>
        /// Unselect all pPanels.
        /// </summary>
        public void UnSelect()
        {
            while (SelectedpPanels.Length > 0)
            {
                UnSelect(SelectedpPanels[0]);
            }
        }

        /// <summary>
        /// Unselect a pPanel. =If !CtrlKey unselect all pPanel on same group.
        /// </summary>
        /// <param name="p">pPanel to unselect.</param>
        public void UnSelect(pPanel p)
        {

            if (!CtrlKey)
            {
                if (p.Groups != 0)
                {
                    foreach (pPanel pp in m_groups[p.Groups])
                        {
                            if (pp == p)
                                continue;
                            pp.Selected = false;
                            Invalidate(pp.Bounds);
                        }
                }
            }

            p.Selected = false;
            Invalidate(p.Bounds);
        }

        /// <summary>
        /// HighLight a pPanel.
        /// </summary>
        /// <param name="p">pPanel do HighLight,</param>
        public void HighLight(pPanel p)
        {
            p.Highlighted = true;

            Invalidate(p.Bounds);
        }

        /// <summary>
        /// UnHighLight all pPanels.
        /// </summary>
        public void UnHighLight()
        {
            while (HighlightedpPanels.Length > 0)
            {
                UnHighLight(HighlightedpPanels[0]);
            }
        }

        /// <summary>
        /// UnHighLight a pPanel.
        /// </summary>
        /// <param name="p">pPanel to UnHighLight.</param>
        public void UnHighLight(pPanel p)
        {
            p.Highlighted = false;
            Invalidate(p.Bounds);
        }

        #endregion

        #region Utils

        /// <summary>
        /// Get a rectangle that contains all pPanel in a List.
        /// </summary>
        /// <param name="lp"></param>
        /// <returns></returns>
        public Rectangle GetGroupRectangle(List<pPanel> lp)
        {
            Rectangle r = lp[0].Bounds;

            foreach (pPanel p in lp)
            {
                r = ExpandRectangle(r, p.Bounds);
            }

            return r;
        }

        /// <summary>
        /// Returns how much pPanels in p2 exists in p1.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
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

        #endregion

        #region Groups

        /// <summary>
        /// Add a pPanel to a group.
        /// </summary>
        /// <param name="p">pPanel to add.</param>
        /// <param name="GroupIndex">New group.</param>
        public void Add(pPanel p, int GroupIndex)
        {

            if (p.Groups != 0)
            {
                m_groups[p.Groups].Remove(p);
                if (OnTreeViewChange != null)
                    OnTreeViewChange(p, pTreeviewRefresh.pPanelRemove);
            }

            m_groups[GroupIndex].Add(p);
            if (p.Groups != GroupIndex)
            {
                p.Groups = GroupIndex;
                Invalidate(p.Bounds);

                if (OnTreeViewChange != null)
                    OnTreeViewChange(p, pTreeviewRefresh.pPanelAdd);
            }
        }

        /// <summary>
        /// Gets all pPanels in a group.
        /// </summary>
        /// <param name="i">The group.</param>
        /// <returns></returns>
        public pPanel[] GroupGetPanel(int i)
        {
            return m_groups[i].ToArray();
        }

        /// <summary>
        /// Clear a group.
        /// </summary>
        /// <param name="iKey">Group to clear.</param>
        public void GroupFree(int iKey)
        {
            foreach (pPanel p in GroupGetPanel(iKey))
            {
                p.Groups = 0;
                Invalidate(p.Bounds);
            }

            m_groups[iKey].Clear();

            if(OnTreeViewChange!=null)
                OnTreeViewChange(iKey, pTreeviewRefresh.pGroupClear);
        }

        /// <summary>
        /// Select all pPanels in a group.
        /// </summary>
        /// <param name="i">Group to select.</param>
        public void GroupSelect(int i)
        {
            foreach (pPanel p in GroupGetPanel(i))
                Select(p);
        }

        /// <summary>
        /// Gets all groups.
        /// </summary>
        /// <returns></returns>
        public List<pPanel>[] Groups()
        {
            List<pPanel>[] d = new List<pPanel>[11];

            int i = 0;
            d[i] = new List<pPanel>();
            foreach (pPanel p in m_pPanels)
                if (p.Groups != 0)
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

        /// <summary>
        /// Add a pPanel.
        /// </summary>
        /// <param name="n">The neuron represented by the pPanel.</param>
        /// <returns></returns>
        public pPanel Add(Neuron n)
        {

            primeira.pNeuron.pPanel p = new primeira.pNeuron.pPanel(m_graphics);

            p.Width = m_gridDistance;
            p.Height = p.Width;
            p.Parent = this;
            p.Tag = n;
            m_pPanels.Add(p);
            int i = m_pPanels.Count - 1;
            p.Text = i.ToString();

            if (OnTreeViewChange != null)
                OnTreeViewChange(p, pTreeviewRefresh.pPanelAdd);

            if (OnNetworkChange != null)
                OnNetworkChange();

            return p;
        }

        /// <summary>
        /// Remove a pPanel.
        /// </summary>
        /// <param name="p"></param>
        public void Remove(pPanel p)
        {
            m_pPanels.Remove(p);

            if (OnTreeViewChange != null)
                OnTreeViewChange(p, pTreeviewRefresh.pPanelRemove);

            if (OnNetworkChange != null)
                OnNetworkChange();
        }

        /// <summary>
        /// Remove a pPanel.
        /// </summary>
        /// <param name="p"></param>
        public void Remove(pPanel[] p)
        {
            foreach (pPanel pp in p)
                Remove(pp);
        }

        #region IpPanels Members

        /// <summary>
        /// All pPanels.
        /// </summary>
        public List<pPanel> pPanels
        {
            get { return m_pPanels; }
        }

        #endregion

        #endregion

    }
}
