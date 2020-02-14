﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.MeterKnife.Common;
using NKnife.Win.Quick.Base;

namespace NKnife.MeterKnife.Workbench.Options
{
    public partial class DataOptionPanel : UserControl, IOptionPanel
    {
        public DataOptionPanel()
        {
            InitializeComponent();
            Name = this.Res("数据");
            _dataSavePathLabel.Text = this.Res(_dataSavePathLabel.Text);
            _brownPathButton.Text = this.Res(_brownPathButton.Text);
            ResponseToEvent();
        }

        private void ResponseToEvent()
        {
            _brownPathButton.Click += (s, e) =>
            {
                var dialog = new FolderBrowserDialog();
                dialog.ShowNewFolderButton = true;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    _dataSavePathTextBox.Text = dialog.SelectedPath;
                    ((IOptionPanel)this).HasDataChanged = true;
                    ((IOptionPanel)this).OptionMap.Add(HabitManager.KEY_MetricalData_Path, _dataSavePathTextBox.Text);
                }
            };
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
        IDictionary<string, object> IOptionPanel.OptionMap { get; set; } = new Dictionary<string, object>(0);

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="getOptionValueFunc">获取选项值的方法</param>
        /// <returns>初始化是否成功</returns>
        bool IOptionPanel.Initialize(Func<string, string, object> getOptionValueFunc)
        {
            _dataSavePathTextBox.Text = getOptionValueFunc.Invoke(HabitManager.KEY_MetricalData_Path, "").ToString();
            return true;
        }

        #endregion
    }
}
