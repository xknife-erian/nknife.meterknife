using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common;
using MeterKnife.Common.Base;
using MeterKnife.Common.Controls.Plots;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.EventParameters;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Tunnels;
using MeterKnife.Instruments.Dialog;
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

        private readonly FiguredData _FiguredData = new FiguredData();
        private readonly ScpiProtocolHandler _Handler = new ScpiProtocolHandler();
        private readonly IMeterKernel _MeterKernel = DI.Get<IMeterKernel>();
        private bool _IsDispose;

        private bool _OnCollect; //是否正在采集
        protected BaseParamPanel _Panel;

        protected FiguredDataPlot _DataPlot = new FiguredDataPlot();
        protected TemperatureDataPlot _TempPlot = new TemperatureDataPlot();
        protected TemperatureFeaturesPlot _TempFeaturesPlot = new TemperatureFeaturesPlot();
        protected TemperatureTrendPlot _TempTrendPlot = new TemperatureTrendPlot();
        protected StandardDeviationPlot _SdPlot = new StandardDeviationPlot();

        public DigitMultiMeterView()
        {
            InitializeComponent();
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

            _StandardDeviationRangeComboBox.TextUpdate += (s, e) => SetStandardDeviationRange();
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
            if (!int.TryParse(_StandardDeviationRangeComboBox.Text, out range))
            {
                _logger.Warn(string.Format("{0}解析错误", _StandardDeviationRangeComboBox.Text));
            }
            _FiguredData.StandardDeviation.SetRange(range);
        }

        public override void SetMeter(CarePort port, BaseMeter meter)
        {
            base.SetMeter(port, meter);
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

            _PhotoToolStripButton.Enabled = !isCollected;
            _ZoomInToolStripButton.Enabled = !isCollected;
            _ZoomOutToolStripButton.Enabled = !isCollected;

            if (isCollected)
                _FeaturesPage.Parent = null;
            else
                _FeaturesPage.Parent = _MainTabControl;

            _StopStripButton.Enabled = isCollected;
            _IntervalTextBox.ReadOnly = isCollected;
        }

        protected virtual void StartCollect()
        {
            _Handler.ProtocolRecevied += OnProtocolRecevied;
            _OnCollect = true;
            _MeterKernel.CollectBeginning(_Meter.GpibAddress, true);

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
            _Comm.Remove(_Port, _Handler);
            Dictionary<CarePort, List<int>> dic = DI.Get<IMeterKernel>().GpibDictionary;
            dic[_Port].Remove(_Meter.GpibAddress);
        }

        private void StopProtocolRecevied()
        {
            _OnCollect = false;
            Thread.Sleep(50);
            var kernel = DI.Get<IMeterKernel>();
            if (kernel != null && _Meter != null)
            {
                kernel.CollectBeginning(_Meter.GpibAddress, false);
            }
            _Handler.ProtocolRecevied -= OnProtocolRecevied;
        }

        private void SendRead(object obj)
        {
            int interval = 1000;
            this.ThreadSafeInvoke(() =>
            {
                string i = _IntervalTextBox.Text;
                int.TryParse(i, out interval);
            });
            ScpiCommandList cmdlist = GetInitCommands();
            foreach (ScpiCommand cmd in cmdlist)
            {
                if (cmd == null)
                    continue;
                byte[] cmdBytes = CareTalking.BuildCareTalking(_Meter.GpibAddress, cmd.Command, false).Generate();
                _Comm.Send(Port, cmdBytes);
                long i = interval;
                if (cmd.Interval > 0)
                    i = cmd.Interval;
                Thread.Sleep((int) i);
            }

            ScpiCommandList reads = GetCollectCommands();
            byte[] temp = CareTalking.TEMP().Generate();
            while (_OnCollect)
            {
                foreach (var read in reads)
                {
                    //_Comm.Send(Port, read);
                    Thread.Sleep(interval);
                }
                _Comm.Send(Port, temp);
                Thread.Sleep(300);
            }
        }

        protected virtual ScpiCommandList GetInitCommands()
        {
            if (_Panel != null && _Panel.ScpiCommands != null)
                return _Panel.ScpiCommands;
            return new ScpiCommandList();
        }

        protected virtual ScpiCommandList GetCollectCommands()
        {
            var list = new List<byte[]> {CareTalking.READ(_Meter.GpibAddress).Generate()};
            return null;
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
                progressDialog.SetTotalCount(_FiguredData.Count);
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

            if (talking.MainCommand == 0xAE)
            {
                string data = talking.Scpi;
                double yzl = 0;
                if (double.TryParse(data, out yzl))
                {
                    _FiguredData.AddTemperature(yzl);
                    this.ThreadSafeInvoke(() => _TempPlot.Update(_FiguredData));
                }
            }
            else
            {
                if ((talking.GpibAddress != _Meter.GpibAddress) || (talking.Scpi.Length < 6))
                    return;
                string data = talking.Scpi; //.Substring(1, saying.Content.Length - 6);
                _logger.Info(data);
                double yzl = 0;
                if (double.TryParse(data, out yzl))
                {
                    _FiguredData.Add(yzl);
                    this.ThreadSafeInvoke(() => _DataPlot.Update(_FiguredData));
                }
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