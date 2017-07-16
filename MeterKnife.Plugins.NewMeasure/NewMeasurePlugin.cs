using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Interfaces.Plugins;

namespace MeterKnife.Plugins.NewMeasure
{
    public class NewMeasurePlugin : IPlugIn
    {
        #region Implementation of IPlugIn

        /// <summary>
        ///     描述本插件类型
        /// </summary>
        public PluginStyle PluginStyle { get; } = PluginStyle.Base;

        /// <summary>
        ///     插件的详细描述
        /// </summary>
        public PluginDetail Detail { get; } 

        /// <summary>
        ///     将本插件的功能绑定于相应的菜单与工具条上，绑定需要呈现的控件到相应的界面组件上。
        /// </summary>
        /// <param name="component"></param>
        public void BindViewComponent(IPluginViewComponent component)
        {
            foreach (ToolStripItemCollection collection in component.ToolStripItemCollections)
            {
                collection.Add("新建测量");
            }
            foreach (Control control in component.Containers)
            {
                
            }
        }

        /// <summary>
        ///     向扩展模组注册并释放核心扩展供给器。
        /// </summary>
        /// <param name="provider">核心扩展供给器</param>
        public bool Register(ref IExtenderProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     从扩展模组回收核心扩展供给器。
        /// </summary>
        public bool UnRegister()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
