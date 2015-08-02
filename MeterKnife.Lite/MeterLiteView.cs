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
    internal class MeterLiteView : DigitMultiMeterView
    {
        private readonly CustomerScpiSubjectPanel _ScpiCommandPanel = new CustomerScpiSubjectPanel();
        /// <summary>
        /// 是否是精灵版
        /// </summary>
        public static bool IsFairy { get; set; }

        public MeterLiteView()
        {
            if (IsFairy)
            {
                _PhotoToolStripButton.Visible = false;
                _ZoomInToolStripButton.Visible = false;
                _ZoomOutToolStripButton.Visible = false;
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
