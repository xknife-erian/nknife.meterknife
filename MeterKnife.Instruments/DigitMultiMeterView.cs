using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Tunnels;
using MeterKnife.Common.Winforms.Dialogs;
using MeterKnife.Common.Winforms.Plots;
using MeterKnife.Scpis;
using NKnife.Events;
using NKnife.Scpi;

namespace MeterKnife.Instruments
{
    public partial class DigitMultiMeterView : MeterView
    {
        private static readonly ILog _Logger = LogManager.GetLogger<DigitMultiMeterView>();

        protected readonly BaseCareCommunicationService _comm;

        private readonly FiguredData _figuredData;
        private readonly ScpiProtocolHandler _handler = new ScpiProtocolHandler();

        protected readonly FiguredDataAndTemperaturePlot _mainPlot = new FiguredDataAndTemperaturePlot();
        private readonly IMeterKernel _meterKernel;
        private readonly CustomerScpiSubjectPanel _scpiCommandPanel;

        protected readonly StandardDeviationPlot _sdPlot = new StandardDeviationPlot();
        protected readonly TemperatureFeaturesPlot _tempFeaturesPlot = new TemperatureFeaturesPlot();
        protected readonly TemperatureTrendPlot _tempTrendPlot = new TemperatureTrendPlot();
        private bool _isDispose;

        public DigitMultiMeterView(BaseCareCommunicationService comm, 
            IMeterKernel meterKernel, 
            FiguredData figuredData, 
            CustomerScpiSubjectPanel scpiCommandPanel):base(meterKernel)
        {
            _comm = comm;
            _meterKernel = meterKernel;
            _figuredData = figuredData;
            _scpiCommandPanel = scpiCommandPanel;
            InitializeComponent();

            _scpiCommandPanel.Dock = DockStyle.Fill;
            _LeftSplitContainer.Panel2.Padding = new Padding(3, 2, 3, 2);
            _LeftSplitContainer.Panel2.Controls.Add(_scpiCommandPanel);

            _PlotPage.Controls.Add(_mainPlot);
            _TempFeaturesPanel.Controls.Add(_tempFeaturesPlot);
            _TempTrendPanel.Controls.Add(_tempTrendPlot);
            _SdPanel.Controls.Add(_sdPlot);

            SetStripButtonState(false);
            SetFiguredDataGrid();
            _meterKernel.Collected += (s, e) =>
            {
                if (e.GpibAddress == _Meter.GpibAddress)
                    SetStripButtonState(e.IsCollected);
            };

            _figuredData.ReceviedCollectData += (sender, args) => this.ThreadSafeInvoke(() =>
            {
                _FiguredDataGridView.DataSource = null;
                _FiguredDataGridView.DataSource = _figuredData.DataSet.Tables[1];
            });

            //TODO:未完成功能
            _SaveStripButton.Visible = false;
            _PrintStripButton.Visible = false;
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

            _isDispose = true;
            base.OnFormClosing(e);

            StopProtocolRecevied();
            _comm.Remove(_CarePort, _handler);
            var dic = _meterKernel.GpibDictionary;
            dic[_CarePort].Remove(_Meter.GpibAddress);
        }

        public override void SetMeter(CommPort port, BaseMeter meter)
        {
            base.SetMeter(port, meter);
            _scpiCommandPanel.GpibAddress = meter.GpibAddress;
            _comm.Bind(port, _handler);
            _figuredData.Meter = _Meter;
            _FiguredDataPropertyGrid.BindFigureData(_figuredData);

            if (!_comm.IsInitialized) _comm.Start(port);

            _Logger.Info("面板初始化仪器完成..");
        }

        protected void SetFiguredDataGrid()
        {
            _FiguredDataGridView.DataSource = _figuredData.DataSet.Tables[1];
        }

        protected void SetStripButtonState(bool isCollected)
        {
            if (_isDispose)
                return;
            _StartStripButton.Enabled = !isCollected;
            _ExportStripButton.Enabled = !isCollected;
            _SaveStripButton.Enabled = !isCollected;
            _ClearDataToolStripButton.Enabled = !isCollected;

            _FeaturesPage.Parent = isCollected ? null : _MainTabControl;
            _StopStripButton.Enabled = isCollected;
            _FiguredDataPropertyGrid.SetStripButtonState(isCollected);
        }

        protected virtual ScpiCommandQueue.Item[] GetInitCommands()
        {
            var commands = _scpiCommandPanel.GetInitCommands();
            return commands.Value;
        }

        protected virtual KeyValuePair<string, ScpiCommandQueue.Item[]> GetCollectCommands()
        {
            var commands = _scpiCommandPanel.GetCollectCommands();
            return commands;
        }

