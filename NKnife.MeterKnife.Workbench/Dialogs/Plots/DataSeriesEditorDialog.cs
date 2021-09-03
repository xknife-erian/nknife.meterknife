using System.Drawing;
using System.Windows.Forms;
using NKnife.MeterKnife.ViewModels.Plots;
using NKnife.Win.Forms;

namespace NKnife.MeterKnife.Workbench.Dialogs.Plots
{
    public partial class DataSeriesEditorDialog : SimpleForm
    {
        public DataSeriesEditorDialog()
        {
            InitializeComponent();
            _LineColor.Color = Color.Yellow;

            // IAcquisitionService antCollectService = DI.Get<IAcquisitionService>();
            // foreach (var e in antCollectService.Exhibits)
            // {
            //     _ExhibitsComboBox.Items.Add(e);
            // }
            if (_ExhibitsComboBox.Items.Count > 0)
            {
                _ExhibitsComboBox.SelectedIndex = 0;
            }

            foreach (var lineStyle in DUTSeriesStyle.GetAllLineStyles())
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

        private DUTSeriesStyle _seriesStyle;

        public DUTSeriesStyle SeriesStyle
        {
            get
            {
                var s = new DUTSeriesStyle();
                s.Color = _LineColor.Color;
                s.Thickness = (double) _ThicknessNumericUpDown.Value;
                //s.SeriesLineStyle = (SeriesStyle) _LineStyleComboBox.SelectedItem;
                s.Offset = (double) _OffsetNumericUpDown.Value;
                // if (_seriesStyle != null) //当修改一个Style时
                //     _seriesStyle.SeriesStyle = s;
                // else//当新建一个Style时
                //     _seriesStyle = new DUTSeriesStyleSolution.DUTSeriesStyle((ExhibitBase) _ExhibitsComboBox.SelectedItem, s);
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
                    // if (exhibit.Equals(value.DUT))
                    // {
                    //     _ExhibitsComboBox.SelectedItem = exhibit;
                    //     _ExhibitsComboBox.Enabled = false;//被测物不可修改
                    //     break;
                    // }
                }
                //_LineStyleComboBox.SelectedItem = value.SeriesStyle.SeriesLineStyle;
                _ThicknessNumericUpDown.Value = (decimal) value.Thickness;
                _OffsetNumericUpDown.Value = (decimal) value.Offset;
                _LineColor.Color = value.Color;
                _MainGroupBox.Text = $"数据线样式设置({value.DUT})";
                _seriesStyle = value;
            }
        }

        /// <summary>
        /// 当新建样式时，已被选择过的数据线，不再列表中出现。
        /// </summary>
        /// <param name="solution">样式方案(被测物列表)</param>
        public void IgnoreExistsExhibits(DUTSeriesStyleSolution solution)
        {
            foreach (var style in solution)
            {
                var i = 0;
                while (i < _ExhibitsComboBox.Items.Count)
                {
                    // var exhibit = _ExhibitsComboBox.Items[i] as ExhibitBase;
                    // if (style.DUT.Equals(exhibit))
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