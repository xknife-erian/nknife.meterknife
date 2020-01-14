using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web.Services.Description;
using Microsoft.CSharp;

namespace NKnife.Utility
{
    /// <summary>
    /// 动态调用WebService soap服务，避免了通过vs服务引用的方式，有些场合不具备添加服务引用的条件
    /// </summary>
    public class UtilityWebService
    {
        /// <summary>
        /// 动态调用WebService
        /// </summary>
        /// <param name="url">WebService地址</param>
        /// <param name="methodname">方法名(模块名)</param>
        /// <param name="args">参数列表</param>
        /// <returns>object</returns>
        public static object InvokeWebService(string url, string methodname, object[] args)
        {
            return InvokeWebService(url, null, methodname, args);
        }

        /// <summary>
        ///     动态调用WebService
        /// </summary>
        /// <param name="url">WebService地址</param>
        /// <param name="classname">类名</param>
        /// <param name="methodname">方法名(模块名)</param>
        /// <param name="args">参数列表</param>
        /// <returns>object</returns>
        public static object InvokeWebService(string url, string classname, string methodname, object[] args)
        {
            try
            {
                const string @namespace = "ServiceBase.WebService.DynamicWebLoad";
                if (string.IsNullOrEmpty(classname))
                {
                    classname = GetClassName(url);
                }

                //获取服务描述语言(WSDL)  
                var wc = new WebClient();
                Stream stream = wc.OpenRead(url + "?WSDL"); //这里如果服务无法访问，则抛出异常
                ServiceDescription sd = ServiceDescription.Read(stream);
                var sdi = new ServiceDescriptionImporter();
                sdi.AddServiceDescription(sd, "", "");
                var cn = new CodeNamespace(@namespace);
                //生成客户端代理类代码  
                var ccu = new CodeCompileUnit();
                ccu.Namespaces.Add(cn);
                sdi.Import(cn, ccu);
                var csc = new CSharpCodeProvider();
                ICodeCompiler icc = csc.CreateCompiler();
                //设定编译器的参数  
                var cplist = new CompilerParameters();
                cplist.GenerateExecutable = false;
                cplist.GenerateInMemory = true;
                cplist.ReferencedAssemblies.Add("System.dll");
                cplist.ReferencedAssemblies.Add("System.XML.dll");
                cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                cplist.ReferencedAssemblies.Add("System.Data.dll");

                //编译代理类  
                CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
                if (cr.Errors.HasErrors)
                {
                    var sb = new StringBuilder();
                    foreach (CompilerError ce in cr.Errors)
                    {
                        sb.Append(ce);
                        sb.Append(Environment.NewLine);
                    }
                    throw new Exception(sb.ToString());
                }


                //生成代理实例,并调用方法  
                Assembly assembly = cr.CompiledAssembly;
                Type t = assembly.GetType(@namespace + "." + classname, true, true);
                object obj = Activator.CreateInstance(t);
                MethodInfo mi = t.GetMethod(methodname);
                return mi.Invoke(obj, args);
            }
            catch (Exception ex) //异常信息直接返回
            {
                return ex.Message;
            }
        }


        private static string GetClassName(string url)
        {
            string[] parts = url.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');
            return pps[0];
        }
    }
}
