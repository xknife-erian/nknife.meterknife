using GalaSoft.MvvmLight;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Plugins;
using NKnife.IoC;

namespace MeterKnife.Base.Viewmodels
{
    public class CommonViewModelBase : ViewModelBase
    {
        protected IHabited Habited { get; }
        protected IExtenderProvider ExtenderProvider { get; set; }

        public CommonViewModelBase()
        {
            Habited = DI.Get<IHabited>();
        }

        public virtual void SetProvider(IExtenderProvider extenderProvider)
        {
            ExtenderProvider = extenderProvider;
        }
    }
}
