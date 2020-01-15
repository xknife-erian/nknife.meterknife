using System;

namespace NKnife.Tests
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class TestCaseAttribute : Attribute
    {
        /// <summary>描述一个测试用例
        /// </summary>
        /// <param name="name">测试用例名称</param>
        /// <param name="catogery">测试用例分类名称</param>
        public TestCaseAttribute(string name, string catogery)
        {
            Catogery = catogery;
            Name = name;
        }

        public string Name { get; private set; }

        public string Catogery { get; private set; }
    }
}