using System.Drawing;
using System.Windows.Forms;
using NKnife.MeterKnife.Plots.Util;
using NKnife.Win.Forms;

namespace NKnife.MeterKnife.Plots.Dialogs
{
    public partial class DataSeriesEditorDialog : SimpleForm
    {
        public DataSeriesEditorDialog()
        {
            InitializeComponent();
            _LineColor.Color = Color.Yellow;

            // IMeasureService measureService = DI.Get<IMeasureService>();
            // foreach (var e in measureService.Exhibits)
            // {
            //     _ExhibitsComboBox.Items.Add(e);
            // }
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

        private PlotSeriesStyleSolution.ExhibitSeriesStyle _seriesStyle;

        public PlotSeriesStyleSolution.ExhibitSeriesStyle SeriesStyle
        {
            get
            {
                var s = new PlotSeriesStyle();
                s.Color = _LineColor.Color;
                s.Thickness = (double) _ThicknessNumericUpDown.Value;
                s.SeriesLineStyle = (PlotSeriesStyle.LineStyleWrap) _LineStyleComboBox.SelectedItem;
                s.Offset = (double) _OffsetNumericUpDown.Value;
                // if (_seriesStyle != null) //当修改一个Style时
                //     _seriesStyle.SeriesStyle = s;
                // else//当新建一个Style时
                //     _seriesStyle = new PlotSeriesStyleSolution.ExhibitSeriesStyle((ExhibitBase) _ExhibitsComboBox.SelectedItem, s);
                return _seriesStyle;
            }
            set
            {
                //当进入该属性的设置时一般是修改一个Style
                //设置Combox的选择项
                foreach (var item in _ExhibitsComboBox.Items)
                {
                    // var exhibit = item as ExhibitBase;
                    // if (exhibit == null)
                    //     continue;
                    // if (exhibit.Equals(value.Exhibit))
                    // {
                    //     _ExhibitsComboBox.SelectedItem = exhibit;
                    //     _ExhibitsComboBox.Enabled = false;//被测物不可修改
                    //     break;
                    // }
                }
                _LineStyleComboBox.SelectedItem = value.SeriesStyle.SeriesLineStyle;
                _ThicknessNumericUpDown.Value = (decimal) value.SeriesStyle.Thickness;
                _OffsetNumericUpDown.Value = (decimal) value.SeriesStyle.Offset;
                _LineColor.Color = value.SeriesStyle.Color;
                _MainGroupBox.Text = $"数据线样式设置({value.Exhibit})";
                _seriesStyle = value;
            }
        }

        /// <summary>
        /// 当新建样式时，已被选择过的数据线，不再列表中出现。
        /// </summary>
        /// <param name="solution">样式方案(被测物列表)</param>
        public void IgnoreExistsExhibits(PlotSeriesStyleSolution solution)
        {
            foreach (var style in solution.Styles)
            {
                var i = 0;
                while (i < _ExhibitsComboBox.Items.Count)
                {
                    // var exhibit = _ExhibitsComboBox.Items[i] as ExhibitBase;
                    // if (style.Exhibit.Equals(exhibit))
                    // {
                    //     _ExhibitsComboBox.Items.RemoveAt(i);
                    //     break;
                    // }
                    i++;
                }
            }
        }
    }
}