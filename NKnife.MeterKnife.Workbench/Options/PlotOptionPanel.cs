using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.Win.Quick.Base;

namespace NKnife.MeterKnife.Workbench.Options
{
    public sealed partial class PlotOptionPanel : UserControl, IOptionPanel
    {
        public PlotOptionPanel()
        {
            InitializeComponent();
            Name = this.Res("图表");
        }

        #region Implementation of IOptionPanel

        /// <summary>
        /// 选项面板的图标
        /// </summary>
        Icon IOptionPanel.Icon { get; set; }

        /// <summary>
        /// 是否有选项数据发生变化
        /// </summary>
        bool IOptionPanel.HasDataChanged { get; set; }

        /// <summary>
        /// 发生了变化的数据
        /// </summary>
        IDictionary<string, object> IOptionPanel.OptionMap { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="getOptionValueFunc">获取选项值的方法</param>
        /// <returns>初始化是否成功</returns>
        bool IOptionPanel.Initialize(Func<string, string, object> getOptionValueFunc)
        {
            return true;
        }

        #endregion
    }
}
