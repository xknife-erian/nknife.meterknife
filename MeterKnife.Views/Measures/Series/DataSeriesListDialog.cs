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
        }

        private void ButtonStateManager()
        {
            if (_ListView.SelectedItems.Count <= 0)
            {
                _DeleteButton.Enabled = false;
                _ModifyButton.Enabled = false;
            }
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
                    var listItem = ByStyle(style);
                    listItem.Text = $"{i++}";
                    _ListView.Items.Add(listItem);
                }
            }
        }

        private static ListViewItem ByStyle(PlotSeriesStyleSolution.ExhibitSeriesStyle style)
        {
            var item = new ListViewItem();
            item.UseItemStyleForSubItems = false;
            item.Tag = style;
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, style.Exhibit.ToString()));
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, style.SeriesStyle.SeriesLineStyle.ToString()));
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, style.SeriesStyle.Thickness.ToString(CultureInfo.InvariantCulture)));

            var subItem = new ListViewItem.ListViewSubItem();
            subItem.BackColor = style.SeriesStyle.Color;
            item.SubItems.Add(subItem);

            subItem = new ListViewItem.ListViewSubItem();
            subItem.BackColor = style.SeriesStyle.MarkerStrokeColor;
            item.SubItems.Add(subItem);

            subItem = new ListViewItem.ListViewSubItem();
            subItem.BackColor = style.SeriesStyle.MarkerFillColor;
            item.SubItems.Add(subItem);
            return item;
        }

        private void _AppendButton_Click(object sender, System.EventArgs e)
        {
            var dialog = new DataSeriesEditorDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var item = ByStyle(dialog.SeriesStyle);
                item.Text = $"{_ListView.Items.Count + 1}";
                _ListView.Items.Add(item);
            }
        }

        private void _DeleteButton_Click(object sender, System.EventArgs e)
        {
        }

        private void _ModifyButton_Click(object sender, System.EventArgs e)
        {
            var dialog = new DataSeriesEditorDialog
            {
                SeriesStyle = (PlotSeriesStyleSolution.ExhibitSeriesStyle) _ListView.SelectedItems[0].Tag
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
            }
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
        }

        private void SaveSolution(string dialogSolutionName)
        {
            throw new System.NotImplementedException();
        }
    }
}