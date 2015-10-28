using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common;
using MeterKnife.Common.Base;
using MeterKnife.Common.Controls.Dialogs;
using MeterKnife.Common.Controls.Plots;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Enums;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Tunnels;
using MeterKnife.Instruments.Dialog;
using MeterKnife.Scpis;
using NKnife.Events;
using NKnife.IoC;
using ScpiKnife;

namespace MeterKnife.Instruments
{
    public partial class DigitMultiMeterView : MeterView
    {
        private static readonly ILog _logger = LogManager.GetLogger<DigitMultiMeterView>();
        protected readonly BaseCareCommunicationService _Comm = DI.Get<BaseCareCommunicationService>();

        private readonly FiguredData _FiguredData = new FiguredData();
        private readonly ScpiProtocolHandler _Handler = new ScpiProtocolHandler();
        private readonly IMeterKernel _MeterKernel = DI.Get<IMeterKernel>();
        private readonly CustomerScpiSubjectPanel _ScpiCommandPanel = new CustomerScpiSubjectPanel();

        protected FiguredDataPlot _DataPlot = new FiguredDataPlot();

        private bool _IsDispose;

        //protected StandardNormalDistributionPlot _SdPlot = new StandardNormalDistributionPlot();
        protected StandardDeviationPlot _SdPlot = new StandardDeviationPlot();
        protected TemperatureFeaturesPlot _TempFeaturesPlot = new TemperatureFeaturesPlot();
        protected TemperatureDataPlot _TempPlot = new TemperatureDataPlot();
        protected TemperatureTrendPlot _TempTrendPlot = new TemperatureTrendPlot();

