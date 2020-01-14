using System;

namespace NKnife.Interface
{
    /// <summary>
    /// 值转换成特定类型的接口
    /// </summary>
    public interface IConverting
    {
        #region Converting

        /// <summary>
        /// 值转换成String类型
        /// </summary>
        /// <returns>String</returns>
        string ToString();

        /// <summary>
        /// 值转换成Byte类型
        /// </summary>
        /// <returns>Byte</returns>
        byte ToByte();

        /// <summary>
        /// 值转换成Char类型
        /// </summary>
        /// <returns>Char</returns>
        char ToChar();

        /// <summary>
        /// 值转换成DateTime类型
        /// </summary>
        /// <returns>DateTime</returns>
        DateTime ToDateTime();

        /// <summary>
        /// 值转换成Int16类型（C#为short）
        /// </summary>
        /// <returns>Int16</returns>
        short ToInt16();

        /// <summary>
        /// 值转换成Int32类型（C#为int）
        /// </summary>
        /// <returns>Int32</returns>
        int ToInt32();

        /// <summary>
        /// 值转换成Int64类型（C#为long）
        /// </summary>
        /// <returns></returns>
        long ToInt64();

        /// <summary>
        /// 值转换成SByte类型
        /// </summary>
        /// <returns>SByte</returns>
        sbyte ToSByte();

        /// <summary>
        /// 值转换成Double类型
        /// </summary>
        /// <returns>Double</returns>
        double ToDouble();

        /// <summary>
        /// 值转换成Decimal类型
        /// </summary>
        /// <returns>Decimal</returns>
        decimal ToDecimal();

        /// <summary>
        /// 值转换成Single类型（C#为float）
        /// </summary>
        /// <returns>Single</returns>
        float ToSingle();

        /// <summary>
        /// 值转换成Single类型（C#为ushort）
        /// </summary>
        /// <returns>UInt16</returns>
        ushort ToUInt16();

        /// <summary>
        /// 值转换成UInt32类型（C#为uint）
        /// </summary>
        /// <returns>UInt32</returns>
        uint ToUInt32();

        /// <summary>
        /// 值转换成UInt64类型（C#为ulong）
        /// </summary>
        /// <returns>UInt64</returns>
        ulong ToUInt64();

        /// <summary>
        /// 值转换成Boolean类型（C#为bool）
        /// </summary>
        /// <returns>Boolean</returns>
        bool ToBoolean();

        /// <summary>
        /// 如果保存的是Type信息，则转换成Type
        /// </summary>
        /// <returns>Type</returns>
        Type ToType();

        /// <summary>
        /// 如果保存的是Type信息，则转换成Type
        /// </summary>
        /// <param name="throwError">是否抛出异常</param>
        /// <returns>Type</returns>
        Type ToType(bool throwError);

        /// <summary>
        /// 根据值创建对象
        /// </summary>
        /// <returns>对象</returns>
        object ToObject();

        /// <summary>
        /// 根据值创建对象
        /// </summary>
        /// <param name="parameters">构造函数的参数</param>
        /// <returns>对象</returns>
        object ToObject(params object[] parameters);

        /// <summary>
        /// 根据值创建对象
        /// </summary>
        /// <param name="expectedType">期望的类型</param>
        /// <param name="throwError">是否抛出异常</param>
        /// <param name="parameters">构造函数的参数</param>
        /// <returns>对象</returns>
        object ToObject(Type expectedType, bool throwError, params object[] parameters);

        /// <summary>
        /// 根据值创建对象
        /// </summary>
        /// <param name="expectedType">期望的类型</param>
        /// <param name="throwError">是否抛出异常</param>
        /// <param name="paramTypes">构造函数的参数类型列表</param>
        /// <param name="paramValues">构造函数的参数列表</param>
        /// <returns>对象</returns>
        object ToObject(Type expectedType, bool throwError, Type[] paramTypes, object[] paramValues);

        /// <summary>
        /// 转换成指定类型
        /// </summary>
        /// <typeparam name="T">转换成的类型</typeparam>
        /// <returns>转换后的类型实例</returns>
        T ToObject<T>();

        /// <summary>
        /// 转换成指定类型（带参数）
        /// </summary>
        /// <typeparam name="T">转换成的类型</typeparam>
        /// <param name="parameters">可变参数列表</param>
        /// <returns>转换后的类型实例</returns>
        T ToObject<T>(params object[] parameters);

        /// <summary>
        /// 转换成指定类型（带参数）
        /// </summary>
        /// <typeparam name="T">转换成的类型</typeparam>
        /// <param name="throwError">如果失败，是否抛出异常</param>
        /// <param name="parameters">可变参数列表</param>
        /// <returns>转换后的类型实例</returns>
        T ToObject<T>(bool throwError, params object[] parameters);

        /// <summary>
        /// 转换成指定类型（带参数）
        /// </summary>
        /// <typeparam name="T">转换成的类型</typeparam>
        /// <param name="throwError">如果失败，是否抛出异常</param>
        /// <param name="paramTypes">参数类型列表</param>
        /// <param name="paramValues">参数值列表</param>
        /// <returns>转换后的类型实例</returns>
        T ToObject<T>(bool throwError, Type[] paramTypes, object[] paramValues);

        /// <summary>
        /// 尝试转换成指定类型
        /// </summary>
        /// <typeparam name="T">转换成的类型</typeparam>
        /// <returns>转换后的类型实例</returns>
        T TryToObject<T>();

        /// <summary>
        /// 尝试转换成指定类型
        /// </summary>
        /// <typeparam name="T">转换成的类型</typeparam>
        /// <param name="defaultValue">如果转换失败返回的缺省值</param>
        /// <returns>转换后的类型实例</returns>
        T TryToObject<T>(T defaultValue);

        #endregion Converting
    }
}
