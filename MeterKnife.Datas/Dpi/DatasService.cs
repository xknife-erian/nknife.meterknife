using System;
using System.IO;
using Common.Logging;
using LiteDB;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Measures;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.Utility;

namespace MeterKnife.Datas.Dpi
{
    public class DatasService : IDatasService, IDisposable
    {
        private static readonly ILog _logger = LogManager.GetLogger<DatasService>();

        private LiteDatabase _Database;

        public LiteDatabase DataBase => _Database;

        #region IDisposable

        /// <summary>执行与释放或重置非托管资源相关的应用程序定义的任务。</summary>
        public void Dispose()
        {
            _Database?.Dispose();
        }

        #endregion

        #region Implementation of IEnvironmentItem

        public bool StartService()
        {
            try
            {
                if (_Database == null)
                {
                    var fullpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Datas\");
                    if (!Directory.Exists(fullpath))
                        UtilityFile.CreateDirectory(fullpath);
                    fullpath = Path.Combine(fullpath, "mk.litedb");
                    _Database = new LiteDatabase(fullpath);
                }
                var measureService = DI.Get<IMeasureService>();
                measureService.Measured += OnMeasured;

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return false;
            }
        }

        public bool CloseService()
        {
            try
            {
                _Database?.Dispose();
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return false;
            }
        }

        public int Order { get; } = 1000;
        public string Description { get; } = "LiteDb全局数据库服务";

        #endregion

        private void OnMeasured(object sender, MeasureEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}