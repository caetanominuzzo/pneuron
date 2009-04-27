using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace primeira.pNeuron
{
    public class pUtils
    {
        static public Point MeasureDisplayString(Graphics graphics, string text,
                                            Font font)
        {

            SizeF f = graphics.MeasureString(text, font);

            return new Point((int)f.Width, (int)f.Height);
        }
    }

  
}
