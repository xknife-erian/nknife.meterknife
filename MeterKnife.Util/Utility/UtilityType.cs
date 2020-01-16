using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using MeterKnife.Util.Base;

namespace MeterKnife.Util.Utility
{
    public static class UtilityType
    {
        /// <summary>
        /// 每次搜索Type是比较耗时的，在这里采用一个字典进行缓存
        /// </summary>
        private static readonly Dictionary<string, Dictionary<string, Type>> _appTypes = new Dictionary<string, Dictionary<string, Type>>();

        /// <summary>
        /// 从程序集中获取程序集实例中具有指定名称的 System.Type 对象。
        /// 当输入assignableFromType时判断该Type是否是从assignableFromType继承。
        /// assignableFromType可以为Null，为Null时不作判断。
        /// </summary>
        /// <param name="assembly">指定的程序集</param>
        /// <param name="classname">类型的全名</param>
        /// <returns>如找到返回该类型，未找到返Null</returns>
        public static Type Load(Assembly assembly, string classname)
        {
            return Load(assembly, classname, null);
        }

        /// <summary>
        /// 从程序集中获取程序集实例中具有指定名称的 System.Type 对象。
        /// 当输入assignableFromType时判断该Type是否是从assignableFromType继承。
        /// assignableFromType可以为Null，为Null时不作判断。
        /// </summary>
        /// <param name="assembly">指定的程序集</param>
        /// <param name="classname">类型的全名</param>
        /// <param name="assignableFromType">被继承的类型</param>
        /// <returns>如找到返回该类型，未找到返Null</returns>
        public static Type Load(Assembly assembly, string classname, Type assignableFromType)
        {
            Type type = assembly.GetType(classname, true, false);
            if (assignableFromType == null)
            {
                return type;
            }
            return assignableFromType.IsAssignableFrom(type) ? type : null;
        }

        /// <summary>
        /// 从类型名称中创建类型
        /// </summary>
        /// <param name="assembly">类型所在程序集.</param>
        /// <param name="typeName">类型名</param>
        /// <param name="throwOnError">失败时是否抛出异常</param>
        /// <returns>Type</returns>
        public static Type CreateType(Assembly assembly, string typeName, bool throwOnError)
        {
            return assembly.GetType(typeName, throwOnError, false);
        }

        /// <summary>
        /// 从类型中创建此类型的实例(一些单例的规范命名的类型也可创建)
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="expectedType">期望的类型</param>
        /// <param name="throwOnError">失败时是否抛出异常</param>
        /// <param name="parameterTypes">创建实例所需参数的类型列表</param>
        /// <param name="parameterValues">创建实例所需的参数值列表</param>
        /// <returns>类型实例</returns>
        public static object CreateObject(Type type, Type expectedType, bool throwOnError, Type[] parameterTypes, object[] parameterValues)
        {
            if (expectedType != null && !expectedType.IsAssignableFrom(type))
            {
                if (throwOnError)
                {
                    throw new Exception(String.Format("将要创建的类型：{0}，不是期望的类型：{1}", type.FullName, expectedType.FullName));
                }
                return null;
            }
            if (parameterTypes != null && parameterValues != null && parameterTypes.Length != parameterValues.Length)
            {
                if (throwOnError)
                {
                    throw new Exception("构造函数参数类型数量和参数数量不一致");
                }
            }
            object createdObject = null;
            if (parameterTypes == null)
            {
                parameterTypes = new Type[] {};
            }
            ConstructorInfo constructor = type.GetConstructor(parameterTypes);
            if (constructor == null)
            {
                try
                {
                    createdObject = CreatedObjectBySingleMode(type, parameterValues);
                }
                catch (Exception)
                {
                    createdObject = CreatedObjectByNonPublic(type, parameterValues);
                }
            }
            else
            {
                try
                {
                    createdObject = constructor.Invoke(parameterValues);
                }
                catch (Exception e)
                {
                    throw new Exception("对象创建失败：" + e.Message, e);
                }
            }
            return createdObject;
        }

