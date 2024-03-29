﻿using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.ViewModels.Plots;
using NKnife.Win.Forms;

namespace NKnife.MeterKnife.Workbench.Dialogs.Plots
{
    public partial class DataSeriesListDialog : SimpleForm
    {
        private DUTSeriesStyleSolution _solution = new DUTSeriesStyleSolution();
        private readonly IHabitManager _habits;
        private SolutionListDialog _solutionListDialog;

        public DataSeriesListDialog(IHabitManager habits, SolutionListDialog solutionListDialog)
        {
            _habits = habits;
            _solutionListDialog = solutionListDialog;
            InitializeComponent();
            ButtonStateManager();
            _ListView.SelectedIndexChanged += (s, e) => ButtonStateManager();
        }

        private void ButtonStateManager()
        {
            _DeleteButton.Enabled = _ListView.SelectedItems.Count > 0;
            _ModifyButton.Enabled = _ListView.SelectedItems.Count > 0;
            var list = _habits.GetOptionValue("SeriesStyleSolutionList", new List<DUTSeriesStyleSolution>());
            if (list.Count <= 0)
            {
                _LoadButton.Enabled = false;
            }
        }

        public DUTSeriesStyleSolution Solution
        {
            get
            {
                _solution.Clear();
                foreach (ListViewItem item in _ListView.Items)
                {
                    var style = (DUTSeriesStyle) item.Tag;
                    _solution.Add(style);
                }
                return _solution;
            }
            set
            {
                _solution = value;
                var i = 1;
                foreach (var style in value)
                {
                    var listItem = new ListViewItem();
                    ByStyle(style, listItem);
                    listItem.Text = $"{i++}";
                    _ListView.Items.Add(listItem);
                }
            }
        }

        private static void ByStyle(DUTSeriesStyle style, ListViewItem item)
        {
            item.UseItemStyleForSubItems = false;
            item.Tag = style;
            item.SubItems.Clear();
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, style.DUT.ToString()));
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, style.LineStyle.ToString()));
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, style.Thickness.ToString(CultureInfo.InvariantCulture)));

            var subItem = new ListViewItem.ListViewSubItem();
            subItem.BackColor = style.Color;
            item.SubItems.Add(subItem);

            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, style.Offset.ToString(CultureInfo.InvariantCulture)));
        }

        private void _AppendButton_Click(object sender, System.EventArgs e)
        {
            var dialog = new DataSeriesEditorDialog();
            dialog.IgnoreExistsExhibits(_solution);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var style = dialog.SeriesStyle;
                var item = new ListViewItem();
                ByStyle(style, item);
                item.Text = $"{_ListView.Items.Count + 1}";
                _ListView.Items.Add(item);
                _solution.Add(style); //向方案中添加样式
                _ListView.Select();
                item.Selected = true;
            }
            ButtonStateManager();
        }

        private void _DeleteButton_Click(object sender, System.EventArgs e)
        {
            var item = _ListView.SelectedItems[0];
            var style = (DUTSeriesStyle) item.Tag;
            var dr = MessageBox.Show(this, $"确认删除{style.DUT}数据的显示样式？", "删除样式",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Yes)
            {
                if (_solution.Remove(style))
                {
                    _ListView.Items.Remove(item);
                    for (int i = 1; i <= _ListView.Items.Count; i++)
                    {
                        _ListView.Items[i - 1].Text = $"{i}";
                    }
                }
            }
            ButtonStateManager();
        }

        private void _ModifyButton_Click(object sender, System.EventArgs e)
        {
            var item = _ListView.SelectedItems[0];
            var index = item.Text;
            var dialog = new DataSeriesEditorDialog
            {
                SeriesStyle = (DUTSeriesStyle) item.Tag
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                ByStyle(dialog.SeriesStyle, item);
            }
            item.Text = index;
        }

        private void _AcceptButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void _LoadButton_Click(object sender, System.EventArgs e)
        {
            if (_solutionListDialog.ShowDialog(this) == DialogResult.OK)
            {
                Solution = _solutionListDialog.Solution;
            }
        }

        private void _SaveButton_Click(object sender, System.EventArgs e)
        {
            if (_solutionListDialog.ShowDialog(this) == DialogResult.OK)
            {
                SaveSolution(_solutionListDialog.SolutionName);
            }
            ButtonStateManager();
        }

        private void SaveSolution(string solutionName)
        {
            Solution.Name = solutionName;
            var list = _habits.GetOptionValue("SeriesStyleSolutionList", new List<DUTSeriesStyleSolution>());
            list.Add(Solution);
            _habits.SetOptionValue("SeriesStyleSolutionList", list);
        }
    }
}