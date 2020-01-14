using System.Data;

namespace NKnife.Databases.Interface
{
    public interface IDataBaseParameters
    {
        /// <summary>
        /// 利用反射构造Parameters
        /// </summary>
        IDataParameter[] CreateParams(object domainObj);

        /// <summary>
        /// 不用反射构造Parameters
        /// </summary>
        IDataParameter[] CreateParams(string[] paramNames, object[] paramValues);

        /// <summary>
        /// 组合Parameters
        /// </summary>
        IDataParameter[] CombineParams(IDataParameter[] oldParms, params IDataParameter[] newParms);

        /// <summary>
        /// 创建单个Parameter
        /// </summary>
        IDataParameter CreateParam(string name, object val);
    }
}
