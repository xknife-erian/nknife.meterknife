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
    public class EngineeringLogic : IEngineeringLogic
    {
        private readonly IStorageManager _storageManager;
        private readonly IStorageDUTWrite<Engineering> _engineeringStorageDUTWrite;
        private readonly IStoragePlatform<Engineering> _engineeringStoragePlatform;
        private readonly IMeasuringLogic _performLogic;

        public EngineeringLogic(IStorageManager storageManager, IStoragePlatform<Engineering> engineeringStoragePlatform, IStorageDUTWrite<Engineering> engineeringStorageDUTWrite,
            IMeasuringLogic performLogic)
        {
            _storageManager = storageManager;
            _engineeringStoragePlatform = engineeringStoragePlatform;
            _engineeringStorageDUTWrite = engineeringStorageDUTWrite;
            _performLogic = performLogic;
        }

        #region Implementation of IEngineeringLogic

        /// <summary>
        /// 新建一个测量工程
        /// </summary>
        /// <returns>是否创建成功</returns>
        public async Task<bool> CreateEngineeringAsync(Engineering engineering)
        {
            //创建工程的数据库或者文件
            BuildEngineeringStore(engineering);
            //将工程的相关信息存入到工程库
            await _engineeringStorageDUTWrite.InsertAsync(engineering);
            SetDUTMap(_performLogic, engineering.CommandPools, engineering);
            //同时也在平台库中存储一份
            return await _engineeringStoragePlatform.InsertAsync(engineering);
        }

        /// <summary>
        /// 创建工程的数据库或者文件
        /// </summary>
        private void BuildEngineeringStore(Engineering engineering)
        {
            if (string.IsNullOrEmpty(engineering.Name))
            {
                var sb = new StringBuilder();
                foreach (var pool in engineering.CommandPools)
                {
                    foreach (var command in pool)
                    {
                        sb.Append(command.DUT.Name).Append('/');
                    }
                }

                engineering.Name = sb.ToString();
            }

            _storageManager.CreateEngineering(engineering);
        }

        private static void SetDUTMap(IMeasuringLogic performLogic, List<ScpiCommandPool> commands, Engineering engineering)
        {
            foreach (var pool in commands)
            {
                foreach (var command in pool)
                {
                    if (command.DUT != null)
                        performLogic.SetDUT(command.DUT.Id, (engineering, command.DUT));
                    //TODO:if(command.IsPool)
                    //SetDUTMap(performLogic, command, engineering);
                }
            }
        }

        #endregion
    }
}
