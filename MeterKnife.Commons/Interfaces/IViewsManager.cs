using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Interfaces
{
    /// <summary>
    /// 程序中的主要窗体的管理
    /// </summary>
    public interface IViewsManager
    {
        Form InstrumentsDiscoveryView { get; set; }
        Form MeasureView { get; set; }
    }
}