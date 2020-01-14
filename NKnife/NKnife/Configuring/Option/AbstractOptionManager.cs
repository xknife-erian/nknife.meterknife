using System;
using System.Collections.Generic;
using System.Data;
using Common.Logging;
using NKnife.Configuring.Interfaces;
using NKnife.Configuring.OptionCase;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.Utility;

namespace NKnife.Configuring.Option
{
    /// <summary>(核心类)描述一个选项信息的管理与贮存的类型。
    /// </summary>
    public abstract class AbstractOptionManager : IOptionManager
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        #region 成员/属性定义

        protected IOptionDataStore DataStore
        {
            get { return OptionServiceCoderSetting.ME.OptionDataStore; }
        }

        public Dictionary<string, IOption> DefaultTableSchemaMap { get; protected set; }

        /// <summary>获取一个选项实例的集合
        /// </summary>
        /// <value>The solutions.</value>
        public IOptionCaseManager CaseManager
        {
            get { return DataStore.CaseManager; }
        }

        /// <summary>初始化选项管理器
        /// </summary>
        /// <param name="baseTarget">Option持久化的目标</param>
        public abstract void Initializes(object baseTarget);

        #endregion

        #region Implementation of IOptionManager

        private string _CurrentClientId = "";
        private OptionCaseItem _CurrentCase;

        /// <summary>当前应用程序的选项信息组的名称
        /// </summary>
        /// <value>The name of the curr option group.</value>
        public virtual OptionCaseItem CurrentCase
        {
            get { return _CurrentCase; }
            set
            {
                _CurrentCase = value;
                if (!CaseManager.Contains(value))
                    CaseManager.Add(value);
            }
        }

