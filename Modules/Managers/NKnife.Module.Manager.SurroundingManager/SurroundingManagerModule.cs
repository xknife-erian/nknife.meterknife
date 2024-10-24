using Moq;
using NKnife.Circe.Base.Modules;
using RAY.Common.Plugin.Modules;

namespace NKnife.Module.Manager.SurroundingManager
{
    public class SurroundingManagerModule : BasePicoModule<ISurroundings>
    {
        public override Lazy<ISurroundings> Build(params object[] args)
        {
            return new Lazy<ISurroundings>(() => new Mock<ISurroundings>().Object);
        }   
        
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



        public override void Dispose()
        {
        }
    }
}
