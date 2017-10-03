using System.Collections.Generic;
using MeterKnife.Base;
using MeterKnife.Interfaces.Measures;
using MeterKnife.Models;
using MeterKnife.ViewModels;
using NKnife.IoC;

namespace MeterKnife.Views.Measures
{
    public partial class DataSeriesSettingDialog : NKnife.ControlKnife.SimpleForm
    {
        public DataSeriesSettingDialog()
        {
            InitializeComponent();

            IMeasureService measureService = DI.Get<IMeasureService>();

            foreach (var exhibit in measureService.Exhibits)
            {
                _ExhibitsComboBox.Items.Add(exhibit);
            }
            if(_ExhibitsComboBox.Items.Count>0)
            _ExhibitsComboBox.SelectedIndex = 0;

            foreach (var lineStyle in PlotSeriesStyle.GetAllLineStyles())
            {
                _LineStyleComboBox.Items.Add(lineStyle);
            }
            _LineStyleComboBox.SelectedIndex = 0;
        }

        public void SetExhibits(List<ExhibitBase> exhibits)
        {
            throw new System.NotImplementedException();
        }
    }
}
