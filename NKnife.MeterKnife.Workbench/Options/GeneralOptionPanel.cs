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
    public partial class GeneralOptionPanel : UserControl, IOptionPanel
    {
        public GeneralOptionPanel()
        {
            InitializeComponent();
            Name = this.Res("通用");
        }

        #region Implementation of IOptionPanel

        /// <summary>
        /// 选项面板的图标
        /// </summary>
        public Icon Icon { get; set; }

        /// <summary>
        /// 是否有选项数据发生变化
        /// </summary>
        public bool HasDataChanged { get; set; }

        /// <summary>
        /// 发生了变化的数据
        /// </summary>
        public IDictionary<string, object> OptionMap { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="getOptionValueFunc">获取选项值的方法</param>
        /// <returns>初始化是否成功</returns>
        public bool Initialize(Func<string, string, object> getOptionValueFunc)
        {
            return true;
        }

        #endregion
    }
}
