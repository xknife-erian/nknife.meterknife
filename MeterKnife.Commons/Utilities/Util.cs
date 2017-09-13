using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MeterKnife.Utilities
{
    public static class Util
    {
        /// <summary>
        /// 保持比例图像缩放简易算法
        /// </summary>
        public static Rectangle AdjustSize(Rectangle boxRectangle, int imageWidth, int imageHeight)
        {
            var rect = Rectangle.Round(boxRectangle);

            // 原始宽高在指定宽高范围内，不作任何处理 
            if (imageWidth <= boxRectangle.Width && imageHeight <= boxRectangle.Height)
            {
                rect.Width = imageWidth;
                rect.Height = imageHeight;
            }
            else
            {
                // 取得比例系数 
                float w = imageWidth / (float)boxRectangle.Width;
                float h = imageHeight / (float)boxRectangle.Height;
                // 宽度比大于高度比 
                if (w > h)
                {
                    rect.Width = boxRectangle.Width;
                    rect.Height = (int)(w >= 1 ? Math.Round(imageHeight / w) : Math.Round(imageHeight * w));
                    rect.Y = rect.Y + (boxRectangle.Height - rect.Height) / 2;
                }
                // 宽度比小于高度比 
                else if (w < h)
                {
                    rect.Width = (int)(h >= 1 ? Math.Round(imageWidth / h) : Math.Round(imageWidth * h));
                    rect.Height = boxRectangle.Height;
                    rect.X = rect.X + (boxRectangle.Width - rect.Width) / 2;
                }
                // 宽度比等于高度比 
                else
                {
                    rect.Width = boxRectangle.Width;
                    rect.Height = boxRectangle.Height;
                }
            }
            return rect;
        }
    }
}
