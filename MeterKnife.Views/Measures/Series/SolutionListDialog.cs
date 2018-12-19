using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Interfaces;
using MeterKnife.Models;
using NKnife.ControlKnife;
using NKnife.IoC;

namespace MeterKnife.Views.Measures.Series
{
    public partial class SolutionListDialog : SimpleForm
    {
        public SolutionListDialog(bool isSave)
        {
            InitializeComponent();
            _NameTextBox.Enabled = isSave;
            var habits = DI.Get<IUserHabits>();
            var list = habits.SeriesStyleSolutionList;
            for (int i = 0; i < list.Count; i++)
            {
                var solution = list[i];
                var viewitem = new ListViewItem($"{i+1}");
                viewitem.Tag = solution;
                var subitem = new ListViewItem.ListViewSubItem(viewitem, solution.SolutionName);
                viewitem.SubItems.Add(subitem);
                subitem = new ListViewItem.ListViewSubItem(viewitem, $"{solution.Styles.Count}");
                viewitem.SubItems.Add(subitem);
                _ListView.Items.Add(viewitem);
            }
            _ListView.SelectedIndexChanged += (s, e) =>
            {
                if (_ListView.SelectedItems.Count > 0)
                    _NameTextBox.Text = ((PlotSeriesStyleSolution) _ListView.SelectedItems[0].Tag).SolutionName;
            };
        }

        public PlotSeriesStyleSolution Solution { get; set; }
        public string SolutionName { get; set; }

        private void _AcceptButton_Click(object sender, EventArgs e)
        {
            if (!_NameTextBox.Enabled)
            {
                if (_ListView.SelectedItems.Count <= 0)
                {
                    MessageBox.Show(this, "未选择已保存的样式。\r\n请选择。");
                    return;
                }
            }
            DialogResult = DialogResult.OK;
            SolutionName = _NameTextBox.Text;
            if (_ListView.SelectedItems.Count > 0)
                Solution = (PlotSeriesStyleSolution) _ListView.SelectedItems[0].Tag;
        }

        private void _CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
