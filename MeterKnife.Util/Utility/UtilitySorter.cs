using System;
using System.Data;
using System.Reflection;

namespace NKnife.Utility
{
    /// <summary>  
    /// 排序帮助类（包括对string[],int[],datatable,T[]进行排序）  
    /// </summary>  
    public class UtilitySorter
    {
        /// <summary>  
        /// 对int数组进行排序  
        /// </summary>  
        /// <param name="list">int数组</param>  
        /// <param name="asc">是否按升序排列</param>  
        public static void SortIntArray(int[] list, bool asc)
        {
            int obj;
            int j, k = 1;
            bool done = false;
            int len = list.Length;
            while (k < len && !done)
            {
                done = true;
                for (j = 0; j < len - k; j++)
                {
                    int b = list[j].CompareTo(list[j + 1]);
                    if ((asc && b > 0) || (!asc && b < 0))
                    {
                        done = false;
                        obj = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = obj;
                    }
                }
                k++;
            }
        }
        /// <summary>  
        /// 对string数组进行排序  
        /// </summary>  
        /// <param name="list">string数组</param>  
        /// <param name="asc">是否按升序排列</param>  
        public static void SortStringArray(string[] list, bool asc)
        {
            string obj;
            int j, k = 1;
            bool done = false;
            int len = list.Length;
            while (k < len && !done)
            {
                done = true;
                for (j = 0; j < len - k; j++)
                {
                    int b = list[j].CompareTo(list[j + 1]);
                    if ((asc && b > 0) || (!asc && b < 0))
                    {
                        done = false;
                        obj = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = obj;
                    }
                }
                k++;
            }
        }
        /// <summary>  
        /// 对datatable进行排序，返回排序后的datatable  
        /// </summary>  
        /// <param name="dt">要排序的datatable</param>  
        /// <param name="order">排序字段</param>  
        /// <param name="asc">是否升序</param>  
        /// <returns></returns>  
        static DataTable SortDataTable(DataTable dt, string order, bool asc)
        {
            DataView view = dt.DefaultView;
            view.Sort = string.Format("{0} {1}", order, (asc ? "asc" : "desc"));
            return view.ToTable();
            /* 
            DataRow[] rows = dt.Select("", order + " " + (asc ? "asc" : "desc")); 
            DataTable dt2 = dt.Clone(); 
            foreach (DataRow row in rows) 
                dt2.Rows.Add(row.ItemArray); 
            dt.Clear(); 
            return dt2; 
            */
        }
        /// <summary>  
        /// 对实体类进行排序  
        /// </summary>  
        /// <typeparam name="T">实体类型，如：User</typeparam>  
        /// <param name="list">实体类的数组</param>  
        /// <param name="order">排序字段（必须为属性）</param>  
        /// <param name="asc">是否按正序排序</param>  
        public static void Sort<T>(object[] list, string order, bool asc)
        {
            Type type = typeof(T);
            PropertyInfo[] pros = type.GetProperties();
            PropertyInfo pro = pros[0];
            order = order.ToLower();
            for (int i = 0; i < pros.Length; i++)
            {
                if (pros[i].Name.ToLower().Equals(order))
                {
                    pro = pros[i];
                    break;
                }
            }
            object obj;
            int j, k = 1;
            bool done = false;
            int len = list.Length;
            while (k < len && !done)
            {
                done = true;
                for (j = 0; j < len - k; j++)
                {
                    int b = pro.GetValue(list[j], null).ToString().CompareTo(pro.GetValue(list[j + 1], null).ToString());
                    if ((asc && b > 0) || (!asc && b < 0))
                    {
                        done = false;
                        obj = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = obj;
                    }
                }
                k++;
            }
        }
    }
}