        /// <summary>当前应用程序ID
        /// </summary>
        /// <value>The name of the curr option group.</value>
        public virtual string CurrentClientId
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_CurrentClientId))
                    _CurrentClientId = UtilityHardware.GetMacAddress();
                return _CurrentClientId;
            }
        }

        /// <summary>根据组名+键名获取选项信息
        /// </summary>
        /// <param name="category">选项信息所在的信息表</param>
        /// <param name="key">选项键</param>
        /// <returns></returns>
        public virtual string GetOptionValue(string category, string key)
        {
            return GetOptionValue(category, key, StringFunc);
        }

        /// <summary>根据组名+键名获取选项信息
        /// </summary>
        /// <param name="category">选项信息所在的信息表</param>
        /// <param name="key">选项键</param>
        /// <returns></returns>
        public virtual T GetOptionValue<T>(string category, string key)
            where T : struct
        {
            Type type = typeof(T);
            if (type == typeof(int))
            {
                return (T)((ValueType)GetOptionValue(category, key, IntegerFunc));
            }
            else if (type == typeof(bool))
            {
                return (T)((ValueType)GetOptionValue(category, key, BooleanFunc));
            }
            else if (type == typeof(DateTime))
            {
                return (T)((ValueType)GetOptionValue(category, key, DateTimeFunc));
            }
            return default(T);
        }

        /// <summary>根据组名+键名获取选项信息，并通过解析器将选项信息转换成指定的类型
        /// </summary>
        /// <param name="category">选项信息所在的信息表</param>
        /// <param name="key">选项键</param>
        /// <param name="parser">解析器</param>
        /// <returns></returns>
        public T GetOptionValue<T>(string category, string key, Func<object, T> parser)
        {
            DataRow row = GetRow(category, key, typeof(T));
            object obj = row[key];
            if (obj == null)
                return default(T);
            return parser.Invoke(obj);
        }

        /// <summary>根据组名+键名设置选项信息
        /// </summary>
        /// <param name="category">选项信息所在的信息表</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public virtual bool SetOptionValue(string category, string key, object value)
        {
            if (string.IsNullOrWhiteSpace(category) || string.IsNullOrWhiteSpace(key) || null == value)
            {
                _logger.Warn(string.Format("设置选项值Table:{0},Key:{1},Value:{2}时有空值。", category, key, value));
                return false;
            }
            try
            {
                Type type = value.GetType();
                DataRow row = GetRow(category, key, type);
                row[key] = value;
                var table = (IOption)row.Table;
                return table.IsModified = true;
            }
            catch (Exception e)
            {
                _logger.Error(string.Format("设置选项值时异常。{0}", e.Message), e);
                return false;
            }
        }

        /// <summary>重新加载所有的选项值
        /// </summary>
        /// <returns></returns>
        public virtual bool ReLoad()
        {
            return DataStore.ReLoad();
        }

        /// <summary>清理选项相关的环境、目录等
        /// </summary>
        /// <returns></returns>
        public virtual bool Clean()
        {
            return DataStore.Clean();
        }

        /// <summary>备份选项存储器
        /// </summary>
        /// <returns></returns>
        public virtual object Backup()
        {
            return DataStore.Backup();
        }

        /// <summary>持久化选项信息
        /// </summary>
        /// <returns></returns>
        public virtual bool Save()
        {
            try
            {
                foreach (IOption table in DataStore.DataTables.Values)
                {
                    if (table.IsModified)
                        DataStore.Update(table);
                }
                return DataStore.Save();
            }
            catch (Exception e)
            {
                _logger.Error(string.Format("选项持久化异常。{0}", e.Message), e);
                return false;
            }
        }

        /// <summary>
        /// 更新解决方案的保存(当已存在时就更新，否则添加)
        /// </summary>
        /// <param name="solution">The solution.</param>
        public void AddOrUpdateCaseStore(OptionCaseItem solution)
        {
            DataStore.AddOrUpdateCaseStore(solution);
        }

        /// <summary>删除一个解决方案的保存
        /// </summary>
        /// <param name="solution">The solution.</param>
        public void RemoveCaseStore(OptionCaseItem solution)
        {
            DataStore.RemoveCaseStore(solution);
        }

        /// <summary>以源方案为模板复制一套新的解决方案
        /// </summary>
        /// <param name="srcSolution">源方案.</param>
        public virtual OptionCaseItem CopyCaseFrom(OptionCaseItem srcSolution)
        {
            var targetSolution = new OptionCaseItem();
            foreach (IOption option in DataStore.DataTables.Values)
            {
                var table = (OptionDataTable) option;
                DataRow[] rows = table[srcSolution.Name];
                foreach (DataRow dataRow in rows)
                {
                    DataRow newRow = table.NewRow();
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        newRow[i] = dataRow[i];
                    }
                    newRow["solution"] = targetSolution.Name;
                    table.Rows.Add(newRow);
                }
                table.Update();
            }
            targetSolution.Name = srcSolution.Name + "1";
            CaseManager.Add(targetSolution);
            return targetSolution;
        }

        /// <summary>
        /// 新增一套方案
        /// </summary>
        /// <param name="solution">The solution.</param>
        /// <param name="isStore">True时同时持久化，否则反之</param>
        public virtual void AddCase(OptionCaseItem solution, bool isStore = true)
        {
            CaseManager.Add(solution);
            if (isStore)
                AddOrUpdateCaseStore(solution);
        }

        /// <summary>删除一套方案,将遍历每张表,删除指定内容的行
        /// </summary>
        /// <param name="solution">The solution.</param>
        /// <param name="isStore"></param>
        public virtual void RemoveCase(OptionCaseItem solution, bool isStore = true)
        {
            foreach (IOption option in DataStore.DataTables.Values)
            {
                var table = (OptionDataTable)option;
                DataRow row = table[solution.Name, CurrentClientId];
                if (row != null)
                    table.Rows.Remove(row);
            }
            CaseManager.Remove(solution);
            if (isStore)
                RemoveCaseStore(solution);
        }

        /// <summary>当初始化完成后发生的事件
        /// </summary>
        public event EventHandler LoadedEvent;

        /// <summary>当初始化完成后
        /// </summary>
        protected virtual void OnLoaded(EventArgs e)
        {
            if (LoadedEvent != null)
                LoadedEvent(this, e);
        }

        /// <summary>重点方法。根据指定的表名，键，和数据格式来获取相应的值。当不存在时创建。
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="key">The key.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <returns></returns>
        protected virtual DataRow GetRow(string category, string key, Type dataType)
        {
            OptionDataTable dt = null;
            if (!DataStore.DataTables.ContainsKey(category))
            {
                _logger.Warn(string.Format("配置存储器没有{0}的表", category));
                dt = (OptionDataTable) BuildNewDataTable(category);
                DataStore.DataTables.TryAdd(category, dt);
                _logger.Info(string.Format("创建表名为{0}的默认表", category));
            }
            else
            {
                dt = (OptionDataTable) DataStore.DataTables[category];
            }
            if (dt.Rows == null || dt.Rows.Count <= 0)
            {
                _logger.Warn(string.Format("配置表({0})中暂无Row,即没有合适的数据", category));
                DataRow newrow = dt.NewRow();
                newrow["solution"] = _CurrentCase.Name;
                newrow["clientId"] = CurrentClientId;
                dt.Rows.Add(newrow);
                dt.AcceptChanges();
            }
            DataRow row = dt[_CurrentCase.Name, CurrentClientId];
            if (row == null)
            {
                row = dt.NewRow();
                row["solution"] = _CurrentCase.Name;
                row["clientId"] = CurrentClientId;
                dt.Rows.Add(row);
            }
            if (!row.Table.Columns.Contains(key))
            {
                _logger.Warn(string.Format("配置中没有{0}的列", key));
                var c = new DataColumn(key);
                c.Caption = key;
                c.DataType = dataType;
                dt.Columns.Add(c);
            }
            return row;
        }

        private IOption BuildNewDataTable(string fulltableName)
        {
            IOption option;
            if (DefaultTableSchemaMap != null && DefaultTableSchemaMap.ContainsKey(fulltableName))
            {
                option = DefaultTableSchemaMap[fulltableName];
            }
            else
            {
                _logger.Error(string.Format("默认表集合中没有{0}的表", fulltableName));
                option = DI.Get<IOption>();
                option.Category = fulltableName;
            }
            var table = (OptionDataTable) option;
            if (!table.Columns.Contains("solution"))
                table.Columns.Add("solution");
            if (!table.Columns.Contains("clientId"))
                table.Columns.Add("clientId");
            return option;
        }

        protected static string StringFunc(object obj)
        {
            if (obj != null)
                return obj.ToString();
            return "";
        }

        public static string[] StringArrayFunc(object obj)
        {
            if (obj != null && !(obj is DBNull))
                return (string[])obj;
            return new string[0];
        }

        protected static DateTime DateTimeFunc(object obj)
        {
            if (obj != null)
            {
                DateTime dt;
                if (DateTime.TryParse(obj.ToString(), out dt))
                    return dt;
            }
            return DateTime.Now;
        }

        protected static bool BooleanFunc(object obj)
        {
            if (obj != null)
            {
                bool dt;
                if (bool.TryParse(obj.ToString(), out dt))
                    return dt;
            }
            return false;
        }

        protected static int IntegerFunc(object obj)
        {
            if (obj != null)
            {
                int dt;
                if (int.TryParse(obj.ToString(), out dt))
                    return dt;
            }
            return -1;
        }

        #endregion
    }
}