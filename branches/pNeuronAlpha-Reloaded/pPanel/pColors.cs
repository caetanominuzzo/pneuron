using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace primeira.pNeuron
{
    public class pColorBase
    {
        private Color Color;
        private Pen _pen;
        private Pen _selectedPen;
        private Brush _brush;
        private Brush _selectedBrush;


        public const byte SELECTED_PEN_WIDTH = 2;

        public pColorBase(Color c)
        {
            Color = c;
            this._pen = new Pen(SetAlpha(AddRGB(Color, -20), 100), 1);
            this._selectedPen = new Pen(SetAlpha(AddRGB(Color, -20), 100), SELECTED_PEN_WIDTH);
            this._brush = new SolidBrush(SetAlpha(AddRGB(Color, 50), 20));
            this._selectedBrush = new SolidBrush(SetAlpha(AddRGB(Color, 0), 30));
        }

        #region Static Color Transformation

        private static Color AddRGB(Color c, int Value)
        {
            return Color.FromArgb(c.A, (byte)(c.R + Value), (byte)(c.G + Value), (byte)(c.B + Value));
        }

        private static Color SetAlpha(Color c, byte Value)
        {
            return Color.FromArgb(Value, c.R, c.G, c.B);
        }

        #endregion

        public Pen Pen
        {
            get { return this._pen; }
        }

        public Pen SelectedPen
        {
            get { return this._selectedPen; }
        }

        public Brush Brush
        {
            get { return this._brush; }
        }

        public Brush SelectedBrush
        {
            get { return this._selectedBrush; }
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
           Silver,
            Red,
            Orange,
            Yellow,
            Green,
            Cyan,
            Blue,
            Purple,
            Pink,
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

