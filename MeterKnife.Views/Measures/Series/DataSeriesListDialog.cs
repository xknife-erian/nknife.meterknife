using System.Globalization;
using System.Windows.Forms;
using MeterKnife.Interfaces;
using MeterKnife.Models;
using NKnife.IoC;

namespace MeterKnife.Views.Measures.Series
{
    public partial class DataSeriesListDialog : NKnife.ControlKnife.SimpleForm
    {
        private PlotSeriesStyleSolution _Solution = new PlotSeriesStyleSolution();
        private readonly IHabited _Habited = DI.Get<IHabited>();

        public DataSeriesListDialog()
        {
            InitializeComponent();
            ButtonStateManager();
            _ListView.SelectedIndexChanged += (s, e) => ButtonStateManager();
        }

        private void ButtonStateManager()
        {
            _DeleteButton.Enabled = _ListView.SelectedItems.Count > 0;
            _ModifyButton.Enabled = _ListView.SelectedItems.Count > 0;
            if (_Habited.SeriesStyleSolutions.Count <= 0)
            {
                _LoadButton.Enabled = false;
            }
        }

        public PlotSeriesStyleSolution Solution
        {
            get
            {
                _Solution.Clear();
                foreach (ListViewItem item in _ListView.Items)
                {
                    var style = (PlotSeriesStyleSolution.ExhibitSeriesStyle) item.Tag;
                    _Solution.Add(style);
                }
                return _Solution;
            }
            set
            {
                _Solution = value;
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

        private static void ByStyle(PlotSeriesStyleSolution.ExhibitSeriesStyle style, ListViewItem item)
        {
            item.UseItemStyleForSubItems = false;
            item.Tag = style;
            item.SubItems.Clear();
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, style.Exhibit.ToString()));
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, style.SeriesStyle.SeriesLineStyle.ToString()));
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, style.SeriesStyle.Thickness.ToString(CultureInfo.InvariantCulture)));

            var subItem = new ListViewItem.ListViewSubItem();
            subItem.BackColor = style.SeriesStyle.Color;
            item.SubItems.Add(subItem);

            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, style.SeriesStyle.Offset.ToString(CultureInfo.InvariantCulture)));
        }

        private void _AppendButton_Click(object sender, System.EventArgs e)
        {
            var dialog = new DataSeriesEditorDialog();
            dialog.IgnoreExistsExhibits(_Solution);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var style = dialog.SeriesStyle;
                var item = new ListViewItem();
                ByStyle(style, item);
                item.Text = $"{_ListView.Items.Count + 1}";
                _ListView.Items.Add(item);
                _Solution.Add(style); //向方案中添加样式
                _ListView.Select();
                item.Selected = true;
            }
            ButtonStateManager();
        }

        private void _DeleteButton_Click(object sender, System.EventArgs e)
        {
            ButtonStateManager();
        }

        private void _ModifyButton_Click(object sender, System.EventArgs e)
        {
            var item = _ListView.SelectedItems[0];
            var index = item.Text;
            var dialog = new DataSeriesEditorDialog
            {
                SeriesStyle = (PlotSeriesStyleSolution.ExhibitSeriesStyle) item.Tag
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
            var dialog = new SolutionListDialog(false);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                Solution = dialog.Solution;
            }
        }

        private void _SaveButton_Click(object sender, System.EventArgs e)
        {
            var dialog = new SolutionListDialog(true);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                SaveSolution(dialog.SolutionName);
            }
            ButtonStateManager();
        }

        private void SaveSolution(string dialogSolutionName)
        {
            throw new System.NotImplementedException();
        }
    }
}