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

        protected PlotModel _MainPlotModel = new PlotModel();
        private IMeter _Meter;
        private bool _OnCollect; //是否正在采集
        protected PlotModel _TemperaturePlotModel = new PlotModel();

        public CollectDataView()
        {
            InitializeComponent();

            BuildMainPlogModel(_MainPlotModel);
            var mainPlot = new PlotView
            {
                Dock = DockStyle.Fill,
                PanCursor = Cursors.Hand,
                BackColor = Color.White,
                ZoomHorizontalCursor = Cursors.SizeWE,
                ZoomRectangleCursor = Cursors.SizeNWSE,
                ZoomVerticalCursor = Cursors.SizeNS,
                Model = _MainPlotModel
            };
            _PlotSplitContainer.Panel1.Controls.Add(mainPlot);

            var temperaturePlot = new PlotView
            {
                Dock = DockStyle.Fill,
                PanCursor = Cursors.Hand,
                BackColor = Color.White,
                ZoomHorizontalCursor = Cursors.SizeWE,
                ZoomRectangleCursor = Cursors.SizeNWSE,
                ZoomVerticalCursor = Cursors.SizeNS,
                Model = _TemperaturePlotModel
            };
            _PlotSplitContainer.Panel2.Controls.Add(temperaturePlot);
        }

        private void BuildMainPlogModel(PlotModel mainModel)
        {
            var linearAxis1 = new LinearAxis();
            mainModel.Axes.Add(linearAxis1);
            var linearAxis2 = new LinearAxis
            {
                Position = AxisPosition.Bottom
            };
            mainModel.Axes.Add(linearAxis2);
            var lineSeries = new LineSeries
            {
                Color = OxyColor.FromArgb(255, 78, 154, 6), 
                MarkerFill = OxyColor.FromArgb(255, 78, 154, 6)
            };
            mainModel.Series.Add(lineSeries);
        }

        public IMeter Meter
        {
            get { return _Meter; }
            set
            {
                _Meter = value;
                _FiguredDataPropertyGrid.SelectedObject = new FiguredData();
                _MeterParamPropertyGrid.SelectedObject = Meter.Parameters;
            }
        }

        public int Port { get; set; }

        public CommunicationType CommunicationType { get; set; }

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
            while (_OnCollect)
            {
                var careSaying = new CareSaying();
                careSaying.MainCommand = 0xAA;
                careSaying.SubCommand = 0x00;
                careSaying.Content = "READ?";
                careSaying.GpibAddress = (byte) _Meter.GpibAddress;
                byte[] data = careSaying.Generate();
                _Comm.Send(Port, data);
                Thread.Sleep(500);
            }
        }

        private void OnProtocolRecevied(object sender, EventArgs<CareSaying> e)
        {
            CareSaying saying = e.Item;
            _logger.Error(saying.Content);
        }
    }
}