        public DigitMultiMeterView()
        {
            InitializeComponent();
            _FiguredDataPropertyGrid.PropertySort = PropertySort.Categorized;
            _ScpiCommandPanel.Dock = DockStyle.Fill;
            _LeftSplitContainer.Panel2.Padding = new Padding(3, 2, 3, 2);
            _LeftSplitContainer.Panel2.Controls.Add(_ScpiCommandPanel);

            _RealtimePlotSplitContainer.Panel1.Controls.Add(_DataPlot);
            _RealtimePlotSplitContainer.Panel2.Controls.Add(_TempPlot);
            _TempFeaturesPanel.Controls.Add(_TempFeaturesPlot);
            _TempTrendPanel.Controls.Add(_TempTrendPlot);
            _SdPanel.Controls.Add(_SdPlot);

            SetStripButtonState(false);
            SetFiguredDataGrid();
            SetStandardDeviationRange();
            SetRangeDropDownButtonState();
            _MeterKernel.Collected += (s, e) =>
            {
                if (e.GpibAddress == _Meter.GpibAddress)
                    SetStripButtonState(e.IsCollected);
            };

            _SampleRangeComboBox.TextChanged += (s, e) => SetStandardDeviationRange();
            _FiguredData.ReceviedCollectData += (sender, args) => this.ThreadSafeInvoke(() =>
            {
                _FiguredDataGridView.DataSource = null;
                _FiguredDataGridView.DataSource = _FiguredData.DataSet.Tables[1];
            });
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!_IsSaved)
            {
                var sr = MessageBox.Show(this, "有数据未保存，是否仍然关闭？", "未保存", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (sr == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
            _IsDispose = true;
            base.OnFormClosing(e);

            StopProtocolRecevied();
            _Comm.Remove(_CarePort, _Handler);
            var dic = DI.Get<IMeterKernel>().GpibDictionary;
            dic[_CarePort].Remove(_Meter.GpibAddress);
        }

        public override void SetMeter(CommPort port, BaseMeter meter)
        {
            base.SetMeter(port, meter);
            _ScpiCommandPanel.GpibAddress = meter.GpibAddress;
            _Comm.Bind(port, _Handler);
            _FiguredData.Meter = _Meter;
            _FiguredDataPropertyGrid.SelectedObject = _FiguredData;
            _logger.Info("面板初始化仪器完成..");
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

        protected void SetFiguredDataGrid()
        {
            _FiguredDataGridView.DataSource = _FiguredData.DataSet.Tables[1];
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

        protected void SetStripButtonState(bool isCollected)
        {
            if (_IsDispose)
                return;
            _StartStripButton.Enabled = !isCollected;
            _ExportStripButton.Enabled = !isCollected;
            _SaveStripButton.Enabled = !isCollected;
            _ClearDataToolStripButton.Enabled = !isCollected;
            _SampleRangeComboBox.Enabled = !isCollected;
            _MeterRangeDropDownButton.Enabled = !isCollected;

            _PhotoToolStripButton.Enabled = !isCollected;
            _ZoomInToolStripButton.Enabled = !isCollected;
            _ZoomOutToolStripButton.Enabled = !isCollected;

            _FeaturesPage.Parent = isCollected ? null : _MainTabControl;
            _StopStripButton.Enabled = isCollected;
        }

        protected virtual ScpiCommandQueue.Item[] GetInitCommands()
        {
            var commands = _ScpiCommandPanel.GetInitCommands();
            return commands.Value;
        }

        protected virtual KeyValuePair<string, ScpiCommandQueue.Item[]> GetCollectCommands()
        {
            var commands = _ScpiCommandPanel.GetCollectCommands();
            return commands;
        }

        private void PlotsClear()
        {
            _FiguredData.Clear();

            _DataPlot.Clear();
            _TempPlot.Clear();
            _TempFeaturesPlot.Clear();
            _TempTrendPlot.Clear();
            _SdPlot.Clear();

            this.ThreadSafeInvoke(() => _FiguredDataPropertyGrid.Refresh());
        }

        #region 按键响应

        private void _StartStripButton_Click(object sender, EventArgs e)
        {
            if (_FiguredData.HasData)
            {
                var rs = MessageBox.Show(this, "是否延续采集?\r\n点击“是”延续采集数据并记录；\r\n点击“否”将清空原有数据重新开始记录。",
                    "数据采集", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (rs == DialogResult.No)
                {
                    _FiguredData.Clear();
                    PlotsClear();
                }
            }
            _IsSaved = false;
            StartCollect();
        }

        private void _StopStripButton_Click(object sender, EventArgs e)
        {
            StopCollect();

            _TempFeaturesPlot.Clear();
            _TempTrendPlot.Clear();
            _SdPlot.Clear();

            _TempFeaturesPlot.Update(_FiguredData);
            _TempTrendPlot.Update(_FiguredData);
            _SdPlot.Update(_FiguredData);
        }

        private void _SaveStripButton_Click(object sender, EventArgs e)
        {
            var start = (DateTime) _FiguredData.DataSet.Tables[1].Rows[0][0];
            var name = string.Format("{0}-{1}.{2}", start.ToString("yyyyMMddHHmmss"), _Meter.Name, "s3db");
            var full = Path.Combine(DI.Get<MeterKnifeUserData>().GetValue(MeterKnifeUserData.DATA_PATH, string.Empty),
                name);
            if (_FiguredData.Save(full))
            {
                MessageBox.Show(string.Format("数据文件已保存:\r\n{0}", full));
                _IsSaved = true;
            }
        }

        private void _ExportStripButton_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var dir = dialog.SelectedPath;
                DateTime start;
                if (_FiguredData.DataSet.Tables[1].Rows.Count > 0)
                    start = (DateTime) _FiguredData.DataSet.Tables[1].Rows[0][0];
                else
                    start = DateTime.Now;
                var name = string.Format("{0}-{1}.{2}", start.ToString("yyyyMMddHHmmss"), _Meter.Name, "xls");
                var full = Path.Combine(dir, name);

                var progressDialog = new ExportProgressDialog();
                progressDialog.SetPath(full);
                progressDialog.SetFigureData(_FiguredData);
                Cursor = Cursors.WaitCursor;
                progressDialog.ShowDialog(this);
                Cursor = Cursors.Default;
                _IsSaved = true;
            }
        }

        private void _ClearDataToolStripButton_Click(object sender, EventArgs e)
        {
            PlotsClear();
        }

        private void _FilterToolStripButton_Click(object sender, EventArgs e)
        {
            var dialog = new FilterSettingDialog();
            dialog.SetFilter(_FiguredData.Filter);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                _FiguredData.Filter.Update(dialog.FiguredDataFilter);
                _logger.Debug(string.Format("过滤参数：倍数{0},统计{1},保存{2}", dialog.FiguredDataFilter.Multiple, dialog.FiguredDataFilter.InStatistical, dialog.FiguredDataFilter.IsSave));
            }
        }

        private void _PhotoToolStripButton_Click(object sender, EventArgs e)
        {
        }

        private void _ZoomInToolStripButton_Click(object sender, EventArgs e)
        {
        }

        private void _ZoomOutToolStripButton_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #region 通讯相关

        protected virtual void StartCollect()
        {
            _Handler.ProtocolRecevied += OnProtocolRecevied;
            _MeterKernel.UpdateCollectState(_CarePort, _Meter.GpibAddress, true, _ScpiCommandPanel.ScpiSubjectKey);

            var thread = new Thread(SendRead);
            thread.Start();
        }

        protected virtual void StopCollect()
        {
            StopProtocolRecevied();
        }

        private void StopProtocolRecevied()
        {
            Thread.Sleep(50);
            var kernel = DI.Get<IMeterKernel>();
            if (kernel != null && _Meter != null)
            {
                kernel.UpdateCollectState(_CarePort, _Meter.GpibAddress, false, _ScpiCommandPanel.ScpiSubjectKey);
            }
            _Handler.ProtocolRecevied -= OnProtocolRecevied;
        }

        private void SendRead(object obj)
        {
            _Comm.SendCommands(Port, GetInitCommands());
            var cmd = GetCollectCommands();
            _Comm.SendLoopCommands(Port, cmd.Key, cmd.Value);
        }

        private void OnProtocolRecevied(object sender, EventArgs<CareTalking> e)
        {
            var talking = e.Item;
            if ((talking.GpibAddress != _Meter.GpibAddress) || (talking.Scpi.Length < 6))
                return;
            var data = talking.Scpi;
            _logger.Debug(string.Format("Recevied:{0}", data));
            double yzl = 0;
            if (double.TryParse(data, out yzl))
            {
                if (_FiguredData.Add(yzl))
                {
                    this.ThreadSafeInvoke(() =>
                    {
                        if (uint.Parse(_FiguredData.Count) <= 1)
                            return;
                        _DataPlot.Update(_FiguredData);
                        _TempPlot.Update(_FiguredData);
                    });
                }
            }
            this.ThreadSafeInvoke(() => _FiguredDataPropertyGrid.Refresh());
        }

        #endregion
    }
}