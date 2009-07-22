using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Drawing;
using primeira.pNeuron.Core;


namespace pNeuronEditor.TopologyEditor
{
    partial class  pDisplay
    {

        #region Select

        /// <summary>
        /// Get an array with all selected pPanel.
        /// </summary>
        public pPanel[] SelectedpPanels
        {
            get
            {
                return (from x in m_pPanels where x.Selected select x).ToArray();
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
        /// Select a pPanel.
        /// </summary>
        /// <param name="t">pPanel to select.</param>
        private void SelectCore(pPanel p)
        {
            bool old = p.Selected;
            
            p.Selected = true;

            if (old != p.Selected && OnSelectedPanelsChange!=null)
                OnSelectedPanelsChange();

            Invalidate(p.Bounds);
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
        /// Unselect a pPanel. 
        /// </summary>
        /// <param name="p">pPanel to unselect.</param>
        public void UnSelect(pPanel p)
        {
            bool old = p.Selected;

            p.Selected = false;

            if (old != p.Selected && OnSelectedPanelsChange != null)
                OnSelectedPanelsChange();

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

        /// <summary>
        /// Add a pPanel.
        /// </summary>
        /// <param name="n">The neuron represented by the pPanel.</param>
        /// <returns></returns>
        public pPanel Add(Neuron n)
        {
            pPanel p = new pPanel(m_graphics);

            p.Size = new Size(m_gridDistance, m_gridDistance);
            
            p.Parent = this;
            p.Neuron = n;
            m_pPanels.Add(p);
            p.Name = n.ID.ToString();
            p.Location = new Point(n.Left, n.Top);
            

            int i = m_pPanels.Count - 1;
            p.Text = i.ToString();

            if (OnNetworkChange != null)
                OnNetworkChange();

            return p;
        }

        public pPanel Add()
        {
            return Add(m_net.AddNeuron());
        }

        /// <summary>
        /// Remove a pPanel.
        /// </summary>
        /// <param name="p"></param>
        public void Remove(pPanel p)
        {
            m_pPanels.Remove(p);
            m_net.RemoveNeuron(p.Neuron);

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


    }
}
