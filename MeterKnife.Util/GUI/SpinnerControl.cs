using System;
using System.Drawing;
using System.Windows.Forms;

namespace MeterKnife.Util.GUI
{
    /// <summary>
    /// 一个仿照ForeFox的UI的旋转进度表示控件
    /// </summary>
    public class SpinnerControl : Control
    {
        private int lines = 8;
        private int current = 0;
        private Timer timer;

        public SpinnerControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        public int Lines
        {
            get { return lines; }
            set { this.lines = value; }
        }

        public void Start()
        {
            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (this.current >= this.lines - 1)
            {
                this.current = 0;
            }
            else
            {
                this.current++;
            }
            Invalidate();
        }

        public void Stop()
        {
            timer.Stop();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            double x, y;
            double radius;
            double half;
            int i;

            x = Width / 2;
            y = Height / 2;
            radius = System.Math.Min(Width / 2, Height / 2) - 5;
            half = lines / 2;

            for (i = 0; i < lines; i++)
            {
                double inset = 0.7 * radius;
                double t = (double)((i + lines - current) % lines) / lines;

                Color c = Color.FromArgb((int)(t * 255), 0, 0, 0);
                Pen pen = new Pen(c);
                pen.Width = 2;

                PointF start = new PointF((float)(x + (radius - inset) * System.Math.Cos(i * System.Math.PI / half)),
                           (float)(y + (radius - inset) * System.Math.Sin(i * System.Math.PI / half)));

                PointF end = new PointF((float)(x + radius * System.Math.Cos(i * System.Math.PI / half)),
                           (float)(y + radius * System.Math.Sin(i * System.Math.PI / half)));

                e.Graphics.DrawLine(pen, start, end);
                pen.Dispose();
            }

            base.OnPaint(e);
        }
    }
}
