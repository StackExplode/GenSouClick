using GenSouClick.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

namespace GenSouClick.Enterty
{
    class YeShen
    {

        private IntPtr _GetBoardHandle()
        {
            IntPtr hd2 = APIHelper.FindWindow(null, "夜神模拟器");
            hd2 = APIHelper.FindWindowEx(hd2, IntPtr.Zero, null, "ScreenBoardClassWindow");
            IntPtr hd = APIHelper.FindWindowEx(hd2, IntPtr.Zero, null, "QWidgetClassWindow");
            return hd;
        }

        private IntPtr _GetSubHandle()
        {
            IntPtr hd2 = APIHelper.FindWindow(null, "夜神模拟器");
            hd2 = APIHelper.FindWindowEx(hd2, IntPtr.Zero, null, "ScreenBoardClassWindow");
            hd2 = APIHelper.FindWindowEx(hd2, IntPtr.Zero, null, "QWidgetClassWindow");
            IntPtr hd = APIHelper.FindWindowEx(hd2, IntPtr.Zero, null, "sub");
            return hd;
        }

        public bool ClickPos(int x, int y)
        {
            IntPtr hd = _GetBoardHandle();
            if (hd == IntPtr.Zero)
                return false;
            IntPtr lParam = (IntPtr)((y << 16) + x);
            const uint downCode = 0x201; // Left click down code 
            const uint upCode = 0x202; // Left click up code 
            APIHelper.SendMessage(hd, downCode, (IntPtr)1, lParam); // Mouse button down 
            APIHelper.SendMessage(hd, upCode, IntPtr.Zero, lParam); // Mouse button up 
            return true;
        }

        public bool ClickPos(double per_x, double per_y)
        {
            int h, w;
            GetYshenBig(out w, out h);
            bool rt = ClickPos((int)(w * per_x), (int)(h * per_y));
            return rt;
        }

        public Color GetPixColor(int x, int y)
        {
            IntPtr hd = _GetBoardHandle();
            APIHelper.RECT rect = new APIHelper.RECT();
            APIHelper.GetWindowRect(hd, out rect);
            x += rect.Left;
            y += rect.Top;

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

        public Color GetAvgColor(int x, int y)
        {
            int rrr = 0, ggg = 0, bbb = 0,sum = 0;
            for (int i = x - 2; i <= x + 2; i++)
            {
                for (int j = y - 2; j <= y + 2; j++)
                {
                    Color c = GetPixColor(i, j);
                    rrr += c.R;
                    ggg += c.G;
                    bbb += c.B;
                    sum++;
                }
            }
            rrr /= sum;
            ggg /= sum;
            bbb /= sum;
            Color rt = Color.FromArgb(rrr, ggg, bbb);
            return rt;
        }

        public Color GetPixColor(double per_x, double per_y)
        {
            int h, w;
            GetYshenBig(out w, out h);
            Color rt = GetPixColor((int)(w * per_x), (int)(h * per_y));
            return rt;
        }

        public void GetYshenBig(out int witdh, out int height)
        {
            IntPtr hd = _GetBoardHandle();
            APIHelper.RECT rect = new APIHelper.RECT();
            APIHelper.GetWindowRect(hd, out rect);
            witdh = rect.Right - rect.Left;
            height = rect.Bottom - rect.Top;
        }

        public Point GetMouseRelativePos()
        {
            Point p = new Point();
            IntPtr hd = _GetBoardHandle();
            APIHelper.RECT rect = new APIHelper.RECT();
            APIHelper.GetWindowRect(hd, out rect);
            APIHelper.GetCursorPos(out p);
            p.X -= rect.Left;
            p.Y -= rect.Top;
            return p;
        }
    }
}
