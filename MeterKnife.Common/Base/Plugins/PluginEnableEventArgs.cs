using System;

namespace MeterKnife.Interfaces.Plugins
{
    /// <summary>
    /// 插件功能的可用性事件的事件数据类
    /// </summary>
    public class PluginEnableEventArgs : EventArgs
    {
        public ushort FunctionType { get; set; }
        public bool Enable { get; set; }
        public bool Checked { get; set; }
    }
}