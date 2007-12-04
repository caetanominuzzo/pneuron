using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace primeira.pHistory
{
    public class pHistoryManager : TreeView
    {


        public delegate pHistoryItem HistoryNeededDelegate();
        public event HistoryNeededDelegate HistoryNeeded;



        private List<pHistoryItem> m_history = new List<pHistoryItem>();
        private Timer m_undoGranularity;

        private bool m_loadingDontPaint = false;

        public bool LowGranulatity
        {
            get {
                    if (m_undoGranularity.Enabled)
                    {
                        m_undoGranularity.Stop();
                        m_undoGranularity.Start();
                    }

                    return !m_undoGranularity.Enabled;
                }
        }


        public pHistoryManager()
        {
            m_undoGranularity = new Timer();
            m_undoGranularity.Tick += new EventHandler(m_undoGranularity_Tick);
            m_undoGranularity.Interval = 500;

            DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            ItemHeight = 35;
            ShowLines = false;
            ShowPlusMinus = false;
            ShowRootLines = false;
            HotTracking = false;
            
        }

        void m_undoGranularity_Tick(object sender, EventArgs e)
        {
            m_undoGranularity.Stop();

            if (HistoryNeeded != null)
            {
                AddHistory(HistoryNeeded());
                m_undoGranularity.Stop();
            }
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {

            if (m_loadingDontPaint)
                return;

            if (!e.Node.IsVisible)
                return;

            Rectangle rc = new Rectangle(e.Bounds.X - 2, e.Bounds.Y, this.ClientRectangle.Width, e.Bounds.Height);

            e.Graphics.FillRectangle(SystemBrushes.Window, rc);

            Point p = e.Bounds.Location;
            p.X = e.Node.Level * 3;

            p.X += 2;
            p.Y += 2;   

            pHistoryItem h = GetItem(e.Node);

            Rectangle tmp = new Rectangle(p, new Size(30,30));

            e.Graphics.DrawImage(h.Cache, tmp);

            p.X += 38;
            p.Y += 2; 
            e.Graphics.DrawString(h.Modified.ToShortDateString(), SystemFonts.CaptionFont, SystemBrushes.ActiveBorder, p);

            p.Y += 16;
            e.Graphics.DrawString(h.Modified.ToShortTimeString(), SystemFonts.DialogFont, SystemBrushes.ButtonShadow, p);

            rc.Width -= 2;
            rc.X += 1;
            rc.Height -= 1;

            if (e.Node.IsSelected)
            {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(50, Color.Blue)), rc);
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(10, Color.Blue)), rc);
                
            }

//            if (e.Node.IsExpanded)
//            {
//                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(10, Color.Blue)), rc);
//                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(50, Color.Blue)), rc);

////                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(10, Color.Blue)), rc);
//            }

        }

        public List<pHistoryItem> GetHistory()
        {
            List<pHistoryItem> history = new List<pHistoryItem>();
            foreach(TreeNode n in Nodes)
            {
                history.Add((pHistoryItem)n.Tag);
            }
            return history;
        }

        public void AddHistory(pHistoryItem HistoryItem)
        {
            m_undoGranularity.Start();
            TreeNode n = new TreeNode();
            n.Tag = HistoryItem;
            Nodes.Add(n);
        }

        public void AddHistory(pHistoryItem[] History)
        {

            foreach (pHistoryItem p in History)
            {
                AddHistory(p);
            }
        }

        public void Load(pHistoryItem[] history)
        {
            m_loadingDontPaint = true;
            Scrollable = false;

            foreach (pHistoryItem p in history)
            {
                TreeNode n = new TreeNode();
                n.Tag = p;
                Nodes.Add(n);
            }

            
            Scrollable = true;
            m_loadingDontPaint = false;

            Invalidate();
        }

        private pHistoryItem GetItem(TreeNode node)
        {
            return (pHistoryItem)node.Tag;
        }



    }
}
