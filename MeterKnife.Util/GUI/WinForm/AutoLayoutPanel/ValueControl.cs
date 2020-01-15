using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace NKnife.GUI.WinForm.AutoLayoutPanel
{
    public partial class ValueControl : Control
    {
        #region 静态变量定义

        static int txtWidth = 80; //将70改成80,其是设置标签文本显示的个数为6
        static int txtHeight = 13;
        static int inpHeignt = 21;

        static int offsetX = 2;
        static int offsetY = 2;
        static int offsetLabelX = -2;
        static int offsetLabelY = 4;

        static int x1 = offsetX + 5; //是用来调整控件的X坐标
        static int x2 = x1 + txtWidth + offsetX;
        static int y1 = offsetY;
        static int y2 = offsetY + txtHeight + offsetY;

        #endregion

        #region 类成员变量定义

        protected Size _txtSize = new Size(txtWidth, txtHeight);
        public Control _mainControl;
        public Control MainControl
        {
            get { return _mainControl; }
            set { _mainControl = value; }
        }

        /// <summary>
        /// 本控件所对应的属性(定制特性就定义在此属性上)
        /// </summary>
        public MemberInfo MemberInfo { get; protected set; }
        /// <summary>
        /// 本控件所依据的定制特性
        /// </summary>
        public AutoLayoutPanelAttribute AutoAttribute { get; protected set; }
        protected object[] _objects;
        protected object _historyValue;
        /// <summary>
        /// 当前控件的
        /// </summary>
        //得到绘制的当前控件的控件类型 by lisuye on 2008年5月28日
        public AutoControlType MainControlType { get { return AutoAttribute.MainControlType; } }

        /// <summary>
        /// 父GroupBoxEx控件
        /// </summary>
        public GroupBoxEx ParentGroup { get; private set; }

        /// <summary>
        /// 所属的AutoPanel
        /// </summary>
        public AutoLayoutPanel OwnerAutoPanel { get { return ParentGroup.OwnerAutoPanel; } }
        /// <summary>
        /// 是否值已修改。只有在RealTimeSave为False时才有意义
        /// </summary>
        public bool IsModified { get; protected set; }

        private bool _different = false;
        /// <summary>
        /// 是否是不一样的值
        /// </summary>
        public bool Different
        {
            get
            {
                return _different;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        protected internal ValueControl(AutoAttributeData data, GroupBoxEx parentGroup)
        {

            this.ParentGroup = parentGroup;
            this.TabStop = false;
            this.MemberInfo = data.Property;
            this.AutoAttribute = data.Attribute;

            /// 控件排版(主要是针对Label和Image标志)
            this.LayoutOwnControl();

            /// 定义控件实时保存的事件
            this.EventDefine(AutoAttribute.MainControlType);
        } 

        #endregion

        #region 控件的值相关

        /// <summary>
        /// 定义控件实时保存的事件
        /// </summary>
        public virtual void EventDefine(AutoControlType type)
        {
            ///更改值后的事件处理
            EventHandler eventHandler = new EventHandler(
                delegate
                {
                    //如果需要实时保存对其进行保存 by lisuye on 2008年5月28日
                    if (OwnerAutoPanel.RealTimeSave)
                    {
                        if (AutoAttribute.IsReadOnly ||
                            (MemberInfo.MemberType == MemberTypes.Property && !((PropertyInfo)MemberInfo).CanWrite))
                        {
                        }
                        else
                        {
                            SetValueFromControl();
                        }
                    }
                      //如果不需要实时保存则让IsModified为true，用来控制保存的按钮 by lisuye on 2008年5月28日
                    else
                    {
                        IsModified = true;         
                    }
                });
            //根据控件的类型，分别给不同类型的控件绑定事件 by lisuye on 2008年5月28日
            switch (type)
            {
                case AutoControlType.SimpleCheckBox:
                    {
                        ((SimpleCheckBox)this._mainControl).CheckedChanged += eventHandler;
                        break;
                    }
                case AutoControlType.TextBox:
                case AutoControlType.NumericUpDown:
                    {
                        this._mainControl.TextChanged += eventHandler;
                        break;
                    }
                case AutoControlType.SelectGroup:
                    {
                        //((SelectGroup)this._mainControl).SelectedValueChanged += eventHandler;
                        break;
                    }
                case AutoControlType.ListBox:
                case AutoControlType.ComboBox:
                    {
                        ((ListControl)this._mainControl).SelectedValueChanged += eventHandler;
                        break;
                    }
                case AutoControlType.RegexTextBox:
                    {
                        ((RegexTextBox)this._mainControl).Changed += eventHandler;
                        break;
                    }
                case AutoControlType.Button:
                    break;
                //case AutoControlType.ColorGeneralButton:
                //    {
                //        ((ColorGeneralButton)this._mainControl).MyColorChanged += eventHandler;
                //        break;
                //    }
                //case AutoControlType.FileSelecterControl:
                //    {
                //        ((FileSelecterControl)this._mainControl).FileSelected += eventHandler;
                //        break;
                //    }
                //case AutoControlType.ComboBoxGroupControl:
                //    {
                //        ((ComboBoxGroupControl)this._mainControl).SelectedValueChanged += eventHandler;
                //        break;
                //    }
                //case AutoControlType.DepartmentControl:
                //    {
                //        ((DepartmentControl)this._mainControl).ValueChanged += eventHandler;
                //        break;
                //    }
                //case AutoControlType.CheckBoxExControl:
                //    {
                //        ((CheckBoxExControl)this._mainControl).CheckChanged += eventHandler;
                //        break;
                //    }
                case AutoControlType.DateTimePicker:
                    {
                        ((DateTimePicker)this._mainControl).ValueChanged += eventHandler;
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// 为控件取值并填充
        /// 如果是多个对象，但是多个对象的值不一样的话，将返回空值
        /// </summary>
        //根据反射得到控件对应的值，并填充值 by lisuye on 2008年5月28日
        public virtual void FillValue(object[] objs)
        {
            this._objects = objs;
            _different = false;

            if (this.MemberInfo.MemberType != MemberTypes.Property)
            {
                return;
            }
            PropertyInfo propertyInfo = (PropertyInfo)MemberInfo;

            object valueFirst = propertyInfo.GetValue(objs[0], null);

            //比较多个值是否相等
            if (objs.Length > 1)
            {
                foreach (object obj in objs)
                {
                    object valueEvery = propertyInfo.GetValue(obj, null);

                    ///不一样则将valueFirstzhiwei置为空,即不显示
                    if (!object.Equals(valueFirst, valueEvery))
                    {
                        _different = true;
                        valueFirst = null;
                        break;
                    }
                }
            }

            if (Different)
            {
                //lukan,2008年2月26日11时45分,当多个对象的值不一样时返回Null值时未处理用户UI
                this._mainControl.BackColor = SystemColors.Info;
                this._historyValue = null;
                this.Value = null;
            }
            else
            {
                if (this._mainControl is TextBox
                    || this._mainControl is ComboBox
                    || this._mainControl is ListBox
                    || this._mainControl is NumericUpDown)
                {
                    this._mainControl.BackColor = SystemColors.Window;
                }
                else
                {
                    this._mainControl.BackColor = SystemColors.Control;
                }
                this.Value = valueFirst;
                this._historyValue = valueFirst;
                
            }
        }

        public virtual void FillValue()
        {
            if (this.MemberInfo.MemberType != MemberTypes.Property)
            {
                return;
            }
            PropertyInfo propertyInfo = (PropertyInfo)MemberInfo;

            object obj;
            obj = propertyInfo.GetValue(null, null);
            
            this.Value = obj;
            this._historyValue = obj;
        }
        //保存值 并设置保存的状态 by lisuye on 2008年5月28日
        public virtual void Save()
        {
            SetValueFromControl();

            IsModified = false;
        }

        /// <summary>
        /// 获取或设置控件的值，GroupBoxEx定义, lukan
        /// </summary>
       //从控件上获取或设置对应的值 by lisuye on 2008年5月28日
        public virtual object Value
        {
            get
            {
                switch (MainControlType)
                {
                    case AutoControlType.NumericUpDown:
                        return ((NumericUpDown)_mainControl).Value.ToString();
                    //case AutoControlType.ValidateTextBox:
                    case AutoControlType.TextBox:
                        return _mainControl.Text;
                    case AutoControlType.DateTimePicker:
                        return ((DateTimePicker)_mainControl).Value;
                    case AutoControlType.ListBox:
                    case AutoControlType.ComboBox:
                        return ((ListControl)_mainControl).SelectedValue;
                    case AutoControlType.ColorGeneralButton:
                        //return ((ColorGeneralButton)_mainControl).MyColor;
                    case AutoControlType.FileSelecterControl:
                        break;
                    //case AutoControlType.SelectGroup:
                    //    if (AutoAttribute.SelectGroupMultiModel)
                    //    {
                    //        string[] values = ((SelectGroup)_mainControl).SelectedStringValues;
                    //        StringBuilder sb = new StringBuilder();
                    //        foreach (string value in values)
                    //        {
                    //            sb.Append(value).Append(":|:");
                    //        }
                    //        return sb.ToString();
                    //    }
                    //    else
                    //        return ((SelectGroup)_mainControl).SelectedValue;
                    //case AutoControlType.SimpleCheckBox:
                    //    return ((SimpleCheckBox)_mainControl).Value;
                    //case AutoControlType.DepartmentControl:
                    //    return ((DepartmentControl)_mainControl).Value;
                    //case AutoControlType.ColorSelectorButton:
                    //    return ((ColorSelectorButton)_mainControl).Value;
                    //case AutoControlType.FontComboBox:
                    //    return ((FontComboBox)_mainControl).SelectedValue;
                    //case AutoControlType.ComboBoxGroupControl:
                    //    return ((ComboBoxGroupControl)_mainControl).SelectedValues;
                    //case AutoControlType.CheckBoxExControl:
                    //    return ((CheckBoxExControl)_mainControl).Value;
                    default:
                        break;
                }
                return _mainControl.Text;
            }
            set
            {
                switch (MainControlType)
                {
                    case AutoControlType.NumericUpDown:
                    case AutoControlType.RegexTextBox:
                    case AutoControlType.TextBox:
                        _mainControl.Text = Convert.ToString(value);
                        break;
                    case AutoControlType.DateTimePicker:
                        {
                            DateTimePicker picker = (DateTimePicker)_mainControl;
                            picker.Value = (DateTime)value;
                            break;
                        }
                    case AutoControlType.ListBox:
                    case AutoControlType.ComboBox:
                        #region
                    
                        //根据不同的form种类设置combox的初始值 by lisuye on 2008年5月28日
                        ListControl tempListControl = ((ListControl)_mainControl);
                        tempListControl.SelectedValue = value;
                        Form form = tempListControl.FindForm();
                        // 在formload的时候对空间进行的赋值 by lisuye on2008年5月28日
                        if (form != null)
                        {
                            form.Load += delegate
                            {
                                if (string.IsNullOrEmpty(Convert.ToString(value)))
                                {
                                    tempListControl.SelectedIndex = 0;
                                }
                                else
                                {
                                    tempListControl.SelectedValue = value;
                                }
                            };
                        }
                        else
                        {
                            // 在formlayout的时候对空间进行的赋值 by lisuye on2008年5月28日
                            LayoutEventHandler eventHandler = delegate
                            {
                                if (string.IsNullOrEmpty(Convert.ToString(value)))
                                {
                                    if (tempListControl.SelectedIndex == -1)
                                        tempListControl.SelectedIndex = -1;
                                    else
                                        tempListControl.SelectedIndex = 0;
                                }
                                else
                                {
                                    tempListControl.SelectedValue = value;
                                }
                                return;
                            };
                            // 在不存在事件发生的情况下的时候对空间进行的赋值 by lisuye on2008年5月28日
                            EventHandler eventHandler2 = delegate
                            {
                                if (string.IsNullOrEmpty(Convert.ToString(value)))
                                {
                                    tempListControl.SelectedIndex = 0;
                                }
                                else
                                {
                                    tempListControl.SelectedValue = value;
                                }
                            };
                            this.Layout -= eventHandler;
                            this.Layout += eventHandler;
                            tempListControl.DataSourceChanged -= eventHandler2;
                            tempListControl.DataSourceChanged += eventHandler2;
                        }
                        
                        #endregion
                        break;
                    case AutoControlType.Button:
                        break;
                    case AutoControlType.ColorGeneralButton:
                        {
                            string str = ColorTranslator.ToHtml((Color)value);
                            _mainControl.Text = str;
                            break;
                        }
                    case AutoControlType.FileSelecterControl:
                        {
                            this._mainControl.Text = ReplacePath((string)value);
                            break;
                        }
                    //case AutoControlType.SelectGroup:
                    //    {
                            //if (AutoAttribute.SelectGroupMultiModel)
                            //{
                            //    //string str = value.ToString();
                            //    //string[] strArr = (str == null ? new string[0] : str.Split(new[] { ":|:" }, StringSplitOptions.RemoveEmptyEntries));
                            //    string[] strArr = (string[])value;
                            //    ((SelectGroup)_mainControl).SelectedStringValues = strArr;
                            //}
                            //else
                            //{
                            //    ((SelectGroup)_mainControl).SelectedValue = value;

                            //}
                            //break;
                        //}
                    case AutoControlType.SimpleCheckBox:
                        if (value == null)
                        {
                            ((SimpleCheckBox)_mainControl).Value = false;
                        }
                        else
                        {
                            ((SimpleCheckBox)_mainControl).Value = (bool)value;
                        }
                        break;
                    //case AutoControlType.DepartmentControl:
                    //    ((DepartmentControl)_mainControl).Value = (DepartmentData[])value;
                    //    break;
                    //case AutoControlType.ColorSelectorButton:
                    //    ((ColorSelectorButton)this._mainControl).Value = (Color)value;
                    //    break;
                    //case AutoControlType.FontComboBox:
                    //    ((FontComboBox)_mainControl).SelectedValue = value;
                    //    break;
                    //case AutoControlType.ComboBoxGroupControl:
                    //    ((ComboBoxGroupControl)_mainControl).SelectedValues = (string[])value;
                    //    break;
                    //case AutoControlType.CheckBoxExControl:
                    //    if (value == null)
                    //    {
                    //        ((CheckBoxExControl)_mainControl).Value = false;
                    //    }
                    //    else
                    //    {
                    //        ((CheckBoxExControl)_mainControl).Value = (bool)value;
                    //    }
                    //    break;
                    default:
                        break;
                }
                _historyValue = value;
            }
        }

        /// <summary>
        /// 替换SimplusD网站路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        //为文件上传行的控件更换路径 by lisuye on 2008年5月28日
        private string ReplacePath(string path)
        {
            string text = "%:mydocument%";
            if (path.StartsWith(text))
            {
                string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                path = path.Replace(text, documentPath);
            }
            return path;
        }

        /// <summary>
        /// 根据字符串来判断Form本体的Size属性值，以及在合适位置插入了换行符的String 
        /// </summary>
        static internal KeyValuePair<string, Size> GetSize(string str, int MaxWidth, Graphics g, Font font)
        {
            KeyValuePair<string, Size> k;
            SizeF strSize = g.MeasureString(str, font);
            StringBuilder sb = new StringBuilder();
            int lineNum = 0;

            char[] c = str.ToCharArray();
            List<string> list = new List<string>();
            foreach (char myChar in c)
            {
                sb.Append(myChar);
                if (g.MeasureString(sb.ToString(), font).Width >= MaxWidth)
                {
                    sb.Append("\r\n");
                    list.Add(sb.ToString());
                    sb = new StringBuilder();
                    lineNum++;
                }
            }
            list.Add(sb.ToString());
            sb = new StringBuilder();
            foreach (string txt in list)
            {
                sb.Append(txt);///生成在合适位置插入了换行符的String
            }

            int w;
            int h;
            if (strSize.Width > MaxWidth)
            {
                w = MaxWidth;
                h = (int)strSize.Height * (lineNum + 1);
            }
            else
            {
                w = (int)strSize.Width;
                h = (int)strSize.Height;
            }
            Size mySize = new Size(w + 10, h);
            k = new KeyValuePair<string, Size>(sb.ToString(), mySize);
            return k;
        }

        /// <summary>
        /// 获取控件的值是否发生改变
        /// </summary>
        //和历史值进行比较看是否控件的值发生了改变 by lisuye on2008年5月28日
        private string NowControlValue { get;set; }
        public virtual bool IsModifiedFromValue
        {
            get
            {
                if (_historyValue == null)
                {
                    if (!(this._mainControl is SimpleCheckBox))
                    {
                        return this._mainControl.Text != string.Empty;
                    }
                    else
                    {
                        return false;
                    }
                }
                switch (MainControlType)
                {
                    case AutoControlType.NumericUpDown:
                    case AutoControlType.TextBox:
                    //case AutoControlType.ValidateTextBox:
                    //    NowControlValue = this._mainControl.Text;
                    //    return NowControlValue != _historyValue.ToString();
                    case AutoControlType.DateTimePicker:
                        break;
                    case AutoControlType.ListBox:
                        break;
                    case AutoControlType.ComboBox:
                        if (((ComboBox)_mainControl).SelectedValue != null)
                        {
                            NowControlValue = ((ComboBox)_mainControl).SelectedValue.ToString();
                        }
                        return NowControlValue != _historyValue.ToString();
                    case AutoControlType.ColorGeneralButton:
                        break;
                    case AutoControlType.FileSelecterControl:
                        break;
                    case AutoControlType.SelectGroup:
                        break;
                    // return ((SelectGroup)_mainControl).SelectedStringValues != (string[])_historyValue;
                    case AutoControlType.SimpleCheckBox:
                        return ((SimpleCheckBox)_mainControl).Value != (bool)_historyValue;
                    case AutoControlType.DepartmentControl:
                        {
                            //DepartmentData[] historyVal =  (DepartmentData[])_historyValue;
                            //return !Utility.IsAllEquals(historyVal, ((DepartmentControl)_mainControl).Value);
                            break;
                        }
                    //case AutoControlType.ComboBoxGroupControl:
                    //    {
                    //        object[] objs = ((ComboBoxGroupControl)_mainControl).SelectedValues;
                    //        return !Utility.IsAllEquals(objs, (object[])_historyValue);
                    //    }
                    //case AutoControlType.CheckBoxExControl:
                    //    return ((CheckBoxExControl)_mainControl).Value != (bool)_historyValue;
                    default:
                        break;
                }
                return true; ///这里需要改变
            }
        }

        /// <summary>
        /// 当焦点离开控件時，设置属性的值
        /// </summary>
        protected virtual void SetValueFromControl()
        {
            if (this.IsModifiedFromValue)
            {
                if (this._objects != null)
                {
                    foreach (object obj in this._objects)
                    {
                        SetValueFromControlSubMethod(obj);
                        OwnerAutoPanel.OnSaved(new ValueSaveEventArgs(this.Value,obj));
                    }
                }
                else
                {
                    SetValueFromControlSubMethod(null);
                    OwnerAutoPanel.OnSaved(new ValueSaveEventArgs(this.Value, null));
                }
            }
        }

        //将控件上的值保存到属性文件中 by lisuye on 2008年5月28日
        protected virtual void SetValueFromControlSubMethod(object obj)
        {
            if (this.MemberInfo.MemberType != MemberTypes.Property)
            {
                return;
            }
            PropertyInfo propertyInfo = (PropertyInfo)MemberInfo;

            if (propertyInfo.PropertyType.BaseType == typeof(System.Enum))
            {
                string value = (string)this.Value;
                if (!string.IsNullOrEmpty(value))
                {
                    Enum returnEnum = (Enum)Enum.Parse(propertyInfo.PropertyType, (string)this.Value);
                    propertyInfo.SetValue(obj, returnEnum, null);
                }
            }
            else
            {
                switch (propertyInfo.PropertyType.FullName)
                {
                    case "System.String":
                    case "System.Boolean":
                    case "System.DateTime":
                    case "System.Drawing.Color":
                    case "System.Object[]":
                    case "Jeelu.DepartmentData[]":
                        {
                            propertyInfo.SetValue(obj, this.Value, null);
                            break;
                        }
                    //case "System.Int32":
                    //    {
                    //        propertyInfo.SetValue(obj, Utility.Convert.StringToInt((string)this.Value), null);
                    //        break;
                    //    }
                    //case "System.Single":
                    //    {
                    //        propertyInfo.SetValue(obj, Utility.Convert.StringToFloat((string)this.Value), null);
                    //        break;
                    //    }
                    //case "Jeelu.ProjectPart[]":
                    //    {
                    //        propertyInfo.SetValue(obj, (ProjectPart[])this.Value, null);
                    //        break;
                    //    }
                    case "System.String[]":
                        {
                            if (MainControlType == AutoControlType.SelectGroup)
                            {
                                propertyInfo.SetValue(obj, this.Value.ToString().Split(new[] { ":|:" }, StringSplitOptions.RemoveEmptyEntries), null);
                            }
                            if (MainControlType == AutoControlType.ComboBoxGroupControl)
                            {
                                propertyInfo.SetValue(obj, this.Value, null);
                            }
                            break;
                        }
                    default:
                        Debug.Fail("design time: Type is not process. " + propertyInfo.PropertyType.FullName);
                        break;
                }
            }
        }

        #endregion

        #region 排版相关

        /* 排版共分6个区域
         * 1. 主控件上部的Label提示文字
         * 2. 主控件左侧的Label提示文字，含最左侧的图像标志
         * 3. 主控件在最中部（值的有效区域）
         * 4. 主控件右侧的Label提示文字
         * 5. 主控件下部的Label提示文字
         * 6. 主控件下部的Label下方的帮助文档
        */

        /// <summary>
        /// 对内部控件进行排版
        /// </summary>
        protected virtual void LayoutOwnControl()
        {

            int x = x1;
            int y = y1;

            #region 对6个控件进行排版

            bool flagTop = false;
            Dictionary<LabelPlace, Label> labelDic = this.BuildLabels();
            this._mainControl = this.BuildMainControl(AutoAttribute.MainControlType);
            this._mainControl.TabIndex = 10;

            Label label = null;
            //控件中label的设置 by lisuye on 2008年5月28日
            #region 是否有头部Label
            if (labelDic.TryGetValue(LabelPlace.Top, out label))
            {
                if (labelDic.ContainsKey(LabelPlace.Left))
                {
                    label.Location = new Point(x2, y);
                    
                }
                else
                {
                    label.Location = new Point(x1, y);
                }
                this.Controls.Add(label);
                flagTop = true;
                IsRed(label);
            }
            #endregion

            #region 是否有左侧Label
            if (labelDic.TryGetValue(LabelPlace.Left, out label))
            {
                //是否有头部Label
                if (flagTop)
                {
                    label.Location = new Point(x1, y2 + offsetLabelY);
                    x = x2 + offsetX;
                    y = y2;
                }
                else
                {
                    if (string.Compare(label.Text, "#:") == 0)
                    {
                        label.Text = "";
                    }
                    label.Location = new Point(x1, y1 + offsetLabelY);
                    x = x2 + offsetX;
                    y = y1;
                }

                #region 判断ValueControl是否有標志性的图片，如有将图片放入左侧Label
                if (!string.IsNullOrEmpty(AutoAttribute.LabelImage))
                {
                    Image image = Image.FromFile(AutoAttribute.LabelImage);
                    if (!string.IsNullOrEmpty(AutoAttribute.LabelLeft))
                    {
                        label.Size = new Size(txtWidth, image.Height);
                    }
                    else
                    {
                        label.Size = image.Size;
                        x = offsetX + offsetX + image.Width + offsetX + offsetX;
                        if (flagTop)
                        {
                            this.Controls[AutoAttribute.LabelTop].Location = new Point(x + offsetLabelX, y1);
                        }
                    }
                    label.Location = new Point(label.Location.X, label.Location.Y - offsetLabelY);
                    label.Image = image;
                    label.ImageAlign = ContentAlignment.MiddleLeft;
                }
                #endregion
                this.Controls.Add(label);
            }
            else
            {
                if (flagTop)
                {
                    y = y2;
                }
            }
            #endregion
            #region 设置主控件
            this.SetMainControlSize(AutoAttribute.MainControlType);

            this._mainControl.Location = new Point(x, y);
            this.Controls.Add(_mainControl);

            int rightX = _mainControl.Location.X + _mainControl.Width;
            #endregion

            #region 是否有右侧Label
            if (labelDic.TryGetValue(LabelPlace.Right, out label))
            {
                label.Location = new Point(x + _mainControl.Width + offsetX, y + offsetLabelY);
                this.Controls.Add(label);
                rightX = label.Location.X + label.Width + offsetX;
                IsRed(label);
            }

            y = _mainControl.Location.Y + _mainControl.Height;
            #endregion

            #region 是否有底部Label
            if (labelDic.TryGetValue(LabelPlace.Footer, out label))
            {
                label.Location = new Point(x + offsetLabelX, y + offsetY);
                this.Controls.Add(label);
                y = y + label.Height;
                IsRed(label);
            }
            x = _mainControl.Location.X;
            #endregion

            #region 是否有帮助文本
            if (labelDic.TryGetValue(LabelPlace.Help, out label))
            {
                int indent = AutoAttribute.LabelHelpLeftIndent;
                int labelWidth;
                if (AutoAttribute.LabelHelpWidth <= 0)
                {
                    labelWidth = _mainControl.Width;
                }
                else
                {
                    labelWidth = AutoAttribute.LabelHelpWidth;
                }
                KeyValuePair<string, Size> pair =
                    GetSize(AutoAttribute.LabelHelpText, labelWidth, label.CreateGraphics(), label.Font);
                label.Size = pair.Value;
                label.Text = pair.Key;
                label.Location = new Point(x + offsetLabelX + indent, y);
                ///判断有了帮助文本后的最大宽度是否大于“有或无右侧文本”时的宽度
                if ((label.Location.X + label.Width) > rightX)
                {
                    x = label.Location.X + label.Width + offsetX;
                }
                else
                {
                    x = rightX;
                }
                y = y + label.Height + offsetY;
                this.Controls.Add(label);
                IsRed(label);
            }
            else
            {
                x = rightX + offsetX;
            }
            #endregion

            this.Size = new Size(x, y);
            #endregion
        }
        protected void IsRed(Label lable)
        {
            if (AutoAttribute.IsRed)
            {
                lable.ForeColor = Color.Red;
            }

        }
        /// <summary>
        /// 根据定制特性中的5个Label的设置生成Label控件Dictionary的集合
        /// </summary>
        protected virtual Dictionary<LabelPlace, Label> BuildLabels()
        {
            Dictionary<LabelPlace, Label> labelDic = new Dictionary<LabelPlace, Label>(5);

            #region 循环创建5个Label
            for (int i = 0; i < 5; i++)
            {
                bool flag = false;
                string labelText = string.Empty;
                ContentAlignment align = ContentAlignment.MiddleLeft;
                LabelPlace place = LabelPlace.Left;

                switch (i)
                {
                    case 0://Top
                        #region
                        {
                            if (!string.IsNullOrEmpty(AutoAttribute.LabelTop))
                            {
                                place = LabelPlace.Top;
                                labelText = AutoLayoutPanel.GetLanguageText(AutoLayoutPanelXmlDocument.Singler, AutoAttribute.LabelTop) + ":";
                                flag = true;
                            }
                            break;
                        }
                        #endregion
                    case 1://Left
                        #region
                        {
                            if (!string.IsNullOrEmpty(AutoAttribute.LabelLeft) || !string.IsNullOrEmpty(AutoAttribute.LabelImage))
                            {
                                place = LabelPlace.Left;
                                labelText = AutoLayoutPanel.GetLanguageText(AutoLayoutPanelXmlDocument.Singler, AutoAttribute.LabelLeft) + ":";
                                align = ContentAlignment.TopRight;
                                flag = true;
                            }
                            break;
                        }
                        #endregion
                    case 2://Right
                        #region
                        {
                            if (!string.IsNullOrEmpty(AutoAttribute.LabelRight))
                            {
                                place = LabelPlace.Right;
                                labelText = AutoLayoutPanel.GetLanguageText(AutoLayoutPanelXmlDocument.Singler, AutoAttribute.LabelRight);
                                flag = true;
                            }
                            break;
                        }
                        #endregion
                    case 3://Footer
                        #region
                        {
                            if (!string.IsNullOrEmpty(AutoAttribute.LabelFooter))
                            {
                                place = LabelPlace.Footer;
                                labelText = AutoLayoutPanel.GetLanguageText(AutoLayoutPanelXmlDocument.Singler, AutoAttribute.LabelFooter);
                                flag = true;
                            }
                            break;
                        }
                        #endregion
                    case 4://Helper
                        #region
                        {
                            if (!string.IsNullOrEmpty(AutoAttribute.LabelHelpText))
                            {

                                place = LabelPlace.Help;
                                labelText = AutoLayoutPanel.GetLanguageText(AutoLayoutPanelXmlDocument.Singler, AutoAttribute.LabelHelpText);
                                flag = true;
                            }
                            break;
                        }
                        #endregion
                }

                if (flag)//生成Label
                {
                    #region
                    Label label = new Label();
                    label.Name = labelText;
                    label.Text = labelText;
              
                    if (place == LabelPlace.Right || place == LabelPlace.Top)
                    {
                        SizeF strSize = label.CreateGraphics().MeasureString(labelText, label.Font);
                        label.Size = Size.Truncate(strSize);
                        label.Width = label.Width + 3;
                    }
                    else
                    {
                        label.Size = _txtSize;
                    }
                    label.TabIndex = 0;
                    label.TextAlign = align;
                    labelDic.Add(place, label); // 如果Label控件被创建，就添加进Dictionary中去
                    #endregion
                }
            }
            #endregion

            return labelDic;
        }

        /// <summary>
        /// 根据定制特性中的设置解析出主数据控件
        /// </summary>
        protected virtual Control BuildMainControl(AutoControlType type)
        {
            PropertyInfo propertyInfo = MemberInfo as PropertyInfo;

            Control control = null;
            switch (type)
            {
                case AutoControlType.TextBox:
                    control = new TextBox();
                    if (propertyInfo.GetSetMethod() == null)
                    {
                        ((TextBox)control).ReadOnly = true;
                    }
                    if (AutoAttribute.TextBoxMultiLine)
                    {
                        ((TextBox)control).Multiline = AutoAttribute.TextBoxMultiLine;
                        ((TextBox)control).ScrollBars = AutoAttribute.TextBoxScrollBars;
                        ((TextBox)control).Height = AutoAttribute.TextBoxHeight;
                    }
                    if (AutoAttribute.TextMaxLength!=0)
                    ((TextBox)control).MaxLength = AutoAttribute.TextMaxLength;
                    break;
                case AutoControlType.SimpleCheckBox:
                    control = new SimpleCheckBox();
                    control.Text = AutoLayoutPanel.GetLanguageText(AutoLayoutPanelXmlDocument.Singler, AutoAttribute.SimpleCheckBoxText);
                    break;
                case AutoControlType.ListBox:
                    control = new ListBox();
                    control.Height = AutoAttribute.ListBoxHeight;
                    ParseBindingState(control);
                    break;
                case AutoControlType.ComboBox:
                    ComboBox box = new ComboBox();
                    if (AutoAttribute.ComboBoxStyle == 0)
                    {
                        box.DropDownStyle = ComboBoxStyle.DropDown;
                    }
                    else
                    {
                        box.DropDownStyle = AutoAttribute.ComboBoxStyle;
                    }
                    control = box;
                    ParseBindingState(control);
                    break;
                case AutoControlType.Button:
                    control = new Button();
                    if (!string.IsNullOrEmpty (AutoAttribute.ButtonText))
                    {
                        control.Text = AutoLayoutPanel.GetLanguageText(AutoLayoutPanelXmlDocument.Singler, AutoAttribute.ButtonText);
                    }
                    control.Click += delegate
                    {
                        ((MethodInfo)MemberInfo).Invoke(null, null);
                    };
                    break;
                case AutoControlType.NumericUpDown:
                    control = new NumericUpDown();
                    ((NumericUpDown)control).DecimalPlaces = AutoAttribute.NumericUpDownDecimalPlaces;
                    ((NumericUpDown)control).Maximum = AutoAttribute.NumericUpDownMax;
                    ((NumericUpDown)control).Minimum = AutoAttribute.NumericUpDownMin;
                    ((NumericUpDown)control).Increment = AutoAttribute.NumericUpDownStep;
                    break;
                //case AutoControlType.ColorGeneralButton:
                //    control = new ColorGeneralButton();
                //    break;
                //case AutoControlType.FileSelecterControl:
                //    {
                //        control = new FileSelecterControl();
                //        ((FileSelecterControl)control).SelectFolder = AutoAttribute.SelectFolder;
                //        ((FileSelecterControl)control).DialogTitle = AutoAttribute.FileSelecterControlDialogTitle;
                //        ((FileSelecterControl)control).FileSelectFilter = AutoAttribute.FileSelecterControlFilter;
                //        ((FileSelecterControl)control).InitialDirectory = AutoAttribute.FileSelecterControlInitialDirectory;
                //        ((FileSelecterControl)control).MultiSelect = AutoAttribute.FileSelecterControlMultiSelect;
                //        ((FileSelecterControl)control).Style = AutoAttribute.FileSelecterControlStyle;
                //        break;
                //    }
                //case AutoControlType.SelectGroup:
                //    {
                //        SelectGroup selectGroup = new SelectGroup();
                //        selectGroup.Width = AutoAttribute.MainControlWidth;
                //        selectGroup.HIndent = AutoAttribute.SelectGroupHIndent;
                //        selectGroup.HorizontalCount = AutoAttribute.SelectGroupHorizontalCount;
                //        selectGroup.LineHeight = AutoAttribute.SelectGroupLineHeight;
                //        selectGroup.MultiSelect = AutoAttribute.SelectGroupMultiModel;
                //        selectGroup.VIndent = AutoAttribute.SelectGroupVIndent;
                //        selectGroup.SelectedItemCount = AutoAttribute.SelectedItemCount;
                //        control = selectGroup;
                //        ParseBindingState(control);
                //        break;
                //    }
                case AutoControlType.DateTimePicker:
                    {
                        control = new DateTimePicker();
                        ((DateTimePicker)control).Checked = AutoAttribute.DateTimePickerChecked;
                        ((DateTimePicker)control).CustomFormat = AutoAttribute.DateTimePickerCustomFormat;
                        ((DateTimePicker)control).ShowCheckBox = AutoAttribute.DateTimePickerShowCheckBox;
                        ((DateTimePicker)control).ShowUpDown = AutoAttribute.DateTimePickerShowUpDown;
                        break;
                    }
                //case AutoControlType.ValidateTextBox:
                //    {
                //        control = new ValidateTextBox();
                //        ((ValidateTextBox)control).RegexText = AutoAttribute.ValidateTextBoxRegexText;
                //        ((ValidateTextBox)control).RegexTextRuntime = AutoAttribute.ValidateTextBoxRegexTextRuntime;
                //        if (propertyInfo.GetSetMethod() == null)
                //        {
                //            ((ValidateTextBox)control).ReadOnly = true;
                //        }
                //        break;
                //    }
                //case AutoControlType.DepartmentControl:
                //    {
                //        // string path = Path.Combine(AutoLayoutPanel.ResourcesPath, _autoAttribute.MainControlBindingFile);
                //        control = new DepartmentControl();
                //        break;
                //    }
                //case AutoControlType.ColorSelectorButton:
                //    {
                //        control = new ColorSelectorButton();
                //        control.Width = AutoAttribute.MainControlWidth;
                //        break;
                //    }
                //case AutoControlType.FontComboBox:
                //    {
                //        control = new FontComboBox();
                //        break;
                //    }
                //case AutoControlType.ComboBoxGroupControl:
                //    {
                //        string path = Path.Combine(AutoLayoutPanel.ResourcesPath, AutoAttribute.MainControlBindingFile);
                //        string mark = "";
                //        if (!string.IsNullOrEmpty(AutoAttribute.SpaceMark))
                //        {
                //            mark = AutoLayoutPanel.GetLanguageText(AutoAttribute.SpaceMark);
                //        }
                //        ComboBoxGroupControl comboBoxGroup = new ComboBoxGroupControl(path, mark);

                //        control = comboBoxGroup;
                //        break;
                //    }
                //case AutoControlType.CheckBoxExControl:
                //    {
                //        string labelText = AutoLayoutPanel.GetLanguageText(AutoLayoutPanelXmlDocument.Singler, AutoAttribute.CheckBoxExLabelText);
                //        string checkText = AutoLayoutPanel.GetLanguageText(AutoLayoutPanelXmlDocument.Singler, AutoAttribute.CheckBoxExText); ;
                //        control = new CheckBoxExControl(labelText, checkText);
                //        break;
                //    }
                default:
                    Debug.Fail("\"" + type + "\" cannot prase!");
                    break;
            }

            ///定制特性的IsReadOnly为true，或属性没有set方法则将控件的Enabled置为false
            if (AutoAttribute.IsReadOnly ||
                (MemberInfo.MemberType == MemberTypes.Property && !((PropertyInfo)MemberInfo).CanWrite))
            {
                if (control is TextBox)
                {
                    ((TextBox)control).ReadOnly = true;
                }
                else
                {
                    control.Enabled = false;
                }
            }
      
            return control;
        }

        #region 绑定相关
        protected virtual void FileBindingService(Control ctr, string fileName, out DataTable dt)
        {
            dt = DataTableService(fileName);
            FileBindingService(ctr, fileName);
        }
        protected virtual void FileBindingService(Control ctr, string fileName)
        {
            DataTable dt = DataTableService(fileName);
            DtBindingService(ctr, dt);
        }
        protected virtual void EnumBindingService(Control ctr, Type myType)
        {
            //Array texts = Enum.GetValues(myType);

            //DataTable dt = new DataTable();
            //dt.Columns.Add("value");
            //dt.Columns.Add("text");

            //foreach (var item in texts)
            //{
            //    string text = Service.Resource.GetEnumResourceText(item.ToString());
            //    dt.Rows.Add(Enum.Parse(myType, item.ToString()), text);
            //}
            //DtBindingService(ctr, dt);
            Debug.Fail("");
        }
        DataTable DataTableService(string fileName)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("value");
            dt.Columns.Add("text");

            Debug.Assert(File.Exists(fileName), "Configtion file isn't Exists");
            using (XmlTextReader reader = new XmlTextReader(fileName))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.GetAttribute("text") != null && reader.GetAttribute("value") != null)
                    {
                        dt.Rows.Add(reader.GetAttribute("value"), reader.GetAttribute("text"));
                    }
                }
            }
            return dt;
        }
        protected void DtBindingService(Control ctr, DataTable dt)
        {
            if (ctr == null)
            {
                return;
            }
            if (ctr is ListControl)
            {
                ListControl myControl = (ListControl)ctr;
                myControl.DataSource = dt;
                myControl.DisplayMember = "text";
                myControl.ValueMember = "value";
            }
            //if (ctr is SelectGroup)
            //{
            //    SelectGroup myControl = (SelectGroup)ctr;
            //    myControl.DataSource = dt;
            //    myControl.DisplayMember = "text";
            //    myControl.ValueMember = "value";
            //    myControl.DataBinding();
            //}
        }

        #endregion                        

        private void ParseBindingState(Control control)
        {
            if (this.MemberInfo.MemberType != MemberTypes.Property)
            {
                return;
            }
            PropertyInfo propertyInfo = (PropertyInfo)MemberInfo;

            if (propertyInfo.PropertyType.BaseType == typeof(System.Enum))
            {
                EnumBindingService(control, propertyInfo.PropertyType);
            }
            else
            {
                string file = Path.Combine(AutoLayoutPanel.ResourcesPath, AutoAttribute.MainControlBindingFile);
                FileBindingService(control, file);
            }
        }

        /// <summary>
        /// 根据定制特性中的设置完成ValueControl中的主控件的Size
        /// </summary>
        protected virtual void SetMainControlSize(AutoControlType type)
        {
            int width = AutoAttribute.MainControlWidth;
            switch (type)
            {
                case AutoControlType.TextBox:
                case AutoControlType.ComboBox:
                case AutoControlType.NumericUpDown:
                case AutoControlType.DateTimePicker:
                case AutoControlType.RegexTextBox:
                    this._mainControl.Size = new Size(width, inpHeignt);
                    break;
                case AutoControlType.ListBox:
                    this._mainControl.Size = new Size(width, AutoAttribute.ListBoxHeight);
                    break;
                case AutoControlType.Button:
                case AutoControlType.ColorGeneralButton:
                    this._mainControl.Size = new Size(width, inpHeignt);
                    break;
                case AutoControlType.FileSelecterControl:
                    this._mainControl.Size = new Size(width, inpHeignt + 1);
                    break;
                case AutoControlType.SimpleCheckBox:
                    ((CheckBox)_mainControl).AutoSize = true;
                    break;
                //case AutoControlType.SelectGroup:
                //    //设置SelectGroup的大小(其实可以得到当前selectGroup的宽度),目前是重新对SelectGroup内部控件进行重新排序
                //    ((SelectGroup)_mainControl).AutoSize = true;
                //    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Label标签的位置
        /// </summary>
        protected enum LabelPlace { Top, Left, Right, Footer, Help, }

        #endregion
    }

    public class SimpleCheckBox : CheckBox
    {
        public bool Value
        {
            get { return this.Checked; }
            set { this.Checked = value; }
        }
    }
}
