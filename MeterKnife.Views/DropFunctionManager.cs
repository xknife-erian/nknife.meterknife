using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Base.Plugins;
using MeterKnife.Interfaces.Plugins;
using MeterKnife.Kernel.Plugins;
using MeterKnife.Views.MenuItems;
using MeterKnife.Views.Menus;
using NKnife.IoC;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Views
{
    public class DropFunctionManager : Dictionary<PluginStyle, IPluginViewComponent>, IDropFunctionManager
    {
        public DropFunctionManager()
        {
            var pv = new PluginViewComponent();
            pv.Add(DI.Get<FileMenuItem>().DropDownItems);
            pv.Add(DI.Get<DockPanel>());
            Add(PluginStyle.FileMenu, pv);

            pv = new PluginViewComponent();
            pv.Add(DI.Get<MeasureMenuItem>().DropDownItems);
            pv.Add(DI.Get<DockPanel>());
            Add(PluginStyle.MeasureMenu, pv);

            pv = new PluginViewComponent();
            pv.Add(DI.Get<DataMenuItem>().DropDownItems);
            pv.Add(DI.Get<DockPanel>());
            Add(PluginStyle.DataMenu, pv);

            pv = new PluginViewComponent();
            pv.Add(DI.Get<ToolMenuItem>().DropDownItems);
            pv.Add(DI.Get<DockPanel>());
            Add(PluginStyle.ToolMenu, pv);

            pv = new PluginViewComponent();
            pv.Add(DI.Get<ViewMenuItem>().DropDownItems);
            pv.Add(DI.Get<DockPanel>());
            Add(PluginStyle.ViewMenu, pv);

            pv = new PluginViewComponent();
            pv.Add(DI.Get<HelpMenuItem>().DropDownItems);
            pv.Add(DI.Get<DockPanel>());
            Add(PluginStyle.HelpMenu, pv);
        }
    }
}
