using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScpiKnife
{
    /// <summary>
    /// Scpi保存文件可能发生版本变化，定义本接口控制未来的变化
    /// </summary>
    public interface IScpiFileVersionProcessor
    {
        ScpisXmlFile Update(ScpisXmlFile scpiFile, bool isSave);
    }
}
