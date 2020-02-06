// ReSharper disable once CheckNamespace

namespace NKnife.MeterKnife.Base
{
    /// <summary>
    ///     软件的“使用习惯记录文件”的记录保存与读取服务
    /// </summary>
    public interface IHabitConfig
    {
        /// <summary>
        ///     尝试获取指定Key的选项的值
        /// </summary>
        T GetOptionValue<T>(string key, T defaultValue);

        /// <summary>
        ///     设置指定Key的选项的值，值对象序列化成Json保存
        /// </summary>
        void SetOptionValue(string key, object value);
    }
}