using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using NKnife.Db;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Storage.Base;

namespace NKnife.MeterKnife.Logic
{
    public class EngineeringLogic : IEngineeringLogic
    {
        private readonly IStoragePlatform<Engineering> _engineeringStorage;

        public EngineeringLogic(IStoragePlatform<Engineering> engineeringStorage)
        {
            _engineeringStorage = engineeringStorage;
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
            return await _engineeringStorage.InsertAsync(engineering);
        }

        private void BuildEngineeringStore(Engineering engineering)
        {
            _engineeringStorage.Create(engineering);
        }

        #endregion
    }
}
