using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace primeira.pNeuron
{



    public class pDragChildrenPanel : Panel
    {


        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (e.Control.GetType() == typeof(pPanel))
            {
                e.Control.Parent = this;
            }
           
            base.OnControlAdded(e);
        }

  

      


    }
}
