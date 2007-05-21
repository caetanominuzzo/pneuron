using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace primeira.pNeuron
{
    public class pColorBase
    {
        private Color Color;

        public const byte COLOR_RGB_MARGIN = 10; //0 - 255
        public const byte SELECTED_PEN_WIDTH = 2;


        public pColorBase(Color c)
        {
            Color = SetRGBMinMax(c);
        }

        #region Static Color Transformation

        private static Color AddRGB(Color c, int Value)
        {
            return Color.FromArgb(c.A, mm(c.R + Value), mm(c.G + Value), mm(c.B + Value));
        }

        private static Color SetAlpha(Color c, int Value)
        {
            return Color.FromArgb(Value, c.R, c.G, c.B);
        }

        private static Color SetRGBMinMax(Color c)
        {
            return Color.FromArgb(c.A, mm(c.R), mm(c.G), mm(c.B));
        }

        private static int mm(int i1)
        {
            //The COLOR_RGB_MARGIN will avoid defaul use of mm() to set the max value to RGB colors
            return mm(i1, 0 + COLOR_RGB_MARGIN, 255 - COLOR_RGB_MARGIN);
        }

        private static int mm(int i1, int i2, int i3)
        {
            return Math.Max(Math.Min(i1, i3), i2);
        }

        #endregion

        public Pen Pen
        {
            get { return new Pen(SetAlpha(AddRGB(Color, -20), 100), 1); }
        }

        public Pen SelectedPen
        {
            get { return new Pen(SetAlpha(AddRGB(Color, -20), 100), SELECTED_PEN_WIDTH); }
        }

        public Brush Brush
        {
            get { return new SolidBrush(SetAlpha(AddRGB(Color, 50), 20)); }
        }

        public Brush SelectedBrush
        {
            get { return new SolidBrush( SetAlpha(AddRGB( Color, 0 ), 30) ); }
        }

        
    }

    public static class pColors
    {




        public static pColorBase Blue = new pColorBase(Color.Blue);
        public static pColorBase Red = new pColorBase(Color.Red);
        public static pColorBase Green = new pColorBase(Color.Green);
        public static pColorBase Yellow = new pColorBase(Color.Yellow);
        public static pColorBase Pink = new pColorBase(Color.Pink);
        public static pColorBase Brown = new pColorBase(Color.Brown);
        public static pColorBase Orange = new pColorBase(Color.Orange);
        public static pColorBase Purple = new pColorBase(Color.Purple);
        public static pColorBase Silver = new pColorBase(Color.Silver);
        public static pColorBase Black = new pColorBase(Color.Black);
        public static pColorBase RosyBrown = new pColorBase(Color.RosyBrown);
        public static pColorBase Cyan = new pColorBase(Color.Cyan);


        public static pColorBase[] Colors = new pColorBase[10] { 
           
            Red,
            Orange,
            Yellow,
            Green,
            Cyan,
            Blue,
            Purple,
            Pink,
            Red,
            Black

           //Green, 
           //Blue,
           //Red,  
           //Yellow,
           //Pink, 
           //Brown, 
           //Orange,
           //Purple,
           //Black,
           //RosyBrown
           
        };
 
        
    }
}

