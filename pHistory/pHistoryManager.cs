using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace primeira.pHistory
{
    public class pHistoryManager : TreeView
    {

        private List<pHistoryItem> m_history = new List<pHistoryItem>();


        public pHistoryManager()
        {

            DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            Indent = 15;
            ItemHeight = 35;
            ShowLines = false;
            ShowPlusMinus = false;
            ShowRootLines = false;
            
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            //if (!paint)
            //    return;

            if (!e.Node.IsVisible)
                return;

            Rectangle rc = new Rectangle(e.Bounds.X, e.Bounds.Y, this.ClientRectangle.Width, e.Bounds.Height);

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


        }

       

        public void AddHistory(pHistoryItem HistoryItem)
        {
            TreeNode n = new TreeNode();
            n.Tag = HistoryItem;
            Nodes.Add(n);

        }

        private pHistoryItem GetItem(TreeNode node)
        {
            return (pHistoryItem)node.Tag;
        }



    }
}
