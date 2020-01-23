using System;
using System.Collections.Generic;
using System.Text;
using NKnife.MeterKnife.Common.Domain;

// ReSharper disable once CheckNamespace
namespace NKnife.MeterKnife.Base
{
    public interface IEngineeringLogic
    {
        /// <summary>
        /// 新建一个测量工程
        /// </summary>
        /// <returns>是否创建成功</returns>
        bool CreateEngineering(Engineering engineering);
    }
}
