using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Base.Plugins;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Plugins;
using NKnife.IoC;

namespace MeterKnife.Plugins.FileMenu
{
    public class Exit : IPlugIn
    {
        private readonly ToolStripMenuItem _ExitMenuItem = new ToolStripMenuItem("退出(&X)");
        private readonly ToolStripMenuItem _ExitContextMenuItem = new ToolStripMenuItem("退出(&X)");

        public Exit()
        {
            _ExitMenuItem.ShortcutKeys = Keys.Control | Keys.Alt | Keys.X;
            _ExitMenuItem.Click += OnExitMenuItemOnClick;
            _ExitContextMenuItem.Click += OnExitMenuItemOnClick;
        }

        private void OnExitMenuItemOnClick(object s, EventArgs e)
        {
            var workbench = DI.Get<IWorkbench>();
            workbench.KernelCallFormClose = true;
            ((Form)workbench).Close();
        }

        #region Implementation of IPlugIn

        /// <summary>
        ///     描述本插件类型
        /// </summary>
        public PluginStyle PluginStyle { get; } = PluginStyle.FileMenu;

        /// <summary>
        ///     插件的详细描述
        /// </summary>
        public PluginDetail Detail { get; } = new PluginDetailKnife();

        /// <summary>
        ///     将本插件的功能绑定于相应的菜单与工具条上，绑定需要呈现的控件到相应的界面组件上。
        /// </summary>
        /// <param name="component"></param>
        public void BindViewComponent(PluginViewComponent component)
        {
            var collection = component.StripItemCollection;
            collection.Add(new ToolStripSeparator());
            collection.Add(_ExitMenuItem);

            component.TrayMenu.Items.Add(_ExitContextMenuItem);
        }

        /// <summary>
        ///     向扩展模组注册核心扩展供给器。
        /// </summary>
        /// <param name="provider">核心扩展供给器</param>
        public bool Register(ref IExtenderProvider provider)
        {
            return true;
        }

        /// <summary>
        ///     从扩展模组回收核心扩展供给器。
        /// </summary>
        public bool UnRegister()
        {
            return true;
        }

        #endregion
    }
}
