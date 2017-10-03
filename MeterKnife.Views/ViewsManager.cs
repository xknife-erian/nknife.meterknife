using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Interfaces;
using NKnife.IoC;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Views
{
    /// <summary>
    /// 程序中的主要窗体的管理
    /// </summary>
    public class ViewsManager : IViewsManager
    {
        #region Implementation of IViewsManager

        public Form InstrumentsDiscoveryView { get; set; } = new Lazy<Form>(() => new InstrumentsDiscoveryView()).Value;
        public Form MeasureView { get; set; } = new MeasureView();
        #endregion
    }
}
