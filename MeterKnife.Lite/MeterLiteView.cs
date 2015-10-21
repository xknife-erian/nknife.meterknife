using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MeterKnife.Common.Base;
using MeterKnife.Common.Controls;
using MeterKnife.Common.DataModels;
using MeterKnife.Instruments;
using MeterKnife.Instruments.Common;
using MeterKnife.Scpis;
using ScpiKnife;

namespace MeterKnife.Lite
{
    /// <summary>
    /// Lite版简化的万用表界面
    /// </summary>
    internal class MeterLiteView : DigitMultiMeterView
    {
        /// <summary>
        /// 是否是精灵版
        /// </summary>
        public static bool IsLite { get; set; }

        public MeterLiteView()
        {
            if (IsLite)
            {
                _ZoomInToolStripButton.Visible = false;
                _ZoomOutToolStripButton.Visible = false;
                _SaveStripButton.Visible = false;
                _PrintStripButton.Visible = false;
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
