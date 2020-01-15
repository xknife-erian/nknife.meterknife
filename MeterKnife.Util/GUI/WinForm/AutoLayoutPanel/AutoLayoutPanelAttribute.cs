using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using NKnife.GUI.WinForm.Common;

namespace NKnife.GUI.WinForm.AutoLayoutPanel
{
    [global::System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class AutoLayoutPanelAttribute : Attribute
    {
        /// <summary>
        /// 关于AutoLayoutPanel的定制特性
        /// </summary>
        /// <param name="groupBoxIndex">获取[GroupBox]在Panel子控件集合的索引,以“0”开始,不为负数</param>
        /// <param name="mainControlIndex">获取[主控件]在GroupBox子控件集合的索引,以“0”开始,不为负数</param>
        /// <param name="labelLeft">获取主控件左部的文本(最大5个中文字符,无需加":")</param>
        public AutoLayoutPanelAttribute(int groupBoxIndex, int mainControlIndex, string labelLeft)
        {
            this._GroupBoxIndex = groupBoxIndex;
            this._MainControlIndex = mainControlIndex;
            this._LabelLeft = labelLeft;
        }

        private int _GroupBoxIndex;

        /// <summary>
        /// 获取GroupBox在Panel子控件集合的索引,以“0”开始,不为负数
        /// </summary>
        [Category("! important"), Description("获取GroupBox在Panel子控件集合的索引,以“0”开始,不为负数")]
        public int GroupBoxIndex
        {
            get { return _GroupBoxIndex; }
            set { _GroupBoxIndex = value; }
        }
        private string _GroupBoxMainImage;

        /// <summary>
        /// 获取GroupBox的标志图片, 36*36, 显示在GroupBox内部的左上角
        /// </summary>
        [Category("GroupBox"), Description("获取GroupBox的标志图片, 36*36, 显示在GroupBox内部的左上角")]
        public string GroupBoxMainImage
        {
            get { return _GroupBoxMainImage; }
            set { _GroupBoxMainImage = value; }
        }
        private bool _GroupBoxUseWinStyle;

        /// <summary>
        /// 获取GroupBox使用系统控件的样式
        /// </summary>
        [Category("GroupBox"), Description("获取GroupBox使用系统控件的样式")]
        public bool GroupBoxUseWinStyle
        {
            get { return _GroupBoxUseWinStyle; }
            set { _GroupBoxUseWinStyle = value; }
        }
        private bool _GroupBoxPaintBorder;

        /// <summary>
        /// 获取GroupBox自定义画边框,直角矩形
        /// </summary>
        [Category("GroupBox"), Description("获取GroupBox自定义画边框,直角矩形")]
        public bool GroupBoxPaintBorder
        {
            get { return _GroupBoxPaintBorder; }
            set { _GroupBoxPaintBorder = value; }
        }

        private Color _GroupBoxBorderColor;
        /// <summary>
        /// 获取GroupBox的边框颜色
        /// </summary>
        [Category("GroupBox"), Description("获取GroupBox的边框颜色")]
        public Color GroupBoxBorderColor
        {
            get { return _GroupBoxBorderColor; }
            set { _GroupBoxBorderColor = value; }
        }
        private bool _GroupBoxDockTop;

        /// <summary>
        /// 获取GroupBox是否Dock到Panel,DockType为Top,请设置在Group中的第一个控件属性中
        /// </summary>
        [Category("GroupBox"), Description("获取GroupBox是否Dock到Panel,DockType为Top,请设置在Group中的第一个控件属性中")]
        public bool GroupBoxDockTop
        {
            get { return _GroupBoxDockTop; }
            set { _GroupBoxDockTop = value; }
        }
        private int _ColumnCount = 1;
        /// <summary>
        /// 设置GroupBoxEx内部控件的显示列数(条件是,存在GroupBox控件)
        /// </summary>
        [Category("GroupBox"), Description("设置GroupBoxEx内部控件的显示列数")]
        public int ColumnCountOfGroupControl
        {
            get { return _ColumnCount; }
            set { _ColumnCount = value; }
        }
        private AutoControlType _MainControlType = AutoControlType.TextBox;

        /// <summary>
        /// 获取主控件类型, 默认一般的TextBox
        /// </summary>
        [Category("! important"), Description("获取主控件类型, 默认一般的TextBox")]
        public AutoControlType MainControlType
        {
            get { return _MainControlType; }
            set { _MainControlType = value; }
        }
        private int _MainControlIndex;

        /// <summary>
        /// 获取主控件在GroupBox子控件集合的索引,以“0”开始,不为负数
        /// </summary>
        [Category("! important"), Description("获取主控件在GroupBox子控件集合的索引,以“0”开始,不为负数")]
        public int MainControlIndex
        {
            get { return _MainControlIndex; }
            set { _MainControlIndex = value; }
        }
        private int _MainControlWidth = 120;

        /// <summary>
        /// 获取主控件的宽度, 默认120
        /// </summary>
        [Category("MainControl"), Description("获取主控件的宽度, 默认120")]
        public int MainControlWidth
        {
            get { return _MainControlWidth; }
            set { _MainControlWidth = value; }
        }
        private string _LabelImage;

        /// <summary>
        /// 获取主控件左侧的文本内左侧的图片标志 13*13
        /// </summary>
        [Category("Label"), Description("获取主控件左侧的文本内左侧的图片标志 13*13")]
        public string LabelImage
        {
            get { return _LabelImage; }
            set { _LabelImage = value; }
        }
        private string _LabelTop;

        /// <summary>
        /// 获取主控件顶部的文本(最大5个中文字符,无需加":")
        /// </summary>
        [Category("Label"), Description("获取主控件顶部的文本(最大5个中文字符,无需加\":\")")]
        public string LabelTop
        {
            get { return _LabelTop; }
            set { _LabelTop = value; }
        }
        private string _LabelLeft;

        /// <summary>
        /// 获取主控件左部的文本(最大5个中文字符,无需加":")
        /// </summary>
        [Category("Label"), Description("获取主控件左部的文本(最大5个中文字符,无需加\":\")")]
        public string LabelLeft
        {
            get { return _LabelLeft; }
            set { _LabelLeft = value; }
        }
        private string _LabelRight;

        /// <summary>
        /// 获取主控件右部的文本(最大5个中文字符,无需加":")
        /// </summary>
        [Category("Label"), Description("获取主控件右部的文本(最大5个中文字符,无需加\":\")")]
        public string LabelRight
        {
            get { return _LabelRight; }
            set { _LabelRight = value; }
        }
        private string _LabelFooter;

        /// <summary>
        /// 获取主控件底部的文本(最大5个中文字符,无需加":")
        /// </summary>
        [Category("Label"), Description("获取主控件底部的文本(最大5个中文字符,无需加\":\")")]
        public string LabelFooter
        {
            get { return _LabelFooter; }
            set { _LabelFooter = value; }
        }

        private string _SpaceMark;
        /// <summary>
        /// 类型之间的间隔符
        /// </summary>
        [Category("ComboBoxGroupControl"), Description("设置类型之间的间隔符")]
        public string SpaceMark
        {
            get { return _SpaceMark; }
            set { _SpaceMark = value; }
        }
        private string _MainControlBindingFile;

        /// <summary>
        /// 获取主控件绑定Xml文件
        /// </summary>
        [Category("MainControl"), Description("获取主控件绑定Xml文件")]
        public string MainControlBindingFile
        {
            get { return _MainControlBindingFile; }
            set { _MainControlBindingFile = value; }
        }
        private string _ToolTipText;

        /// <summary>
        /// 获取ValueControl的ToolTip的文本
        /// </summary>
        [Category("ToolTip"), Description("获取ValueControl的ToolTip的文本")]
        public string ToolTipText
        {
            get { return _ToolTipText; }
            set { _ToolTipText = value; }
        }
        private int _ListBoxHeight = 90;

        /// <summary>
        /// 获取主控件为ListBox时该控件的高度
        /// </summary>
        [Category("ListBox"), Description("获取主控件为ListBox时该控件的高度")]
        public int ListBoxHeight
        {
            get { return _ListBoxHeight; }
            set { _ListBoxHeight = value; }
        }
        private bool _SelectGroupMultiModel = false;

        /// <summary>
        /// 获取SelectGroup的选择模式, 为true时是CheckBox, 为false时是RadioButton
        /// </summary>
        [Category("SelectGroup"), Description("获取SelectGroup的选择模式, 为true时是CheckBox, 为false时是RadioButton")]
        public bool SelectGroupMultiModel
        {
            get { return _SelectGroupMultiModel; }
            set { _SelectGroupMultiModel = value; }
        }
        private int _SelectGroupHorizontalCount = 1;

        /// <summary>
        /// 获取SelectGroup的水平控件个数
        /// </summary>
        [Category("SelectGroup"), Description("获取SelectGroup的水平控件个数")]
        public int SelectGroupHorizontalCount
        {
            get { return _SelectGroupHorizontalCount; }
            set { _SelectGroupHorizontalCount = value; }
        }
        private int _SelectGroupLineHeight = 20;

        /// <summary>
        /// 获取SelectGroup的行高
        /// </summary>
        [Category("SelectGroup"), Description("获取SelectGroup的行高")]
        public int SelectGroupLineHeight
        {
            get { return _SelectGroupLineHeight; }
            set { _SelectGroupLineHeight = value; }
        }
        private int _SelectGroupVIndent = 5;

        /// <summary>
        /// 获取SelectGroup的水平缩进距离
        /// </summary>
        [Category("SelectGroup"), Description("获取SelectGroup的水平缩进距离")]
        public int SelectGroupVIndent
        {
            get { return _SelectGroupVIndent; }
            set { _SelectGroupVIndent = value; }
        }
        private int _SelectGroupHIndent = 5;

        /// <summary>
        /// 获取SelectGroup的垂直缩进距离
        /// </summary>
        [Category("SelectGroup"), Description("获取SelectGroup的垂直缩进距离")]
        public int SelectGroupHIndent
        {
            get { return _SelectGroupHIndent; }
            set { _SelectGroupHIndent = value; }
        }
        private int _SelectedItemCount;
        /// <summary>
        /// 多选择情况下,最多选择的个数
        /// </summary>
        [Category("SelectGroup"), Description("获取SelectGroup最大选择个数(针对多个CheckBox)")]
        public int SelectedItemCount 
        {
            get { return _SelectedItemCount; } 
            set { _SelectedItemCount = value; } 
        }
        private string _ValidateTextBoxRegexText;

        /// <summary>
        /// 获取ValidateTextBox的正则String
        /// </summary>
        [Category("ValidateTextBox"), Description("获取ValidateTextBox的正则String")]
        public string ValidateTextBoxRegexText
        {
            get { return _ValidateTextBoxRegexText; }
            set { _ValidateTextBoxRegexText = value; }
        }
        private string _ValidateTextBoxRegexTextRuntime;

        /// <summary>
        /// 获取ValidateTextBox的运行时正则String
        /// </summary>
        [Category("ValidateTextBox"), Description("获取ValidateTextBox的运行时正则String")]
        public string ValidateTextBoxRegexTextRuntime
        {
            get { return _ValidateTextBoxRegexTextRuntime; }
            set { _ValidateTextBoxRegexTextRuntime = value; }
        }
        private string _GroupBoxUseWinStyleText;

        /// <summary>
        /// 获取GroupBox使用系统控件样式时的Text值
        /// </summary>
        [Category("GroupBox"), Description("获取GroupBox使用系统控件样式时的Text值")]
        public string GroupBoxUseWinStyleText
        {
            get { return _GroupBoxUseWinStyleText; }
            set { _GroupBoxUseWinStyleText = value; }
        }
        private long _NumericUpDownMin;

        /// <summary>
        /// 获取NumericUpDown的最小值
        /// </summary>
        [Category("NumericUpDown"), Description("获取NumericUpDown的最小值")]
        public long NumericUpDownMin
        {
            get { return _NumericUpDownMin; }
            set { _NumericUpDownMin = value; }
        }
        private long _NumericUpDownMax;

        /// <summary>
        /// 获取NumericUpDown的最大值
        /// </summary>
        [Category("NumericUpDown"), Description("获取NumericUpDown的最大值")]
        public long NumericUpDownMax
        {
            get { return _NumericUpDownMax; }
            set { _NumericUpDownMax = value; }
        }
        private long _NumericUpDownStep;

        /// <summary>
        /// 获取NumericUpDown的每一步递增值
        /// </summary>
        [Category("NumericUpDown"), Description("获取NumericUpDown的每一步递增值")]
        public long NumericUpDownStep
        {
            get { return _NumericUpDownStep; }
            set { _NumericUpDownStep = value; }
        }

        private int _NumericUpDownDecimalPlaces;

        /// <summary>
        /// 获取NumericUpDown显示的小数个数
        /// </summary>
        [Category("NumericUpDown"), Description("获取NumericUpDown显示的小数个数")]
        public int NumericUpDownDecimalPlaces
        {
            get { return _NumericUpDownDecimalPlaces; }
            set { _NumericUpDownDecimalPlaces = value; }
        }
        private string _LabelHelpText;

        /// <summary>
        /// 获取主控件底部的帮助文本
        /// </summary>
        [Category("Label"), Description("获取主控件底部的帮助文本")]
        public string LabelHelpText
        {
            get { return _LabelHelpText; }
            set { _LabelHelpText = value; }
        }
        private string _MainControlBindingElement;

        /// <summary>
        /// 获取主控件绑定的Element
        /// </summary>
        [Category("MainControl"), Description("获取主控件绑定的Element")]
        public string MainControlBindingElement
        {
            get { return _MainControlBindingElement; }
            set { _MainControlBindingElement = value; }
        }
        private bool _DateTimePickerShowCheckBox;

        /// <summary>
        /// 获取DateTimePicker控件是否显示CheckBox
        /// </summary>
        [Category("DateTimePicker"), Description("获取DateTimePicker控件是否显示CheckBox")]
        public bool DateTimePickerShowCheckBox
        {
            get { return _DateTimePickerShowCheckBox; }
            set { _DateTimePickerShowCheckBox = value; }
        }
        private bool _DateTimePickerShowUpDown;

        /// <summary>
        /// 获取DateTimePicker控件是否显示UpDown按钮(不显示下拉日期)
        /// </summary>
        [Category("DateTimePicker"), Description("获取DateTimePicker控件是否显示UpDown按钮(不显示下拉日期)")]
        public bool DateTimePickerShowUpDown
        {
            get { return _DateTimePickerShowUpDown; }
            set { _DateTimePickerShowUpDown = value; }
        }
        private bool _DateTimePickerChecked;

        /// <summary>
        /// 获取DateTimePicker控件有CheckBox时的CheckBox的状态
        /// </summary>
        [Category("DateTimePicker"), Description("该值指示是否已用有效日期/时间值设置了DateTimePicker.Value 属性且显示的值可以更新")]
        public bool DateTimePickerChecked
        {
            get { return _DateTimePickerChecked; }
            set { _DateTimePickerChecked = value; }
        }
        private string _DateTimePickerCustomFormat;

        /// <summary>
        /// 获取DateTimePicker控件的自定义日期时间格式
        /// </summary>
        [Category("DateTimePicker"), Description("获取DateTimePicker控件的自定义日期时间格式")]
        public string DateTimePickerCustomFormat
        {
            get { return _DateTimePickerCustomFormat; }
            set { _DateTimePickerCustomFormat = value; }
        }
        private string _DateTimePickerMax;

        /// <summary>
        /// 获取DateTimePicker控件的最大日期
        /// </summary>
        [Category("DateTimePicker"), Description("获取DateTimePicker控件的最大日期")]
        public string DateTimePickerMax
        {
            get { return _DateTimePickerMax; }
            set { _DateTimePickerMax = value; }
        }
        private string _DateTimePickerMin;

        /// <summary>
        /// 获取DateTimePicker控件的最小日期
        /// </summary>
        [Category("DateTimePicker"), Description("获取DateTimePicker控件的最小日期")]
        public string DateTimePickerMin
        {
            get { return _DateTimePickerMin; }
            set { _DateTimePickerMin = value; }
        }

        private int _LabelHelpWidth = -1;

        /// <summary>
        /// 获取主控件底部的帮助文本的最大宽度
        /// </summary>
        [Category("Label"), Description("获取主控件底部的帮助文本的最大宽度")]
        public int LabelHelpWidth
        {
            get { return _LabelHelpWidth; }
            set { _LabelHelpWidth = value; }
        }
        private int _LabelHelpLeftIndent;

        /// <summary>
        /// 获取主控件底部的帮助文本的左缩进
        /// </summary>
        [Category("Label"), Description("获取主控件底部的帮助文本的左缩进")]
        public int LabelHelpLeftIndent
        {
            get { return _LabelHelpLeftIndent; }
            set { _LabelHelpLeftIndent = value; }
        }

        private FileSelectControlStyle _FileSelecterControlStyle = FileSelectControlStyle.TextBoxAndImageButton;

        /// <summary>
        /// 获取FileSelecterControl控件的样式, 枚举
        /// </summary>
        [Category("FileSelecterControl"), Description("获取FileSelecterControl控件的样式, 枚举")]
        public FileSelectControlStyle FileSelecterControlStyle
        {
            get { return _FileSelecterControlStyle; }
            set { _FileSelecterControlStyle = value; }
        }

        /// <summary>
        /// 获取FileSelecterControl控件的过滤字符串(class FileSelectFilter中定义了强类型的只读变量)
        /// </summary>
        [Category("FileSelecterControl"), Description("获取FileSelecterControl控件的过滤字符串(class FileSelectFilter中定义了强类型的只读变量)")]
        public string FileSelecterControlFilter
        {
            get { return _FileSelecterControlFilter; }
            set { _FileSelecterControlFilter = value; }
        }
        private bool _FileSelecterControlMultiSelect;

        /// <summary>
        /// 获取FileSelecterControl控件是否可以多选
        /// </summary>
        [Category("FileSelecterControl"), Description("获取FileSelecterControl控件是否可以多选")]
        public bool FileSelecterControlMultiSelect
        {
            get { return _FileSelecterControlMultiSelect; }
            set { _FileSelecterControlMultiSelect = value; }
        }
        private string _FileSelecterControlInitialDirectory;

        /// <summary>
        /// 获取FileSelecterControl控件的初始目录
        /// </summary>
        [Category("FileSelecterControl"), Description("获取FileSelecterControl控件的初始目录")]
        public string FileSelecterControlInitialDirectory
        {
            get { return _FileSelecterControlInitialDirectory; }
            set { _FileSelecterControlInitialDirectory = value; }
        }
        private string _FileSelecterControlDialogTitle;

        /// <summary>
        /// 获取FileSelecterControl控件的标题
        /// </summary>
        [Category("FileSelecterControl"), Description("获取FileSelecterControl控件的标题")]
        public string FileSelecterControlDialogTitle
        {
            get { return _FileSelecterControlDialogTitle; }
            set { _FileSelecterControlDialogTitle = value; }
        }
        private bool _SelectFolder;
        /// <summary>
        /// 获取FileSelecterControl控件的标题
        /// </summary>
        [Category("FileSelecterControl"), Description("获取对话框选择方式")]
        public bool SelectFolder 
        {
            get { return _SelectFolder; }
            set { _SelectFolder = value; }
        }

        private bool _TextBoxMultiLine;
        /// <summary>
        /// 获取TextBox是否为多行
        /// </summary>
        [Category("TextBox"), Description("获取TextBox是否为多行")]
        public bool TextBoxMultiLine
        {
            get { return _TextBoxMultiLine; }
            set { _TextBoxMultiLine = value; }
        }

        private int _TextBoxHeight = 120;
        /// <summary>
        /// 获取当TextBox为多行时TextBox控件的高度
        /// </summary>
        [Category("TextBox"), Description("获取当TextBox为多行时TextBox控件的高度")]
        public int TextBoxHeight
        {
            get { return _TextBoxHeight; }
            set { _TextBoxHeight = value; }
        }

        private ScrollBars _TextBoxScrollBars = ScrollBars.Vertical;
        /// <summary>
        /// 获取当TextBox为多行时TextBox控件的滚动条
        /// </summary>
        [Category("TextBox"), Description("获取当TextBox为多行时TextBox控件的高度")]
        public ScrollBars TextBoxScrollBars
        {
            get { return _TextBoxScrollBars; }
            set { _TextBoxScrollBars = value; }
        }

        /// <summary>
        /// 获取SimpleCheckBox控件的文字
        /// </summary>
        [Category("SimpleCheckBox"), Description("获取SimpleCheckBox控件的文字")]
        public string SimpleCheckBoxText { get; set; }
        #region
        /// <summary>
        /// 获取CheckBoxEx控件的文字
        /// </summary>
        [Category("CheckBoxExText"), Description("设置CheckBoxEx控件的文字")]
        public string CheckBoxExText { get; set; }
        /// <summary>
        /// 获取CheckBoxEx控件的文字
        /// </summary>
        [Category("CheckBoxExLabel"), Description("设置CheckBoxEx控件左侧显示的文字")]
        public string CheckBoxExLabelText { get; set; }
        #endregion
        /// <summary>
        /// 获取SimpleRadioButton控件的项数与每项的文字
        /// </summary>
        [Category("SimpleRadioButton"), Description("获取SimpleRadioButton控件的项数与每项的文字，每项的文字用“,“分隔，只可")]
        public string SimpleRadioButtonText { get; set; }

        [Category("ComboBoxStyle"), Description("获取ComboBox控件组合框的外观和功能")]
        public ComboBoxStyle ComboBoxStyle { get; set; }
        //[Category("ResourceFileName"), Description("获取Combox连级的文件名")]
        //public string ResourceFileName { get; set; }
        [Category("ButtonText"), Description("设置Button上的文本")]
        public string ButtonText { get; set; }
        [Category("PageName"), Description("设置PageName")]
        public string PageName { get; set; }
        [Category("IsBigImage"), Description("获取图片是否为大图")]
        public bool IsBigImage { get; set; }
        [Category("IsReadOnly"), Description("此字段是否只读")]
        public bool IsReadOnly { get; set; }
        [Category("IsRed"), Description("获取提示语言的颜色是否为红色")]
        public bool IsRed { get; set; }
        [Category("TextMaxLength"), Description("获取控件文本输入的最大长度")]
        public int TextMaxLength { get; set; }
        [Category("IsCanFind"), Description("获取该属性是否需要被查找")]
        public bool IsCanFind { get; set; }
#if DEBUG

        // 以下两个方法供测试使用，Release时全部删除

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[PropertyPad(").Append(_GroupBoxIndex).Append(", ").
               Append(_MainControlIndex).Append(", \"").Append(_LabelLeft).Append("\",\r\n");

            string str;

            if (_GroupBoxIndex != 0)
            {
                //str = "GroupBoxIndex = " + GroupBoxIndex.ToString() + ",\r\n";
                //sb.Append(str);
            }
            if (!string.IsNullOrEmpty(_GroupBoxMainImage))
            {
                str = "GroupBoxMainImage = \"" + GroupBoxMainImage + "\" ,\r\n";
                sb.Append(str);
            }
            if (_GroupBoxUseWinStyle != false)
            {
                str = "GroupBoxUseWinStyle = " + GroupBoxUseWinStyle.ToString().ToLower() + ",\r\n";
                sb.Append(str);
            }
            if (_GroupBoxPaintBorder != false)
            {
                str = "GroupBoxPaintBorder = " + GroupBoxPaintBorder.ToString().ToLower() + ",\r\n";
                sb.Append(str);
            }
            if (!string.IsNullOrEmpty(_GroupBoxBorderColor.ToString()))
            {
                str = "GroupBoxBorderColor = \"" + GroupBoxBorderColor + "\" ,\r\n";
                sb.Append(str);
            }
            if (_GroupBoxDockTop != false)
            {
                str = "GroupBoxDockTop = " + GroupBoxDockTop.ToString().ToLower() + ",\r\n";
                sb.Append(str);
            }
            if (_MainControlType != AutoControlType.TextBox)
            {
                str = "MainControlType = MainControlType." + MainControlType + ",\r\n";
                sb.Append(str);
            }
            if (_MainControlIndex != 0)
            {
                //str = "MainControlIndex = " + MainControlIndex + ",\r\n";
                //sb.Append(str);
            }
            if (_MainControlWidth != 120)
            {
                str = "MainControlWidth = " + MainControlWidth + ",\r\n";
                sb.Append(str);
            }
            if (!string.IsNullOrEmpty(_LabelImage))
            {
                str = "LabelImage = \"" + LabelImage + "\" ,\r\n";
                sb.Append(str);
            }
            if (!string.IsNullOrEmpty(_LabelTop))
            {
                str = "LabelTop = \"" + LabelTop + "\" ,\r\n";
                sb.Append(str);
            }
            if (!string.IsNullOrEmpty(_LabelLeft))
            {
                //str = "LabelLeft = \"" + LabelLeft + "\" ,\r\n";
                //sb.Append(str);
            }
            if (!string.IsNullOrEmpty(_LabelRight))
            {
                str = "LabelRight = \"" + LabelRight + "\" ,\r\n";
                sb.Append(str);
            }
            if (!string.IsNullOrEmpty(_LabelFooter))
            {
                str = "LabelFooter = \"" + LabelFooter + "\" ,\r\n";
                sb.Append(str);
            }
            if (!string.IsNullOrEmpty(_MainControlBindingFile))
            {
                str = "MainControlBindingFile = \"" + MainControlBindingFile + "\" ,\r\n";
                sb.Append(str);
            }
            if (!string.IsNullOrEmpty(_ToolTipText))
            {
                str = "ToolTipText = \"" + ToolTipText + "\" ,\r\n";
                sb.Append(str);
            }
            if (_ListBoxHeight != 90)
            {
                str = "ListBoxHeight = " + ListBoxHeight + ",\r\n";
                sb.Append(str);
            }
            if (_SelectGroupMultiModel != false)
            {
                str = "SelectGroupMultiModel = " + SelectGroupMultiModel.ToString().ToLower() + ",\r\n";
                sb.Append(str);
            }
            if (_SelectGroupHorizontalCount != 1)
            {
                str = "SelectGroupHorizontalCount = " + SelectGroupHorizontalCount + ",\r\n";
                sb.Append(str);
            }
            if (_SelectGroupLineHeight != 20)
            {
                str = "SelectGroupLineHeight = " + SelectGroupLineHeight + ",\r\n";
                sb.Append(str);
            }
            if (_SelectGroupVIndent != 5)
            {
                str = "SelectGroupVIndent = " + SelectGroupVIndent + ",\r\n";
                sb.Append(str);
            }
            if (_SelectGroupHIndent != 5)
            {
                str = "SelectGroupHIndent = " + SelectGroupHIndent + ",\r\n";
                sb.Append(str);
            }
            if (!string.IsNullOrEmpty(_ValidateTextBoxRegexText))
            {
                str = "ValidateTextBoxRegexText = \"" + ValidateTextBoxRegexText + "\" ,\r\n";
                sb.Append(str);
            }
            if (!string.IsNullOrEmpty(_ValidateTextBoxRegexTextRuntime))
            {
                str = "ValidateTextBoxRegexTextRuntime = \"" + ValidateTextBoxRegexTextRuntime + "\" ,\r\n";
                sb.Append(str);
            }
            if (!string.IsNullOrEmpty(_GroupBoxUseWinStyleText))
            {
                str = "GroupBoxUseWinStyleText = \"" + GroupBoxUseWinStyleText + "\" ,\r\n";
                sb.Append(str);
            }
            if (_NumericUpDownMin != 0)
            {
                str = "NumericUpDownMin = " + NumericUpDownMin + ",\r\n";
                sb.Append(str);
            }
            if (_NumericUpDownMax != 0)
            {
                str = "NumericUpDownMax = " + NumericUpDownMax + ",\r\n";
                sb.Append(str);
            }
            if (_NumericUpDownStep != 0)
            {
                str = "NumericUpDownStep = " + NumericUpDownStep + ",\r\n";
                sb.Append(str);
            }
            if (_NumericUpDownDecimalPlaces != 0)
            {
                str = "NumericUpDownDecimalPlaces = " + NumericUpDownDecimalPlaces + ",\r\n";
                sb.Append(str);
            }
            if (!string.IsNullOrEmpty(_LabelHelpText))
            {
                str = "LabelHelpText = \"" + LabelHelpText + "\" ,\r\n";
                sb.Append(str);
            }
            if (!string.IsNullOrEmpty(_MainControlBindingElement))
            {
                str = "MainControlBindingElement = \"" + MainControlBindingElement + "\" ,\r\n";
                sb.Append(str);
            }
            if (_DateTimePickerShowCheckBox != false)
            {
                str = "DateTimePickerShowCheckBox = " + DateTimePickerShowCheckBox.ToString().ToLower() + ",\r\n";
                sb.Append(str);
            }
            if (_DateTimePickerShowUpDown != false)
            {
                str = "DateTimePickerShowUpDown = " + DateTimePickerShowUpDown.ToString().ToLower() + ",\r\n";
                sb.Append(str);
            }
            if (_DateTimePickerChecked != false)
            {
                str = "DateTimePickerChecked = " + DateTimePickerChecked.ToString().ToLower() + ",\r\n";
                sb.Append(str);
            }
            if (!string.IsNullOrEmpty(_DateTimePickerCustomFormat))
            {
                str = "DateTimePickerCustomFormat = \"" + DateTimePickerCustomFormat + "\" ,\r\n";
                sb.Append(str);
            }
            if (!string.IsNullOrEmpty(_DateTimePickerMax))
            {
                str = "DateTimePickerMax = \"" + DateTimePickerMax + "\" ,\r\n";
                sb.Append(str);
            }
            if (!string.IsNullOrEmpty(_DateTimePickerMin))
            {
                str = "DateTimePickerMin = \"" + DateTimePickerMin + "\" ,\r\n";
                sb.Append(str);
            }
            if (_LabelHelpWidth != 120)
            {
                str = "LabelHelpWidth = " + LabelHelpWidth + ",\r\n";
                sb.Append(str);
            }
            if (_LabelHelpLeftIndent != 0)
            {
                str = "LabelHelpLeftIndent = " + LabelHelpLeftIndent + ",\r\n";
                sb.Append(str);
            }
            //if (_FileSelecterControlStyle != FileSelectControlStyle.TextBoxAndImageButton)
            //{
            //    str = "FileSelecterControlStyle = FileSelectControlStyle." + FileSelecterControlStyle + " ,\r\n";
            //    sb.Append(str);
            //}
            //if (_FileSelecterControlFilter != FileSelectFilter.All)
            //{
            //    str = "FileSelecterControlFilter = FileSelectFilter." + FileSelecterControlFilter + " ,\r\n";
            //    sb.Append(str);
            //}
            if (_FileSelecterControlMultiSelect != false)
            {
                str = "FileSelecterControlMultiSelect = " + FileSelecterControlMultiSelect.ToString().ToLower() + ",\r\n";
                sb.Append(str);
            }
            if (!string.IsNullOrEmpty(_FileSelecterControlInitialDirectory))
            {
                str = "FileSelecterControlInitialDirectory = @\"" + FileSelecterControlInitialDirectory + "\" ,\r\n";
                sb.Append(str);
            }
            if (!string.IsNullOrEmpty(_FileSelecterControlDialogTitle))
            {
                str = "FileSelecterControlDialogTitle = \"" + FileSelecterControlDialogTitle + "\" ,\r\n";
                sb.Append(str);
            }
            if (TextBoxMultiLine != false)
            {
                str = "TextBoxMultiLine = \"" + TextBoxMultiLine.ToString().ToLower() + "\" ,\r\n";
                sb.Append(str);
            }
            if (TextBoxHeight != 120)
            {
                str = "TextBoxHeight = \"" + TextBoxHeight + "\" ,\r\n";
                sb.Append(str);
            }
            if (TextBoxScrollBars != ScrollBars.Vertical)
            {
                str = "TextBoxScrollBars = \"" + TextBoxScrollBars + "\" ,\r\n";
                sb.Append(str);
            }
            if (!string.IsNullOrEmpty(SimpleCheckBoxText))
            {
                str = "SimpleCheckBoxText = \"" + SimpleCheckBoxText + "\" ,\r\n";
                sb.Append(str);
            }
            if (!string.IsNullOrEmpty(SimpleRadioButtonText))
            {
                str = "SimpleRadioButtonText = \"" + SimpleRadioButtonText + "\" ,\r\n";
                sb.Append(str);
            }

            string returnstring = sb.ToString();
            returnstring = returnstring.Remove(returnstring.LastIndexOf(','), 1);
            returnstring += ")]";

            return returnstring;
        }

        /// <summary>
        /// 生代上方法代码的代码，要删除掉
        /// </summary>
        public string BuildCode()
        {
            PropertyInfo[] infos = this.GetType().GetProperties();
            StringBuilder sb = new StringBuilder();
            foreach (PropertyInfo info in infos)
            {
                sb.Append("if (_").Append(info.Name).Append(" != string.Empty) {");
                sb.Append("    str = \"").Append(info.Name).Append(" = \" + ").Append(info.Name).Append(" + \",\\r\\n\";\r\n");
                sb.Append("sb.Append(str);\r\n");
                sb.Append("}\r\n");
            }
            return sb.ToString();
        }
#endif

        public string _FileSelecterControlFilter { get; set; }
    }
}
