using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Win.Quick.Base
{
    public interface IWorkbench
    {
        DockPanel MainDockPanel { get; }
        bool HideOnClosing { get; set; }
    }
}
