using NKnife.Circe.Base.Modules;
using NKnife.Circe.Base.Modules.Manager;
using RAY.Common.Plugin.Modules;

namespace NKnife.Module.Manager.SurroundingManager
{
    public class SurroundingManagerModule : BasePicoModule<ISurroundingsManager>
    {
        public override Task<bool> StartServiceAsync()
        {
            return Task.FromResult(true);
        }

        public override Task<bool> StopServiceAsync()
        {
            return Task.FromResult(true);
        }

        public override Task<bool> ResetServiceAsync()
        {
            return Task.FromResult(true);
        }

        public override Lazy<ISurroundingsManager> Build(params object[] args)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
        }
    }
}
