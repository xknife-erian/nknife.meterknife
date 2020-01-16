using System.Data;
using System.Windows.Forms;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class ListViewExtension
    {
        /// <summary>从一个DataTable中获取数据并进行填充
        /// </summary>
        /// <param name="listview">The lv.</param>
        /// <param name="dt">The dt.</param>
        public static void FromDataTable(this ListView listview, DataTable dt)
        {
            if (dt != null)
            {
                listview.Items.Clear();
                listview.Columns.Clear();
                // 向ListView中添加列
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    var ch = new ColumnHeader();
                    ch.Name = dt.Columns[i].ColumnName;
                    ch.Text = dt.Columns[i].Caption;
                    listview.Columns.Add(ch);
                }
                foreach (DataRow dr in dt.Rows)
                {
                    var lvi = new ListViewItem();
                    lvi.SubItems[0].Text = dr[0].ToString();

                    for (int i = 1; i < dt.Columns.Count; i++)
                    {
                        lvi.SubItems.Add(dr[i].ToString());
                    }

                    listview.Items.Add(lvi);
                }
                listview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        /// <summary>将View里的数据向DataTable进行转换。
        /// </summary>
        /// <param name="lv">The lv.</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static void WriteDataTable(this ListView lv, DataTable dt)
        {
            if (dt == null)
                return;
            dt.Clear();
            dt.Columns.Clear();
            // 生成DataTable列头
            for (int i = 0; i < lv.Columns.Count; i++)
            {
                var dc = new DataColumn();
                dc.ColumnName = lv.Columns[i].Name;
                dc.Caption = lv.Columns[i].Text;
                dt.Columns.Add(dc);
            }
            // 每行内容
            for (int i = 0; i < lv.Items.Count; i++)
            {
                DataRow dr = dt.NewRow();
                int j;
                for (j = 0; j < lv.Columns.Count; j++)
                {
                    dr[j] = lv.Items[i].SubItems[j].Text.Trim();
                }
                dt.Rows.Add(dr);
            }
        }
    }
}