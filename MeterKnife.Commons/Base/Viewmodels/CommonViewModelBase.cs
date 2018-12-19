using GalaSoft.MvvmLight;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Plugins;
using NKnife.IoC;

namespace MeterKnife.Base.Viewmodels
{
    public class CommonViewModelBase : ViewModelBase
    {
        protected IUserHabits Habit { get; }

        public CommonViewModelBase()
        {
            Habit = DI.Get<IUserHabits>();
        }
    }
}