        /// <summary>常见的几个单建模式的实例创建
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        private static object CreatedObjectBySingleMode(Type type, object[] parameterValues)
        {
            object createdObject = null;
            bool isSueess = false;
            const string ME_LOWER = "Me";
            const string ME = "ME";
            const string INSTANCE = "Instance";
            PropertyInfo propertyMe = type.GetProperty(ME);
            PropertyInfo propertyMeLower = type.GetProperty(ME_LOWER);
            PropertyInfo propertyInstance = type.GetProperty(INSTANCE);
            MethodInfo methodMe = type.GetMethod(ME);
            MethodInfo methodInstance = type.GetMethod(INSTANCE);
            if (propertyMe != null)
            {
                createdObject = propertyMe.GetValue(null, null);
                isSueess = true;
            }
            if (!isSueess && propertyInstance != null)
            {
                createdObject = propertyInstance.GetValue(null, null);
                isSueess = true;
            }
            if (!isSueess && methodInstance != null)
            {
                createdObject = methodInstance.Invoke(null, null);
                isSueess = true;
            }
            if (!isSueess && propertyMeLower != null)
            {
                createdObject = propertyMeLower.GetValue(null, null);
                isSueess = true;
            }
            if (!isSueess && methodMe != null)
            {
                createdObject = methodMe.Invoke(null, null);
                isSueess = true;
            }
            if (!isSueess)
                createdObject = CreatedObjectByNonPublic(type, parameterValues);
            return createdObject;
        }

        /// <summary>从私有构造函数创建
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        private static object CreatedObjectByNonPublic(Type type, object[] parameterValues)
        {
            try
            {
                const BindingFlags BINDING_FLAGS = BindingFlags.CreateInstance | (BindingFlags.NonPublic | (BindingFlags.Public | BindingFlags.Instance));
                return Activator.CreateInstance(type, BINDING_FLAGS, null, parameterValues, null);
            }
            catch (Exception e)
            {
                throw new Exception("即将创建的类型不支持指定的构造函数：" + e.Message, e);
            }
        }

        /// <summary>从类型中创建此类型的实例（本方法不支持参数可为Null的构造函数）
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="expectedType">期望的类型</param>
        /// <param name="throwOnError">失败时是否抛出异常</param>
        /// <param name="parameters">创建实例所需的参数值列表</param>
        /// <returns>类型实例</returns>
        public static object CreateObject(Type type, Type expectedType, bool throwOnError, params object[] parameters)
        {
            int paramNum = 0;
            if (parameters != null)
            {
                paramNum = parameters.Length;
            }
            var paramTypes = new Type[paramNum];
            var paramValues = new object[paramNum];
            for (int i = 0; i < paramNum; i++)
            {
                if (parameters == null) continue;
                if (parameters[i] == null)
                {
                    if (throwOnError)
                    {
                        throw new Exception("不支持参数可为Null的构造函数，请使用本方法的另外重载版本");
                    }
                    return null;
                }
                paramTypes[i] = parameters[i].GetType();
                paramValues[i] = parameters[i];
            }
            return CreateObject(type, expectedType, throwOnError, paramTypes, paramValues);
        }

        /// <summary>从类型名中创建此类型的实例
        /// </summary>
        /// <param name="assembly">类型所在程序集.</param>
        /// <param name="typeName">类型名</param>
        /// <param name="expectedType">期望的类型</param>
        /// <param name="throwOnError">失败时是否抛出异常</param>
        /// <param name="parameters">创建实例所需的参数值列表</param>
        /// <returns>类型实例</returns>
        public static object CreateObject(Assembly assembly, string typeName, Type expectedType, bool throwOnError, params object[] parameters)
        {
            Type type = CreateType(assembly, typeName, throwOnError);
            return CreateObject(type, expectedType, throwOnError, parameters);
        }

