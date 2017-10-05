using System.Drawing;
using System.Windows.Forms;
using MeterKnife.Base;
using MeterKnife.Interfaces.Measures;
using MeterKnife.Models;
using NKnife.ControlKnife;
using NKnife.IoC;

namespace MeterKnife.Views.Measures.Series
{
    public partial class DataSeriesEditorDialog : SimpleForm
    {
        private PlotSeriesStyleSolution.ExhibitSeriesStyle _Style;

        public DataSeriesEditorDialog()
        {
            InitializeComponent();
            _LineColor.Color = Color.Yellow;
            _MarkerFillColor.Color = Color.Red;
            _MarkerStrokeColor.Color = Color.Red;

            IMeasureService measureService = DI.Get<IMeasureService>();

            foreach (var exhibit in measureService.Exhibits)
            {
                _ExhibitsComboBox.Items.Add(exhibit);
            }
            if (_ExhibitsComboBox.Items.Count > 0)
            {
                _ExhibitsComboBox.SelectedIndex = 0;
            }

            foreach (var lineStyle in PlotSeriesStyle.GetAllLineStyles())
            {
                _LineStyleComboBox.Items.Add(lineStyle);
            }
            _LineStyleComboBox.SelectedIndex = 0;

            _AcceptButton.Click += (s, e) =>
            {
                DialogResult = DialogResult.OK;
                Close();
            };
            _CancelButton.Click += (s, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };
        }

        public PlotSeriesStyleSolution.ExhibitSeriesStyle SeriesStyle
        {
            get
            {
                var s = new PlotSeriesStyle();
                s.Color = _LineColor.Color;
                s.MarkerStrokeColor = _MarkerStrokeColor.Color;
                s.MarkerFillColor = _MarkerFillColor.Color;
                s.Thickness = (double) _ThicknessNumericUpDown.Value;
                s.SeriesLineStyle = (PlotSeriesStyle.LineStyleWrap) _LineStyleComboBox.SelectedItem;
                var style = new PlotSeriesStyleSolution.ExhibitSeriesStyle((ExhibitBase) _ExhibitsComboBox.SelectedItem, s);
                return style;
            }
            set
            {
                //设置Combox的选择项
                foreach (var item in _ExhibitsComboBox.Items)
                {
                    var exhibit = item as ExhibitBase;
                    if (exhibit == null)
                        continue;
                    if (exhibit.Equals(value.Exhibit))
                    {
                        _ExhibitsComboBox.SelectedItem = exhibit;
                        break;
                    }
                }
                _LineStyleComboBox.SelectedItem = value.SeriesStyle.SeriesLineStyle;
                _ThicknessNumericUpDown.Value = (decimal) value.SeriesStyle.Thickness;
                _LineColor.Color = value.SeriesStyle.Color;
                _MarkerFillColor.Color = value.SeriesStyle.MarkerFillColor;
                _MarkerStrokeColor.Color = value.SeriesStyle.MarkerStrokeColor;
            }
        }
    }
}