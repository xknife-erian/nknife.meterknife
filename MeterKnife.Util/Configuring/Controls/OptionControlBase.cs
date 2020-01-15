using System;
using System.Windows.Forms;
using Common.Logging;
using NKnife.Configuring.Common;
using NKnife.Configuring.Interfaces;
using NKnife.Configuring.OptionCase;
using NKnife.Interface;

namespace NKnife.Configuring.Controls
{
    public class OptionControlBase : UserControl, IOptionControl
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

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
            try
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
            catch (Exception e)
            {
                _logger.Warn("初始化时为所有控件加载事件异常", e);
            }
        }

        private void ControlValueModifiedChanged(object sender, System.EventArgs e)
        {
            var ctl = (Control) sender;
            string eventInfo = ctl.Tag != null ? ctl.Tag.ToString() : ctl.Name;
            OnHasModifiedEvent(new HasModifiedEventArgs(eventInfo));
        }

        /// <summary>时间方案
        /// </summary>
        public OptionCaseItem CaseItem { get; set; }

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

        /// <summary>取消界面中做出的临时修改
        /// </summary>
        /// <returns></returns>
        public virtual bool CancelSave()
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
}