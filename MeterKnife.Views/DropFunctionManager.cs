using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Base;
using MeterKnife.Base.Plugins;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Plugins;
using MeterKnife.Kernel.Plugins;
using MeterKnife.Views.Menus;
using Ninject;
using NKnife.IoC;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Views
{
    public class DropFunctionManager : Dictionary<PluginStyle, PluginViewComponent>, IDropFunctionManager
    {
        public DropFunctionManager()
        {
            var contextMenu = DI.Get<TrayMenuStrip>();

            var pv = new PluginViewComponent();
            pv.Set(DI.Get<FileMenuItem>().DropDownItems, contextMenu);
            pv.Add(DI.Get<DockPanel>());
            Add(PluginStyle.FileMenu, pv);

            pv = new PluginViewComponent();
            pv.Set(DI.Get<MeasureMenuItem>().DropDownItems, contextMenu);
            pv.Add(DI.Get<DockPanel>());
            Add(PluginStyle.MeasureMenu, pv);

            pv = new PluginViewComponent();
            pv.Set(DI.Get<DataMenuItem>().DropDownItems, contextMenu);
            pv.Add(DI.Get<DockPanel>());
            Add(PluginStyle.DataMenu, pv);

            pv = new PluginViewComponent();
            pv.Set(DI.Get<ToolMenuItem>().DropDownItems, contextMenu);
            pv.Add(DI.Get<DockPanel>());
            Add(PluginStyle.ToolMenu, pv);

            pv = new PluginViewComponent();
            pv.Set(DI.Get<ViewMenuItem>().DropDownItems, contextMenu);
            pv.Add(DI.Get<DockPanel>());
            Add(PluginStyle.ViewMenu, pv);

            pv = new PluginViewComponent();
            pv.Set(DI.Get<HelpMenuItem>().DropDownItems, contextMenu);
            pv.Add(DI.Get<DockPanel>());
            Add(PluginStyle.HelpMenu, pv);
        }
    }
}
