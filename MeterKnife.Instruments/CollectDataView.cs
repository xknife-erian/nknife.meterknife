using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Tunnels;
using MeterKnife.Common.Util;
using MeterKnife.Instruments.Properties;
using NKnife.Events;
using NKnife.IoC;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Instruments
{
    public partial class CollectDataView : DockContent
    {
        private static readonly ILog _logger = LogManager.GetLogger<CollectDataView>();
        private readonly BaseCareCommunicationService _Comm = DI.Get<BaseCareCommunicationService>();

        private readonly FiguredData _FiguredData = new FiguredData();

        protected LineSeries _MainLineSeries = new LineSeries();
        protected LinearAxis _MainValueAxis = new LinearAxis();
        protected LineSeries _TemperatureLineSeries = new LineSeries();
        protected LinearAxis _TemperatureValueAxis = new LinearAxis();
        private BaseMeter _Meter;
        private BaseParamPanel _Panel;
        private bool _OnCollect; //是否正在采集

        public CollectDataView()
        {
            InitializeComponent();
            _StartStripButton.Image = Resources.start;
            _StopStripButton.Image = Resources.stop;
            _SaveStripButton3.Image = Resources.save;
            _ExportStripButton1.Image = Resources.export;
            _PhotoToolStripButton.Image = Resources.photo;
            _ZoomInToolStripButton.Image = Resources.zoom_in;
            _ZoomOutToolStripButton.Image = Resources.zoom_out;

            PlotModel mainModel = BuildMainPlogModel();
            var mainPlot = new PlotView
            {
                Dock = DockStyle.Fill,
                PanCursor = Cursors.Hand,
                BackColor = Color.White,
                ZoomHorizontalCursor = Cursors.SizeWE,
                ZoomRectangleCursor = Cursors.SizeNWSE,
                ZoomVerticalCursor = Cursors.SizeNS,
                Model = mainModel
            };
            _PlotSplitContainer.Panel1.Controls.Add(mainPlot);

            PlotModel temperatureModel = BuildTemperaturePlogModel();
            var temperaturePlot = new PlotView
            {
                Dock = DockStyle.Fill,
                PanCursor = Cursors.Hand,
                BackColor = Color.White,
                ZoomHorizontalCursor = Cursors.SizeWE,
                ZoomRectangleCursor = Cursors.SizeNWSE,
                ZoomVerticalCursor = Cursors.SizeNS,
                Model = temperatureModel
            };
            _PlotSplitContainer.Panel2.Controls.Add(temperaturePlot);
        }

        public BaseMeter Meter
        {
            get { return _Meter; }
            set
            {
                _Meter = value;
                _FiguredDataPropertyGrid.SelectedObject = _FiguredData;
                _Panel = value.ParamPanel;
                _ParamsPanel.Controls.Add(_Panel);
            }
        }

        public int Port { get; set; }

        public CommunicationType CommunicationType { get; set; }

        private PlotModel BuildMainPlogModel()
        {
            var model = new PlotModel();

            _MainValueAxis.MaximumPadding = 0;
            _MainValueAxis.MinimumPadding = 0;
            _MainValueAxis.Maximum = 15;
            _MainValueAxis.Minimum = 5;
            _MainValueAxis.Position = AxisPosition.Left;
            model.Axes.Add(_MainValueAxis);

            var timeAxis = new DateTimeAxis(); //时间刻度
            timeAxis.MajorGridlineStyle = LineStyle.Solid;
            timeAxis.MaximumPadding = 0;
            timeAxis.MinimumPadding = 0;
            timeAxis.MinorGridlineStyle = LineStyle.Dot;
            timeAxis.Position = AxisPosition.Bottom;
            model.Axes.Add(timeAxis);

            _MainLineSeries.Color = OxyColor.FromArgb(255, 78, 154, 6);
            _MainLineSeries.MarkerFill = OxyColor.FromArgb(255, 78, 154, 6);
            model.Series.Add(_MainLineSeries);
            return model;
        }

        private PlotModel BuildTemperaturePlogModel()
        {
            var mainModel = new PlotModel();

            _TemperatureValueAxis.MaximumPadding = 0;
            _TemperatureValueAxis.MinimumPadding = 0;
            _TemperatureValueAxis.Maximum = 15;
            _TemperatureValueAxis.Minimum = 5;
            _TemperatureValueAxis.Position = AxisPosition.Left;
            mainModel.Axes.Add(_TemperatureValueAxis);

            var timeAxis = new DateTimeAxis(); //时间刻度
            timeAxis.MajorGridlineStyle = LineStyle.Solid;
            timeAxis.MaximumPadding = 0;
            timeAxis.MinimumPadding = 0;
            timeAxis.MinorGridlineStyle = LineStyle.Dot;
            timeAxis.Position = AxisPosition.Bottom;
            mainModel.Axes.Add(timeAxis);

            _TemperatureLineSeries.Color = OxyColor.FromArgb(255, 124, 124, 248);
            _TemperatureLineSeries.MarkerFill = OxyColor.FromArgb(255, 78, 154, 6);
            mainModel.Series.Add(_TemperatureLineSeries);
            return mainModel;
        }

        private void _StartStripButton_Click(object sender, EventArgs e)
        {
            var handler = (ScpiProtocolHandler) _Comm.CareHandlers[Port];
            handler.ProtocolRecevied += OnProtocolRecevied;
            _OnCollect = true;
            var thread = new Thread(SendRead);
            thread.Start();
        }

        private void _StopStripButton_Click(object sender, EventArgs e)
        {
            _OnCollect = false;
            Thread.Sleep(50);
            var handler = (ScpiProtocolHandler) _Comm.CareHandlers[Port];
            handler.ProtocolRecevied -= OnProtocolRecevied;
        }

        private void SendRead(object obj)
        {
            var cmdlist = _Panel.GpibCommands;
            foreach (var cmd in cmdlist)
            {
                if (cmd == null)
                    continue;
                byte[] cmdBytes = CareSaying.BuildCareSaying(_Meter.GpibAddress, cmd.Command, false).Generate();
                _Comm.Send(Port, cmdBytes);
                Thread.Sleep((int) cmd.Interval);
            }

            byte[] read = CareSaying.READ(_Meter.GpibAddress).Generate();
            byte[] temp = CareSaying.TEMP().Generate();
            while (_OnCollect)
            {
                _Comm.Send(Port, read);
                Thread.Sleep(600);
                _Comm.Send(Port, temp);
                Thread.Sleep(300);
            }
        }

        private void OnProtocolRecevied(object sender, EventArgs<CareSaying> e)
        {
            CareSaying saying = e.Item;

            if (saying.MainCommand == 0xAE)
            {
                string data = saying.Content.Substring(0, 5);
                double yzl = 0;
                if (double.TryParse(data, out yzl))
                {
                    _FiguredData.AddTemperature(yzl);
                    if (Math.Abs(_FiguredData.MaxTemperature) > 0 && Math.Abs(_FiguredData.MinTemperature) > 0)
                    {
                        double j = (Math.Abs(_FiguredData.MaxTemperature - _FiguredData.MinTemperature))/4;
                        if (Math.Abs(j) <= 0)
                        {
                            _TemperatureValueAxis.Maximum = _FiguredData.MaxTemperature + 0.02;
                            _TemperatureValueAxis.Minimum = _FiguredData.MaxTemperature - 0.02;
                        }
                        else
                        {
                            if (_TemperatureValueAxis.Maximum < _FiguredData.MaxTemperature + j)
                                _TemperatureValueAxis.Maximum = _FiguredData.MaxTemperature + j;
                            if (_TemperatureValueAxis.Minimum > _FiguredData.MinTemperature - j)
                                _TemperatureValueAxis.Minimum = _FiguredData.MinTemperature - j;
                        }
                    }

                    DataPoint v = DateTimeAxis.CreateDataPoint(DateTime.Now, yzl);
                    _TemperatureLineSeries.Points.Add(v);
                    _TemperatureLineSeries.PlotModel.InvalidatePlot(true);
                }
            }
            else
            {
                if ((saying.GpibAddress != Meter.GpibAddress) || (saying.Content.Length < 6))
                    return;
                string data = saying.Content.Substring(1, saying.Content.Length - 6);
                double yzl = 0;
                if (double.TryParse(data, out yzl))
                {
                    _FiguredData.Add(yzl);

                    if (Math.Abs(_FiguredData.Max) > 0 && Math.Abs(_FiguredData.Min) > 0)
                    {
                        double j = (Math.Abs(_FiguredData.Max - _FiguredData.Min)) / 4;
                        if (Math.Abs(j) > 0)
                        {
                            _MainValueAxis.Maximum = _FiguredData.Max + j;
                            _MainValueAxis.Minimum = _FiguredData.Min - j;
                        }
                    }

                    DataPoint v = DateTimeAxis.CreateDataPoint(DateTime.Now, yzl);
                    _MainLineSeries.Points.Add(v);
                    _MainLineSeries.PlotModel.InvalidatePlot(true);
                }
            }
            _FiguredDataPropertyGrid.ThreadSafeInvoke(() => _FiguredDataPropertyGrid.Refresh());
        }
    }
}