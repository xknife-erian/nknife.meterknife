using System;
using System.Linq;

namespace NKnife.Configuring.OptionCase
{
    /// <summary>往往应用程序的选项可以是多份，每一份在匹配的场景或时段下被使用，在这里我们理解一份选项是一个广义的实例。
    /// 本类型描述的就是一个选项实例。
    /// </summary>
    [Serializable]
    public class OptionCaseItem : IEquatable<OptionCaseItem>, ICloneable
    {
        public OptionCaseItem()
        {
            IsDefault = false;
        }

        /// <summary>配置名称
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>配置方案执行效率模式
        /// </summary>
        /// <value>The work mode.</value>
        public OptionCaseMode WorkMode { get; set; }

        /// <summary>按周为频率时，星期的集合
        /// </summary>
        /// <value>The day of weeks.</value>
        public DayOfWeek[] DayOfWeeks { get; set; }

        /// <summary>按月为频率时，天的集合
        /// </summary>
        /// <value>The count of month.</value>
        public CountOfMonth CountOfMonth { get; set; }

        /// <summary>指定时间
        /// </summary>
        /// <value>The specify.</value>
        public DateTime Specify { get; set; }

        /// <summary>是否是默认方案
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is default; otherwise, <c>false</c>.
        /// </value>
        public bool IsDefault { get; set; }

        /// <summary>指定的时间是否匹配
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public bool Matching(DateTime dt)
        {
            var today = DateTime.Now;
            switch (WorkMode)
            {
                case OptionCaseMode.Specify:
                    if (Specify.Date.Equals(today.Date))
                        return true;
                    break;
                case OptionCaseMode.Month:
                    if (CountOfMonth.Matching(today))
                        return true;
                    break;
                case OptionCaseMode.Week:
                    if (DayOfWeeks.Contains(today.DayOfWeek))
                        return true;
                    break;
            }
            return false;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (OptionCaseItem)) return false;
            return Equals((OptionCaseItem) obj);
        }

        public bool Equals(OptionCaseItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Name, Name);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        public object Clone()
        {
            var oci = new OptionCaseItem
                          {
                              CountOfMonth = CountOfMonth, 
                              DayOfWeeks = DayOfWeeks, 
                              IsDefault = IsDefault, 
                              Name = Name, 
                              Specify = Specify, 
                              WorkMode = WorkMode
                          };
            return oci;
        }

        public static bool operator ==(OptionCaseItem left, OptionCaseItem right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(OptionCaseItem left, OptionCaseItem right)
        {
            return !Equals(left, right);
        }

        /// <summary>生成一套选项的基本用例
        /// </summary>
        /// <returns></returns>
        public static OptionCaseItem GetBase()
        {
            var os = new OptionCaseItem
                         {
                             IsDefault = true, 
                             Name = "Default", 
                             WorkMode = OptionCaseMode.Week, 
                             DayOfWeeks = new[]
                                              {
                                                  DayOfWeek.Friday, 
                                                  DayOfWeek.Monday, 
                                                  DayOfWeek.Saturday, 
                                                  DayOfWeek.Sunday,
                                                  DayOfWeek.Thursday, 
                                                  DayOfWeek.Tuesday, 
                                                  DayOfWeek.Wednesday
                                              }
                         };
            return os;
        }
    }
}
