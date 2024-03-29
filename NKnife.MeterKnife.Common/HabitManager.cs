﻿using System.IO;
using System.Xml;
using Newtonsoft.Json;
using NKnife.MeterKnife.Base;
using NKnife.XML;
using NLog;

namespace NKnife.MeterKnife.Common
{
    /// <summary>
    ///     软件的“使用习惯记录文件”的记录保存与读取服务
    /// </summary>
    public class HabitManager : IHabitManager
    {
        private const string OPTION_FILE = "Habit.xml";
        private readonly string _optionFile;
        private readonly XmlDocument _optionXml;

        public HabitManager(IPathManager pathManager)
        {
            _optionFile = Path.Combine(pathManager.UserApplicationDataPath, OPTION_FILE);
            if (!File.Exists(_optionFile))
            {
                _optionXml = XmlHelper.CreateNewDocument(_optionFile, "options");
            }
            else
            {
                _optionXml = new XmlDocument();
                _optionXml.Load(_optionFile);
            }
        }

        /// <summary>
        ///     尝试获取指定Key的使用习惯的值
        /// </summary>
        public T GetHabitValue<T>(string key, T defaultValue)
        {
            return GetOptionValue<T>(key, defaultValue);
        }

        /// <summary>
        ///     设置指定Key的使用习惯的值，值对象序列化成Json保存
        /// </summary>
        public void SetHabitValue(string key, object value)
        {
            SetOptionValue(key, value);
        }

        /// <summary>
        ///     尝试获取指定Key的选项的值
        /// </summary>
        public T GetOptionValue<T>(string key, T defaultValue)
        {
            var ele = _optionXml.DocumentElement?.SelectSingleNode(key);
            if (ele == null)
            {
                SetOptionValue(key, defaultValue);
                ele = _optionXml.DocumentElement?.SelectSingleNode(key);
            }

            return JsonConvert.DeserializeObject<T>(ele.GetCDataElement().Value);
        }

        /// <summary>
        ///     设置指定Key的选项的值，值对象序列化成Json保存
        /// </summary>
        public void SetOptionValue(string key, object value)
        {
            var ele = _optionXml.DocumentElement?.SelectSingleNode(key) as XmlElement ?? _optionXml.CreateElement(key);
            ele.RemoveAll();
            var json = JsonConvert.SerializeObject(value);
            ele.SetCDataElement(json);
            _optionXml.DocumentElement?.AppendChild(ele);
            _optionXml.Save(_optionFile);
        }

    }

    public static class HabitKey
    {
        #region General

        #endregion

        #region Data

        /// <summary>
        /// 用户测量数据的保存路径。关键选项。
        /// </summary>
        public const string Data_MetricalData_Path = nameof(Data_MetricalData_Path);

        #endregion

        #region Plot

        /// <summary>
        ///     采集数据时，丢弃开始的数据个数
        /// </summary>
        public static string Plot_DroppedDataCount = nameof(Plot_DroppedDataCount);

        public static string Plot_PlotThemes = nameof(Plot_PlotThemes);
        public static string Plot_UsingTheme = nameof(Plot_UsingTheme);

        /// <summary>
        ///     图表上下留白占比。
        /// </summary>
        public const string Plot_YSpace = nameof(Plot_YSpace);

        #endregion
    }
}