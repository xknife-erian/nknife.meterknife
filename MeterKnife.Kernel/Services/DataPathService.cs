using Common.Logging;
using MeterKnife.Common;
using MeterKnife.Common.Interfaces;
using NKnife.Configuring.Interfaces;
using NKnife.Interface;
using NKnife.IoC;

namespace MeterKnife.Kernel.Services
{
    public class DataPathService : IEnvironmentItem
    {
        private static readonly ILog _logger = LogManager.GetLogger<DataPathService>();

        public bool StartService()
        {
            var userdata = DI.Get<IUserApplicationData>();
            var dataPath = userdata.GetValue(MeterKnifeUserData.DATA_PATH, string.Empty);
            _logger.Info(string.Format("数据存储路径:{0}", dataPath));
            return true;
        }

        public bool CloseService()
        {
            return true;
        }

        public int Order { get { return 999; } }

        public string Description { get { return "数据路径检查器"; } }
    }
}
