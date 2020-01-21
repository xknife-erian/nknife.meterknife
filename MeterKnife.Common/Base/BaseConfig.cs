namespace NKnife.MeterKnife.Common.Base
{
    public abstract class BaseConfig : BaseRecord
    {
        /// <summary>
        ///     以Json字符串进行保存的配置信息
        /// </summary>
        public string Config { get; set; }
    }
}
