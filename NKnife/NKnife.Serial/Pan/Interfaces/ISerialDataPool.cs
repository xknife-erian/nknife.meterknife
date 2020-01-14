using SerialKnife.Pan.Common;

namespace SerialKnife.Pan.Interfaces
{
    /// <summary>向串口即将发送的指令包的集合
    /// </summary>
    public interface ISerialDataPool
    {
        /// <summary>向数据池中添加一个数据封装包
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="package">The package.</param>
        void AddPackage<T>(T package) where T : PackageBase;

        /// <summary>尝试获取一个数据封装包
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="packageType">1=单向，2=双向,3=轮询</param>
        /// <returns></returns>
        bool TryGetPackage(out PackageBase package,out int packageType);
    }
}