using System.Collections.Generic;
using NKnife.Db;
using NKnife.MeterKnife.Common.Domain;

// ReSharper disable once CheckNamespace
namespace NKnife.MeterKnife.Base
{
    /// <summary>
    ///     软件全局的一些数据
    /// </summary>
    public interface IGlobal
    {
        /// <summary>
        ///     软件使用的数据库模式
        /// </summary>
        DatabaseType DatabaseType { get; set; }

        /// <summary>
        ///     串口下的Gpib地址
        /// </summary>
        Dictionary<Slot, List<int>> GpibDictionary { get; }
    }
}