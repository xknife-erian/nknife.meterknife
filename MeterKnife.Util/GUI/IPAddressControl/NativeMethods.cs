using System;
using System.Runtime.InteropServices;

namespace MeterKnife.Util.GUI.IPAddressControl
{
   internal class NativeMethods
   {
      private NativeMethods() {}

      [DllImport( "user32.dll" )]
      public static extern IntPtr GetWindowDC( IntPtr hWnd );

      [DllImport( "user32.dll" )]
      public static extern int ReleaseDC( IntPtr hWnd, IntPtr hDC );

      [DllImport( "gdi32.dll", CharSet = CharSet.Unicode )]
      [return: MarshalAs( UnmanagedType.Bool )]
      public static extern bool GetTextMetrics( IntPtr hdc, out TEXTMETRIC lptm );

      [DllImport( "gdi32.dll", CharSet = CharSet.Unicode )]
      public static extern IntPtr SelectObject( IntPtr hdc, IntPtr hgdiobj );

      [DllImport( "gdi32.dll", CharSet = CharSet.Unicode )]
      [return : MarshalAs( UnmanagedType.Bool)]
      public static extern bool DeleteObject( IntPtr hdc );

      [Serializable, StructLayout( LayoutKind.Sequential, CharSet = CharSet.Unicode )]
      public struct TEXTMETRIC
      {
         public int tmHeight;
         public int tmAscent;
         public int tmDescent;
         public int tmInternalLeading;
         public int tmExternalLeading;
         public int tmAveCharWidth;
         public int tmMaxCharWidth;
         public int tmWeight;
         public int tmOverhang;
         public int tmDigitizedAspectX;
         public int tmDigitizedAspectY;
         public char tmFirstChar;
         public char tmLastChar;
         public char tmDefaultChar;
         public char tmBreakChar;
         public byte tmItalic;
         public byte tmUnderlined;
         public byte tmStruckOut;
         public byte tmPitchAndFamily;
         public byte tmCharSet;
      }
   }
}
