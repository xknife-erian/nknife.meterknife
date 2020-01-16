using System;
using System.Drawing;
using System.Windows.Forms;

namespace MeterKnife.Util.GUI
{
    /// <summary>一个可承载并播放Gif动画的控件
    /// </summary>
    public class GifBox : Control
    {
        private bool _FirstTime = true;
        private Image _GifImage;

        public Image GifImage
        {
            get { return _GifImage; }
            set
            {
                _GifImage = value;
                Width = _GifImage.Width;
                Height = _GifImage.Height;
            }
        }

        /// <summary>图片帧更新时执行的动作
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        private void OnFramChanged(object obj, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //e.Graphics.DrawRectangle(new Pen(Color.Blue, 1), new Rectangle(0, 0, Width - 1, Height - 1));
            if (GifImage != null)
            {
                if (ImageAnimator.CanAnimate(GifImage) == false)
                {
                    //e.Graphics.Clear(Color.White);
                    e.Graphics.DrawImage(GifImage, Location.X, Location.Y);
                }
                else
                {
                    if (_FirstTime)
                    {
                        //这个函数其实只调用一次，以后只调用updateframes函数。
                        ImageAnimator.Animate(GifImage, OnFramChanged);
                        _FirstTime = false;
                    }
                    e.Graphics.Clear(Color.Transparent);
                    e.Graphics.DrawImage(GifImage, Location.X, Location.Y);
                    ImageAnimator.UpdateFrames(GifImage);
                }
            }
            else
            {
                base.OnPaint(e);
            }
        }
    }
}