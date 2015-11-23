using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Instruments;

namespace MeterKnife.App.Lite
{
    /// <summary>
    /// Lite版简化的万用表界面
    /// </summary>
    internal class DigitMultiMeterLiteView : DigitMultiMeterView
    {
        /// <summary>
        /// 是否是精灵版
        /// </summary>
        public static bool IsLite { get; set; }

        public DigitMultiMeterLiteView()
        {
            if (IsLite)
            {
                _ZoomInToolStripButton.Visible = false;
                _ZoomOutToolStripButton.Visible = false;
                _PrintStripButton.Visible = false;
                _PhotoToolStripButton.Visible = false;
                //_FilterToolStripButton.Visible = false;
                _SaveStripButton.Visible = false;
            }
        }

        public override void SetMeter(CommPort port, BaseMeter meter)
        {
            base.SetMeter(port, meter);
            if (!_Comm.IsInitialized)
            {
                _Comm.Start(port);
            }
        }
    }
}
