using System;

namespace NKnife.Configuring.Common
{
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
