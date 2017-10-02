using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Interfaces.Measures;
using MeterKnife.Models;
using MeterKnife.ViewModels;
using NKnife.IoC;
using OxyPlot;

namespace MeterKnife.Views.Measures.Dialogs
{
    public partial class DataSeriesSettingDialog : NKnife.ControlKnife.SimpleForm
    {
        private MeasureViewModel _MeasureViewModel;

        public DataSeriesSettingDialog(MeasureViewModel measureViewModel)
        {
            _MeasureViewModel = measureViewModel;
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
    }
}
