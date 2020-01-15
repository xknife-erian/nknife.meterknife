using System;
using System.Collections;
using System.Reflection;

namespace NKnife.Base
{
    /// <summary>  
    /// CloneableObject类是一个用来继承的抽象类。   
    /// 每一个由此类继承而来的类将自动支持克隆方法。  
    /// 该类实现了ICloneable接口，并且每个从该对象继承而来的对象都将同样地  
    /// 支持ICloneable接口。   
    /// </summary>   
    public abstract class CloneableObject : ICloneable
    {
        #region ICloneable Members

        /// <summary>      
        /// 克隆对象，并返回一个已克隆对象的引用      
        /// </summary>      
        /// <returns>引用新的克隆对象</returns>       
        public object Clone()
        {
            //首先建立指定类型的一个实例           
            object newObject = Activator.CreateInstance(GetType());
            //取得新的类型实例的字段数组。           
            FieldInfo[] fields = newObject.GetType().GetFields();
            int i = 0;
            foreach (FieldInfo fi in GetType().GetFields())
            {
                //判断字段是否支持ICloneable接口。               
                Type cloneType = fi.FieldType.GetInterface("ICloneable", true);
                if (cloneType != null)
                {
                    //取得对象的Icloneable接口。                   
                    var clone = (ICloneable) fi.GetValue(this);
                    //使用克隆方法给字段设定新值。                  
                    fields[i].SetValue(newObject, clone.Clone());
                }
                else
                {
                    // 如果该字段部支持Icloneable接口，直接设置即可。                   
                    fields[i].SetValue(newObject, fi.GetValue(this));
                }
                //现在检查该对象是否支持IEnumerable接口，如果支持，               
                //还需要枚举其所有项并检查他们是否支持IList 或 IDictionary 接口。              
                Type enumerableType = fi.FieldType.GetInterface("IEnumerable", true);
                if (enumerableType != null)
                {
                    //取得该字段的IEnumerable接口                  
                    var em = (IEnumerable) fi.GetValue(this);
                    Type listType = fields[i].FieldType.GetInterface("IList", true);
                    Type dicType = fields[i].FieldType.GetInterface("IDictionary", true);
                    int j = 0;
                    if (listType != null)
                    {
                        //取得IList接口。                       
                        var list = (IList) fields[i].GetValue(newObject);
                        foreach (object obj in em)
                        {
                            //查看当前项是否支持支持ICloneable 接口。                           
                            cloneType = obj.GetType().GetInterface("ICloneable", true);
                            if (cloneType != null)
                            {
                                //如果支持ICloneable 接口，               
                                //用它李设置列表中的对象的克隆             
                                var clone = (ICloneable) obj;
                                list[j] = clone.Clone();
                            }
                            //注意：如果列表中的项不支持ICloneable接口，那么                        
                            //在克隆列表的项将与原列表对应项相同                        
                            //（只要该类型是引用类型）                          
                            j++;
                        }
                    }
                    else if (dicType != null)
                    {
                        //取得IDictionary 接口                      
                        var dic = (IDictionary) fields[i].GetValue(newObject);
                        j = 0;
                        foreach (DictionaryEntry de in em)
                        {
                            //查看当前项是否支持支持ICloneable 接口。                           
                            cloneType = de.Value.GetType().
                                GetInterface("ICloneable", true);
                            if (cloneType != null)
                            {
                                var clone = (ICloneable) de.Value;
                                dic[de.Key] = clone.Clone();
                            }
                            j++;
                        }
                    }
                }
                i++;
            }
            return newObject;
        }

        #endregion
    }
}