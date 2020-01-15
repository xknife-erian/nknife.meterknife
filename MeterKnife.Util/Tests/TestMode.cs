using System.ComponentModel;

namespace NKnife.Tests
{
    /// <summary>测试执行过程参数
    /// </summary>
    public class TestMode
    {
        private uint _Count = 1;

        private uint _Interval = 200;

        [Category("测试执行过程参数")]
        [Description("运行次数")]
        public uint Count
        {
            get { return _Count; }
            set { _Count = value; }
        }

        [Category("测试执行过程参数")]
        [Description("运行间隔")]
        public uint Interval
        {
            get { return _Interval; }
            set { _Interval = value; }
        }
    }
}