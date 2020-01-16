using System;
using System.Collections.Concurrent;
using Common.Logging;
using MeterKnife.Util.Serial.Pan.Interfaces;

namespace MeterKnife.Util.Serial.Pan.Common
{
    /// <summary>向串口即将发送的指令包的集合
    /// TODO:这里分成三类包，双向包，单向包，巡查包，三类包的优先级不同，其实三类包没有必要，下一步引入包优先级的概念
    /// </summary>
    internal class SerialDataPool : ISerialDataPool
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
        private readonly ConcurrentQueue<PackageBase> _OneWayPool = new ConcurrentQueue<PackageBase>();
        private readonly ConcurrentQueue<PackageBase> _QueryPool = new ConcurrentQueue<PackageBase>();
        private readonly ConcurrentQueue<PackageBase> _TwoWayPool = new ConcurrentQueue<PackageBase>();

        /// <summary>添加数据包
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="package"></param>
        public void AddPackage<T>(T package) where T : PackageBase
        {
            if (package is TwoWayPackage)
            {
                _TwoWayPool.Enqueue(package);
            }
            else if (package is OneWayPackage)
            {
                _OneWayPool.Enqueue(package);
            }
            else if (package is QueryPackage)
            {
                _QueryPool.Enqueue(package);
            }
        }

        /// <summary>尝试获取数据包
        /// </summary>
        /// <param name="package"></param>
        /// <param name="packageType"></param>
        /// <returns>有数据包则返回true，无则返回false</returns>
        public bool TryGetPackage(out PackageBase package,out int packageType)
        {
            if (!_TwoWayPool.IsEmpty)
            {
                if (GetTwoWayPackage(out package))
                {
                    packageType = 2;
                    return true;
                }
            }
            if (!_OneWayPool.IsEmpty)
            {
                if (_OneWayPool.TryDequeue(out package))
                {
                    packageType = 1;
                    return true;
                }
            }
            if (!_QueryPool.IsEmpty)
            {
                if (_QueryPool.TryDequeue(out package))
                {
                    packageType = 3;
                    return true;
                }
            }
            package = null;
            packageType = 0;
            return false;
        }

        /// <summary>获取双向协议的指令包。包含失败重发的逻辑控制。
        /// </summary>
        /// <param name="outTwoWayPackage">The package.</param>
        /// <returns></returns>
        private bool GetTwoWayPackage(out PackageBase outTwoWayPackage)
        {
            PackageBase package;
            if (_TwoWayPool.TryPeek(out package))
            {
                var two = (TwoWayPackage) package;
                if (two.AlreadySentTimes == 0)
                {
                    //当已发次数为零时，也就是第一次发送前，注册一次事件
                    two.PackageSent +=
                        (s, e) =>
                            {
                                if (e.Replied)
                                    _TwoWayPool.TryDequeue(out package);
                            };
                }
                if (two.AlreadySentTimes < two.SendTimes)
                {
                    if (two.AlreadySentTimes > 0)
                        _logger.Trace(string.Format("[{0}/{1}]重发:,{2}", two.AlreadySentTimes + 1, two.SendTimes, two.DataToSend.ToHexString()));
                    two.AlreadySentTimes++;
                    outTwoWayPackage = package;
                    return true;
                }
                two.OnNoResponse();
                _TwoWayPool.TryDequeue(out package);
            }
            outTwoWayPackage = package;
            return false;
        }
    }
}