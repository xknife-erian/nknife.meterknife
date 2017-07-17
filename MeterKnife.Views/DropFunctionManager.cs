using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Interfaces.Plugins;
using MeterKnife.Views.MenuItems;
using NKnife.IoC;

namespace MeterKnife.Views
{
    public class DropFunctionManager : Dictionary<PluginStyle, ToolStripItemCollection>, IDropFunctionManager
    {

        public DropFunctionManager()
        {
            Add(PluginStyle.FileMenu, DI.Get<FileMenuItem>().DropDownItems);
            Add(PluginStyle.MeasureMenu, DI.Get<MeasureMenuItem>().DropDownItems);
            Add(PluginStyle.DataMenu, DI.Get<DataMenuItem>().DropDownItems);
            Add(PluginStyle.ToolMenu, DI.Get<ToolMenuItem>().DropDownItems);
            Add(PluginStyle.ViewMenu, DI.Get<ViewMenuItem>().DropDownItems);
            Add(PluginStyle.HelpMenu, DI.Get<HelpMenuItem>().DropDownItems);
        }
    }
}
