using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NKnife.GUI.WinForm.AutoLayoutPanel
{
    /// <summary>
    /// 一个Group对应于一组值(一组ValueControl)
    /// </summary>
    public partial class GroupBoxEx : Control
    {
        /// <summary>
        /// 所属的AutoPanel
        /// </summary>
        public AutoLayoutPanel OwnerAutoPanel { get; private set; }

        private GroupAttsData _itemList;
        private bool _PaintBorder;
        private bool _isGroupBox; ///当前属性是否被包含在GroupBox

        /// <summary>
        /// 本控件所依据的定制特性
        /// </summary>
        private AutoLayoutPanelAttribute _autoAttribute;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="itemList">Group里的Item</param>
        internal protected GroupBoxEx(GroupAttsData data, AutoLayoutPanel ownerAutoPanel)
        {
            this.OwnerAutoPanel = ownerAutoPanel;
            this.TabStop = false;
            this._itemList = data;
            this._autoAttribute = data.AutoAttributeDatas[0].Attribute;
            this._PaintBorder = data.AutoAttributeDatas[0].Attribute.GroupBoxPaintBorder;
            ///这里是根据是否使用groupBox的属性来区别，当前是要创建 groupBox
            this._isGroupBox = data.AutoAttributeDatas[0].Attribute.GroupBoxUseWinStyle;
            if (_isGroupBox)
            {
                _innerGroupBox = new GroupBox();
                _innerGroupBox.TabStop = false;
            }
            this.LayoutOwnControl();
        }

        private GroupBox _innerGroupBox = null;
        /// <summary>
        /// 内部的GroupBox
        /// </summary>
        public GroupBox InnerGroupBox
        {
            get
            {
                if (_innerGroupBox != null)
                {
                    _innerGroupBox.TabStop = false;
                    return _innerGroupBox;
                }
                else
                    return null;

            }
        }

        /// <summary>
        /// OnPaint, 绘制GroupBox的边框线
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_PaintBorder)
            {
                //Graphics g = this.CreateGraphics();
                //Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                //Pen pen = new Pen(AutoLayoutPanelService.ParseColorStringService(_autoAttribute.GroupBoxBorderColor));
                //Brush b = Brushes.Black;
                //g.DrawRectangle(pen, rect);

                ///画边框
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                ControlPaint.DrawBorder(e.Graphics, rect,
                    _autoAttribute.GroupBoxBorderColor,
                    ButtonBorderStyle.Solid);
            }
        }

        /// <summary>
        /// 布局Group内的控件
        /// </summary>
        private void LayoutOwnControl()
        {
            List<KeyValuePair<int, ValueControl>> ctrList = new List<KeyValuePair<int, ValueControl>>();

            //将PropertyInfo属性，属性的定制特性（Attribute）传递给ValueControl，
            //创建好控件后，按照定制属性中的SubControlIndex存储入List
            foreach (AutoAttributeData autoAttData in _itemList.AutoAttributeDatas)
            {
                ValueControl ctr = OwnerAutoPanel.CreateValueControl(autoAttData, this);
                ctrList.Add(new KeyValuePair<int, ValueControl>(autoAttData.Attribute.MainControlIndex, ctr));
            }

            //使用List.Sort方法对List中的Item进行排序
            ctrList.Sort(AutoLayoutPanelCtrPairComparer.CreateComparer());

            int height = 1;
            int width = 0;
            int beginX = 1;

            //如果使用系统自带的GroupBox样式
            //1. 计算宽度与高度，2. 无论用户如何设IsGroupPaintBorder，都不画边框
            if (_autoAttribute.GroupBoxUseWinStyle)
            {
                height = 13;
                width = 2;
                beginX = 2;
                _PaintBorder = false;
            }

            //如果Group有标志图片
            #region
            if (_autoAttribute.GroupBoxMainImage != null)
            {
                width = 6 + 36 + 6;
                beginX = 6 + 36 + 6;
                PictureBox picBox = new PictureBox();
                picBox.Size = new Size(36, 36);
                picBox.Location = new Point(6, 6);
                picBox.Image = Image.FromFile(Path.Combine(Application.StartupPath, _autoAttribute.GroupBoxMainImage));
                this.Controls.Add(picBox);
            }
            #endregion

            foreach (KeyValuePair<int, ValueControl> pair in ctrList)
            {
                ValueControl ctr = pair.Value;
                OwnerAutoPanel.ValueControls.Add(ctr);

                ctr.Location = new Point(beginX, height);
                height = height + ctr.Height;

                //根据是否用GroupBox样式的需求:
                //true使用System.GroupBox再封装一层
                if (_isGroupBox)
                {
                    _innerGroupBox.Controls.Add(ctr);
                }
                else
                {
                    this.Controls.Add(ctr);
                }

                //如果控件的索引是List的第1个时，给width赋值
                if (ctrList.IndexOf(pair) == 0)
                    width = width + ctr.Width;

                //如果当前控件的宽度比width大时
                if (ctr.Width > width)
                    width = ctr.Width;

            }//foreach

            //判断是否使用GroupBox样式，来生成this的大小
            if (_autoAttribute.GroupBoxUseWinStyle && _innerGroupBox != null)
            {
                if (_autoAttribute.ColumnCountOfGroupControl != 1)
                {
                    ReLayOutControl(width);
                    this.Controls.Add(_innerGroupBox);
                    this.Size = new Size(_innerGroupBox.Width + 4, _innerGroupBox.Height );

                }
                else
                {
                    _innerGroupBox.Size = new Size(width + 2, height + 8);
                    this.Controls.Add(_innerGroupBox);
                    this.Size = new Size(width + 4, height + 10);
                }
            }
            else
            {
                this.Size = new Size(width + 2, height + 5);
            }
        }
        /// <summary>
        /// 重新布局GroupBox里的控件,分成几列显示
        /// </summary>
        /// <param name="maxWidth">此组控件中最大控件宽度</param>
        private void ReLayOutControl(int maxWidth)
        {
            ControlCollection coll = _innerGroupBox.Controls;

            int col = _autoAttribute.ColumnCountOfGroupControl;
            int groupWidth = col * maxWidth;
            int row = coll.Count / col + (((coll.Count % col) == 0) ? 0 : 1);


            int _height = 0;
            int _addIndex = 0;
            foreach (Control control in coll)
            {
                control.Location = new Point(2 + ((groupWidth - 2) / col) * (_addIndex % col),15 + control.Height * (_addIndex / col));
                _height = control.Height;
                _addIndex++;
            }
            _innerGroupBox.Size = new Size(groupWidth + 2, (row * _height) + 22);
        }

    }
}
