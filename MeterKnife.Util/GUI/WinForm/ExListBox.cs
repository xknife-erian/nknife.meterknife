using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NKnife.GUI.WinForm
{
    /*2012年2月2日，因去除Gean引用，本类型不再使用*/
    public sealed class ExListBox : ListBox//, IOptionItemManager
    {
        private readonly Image _DefaultConfigImage;

        /// <summary>
        /// 创建 ExListBox class 的新实例
        /// </summary>
        public ExListBox()
        {
            MeasureItem += ExListBoxMeasureItem;
            DrawItem += ExListBoxDrawItem;
            DrawMode = DrawMode.OwnerDrawVariable;
        }

        #region IOptionItemManager Members

        //public IEnumerable<object> Initialize(IEnumerable<object> optionItems)
        //{
        //    var controls = new List<object>();
        //    foreach (object optionItem in optionItems)
        //    {
        //        Items.Add(optionItem);
        //        //controls.Add(optionItem.OptionUI);
        //    }
        //    return controls;
        //}

        public void Initialize(IEnumerable<IOptionListItem> optionItems)
        {
            foreach (object optionItem in optionItems)
            {
                Items.Add(optionItem);
                //controls.Add(optionItem.OptionUI);
            }
        }

        #endregion

        private void ExListBoxDrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= Items.Count)
                return;

            var cfgItem = Items[e.Index] as IOptionListItem;
            if (cfgItem == null) return;

            e.DrawBackground();
            e.DrawFocusRectangle();

            Graphics g = e.Graphics;

            g.DrawImage(_DefaultConfigImage,
                        new Rectangle(new Point(3 + e.Bounds.Left, 3 + e.Bounds.Top), new Size(16, 16)));

            var brush = new SolidBrush(e.ForeColor);
            g.DrawString(cfgItem.MenuName, new Font("宋体", 12.0F, FontStyle.Regular, GraphicsUnit.Pixel), brush,
                         new PointF(23 + e.Bounds.Left, 5 + e.Bounds.Top));
        }

        private void ExListBoxMeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index >= Items.Count) return;

            var cfgItem = Items[e.Index] as IOptionListItem;
            if (cfgItem == null)
                return;

            e.ItemHeight = cfgItem.MenuHeight;
        }
    }


    public interface IOptionListItem : IComparable
    {
        /// <summary>当Item做为菜单时的高度
        /// </summary>
        /// <value>The height of the menu.</value>
        int MenuHeight { get; }

        /// <summary>当Item做为菜单时的显示名
        /// </summary>
        /// <value>The name of the menu.</value>
        string MenuName { get; }

        /// <summary>当Item做为菜单时的提示
        /// </summary>
        string MenuHint { get; }

        /// <summary>当Item做为菜单时的显示的图标
        /// </summary>
        Image MenuIcon { get; }

        /// <summary>获取一个配置的编辑面板，请务必采用单建模式
        /// </summary>
        OptionControlBase OptionUI { get; }

        /// <summary>该项是否显示
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        bool Visible { get; set; }

        /// <summary>当Item在一个集合中的排序，数字越大，将排列越靠前
        /// </summary>
        /// <value>The index of the order.</value>
        int OrderIndex { get; }

        /// <summary>释放链接的OptionControlBase
        /// </summary>
        void DisposeUI();
    }

    public abstract class AbstractOptionListItem : IOptionListItem
    {
        #region IOptionItem Members

        /// <summary>
        /// 当Item做为菜单时的高度
        /// </summary>
        /// <value>The height of the menu.</value>
        public virtual int MenuHeight
        {
            get { return 20; }
        }

        /// <summary>
        /// 该项是否显示
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        public bool Visible
        {
            get { return OptionUI.Visible; }
            set { OptionUI.Visible = value; }
        }

        /// <summary>
        /// 将当前实例与同一类型的另一个对象进行比较，并返回一个整数，该整数指示当前实例在排序顺序中的位置是位于另一个对象之前、之后还是与其位置相同。
        /// </summary>
        /// <param name="obj">与此实例进行比较的对象。</param>
        /// <returns>
        /// 一个值，指示要比较的对象的相对顺序。返回值的含义如下：值含义小于零此实例小于 <paramref name="obj"/>。零此实例等于 <paramref name="obj"/>。大于零此实例大于 <paramref name="obj"/>。
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        /// 	<paramref name="obj"/> 不具有与此实例相同的类型。</exception>
        public int CompareTo(object obj)
        {
            if (obj is IOptionListItem)
            {
                var item = (IOptionListItem)obj;
                return item.OrderIndex - this.OrderIndex;
            }
            return 0;
        }

        /// <summary>
        /// 当Item做为菜单时的显示名
        /// </summary>
        /// <value>The name of the menu.</value>
        public abstract string MenuName { get; }

        /// <summary>
        /// 当Item做为菜单时的提示
        /// </summary>
        public virtual string MenuHint
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>当Item做为菜单时的显示的图标
        /// </summary>
        public abstract Image MenuIcon { get; }

        /// <summary>
        /// 获取一个配置的编辑面板，请务必采用单建模式
        /// </summary>
        /// <value></value>
        public abstract OptionControlBase OptionUI { get; protected set; }

        /// <summary>当Item在一个集合中的排序，数字越大，将排列越靠前
        /// </summary>
        /// <value>The index of the order.</value>
        public abstract int OrderIndex { get; }

        /// <summary>释放链接的OptionControlBase
        /// </summary>
        public void DisposeUI()
        {
            OptionUI.Dispose();
            OptionUI = null;
        }

        #endregion
    }

    public class OptionControlBase : UserControl, IOptionControl
    {
        public OptionControlBase()
        {
            NeedReStartApp = false;
            IsModified = false;
            Load += (s, e) => Initialize();
        }

        #region IOptionControl Members

        /// <summary> 初始化
        /// </summary>
        /// <returns></returns>
        public virtual bool Initialize()
        {
            SetTextBoxModifiedEvent(this);
            return true;
        }
        /// <summary>初始化时为所有控件加载事件
        /// </summary>
        /// <param name="ctr"></param>
        private void SetTextBoxModifiedEvent(Control ctr)
        {
            foreach (Control control in ctr.Controls)
            {
                if (control.Controls.Count > 0)
                    SetTextBoxModifiedEvent(control);//当控件是窗口时，递归所有控件的子控件
                if (control is TextBox)
                    control.TextChanged += ControlValueModifiedChanged;
                else if (control is CheckBox)
                    ((CheckBox)control).CheckedChanged += ControlValueModifiedChanged;
                else if (control is RadioButton)
                    ((RadioButton)control).CheckedChanged += ControlValueModifiedChanged;
                else if (control is NumericUpDown)
                    ((NumericUpDown)control).ValueChanged += ControlValueModifiedChanged;
                else if (control is DateTimePicker)
                    ((DateTimePicker)control).ValueChanged += ControlValueModifiedChanged;
                else if (control is ComboBox)
                    ((ComboBox)control).SelectedIndexChanged += ControlValueModifiedChanged;
                else if (control is ListBox)
                    ((ListBox)control).SelectedIndexChanged += ControlValueModifiedChanged;
            }
        }

        private void ControlValueModifiedChanged(object sender, EventArgs e)
        {
            var ctl = (Control)sender;
            string eventInfo = ctl.Tag != null ? ctl.Tag.ToString() : ctl.Name;
            OnHasModifiedEvent(new HasModifiedEventArgs(eventInfo));
        }

        /// <summary>本界面中的值已经发生修改
        /// </summary>
        /// <value></value>
        public bool IsModified { get; internal set; }

        /// <summary>是否需要重启应用程序
        /// </summary>
        /// <value></value>
        public bool NeedReStartApp { get; set; }

        /// <summary> 是否需要重启计算机
        /// </summary>
        /// <value></value>
        public bool NeedReStartComputer
        {
            get { return false; }
        }

        /// <summary>从界面中取出所有的值进行保存
        /// </summary>
        /// <returns></returns>
        public virtual bool Save()
        {
            return true;
        }

        /// <summary> 应用设置值
        /// </summary>
        /// <returns></returns>
        public virtual bool FillApplication()
        {
            return true;
        }

        /// <summary>撤消本次界面所做的修改，加载本次修改前的设置
        /// </summary>
        /// <returns></returns>
        public virtual bool Retract()
        {
            return true;
        }

        /// <summary> 加载默认值
        /// </summary>
        /// <returns></returns>
        public virtual bool LoadDefault()
        {
            return true;
        }

        /// <summary> 当有数据改变后发生的事件
        /// </summary>
        public virtual event HasModifiedEventHandler HasModifiedEvent;

        protected virtual void OnHasModifiedEvent(HasModifiedEventArgs e)
        {
            if (HasModifiedEvent != null)
            {
                HasModifiedEvent(this, e);
                IsModified = true;
            }
        }

        #endregion
    }

    public interface IOptionControl
    {
        /// <summary>本界面中的值已经发生修改
        /// </summary>
        bool IsModified { get; }

        /// <summary>是否需要重启应用程序
        /// </summary>
        bool NeedReStartApp { get; }

        /// <summary>是否需要重启计算机
        /// </summary>
        bool NeedReStartComputer { get; }

        /// <summary>初始化
        /// </summary>
        /// <returns></returns>
        bool Initialize();

        /// <summary>从界面中取出所有的值进行保存
        /// </summary>
        /// <returns></returns>
        bool Save();

        /// <summary>应用设置值
        /// </summary>
        /// <returns></returns>
        bool FillApplication();

        /// <summary>撤消本次界面所做的修改，加载本次修改前的设置
        /// </summary>
        /// <returns></returns>
        bool Retract();

        /// <summary>加载默认值
        /// </summary>
        /// <returns></returns>
        bool LoadDefault();

        /// <summary>
        /// 当有数据改变后发生的事件
        /// </summary>
        event HasModifiedEventHandler HasModifiedEvent;

        /*命令模式，由于有难度，暂时不纳入了。2011-01-25 0:12:33 lukan*/
        ///// <summary>
        ///// 实现命令模式，以实现“重做”
        ///// </summary>
        //void Execute();
        ///// <summary>
        ///// 实现命令模式，以实现“撤消”
        ///// </summary>
        //void UnExecute();
    }

    public delegate void HasModifiedEventHandler(object sender, HasModifiedEventArgs e);

    /// <summary>
    /// 当有配置数据改变后发生的事件时，本类可能携带修改信息提示，用以提示用户需要保存
    /// </summary>
    public class HasModifiedEventArgs : EventArgs
    {
        public string ModifiedInfo { get; private set; }
        public HasModifiedEventArgs(string modifiedInfo)
        {
            this.ModifiedInfo = modifiedInfo;
        }
        public HasModifiedEventArgs()
        {
        }
    }
}