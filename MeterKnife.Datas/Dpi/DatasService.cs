using System;
using System.IO;
using System.Linq;
using Common.Logging;
using LiteDB;
using MeterKnife.Events;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Measures;
using MeterKnife.Models;
using NKnife.Events;
using NKnife.IoC;
using NKnife.Utility;

namespace MeterKnife.Datas.Dpi
{
    public class DatasService : IDatasService, IDisposable
    {
        private static readonly ILog Logger = LogManager.GetLogger<DatasService>();

        private LiteDatabase _globalDatabase;

        public LiteDatabase GlobalDataBase => _globalDatabase;

        private MeasureJobRepository _measureJobs;

        #region IDisposable

        /// <summary>执行与释放或重置非托管资源相关的应用程序定义的任务。</summary>
        public void Dispose()
        {
            _globalDatabase?.Dispose();
        }

        #endregion

        #region Implementation of IEnvironmentItem

        public bool StartService()
        {
            try
            {
                if (_globalDatabase == null)
                {
                    var fullpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Datas\");
                    if (!Directory.Exists(fullpath))
                        UtilityFile.CreateDirectory(fullpath);
                    fullpath = Path.Combine(fullpath, "mk.litedb");
                    _globalDatabase = new LiteDatabase(fullpath);
                }
                MeasureEvent();
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                return false;
            }
        }

        public bool CloseService()
        {
            try
            {
                _globalDatabase?.Dispose();
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                return false;
            }
        }

        public int Order { get; } = 1000;
        public string Description { get; } = "LiteDb全局数据库服务";

        #endregion

        private void MeasureEvent()
        {
            _measureJobs = new MeasureJobRepository();
            var measureService = DI.Get<IMeasureService>();
            measureService.Measured += OnMeasured;
            measureService.ExhibitAdded += MeasureService_ExhibitAdded;
            measureService.ExhibitRemoved += MeasureService_ExhibitRemoved;
            measureService.MeasureJobAdded += MeasureService_MeasureJobAdded;
            measureService.MeasureJobRemoved += MeasureService_MeasureJobRemoved;
        }

        private void MeasureService_MeasureJobRemoved(object sender, EventArgs<MeasureJob> e)
        {
        }

        private void MeasureService_MeasureJobAdded(object sender, EventArgs<MeasureJob> e)
        {
            Console.WriteLine($"--------->MeasureJob Count: {_measureJobs.FindAll().Count()}");
            _measureJobs.Save(e.Item);
        }

        private void MeasureService_ExhibitRemoved(object sender, EventArgs<IExhibit> e)
        {
            throw new NotImplementedException();
        }

        private void MeasureService_ExhibitAdded(object sender, EventArgs<IExhibit> e)
        {
            throw new NotImplementedException();
        }

        private void OnMeasured(object sender, MeasureEventArgs e)
        {
            var jobId = e.JobId;
            var exhibitId = e.ExhibitId;
            var time = e.Time;
            var value = e.Value;
        }
    }
}