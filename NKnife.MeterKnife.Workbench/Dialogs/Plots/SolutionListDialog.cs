using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.ViewModels.Plots;
using NKnife.Win.Forms;

namespace NKnife.MeterKnife.Workbench.Dialogs.Plots
{
    public partial class SolutionListDialog : SimpleForm
    {
        private IHabitManager _habits;
        public SolutionListDialog(IHabitManager habits)
        {
            _habits = habits;
            InitializeComponent();
            //_NameTextBox.Enabled = isSave;
            var list = _habits.GetOptionValue("SeriesStyleSolutionList", new List<PlotSeriesStyleSolution>());
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
