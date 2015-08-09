using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Common.Logging;
using MeterKnife.Common;
using MeterKnife.Common.Base;
using MeterKnife.Common.Controls.Plots;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.EventParameters;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Tunnels;
using MeterKnife.Instruments.Dialog;
using MeterKnife.Scpis;
using NKnife.Events;
using NKnife.IoC;
using NKnife.Utility;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using ScpiKnife;

namespace MeterKnife.Instruments
{
    public partial class DigitMultiMeterView : MeterView
    {
        private static readonly ILog _logger = LogManager.GetLogger<DigitMultiMeterView>();

        protected readonly BaseCareCommunicationService _Comm = DI.Get<BaseCareCommunicationService>();
        private readonly CustomerScpiSubjectPanel _ScpiCommandPanel = new CustomerScpiSubjectPanel();

        private readonly FiguredData _FiguredData = new FiguredData();
        private readonly ScpiProtocolHandler _Handler = new ScpiProtocolHandler();
        private readonly IMeterKernel _MeterKernel = DI.Get<IMeterKernel>();
        private bool _IsDispose;

        protected FiguredDataPlot _DataPlot = new FiguredDataPlot();
        protected TemperatureDataPlot _TempPlot = new TemperatureDataPlot();
        protected TemperatureFeaturesPlot _TempFeaturesPlot = new TemperatureFeaturesPlot();
        protected TemperatureTrendPlot _TempTrendPlot = new TemperatureTrendPlot();
        protected StandardNormalDistributionPlot _SdPlot = new StandardNormalDistributionPlot();

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

        protected void SetFiguredDataGrid()
        {
            _FiguredDataGridView.DataSource = _FiguredData.DataSet.Tables[1];
        }

        protected void SetStandardDeviationRange()
        {
            int range = 100;
            if (!int.TryParse(_SampleRangeComboBox.Text, out range))
            {
                _logger.Warn(string.Format("{0}解析错误", _SampleRangeComboBox.Text));
            }
            if (range < 10)
                return;
            _FiguredData.SetRange(range);
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

        protected void SetStripButtonState(bool isCollected)
        {
            if (_IsDispose)
                return;
            _StartStripButton.Enabled = !isCollected;
            _ExportStripButton.Enabled = !isCollected;
            _SaveStripButton.Enabled = !isCollected;
            _ClearDataToolStripButton.Enabled = !isCollected;
            _SampleRangeComboBox.Enabled = !isCollected;

            _PhotoToolStripButton.Enabled = !isCollected;
            _ZoomInToolStripButton.Enabled = !isCollected;
            _ZoomOutToolStripButton.Enabled = !isCollected;

            _FeaturesPage.Parent = isCollected ? null : _MainTabControl;
            _StopStripButton.Enabled = isCollected;
        }

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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _IsDispose = true;
            base.OnFormClosing(e);
            StopProtocolRecevied();
            _Comm.Remove(_CarePort, _Handler);
            Dictionary<CommPort, List<int>> dic = DI.Get<IMeterKernel>().GpibDictionary;
            dic[_CarePort].Remove(_Meter.GpibAddress);
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

        private void _StartStripButton_Click(object sender, EventArgs e)
        {
            if (_FiguredData.HasData)
            {
                DialogResult rs = MessageBox.Show(this, "是否延续采集?\r\n点击“是”延续采集数据并记录；\r\n点击“否”将清空原有数据重新开始记录。",
                    "数据采集", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (rs == DialogResult.No)
                {
                    _FiguredData.Clear();
                    PlotsClear();
                }
            }
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
            string name = string.Format("{0}-{1}.{2}", start.ToString("yyyyMMddHHmmss"), _Meter.Name, "s3db");
            string full = Path.Combine(DI.Get<MeterKnifeUserData>().GetValue(MeterKnifeUserData.DATA_PATH, string.Empty), name);
            if (_FiguredData.Save(full))
            {
                MessageBox.Show(string.Format("数据文件已保存:\r\n{0}", full));
            }
        }

        private void _ExportStripButton_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                string dir = dialog.SelectedPath;
                var start = (DateTime) _FiguredData.DataSet.Tables[1].Rows[0][0];
                string name = string.Format("{0}-{1}.{2}", start.ToString("yyyyMMddHHmmss"), _Meter.Name, "xls");
                string full = Path.Combine(dir, name);
                var progressDialog = new ExportProgressDialog();
                ExportRowCountChanged += (s, ex) => progressDialog.SetCurrentCount(ex.Item);
                progressDialog.SetTotalCount(uint.Parse(_FiguredData.Count));
                progressDialog.SetPath(full);
                progressDialog.Show(this);
                progressDialog.Shown += (s, args) =>
                {
                    _FiguredData.Export(full, AddRowCount);
                    progressDialog.SetFinished();
                };
            }
        }

        private void _ClearDataToolStripButton_Click(object sender, EventArgs e)
        {
            PlotsClear();
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

        private void _PhotoToolStripButton_Click(object sender, EventArgs e)
        {
        }

        private void _ZoomInToolStripButton_Click(object sender, EventArgs e)
        {
        }

        private void _ZoomOutToolStripButton_Click(object sender, EventArgs e)
        {
        }

        private void OnProtocolRecevied(object sender, EventArgs<CareTalking> e)
        {
            CareTalking talking = e.Item;
            if ((talking.GpibAddress != _Meter.GpibAddress) || (talking.Scpi.Length < 6))
                return;
            string data = talking.Scpi;
            _logger.Debug(string.Format("Recevied:{0}", data));
            double yzl = 0;
            if (double.TryParse(data, out yzl))
            {
                _FiguredData.Add(yzl);
                this.ThreadSafeInvoke(() =>
                {
                    if (uint.Parse(_FiguredData.Count) <= 1)
                        return;
                    _DataPlot.Update(_FiguredData);
                    _TempPlot.Update(_FiguredData);
                });
            }
            this.ThreadSafeInvoke(() => _FiguredDataPropertyGrid.Refresh());
        }

        private void AddRowCount(int rowCount)
        {
            OnExportRowCountChanged(new EventArgs<int>(rowCount));
        }

        private event EventHandler<EventArgs<int>> ExportRowCountChanged;

        private void OnExportRowCountChanged(EventArgs<int> e)
        {
            EventHandler<EventArgs<int>> handler = ExportRowCountChanged;
            if (handler != null)
                handler(this, e);
        }
    }
}