using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime;
using System.Text;
using System.Drawing;
using System.Windows.Forms;


namespace GenSouClick.Helpers
{
    static class APIHelper
    {
        #region 特定数据结构
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left; // x position of upper-left corner
            public int Top; // y position of upper-left corner
            public int Right; // x position of lower-right corner
            public int Bottom; // y position of lower-right corner
        }

        //定义了辅助键的名称（将数字转变为字符以便于记忆，也可去除此枚举而直接使用数值）
        [Flags()]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8
        }
        #endregion

        #region 常量
        public static class Consts
        {
            public const int MOUSEEVENTF_MOVE = 0x0001;      //移动鼠标 
            public const int MOUSEEVENTF_LEFTDOWN = 0x0002; //模拟鼠标左键按下 
            public const int MOUSEEVENTF_LEFTUP = 0x0004; //模拟鼠标左键抬起 
            public const int MOUSEEVENTF_RIGHTDOWN = 0x0008; //模拟鼠标右键按下 
            public const int MOUSEEVENTF_RIGHTUP = 0x0010; //模拟鼠标右键抬起 
            public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020; //模拟鼠标中键按下 
            public const int MOUSEEVENTF_MIDDLEUP = 0x0040; //模拟鼠标中键抬起 
            public const int MOUSEEVENTF_ABSOLUTE = 0x8000; //标示是否采用绝对坐标
        }

        #endregion

        #region 方法
        //模拟鼠标事件
        [DllImport("user32")]
        public static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        //获取鼠标当前位置
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point pt);

        //寻找窗口
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);

        //获取窗体矩形
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        //根据句柄获取类名
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        //发送消息
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        //获取DC
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr Hwnd);

        //释放DC
        [DllImport("user32.dll")] 
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        //获取像素颜色
        [DllImport("gdi32.dll")]
        public static extern int GetPixel(IntPtr hDC, int x, int y);

        //如果函数执行成功，返回值不为0。
        //如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
            IntPtr hWnd,                //要定义热键的窗口的句柄
            int id,                     //定义热键ID（不能与其它ID重复）           
            KeyModifiers fsModifiers,   //标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效
            Keys vk                     //定义热键的内容
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
            IntPtr hWnd,                //要取消热键的窗口的句柄
            int id                      //要取消热键的ID
            );

        

        #endregion
    }
}
