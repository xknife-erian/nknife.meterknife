using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Enums;

namespace MeterKnife.Common.Winforms.Controls
{
    public partial class FiguredDataGrid : UserControl
    {
        private static readonly ILog _logger = LogManager.GetLogger<FiguredDataGrid>();

        private FiguredData _FiguredData;

        public FiguredDataGrid()
        {
            InitializeComponent();
            _FiguredDataPropertyGrid.PropertySort = PropertySort.Categorized;
            _SampleRangeComboBox.TextChanged += (s, e) => SetStandardDeviationRange();
            SetRangeDropDownButtonState();
        }

        public void BindFigureData(FiguredData figuredData)
        {
            _FiguredData = figuredData;
            _FiguredDataPropertyGrid.SelectedObject = figuredData;
            SetStandardDeviationRange();
        }

        public override void Refresh()
        {
            _FiguredDataPropertyGrid.Refresh();
            base.Refresh();
        }

        protected void SetStripButtonState(bool isCollected)
        {
            _SampleRangeComboBox.Enabled = !isCollected;
            _MeterRangeDropDownButton.Enabled = !isCollected;
        }

        protected void SetStandardDeviationRange()
        {
            var range = 100;
            if (!int.TryParse(_SampleRangeComboBox.Text, out range))
            {
                _logger.Warn(string.Format("{0}解析错误", _SampleRangeComboBox.Text));
            }
            if (range < 10)
                return;
            _FiguredData.SetRange(range);
        }

        protected void SetRangeDropDownButtonState()
        {
            autoToolStripMenuItem.Click += (s, e) =>
            {
                _MeterRangeDropDownButton.Text = autoToolStripMenuItem.Text;
                _FiguredData.MeterRange = MeterRange.None;
            };
            x0001ToolStripMenuItem.Click += (s, e) =>
            {
                _MeterRangeDropDownButton.Text = x0001ToolStripMenuItem.Text;
                _FiguredData.MeterRange = MeterRange.X0001;
            };
            x001ToolStripMenuItem.Click += (s, e) =>
            {
                _MeterRangeDropDownButton.Text = x001ToolStripMenuItem.Text;
                _FiguredData.MeterRange = MeterRange.X001;
            };
            x01ToolStripMenuItem.Click += (s, e) =>
            {
                _MeterRangeDropDownButton.Text = x01ToolStripMenuItem.Text;
                _FiguredData.MeterRange = MeterRange.X01;
            };
            x1ToolStripMenuItem.Click += (s, e) =>
            {
                _MeterRangeDropDownButton.Text = x1ToolStripMenuItem.Text;
                _FiguredData.MeterRange = MeterRange.X1;
            };
            x10ToolStripMenuItem.Click += (s, e) =>
            {
                _MeterRangeDropDownButton.Text = x10ToolStripMenuItem.Text;
                _FiguredData.MeterRange = MeterRange.X10;
            };
            x100ToolStripMenuItem.Click += (s, e) =>
            {
                _MeterRangeDropDownButton.Text = x100ToolStripMenuItem.Text;
                _FiguredData.MeterRange = MeterRange.X100;
            };
            x1KToolStripMenuItem.Click += (s, e) =>
            {
                _MeterRangeDropDownButton.Text = x1KToolStripMenuItem.Text;
                _FiguredData.MeterRange = MeterRange.X1K;
            };
            x10KToolStripMenuItem.Click += (s, e) =>
            {
                _MeterRangeDropDownButton.Text = x10KToolStripMenuItem.Text;
                _FiguredData.MeterRange = MeterRange.X10K;
            };
            x100KToolStripMenuItem.Click += (s, e) =>
            {
                _MeterRangeDropDownButton.Text = x100KToolStripMenuItem.Text;
                _FiguredData.MeterRange = MeterRange.X100K;
            };
            x1MToolStripMenuItem.Click += (s, e) =>
            {
                _MeterRangeDropDownButton.Text = x1MToolStripMenuItem.Text;
                _FiguredData.MeterRange = MeterRange.X1M;
            };
            x10MToolStripMenuItem.Click += (s, e) =>
            {
                _MeterRangeDropDownButton.Text = x10MToolStripMenuItem.Text;
                _FiguredData.MeterRange = MeterRange.X10M;
            };
            x100MToolStripMenuItem.Click += (s, e) =>
            {
                _MeterRangeDropDownButton.Text = x100MToolStripMenuItem.Text;
                _FiguredData.MeterRange = MeterRange.X100M;
            };
        }

    }
}