        private void PlotsClear()
        {
            _figuredData.Clear();

            _mainPlot.Clear();
            _tempFeaturesPlot.Clear();
            _tempTrendPlot.Clear();
            _sdPlot.Clear();

            this.ThreadSafeInvoke(() => _FiguredDataPropertyGrid.Refresh());
        }

        #region 按键响应

        private void _StartStripButton_Click(object sender, EventArgs e)
        {
            if (_figuredData.HasData)
            {
                var rs = MessageBox.Show(this, "是否延续采集?\r\n点击“是”延续采集数据并记录；\r\n点击“否”将清空原有数据重新开始记录。",
                    "数据采集", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (rs == DialogResult.No) PlotsClear();
            }

            _IsSaved = false;
            StartCollect();
        }

        private void _StopStripButton_Click(object sender, EventArgs e)
        {
            StopCollect();

            _tempFeaturesPlot.Clear();
            _tempTrendPlot.Clear();
            _sdPlot.Clear();

            _tempFeaturesPlot.Update(_figuredData);
            _tempTrendPlot.Update(_figuredData);
            _sdPlot.Update(_figuredData);
        }

        private void _SaveStripButton_Click(object sender, EventArgs e)
        {
//            var start = (DateTime) _FiguredData.DataSet.Tables[1].Rows[0][0];
//            var name = string.Format("{0}-{1}.{2}", start.ToString("yyyyMMddHHmmss"), _Meter.Name, "db3");
//            var full = Path.Combine(DI.Get<MeterKnifeUserData>().GetValue(MeterKnifeUserData.DATA_PATH, string.Empty),
//                name);
//            if (_FiguredData.Save(full))
//            {
//                MessageBox.Show(string.Format("数据文件已保存:\r\n{0}", full));
//                _IsSaved = true;
//            }
        }

        private void _ExportStripButton_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var dir = dialog.SelectedPath;
                DateTime start;
                if (_figuredData.DataSet.Tables[1].Rows.Count > 0)
                    start = (DateTime) _figuredData.DataSet.Tables[1].Rows[0][0];
                else
                    start = DateTime.Now;
                var name = $"{start:yyyyMMddHHmmss}-{_Meter.Name}.{"xls"}";
                var full = Path.Combine(dir, name);

                var progressDialog = new ExportProgressDialog();
                progressDialog.SetPath(full);
                progressDialog.SetFigureData(_figuredData);
                Cursor = Cursors.WaitCursor;
                progressDialog.ShowDialog(this);
                Cursor = Cursors.Default;
                _IsSaved = true;
            }
        }

        private void _ClearDataToolStripButton_Click(object sender, EventArgs e)
        {
            var rs = MessageBox.Show(this, "是否清除已采集的数据?\r\n点击“是”将清空已采集的数据，\r\n点击“否”取消操作",
                "清除", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (rs == DialogResult.Yes) PlotsClear();
            _IsSaved = false;
        }

        private void _FilterToolStripButton_Click(object sender, EventArgs e)
        {
            var dialog = new FilterSettingDialog();
            dialog.SetFilter(_figuredData.Filter);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                _figuredData.Filter.Update(dialog.FiguredDataFilter);
                _Logger.Debug($"过滤参数：倍数{dialog.FiguredDataFilter.Multiple},统计{dialog.FiguredDataFilter.InStatistical},保存{dialog.FiguredDataFilter.IsSave}");
            }
        }

        #endregion

        #region 通讯相关

        protected virtual void StartCollect()
        {
            _handler.ProtocolRecevied += OnProtocolRecevied;
            _meterKernel.UpdateCollectState(_CarePort, _Meter.GpibAddress, true, _scpiCommandPanel.ScpiSubjectKey);

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
            if (_meterKernel != null && _Meter != null) _meterKernel.UpdateCollectState(_CarePort, _Meter.GpibAddress, false, _scpiCommandPanel.ScpiSubjectKey);
            _handler.ProtocolRecevied -= OnProtocolRecevied;
        }

        private void SendRead(object obj)
        {
            _comm.SendCommands(Port, GetInitCommands());
            var cmd = GetCollectCommands();
            _comm.SendLoopCommands(Port, cmd.Key, cmd.Value);
        }

        private void OnProtocolRecevied(object sender, EventArgs<CareTalking> e)
        {
            var talking = e.Item;
            if (talking.GpibAddress != _Meter.GpibAddress || talking.Scpi.Length < 6)
                return;
            var data = talking.Scpi;
            _Logger.Debug($"Recevied:{data}");
            double yzl = 0;
            if (double.TryParse(data, out yzl))
                if (_figuredData.Add(yzl))
                    this.ThreadSafeInvoke(() =>
                    {
                        if (uint.Parse(_figuredData.Count) <= 1)
                            return;
                        _mainPlot.Update(_figuredData);
                    });
            this.ThreadSafeInvoke(() => _FiguredDataPropertyGrid.Refresh());
        }

        #endregion
    }
}