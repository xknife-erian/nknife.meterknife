using System;
using System.Windows.Forms;

namespace NKnife.GUI.WinForm
{
    public class SingleRowCheckedListView : ListView
    {
        public SingleRowCheckedListView()
        {
            Initialize();
            Click += ListViewSelectedIndexChanged;
            SelectedIndexChanged += ListViewSelectedIndexChanged;
            ItemCheck += ListViewItemCheck;
        }

        private static void ListViewSelectedIndexChanged(object sender, EventArgs e)
        {
            var lv = (ListView)sender;
            foreach (ListViewItem item in lv.Items)
            {
                item.Checked = item.Selected;
            }
        }

        private static void ListViewItemCheck(object sender, ItemCheckEventArgs e)
        {
            var lv = (ListView)sender;
            lv.ItemCheck -= ListViewItemCheck;
            if (e.NewValue == CheckState.Checked)
            {
                for (int i = 0; i < lv.Items.Count; i++)
                {
                    lv.Items[i].Checked = (i == e.Index);
                    lv.Items[i].Selected = (i == e.Index);
                }
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                e.NewValue = lv.Items[e.Index].Selected ? CheckState.Checked : CheckState.Unchecked;
            }
            lv.ItemCheck += ListViewItemCheck;
        }

        private void Initialize()
        {
            _LevelSelectColumn = new ColumnHeader {Text = "", Width = 24};
            Columns.AddRange(new[] {_LevelSelectColumn});
            CheckBoxes = true;
            FullRowSelect = true;
            GridLines = true;
            Location = new System.Drawing.Point(13, 24);
            MultiSelect = false;
            ShowGroups = false;
            UseCompatibleStateImageBehavior = false;
            View = View.Details;
        }

        private ColumnHeader _LevelSelectColumn;
    }
}
