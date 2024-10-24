using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKnife.Circe.Base.Modules;
using NKnife.Circe.Base.Modules.Manager;
using RAY.Common;

namespace NKnife.Module.Manager.SurroundingManager.Internal
{
    internal class DefaultSurroundingsManager : ISurroundingsManager
    {
        public bool IsLaunched { get; private set; } = false;

        public IManager Initialize(params object[] args)
        {
            IsLaunched = true;
            return this;
        }

        public Task<IManager> LaunchAsync(params object[] args)
        {
            return Task.FromResult<IManager>(this);
        }

        public bool Stop()
        {
            return true;
        }

        public string Description { get; } = "软件运行环境的管理器";

        public void Dispose()
        {
        }
    }
}
