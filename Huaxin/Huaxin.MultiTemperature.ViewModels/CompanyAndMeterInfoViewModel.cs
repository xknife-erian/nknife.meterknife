using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using Huaxin.MultiTemperature.ViewModels.Entities;
using NKnife.Interface;
using NKnife.IoC;

namespace Huaxin.MultiTemperature.ViewModels
{
    public class CompanyAndMeterInfoViewModel : ViewModelBase
    {
        private Kernel _Kernel = DI.Get<Kernel>();

        public CompanyAndMeterInfoViewModel()
        {
            MeterageNumber = DI.Get<IIdGenerator>().Generate();
        }

        private Company _CurrentCompany = new Company();

        public Company CurrentCompany
        {
            get => _CurrentCompany;
            set { Set(() => CurrentCompany, ref _CurrentCompany, value); }
        }

        private MeterInfo _CurrentMeterInfo;

        public MeterInfo CurrentMeterInfo
        {
            get => _CurrentMeterInfo;
            set { Set(() => CurrentMeterInfo, ref _CurrentMeterInfo, value); }
        }

        public string MeterageNumber { get; set; }

        public void Clear()
        {
            CurrentCompany = new Company();
        }

        public void Accept()
        {
            _Kernel.WorkedCompany = CurrentCompany;
            _Kernel.WorkedMeter = CurrentMeterInfo;
            _Kernel.MeterageNumber = MeterageNumber;
        }

        public void LoadCompany()
        {
            for (int i = 0; i < 5; i++)
            {
                var c = new Company();
                c.Name = $"{i}{i}{i}{i}-company";
                c.MeterInfos.Add(new MeterInfo() { Name = $"{i}{i}{i}{i}-meter" });
                c.MeterInfos.Add(new MeterInfo() { Name = $"{i}{i}{i}{i}-meter" });
                c.MeterInfos.Add(new MeterInfo() { Name = $"{i}{i}{i}{i}-meter" });
                c.MeterInfos.Add(new MeterInfo() { Name = $"{i}{i}{i}{i}-meter" });
                Companies.Add(c);
            }
        }

        public ObservableCollection<Company> Companies { get; } = new ObservableCollection<Company>();

        public void SetCurrentCompaniesSelectedIndex(int selectedIndex)
        {
            CurrentCompany = selectedIndex >= 0 ? Companies[selectedIndex] : new Company();
        }

        public void SetCurrentMeterInfosSelectedIndex(int selectedIndex)
        {
            CurrentMeterInfo = selectedIndex >= 0 ? CurrentCompany.MeterInfos[selectedIndex] : null;
        }
    }
}
