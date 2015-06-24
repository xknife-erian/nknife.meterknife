using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeterKnife.Common.Interfaces
{
    /// <summary>
    /// 一个描述电子行业的数据采集的实时算法的接口
    /// </summary>
    public interface IElectronAlgorithm
    {
        /// <summary>
        /// 输出数据
        /// </summary>
        double Output { get; }
        /// <summary>
        /// 输入数据
        /// </summary>
        /// <param name="src">指定的输入数据</param>
        void Input(params double[] src);
    }
}
