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
            throw new NotImplementedException();
        }

        public void LoadCompany()
        {
            for (int i = 0; i < 5; i++)
            {
                var c = new Company();
                c.Name = $"{i}{i}{i}{i}-company";
                Companies.Add(c);
            }
        }

        public ObservableCollection<Company> Companies { get; } = new ObservableCollection<Company>();

        public void SetCurrentCompaniesSelectedIndex(int selectedIndex)
        {
            if (selectedIndex >= 0)
                CurrentCompany = Companies[selectedIndex];
        }

        public void SetCurrentMeterInfosSelectedIndex(int selectedIndex)
        {
            if (selectedIndex >= 0)
                CurrentMeterInfo = CurrentCompany.MeterInfos[selectedIndex];
        }
    }
}
