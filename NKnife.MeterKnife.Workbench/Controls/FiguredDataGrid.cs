using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Common.DataModels;
using NKnife.MeterKnife.Util;
using NLog;

namespace MeterKnife.Common.Winforms.Controls
{
    public partial class FiguredDataGrid : UserControl
    {
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();

        private FiguredData _FiguredData;

        public FiguredDataGrid()
        {
            InitializeComponent();
            _FiguredDataPropertyGrid.PropertySort = PropertySort.Categorized;
            _SampleRangeDropMenu.TextChanged += (s, e) => SetStandardDeviationRange();
            SetRangeDropDownButtonState();
        }

        public void BindFigureData(FiguredData figuredData)
        {
            _FiguredData = figuredData;
            _FiguredDataPropertyGrid.SelectedObject = figuredData;
            SetSampleMenuClick();
            SetStandardDeviationRange();
        }

        private void SetSampleMenuClick()
        {
            _1000MenuItem.Click += OnSampleMenuItemOnClick;
            _100MenuItem.Click += OnSampleMenuItemOnClick;
            _2000MenuItem.Click += OnSampleMenuItemOnClick;
            _200MenuItem.Click += OnSampleMenuItemOnClick;
            _500MenuItem.Click += OnSampleMenuItemOnClick;
            _50MenuItem.Click += OnSampleMenuItemOnClick;
        }

        private void OnSampleMenuItemOnClick(object s, EventArgs e)
        {
            SetSampleMenuChecked();
            var menu = (ToolStripMenuItem) s;
            menu.CheckState = CheckState.Checked;
            _SampleRangeDropMenu.Text = menu.Text;
        }

        private void SetSampleMenuChecked()
        {
            _1000MenuItem.CheckState = CheckState.Unchecked;
            _100MenuItem.CheckState = CheckState.Unchecked;
            _2000MenuItem.CheckState = CheckState.Unchecked;
            _200MenuItem.CheckState = CheckState.Unchecked;
            _500MenuItem.CheckState = CheckState.Unchecked;
            _50MenuItem.CheckState = CheckState.Unchecked;
        }

        public override void Refresh()
        {
            _FiguredDataPropertyGrid.Refresh();
            base.Refresh();
        }

        public void SetStripButtonState(bool isCollected)
        {
            _SampleRangeDropMenu.Enabled = !isCollected;
            _MeterRangeDropDownButton.Enabled = !isCollected;
        }

        protected void SetStandardDeviationRange()
        {
            var range = 100;
            if (!int.TryParse(_SampleRangeDropMenu.Text, out range))
            {
                _Logger.Warn(string.Format("{0}解析错误", _SampleRangeDropMenu.Text));
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
