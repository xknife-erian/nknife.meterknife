using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using MeterKnife.Util.ShareResources;
using MeterKnife.Util.Utility;
using NLog;

namespace MeterKnife.Util.NLog3.Controls
{
    /// <summary>展示日志的具体ListView，同时双缓冲解决闪烁
    /// </summary>
    public class NLogListView : ListView
    {
        private static readonly ConcurrentDictionary<string, ListViewGroup> _LevelGroups = new ConcurrentDictionary<string, ListViewGroup>();

        private int _MaxRowCount = 120;

        public NLogListView()
        {
            if (_LevelGroups.Count <= 0)
            {
                _LevelGroups.TryAdd(LogLevel.Trace.Name, new ListViewGroup(LogLevel.Trace.Name));
                _LevelGroups.TryAdd(LogLevel.Debug.Name, new ListViewGroup(LogLevel.Debug.Name));
                _LevelGroups.TryAdd(LogLevel.Info.Name, new ListViewGroup(LogLevel.Info.Name));
                _LevelGroups.TryAdd(LogLevel.Warn.Name, new ListViewGroup(LogLevel.Warn.Name));
                _LevelGroups.TryAdd(LogLevel.Error.Name, new ListViewGroup(LogLevel.Error.Name));
                _LevelGroups.TryAdd(LogLevel.Fatal.Name, new ListViewGroup(LogLevel.Fatal.Name));
            }

            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();

            var timeHeader = new ColumnHeader();
            timeHeader.Text = UtilityResource.GetString(StringResource.ResourceManager, "LogPanel_Time_Header");
            timeHeader.Width = 80;

            var logMessageHeader = new ColumnHeader();
            logMessageHeader.Text = UtilityResource.GetString(StringResource.ResourceManager, "LogPanel_Info_Header");
            logMessageHeader.Width = 380;

            var loggerNameHeader = new ColumnHeader();
            loggerNameHeader.Text = UtilityResource.GetString(StringResource.ResourceManager, "LogPanel_Source_Header");
            loggerNameHeader.Width = 200;

            Columns.AddRange(
                new[]
                    {
                        timeHeader,
                        logMessageHeader,
                        loggerNameHeader
                    });
            GridLines = true;
            MultiSelect = false;
            FullRowSelect = true;
            View = View.Details;
            ShowItemToolTips = true;
            MouseDoubleClick += LoggerListViewDoubleClick;
        }

        /// <summary>控件中最大显示行数，默认120行，即保留最后120条日志
        /// </summary>
        /// <value>The max row count.</value>
        public int MaxRowCount
        {
            get { return _MaxRowCount; }
            set { _MaxRowCount = value; }
        }

        /// <summary>添加一条日志
        /// </summary>
        /// <param name="logEvent">The log event.</param>
        internal void AddLog(LogEventInfo logEvent)
        {
            if (InvokeRequired)
                BeginInvoke(new AddLogToControlDelegate(AddLog), new object[] {logEvent});
            else
                AddLogMethod(logEvent);
        }

        /// <summary>添加一条日志的线程方法
        /// </summary>
        /// <param name="logEvent">The log event.</param>
        private void AddLogMethod(LogEventInfo logEvent)
        {
            try
            {
                if (Items.Count > MaxRowCount)
                {
                    for (int i = Items.Count - 1; i > MaxRowCount*0.9; i--)
                        Items.RemoveAt(i);
                }
                string logDatetime = logEvent.TimeStamp.ToString("HH:mm:ss fff");
                string logMsg = logEvent.FormattedMessage;
                string logLoggerName = "";
                if (logEvent.LoggerName.LastIndexOf('.') > 0)
                {
                    logLoggerName = ParseLoggerName(logEvent.LoggerName);
                }
                else
                {
                    logLoggerName = logEvent.LoggerName;
                }
                if (logEvent.HasStackTrace)
                    logMsg += logEvent.StackTrace.ToString();

                var viewItem = new ListViewItem();
                viewItem.Tag = logEvent;
                //TODO:日志分组导致出现一些无法理解的异常，暂时未处理
//            ListViewGroup group;
//            _LevelGroups.TryGetValue(logEvent.Level.Name, out group);
//            if (group != null)
//                viewItem.Group = group;
                viewItem.Text = logDatetime;
                viewItem.SubItems.Add(new ListViewItem.ListViewSubItem(viewItem, logMsg));
                viewItem.SubItems.Add(new ListViewItem.ListViewSubItem(viewItem, logLoggerName));

                switch (logEvent.Level.Name)
                {
                    case "Trace":
                        viewItem.ForeColor = Color.FromArgb(128, 64, 64);
                        break;
                    case "Debug":
                        viewItem.ForeColor = Color.FromArgb(64, 0, 0);
                        break;
                    case "Info":
                        break;
                    case "Warn":
                        viewItem.BackColor = Color.Yellow;
                        break;
                    case "Error":
                        viewItem.BackColor = Color.Orange;
                        break;
                    case "Fatal":
                        viewItem.ForeColor = Color.Yellow;
                        viewItem.BackColor = Color.DarkRed;
                        break;
                }
                Items.Insert(0, viewItem);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public string ParseLoggerName(string nameSource)
        {
            try
            {
                if (nameSource.Contains("[["))
                {
                    var ns = nameSource.Substring(0, nameSource.LastIndexOf("`1"));
                    var className = ns.Substring(ns.LastIndexOf('.') + 1);

                    var fxIndex = nameSource.LastIndexOf("[[") + 2;
                    var fx = nameSource.Substring(fxIndex, nameSource.IndexOf(',', fxIndex) - fxIndex);
                    return string.Format("{0}<{1}>", className, fx.Substring(fx.LastIndexOf('.') + 1));
                }
                return nameSource.Substring(nameSource.LastIndexOf('.') + 1);
            }
            catch (Exception)
            {
                return nameSource;
            }
        }

        /// <summary>双击一条日志弹出详细窗口
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        protected void LoggerListViewDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo si = HitTest(e.X, e.Y);
            if (si.Item != null)
            {
                var info = (LogEventInfo) si.Item.Tag;
                if (info != null)
                {
                    LoggerInfoDetailForm.Show(info);
                }
            }
        }

        #region Nested type: AddLogToControlDelegate

        private delegate void AddLogToControlDelegate(LogEventInfo logEvent);

        #endregion
    }
}