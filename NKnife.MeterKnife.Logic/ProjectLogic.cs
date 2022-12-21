using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using NKnife.Db;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.Storage.Base;

namespace NKnife.MeterKnife.Logic
{
    public class ProjectLogic : IProjectLogic
    {
        private readonly IStorageManager _storageManager;
        private readonly IStorageDUTWrite<Project> _projectStorageDUTWrite;
        private readonly IStorageDUTRead<MeasureData> _measureDataRead;
        private readonly IStoragePlatform<Project> _projectStoragePlatform;
        private readonly IMeasuringLogic _performLogic;

        public ProjectLogic(IStorageManager storageManager, IStoragePlatform<Project> projectStoragePlatform, IStorageDUTWrite<Project> projectStorageDUTWrite,
            IMeasuringLogic performLogic, IStorageDUTRead<MeasureData> measureDataRead)
        {
            _storageManager = storageManager;
            _projectStoragePlatform = projectStoragePlatform;
            _projectStorageDUTWrite = projectStorageDUTWrite;
            _performLogic = performLogic;
            _measureDataRead = measureDataRead;
        }

        #region Implementation of IProjectLogic

        /// <summary>
        /// 新建一个测量工程
        /// </summary>
        /// <returns>是否创建成功</returns>
        public async Task<bool> CreateProjectAsync(Project project)
        {
            //创建工程的数据库或者文件
            BuildEngineeringStore(project);
            //将工程的相关信息存入到工程库
            await _projectStorageDUTWrite.InsertAsync(project);
            _performLogic.SetDUTMap(project.CommandPools, project);
            //同时也在平台库中存储一份
            return await _projectStoragePlatform.InsertAsync(project);
        }

        /// <summary>
        ///     修改一个测量工程
        /// </summary>
        public async Task UpdateProjectAsync(Project project)
        {
            await _projectStoragePlatform.UpdateAsync(project);
            await _projectStorageDUTWrite.UpdateAsync(project);
        }

        /// <summary>
        ///     获取指定被测物的测量数据
        /// </summary>
        /// <param name="dut">指定被测物</param>
        public async Task<IEnumerable<MeasureData>> GetDUTDataAsync((Project, DUT) dut)
        {
            return await _measureDataRead.FindAllAsync(dut);
        }

        /// <summary>
        ///     删除一个指定的工程
        /// </summary>
        /// <param name="eng">指定的工程</param>
        public async Task RemoveProjectAsync(Project eng)
        {
            await _projectStoragePlatform.RemoveAsync(eng.Id);
        }

        /// <summary>
        /// 创建工程的数据库或者文件
        /// </summary>
        private void BuildEngineeringStore(Project project)
        {
            if (string.IsNullOrEmpty(project.Name))
            {
                var sb = new StringBuilder();
                foreach (var pool in project.CommandPools)
                {
                    foreach (ScpiCommand command in pool)
                    {
                        sb.Append(command.DUT.Name).Append('/');
                    }
                }

                project.Name = sb.ToString().TrimEnd('/');
            }

            _storageManager.CreateEngineering(project);
        }

        #endregion
    }
}
