// ReSharper disable once CheckNamespace
namespace NKnife.MeterKnife.Base
{
    public interface IPathManager
    {
        /// <summary>
        /// 用户应用程序数据路径。“使用习惯记录文件”与“软件选项数据文件”将会被保存在这里。eg. C:\Users\xxx\AppData\Roaming
        /// </summary>
        string UserApplicationDataPath { get; }

        /// <summary>
        /// 用户操作系统的文档目录路径。eg. C:\Users\xxx\Documents
        /// </summary>
        string UserDocumentsPath { get; }
    }
}