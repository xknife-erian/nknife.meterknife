using System;

namespace NKnife.Tests
{
    /// <summary>测试案例
    /// </summary>
    public interface ITestCase
    {
        /// <summary>对本测试的详细描述
        /// </summary>
        string Description { get; }

        /// <summary>测试参数
        /// </summary>
        object[] TestParams { get; set; }

        /// <summary>测试模式
        /// </summary>
        TestMode TestMode { get; set; }

        /// <summary>测试结果
        /// </summary>
        object TestResult { get; set; }

        /// <summary>当测试结果发生更新时发生的事件
        /// </summary>
        event EventHandler TestResultUpdated;

        /// <summary>运行测试
        /// </summary>
        void Run();
    }
}
