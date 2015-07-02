using System.Windows.Forms;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Tunnels;
using MeterKnife.Common.Util;
using NKnife.IoC;
using NKnife.Tunnel;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Instruments
{
    public class MeterView : DockContent
    {
        protected BaseMeter _Meter;
        protected CarePort _Port;

        public virtual void SetMeter(CarePort port, BaseMeter meter)
        {
            _Port = port;
            _Meter = meter;
        }

        public CarePort Port
        {
            get { return _Port; }
            set { _Port = value; }
        }

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