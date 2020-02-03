﻿using System;
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
        private readonly IStoragePlatform<Engineering> _engineeringStoragePlatform;
        private readonly IStorageDUTWrite<Engineering> _engineeringStorageDUTWrite;
        private readonly IPerformStorageLogic _performLogic;

        public EngineeringLogic(IStoragePlatform<Engineering> engineeringStoragePlatform, IStorageDUTWrite<Engineering> engineeringStorageDUTWrite, IPerformStorageLogic performLogic)
        {
            _engineeringStoragePlatform = engineeringStoragePlatform;
            _engineeringStorageDUTWrite = engineeringStorageDUTWrite;
            _performLogic = performLogic;
        }

        #region Implementation of IEngineeringLogic

        /// <summary>
        /// 新建一个测量工程
        /// </summary>
        /// <returns>是否创建成功</returns>
        public async Task<bool> CreateEngineering(Engineering engineering)
        {
            //创建工程的数据库或者文件
            BuildEngineeringStore(engineering);
            //将工程的相关信息存入到工程库
            await _engineeringStorageDUTWrite.InsertAsync(engineering);
            SetDUTMap(_performLogic, engineering.Commands, engineering);
            //同时也在平台库中存储一份
            return await _engineeringStoragePlatform.InsertAsync(engineering);
        }

        private static void SetDUTMap(IPerformStorageLogic performLogic, CareCommandPool commands, Engineering engineering)
        {
            foreach (var command in commands)
            {
                if (!command.IsCare)
                {
                    var bs = command.Scpi.GenerateProtocol(command.GpibAddress);
                    performLogic.SetDUT(bs, (engineering, command.DUT));
                }
                else
                    performLogic.SetDUT(new[] {command.Heads.Item1, command.Heads.Item2}, (engineering, command.DUT));

                //if(command.IsPool)
                //SetDUTMap(performLogic, command, engineering);
            }
        }

        private void BuildEngineeringStore(Engineering engineering)
        {
            _engineeringStoragePlatform.Create(engineering);
        }

        #endregion
    }
}