        /// <summary>从类型名中创建此类型的实例
        /// </summary>
        /// <param name="assembly">类型所在程序集.</param>
        /// <param name="typeName">类型名</param>
        /// <param name="expectedType">期望的类型</param>
        /// <param name="throwOnError">失败时是否抛出异常</param>
        /// <param name="parameterTypes">创建实例所需参数的类型列表</param>
        /// <param name="parameterValues">创建实例所需的参数值列表</param>
        /// <returns>类型实例</returns>
        public static object CreateObject(Assembly assembly, string typeName, Type expectedType, bool throwOnError, Type[] parameterTypes, object[] parameterValues)
        {
            Type type = CreateType(assembly, typeName, throwOnError);
            return CreateObject(type, expectedType, throwOnError, parameterTypes, parameterValues);
        }

        /// <summary>在指定的目录中查找指定的类型
        /// </summary>
        /// <param name="typeName">类型全名（包括命名空间）</param>
        /// <param name="path"></param>
        /// <returns>找到则返回指定的类型，否则返回空</returns>
        public static Type FindType(string typeName, string path)
        {
            if (String.IsNullOrWhiteSpace(typeName))
            {
                throw new ArgumentNullException();
            }
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException(path + "不存在");
            }
            Dictionary<string, Type> typeMap = null;
            if (!_appTypes.ContainsKey(path))
            {
                typeMap = FindTypeMap(path);
                _appTypes.Add(path, typeMap);
            }
            else
            {
                typeMap = _appTypes[path];
            }
            if (typeMap != null && typeMap.ContainsKey(typeName))
            {
                return typeMap[typeName];
            }
            return null;
        }

        /// <summary>在当前应用程序域中查找指定的类型
        /// </summary>
        /// <param name="typeName">类型全名（包括命名空间）</param>
        /// <returns>找到则返回指定的类型，否则返回空</returns>
        public static Type FindType(string typeName)
        {
            return FindType(typeName, AppDomain.CurrentDomain.BaseDirectory);
        }

