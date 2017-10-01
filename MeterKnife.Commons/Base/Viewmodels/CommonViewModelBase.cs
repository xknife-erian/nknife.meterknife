using GalaSoft.MvvmLight;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Plugins;
using NKnife.IoC;

namespace MeterKnife.Base.Viewmodels
{
    public class CommonViewModelBase : ViewModelBase
    {
        protected IHabited Habited { get; }

        public CommonViewModelBase()
        {
            Habited = DI.Get<IHabited>();
        }
    }
}
