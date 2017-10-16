using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;


namespace GenSouClick.DebugTest
{
#if DEBUG
    static class StaticTest
    {
        public static Color GetColor(int x,int y)
        {
            IntPtr ptr_dc = Helpers.APIHelper.GetDC(IntPtr.Zero);
            int cl = Helpers.APIHelper.GetPixel(ptr_dc, x, y);
            Color rt = Color.FromArgb
                (
                    cl & 0x000000FF,
                    (cl & 0x0000FF00) >> 8,
                    (cl & 0x00FF0000) >> 16
                );
            return rt;
        }
    }
#endif
}
