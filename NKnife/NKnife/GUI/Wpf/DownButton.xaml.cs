using System;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NKnife.GUI.Wpf
{
    /// <summary>
    ///     UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class DownButton : UserControl
    {
        // 按钮的背景前景图片
        private readonly Timer msgTimer = new Timer();
        private ImageSource imageBack;
        private ImageSource imageFront;
        public bool isClickEvent = false;
        // 文本属性

        // 倒计时最大值
        private double maxCounter;
        // 无动作时间
        private double nowCounter;
        private Brush textColor;
        private FontFamily textFamily;
        private double textSize;

        public DownButton()
        {
            InitializeComponent();

            // 初始化海报倒计时计数器
            msgTimer.Elapsed += msgTimer_Elapsed;
            msgTimer.Interval = 200;
        }

        // 点击事件 或者倒计时结束触发

        // 按钮背景图片
        public ImageSource ImageBack
        {
            get { return imageBack; }
            set
            {
                imageBack = value;
                imageBtnBack.Source = imageBack;
            }
        }

        // 按钮前景图片
        public ImageSource ImageFront
        {
            get { return imageFront; }
            set
            {
                imageFront = value;
                imageBtnFront.Source = imageFront;
            }
        }

        // 文本字体
        public FontFamily TextFamily
        {
            get { return textFamily; }
            set
            {
                textFamily = value;
                labelCounter.FontFamily = textFamily;
            }
        }

        // 文本字号
        public double TextSize
        {
            get { return textSize; }
            set
            {
                textSize = value;
                labelCounter.FontSize = textSize;
            }
        }

        // 文本颜色
        public Brush TextColor
        {
            get { return textColor; }
            set
            {
                textColor = value;
                labelCounter.Foreground = textColor;
            }
        }

        // 倒计时最大值
        public double MaxCounter
        {
            get { return maxCounter; }
            set
            {
                nowCounter = 0;
                maxCounter = value;
                // 启动计时器
                msgTimer.Start();
            }
        }

        public event EventHandler Click = null;

        // 计数器到期
        private void msgTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //nowCounter = GetIdleTick() / 1000;
            nowCounter += 0.2;
            if (nowCounter < maxCounter + 0.2)
            {
                Dispatcher.Invoke(new invokeTimerTrick(reDraw));
            }
            else
            {
                isClickEvent = false;
                Dispatcher.Invoke(new invokeTimerTrick(buttonClick));
            }
        }

        private void reDraw()
        {
            // 显示剩余时间
            int timeLeft = Convert.ToInt32(maxCounter - nowCounter);
            int intMinute = timeLeft/60;
            int intSecond = timeLeft%60;
            labelCounter.Content = string.Format("{0:D2}:{1:D2}", intMinute, intSecond);
            // 裁剪剩余时间扇形区域
            double angle = 360 - 360*nowCounter/maxCounter;
            if (360 == angle)
            {
                imageBtnFront.Clip = new EllipseGeometry(new Rect(0, 0, imageBtnFront.ActualWidth, imageBtnFront.ActualHeight));
            }
            else
            {
                //PathGeometry PathGeometry1 = new PathGeometry();
                //PathFigure PathFigure1 = new PathFigure();
                //PathFigure1.StartPoint = new Point(imageBtnFront.ActualWidth / 2, imageBtnFront.ActualHeight / 2);
                //LineSegment LineSegment1 = new LineSegment();
                //LineSegment1.Point = new Point(imageBtnFront.ActualWidth / 2, 0);
                //PathFigure1.Segments.Add(LineSegment1);
                //double offsetX = Math.Sin(angle * Math.PI / 180);
                //double offsetY = Math.Cos(angle * Math.PI / 180);
                //Point endPoint = new Point(imageBtnFront.ActualWidth * (1 + offsetX) / 2, imageBtnFront.ActualHeight * (1 - offsetY) / 2);
                //ArcSegment ArcSegment1 = new ArcSegment(
                //  endPoint,
                //  new Size(imageBtnFront.ActualWidth / 2, imageBtnFront.ActualHeight / 2),
                //  angle,
                //  (angle > 180),
                //  SweepDirection.Clockwise,
                //  true);
                //PathFigure1.Segments.Add(ArcSegment1);
                //PathGeometry1.Figures.Add(PathFigure1);
                //imageBtnFront.Clip = PathGeometry1;
                // 顺时针
                var PathGeometry1 = new PathGeometry();
                var PathFigure1 = new PathFigure();
                // 起点为中心点
                PathFigure1.StartPoint = new Point(imageBtnFront.ActualWidth/2, imageBtnFront.ActualHeight/2);
                // 连接到扇形的圆弧起点
                var LineSegment1 = new LineSegment();
                double offsetX = Math.Sin(angle*Math.PI/180);
                double offsetY = Math.Cos(angle*Math.PI/180);
                LineSegment1.Point = new Point(imageBtnFront.ActualWidth*(1 - offsetX)/2, imageBtnFront.ActualHeight*(1 - offsetY)/2);
                PathFigure1.Segments.Add(LineSegment1);
                // 圆弧 终点在正上方
                var ArcSegment1 = new ArcSegment(
                    new Point(imageBtnFront.ActualWidth/2, 0),
                    new Size(imageBtnFront.ActualWidth/2, imageBtnFront.ActualHeight/2),
                    angle,
                    (angle > 180),
                    SweepDirection.Clockwise,
                    true);
                PathFigure1.Segments.Add(ArcSegment1);
                // 对图像控件进行裁剪
                PathGeometry1.Figures.Add(PathFigure1);
                imageBtnFront.Clip = PathGeometry1;
            }
        }

        // 点击事件
        private void imageBtnBack_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isClickEvent = true;
            buttonClick();
        }

        private void buttonClick()
        {
            // 停止计时器
            msgTimer.Stop();
            // 触发点击事件
            if (null != Click) Click(null, null);
        }

        public void stopCount()
        {
            // 停止计时器
            msgTimer.Stop();
        }

        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        /// <summary>
        ///     获取最后一次输入经过的时间
        /// </summary>
        /// <returns></returns>
        private int GetIdleTick()
        {
            var lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = Marshal.SizeOf(lastInputInfo);
            if (!GetLastInputInfo(ref lastInputInfo)) return 0;
            return Environment.TickCount - (int) lastInputInfo.dwTime;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LASTINPUTINFO
        {
            [MarshalAs(UnmanagedType.U4)] public int cbSize;
            [MarshalAs(UnmanagedType.U4)] public readonly uint dwTime;
        }

        private delegate void invokeTimerTrick();
    }
}