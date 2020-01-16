using System;
using MeterKnife.Util.Interface;
using Microsoft.Win32;

namespace MeterKnife.Util.Wrapper
{
    public class LocalRegistry : IRegistry
    {
        #region Implementation of IRegistry

        /// <summary>
        /// 创建子键
        /// </summary>
        /// <param name="sunbkey">参数sunbkey表示要创建的子键的名称或路径名。创建成功返回被创建的子键，否则返回null。</param>
        /// <returns></returns>
        public RegistryKey CreateSubKey(string sunbkey)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 打开子键
        /// </summary>
        /// <param name="name">参数name表示要打开的子键名或其路径名</param>
        /// <returns></returns>
        public RegistryKey OpenSubKey(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 打开子键
        /// </summary>
        /// <param name="name">参数name表示要打开的子键名或其路径名</param>
        /// <param name="writable">是否以只读方式打开子键</param>
        /// <returns></returns>
        public RegistryKey OpenSubKey(string name, bool writable)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除子键
        /// </summary>
        /// <param name="subkey">如果要删除的子键还包含主键则删除失败，并返回一个异常</param>
        public void DeleteKey(string subkey)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 如果要彻底删除该子键及其目录下的子键
        /// </summary>
        /// <param name="subkey"></param>
        public void DeleteKeyTree(string subkey)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 读取键值的方法原型
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public object GetValue(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">参数name表示键的名称</param>
        /// <param name="defaultValue">如果失败又不希望返回的值是null则可以指定参数defaultValue</param>
        /// <returns>返回类型是一个object类型</returns>
        public object GetValue(string name, object defaultValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 设置键值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public object SetValue(string name, object value)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
