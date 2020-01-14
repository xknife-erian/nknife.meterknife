using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NKnife.GUI.WinForm
{
    /// <summary>
    /// 一个实现用正则校验的TextBox
    /// </summary>
    public class RegexTextBox : TextBox
    {
        private bool _IsValidating;
        private Regex _Regex;
        private Regex _RegexRuntime;
        private int _SelectionStart;
        private int _SelectionLength;
        private string _OldText;

        public string RegexText
        {
            get { return Convert.ToString(_Regex); }
            set { _Regex = new Regex(value, RegexOptions.ExplicitCapture); }
        }

        public string RegexTextRuntime
        {
            get { return Convert.ToString(_RegexRuntime); }
            set { _RegexRuntime = new Regex(value, RegexOptions.ExplicitCapture); }
        }

        public bool IsValidate()
        {
            if (_Regex == null)
            {
                throw new ArgumentNullException("RegexText", "没有初始化RegexText");
            }
            return _Regex.IsMatch(Text);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            OnCheckChanged(EventArgs.Empty);
            if (_IsValidating)
            {
                return;
            }
            //用指定的正则验证
            if (_RegexRuntime != null)
            {
                if (_RegexRuntime.IsMatch(Text))
                {
                    _OldText = Text;
                    _SelectionStart = SelectionStart;
                    _SelectionLength = SelectionLength;
                }
                else
                {
                    _IsValidating = true;
                    Text = _OldText;
                    _IsValidating = false;
                    SelectionStart = _SelectionStart;
                    SelectionLength = _SelectionLength;
                }
            }

            base.OnTextChanged(e);
        }

        public event EventHandler Changed;
        protected virtual void OnCheckChanged(EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            _SelectionStart = SelectionStart;
            _SelectionLength = SelectionLength;
            base.OnKeyDown(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            _SelectionStart = SelectionStart;
            _SelectionLength = SelectionLength;
            base.OnMouseDown(e);
        }
    }
}
