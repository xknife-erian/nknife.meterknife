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
        protected int _Port;

        public virtual void SetMeter(int port, BaseMeter meter)
        {
            _Port = port;
            _Meter = meter;
        }

        public int Port { get; set; }

        public CommunicationType CommunicationType { get; set; }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            var map = DI.Get<IMeterKernel>().MeterContents;
            if (_Meter!= null && map.ContainsKey(_Meter))
            {
                map.Remove(_Meter);
            }
        }
    }
}