using System;
using System.Drawing;
using System.Windows.Forms;

namespace MeterKnife.Util.Wrapper
{
    public class SnipScreen
    {
        public static Bitmap SnipFullScreen()
        {
            //获取屏幕句柄，即源显示设备句柄
            IntPtr dc1 = CreateDC("DISPLAY", null, null, (IntPtr)null);
            Graphics hdcGrpx = Graphics.FromHdc(dc1);
            //初始化Bitbmp实例
            Bitmap screenBmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, hdcGrpx);
            Graphics bmpGrpx = Graphics.FromImage(screenBmp);
            dc1 = hdcGrpx.GetHdc();
            //获取Bitbmp实例对应的句柄
            IntPtr dc2 = bmpGrpx.GetHdc();
            //利用Bitblt函数，把当前屏幕拷贝到创建的Bitbmp实例中
            BitBlt(dc2, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, dc1, 0, 0, 13369376);
            hdcGrpx.ReleaseHdc(dc1);
            bmpGrpx.ReleaseHdc(dc2);
            return screenBmp;
        }

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern bool BitBlt(
            IntPtr hdcDest, // 目标DC的句柄
            int nXDest,     // 目标设备左上角的X坐标
            int nYDest,     // 目标设备左上角的Y坐标
            int nWidth,     // 目标设备的宽度
            int nHeight,    // 目标设备的长度
            IntPtr hdcSrc,  // 源DC的句柄
            int nXSrc,      // 源设备左上角的X坐标
            int nYSrc,      // 源设备左上角的Y坐标
            Int32 dwRop     // 光栅的处理数值
            );

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern IntPtr CreateDC(
            string lpszDriver,  // 驱动名称
            string lpszDevice,  // 设备名称
            string lpszOutput,  // 没用，为NULL
            IntPtr lpInitData	// 打印选项数据
            );
    }
}
