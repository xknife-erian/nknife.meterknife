using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NKnife.Entities;

namespace NKnife.Wrapper
{
    public class ClassLoader<T> where T:class 
    {
        /// <summary>
        /// 在指定的路径中查找继承了I的类
        /// </summary>
        /// <param name="loaderPath">文件夹列表</param>
        /// <returns>查找的结果</returns>
        public static List<AssemblyWrapper<T>> GetInstanceByInterface(params string[] loaderPath) 
        {
            var list = new List<AssemblyWrapper<T>>();
            Action<string> loader = s =>
            {
                IEnumerable<AssemblyWrapper<T>> slist = GetServicesInAssembly(s);
                if (slist != null) list.AddRange(slist);
            };
            Action<string> folderLoader = s =>
            {
                string[] files = Directory.GetFiles(s, "*.exe");
                Array.ForEach(files, loader);

                files = Directory.GetFiles(s, "*.dll");
                Array.ForEach(files, loader);
            };

            folderLoader(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            Array.ForEach(loaderPath, folderLoader);

            return list;
        }

        /// <summary>
        /// 通过文件路径来查找所有服务
        /// </summary>
        /// <param name="assemblyPath"></param>
        /// <returns></returns>
        private static IEnumerable<AssemblyWrapper<T>> GetServicesInAssembly(string assemblyPath)
        {
            try
            {
                return GetServicesInAssembly(Assembly.LoadFile(assemblyPath));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 查找指定程序集中所有的服务类
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        private static IEnumerable<AssemblyWrapper<T>> GetServicesInAssembly(Assembly assembly)
        {
            Type[] types = assembly.GetTypes();

            var typeList = new List<AssemblyWrapper<T>>();
            Array.ForEach(types, s =>
            {
                if (!s.IsPublic || s.IsAbstract) return;
                Type t = s.GetInterface(typeof(T).FullName);
                if (t == null) return;

                ConstructorInfo constInfo = s.GetConstructor(Type.EmptyTypes);
                if (constInfo == null) return;

                T instance = constInfo.Invoke(new object[] { }) as T;
                if (instance == null) return;

                var info = new AssemblyWrapper<T>
                {
                    Assembly = Path.GetFileName(assembly.Location),
                    TypeName = s.FullName,
                    ClassInstance = instance
                };
                typeList.Add(info);
            });

            return typeList.ToArray();
        }
    }
}
