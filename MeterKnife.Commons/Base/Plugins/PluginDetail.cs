using System;

namespace MeterKnife.Interfaces.Plugins
{
    /// <summary>
    ///     插件的研发方详细信息
    /// </summary>
    public class PluginDetail
    {
        /// <summary>
        ///     插件的作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        ///     插件的联系方式
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        ///     插件的描述
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        ///     插件的发布时间
        /// </summary>
        public DateTime PublishDate { get; set; }

        /// <summary>
        ///     插件的版本
        /// </summary>
        public Version Version { get; set; }
    }
}