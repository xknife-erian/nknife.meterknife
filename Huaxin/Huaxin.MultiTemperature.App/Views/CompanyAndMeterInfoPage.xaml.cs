using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Huaxin.MultiTemperature.ViewModels;

namespace Huaxin.MultiTemperature.App.Views
{
    /// <summary>
    /// CompanyAndMeterInfoPage.xaml 的交互逻辑
    /// </summary>
    public partial class CompanyAndMeterInfoPage
    {
        private readonly CompanyAndMeterInfoViewModel _ViewModel;

        public CompanyAndMeterInfoPage()
        {
            InitializeComponent();
            _ViewModel = (CompanyAndMeterInfoViewModel) DataContext;
            ViewModelPropertyChangedManager();
            ViewModelCollectionManager();
            ControlEventManager();
            Loaded += (s, e) =>
            {
                _MeterageNumber_TextBox.Text = _ViewModel.MeterageNumber;
                _ViewModel.LoadCompany();
                FillCompanyInfo();
            };
        }

        private void ViewModelPropertyChangedManager()
        {
            _ViewModel.PropertyChanged += (s, e) =>
            {
                switch (e.PropertyName)
                {
                    case nameof(_ViewModel.CurrentCompany):
                        FillCompanyInfo();
                        break;
                    case nameof(_ViewModel.CurrentMeterInfo):
                        FillMeterInfo();
                        break;
                }
            };
        }

        private void ControlEventManager()
        {
            _Companies_ComboBox.SelectionChanged += (s, e) =>
            {
                _ViewModel.SetCurrentCompaniesSelectedIndex(_Companies_ComboBox.SelectedIndex);
            };
            _MeterInfos_ComboBox.SelectionChanged += (s, e) =>
            {
                _ViewModel.SetCurrentMeterInfosSelectedIndex(_MeterInfos_ComboBox.SelectedIndex);
            };
            _Clear_Button.Click += (s, e) =>
            {
                _Companies_ComboBox.SelectedIndex = -1;
                _MeterInfos_ComboBox.SelectedIndex = -1;
                _ViewModel.Clear();
            };
            _Accept_Button.Click += (s, e) =>
            {
                _ViewModel.Accept();
            };
        }

        private void ViewModelCollectionManager()
        {
            _ViewModel.Companies.CollectionChanged += (s, e) =>
            {
                _Companies_ComboBox.Items.Clear();
                foreach (var company in _ViewModel.Companies)
                {
                    _Companies_ComboBox.Items.Add(company.Name);
                }
            };
        }

        private void FillCompanyInfo()
        {
            if(_ViewModel.CurrentCompany==null)
                return;
            _CompanyNumber_TextBox.Text = _ViewModel.CurrentCompany.Number;
            _CompanyAddress_TextBox.Text = _ViewModel.CurrentCompany.Address;
            _CompanyCharger_TextBox.Text = _ViewModel.CurrentCompany.Charger;
            _CompanyEmail_TextBox.Text = _ViewModel.CurrentCompany.Email;
            _CompanyMemo_TextBox.Text = _ViewModel.CurrentCompany.Memo;
            _CompanyName_TextBox.Text = _ViewModel.CurrentCompany.Name;
            _CompanyTelephone_TextBox.Text = _ViewModel.CurrentCompany.Telephone;
            if (_ViewModel.CurrentCompany.MeterInfos.Count > 0)
            {
                _MeterInfos_ComboBox.Items.Clear();
                foreach (var meterInfo in _ViewModel.CurrentCompany.MeterInfos)
                {
                    _MeterInfos_ComboBox.Items.Add(meterInfo.Name);
                }
                FillMeterInfo();
            }
        }

        private void FillMeterInfo()
        {
            if (_ViewModel.CurrentMeterInfo == null)
            {
                _MeterInfoManufacturer_TextBox.Text = string.Empty;
                _MeterInfoModelNumber_TextBox.Text = string.Empty;
                _MeterInfoName_TextBox.Text = string.Empty;
                _MeterInfoNumber_TextBox.Text = string.Empty;
            }
            else
            {
                _MeterInfoManufacturer_TextBox.Text = _ViewModel.CurrentMeterInfo.Manufacturer;
                _MeterInfoModelNumber_TextBox.Text = _ViewModel.CurrentMeterInfo.ModelNumber;
                _MeterInfoName_TextBox.Text = _ViewModel.CurrentMeterInfo.Name;
                _MeterInfoNumber_TextBox.Text = _ViewModel.CurrentMeterInfo.Number;
            }
        }
    }
}
