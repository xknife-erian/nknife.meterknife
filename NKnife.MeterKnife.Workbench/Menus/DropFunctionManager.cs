using System.Collections.Generic;
using NKnife.MeterKnife.Workbench.Base.Plugins;
using NKnife.MeterKnife.Workbench.Plugins;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.MeterKnife.Workbench.Menus
{
    public class DropFunctionManager : Dictionary<PluginStyle, PluginViewComponent>, IDropFunctionManager
    {
        public DropFunctionManager()
        {
            // var contextMenu = DI.Get<TrayMenuStrip>();
            //
            // var pv = new PluginViewComponent();
            // pv.Set(DI.Get<FileMenuItem>().DropDownItems, contextMenu);
            // pv.Add(DI.Get<DockPanel>());
            // Add(PluginStyle.FileMenu, pv);
            //
            // pv = new PluginViewComponent();
            // pv.Set(DI.Get<MeasureMenuItem>().DropDownItems, contextMenu);
            // pv.Add(DI.Get<DockPanel>());
            // Add(PluginStyle.MeasureMenu, pv);
            //
            // pv = new PluginViewComponent();
            // pv.Set(DI.Get<DataMenuItem>().DropDownItems, contextMenu);
            // pv.Add(DI.Get<DockPanel>());
            // Add(PluginStyle.DataMenu, pv);
            //
            // pv = new PluginViewComponent();
            // pv.Set(DI.Get<ToolMenuItem>().DropDownItems, contextMenu);
            // pv.Add(DI.Get<DockPanel>());
            // Add(PluginStyle.ToolMenu, pv);
            //
            // pv = new PluginViewComponent();
            // pv.Set(DI.Get<ViewMenuItem>().DropDownItems, contextMenu);
            // pv.Add(DI.Get<DockPanel>());
            // Add(PluginStyle.ViewMenu, pv);
            //
            // pv = new PluginViewComponent();
            // pv.Set(DI.Get<HelpMenuItem>().DropDownItems, contextMenu);
            // pv.Add(DI.Get<DockPanel>());
            // Add(PluginStyle.HelpMenu, pv);
        }
    }
}
