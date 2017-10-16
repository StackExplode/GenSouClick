using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GenSouClick.Enterty;
using System.Drawing;

namespace GenSouClick.Helpers
{
    /* struct PlaceAttr
    {
        public double Per_X;
        public double Per_Y;
        public Color color;
        public PlaceAttr(double x, double y, int r, int g, int b)
        {
            Per_X = x;
            Per_Y = y;
            color = Color.FromArgb(r, g, b);
        }
    }*/



    class PlaceAnalysiser
    {
        private YeShen yeshen;

        public PlaceAnalysiser(YeShen ys)
        {
            yeshen = ys;
        }

        public bool JudgeColor(Color real, Color c,int tor = 0)
        {
            int c1 = Math.Abs(real.R - c.R);
            int c2 = Math.Abs(real.G - c.G);
            int c3 = Math.Abs(real.B - c.B);
            if (c1 <= tor && c2 <= tor && c3 <= tor)
                return true;
            else
                return false;
        }

        public bool IsHere(List<PlaceAttr> pls)
        {
            
            foreach (PlaceAttr pa in pls)
            {
                Color ral = yeshen.GetPixColor(pa.Position.X, pa.Position.Y);
                if (!JudgeColor(ral, pa.AttrColor,pa.Tolerate))
                    return false;
            }
            return true;

        }

        



    }
}
