using System.Windows.Forms;
using MeterKnife.Common.Base;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Util;
using NKnife.IoC;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Instruments
{
    public class MeterView : DockContent
    {
        protected BaseMeter _Meter;

        public virtual void SetMeter(BaseMeter meter)
        {
            _Meter = meter;
        }

        public int Port { get; set; }

        public CommunicationType CommunicationType { get; set; }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            var map = DI.Get<IMeterKernel>().MeterContents;
            if (map.ContainsKey(_Meter))
            {
                map.Remove(_Meter);
            }
        }
    }
}