        /// <summary>从指定的目录找到所有Type，并返回Type全名为Key，Type为Value的Map
        /// </summary>
        /// <param name="path">指定的目录.</param>
        /// <returns></returns>
        public static Dictionary<string, Type> FindTypeMap(string path)
        {
            if (_appTypes.ContainsKey(path))
            {
                return _appTypes[path];
            }
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException(path + "目录不存在。");
            }
            var typeMap = new Dictionary<string, Type>();
            var assemblys = UtilityAssembly.SearchAssemblyByDirectory(path);
            foreach (var assembly in assemblys)
            {
                Type[] types;
                try
                {
                    types = assembly.GetTypes();
                }
                catch (Exception e)
                {
                    continue;
                }
                foreach (var type in types)
                {
                    if (type.FullName == null)
                        continue;
                    if (!typeMap.ContainsKey(type.FullName))
                        typeMap.Add(type.FullName, type);
                }
            }
            return typeMap;
        }

        /// <summary>从指定的目录中找到所有的.Net程序集，并遍历所有程序集找到所有实现了指定接口或基类的类型
        /// </summary>
        /// <param name="path">指定的目录</param>
        /// <param name="targetType">指定接口的类型</param>
        /// <param name="isGenericTypeInterface">是否是泛型接口</param>
        /// <param name="containAbstract">是否包含虚类型</param>
        /// <returns></returns>
        public static IEnumerable<Type> FindTypesByDirectory(string path, Type targetType, bool isGenericTypeInterface = false, bool containAbstract = false)
        {
            if (!_appTypes.ContainsKey(path))
            {
                _appTypes.Add(path, FindTypeMap(path));
            }
            var typemap = _appTypes[path];
            var list = new List<Type>();
            foreach (var type in typemap.Values)
            {
                if (!containAbstract && type.IsAbstract)
                {
                    continue;
                }
                if (!isGenericTypeInterface)
                {
                    if (type.ContainsInterface(targetType))
                        list.Add(type);
                    else if(type.IsSubclassOf(targetType))
                        list.Add(type);
                }
                else
                {
                    if (type.ContainsGenericInterface(targetType))
                        list.Add(type);
                }
            }
            return list;
        }

        /// <summary>
        /// 从目录中找到所有的.Net程序集，并遍历所有程序集找到所有指定的定制特性
        /// </summary>
        /// <param name="appStartPath">The app start path.</param>
        /// <returns></returns>
        public static T[] FindAttributes<T>(string appStartPath) where T : Attribute
        {
            var typeList = new List<T>();
            Assembly[] assArray = UtilityAssembly.SearchAssemblyByDirectory(appStartPath);
            if (UtilityCollection.IsNullOrEmpty(assArray))
                return typeList.ToArray();

            Parallel.ForEach(assArray, ass =>
            {
                Type[] types = null;
                try
                {
                    types = ass.GetTypes();
                }
                catch (Exception e)
                {
                    Debug.Fail(string.Format("Assembly.GetTypes()异常:{0}", e.Message));
                }
                if (!UtilityCollection.IsNullOrEmpty(types))
                {
                    Parallel.ForEach(types, type =>
                    {
                        object[] attrs = type.GetCustomAttributes(true);
                        typeList.AddRange(attrs.Where(attr => attr.GetType() == typeof (T)).Cast<T>());
                    });
                }
            });
            return typeList.ToArray();
        }

        /// <summary>
        /// 从目录中找到所有的.Net程序集，并遍历所有程序集找到所有指定的定制特性
        /// </summary>
        /// <param name="appStartPath">The app start path.</param>
        /// <returns></returns>
        public static List<Pair<T, Type>> FindAttributeMap<T>(string appStartPath) where T : Attribute
        {
            var list = new List<Pair<T, Type>>();
            Assembly[] assArray = UtilityAssembly.SearchAssemblyByDirectory(appStartPath);
            if (UtilityCollection.IsNullOrEmpty(assArray))
                return list;

            Parallel.ForEach(assArray, ass =>
            {
                Type[] types = null;
                try
                {
                    types = ass.GetTypes();
                }
                catch (Exception e)
                {
                    Debug.Fail(string.Format("程序集获取Type失败。{0}", e.Message));
                }
                if (!UtilityCollection.IsNullOrEmpty(types))
                {
                    Parallel.ForEach(types, type =>
                    {
                        object[] attrs = type.GetCustomAttributes(true);
                        if (!UtilityCollection.IsNullOrEmpty(attrs))
                        {
                            list.AddRange(from attr in attrs
                                where attr.GetType() == typeof (T)
                                select new Pair<T, Type>()
                                {
                                    First = (T) attr,
                                    Second = type
                                });
                        }
                    });
                }
            });
            return list;
        }

        /// <summary>
        /// 从目录中找到所有的.Net程序集，并遍历所有程序集找到所有拥有指定的定制特性的类型
        /// </summary>
        /// <param name="appStartPath">The app start path.</param>
        /// <param name="targetAttribute">指定接口的类型</param>
        /// <returns></returns>
        public static Type[] FindAttributesByDirectory(string appStartPath, Type targetAttribute)
        {
            var typeList = new List<Type>();
            Assembly[] assArray = UtilityAssembly.SearchAssemblyByDirectory(appStartPath);
            if (UtilityCollection.IsNullOrEmpty(assArray))
                return typeList.ToArray();

            Parallel.ForEach(assArray, ass =>
            {
                Type[] types = null;
                try
                {
                    types = ass.GetTypes();
                }
                catch (Exception e)
                {
                    Debug.Fail(string.Format("程序集获取Type失败。{0}", e.Message));
                }
                if (!UtilityCollection.IsNullOrEmpty(types))
                {
                    typeList.AddRange(from type in types
                        let attrs = type.GetCustomAttributes(true)
                        where attrs.Any(attr => attr.GetType() == targetAttribute)
                        select type);
                }
            });
            return typeList.ToArray();
        }

        /// <summary>
        /// 从程序集中获得元属性
        /// </summary>
        /// <param name="assemblies">程序集，如果为null，则从当前应用程序域中获取所载入的所有程序集</param>
        /// <returns>找到的元属性的数组</returns>
        public static T[] GetAttributeFromAssembly<T>(Assembly[] assemblies) where T : Attribute
        {
            var list = new List<T>();
            if (assemblies == null)
            {
                assemblies = UtilityAssembly.SearchAssemblyByDirectory(AppDomain.CurrentDomain.BaseDirectory);
            }

            Parallel.ForEach(assemblies, assembly =>
            {
                var attributes = (T[]) assembly.GetCustomAttributes(typeof (T), false);
                if (attributes.Length > 0)
                {
                    list.AddRange(attributes);
                }
            });
            return list.ToArray();
        }

        /// <summary>
        /// 从运行时的堆栈中获取元属性
        /// </summary>
        /// <param name="includeAll">是否包含堆栈上所有的元属性</param>
        /// <typeparam name="T">元属性类型</typeparam>
        /// <returns>找到的元属性的数组</returns>
        public static T[] GetAttributeFromRuntimeStack<T>(bool includeAll) where T : Attribute
        {
            var list = new List<T>();
            var t = new StackTrace();
            for (int i = 0; i < t.FrameCount; i++)
            {
                StackFrame f = t.GetFrame(i);
                var m = (MethodInfo) f.GetMethod();
                var a = Attribute.GetCustomAttributes(m, typeof (T)) as T[];
                if (a != null && a.Length > 0)
                {
                    list.AddRange(a);
                    if (!includeAll)
                    {
                        break;
                    }
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 指定的目标类型是否实现了指定的接口类型
        /// </summary>
        /// <param name="targetType">指定的目标类型.</param>
        /// <param name="implType">指定的接口类型.</param>
        /// <returns>
        /// 	<c>true</c> if the specified target type contains interface; otherwise, <c>false</c>.
        /// </returns>
        public static bool ContainsInterface(this Type targetType, Type implType)
        {
            var interfaces = targetType.GetInterfaces();
            return interfaces.Contains(implType);
        }

        /// <summary>
        /// 指定的目标类型是否实现了指定的【泛型接口】类型
        /// </summary>
        /// <param name="targetType">指定的目标类型.</param>
        /// <param name="implType">指定的接口类型.</param>
        /// <returns>
        /// 	<c>true</c> if the specified target type contains interface; otherwise, <c>false</c>.
        /// </returns>
        public static bool ContainsGenericInterface(this Type targetType, Type implType)
        {
            return targetType.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == implType);
        }

        /// <summary>
        /// 指定的目标类型是否实现了指定的定制特性
        /// </summary>
        /// <param name="targetType">指定的目标类型.</param>
        /// <param name="attribute">指定的定制特性.</param>
        /// <returns>
        /// 	<c>true</c> if the specified target type contains CustomAttribute; otherwise, <c>false</c>.
        /// </returns>
        public static bool ContainsCustomAttribute(this Type targetType, Type attribute)
        {
            object[] attrs = targetType.GetCustomAttributes(true);
            return attrs.Any(attr => attr.GetType() == attribute);
        }

        /// <summary>尝试获取定制特性，如该类型没有指定的定制特性，将为空值
        /// </summary>
        /// <param name="targetType">Type of the target.</param>
        /// <returns></returns>
        public static Attribute GetFirstCustomAttribute(this Type targetType)
        {
            object[] attrs = targetType.GetCustomAttributes(true);
            if (attrs.Length > 0)
                return (Attribute) attrs[0];
            return null;
        }

        /// <summary>尝试获取指定类型的定制特性，如该类型没有指定的定制特性，将为空值
        /// </summary>
        /// <param name="targetType">Type of the target.</param>
        /// <returns></returns>
        public static T GetFirstCustomAttribute<T>(this Type targetType)
        {
            object[] attrs = targetType.GetCustomAttributes(typeof (T), true);
            if (attrs.Length > 0)
                return (T) attrs[0];
            return default(T);
        }

        /// <summary>
        /// 根据类型的名称创建一个对象（无参的构造函数）, 考虑了从程序目录中所有的程序集进行创建
        /// </summary>
        /// <param name="klass">The klass.</param>
        /// <returns></returns>
        public static object CreateSimpleObject(string klass)
        {
            Type type = FindType(klass);
            if (null != type)
            {
                try
                {
                    return Activator.CreateInstance(type);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// 一个XML的节点，有name属性，其值为定义的接口名；有class属性，其值是实现了该接口的类的全名；
        /// 通过该方法快速创建该类型。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static Pair<string, T> InterfaceBuilder<T>(XmlNode node)
        {
            if (node == null || node.Attributes == null || node.Attributes.Count <= 0)
            {
                throw new ArgumentNullException("node", @"参数不能为空");
            }
            string name = node.Attributes["name"].Value;
            string classname = node.Attributes["class"].Value;
            if (!String.IsNullOrWhiteSpace(classname))
            {
                Type type = FindType(classname);
                object klass = CreateObject(type, typeof (T), true);
                var pair = new Pair<string, T> {First = name, Second = (T) klass};
                return pair;
            }
            return new Pair<string, T> {First = name, Second = default(T)};
        }

        /// <summary>
        /// 一个XML的节点，有name属性，其值为定义的接口名；有class属性，其值是实现了该接口的类的全名；
        /// 通过该方法快速创建该类型。接收返回值时，必须校验是否为空。
        /// 当有多个实现时，通过过滤器委托判断是否是需要的接口实现。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <param name="typeFilter"></param>
        /// <returns></returns>
        public static Pair<string, T> CoderSettingClassBuilder<T>(XmlNode node, Func<Type, bool> typeFilter)
        {
            if (node == null || node.ChildNodes.Count <= 0)
            {
                throw new ArgumentNullException("node", @"参数不能为空");
            }
            var typeList = new List<Type>();
            XmlNode selectSingleNode = node.SelectSingleNode("Interface");
            if (selectSingleNode == null)
            {
                throw new ArgumentNullException("node", @"Node中的关键参数不能解析到");
            }
            string interfaceName = selectSingleNode.InnerText;
            var classNameList = node.SelectNodes("ClassName");
            for (int i = 0; i < classNameList.Count; i++)
            {
                XmlNode nd = classNameList[i];
                string className = nd.InnerText;
                if (!String.IsNullOrWhiteSpace(className))
                {
                    Type type = FindType(className);
                    typeList.Add(type);
                }
            }
            Type finalType = null;
            foreach (Type type in typeList)
            {
                if (typeFilter.Invoke(type))
                    finalType = type;
            }
            object klass = CreateObject(finalType, typeof (T), true);
            var pair = new Pair<string, T> {First = interfaceName, Second = (T) klass};
            return pair;
        }

        /// <summary>
        /// 一个XML的节点，有name属性，其值为定义的接口名；有class属性，其值是实现了该接口的类的全名；
        /// 通过该方法快速创建该类型。接收返回值时，必须校验是否为空。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static Pair<string, T> CoderSettingClassBuilder<T>(XmlNode node)
        {
            if (node == null || node.ChildNodes.Count <= 0)
            {
                throw new ArgumentNullException("node", "参数不能为空");
            }
            var interfaceNode = node.SelectSingleNode("Interface");
            var classnameNode = node.SelectSingleNode("ClassName");

            if (interfaceNode != null && classnameNode != null)
            {
                string interfaceName = interfaceNode.InnerText;
                string className = classnameNode.InnerText;
                if (!String.IsNullOrWhiteSpace(className))
                {
                    object klass = CreateObject(FindType(className), typeof (T), true);
                    var pair = new Pair<string, T> {First = interfaceName, Second = (T) klass};
                    return pair;
                }
                return new Pair<string, T> {First = interfaceName, Second = default(T)};
            }
            return new Pair<string, T>();
        }
    }
}