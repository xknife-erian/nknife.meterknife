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
            Add(PluginStyle.Base, DI.Get<FileMenuItem>().DropDownItems);
            Add(PluginStyle.Measure, null);
            Add(PluginStyle.Data, null);
            Add(PluginStyle.Tool, null);
        }
    }
}
