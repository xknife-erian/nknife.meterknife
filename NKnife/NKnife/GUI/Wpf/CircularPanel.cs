using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NKnife.GUI.Wpf
{
    /// <summary>
    ///     圆形布局面板
    ///     核心是要解决Panel里面的控件如何排列，以及尺寸的问题。
    ///     实际上Panel布置它当中的控件位置和尺寸经历了两个阶段，第一个阶段是测量（Measure）阶段，
    ///     在这个阶段中父元素会逐一询问子元素所期望的尺寸，从而确定自己所期望的尺寸。第二个阶段
    ///     是布置（Arrange）阶段，在这个期间父元素会明确子元素的尺寸和位置。具体到编程模型里面，
    ///     主要涉及到要重载两个函数，一个是MeasureOverride，另一个是ArrangeOverride。
    ///     MeasureOverride函数的实现里面需要注意要做如下几件事：
    ///     （1）遍历所有包含的子元素，并且调用它们的Measure方法；
    ///     （2）调用完了Measure方法后，子元素的DesiredSize即是它们各自期望的尺寸；
    ///     您可以获得它们的DesiredSize属性；
    ///     （3）根据所包含的子元素的尺寸，计算自己所期望的尺寸，并返回该值。
    ///     注意MeasureOverride传递过来的参数，是父元素告诉子元素，它能够分配子元素的空间大小。
    ///     当然子元素所期望的尺寸可以大于父元素给子元素分配的尺寸大小
    /// </summary>
    public class CircularPanel : Panel
    {
        public enum AlignmentOptions
        {
            Left,
            Center,
            Right
        };

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof (double),
                typeof (CircularPanel),
                new PropertyMetadata(RadiusChanged));

        public static readonly DependencyProperty AngleItemProperty =
            DependencyProperty.Register("AngleItem",
                typeof (double),
                typeof (CircularPanel),
                new PropertyMetadata(AngleItemChanged));

        public static readonly DependencyProperty InitialAngleProperty =
            DependencyProperty.Register("InitialAngle",
                typeof (double),
                typeof (
                    CircularPanel),
                new PropertyMetadata(InitialAngleChanged));

        public static readonly DependencyProperty AlignProperty =
            DependencyProperty.Register("Align",
                typeof (AlignmentOptions),
                typeof (CircularPanel),
                new PropertyMetadata(AlignChanged));

        /// <summary>
        ///     旋转的中心点
        /// </summary>
        /// <value>The align.</value>
        [Category("Circular Panel")]
        public AlignmentOptions Align
        {
            get { return (AlignmentOptions) GetValue(AlignProperty); }
            set { SetValue(AlignProperty, value); }
        }

        /// <summary>
        ///     间隔角度
        /// </summary>
        /// <value>The angle item.</value>
        [Category("Circular Panel")]
        public double AngleItem
        {
            get { return (double) GetValue(AngleItemProperty); }
            set { SetValue(AngleItemProperty, value); }
        }

        /// <summary>
        ///     初始的角度
        /// </summary>
        /// <value>The initial angle.</value>
        [Category("Circular Panel")]
        public double InitialAngle
        {
            get { return (double) GetValue(InitialAngleProperty); }
            set { SetValue(InitialAngleProperty, value); }
        }

        /// <summary>
        ///     半径
        /// </summary>
        /// <value>The radius.</value>
        [Category("Circular Panel")]
        public double Radius
        {
            get { return (double) GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        private static void RadiusChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((CircularPanel) sender).Refresh();
        }

        private static void AngleItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((CircularPanel) sender).Refresh();
        }

        private static void InitialAngleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((CircularPanel) sender).Refresh();
        }

        private static void AlignChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((CircularPanel) sender).Refresh();
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var resultSize = new Size(0, 0);

            foreach (UIElement child in Children)
            {
                child.Measure(availableSize);
                resultSize.Width = Math.Max(resultSize.Width, child.DesiredSize.Width);
                resultSize.Height = Math.Max(resultSize.Height, child.DesiredSize.Height);
            }

            resultSize.Width =
                double.IsPositiveInfinity(availableSize.Width)
                    ? resultSize.Width
                    : availableSize.Width;

            resultSize.Height =
                double.IsPositiveInfinity(availableSize.Height)
                    ? resultSize.Height
                    : availableSize.Height;

            return resultSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Refresh();
            return base.ArrangeOverride(finalSize);
        }

        private void Refresh()
        {
            int count = 0;
            if (double.IsNaN(Width))
            {
                Width = 200;
            }
            if (double.IsNaN(Height))
            {
                Height = 200;
            }

            foreach (FrameworkElement element in Children)
            {
                var r = new RotateTransform();
                double alignX = 0;
                double alignY = 0;
                switch (Align)
                {
                    case AlignmentOptions.Left:
                        alignX = 0;
                        alignY = 0;
                        break;
                    case AlignmentOptions.Center:
                        alignX = element.DesiredSize.Width/2;
                        alignY = element.DesiredSize.Height/2;
                        break;
                    case AlignmentOptions.Right:
                        alignX = element.DesiredSize.Width;
                        alignY = element.DesiredSize.Height;
                        break;
                }
                r.CenterX = alignX;
                r.CenterY = alignY;
                r.Angle = (AngleItem*count++) - InitialAngle;
                element.RenderTransform = r;
                double x = Radius*Math.Cos(Math.PI*r.Angle/180);
                double y = Radius*Math.Sin(Math.PI*r.Angle/180);

                if (!(double.IsNaN(Width)) && !(double.IsNaN(Height)) && !(double.IsNaN(alignX)) &&
                    !(double.IsNaN(alignY)) && !(double.IsNaN(element.DesiredSize.Width)) &&
                    !(double.IsNaN(element.DesiredSize.Height)))
                {
                    element.Arrange(new Rect(x + Width/2 - alignX, y + Height/2 - alignY,
                        element.DesiredSize.Width, element.DesiredSize.Height));
                }
            }
        }
    }
}