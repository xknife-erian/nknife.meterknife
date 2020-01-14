using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NKnife.GUI.Wpf
{
    /// <summary>
    ///     rotateButton.xaml 的交互逻辑
    /// </summary>
    public partial class RotateButton : UserControl
    {
        // 设置按钮使能状态
        private double _AngleDelta;
        private int _AnglePerValue = 18;
        private int _BtnValue;
        private ImageSource _ImageDisable;
        private ImageSource _ImageDown;
        private ImageSource _ImageHover;
        private ImageSource _ImageUp;
        private bool _IsEnable = true;
        private bool _IsMouseDown;
        private Brush _TextColor;
        // 按钮的文本属性
        private FontFamily _TextFamily;
        private double _TextSize;
        private int _ValueMax = 100;
        private int _ValueMin;
        // 角度单位 每单位数值表示角度 可转动(valueMax*anglePerValue/360)圈

        public RotateButton()
        {
            InitializeComponent();
        }

        #region 属性赋值

        // 按钮可用
        public bool IsEnable
        {
            get { return _IsEnable; }
            set
            {
                _IsEnable = value;
                imageBtn.Source = _IsEnable ? _ImageUp : _ImageDisable;
            }
        }

        // 按钮弹起图片
        public ImageSource ImageUp
        {
            get { return _ImageUp; }
            set
            {
                _ImageUp = value;
                imageBtn.Source = _ImageUp;
            }
        }

        // 按钮划过图片
        public ImageSource ImageHover
        {
            get { return _ImageHover; }
            set { _ImageHover = value; }
        }

        // 按钮按下图片
        public ImageSource ImageDown
        {
            get { return _ImageDown; }
            set { _ImageDown = value; }
        }

        // 按钮禁用图片
        public ImageSource ImageDisable
        {
            get { return _ImageDisable; }
            set { _ImageDisable = value; }
        }

        // 按钮字体
        public FontFamily TextFamily
        {
            get { return _TextFamily; }
            set
            {
                _TextFamily = value;
                _LabelButton.FontFamily = _TextFamily;
            }
        }

        // 按钮字号
        public double TextSize
        {
            get { return _TextSize; }
            set
            {
                _TextSize = value;
                _LabelButton.FontSize = _TextSize;
            }
        }

        // 文字颜色
        public Brush TextColor
        {
            get { return _TextColor; }
            set
            {
                _TextColor = value;
                _LabelButton.Foreground = _TextColor;
            }
        }

        // 按钮数值
        public int Value
        {
            get { return _BtnValue; }
            set
            {
                if ((value > ValueMin) && (value < ValueMax))
                {
                    _BtnValue = value;
                    // 图像对齐
                    ValueReNew();
                }
            }
        }

        // 最小值
        public int ValueMin
        {
            get { return _ValueMin; }
            set { _ValueMin = value; }
        }

        // 最大值
        public int ValueMax
        {
            get { return _ValueMax; }
            set { _ValueMax = value; }
        }

        // 角度单位
        public int AnglePerValue
        {
            get { return _AnglePerValue; }
            set { _AnglePerValue = value; }
        }

        #endregion

        #region 按钮事件

        // 进入
        private void ellipseBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            if (_IsEnable)
            {
                if (null != _ImageHover)
                {
                    imageBtn.Source = _ImageHover;
                }
            }
        }

        // 按下
        private void ellipseBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_IsEnable)
            {
                _IsMouseDown = true;
                if (null != _ImageDown)
                {
                    imageBtn.Source = _ImageDown;
                    // 重新设置旋转中心点
                    //rotateImage.CenterX = imageBtn.ActualWidth / 2;
                    //rotateImage.CenterY = imageBtn.ActualHeight / 2;
                    //rotateEllipse.CenterX = ellipseBtn.ActualWidth / 2;
                    //rotateEllipse.CenterY = ellipseBtn.ActualHeight / 2;
                    // 圆形旋转对齐
                    Point P = e.GetPosition(sender as IInputElement);
                    //double angle = Math.Atan2(P.Y - rotateEllipse.CenterY, P.X - rotateEllipse.CenterX) * 180 / Math.PI;
                    double angle = Math.Atan2(P.Y - (_EllipseButton.ActualHeight / 2), P.X - (_EllipseButton.ActualWidth / 2)) * 180 / Math.PI;
                    _RotateEllipse.Angle = angle;
                    // 图像对齐
                    ValueReNew();
                }
            }
        }

        // 移动
        private void ellipseBtn_MouseMove(object sender, MouseEventArgs e)
        {
            if (_IsEnable)
            {
                //鼠标已按下
                if (_IsMouseDown)
                {
                    // 计算旋转角度
                    Point P = e.GetPosition(sender as IInputElement);
                    //double angle = Math.Atan2(P.Y - rotateEllipse.CenterY, P.X - rotateEllipse.CenterX) * 180 / Math.PI;
                    double angle = Math.Atan2(P.Y - (_EllipseButton.ActualHeight / 2), P.X - (_EllipseButton.ActualWidth / 2)) * 180 / Math.PI;
                    // 加上累积角度
                    angle += _AngleDelta;
                    // 大于一格的刻度
                    if (Math.Abs(angle) > _AnglePerValue)
                    {
                        // 与单元角度整除的数 作为数值和旋转的增量
                        int angleGrid = (int) angle/_AnglePerValue;
                        // 当前数值不超过最大最小值
                        int currentValue = angleGrid + _BtnValue;
                        if (currentValue < _ValueMin)
                        {
                            currentValue = _ValueMin;
                        }
                        if (currentValue > ValueMax)
                        {
                            currentValue = ValueMax;
                        }
                        // 更新数值
                        _BtnValue = currentValue;
                        //  触发数值更新事件
                        if (null != ValueChange)
                        {
                            ValueChange(this, null);
                        }
                        // 圆形旋转对齐
                        _RotateEllipse.Angle += _AnglePerValue*angleGrid;
                        // 余数作为新的累积角度
                        angle = angle%_AnglePerValue;
                    }
                    // 保存累积角度
                    _AngleDelta = angle;
                    // 图像对齐
                    ValueReNew();
                }
            }
        }

        // 弹起
        private void ellipseBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_IsEnable)
            {
                // 完成在控件上点击
                if (_IsMouseDown)
                {
                    _IsMouseDown = false;
                    imageBtn.Source = _ImageUp;
                    // 清空累积值
                    _AngleDelta = 0;
                    // 图像对齐
                    ValueReNew();
                }
            }
        }

        // 离开
        private void ellipseBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (_IsEnable)
            {
                _IsMouseDown = false;
                imageBtn.Source = _ImageUp;
                // 清空累积值
                _AngleDelta = 0;
                // 图像对齐
                ValueReNew();
            }
        }

        #endregion

        public event EventHandler ValueChange;

        // 图像对齐
        private void ValueReNew()
        {
            // 显示数值
            _LabelButton.Content = _BtnValue.ToString();
            // 计算旋转角度
            //double angle = (anglePerValue * btnValue) + angleDelta;
            double angle = (_AnglePerValue*_BtnValue);
            // 旋转图片
            rotateImage.Angle = angle;
        }
    }
}