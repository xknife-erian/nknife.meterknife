using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeterKnife.Interfaces.Plugins;
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
            Add(PluginStyle.MeasureMenu, pv);

            pv = new PluginViewComponent();
            pv.Add(DI.Get<DataMenuItem>().DropDownItems);
            Add(PluginStyle.DataMenu, pv);

            pv = new PluginViewComponent();
            pv.Add(DI.Get<ToolMenuItem>().DropDownItems);
            Add(PluginStyle.ToolMenu, pv);

            pv = new PluginViewComponent();
            pv.Add(DI.Get<ViewMenuItem>().DropDownItems);
            Add(PluginStyle.ViewMenu, pv);

            pv = new PluginViewComponent();
            pv.Add(DI.Get<HelpMenuItem>().DropDownItems);
            Add(PluginStyle.HelpMenu, pv);
        }
    }
}
